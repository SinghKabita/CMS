<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReceiptEntry.aspx.cs" Inherits="finance_ReceiptEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <style type="text/css">
        table .tr_detail {
            height: 40px;
            vertical-align: top;
        }
    </style>

    <div class="container mt-20" style="padding-right: 10px; padding-left: 10px">

        <table id="tablereceipt" runat="server" style="width: 100%; padding-right: 10px; padding-left: 30px">
            <tr>
                <td style="width: 50%; border: 1px solid black">
                    <table style="width: 100%; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: auto; margin-top: 10px">


                        <tr>
                            <td class="auto-style3">Refrence Bill No:</td>
                            <td class="auto-style3">Bill No:</td>
                            <td class="auto-style3">For Month:</td>
                           

                        </tr>
                        <tr class="tr_detail">
                            <td>
                                <asp:TextBox ID="txtRefBillNo" runat="server" Height="22px" AutoPostBack="true" OnTextChanged="txtRefBillNo_TextChanged"></asp:TextBox>
                            </td>
                             <td>
                                <asp:TextBox ID="txtBillNo" runat="server" Height="22px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtForMonths" runat="server" Height="22px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Student Id:
                            </td>
                        </tr>
                        <tr class="tr_detail">
                            <td>

                                <%--<asp:Label ID="lblCode" runat="server" Height="22px" Style="display: inline"></asp:Label>--%>

                                <asp:TextBox ID="txtStudentId" runat="server" Height="22px" Width="100px" Style="display: inline" 
                                    AutoPostBack="True" OnTextChanged="txtStudentId_TextChanged"></asp:TextBox>

                            </td>
                        </tr>

                        <tr runat="server" id="trDetail1" visible="false">
                            <td>Faculty:
                            </td>
                            <td>Level</td>
                            <td>Program:</td>
                        </tr>
                        <tr class="tr_detail" runat="server" id="trDetail2" visible="false">
                            <td>
                                <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList></td>

                            <td>
                                <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>

                            <td>

                                <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

                            </td>

                        </tr>


                        <tr runat="server" id="trDetail3" visible="false">
                            <td>Batch:</td>
                            <td>Semester</td>
                        </tr>
                        <tr class="tr_detail" runat="server" id="trDetail4" visible="false">
                            <td>
                                <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr runat="server" id="trDetail5" visible="false">
                            <td>Student Name:</td>
                        </tr>

                        <tr class="tr_detail" runat="server" id="trDetail6" visible="false">

                            <td colspan="3">
                                <asp:DropDownList ID="ddlStudentName" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentName_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr id="labelrow" runat="server" visible="false">
                            <td colspan="3" style="width: 100%">
                                <table style="width: 100%; background-color: Aqua; font-weight: bold;">

                                    <tr>
                                        <td>Std Id:</td>
                                        <td>Name:</td>
                                        <td>Semester: </td>
                                        <td>Batch:</td>
                                    </tr>
                                    <tr class="tr_detail">
                                        <td>
                                            <asp:Label ID="lblStudentId" runat="server" Height="22px" Style="display: inline-block"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblStudentName" runat="server" Height="22px" Style="display: inline-block"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Height="22px" Style="display: inline-block"></asp:Label>
                                            <asp:Label ID="lblSection" runat="server" Height="22px" Style="display: inline-block"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBatchYear" runat="server" Height="22px" Style="display: inline-block"></asp:Label></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>

                            <td>Receipt No:</td>
                            <td>Date (AD):</td>
                            <td>Date (BS):</td>

                        </tr>
                        <tr class="tr_detail">
                            <td style="height: 27px">
                                <asp:TextBox ID="txtReceiptNo" runat="server" Height="22px" AutoPostBack="True"></asp:TextBox></td>
                            <td style="height: 27px">
                                <asp:TextBox ID="txtReceiptDate" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtReceiptDate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceiptDate"
                                    Enabled="True" Format="dd.MMM.yyyy">
                                </cc1:CalendarExtender>
                                [dd/mm/yyyy]</td>
                            <td style="height: 27px">
                                <asp:TextBox ID="txtDate" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtDate_TextChanged"></asp:TextBox></td>

                        </tr>

                        <tr>
                            <td>Payment Type:</td>
                            <td runat="server" visible="false"> 
                                <asp:Label ID="lblNumInstallment" runat="server" Text=" Number of Installment:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInstallments" runat="server" Text="Installments:"></asp:Label>
                            </td>

                        </tr>
                        <tr class="tr_detail">
                            <td>
                                <asp:DropDownList ID="ddlPaymentType" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged">

                                    <asp:ListItem Value="Installment" Selected="True">Installment</asp:ListItem>

                                    <asp:ListItem Value="Miscellaneous">Miscellaneous</asp:ListItem>
                                </asp:DropDownList></td>

                            <td id="tdmonths" runat="server">
                                <asp:DropDownList ID="ddlInstallments" runat="server" Height="22px" AutoPostBack="True" Enabled="False" Visible="false">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtNumInstallment" runat="server" Height="22px" Text="1" Width="30%" AutoPostBack="True" 
                                    OnTextChanged="txtNumInstallment_TextChanged" Visible="false"></asp:TextBox>

                                <asp:Label ID="lblRemainingMonth" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtInstallments" runat="server" Height="22px" Width="30%"></asp:TextBox>

                                <asp:GridView ID="gridMiscelleneous" runat="server" Width="100%" Visible="false" AutoGenerateColumns="False" EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particular">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParticularName" runat="server" Text='<%# Bind("PARTICULAR_NAME") %>'></asp:Label>
                                                <asp:Label ID="lblParticularId" runat="server" Text='<%# Bind("PARTICULARS") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRate" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td>
                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>


                        <tr>
                            <td class="auto-style3">Installment Amount:</td>
                            <td class="auto-style3">Remaining Balance:</td>
                            <td class="auto-style3">Payable Amount:</td>

                        </tr>
                        <tr class="tr_detail">
                            <td class="auto-style2">
                                <asp:Label ID="lblInstallmentAmt" runat="server" Height="22px" Width="100%" Style="display: inline-block; font-size: 16px; color: blue"></asp:Label>
                            </td>
                            <td class="auto-style2">
                                <asp:Label ID="lblRemainingAmt" runat="server" Height="22px" Width="100%" Style="display: inline-block; font-size: 16px; color: green"></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="lblTotalAmt" runat="server" Height="22px" Width="100%" Style="display: inline-block; font-size: 16px; color: red"></asp:Label>
                            </td>

                        </tr>


                        <tr>
                            <td colspan="2">Amount:</td>
                            <td></td>

                        </tr>
                        <tr class="tr_detail">
                            <td colspan="2">
                                <asp:TextBox ID="txtAmount" runat="server" Height="22px" Width="50%" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"></asp:TextBox></td>
                            <td></td>

                        </tr>
                        <tr>
                            <td colspan="3">Remarks:</td>
                        </tr>
                        <tr>
                            <td colspan="3" class="tr_detail">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>


                        <tr>
                           
                            <td class="auto-style3"><%--Send SMS:--%>Payment Mode:</td>

                        </tr>
                        <tr class="tr_detail">
                           
                            <td>
                                <%-- <asp:RadioButtonList ID="rbtnSendSMS" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>--%>
                                <asp:RadioButtonList ID="rbtnPaymentMode" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtnPaymentMode_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Cash</asp:ListItem>
                                    <asp:ListItem>Cheque</asp:ListItem>
                                    <asp:ListItem>Voucher</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>


                        <tr id="chequedetail" runat="server" visible="false">
                            <td>Cheque/Voucher Date:</td>
                            <td>Bank Name:</td>
                            <td>Cheque/Voucher No:</td>

                        </tr>
                        <tr id="chequedetails" class="tr_detail" runat="server" visible="false">
                            <td>
                                <asp:TextBox ID="txtChequeDate" runat="server" Height="22px" Width="100%"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtBankName" runat="server" Height="22px" Width="100%"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtChequeNo" runat="server" Height="22px" Width="100%"></asp:TextBox></td>

                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" Height="22px"
                                    Width="80px" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="22px" Width="80px"></asp:Button>
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td colspan="3">&nbsp;</td>

                        </tr>
                        <tr runat="server" id="trDetail7" visible="false">
                            <td colspan="3">
                                <hr>
                                <h2 style="color: black">Fee Structure</h2>
                                <asp:GridView ID="gridFeeStructure" CssClass="gridtable" Width="100%" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" ShowFooter="True" OnRowDataBound="gridFeeStructure_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Installment No">
                                            <FooterTemplate>
                                                Total
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="true" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Bind("INSTALLMENT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="true" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                &nbsp;</td>
                        </tr>
                        <tr runat="server" id="trDetail8" visible="false">
                            <td>
                                <asp:Label ID="lblGrandTotal" runat="server" Text="" Visible="true"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </td>
                <td style="width: 45%; vertical-align: top">
                    <asp:GridView ID="grdPayment" runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False"
                         EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False"
                        OnRowDataBound="grdPayment_RowDataBound" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex +1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    AD:
                                <asp:Label ID="lblRecpDate" runat="server" Text='<%# Bind("RECEIPTDATEEN") %>'></asp:Label><br>
                                    BS:
                                <asp:Label ID="lblNDay" runat="server" Text='<%# Bind("DAY") %>'></asp:Label>/<asp:Label ID="lblNMonth" runat="server" Text='<%# Bind("MONTH") %>'></asp:Label>/<asp:Label ID="lblNYear" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rcpt">
                                <ItemTemplate>
                                    <asp:Label ID="lblRecepNo" runat="server" Text='<%# Bind("RECEIPTNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inst">
                                <ItemTemplate>
                                    Sem <asp:Label ID="lblSem" runat="server" Text='<%# Bind("SEMESTER") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSemCode" runat="server" Text=''></asp:Label>
                                   - <asp:Label ID="lblInstallment" runat="server" Text='<%# Bind("INSTALLMENT_NO") %>'></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">

                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("PAIDAMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">

                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
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
                                    <asp:Label ID="lblCash" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                                    <asp:Label ID="lblReceipt" runat="server" Text="Receipt" Style="font-weight: bold"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">
                                <asp:Label ID="lblReceiptNoH" runat="server" Text="Receipt No: "></asp:Label>
                                <asp:Label ID="lblReceiptNo" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                                <asp:Label ID="lblDate" runat="server" Text="" Style="font-weight: bold; float: right; padding-right: 40px"></asp:Label>
                                <asp:Label ID="lblDD" runat="server" Text="Date: " Style="float: right"></asp:Label>
                                <br>
                                <asp:Label ID="lblDateNep" runat="server" Text="" Style="font-weight: bold; float: right; padding-right: 40px"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">
                                <asp:Label ID="lblNameH" runat="server" Text="Received with thanks from M/s "></asp:Label>
                                <asp:Label ID="lblName" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">
                                <asp:Label ID="lblAmoundWordH" runat="server" Text="a sum of Rupees "></asp:Label>
                                <asp:Label ID="lblAmountWord" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr id="tr_drownon" runat="server" visible="false">
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">



                                <asp:Label ID="lblDrownDate" runat="server" Text="" Visible="true" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>

                        <tr runat="server" visible="false" id="trBillInsNo">
                            <td colspan="3" style="padding-left: 40px; font-size: 16px">

                                <asp:Label ID="lblBillNoH" runat="server" Text=" with reference to Bill No "></asp:Label>
                                <asp:Label ID="lblBillNo" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                                <asp:Label ID="lblPeroidH" runat="server" Text=" for the Installment of "></asp:Label>
                                <asp:Label ID="lblSemesterP" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>


                        <tr>
                            <td colspan="3" style="padding-left: 40px; padding-right: 40px; font-size: 16px; text-align: justify">
                                <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label>
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
                                    &nbsp;<asp:Label ID="lblAmount" runat="server" Style="font-size: 19px; font-weight: bold" Text=""></asp:Label>
                                </div>
                                <asp:Label ID="lblCollgeSign" runat="server" Style="float: right; padding-right: 40px; font-style: italic; font-size: 14px" Text=" "></asp:Label>
                                <br>

                                <asp:Label ID="lblUserName" runat="server" Style="float: right; padding-right: 100px; font-size: 14px" Text=""></asp:Label>
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


