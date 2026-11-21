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
                string connString, email;
                connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
                email = Txbemail.Text.Trim();


                //connect to the database
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("SELECT UserID, Name, Role FROM Users WHERE Email = @Email AND PasswordHash = @Hash", con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Hash", Txbpassword.Text);


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    Session["UserID"] = reader["UserID"].ToString();
                    Session["UserName"] = reader["Name"].ToString();
                    Session["Role"] = reader["Role"].ToString();
                    reader.Close();
                    con.Close();
                    Response.Redirect("~/Tutor_List.aspx");
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




    }
}