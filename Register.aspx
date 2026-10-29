<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SkillBridge.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auth-wrap">
        <div class="auth-card">
            <div class="auth-title">Create your account</div>
            <div class="auth-sub">Join SkillBridge as a student or a tutor</div>

            <asp:Label ID="Lblrole" runat="server" Text="I am a" CssClass="auth-label"></asp:Label>
            <asp:DropDownList ID="Ddlrole" runat="server" CssClass="auth-input">
                <asp:ListItem>Select Role</asp:ListItem>
                <asp:ListItem>Student</asp:ListItem>
                <asp:ListItem>Tutor</asp:ListItem>
            </asp:DropDownList>

            <asp:Label ID="Lblname" runat="server" Text="Full Names" CssClass="auth-label"></asp:Label>
            <asp:TextBox ID="Txbfullname" runat="server" CssClass="auth-input"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txbfullname" ErrorMessage="Full Names Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>

            <asp:Label ID="Lblemail" runat="server" Text="Email Address" CssClass="auth-label"></asp:Label>
            <asp:TextBox ID="Txbemail" runat="server" CssClass="auth-input"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txbemail" ErrorMessage="Email Address Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txbemail" ErrorMessage="Invalid email. It must contain . and @" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgRegistration"></asp:RegularExpressionValidator>

            <asp:Label ID="Lblpassword" runat="server" Text="Password" CssClass="auth-label"></asp:Label>
            <asp:TextBox ID="Txbpassword" runat="server" TextMode="Password" CssClass="auth-input"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txbpassword" ErrorMessage="Password Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>

            <asp:Label ID="Lblconfirm" runat="server" Text="Confirm Password" CssClass="auth-label"></asp:Label>
            <asp:TextBox ID="Txbconfirmpass" runat="server" TextMode="Password" CssClass="auth-input"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txbconfirmpass" ErrorMessage="Confirm Password Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="Txbconfirmpass" ControlToCompare="Txbpassword" ErrorMessage="Passwords Do Not Match" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:CompareValidator>

            <asp:Label ID="Lblbio" runat="server" Text="Short Bio (optional)" CssClass="auth-label"></asp:Label>
            <asp:TextBox ID="Txbbio" runat="server" TextMode="MultiLine" Rows="3" CssClass="auth-input"></asp:TextBox>

            <asp:Button ID="Btncreateaccount" runat="server" Text="Create Account" CssClass="auth-button" OnClick="Btncreateaccount_Click" ValidationGroup="vgRegistration" />

            <asp:Label ID="Lblregister" runat="server" Text="" CssClass="auth-error"></asp:Label>

            <div class="auth-footer">
                Already have an account? <asp:HyperLink ID="LnkLogin" runat="server" NavigateUrl="~/Login.aspx">Login here</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
