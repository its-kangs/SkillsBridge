<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tutor_Profile.aspx.cs" Inherits="SkillBridge.TutorProfile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tutor Profile - SkillBridge</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- HEADER -->
        <header class="header profile-header">
            <a href="Default.aspx" class="logo-link">
                <span class="skill">Skill</span><span class="bridge">Bridge</span>
            </a>
            <nav class="nav-links">
                <a href="Search.aspx" class="nav-item">Search</a>
                <a href="MySessions.aspx" class="nav-item">My Sessions</a>
                <div class="nav-avatar" title="Your Profile"></div>
            </nav>
        </header>

        <!-- MAIN CONTENT -->
        <main class="main">
            <div class="profile-container">
                <!-- LEFT COLUMN -->
                <div class="profile-left">
                    <!-- Tutor Summary Header -->
                    <div class="tutor-hero">
                        <div class="tutor-avatar"></div>
                        <div class="tutor-info">
                            <h1 class="tutor-name">
                                <asp:Label ID="lblTutorName" runat="server" Text="Tutor Name"></asp:Label>
                            </h1>
                            <div class="tutor-rating">
                                <span class="stars-gray">★★★★★</span>
                                <asp:Label ID="lblAverageRating" runat="server" Text="0.0"></asp:Label> average &middot;
                                <asp:Label ID="lblReviewCount" runat="server" Text="0"></asp:Label> reviews
                            </div>
                            <div class="skill-tags">
                                <asp:Repeater ID="rptSkills" runat="server">
                                    <ItemTemplate>
                                        <span class="skill-pill"><%# Eval("SkillName") %></span>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>

                    <!-- About Card -->
                    <section class="profile-card">
                        <h2 class="card-heading">About</h2>
                        <p class="card-body-text">
                            <asp:Label ID="lblAbout" runat="server" Text="No bio available."></asp:Label>
                        </p>
                    </section>

                    <!-- Reviews Card -->
                    <section class="profile-card">
                        <h2 class="card-heading">Reviews</h2>
                        <asp:Repeater ID="rptReviews" runat="server">
                            <ItemTemplate>
                                <div class="review-item">
                                    <div class="review-header">
                                        <strong><%# Eval("ReviewerName") %></strong> &mdash; 
                                        <span class="review-stars"><%# FormatStars(Eval("Rating")) %></span>
                                    </div>
                                    <p class="review-comment"><%# Eval("Comment") %></p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblNoReviews" runat="server" Text="No reviews yet." CssClass="card-body-text" Visible="false"></asp:Label>

                        <asp:Panel ID="pnlMoreReviews" runat="server" CssClass="more-reviews" Visible="false">
                            <asp:LinkButton ID="lnkShowMoreReviews" runat="server" OnClick="lnkShowMoreReviews_Click" CssClass="more-reviews-link">
                                + <asp:Label ID="lblMoreReviewsCount" runat="server" Text="0"></asp:Label> more reviews
                            </asp:LinkButton>
                        </asp:Panel>
                    </section>
                </div>

                <!-- RIGHT COLUMN (SIDEBAR) -->
                <aside class="profile-right">
                    <div class="profile-card request-card">
                        <h2 class="card-heading">Request a session</h2>
                        <p class="request-sub">Pick a skill to get started</p>

                        <asp:DropDownList ID="ddlSkills" runat="server" CssClass="profile-select">
                        </asp:DropDownList>

                        <asp:Button ID="btnSendRequest" runat="server" Text="Send Request" 
                            CssClass="profile-submit-btn" OnClick="btnSendRequest_Click" />

                        <asp:Label ID="lblStatus" runat="server" CssClass="session-error" Visible="false"></asp:Label>
                    </div>
                </aside>
            </div>
        </main>

        <!-- FOOTER -->
        <footer class="profile-footer">
            LOW-FIDELITY WIREFRAME &mdash; SkillBridge
        </footer>
    </form>
</body>
</html>