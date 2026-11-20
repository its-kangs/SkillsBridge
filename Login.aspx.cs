using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Data.SqlClient;

namespace SkillBridge
{
    public partial class Login : System.Web.UI.Page
    {
        SqlCommand cmd; //can be accessed by all events in this page

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
                //declare local variables
                string email = Txbemail.Text;
                string hashed = HashPassword(Txbpassword.Text);

                //Declare SQL statement to be executed
                string sql = "SELECT UserID, Name, Role FROM Users WHERE Email = '" + email + "' AND PasswordHash = '" + hashed + "'";

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
                    //save user details in session
                    Session["UserID"] = reader["UserID"].ToString();
                    Session["UserName"] = reader["Name"].ToString();
                    Session["Role"] = reader["Role"].ToString();

                    reader.Close();

                    //Close database connection
                    con.Close();

                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    reader.Close();
                    con.Close();

                    Lbllogin.Text = "Invalid Login Credentials";
                    Lbllogin.ForeColor = System.Drawing.Color.Red;

                    return;
                }
            }
        }

        //turns the password into a SHA256 code
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
