using System;
using System.Data;
using System.Data.SqlClient;

namespace SkillBridge
{
    public partial class Tutor_List : System.Web.UI.Page
    {
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTutors();
            }
        }

        // RETRIEVE TUTORS FROM DATABASE
        private void BindTutors()
        {
            string search = txtSearch.Text.Trim().Replace("'", "''");
            string category = ddlCategory.SelectedValue.Replace("'", "''");

            string query = "SELECT " +
                           "U.UserID AS TutorID, " +
                           "U.Name AS TutorName, " +
                           "S.SkillName, " +
                           "S.Category, " +
                           "US.ProficiencyLevel, " +
                           "ISNULL(AVG(CAST(R.Rating AS DECIMAL(3,1))), 0) AS Rating, " +
                           "COUNT(R.ReviewID) AS ReviewCount " +
                           "FROM Users U " +
                           "INNER JOIN UserSkills US ON U.UserID = US.UserID " +
                           "INNER JOIN Skills S ON US.SkillID = S.SkillID " +
                           "LEFT JOIN SessionRequests SR ON SR.TeacherID = U.UserID " +
                           "LEFT JOIN Reviews R ON R.RequestID = SR.RequestID " +
                           "WHERE U.Role = 'Tutor' " +
                           "AND US.Type = 'Teach' " +
                           "AND ('" + search + "' = '' OR S.SkillName LIKE '%" + search + "%') " +
                           "AND ('" + category + "' = '' OR S.Category = '" + category + "') " +
                           "GROUP BY U.UserID, U.Name, S.SkillName, S.Category, US.ProficiencyLevel " +
                           "ORDER BY U.Name ASC";

            try
            {
                // Create a new database connection
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g\source\repos\SkillBridge\App_Data\SkilledBridge.mdf;Integrated Security=True");

                // Open database connection
                con.Open();

                // Associate SQL statement with command object
                cmd = new SqlCommand(query, con);

                // Execute SQL statement
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                // Display tutors in the Repeater
                rptTutors.DataSource = dt;
                rptTutors.DataBind();

                // Display number of tutors found
                lblTutorCount.Text = dt.Rows.Count + " tutors found";

                // Display message if no tutors are available
                pnlNoTutors.Visible = dt.Rows.Count == 0;

                lblError.Visible = false;

                // Close database connection
                con.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "Unable to load tutors: " + ex.Message;
                lblError.Visible = true;

                pnlNoTutors.Visible = false;
            }
        }

        // SEARCH BY SKILL
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindTutors();
        }

        // CATEGORY DROPDOWN
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTutors();
        }

        // CATEGORY FILTER BUTTONS
        protected void btnAll_Click(object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "";
            BindTutors();
        }

        protected void btnProgramming_Click(object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Programming";
            BindTutors();
        }

        protected void btnDesign_Click(object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Design";
            BindTutors();
        }

        protected void btnLanguages_Click(object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Languages";
            BindTutors();
        }

        protected void btnPublicSpeaking_Click(object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Public Speaking";
            BindTutors();
        }

        protected void btnOfficeTools_Click(object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Office Tools";
            BindTutors();
        }
    }
}
