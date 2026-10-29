using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace SkillBridge
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btnlogin_Click(object sender, EventArgs e)
        {
            //check that both boxes are filled
            if (String.IsNullOrEmpty(Txbemail.Text))
            {
                Lbllogin.Text = "Please enter your email";
                Lbllogin.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (String.IsNullOrEmpty(Txbpassword.Text))
            {
                Lbllogin.Text = "Please enter your password";
                Lbllogin.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                //declare program variables
                string connString, email, hashed;
                connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
                email = Txbemail.Text.Trim();
                hashed = HashPassword(Txbpassword.Text);

                //connect to the database
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("SELECT UserID, Name, Role FROM Users WHERE Email = @Email AND PasswordHash = @Hash", con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Hash", hashed);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //save the user details in session then go to dashboard
                    Session["UserID"] = reader["UserID"].ToString();
                    Session["UserName"] = reader["Name"].ToString();
                    Session["Role"] = reader["Role"].ToString();
                    reader.Close();
                    con.Close();
                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    reader.Close();
                    con.Close();
                    Lbllogin.Text = "Invalid Login Credentials";
                    Lbllogin.ForeColor = System.Drawing.Color.Red;
                    return;//Halts the login operation
                }
            }
        }

        //turns the password into a SHA256 code (same method used in Register)
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
