<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LogBookReport.aspx.cs" Inherits="frontdesk_visitors_LogBookReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">


        function printPartOfPage() {
            var printContent = document.getElementById('print_div');
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
                <td>Name:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtName" runat="server" Height="22px" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>From Date</td>
                <td>
                    <asp:TextBox ID="txtFDate" runat="server" Height="22px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td>To Date</td>
                <td>
                    <asp:TextBox ID="txtTDate" runat="server" Height="22px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                </td>
                <td>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" /></td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <div id="print_div">
            <div id="hide" runat="server" visible="false">

                <table style="width: 100%">
                    <tr>
                        <td></td>
                        <td style="text-align: center">
                            <asp:Label ID="lblCompanyName" runat="server" Text="" Style="font-weight: bold; font-size: 16px; font-family: Neuropol"></asp:Label><br>
                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label><br>
                            Contact No:
                            <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
                            Website:<asp:Label ID="lblWebsite" runat="server" Text=""></asp:Label><br>
                            Report:<asp:Label ID="lblReport" runat="server" Text="" Style="font-weight: bold; font-size: 14px"></asp:Label><br>
                            From Date:<asp:Label ID="lblFDate" runat="server" Text="" Style="font-weight: bold; font-size: 14px"></asp:Label>
                            To Date:<asp:Label ID="lblTDate" runat="server" Text="" Style="font-weight: bold; font-size: 14px"></asp:Label><br>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="griLogList" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date AD">
                                        <HeaderTemplate>
                                            Date (AD)
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("engdate") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date BS">
                                        <HeaderTemplate>
                                            Date (BS)
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateBs" runat="server" Text='<%# Bind("nepdate") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Visitor's Name">
                                        <HeaderTemplate>
                                            Visitor's Name
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVisitorName" runat="server" Text='<%# Bind("VISITORS_NAME") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Organization">
                                        <HeaderTemplate>
                                            Organization
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrganization" runat="server" Height="22px" Text='<%# Bind("ORGANIZATION") %>'></asp:Label>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purpose">
                                        <HeaderTemplate>
                                            Purpose
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurpose" runat="server" Height="22px" Text='<%# Bind("PURPOSE") %>'></asp:Label>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone No">
                                        <HeaderTemplate>
                                            Phone No
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Bind("CONTACT_NO") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vechicle No">
                                        <HeaderTemplate>
                                            Vechicle No
                                        </HeaderTemplate>

                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Bind("VECHICLE_NO") %>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time In">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTimeIn" runat="server" Text='<%# Bind("TIME_IN") %>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTimeOut" runat="server" Text='<%# Bind("TIME_OUT") %>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
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
    </div>
</asp:Content>
