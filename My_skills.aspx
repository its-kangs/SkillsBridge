<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="My_skills.aspx.cs" Inherits="SkillBridge.My_skills" %>
<%@ Register Src="~/SiteHeader.ascx" TagPrefix="uc" TagName="SiteHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
    <uc:SiteHeader ID="SiteHeader1" runat="server" />
     <div class="ms-wrap">
        <div class="ms-title">My Skills</div>
        <p>
            What can you teach?
        </p>
        <p>
            <asp:DropDownList ID="Ddlskill" runat="server" CssClass="ms-select"></asp:DropDownList>
            <asp:Button ID="Btnadd" runat="server" Text="Add Skill" CssClass="ms-btn" OnClick="Btnadd_Click" />
        </p>
        <p>
            <asp:Label ID="Lblmessage" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <asp:GridView ID="Gvmyskills" runat="server" GridLines="None" CssClass="ms-grid"></asp:GridView>
        </p>
    </div>

</asp:Content>
