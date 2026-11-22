using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;


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
                //declare program variables
                string connString, email;
                connString = ConfigurationManager.ConnectionStrings["SkillBridgeDB"].ConnectionString;
                email = Txbemail.Text.Trim();


                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("SELECT UserID, Name, Role FROM Users WHERE Email = @Email AND PasswordHash = @Hash", con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Hash", Txbpassword.Text);

                con.Open();

                //Execute SQL statement
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    Session["UserID"] = reader["UserID"].ToString();
                    Session["UserName"] = reader["Name"].ToString();
                    Session["Role"] = reader["Role"].ToString();

                    reader.Close();

                    //Close database connection
                    con.Close();
                    if (Session["Role"].ToString() == "Tutor")
                    {
                        Response.Redirect("~/My_sessions.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Tutor_List.aspx");
                    }
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