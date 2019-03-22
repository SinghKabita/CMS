<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Faculty_Leave_Report.aspx.cs" Inherits="class_routine_reports_Faculty_Leave_Report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">


        function printPartOfPage() {
            var printContent = document.getElementById('div_print');
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=0,top=0,width=0,height=0');

            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>
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
            <tr>
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Faculty Member</td>
                <td>
                    <asp:DropDownList ID="ddlFacultyMember" Height="22px" AutoPostBack="True" Style="height: 22px; font-size: medium" runat="server"></asp:DropDownList>

                </td>

                <td>
                    <asp:Button runat="server" Text="View" ID="btnView" OnClick="btnView_Click" /></td>
                <td>
                    <asp:Button Text="Print" ID="btnPrint" Visible="false" OnClick="btnPrint_Click" runat="server" />
                </td>


            </tr>
        </table>
        <div id="div_print">
            <div class="hide" runat="server" >
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: center; font-weight: bold;"><span style="font-size: large">
                            <asp:Label Text="" ID="lblCollegeName" runat="server" />
                         </span></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-weight: bold;">
                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-weight: bold;"><span style="font-size: large">
                            <asp:Label ID="lbl" runat="server" Text="Faculty Leave Report"></asp:Label></span></td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" ID="gridFacultyLeave" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridFacultyLeave_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Bind("PK_ID") %>'></asp:Label>

                                        <asp:Label ID="lblEmployeeId" runat="server" Text='<%# Bind("employee_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>

                                        <asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved Date">
                                    <ItemTemplate>

                                        <asp:Label ID="lblApprovedDate" runat="server" Text='<%# Bind("approve_date") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="No Of Periods">
                                    <ItemTemplate>

                                        <asp:Label ID="lblNoofPeriods" runat="server" Text='<%# Bind("no_of_period") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>

                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved By">
                                    <ItemTemplate>

                                        <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Bind("approve_by") %>'></asp:Label>

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
    </div>
</asp:Content>
