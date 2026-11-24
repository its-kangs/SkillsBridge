<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SkillBridge.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="reg-container">
        <h2>Create your account</h2>
        <p class="reg-sub">Join SkillBridge as a student or a tutor</p>

        <p>
            <asp:Label ID="Lblrole" runat="server" Text="I am a"></asp:Label><br />
            <asp:DropDownList ID="Ddlrole" runat="server" CssClass="reg-input">
                <asp:ListItem>Select Role</asp:ListItem>
                <asp:ListItem>Student</asp:ListItem>
                <asp:ListItem>Tutor</asp:ListItem>
            </asp:DropDownList>
        </p>

        <p>
            <asp:Label ID="Lblname" runat="server" Text="Full Names"></asp:Label><br />
            <asp:TextBox ID="Txbfullname" runat="server" CssClass="reg-input"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txbfullname" ErrorMessage="Full Names Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
        </p>

        <p>
            <asp:Label ID="Lblemail" runat="server" Text="Email Address"></asp:Label><br />
            <asp:TextBox ID="Txbemail" runat="server" CssClass="reg-input"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txbemail" ErrorMessage="Email Address Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txbemail" ErrorMessage="Invalid email. It must contain . and @" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgRegistration"></asp:RegularExpressionValidator>
        </p>

        <p>
            <asp:Label ID="Lblpassword" runat="server" Text="Password"></asp:Label><br />
            <asp:TextBox ID="Txbpassword" runat="server" TextMode="Password" CssClass="reg-input"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txbpassword" ErrorMessage="Password Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
        </p>

        <p>
            <asp:Label ID="Lblconfirm" runat="server" Text="Confirm Password"></asp:Label><br />
            <asp:TextBox ID="Txbconfirmpass" runat="server" TextMode="Password" CssClass="reg-input"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txbconfirmpass" ErrorMessage="Confirm Password Is Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="Txbconfirmpass" ControlToCompare="Txbpassword" ErrorMessage="Passwords Do Not Match" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegistration"></asp:CompareValidator>
        </p>

        <p>
            <asp:Label ID="Lblbio" runat="server" Text="Short Bio (optional)"></asp:Label><br />
            <asp:TextBox ID="Txbbio" runat="server" TextMode="MultiLine" Rows="3" CssClass="reg-input"></asp:TextBox>
        </p>

        <p>
            <asp:Button ID="Btncreateaccount" runat="server" Text="Create Account" CssClass="reg-btn" OnClick="Btncreateaccount_Click" ValidationGroup="vgRegistration" />
        </p>

        <p>
            <asp:Label ID="Lblregister" runat="server" Text=""></asp:Label>
        </p>

        <p class="reg-note">
            Already have an account? <asp:HyperLink ID="LnkLogin" runat="server" NavigateUrl="~/Login.aspx">Login here</asp:HyperLink>
        </p>
    </div>
</asp:Content>