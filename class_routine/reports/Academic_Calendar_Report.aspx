<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Academic_Calendar_Report.aspx.cs" Inherits="class_routine_reports_Academic_Calendar_Report" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
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
        </table>

        <table class="gridtable">
            <tr>
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" Height="22px" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" Height="22px" runat="server"></asp:DropDownList></td>
                

                <td>
                    <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click"></asp:Button></td>
            </tr>
            <tr runat="server" visible="false">
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px"></asp:DropDownList></td>
            </tr>
        </table>
        <table style="width: 60%">
            <tr>
                <td>
                    <asp:GridView runat="server" CssClass="gridtable" AutoGenerateColumns="False" Width="100%"
                         EnableModelValidation="True" ID="gridAcademicCalendar" OnRowDataBound="gridAcademicCalendar_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("CAL_MONTH") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblMonthName" runat="server" Width="10px" Style="font-size: 18pt; font-weight: bold"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDay" runat="server" Text='<%# Bind("CAL_DAY") %>' Width="10px"></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblDayofWeek" runat="server" Text='<%# Bind("CAL_DAY_OF_WEEK") %>' Width="60px"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Days">
                                <ItemTemplate>
                                    <asp:Label ID="lblDays" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblWorkingDays" runat="server" Text='<%# Bind("WORKING_DAY") %>' Visible="false"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Width="250px" Text='<%# Bind("REMARKS") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

