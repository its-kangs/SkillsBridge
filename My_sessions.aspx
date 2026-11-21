<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="My_sessions.aspx.cs" Inherits="SkillBridge.My_sessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="ms-wrap">

        <div class="ms-title">My Sessions</div>

        <div class="ms-filter">
            <span class="ms-filter-label">Show</span>
            <asp:DropDownList ID="Ddlstatus" runat="server" AutoPostBack="True" CssClass="ms-select">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>Accepted</asp:ListItem>
                <asp:ListItem>Completed</asp:ListItem>
                <asp:ListItem>Declined</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="ms-scroll">
            <asp:GridView ID="Gvsessions" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="ms-grid" EmptyDataText="No sessions found.">
                <EmptyDataRowStyle CssClass="ms-empty" />
                <Columns>

                    <asp:TemplateField HeaderText="Skill / Partner">
                        <ItemTemplate>
                            <div class="ms-skill"><%#: Eval("SkillName") %></div>
                            <div class="ms-partner">with <%#: Eval("Partner") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date &amp; Location">
                        <ItemTemplate>
                            <div><%#: Eval("Request_Date", "{0:MMM dd}") %> &middot; <%#: Eval("Request_Time", "{0:hh\\:mm}") %></div>
                            <div class="ms-partner"><%#: Eval("Location") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class="ms-status ms-<%# Convert.ToString(Eval("status")).ToLower() %>"><%#: Eval("status") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="MyRole" HeaderText="Role" />

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="Btnaccept" runat="server" Text="Accept" CommandName="accept" CommandArgument='<%# Eval("RequestID") %>' CssClass="ms-btn" />
                            <asp:Button ID="Btndecline" runat="server" Text="Decline" CommandName="decline" CommandArgument='<%# Eval("RequestID") %>' CssClass="ms-btn ms-btn-outline" />
                            <asp:Button ID="Btncomplete" runat="server" Text="Mark Complete" CommandName="complete" CommandArgument='<%# Eval("RequestID") %>' CssClass="ms-btn" />
                            <asp:Button ID="Btnreview" runat="server" Text="Leave Review" CommandName="review" CommandArgument='<%# Eval("RequestID") %>' CssClass="ms-btn ms-btn-outline" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
