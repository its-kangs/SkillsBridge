
<%@ Page Title="Session Request" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeBehind="Session_Request.aspx.cs" Inherits="SkillBridge.SessionRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="session-wrap">

        <div class="session-card">

            <h1 class="session-title">Request a session</h1>

            <p class="session-sub">
                with <asp:Label ID="lblTeacher" runat="server" Text="Faith Njeri"></asp:Label>
                ·
                <asp:Label ID="lblSkill" runat="server" Text="Graphic Design"></asp:Label>
            </p>

            <div class="session-row">

                <div class="session-field">
                    <label>PROPOSED DATE</label>
                    <asp:TextBox ID="txtDate" runat="server"
                        CssClass="session-input" TextMode="Date">
                    </asp:TextBox>
                </div>

                <div class="session-field">
                    <label>TIME</label>
                    <asp:TextBox ID="txtTime" runat="server"
                        CssClass="session-input" TextMode="Time">
                    </asp:TextBox>
                </div>

            </div>

            <div class="session-field">
                <label>LOCATION</label>
                <asp:TextBox ID="txtLocation" runat="server"
                    CssClass="session-input"
                    placeholder="e.g. Library, 2nd floor">
                </asp:TextBox>
            </div>

            <div class="session-field">
                <label>MESSAGE TO TUTOR (OPTIONAL)</label>
                <asp:TextBox ID="txtMessage" runat="server"
                    CssClass="session-input session-message"
                    TextMode="MultiLine"
                    placeholder="I'd like help with logo design for a class project...">
                </asp:TextBox>
            </div>

            <asp:Label ID="lblError" runat="server"
                CssClass="session-error">
            </asp:Label>

            <asp:Label ID="lblSuccess" runat="server"
                CssClass="session-success">
            </asp:Label>

            <div class="session-buttons">

                <asp:Button ID="btnCancel" runat="server"
                    Text="Cancel"
                    CssClass="session-cancel"
                    CausesValidation="false"
                    OnClientClick="history.back(); return false;" />

       <asp:Button ID="btnSend" runat="server"
    Text="Send Request"
    CssClass="session-submit"
    OnClick="btnSend_Click" />

            </div>

            <p class="session-footer">
                The tutor will accept or decline your request
            </p>

        </div>

    </div>

</asp:Content>

