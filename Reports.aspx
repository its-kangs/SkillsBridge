<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SkillBridge.Reports" %>
<%@ Register Src="~/SiteHeader.ascx" TagPrefix="uc" TagName="SiteHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
