
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SkillBridge
{
    public partial class Tutor_List : System.Web.UI.Page
    {
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
            string query = @"
                SELECT
                    U.UserID AS TutorID,
                    U.Name AS TutorName,
                    S.SkillName,
                    S.Category,
                    US.ProficiencyLevel,

                    ISNULL(AVG(CAST(R.Rating AS DECIMAL(3,1))), 0)
                        AS Rating,

                    COUNT(R.ReviewID) AS ReviewCount

                FROM Users U

                INNER JOIN UserSkills US
                    ON U.UserID = US.UserID

                INNER JOIN Skills S
                    ON US.SkillID = S.SkillID

                LEFT JOIN SessionRequests SR
                    ON SR.TeacherID = U.UserID

                LEFT JOIN Reviews R
                    ON R.RequestID = SR.RequestID

                WHERE
                    U.Role = 'Tutor'
                    AND US.Type = 'Teach'
                    AND (
                        @Search = ''
                        OR S.SkillName LIKE '%' + @Search + '%'
                    )
                    AND (
                        @Category = ''
                        OR S.Category = @Category
                    )

                GROUP BY
                    U.UserID,
                    U.Name,
                    S.SkillName,
                    S.Category,
                    US.ProficiencyLevel

                ORDER BY U.Name ASC";

            try
            {
                string connectionString =
                    ConfigurationManager
                    .ConnectionStrings["SkillBridgeDB"]
                    .ConnectionString;

                using (SqlConnection con =
                    new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Search", txtSearch.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@Category", ddlCategory.SelectedValue);

                        using (SqlDataAdapter da =
                            new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();

                            da.Fill(dt);

                            rptTutors.DataSource = dt;
                            rptTutors.DataBind();

                            lblTutorCount.Text =
                                dt.Rows.Count + " tutors found";

                            pnlNoTutors.Visible =
                                dt.Rows.Count == 0;

                            lblError.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text =
                    "Unable to load tutors: " + ex.Message;

                lblError.Visible = true;

                pnlNoTutors.Visible = false;
            }
        }

        // SEARCH BY SKILL
        protected void txtSearch_TextChanged(
            object sender, EventArgs e)
        {
            BindTutors();
        }

        // CATEGORY DROPDOWN
        protected void ddlCategory_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            BindTutors();
        }

        // CATEGORY FILTER BUTTONS
        protected void btnAll_Click(
            object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "";
            BindTutors();
        }

        protected void btnProgramming_Click(
            object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Programming";
            BindTutors();
        }

        protected void btnDesign_Click(
            object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Design";
            BindTutors();
        }

        protected void btnLanguages_Click(
            object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Languages";
            BindTutors();
        }

        protected void btnPublicSpeaking_Click(
            object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Public Speaking";
            BindTutors();
        }

        protected void btnOfficeTools_Click(
            object sender, EventArgs e)
        {
            ddlCategory.SelectedValue = "Office Tools";
            BindTutors();
        }
    }
}
