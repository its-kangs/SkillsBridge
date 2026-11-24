<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="My_sessions.aspx.cs" Inherits="SkillBridge.My_sessions" %>
<%@ Register Src="~/SiteHeader.ascx" TagPrefix="uc" TagName="SiteHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .sessions-box {
            padding: 15px 25px;
        }
        .sessions-box h2 {
            margin-bottom: 12px;
        }
        .sessions-table {
            width: 100%;
            border-collapse: collapse;
        }
        .sessions-table th {
            text-align: left;
            font-size: 12px;
            color: #666;
            border-bottom: 1px solid #ddd;
            padding: 8px 6px;
        }
        .sessions-table td {
            padding: 10px 6px;
            border-bottom: 1px solid #eee;
        }
        .session-btn {
            background-color: #203253;
            color: white;
            border: none;
            padding: 6px 12px;
            margin-right: 5px;
            cursor: pointer;
        }
        .session-btn:hover {
            background-color: #2d4a75;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:SiteHeader ID="SiteHeader1" runat="server" />
    <div class="sessions-box">
        <h2>My Sessions</h2>
        <p>
            Filter by status:<br />
            <asp:DropDownList ID="Ddlstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddlstatus_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>Accepted</asp:ListItem>
                <asp:ListItem>Completed</asp:ListItem>
                <asp:ListItem>Rejected</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <asp:GridView ID="Gvsessions" runat="server" AutoGenerateSelectButton="True" DataKeyNames="ID" GridLines="None" CssClass="sessions-table">
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
            </asp:GridView>
        </p>
        <p>
            <asp:Button ID="Btnaccept" runat="server" Text="Accept" OnClick="Btnaccept_Click" CssClass="session-btn" />
            <asp:Button ID="Btndecline" runat="server" Text="Decline" OnClick="Btndecline_Click" CssClass="session-btn" />
            <asp:Button ID="Btncomplete" runat="server" Text="Mark Complete" OnClick="Btncomplete_Click" CssClass="session-btn" />
        </p>
        <p>
            <asp:Label ID="Lblmessage" runat="server" Text=""></asp:Label>
        </p>
    </div>
</asp:Content>