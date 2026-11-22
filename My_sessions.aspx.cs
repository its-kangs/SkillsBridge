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
    public partial class My_sessions : System.Web.UI.Page

    {
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //send the user back to login if they are not logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else if (!IsPostBack)
            {
                LoadSessions();
            }

        }
        //fills the table with the sessions of the logged in user
        private void LoadSessions()
        {
            //declare local variables
            string connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
            int me = Convert.ToInt32(Session["UserID"]);

            //sql statement to execute
            string sql = "SELECT q.RequestID AS ID, s.SkillName AS Skill, " +
                         "CASE WHEN q.RequesterID = @Me THEN t.Name ELSE r.Name END AS Partner, " +
                         "CONVERT(varchar(12), q.Request_Date, 106) AS [Date], " +
                         "CONVERT(varchar(5), q.Request_Time, 108) AS [Time], " +
                         "q.Location, q.status AS Status, " +
                         "CASE WHEN q.RequesterID = @Me THEN 'Student' ELSE 'Tutor' END AS Role " +
                         "FROM SessionRequests q " +
                         "JOIN Skills s ON s.SkillID = q.SkillID " +
                         "JOIN Users r ON r.UserID = q.RequesterID " +
                         "JOIN Users t ON t.UserID = q.TeacherID " +
                         "WHERE (q.RequesterID = @Me OR q.TeacherID = @Me) " +
                         "AND (@Status = 'All' OR q.status = @Status) " +
                         "ORDER BY q.Request_Date DESC";

            //create a new instance of sql connection
            SqlConnection con = new SqlConnection(connString);

            //open database connection
            con.Open();

            //set up our command
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Me", me);
            cmd.Parameters.AddWithValue("@Status", Ddlstatus.SelectedValue);

            //declare object of type SQLREADER
            SqlDataReader dr = cmd.ExecuteReader();

            //pull records and display them on gridview using data reader
            Gvsessions.SelectedIndex = -1;
            Gvsessions.DataSource = dr;
            Gvsessions.DataBind();

            con.Close();
        }

        //runs when the user picks another status in the dropdown
        protected void Ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSessions();
        }

        protected void Btnaccept_Click(object sender, EventArgs e)
        {
            //the tutor accepts a pending request
            ChangeStatus("Accepted", "Pending", "TeacherID");
        }

        protected void Btndecline_Click(object sender, EventArgs e)
        {
            //the tutor declines a pending request
            ChangeStatus("Rejected", "Pending", "TeacherID");
        }

        protected void Btncomplete_Click(object sender, EventArgs e)
        {
            //the student marks an accepted session as complete
            ChangeStatus("Completed", "Accepted", "RequesterID");
        }

        //changes the status of the selected session
        private void ChangeStatus(string newStatus, string oldStatus, string column)
        {
            //check that a session was selected
            if (Gvsessions.SelectedIndex == -1)
            {
                Lblmessage.Text = "Please select a session first";
                Lblmessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            //declare local variables
            string connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
            int id = Convert.ToInt32(Gvsessions.SelectedDataKey.Value);
            int me = Convert.ToInt32(Session["UserID"]);

            //sql statement to execute (only works if the status and the user are right)
            string sql = "UPDATE SessionRequests SET status = @New WHERE RequestID = @Id AND status = @Old AND " + column + " = @Me";

            //create a new instance of sql connection
            SqlConnection con = new SqlConnection(connString);

            //open database connection
            con.Open();

            //set up our command
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@New", newStatus);
            cmd.Parameters.AddWithValue("@Old", oldStatus);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Me", me);

            //execute sql statement (rows is how many records were changed)
            int rows = cmd.ExecuteNonQuery();

            //close database connection
            con.Close();

            if (rows == 0)
            {
                Lblmessage.Text = "You cannot do that on this session";
                Lblmessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                LoadSessions();
                Lblmessage.Text = "Session updated";
                Lblmessage.ForeColor = System.Drawing.Color.Green;
            }

        }
    }
}