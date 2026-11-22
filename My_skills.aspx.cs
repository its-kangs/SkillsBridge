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
    public partial class My_skills : System.Web.UI.Page
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
                LoadSkills();
                LoadMySkills();
            }
        }

        //fill the dropdown with all skills
        private void LoadSkills()
        {
            string connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            cmd = new SqlCommand("SELECT SkillID, SkillName FROM Skills ORDER BY SkillName", con);
            SqlDataReader dr = cmd.ExecuteReader();
            Ddlskill.DataSource = dr;
            Ddlskill.DataTextField = "SkillName";
            Ddlskill.DataValueField = "SkillID";
            Ddlskill.DataBind();
            dr.Close();

            con.Close();
        }

        //show the skills this tutor already teaches
        private void LoadMySkills()
        {
            string connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            string sql = "SELECT s.SkillName AS Skill, s.Category " +
                         "FROM UserSkills us JOIN Skills s ON s.SkillID = us.SkillID " +
                         "WHERE us.UserID = @Me AND us.Type = 'Teach'";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Me", Session["UserID"].ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            Gvmyskills.DataSource = dr;
            Gvmyskills.DataBind();
            dr.Close();

            con.Close();
        }

        protected void Btnadd_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            //check if the tutor already added this skill
            cmd = new SqlCommand("SELECT COUNT(*) FROM UserSkills WHERE UserID = @Me AND SkillID = @Skill AND Type = 'Teach'", con);
            cmd.Parameters.AddWithValue("@Me", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Skill", Ddlskill.SelectedValue);
            int exists = Convert.ToInt32(cmd.ExecuteScalar());

            if (exists > 0)
            {
                Lblmessage.Text = "You already teach this skill";
                Lblmessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                cmd = new SqlCommand("INSERT INTO UserSkills (UserID, SkillID, Type) VALUES (@Me, @Skill, 'Teach')", con);
                cmd.Parameters.AddWithValue("@Me", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Skill", Ddlskill.SelectedValue);
                cmd.ExecuteNonQuery();

                Lblmessage.Text = "Skill added";
                Lblmessage.ForeColor = System.Drawing.Color.Green;
            }

            con.Close();
            LoadMySkills();
        }
    }
}