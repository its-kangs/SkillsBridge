<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SkillBridge.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auth-wrap">
        <div class="auth-card">
            <div class="auth-title">Welcome back</div>
            <div class="auth-sub">Login to your SkillBridge account</div>

            <asp:Label ID="Lblemail" runat="server" Text="Email Address" CssClass="auth-label"></asp:Label>
            <asp:TextBox ID="Txbemail" runat="server" CssClass="auth-input"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txbemail" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>

            <asp:Label ID="Lblpassword" runat="server" Text="Password" CssClass="auth-label"></asp:Label>
            <asp:TextBox ID="Txbpassword" runat="server" TextMode="Password" CssClass="auth-input"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txbpassword" ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>

            <asp:Button ID="Btnlogin" runat="server" Text="Login" CssClass="auth-button" OnClick="Btnlogin_Click" ValidationGroup="vgLogin" />

            <asp:Label ID="Lbllogin" runat="server" Text="" CssClass="auth-error"></asp:Label>

            <div class="auth-footer">
                Don't have an account? <asp:HyperLink ID="LnkRegister" runat="server" NavigateUrl="~/Register.aspx">Register here</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
