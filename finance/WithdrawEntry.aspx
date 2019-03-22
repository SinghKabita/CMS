<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WithdrawEntry.aspx.cs" Inherits="finance_WithdrawEntry" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style type="text/css">
        table .tr_detail {
            height: 40px;
            vertical-align: top;
        }

        .auto-style1 {
            height: 24px;
        }
    </style>

    <div class="container">
        <table id="tablereceipt" runat="server" style="width: 100%; padding: 5px">
            <tr>
                <td style="width: 50%; border: 1px solid black">
                    <table style="width: 100%; font-weight: bold; font-size: 13px">
                        <tr>
                            <td>Student Id:
                            </td>
                            <td>Batch</td>

                            <td>Student Name:</td>

                        </tr>
                        <tr class="tr_detail">
                            <td>
                                <asp:Label ID="lblCode" runat="server" Height="22px" Style="display: inline"></asp:Label>
                                <asp:TextBox ID="txtStudentId" runat="server" Height="22px" Width="50px" Style="display: inline" AutoPostBack="True" OnTextChanged="txtStudentId_TextChanged"></asp:TextBox></td>




                            <td>

                                <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>

                            </td>
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
                                            <asp:Label ID="lblSemester" runat="server" Height="22px" Style="display: inline-block"></asp:Label></td>
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
                            <td>Amount:</td>
                            <td>Remarks:</td>
                            <td></td>

                        </tr>
                        <tr class="tr_detail">
                            <td>
                                <asp:TextBox ID="txtAmount" runat="server" Height="22px"></asp:TextBox></td>
                            <td colspan="2">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox></td>



                        </tr>

                        <tr>

                            <td>Payment Mode:</td>
                            <td></td>
                            <td></td>

                        </tr>
                        <tr class="tr_detail">

                            <td colspan="2">
                                <asp:RadioButtonList ID="rbtnPaymentMode" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtnPaymentMode_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Cash</asp:ListItem>
                                    <asp:ListItem>Cheque</asp:ListItem>
                                    <asp:ListItem>Voucher</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <%-- <asp:RadioButtonList ID="rbtnSendSMS" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>--%>
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
                            <td></td>
                        </tr>

                        <tr>
                            <td colspan="3">&nbsp;</td>

                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <asp:GridView ID="grdPayment" runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
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
                                <asp:Label ID="lblRecpDate" runat="server" Text='<%# bind("RECEIPTDATEEN") %>'></asp:Label><br>
                                    BS:
                                <asp:Label ID="lblNDay" runat="server" Text='<%# bind("DAY") %>'></asp:Label>/<asp:Label ID="lblNMonth" runat="server" Text='<%# bind("MONTH") %>'></asp:Label>/<asp:Label ID="lblNYear" runat="server" Text='<%# bind("YEAR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rcpt">
                                <ItemTemplate>
                                    <asp:Label ID="lblRecepNo" runat="server" Text='<%# bind("RECEIPTNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inst">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstallment" runat="server" Text='<%# bind("INSTALLMENT_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">

                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# bind("PAIDAMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">

                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# bind("REMARKS") %>'></asp:Label>
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



