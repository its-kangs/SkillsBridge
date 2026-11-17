
<%@ Page Title="Find a Tutor" Language="C#"
    MasterPageFile="~/Site1.master"
    AutoEventWireup="true"
    CodeBehind="Tutor_List.aspx.cs"
    Inherits="SkillBridge.Tutor_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
    .tutor-page {
        font-family: Arial, sans-serif;
        color: #252525;
        padding: 25px 32px;
        min-height: 90vh;
        background: #fff;
    }

    .tutor-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        border-bottom: 1px solid #dce3eb;
        padding: 0 0 18px;
        margin-bottom: 30px;
    }

    .brand {
        font-size: 27px;
        font-weight: bold;
        color: #203253;
    }

    .brand span {
        color: #5c9bd4;
        font-weight: normal;
    }

    .header-links {
        display: flex;
        align-items: center;
        gap: 28px;
    }

    .header-links a {
        color: #444;
        text-decoration: none;
        font-size: 16px;
    }

    .header-links a.active {
        color: #203253;
        font-weight: bold;
        border-bottom: 3px solid #203253;
        padding-bottom: 7px;
    }

    .profile-circle {
        width: 44px;
        height: 44px;
        border-radius: 50%;
        background: #d7e8f4;
        display: inline-block;
    }

    .page-title {
        font-size: 27px;
        font-weight: bold;
        margin: 0 0 19px;
    }

    .search-row {
        display: grid;
        grid-template-columns: 2fr 1.05fr;
        gap: 13px;
        margin-bottom: 18px;
    }

    .search-input, .category-select {
        width: 100%;
        height: 53px;
        padding: 0 16px;
        border: 1px solid #dce3eb;
        border-radius: 7px;
        font-size: 16px;
        color: #555;
        background: white;
        box-sizing: border-box;
    }

    .category-filters {
        display: flex;
        flex-wrap: wrap;
        gap: 11px;
        margin: 17px 0 25px;
    }

    .filter-btn {
        display: inline-block;
        padding: 11px 18px;
        border: 1px solid #dce3eb;
        border-radius: 25px;
        color: #666;
        background: white;
        text-decoration: none;
        font-size: 15px;
        cursor: pointer;
    }

    .filter-btn.active {
        color: white;
        background: #203253;
        border-color: #203253;
    }

    .results-count {
        color: #888;
        font-size: 15px;
        margin: 0 0 18px;
    }

    .tutor-grid {
        display: grid;
        grid-template-columns: repeat(3, minmax(0, 1fr));
        gap: 21px;
    }

    .tutor-card {
        border: 1px solid #dce3eb;
        border-radius: 11px;
        padding: 19px;
        min-height: 190px;
        box-sizing: border-box;
        background: #fff;
        transition: box-shadow .2s ease;
    }

    .tutor-card:hover {
        box-shadow: 0 4px 14px rgba(32, 50, 83, .09);
    }

    .tutor-top {
        display: flex;
        align-items: center;
        gap: 13px;
        margin-bottom: 13px;
    }

    .tutor-avatar {
        width: 61px;
        height: 61px;
        min-width: 61px;
        border-radius: 50%;
        background: #d7e8f4;
    }

    .tutor-name {
        font-size: 19px;
        font-weight: bold;
        margin-bottom: 4px;
        color: #252525;
    }

    .tutor-rating {
        color: #858585;
        font-size: 13px;
    }

    .stars {
        color: #888;
        letter-spacing: 1px;
    }

    .tutor-skills {
        font-size: 15px;
        color: #555;
        margin-bottom: 13px;
    }

    .tutor-level {
        font-size: 14px;
        color: #999;
        margin-bottom: 12px;
    }

    .request-btn {
        display: inline-block;
        padding: 11px 15px;
        background: #203253;
        color: white !important;
        border: none;
        border-radius: 6px;
        text-decoration: none;
        font-size: 14px;
        cursor: pointer;
    }

    .request-btn:hover {
        background: #30496f;
    }

    .empty-message {
        padding: 30px;
        text-align: center;
        color: #888;
        border: 1px dashed #dce3eb;
        border-radius: 10px;
        margin-top: 15px;
    }

    .error-message {
        display: block;
        margin: 10px 0;
        color: #c0392b;
    }

    .tutor-footer {
        text-align: center;
        color: #aaa;
        font-size: 12px;
        border-top: 1px solid #ddd;
        margin-top: 28px;
        padding: 17px;
    }

    @media(max-width: 850px) {
        .tutor-grid {
            grid-template-columns: repeat(2, minmax(0, 1fr));
        }
    }

    @media(max-width: 600px) {
        .tutor-page {
            padding: 18px 14px;
        }

        .search-row {
            grid-template-columns: 1fr;
        }

        .tutor-grid {
            grid-template-columns: 1fr;
        }

        .header-links {
            gap: 12px;
        }
    }
