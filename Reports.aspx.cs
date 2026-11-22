using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SkillBridge
{
    public partial class Reports : System.Web.UI.Page
    {
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //send the user back to login if they are not logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                LoadReports();
            }

        }
        private void LoadReports()
        {
            //create a new instance of sql connection
            string connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);

            //open database connection
            con.Open();

            //1. total requests
            cmd = new SqlCommand("SELECT COUNT(*) FROM SessionRequests", con);
            int total = Convert.ToInt32(cmd.ExecuteScalar());
            Lbltotal.Text = "Total Requests: " + total;

            //2. sessions completed
            cmd = new SqlCommand("SELECT COUNT(*) FROM SessionRequests WHERE status = 'Completed'", con);
            int completed = Convert.ToInt32(cmd.ExecuteScalar());
            Lblcompleted.Text = "Sessions Completed: " + completed;

            //3. active tutors
            cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Tutor'", con);
            int tutors = Convert.ToInt32(cmd.ExecuteScalar());
            Lbltutors.Text = "Active Tutors: " + tutors;

            //4. most requested skills (top 5)
            string sql = "SELECT TOP 5 s.SkillName AS Skill, COUNT(*) AS Requests " +
                         "FROM SessionRequests q JOIN Skills s ON s.SkillID = q.SkillID " +
                         "GROUP BY s.SkillName ORDER BY COUNT(*) DESC";
            cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            Gvskills.DataSource = dr;
            Gvskills.DataBind();
            dr.Close();

            //5. most active tutors (top 5) with their average rating
            sql = "SELECT TOP 5 u.Name AS Tutor, COUNT(DISTINCT q.RequestID) AS Requests, " +
                  "ROUND(ISNULL(AVG(CAST(r.Rating AS FLOAT)), 0), 1) AS Rating " +
                  "FROM Users u JOIN SessionRequests q ON q.TeacherID = u.UserID " +
                  "LEFT JOIN Reviews r ON r.RequestID = q.RequestID " +
                  "GROUP BY u.UserID, u.Name ORDER BY COUNT(DISTINCT q.RequestID) DESC";
            cmd = new SqlCommand(sql, con);
            dr = cmd.ExecuteReader();
            Gvtutors.DataSource = dr;
            Gvtutors.DataBind();
            dr.Close();

            //close database connection
            con.Close();

        }
    }

}