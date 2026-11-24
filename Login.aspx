<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SkillBridge.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="reg-container">
        <h2>Welcome back</h2>
        <p class="reg-sub">Login to your SkillBridge account</p>

        <p>
            <asp:Label ID="Lblemail" runat="server" Text="Email Address"></asp:Label><br />
            <asp:TextBox ID="Txbemail" runat="server" CssClass="reg-input"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txbemail" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
        </p>

        <p>
            <asp:Label ID="Lblpassword" runat="server" Text="Password"></asp:Label><br />
            <asp:TextBox ID="Txbpassword" runat="server" TextMode="Password" CssClass="reg-input"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txbpassword" ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
        </p>

        <p>
            <asp:Button ID="Btnlogin" runat="server" Text="Login" CssClass="reg-btn" OnClick="Btnlogin_Click" ValidationGroup="vgLogin" />
        </p>

        <p>
            <asp:Label ID="Lbllogin" runat="server" Text=""></asp:Label>
        </p>

        <p class="reg-note">
            Don't have an account? <asp:HyperLink ID="LnkRegister" runat="server" NavigateUrl="~/Register.aspx">Register here</asp:HyperLink>
        </p>
    </div>
</asp:Content>