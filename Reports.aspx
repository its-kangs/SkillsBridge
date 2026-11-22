<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SkillBridge.Reports" %>
<%@ Register Src="~/SiteHeader.ascx" TagPrefix="uc" TagName="SiteHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
    .ms-wrap { padding: 0 32px 25px 32px; }
    .ms-title { font-size: 27px; font-weight: bold; color: #252525; margin-bottom: 19px; }
    .ms-select { height: 38px; min-width: 180px; padding: 0 12px; font-size: 13px; background: white; border: 1px solid #dde3ea; border-radius: 4px; }
    .ms-grid { width: 100%; border-collapse: collapse; font-size: 13px; }
    .ms-grid th { text-align: left; font-size: 11px; font-weight: normal; text-transform: uppercase; color: #999; border-bottom: 1px solid #dde3ea; padding: 8px 6px; }
    .ms-grid td { padding: 12px 6px; border-bottom: 1px solid #eee; }
    .ms-btn { background: #203253; color: white; border: 1px solid #203253; border-radius: 4px; font-size: 11px; padding: 6px 10px; margin-right: 4px; cursor: pointer; }
    .ms-btn:hover { background: #2d4a75; }
</style>
    <uc:SiteHeader ID="SiteHeader1" runat="server" />
     <div class="ms-wrap">
        <div class="ms-title">Reports</div>
        <p>
            <asp:Label ID="Lbltotal" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <asp:Label ID="Lblcompleted" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <asp:Label ID="Lbltutors" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <b>Most Requested Skills</b>
        </p>
        <p>
            <asp:GridView ID="Gvskills" runat="server" GridLines="None" CssClass="ms-grid"></asp:GridView>
        </p>
        <p>
            <b>Most Active Tutors</b>
        </p>
        <p>
            <asp:GridView ID="Gvtutors" runat="server" GridLines="None" CssClass="ms-grid"></asp:GridView>
        </p>
    </div>

</asp:Content>
