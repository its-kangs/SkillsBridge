using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SkillBridge
{
    public partial class TutorProfile : System.Web.UI.Page
    {
        SqlCommand cmd;

        // GETS THE TUTOR ID FROM THE URL
        private int TeacherId
        {
            get
            {
                int id;

                if (int.TryParse(Request.QueryString["TeacherID"], out id))
                {
                    return id;
                }
                else if (int.TryParse(Request.QueryString["TutorID"], out id))
                {
                    return id;
                }
                else
                {
                    return 1;
                }
            }
        }

        // CHECKS WHETHER ALL REVIEWS SHOULD BE DISPLAYED
        private bool ShowAllReviews
        {
            get
            {
                if (ViewState["ShowAllReviews"] == null)
                    return false;

                return (bool)ViewState["ShowAllReviews"];
            }
            set
            {
                ViewState["ShowAllReviews"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTutorProfile();
                LoadTutorSkills();
                LoadTutorReviews();
            }
        }

        // LOADS THE TUTOR'S INFORMATION
        private void LoadTutorProfile()
        {
            string query = "SELECT U.Name, U.Bio, " +
                           "ISNULL((SELECT AVG(CAST(R.Rating AS FLOAT)) " +
                           "FROM Reviews R " +
                           "INNER JOIN SessionRequests SR ON R.RequestID = SR.RequestID " +
                           "WHERE SR.TeacherID = U.UserID), 0) AS AvgRating, " +
                           "(SELECT COUNT(*) " +
                           "FROM Reviews R " +
                           "INNER JOIN SessionRequests SR ON R.RequestID = SR.RequestID " +
                           "WHERE SR.TeacherID = U.UserID) AS TotalReviews " +
                           "FROM Users U " +
                           "WHERE U.UserID = " + TeacherId;

            // Create database connection
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

            // Open database connection
            con.Open();

            // Associate SQL statement with command object
            cmd = new SqlCommand(query, con);

            // Execute SQL statement
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                lblTutorName.Text = reader["Name"].ToString();

                if (reader["Bio"] == DBNull.Value || String.IsNullOrEmpty(reader["Bio"].ToString()))
                {
                    lblAbout.Text = "This tutor has not added a bio yet.";
                }
                else
                {
                    lblAbout.Text = reader["Bio"].ToString();
                }

                double rating = Convert.ToDouble(reader["AvgRating"]);
                int count = Convert.ToInt32(reader["TotalReviews"]);

                lblAverageRating.Text = rating.ToString("0.0");
                lblReviewCount.Text = count.ToString();
            }
            else
            {
                lblTutorName.Text = "Tutor Not Found";
                btnSendRequest.Enabled = false;
            }

            // Close reader and connection
            reader.Close();
            con.Close();
        }

        // LOADS THE TUTOR'S SKILLS
        private void LoadTutorSkills()
        {
            string query = "SELECT S.SkillID, S.SkillName " +
                           "FROM UserSkills US " +
                           "INNER JOIN Skills S ON US.SkillID = S.SkillID " +
                           "WHERE US.UserID = " + TeacherId + " " +
                           "ORDER BY S.SkillName ASC";

            DataTable dt = new DataTable();

            // Create database connection
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

            // Open database connection
            con.Open();

            // Associate SQL statement with command object
            cmd = new SqlCommand(query, con);

            // Execute SQL statement
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            // Close connection
            con.Close();

            // Display skills on the profile
            rptSkills.DataSource = dt;
            rptSkills.DataBind();

            // Add skills to the dropdown list
            ddlSkills.DataSource = dt;
            ddlSkills.DataTextField = "SkillName";
            ddlSkills.DataValueField = "SkillID";
            ddlSkills.DataBind();

            if (dt.Rows.Count == 0)
            {
                ddlSkills.Items.Insert(0, new ListItem("No skills listed", ""));
                btnSendRequest.Enabled = false;
            }
        }

        // LOADS THE TUTOR'S REVIEWS
        private void LoadTutorReviews()
        {
            // Count total reviews
            string countQuery = "SELECT COUNT(*) " +
                                "FROM Reviews R " +
                                "INNER JOIN SessionRequests SR ON R.RequestID = SR.RequestID " +
                                "WHERE SR.TeacherID = " + TeacherId;

            // Create database connection
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

            // Open database connection
            con.Open();

            // Associate SQL statement with command object
            cmd = new SqlCommand(countQuery, con);

            // Execute SQL statement
            int totalReviews = Convert.ToInt32(cmd.ExecuteScalar());

            // Display only two reviews unless Show All is clicked
            string top = "";

            if (ShowAllReviews == false)
            {
                top = "TOP 2";
            }

            string query = "SELECT " + top + " " +
                           "U.Name AS ReviewerName, R.Rating, R.Comment " +
                           "FROM Reviews R " +
                           "INNER JOIN SessionRequests SR ON R.RequestID = SR.RequestID " +
                           "INNER JOIN Users U ON R.ReviewerID = U.UserID " +
                           "WHERE SR.TeacherID = " + TeacherId + " " +
                           "ORDER BY R.ReviewDate DESC, R.ReviewID DESC";

            // Associate SQL statement with command object
            cmd = new SqlCommand(query, con);

            // Execute SQL statement
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            // Close connection
            con.Close();

            // Display reviews
            rptReviews.DataSource = dt;
            rptReviews.DataBind();

            lblNoReviews.Visible = (dt.Rows.Count == 0);

            int remaining = totalReviews - dt.Rows.Count;

            if (remaining > 0 && ShowAllReviews == false)
            {
                pnlMoreReviews.Visible = true;
                lblMoreReviewsCount.Text = remaining.ToString();
            }
            else
            {
                pnlMoreReviews.Visible = false;
            }
        }

        // CONVERTS A RATING INTO STARS
        protected string FormatStars(object ratingObj)
        {
            int rating = Convert.ToInt32(ratingObj);

            if (rating > 5)
                rating = 5;

            if (rating < 0)
                rating = 0;

            string stars = "";

            for (int i = 0; i < rating; i++)
            {
                stars = stars + "★";
            }

            for (int i = rating; i < 5; i++)
            {
                stars = stars + "☆";
            }

            return stars;
        }

        // DISPLAYS ALL REVIEWS
        protected void lnkShowMoreReviews_Click(object sender, EventArgs e)
        {
            ShowAllReviews = true;
            LoadTutorReviews();
        }

        // TAKES THE STUDENT TO THE SESSION REQUEST PAGE
        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ddlSkills.SelectedValue))
            {
                lblStatus.Text = "Please select a skill first.";
                lblStatus.Visible = true;
                return;
            }
            else
            {
                string skillId = ddlSkills.SelectedValue;

                Response.Redirect("Session_Request.aspx?TeacherID=" + TeacherId +
                                  "&SkillID=" + skillId);
            }
        }
    }
}
