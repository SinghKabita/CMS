<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReceiptReprint.aspx.cs" Inherits="finance_report_ReceiptReprint" %>

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
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Date From:</td>
                <td>
                    <asp:TextBox ID="txtFDate" runat="server" Height="22px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" TargetControlID="txtFDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td>&nbsp;</td>
                <td>Date To:</td>
                <td>
                    <asp:TextBox ID="txtTDate" runat="server" Height="22px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtTDate_CalendarExtender" runat="server" TargetControlID="txtTDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                </td>
                
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

        </table>

        <table style="width: 100%">

            <tr>
                <td colspan="3">
                    <asp:GridView ID="gridDailyCollection" runat="server" AutoGenerateColumns="False"
                        EnableModelValidation="True" Width="100%" ShowFooter="True"
                        OnRowDataBound="gridDailyCollection_RowDataBound"
                        OnRowCommand="gridDailyCollection_RowCommand"
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <HeaderTemplate>
                                    SN
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date (BS)">

                                <HeaderTemplate>
                                    Date (BS)
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDatebs" runat="server" Text='<%# Bind("receiptdatenp") %>' Font-Bold="False"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date (AD)">
                                <FooterTemplate>
                                    Total
                                </FooterTemplate>
                                <FooterStyle Font-Bold="true" />
                                <HeaderTemplate>
                                    Date (AD)
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("receiptdateen") %>' Font-Bold="False"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt No">
                                <HeaderTemplate>
                                    Receipt No
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("RECEIPTNO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class">
                                <HeaderTemplate>
                                    Batch
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("BATCHNO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <HeaderTemplate>
                                    Student Id
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENTID") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <HeaderTemplate>
                                    Student Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </FooterTemplate>

                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />

                                <HeaderTemplate>
                                    Paid Amt
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Bind("PAIDAMOUNT") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill No.">
                                <HeaderTemplate>
                                    Bill No.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRefBillNo" runat="server" Text='<%# Bind("REFRENCE_BILL_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="For Month">
                                <HeaderTemplate>
                                    For Month
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblForMonth" runat="server" Text='<%# Bind("FOR_MONTH") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inst">
                                <HeaderTemplate>
                                    Inst
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Bind("INSTALLMENT_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received By">
                                <HeaderTemplate>
                                    Received By
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReceivedBy" runat="server" Text='<%# Bind("RECEIVED_BY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <HeaderTemplate>
                                    Remarks
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">

                                <ItemTemplate>
                                    <asp:Button Text="Print" CommandName="Print" Height="22px" runat="server" />
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle Font-Bold="True" Font-Size="10pt" BackColor="#5D7B9D" ForeColor="White" />
                        <HeaderStyle Font-Size="10pt" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                    <br>
                </td>
            </tr>
            <tr>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td id="tr_withdraw" runat="server" visible="false" colspan="3" style="text-align: left; color: #000; font-size: 16px; font-weight: bold">Withdraw Detail</td>
            </tr>

            <tr>
                <td colspan="3" id="tr_withdraw_detail" runat="server" visible="false">
                    <asp:GridView ID="gridDailyWithDraw" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" ShowFooter="True" OnRowDataBound="gridDailyWithDraw_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <HeaderTemplate>
                                    SN
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date (BS)">

                                <HeaderTemplate>
                                    Date (BS)
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDatebs" runat="server" Text='<%# Bind("receiptdatenp") %>' Font-Bold="False"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date (AD)">
                                <FooterTemplate>
                                    Total
                                </FooterTemplate>
                                <FooterStyle Font-Bold="true" />
                                <HeaderTemplate>
                                    Date (AD)
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("receiptdateen") %>' Font-Bold="False"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt No">
                                <HeaderTemplate>
                                    Receipt No
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("RECEIPTNO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class">
                                <HeaderTemplate>
                                    Batch
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Batch" runat="server" Text='<%# Bind("BATCHNO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <HeaderTemplate>
                                    Student Id
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENTID") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <HeaderTemplate>
                                    Student Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </FooterTemplate>

                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />

                                <HeaderTemplate>
                                    Paid Amt
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Bind("PAIDAMOUNT") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Withdraw By">
                                <HeaderTemplate>
                                    Withdraw By
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReceivedBy" runat="server" Text='<%# Bind("RECEIVED_BY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <HeaderTemplate>
                                    Remarks
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle Font-Bold="True" Font-Size="10pt" BackColor="#5D7B9D" ForeColor="White" />
                        <HeaderStyle Font-Size="10pt" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                    <br />
                </td>
            </tr>

        </table>

        <div id="hide" runat="server" visible="false" style="margin-top: 1000px;">
            <div id="print_div">
                <div id="divid" style="page-break-before: always; width: 210mm; height: 99mm; padding: 5px; margin: 0; box-sizing: border-box">

                    <table style="width: 100%; height: 100%; background-image: url(../images/nnn.jpg)">

                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <asp:Label ID="lblCollegeName" runat="server" Font-Names="Times New Roman" Style="font-size: 20px; font-weight: bold; font-style: italic"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 12%; height: 7mm; text-align: left; padding-left: 10px; vertical-align: top">
                                <asp:Image ID="imgNccsLogo" runat="server" Width="125px" ImageUrl="~/images/ncft.png" />
                            </td>
                            <td style="width: 68%; text-align: center; height: 7mm;">

                                <asp:Label ID="lblAddress" runat="server" Font-Names="Times New Roman" Style="font-size: 14px"></asp:Label>
                                <br>
                                <asp:Label ID="lblPhoneNo" runat="server" Font-Names="Times New Roman" Style="font-size: 14px"></asp:Label>
                                <br>
                                <asp:Label ID="lblEmail" runat="server" Font-Names="Times New Roman" Style="font-size: 14px"></asp:Label>
                            </td>
                            <td></td>

                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 20px; font-weight: bold; text-align: center">
                                <div style="border: 1px black solid; width: 150px; height: 30px; box-shadow: 0px 5px 10px #000; margin: auto; text-align: center">
                                    <asp:Label ID="lblCash" runat="server" Text="Cash" Style="font-weight: bold"></asp:Label>
                                    <asp:Label ID="lblReceipt" runat="server" Text="Receipt" Style="font-weight: bold"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">
                                <asp:Label ID="lblReceiptNoH" runat="server" Text="Receipt No: "></asp:Label>
                                <asp:Label ID="lblReceiptNoP" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                                <asp:Label ID="lblDateP" runat="server" Text="" Style="font-weight: bold; float: right; padding-right: 40px"></asp:Label>
                                <asp:Label ID="lblDDP" runat="server" Text="Date: " Style="float: right"></asp:Label>
                                <br>
                                <asp:Label ID="lblDateNepP" runat="server" Text="" Style="font-weight: bold; float: right; padding-right: 40px"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">
                                <asp:Label ID="lblNameH" runat="server" Text="Received with thanks from M/s "></asp:Label>
                                <asp:Label ID="lblNameP" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">
                                <asp:Label ID="lblAmoundWordH" runat="server" Text="a sum of Rupees "></asp:Label>
                                <asp:Label ID="lblAmountWordP" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr id="tr_drownon" runat="server" visible="false">
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">



                                <asp:Label ID="lblDrownDateP" runat="server" Text="" Visible="true" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>

                        <tr runat="server" visible="false" id="trBillInsNo">
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">

                                <asp:Label ID="lblBillNoH" runat="server" Text=" with reference to Bill No "></asp:Label>
                                <asp:Label ID="lblBillNoP" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                                <asp:Label ID="lblPeroidH" runat="server" Text=" for the Installment of "></asp:Label>
                                <asp:Label ID="lblSemesterP" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>


                        <tr>
                            <td colspan="3" style="padding-left: 40px; padding-right: 40px; font-size: 16px; text-align: justify">
                                <asp:Label ID="lblRemarksP" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">
                                <div style="border: 1px black solid; width: 150px; height: 30px; box-shadow: 0px 5px 10px #000">
                                    &nbsp;<asp:Label ID="lblAmountP" runat="server" Style="font-size: 19px; font-weight: bold" Text=""></asp:Label>
                                </div>
                                <asp:Label ID="lblCollgeSignP" runat="server" Style="float: right; padding-right: 40px; font-style: italic; font-size: 14px" Text=" "></asp:Label>
                                <br>

                                <asp:Label ID="lblUserNameP" runat="server" Style="float: right; padding-right: 100px; font-size: 14px" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>

    </div>

</asp:Content>

