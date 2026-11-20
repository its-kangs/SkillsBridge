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
                //declare local variables
                string email = Txbemail.Text.Trim();
                string hashed = HashPassword(Txbpassword.Text);
                string name = Txbfullname.Text.Trim();
                string role = Ddlrole.SelectedItem.Text;
                string bio = Txbbio.Text.Trim();

                //Create a new instance of SqlConnection
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

                //Open db connection
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
                    //Declare SQL statement to save the new user
                    string sql = "INSERT INTO Users (Name, Email, PasswordHash, Role, Bio, date_joined) VALUES ('" + name + "','" + email + "','" + hashed + "','" + role + "','" + bio + "', GETDATE())";

                    //associate SQL statement with command object
                    cmd = new SqlCommand(sql, con);

                    //execute SQL statement
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

        //turns the password into a SHA256 code (same method used in Login)
        private string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();

            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            string result = "";

            foreach (byte b in bytes)
            {
                result = result + b.ToString("x2");
            }

            return result;
        }
    }
}
