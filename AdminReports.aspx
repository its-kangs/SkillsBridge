<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminReports.aspx.cs" Inherits="SkillBridge.AdminReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="ar-wrap">

        <div class="ar-title">Reports</div>

        <div class="ar-stats">
            <div class="ar-stat">
                <div class="ar-stat-name">Total Requests</div>
                <div class="ar-stat-number"><asp:Label ID="Lbltotal" runat="server" Text="0"></asp:Label></div>
            </div>
            <div class="ar-stat">
                <div class="ar-stat-name">Sessions Completed</div>
                <div class="ar-stat-number"><asp:Label ID="Lblcompleted" runat="server" Text="0"></asp:Label></div>
            </div>
            <div class="ar-stat">
                <div class="ar-stat-name">Active Tutors</div>
                <div class="ar-stat-number"><asp:Label ID="Lbltutors" runat="server" Text="0"></asp:Label></div>
            </div>
        </div>

        <div class="ar-panels">

            <div class="ar-panel">
                <div class="ar-panel-title">Most Requested Skills</div>
                <asp:GridView ID="Gvskills" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="ar-grid" EmptyDataText="No requests yet.">
                    <EmptyDataRowStyle CssClass="ar-empty" />
                    <Columns>
                        <asp:BoundField DataField="SkillName" HeaderText="Skill" />
                        <asp:BoundField DataField="Requests" HeaderText="Requests" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="ar-panel">
                <div class="ar-panel-title">Most Active Tutors</div>
                <asp:GridView ID="Gvtutors" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="ar-grid" EmptyDataText="No requests yet.">
                    <EmptyDataRowStyle CssClass="ar-empty" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Tutor" />
                        <asp:BoundField DataField="Requests" HeaderText="Requests" />
                        <asp:BoundField DataField="AvgRating" HeaderText="Rating" DataFormatString="{0:0.0}" />
                    </Columns>
                </asp:GridView>
            </div>

        </div>

    </div>
</asp:Content>


