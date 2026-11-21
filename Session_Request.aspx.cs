using System;
using System.Data.SqlClient;

namespace SkillBridge
{
    public partial class SessionRequest : System.Web.UI.Page
    {
        SqlCommand cmd; //can be accessed by all events in this page

        //Runs when the page first loads
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //declare local variables
                int teacherID;
                int skillID;

                //Check URL parameters
                if (!int.TryParse(Request.QueryString["TeacherID"], out teacherID) ||
                    !int.TryParse(Request.QueryString["SkillID"], out skillID))
                {
                    lblError.Text = "Invalid tutor or skill selection.";
                    btnSend.Enabled = false;
                    return;
                }
                else
                {
                    //Declare SQL statement to be executed
                    string sql = "SELECT Users.Name, Skills.SkillName FROM UserSkills " +
                                 "INNER JOIN Users ON UserSkills.UserID = Users.UserID " +
                                 "INNER JOIN Skills ON UserSkills.SkillID = Skills.SkillID " +
                                 "WHERE UserSkills.UserID = " + teacherID +
                                 " AND UserSkills.SkillID = " + skillID +
                                 " AND UserSkills.Type = 'Teach'";

                    //Create a new instance of SqlConnection
                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

                    //Open db connection
                    con.Open();

                    //Associate SQL statement with command object
                    cmd = new SqlCommand(sql, con);

                    //Execute SQL statement
                    SqlDataReader reader = cmd.ExecuteReader();

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

                    reader.Close();

                    //Close database connection
                    con.Close();
                }
            }
        }

        //Runs when the student clicks the Send Request button
        protected void btnSend_Click(object sender, EventArgs e)
        {
            //Clear previous messages
            lblError.Text = "";
            lblSuccess.Text = "";

            //Check required fields
            if (string.IsNullOrWhiteSpace(txtDate.Text) ||
                string.IsNullOrWhiteSpace(txtTime.Text) ||
                string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                lblError.Text = "Please fill in all required fields.";
                return;
            }
            else if (Session["UserID"] == null)
            {
                lblError.Text = "Please log in first.";
                return;
            }
            else
            {
                //declare local variables
                int teacherID;
                int skillID;

                //Validate IDs from URL
                if (!int.TryParse(Request.QueryString["TeacherID"], out teacherID) ||
                    !int.TryParse(Request.QueryString["SkillID"], out skillID))
                {
                    lblError.Text = "Invalid tutor or skill selection.";
                    return;
                }

                int requesterID = Convert.ToInt32(Session["UserID"]);

                //Validate date and time
                DateTime requestDate;
                TimeSpan requestTime;

                if (!DateTime.TryParse(txtDate.Text, out requestDate))
                {
                    lblError.Text = "Please enter a valid date.";
                    return;
                }
                else if (!TimeSpan.TryParse(txtTime.Text, out requestTime))
                {
                    lblError.Text = "Please enter a valid time.";
                    return;
                }

                //declare local variables
                string location = txtLocation.Text.Trim().Replace("'", "''");
                string message;

                if (string.IsNullOrWhiteSpace(txtMessage.Text))
                {
                    message = "NULL";
                }
                else
                {
                    message = "'" + txtMessage.Text.Trim().Replace("'", "''") + "'";
                }

                string date = requestDate.ToString("yyyyMMdd");
                string time = requestTime.ToString(@"hh\:mm\:ss");

                //Declare SQL statement to be executed
                string sql = "INSERT INTO SessionRequests " +
                             "(RequesterID, TeacherID, SkillID, Request_Date, Request_Time, Location, status, Message) " +
                             "VALUES (" + requesterID + ", " + teacherID + ", " + skillID +
                             ", '" + date + "', '" + time + "', '" + location +
                             "', 'Pending', " + message + ")";

                //Create a new instance of SqlConnection
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

                //Open db connection
                con.Open();

                //Associate SQL statement with command object
                cmd = new SqlCommand(sql, con);

                //Execute SQL statement
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    //Display success message
                    lblSuccess.Text = "Your session request has been sent successfully!";

                    //Clear the textboxes
                    txtDate.Text = "";
                    txtTime.Text = "";
                    txtLocation.Text = "";
                    txtMessage.Text = "";
                }
                else
                {
                    lblError.Text = "The request could not be saved.";
                }

                //Close database connection
                con.Close();
            }
        }

        //Test database connection
        protected void TestDatabaseConnection()
        {
            try
            {
                //Create a new instance of SqlConnection
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

                //Open db connection
                con.Open();

                lblSuccess.Text = "DATABASE CONNECTED SUCCESSFULLY!";

                //Close database connection
                con.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "DATABASE CONNECTION FAILED: " + ex.Message;
            }
        }
    }
}
