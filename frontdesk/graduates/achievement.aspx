<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="achievement.aspx.cs" Inherits="frontdesk_graduates_achievement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Level<br />
                </td>
                <td>

                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Batch
                </td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="lblPKIDU" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Student
                </td>
                <td>
                    <asp:DropDownList ID="ddlStudentId" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Achievement Date
                </td>
                <td>
                    <asp:TextBox ID="txtAchievementDate" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtAchievementDate_TextChanged"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAchievementDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td>Date(BS)</td>
                <td>
                    <asp:TextBox ID="txtDay" runat="server" Height="22px" Placeholder="DD" Width="30px" AutoPostBack="True" OnTextChanged="txtDay_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtMonth" runat="server" Height="22px" Placeholder="MM" Width="30px" AutoPostBack="True" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtYear" runat="server" Height="22px" Placeholder="YYYY" Width="50px" AutoPostBack="True" OnTextChanged="txtYear_TextChanged"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>Achievement
                </td>
                <td>
                    <asp:TextBox ID="txtAchievement" runat="server" Height="22px" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Remarks
                </td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" Height="55px" Width="250px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
                </td>
                <td></td>
            </tr>
        </table>

        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridAchievement" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridAchievement_RowDataBound" OnRowCommand="gridAchievement_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch">
                                <HeaderTemplate>
                                    Batch
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("batch") %>'></asp:Label>
                                    <asp:Label ID="lblPKID" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <HeaderTemplate>
                                    Student Id
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <HeaderTemplate>
                                    Name
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Achievement Date">
                                <HeaderTemplate>
                                    Achievement Date
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAchievementDate" runat="server" Text='<%# Bind("ACHIEVE_DATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Achivement">
                                <HeaderTemplate>
                                    Achievement
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAchievement" runat="server" Text='<%# Bind("ACHIEVEMENTS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <HeaderTemplate>
                                    Remarks
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" CommandName="Change" runat="server" Text="" ImageUrl="~/images/icons/edit.png"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