</style>

<div class="tutor-page">

    <!-- HEADER -->
    <div class="tutor-header">

        <div class="brand">
            Skill<span>Bridge</span>
        </div>

        <div class="header-links">
            <a href="Tutor_List.aspx" class="active">Search</a>
            <a href="My_Sessions.aspx">My Sessions</a>
            <span class="profile-circle"></span>
        </div>

    </div>

    <!-- PAGE TITLE -->
    <h1 class="page-title">Find a tutor</h1>

    <!-- SEARCH AND CATEGORY -->
    <div class="search-row">

        <asp:TextBox
            ID="txtSearch"
            runat="server"
            CssClass="search-input"
            placeholder="Search by skill, e.g. &quot;Excel&quot;"
            AutoPostBack="true"
            OnTextChanged="txtSearch_TextChanged">
        </asp:TextBox>

        <asp:DropDownList
            ID="ddlCategory"
            runat="server"
            CssClass="category-select"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">

            <asp:ListItem Text="Category: All" Value="" />
            <asp:ListItem Text="Programming" Value="Programming" />
            <asp:ListItem Text="Design" Value="Design" />
            <asp:ListItem Text="Languages" Value="Languages" />
            <asp:ListItem Text="Public Speaking" Value="Public Speaking" />
            <asp:ListItem Text="Office Tools" Value="Office Tools" />

        </asp:DropDownList>

    </div>

    <!-- CATEGORY FILTERS -->
    <div class="category-filters">

        <asp:LinkButton ID="btnAll" runat="server"
            CssClass="filter-btn active"
            CausesValidation="false"
            OnClick="btnAll_Click">All</asp:LinkButton>

        <asp:LinkButton ID="btnProgramming" runat="server"
            CssClass="filter-btn"
            CausesValidation="false"
            OnClick="btnProgramming_Click">Programming</asp:LinkButton>

        <asp:LinkButton ID="btnDesign" runat="server"
            CssClass="filter-btn"
            CausesValidation="false"
            OnClick="btnDesign_Click">Design</asp:LinkButton>

        <asp:LinkButton ID="btnLanguages" runat="server"
            CssClass="filter-btn"
            CausesValidation="false"
            OnClick="btnLanguages_Click">Languages</asp:LinkButton>

        <asp:LinkButton ID="btnPublicSpeaking" runat="server"
            CssClass="filter-btn"
            CausesValidation="false"
            OnClick="btnPublicSpeaking_Click">Public Speaking</asp:LinkButton>

        <asp:LinkButton ID="btnOfficeTools" runat="server"
            CssClass="filter-btn"
            CausesValidation="false"
            OnClick="btnOfficeTools_Click">Office Tools</asp:LinkButton>

    </div>

    <!-- RESULTS COUNT -->
    <p class="results-count">
        <asp:Label ID="lblTutorCount" runat="server"
            Text="0 tutors found">
        </asp:Label>
    </p>

    <!-- ERROR MESSAGE -->
    <asp:Label ID="lblError" runat="server"
        CssClass="error-message"
        Visible="false">
    </asp:Label>

    <!-- TUTOR CARDS -->
    <div class="tutor-grid">

        <asp:Repeater ID="rptTutors" runat="server">

            <ItemTemplate>

                <div class="tutor-card">

                    <div class="tutor-top">

                        <div class="tutor-avatar"></div>

                        <div>

                            <div class="tutor-name">
                                <%#: Eval("TutorName") %>
                            </div>

                            <div class="tutor-rating">
                                <span class="stars">★★★★★</span>
                                <%#: Eval("Rating", "{0:0.0}") %>
                                (<%#: Eval("ReviewCount") %> reviews)
                            </div>

                        </div>

                    </div>

                    <div class="tutor-skills">
                        Teaches: <%#: Eval("SkillName") %>
                    </div>

                    <div class="tutor-level">
                        <%#: Eval("ProficiencyLevel") %>
                        · <%#: Eval("Category") %>
                    </div>

                    <a class="request-btn"
                       href='<%# "Tutor_Profile.aspx?TutorID=" + Eval("TutorID") %>'>
                        Request Session
                    </a>

                </div>

            </ItemTemplate>

        </asp:Repeater>

    </div>

    <!-- NO TUTORS FOUND -->
    <asp:Panel ID="pnlNoTutors" runat="server"
        CssClass="empty-message"
        Visible="false">

        No tutors found. Try searching for another skill.

    </asp:Panel>

    <!-- FOOTER -->
    <div class="tutor-footer">
        SkillBridge — Find the right tutor for your learning journey
    </div>

</div>

</asp:Content>
