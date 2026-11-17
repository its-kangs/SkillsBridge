
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SkillBridge
{
    public partial class SessionRequest : System.Web.UI.Page
    {
        // Creates a connection to the SkillBridge database
        SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString);


        // Runs when the page first loads

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int teacherID;
                int skillID;

                // Check URL parameters
                if (!int.TryParse(Request.QueryString["TeacherID"], out teacherID) ||
                    !int.TryParse(Request.QueryString["SkillID"], out skillID))
                {
                    lblError.Text = "Invalid tutor or skill selection.";
                    btnSend.Enabled = false;
                    return;
                }

                string query = @"
            SELECT Users.Name, Skills.SkillName
            FROM UserSkills
            INNER JOIN Users
                ON UserSkills.UserID = Users.UserID
            INNER JOIN Skills
                ON UserSkills.SkillID = Skills.SkillID
            WHERE UserSkills.UserID = @TeacherID
            AND UserSkills.SkillID = @SkillID
            AND UserSkills.Type = 'Teach'";

                try
                {
                    using (SqlConnection con = new SqlConnection(
                        ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                            cmd.Parameters.AddWithValue("@SkillID", skillID);

                            con.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    lblTeacher.Text = reader["Name"].ToString();
                                    lblSkill.Text = reader["SkillName"].ToString();
                                }
                                else
                                {
                                    lblError.Text = "Tutor or skill not found.";
                                    btnSend.Enabled = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "Error loading tutor: " + ex.Message;
                    btnSend.Enabled = false;
                }
            }
        }



        // Runs when the student clicks the Send Request button

        protected void btnSend_Click(object sender, EventArgs e)
        {
           
            try
            {
                lblError.Text = "";
                lblSuccess.Text = "";

                // Check required fields
                if (string.IsNullOrWhiteSpace(txtDate.Text) ||
                    string.IsNullOrWhiteSpace(txtTime.Text) ||
                    string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    lblError.Text = "Please fill in all required fields.";
                    return;
                }

                // Check login
                if (Session["UserID"] == null)
                {
                    lblError.Text = "Please log in first.";
                    return;
                }

                // Validate IDs from URL
                if (!int.TryParse(Request.QueryString["TeacherID"], out int teacherID) ||
                    !int.TryParse(Request.QueryString["SkillID"], out int skillID))
                {
                    lblError.Text = "Invalid tutor or skill selection.";
                    return;
                }

                int requesterID = Convert.ToInt32(Session["UserID"]);

                // Validate date and time
                if (!DateTime.TryParse(txtDate.Text, out DateTime requestDate))
                {
                    lblError.Text = "Please enter a valid date.";
                    return;
                }

                if (!TimeSpan.TryParse(txtTime.Text, out TimeSpan requestTime))
                {
                    lblError.Text = "Please enter a valid time.";
                    return;
                }

                string query = @"
            INSERT INTO SessionRequests
            (RequesterID, TeacherID, SkillID, Request_Date,
             Request_Time, Location, status, Message)
            VALUES
            (@RequesterID, @TeacherID, @SkillID, @Date,
             @Time, @Location, @Status, @Message)";

                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RequesterID", requesterID);
                        cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                        cmd.Parameters.AddWithValue("@SkillID", skillID);
                        cmd.Parameters.AddWithValue("@Date", requestDate.Date);
                        cmd.Parameters.AddWithValue("@Time", requestTime);
                        cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                        cmd.Parameters.AddWithValue("@Message",
                            string.IsNullOrWhiteSpace(txtMessage.Text)
                            ? (object)DBNull.Value
                            : txtMessage.Text.Trim());

                        con.Open();

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            lblSuccess.Text =
                                "Your session request has been sent successfully!";

                            txtDate.Text = "";
                            txtTime.Text = "";
                            txtLocation.Text = "";
                            txtMessage.Text = "";
                        }
                        else
                        {
                            lblError.Text = "The request could not be saved.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error sending request: " + ex.Message;
            }
        }
        protected void TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString))
                {
                    con.Open();
                    lblSuccess.Text = "DATABASE CONNECTED SUCCESSFULLY!";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "DATABASE CONNECTION FAILED: " + ex.Message;
            }
        }

    }

}
