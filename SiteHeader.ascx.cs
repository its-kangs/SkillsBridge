using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SkillBridge
{
    public partial class SiteHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            {
                //underline the link of the page we are on, like Tutor_List
                string page = System.IO.Path.GetFileName(Request.Path).ToLower();
                if (page == "tutor_list.aspx") Hlsearch.CssClass = "active";
                else if (page == "my_sessions.aspx") Hlsessions.CssClass = "active";
                else if (page == "my_skills.aspx") Hlskills.CssClass = "active";
                else if (page == "reports.aspx") Hlreports.CssClass = "active";

                //tutors do not need to find a tutor, students do not teach skills
                if (Session["Role"] != null && Session["Role"].ToString() == "Tutor")
                {
                    Hlsearch.Visible = false;
                }
                else
                {
                    Hlskills.Visible = false;
                }
            }
        }
        protected void Lnklogout_Click(object sender, EventArgs e)
        {
            //clear the session and go back to login
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");


        }
    }
}