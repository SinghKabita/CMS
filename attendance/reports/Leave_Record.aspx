<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Leave_Record.aspx.cs" Inherits="attendance_reports_Leave_Record" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
    <table class="gridtable">
        <tr>
            <td>Faculty
            </td>
            <td>
                <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
            </td>

            <td>Level
            </td>
            <td>
                <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"></asp:DropDownList>
            </td>


            <td>Program
            </td>
            <td>

                <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
        <tr>
             <td>Semester</td>
            <td>
                <asp:DropDownList ID="ddlSemester" runat="server" Style="height: 22px; width: 100%;" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
            </td>

            <td id="Td1" runat="server" visible="false">
                Batch</td>
             <td id="Td2" runat="server" visible="false">
                <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                </asp:DropDownList>
             </td>
           
        </tr>
        <tr>
            <td>Student Id</td>
            <td>
                <asp:DropDownList ID="ddlStudentId" AutoPostBack="True" Style="height: 22px;" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
            </td>
        </tr>


    </table>

    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" ID="gridStudentLeave" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridStudentLeave_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="SN">
                            <ItemTemplate>
                                <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Id">
                            <ItemTemplate>
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Bind("PK_ID") %>'></asp:Label>
                                <asp:Label ID="lblSemester" runat="server" Visible="false" Text='<%# Bind("SEMESTER_ID") %>'></asp:Label>
                                <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>

                                <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Application Date">
                            <ItemTemplate>

                                <asp:Label ID="lblApplicationDate" runat="server" Text='<%# Bind("application_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leave From Date">
                            <ItemTemplate>

                                <asp:Label ID="lblLeaveFromDate" runat="server" Text='<%# Bind("leave_from_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Leave To Date">
                            <ItemTemplate>

                                <asp:Label ID="lblLeaveToDate" runat="server" Text='<%# Bind("leave_to_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>

                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved By">
                            <ItemTemplate>

                                <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Bind("approved_by") %>'></asp:Label>
                                <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Bind("no_of_days") %>' Visible="false"></asp:Label>
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

