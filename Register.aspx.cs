using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace SkillBridge
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btncreateaccount_Click(object sender, EventArgs e)
        {
            //check that a role was chosen (validators cannot check the dropdown)
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
                string connString, email, hashed;
                connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
                email = Txbemail.Text.Trim();
                hashed = HashPassword(Txbpassword.Text);

                SqlConnection con = new SqlConnection(connString);
                con.Open();

                //check if the email is already used
                SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", con);
                check.Parameters.AddWithValue("@Email", email);
                int count = Convert.ToInt32(check.ExecuteScalar());

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
                    cmd.Parameters.AddWithValue("@Hash", hashed);
                    cmd.Parameters.AddWithValue("@Role", Ddlrole.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Bio", Txbbio.Text.Trim());
                    cmd.ExecuteNonQuery();
                    con.Close();

                    //display success message
                    Lblregister.Text = "Account created! You can now login.";
                    Lblregister.ForeColor = System.Drawing.Color.Green;
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
