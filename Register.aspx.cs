using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace SkillBridge
{
    public partial class Register : System.Web.UI.Page
    {
        SqlCommand cmd; //can be accessed by all events in this page

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btncreateaccount_Click(object sender, EventArgs e)
        {
            //check that a role was chosen
            if (Ddlrole.SelectedIndex == 0)
            {
                Lblregister.Text = "Please select if you are a Student or a Tutor";
                Lblregister.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (Txbpassword.Text.Length < 6)
            {
                Lblregister.Text = "Password must be at least 6 characters";
                Lblregister.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                //declare program variables
                string connString, email;
                connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
                email = Txbemail.Text.Trim();


                SqlConnection con = new SqlConnection(connString);
                con.Open();

                //check if the email is already used
                string checkEmail = "SELECT COUNT(*) FROM Users WHERE Email = '" + email + "'";

                //associate SQL statement with command object
                cmd = new SqlCommand(checkEmail, con);

                //execute SQL statement
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    con.Close();

                    Lblregister.Text = "That email is already registered";
                    Lblregister.ForeColor = System.Drawing.Color.Red;

                    return;
                }
                else
                {
                    //save the new user
                    SqlCommand cmd = new SqlCommand("INSERT INTO Users (Name, Email, PasswordHash, Role, Bio, date_joined) VALUES (@Name, @Email, @Hash, @Role, @Bio, GETDATE())", con);
                    cmd.Parameters.AddWithValue("@Name", Txbfullname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Hash", Txbpassword.Text);
                    cmd.Parameters.AddWithValue("@Role", Ddlrole.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Bio", Txbbio.Text.Trim());
                    cmd.ExecuteNonQuery();

                    //Close database connection
                    con.Close();

                    //display success message
                    Lblregister.Text = "Account created! You can now login.";
                    Lblregister.ForeColor = System.Drawing.Color.Green;

                    //Clear the textboxes after successful registration
                    Txbfullname.Text = "";
                    Txbemail.Text = "";
                    Txbpassword.Text = "";
                    Txbbio.Text = "";
                    Ddlrole.SelectedIndex = 0;
                }
            }
        }


    }
}