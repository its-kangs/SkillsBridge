<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteHeader.ascx.cs" Inherits="SkillBridge.SiteHeader" %>
<style>
    .hdr-wrap { padding: 25px 32px 0 32px; font-family: Arial, sans-serif; }
    .hdr-wrap .tutor-header { display: flex; justify-content: space-between; align-items: center; border-bottom: 1px solid #dce3eb; padding: 0 0 18px; margin-bottom: 30px; }
    .hdr-wrap .brand { font-size: 27px; font-weight: bold; color: #203253; }
    .hdr-wrap .brand span { color: #5c9bd4; font-weight: normal; }
    .hdr-wrap .header-links { display: flex; align-items: center; gap: 28px; }
    .hdr-wrap .header-links a { color: #444; text-decoration: none; font-size: 16px; }
    .hdr-wrap .header-links a:hover { color: #568fcb; }
    .hdr-wrap .header-links a.active { color: #203253; font-weight: bold; border-bottom: 3px solid #203253; padding-bottom: 7px; }
    .hdr-wrap .profile-circle { width: 44px; height: 44px; border-radius: 50%; background: #d7e8f4; display: inline-block; }
</style>
<div class="hdr-wrap">
    <div class="tutor-header">
        <div class="brand">Skill<span>Bridge</span></div>
        <div class="header-links">
            <asp:HyperLink ID="Hlsearch" runat="server" NavigateUrl="~/Tutor_List.aspx">Search</asp:HyperLink>
            <asp:HyperLink ID="Hlsessions" runat="server" NavigateUrl="~/My_sessions.aspx">My Sessions</asp:HyperLink>
            <asp:HyperLink ID="Hlskills" runat="server" NavigateUrl="~/My_skills.aspx">My Skills</asp:HyperLink>
            <asp:HyperLink ID="Hlreports" runat="server" NavigateUrl="~/Reports.aspx">Reports</asp:HyperLink>
            <asp:LinkButton ID="Lnklogout" runat="server" OnClick="Lnklogout_Click">Logout</asp:LinkButton>
            <span class="profile-circle"></span>
        </div>
    </div>
</div>
