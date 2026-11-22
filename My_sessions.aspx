<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="My_sessions.aspx.cs" Inherits="SkillBridge.My_sessions" %>
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
        <div class="ms-title">My Sessions</div>
        <p>
            <asp:DropDownList ID="Ddlstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddlstatus_SelectedIndexChanged" CssClass="ms-select">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>Accepted</asp:ListItem>
                <asp:ListItem>Completed</asp:ListItem>
                <asp:ListItem>Rejected</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:GridView ID="Gvsessions" runat="server" AutoGenerateSelectButton="True" DataKeyNames="ID" GridLines="None" CssClass="ms-grid">
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
            </asp:GridView>
        </p>
        <p>
            <asp:Button ID="Btnaccept" runat="server" Text="Accept" OnClick="Btnaccept_Click" CssClass="ms-btn" />
            <asp:Button ID="Btndecline" runat="server" Text="Decline" OnClick="Btndecline_Click" CssClass="ms-btn ms-btn-outline" />
            <asp:Button ID="Btncomplete" runat="server" Text="Mark Complete" OnClick="Btncomplete_Click" CssClass="ms-btn" />
        </p>
        <p>
            <asp:Label ID="Lblmessage" runat="server" Text=""></asp:Label>
        </p>
    </div>

</asp:Content>
