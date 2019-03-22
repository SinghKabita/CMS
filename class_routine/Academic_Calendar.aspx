<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Academic_Calendar.aspx.cs" Inherits="class_routine_Academic_Calendar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Year</td>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Height="22px"></asp:TextBox></td>
                <td>Month</td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" Height="22px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>

                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" Height="22px" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" runat="server" Height="22px"></asp:DropDownList></td>

            </tr>
            <tr runat="server" visible="false">
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click"></asp:Button></td>
                <td>
                    <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click"></asp:Button></td>
                <td></td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gridAcademicCalendar" runat="server" AutoGenerateColumns="False"
                        EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="gridAcademicCalendar_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblDay" runat="server" Text='<%# Bind("CAL_DAY_OF_WEEK") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Working Day">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkWorkingDay" runat="server" Text="" AutoPostBack="True" OnCheckedChanged="chkWorkingDay_CheckedChanged"></asp:CheckBox>
                                    <asp:Label ID="lblWorkingDay" runat="server" Visible="False" Text='<%# Bind("WORKING_DAY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="300px" Height="22px" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
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
