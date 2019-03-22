<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="printbill.aspx.cs" Inherits="finance_printbill" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">


        function printPartOfPage() {
            var printContent = document.getElementById('printall');
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
        <table runat="server" id="tblFirstTable" class="gridtable">
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>Level<br />
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
               <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True" Enabled="true">
                    </asp:DropDownList>
                </td>
                <td>Semester</td>
                <td>
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" runat="server" ID="ddlSemester" Height="22px" />

                </td>
                
            </tr>

            <tr>
                <td>Bill Type</td>
                <td>
                    <asp:DropDownList ID="ddlBillType" AutoPostBack="true" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged" runat="server">
                        <asp:ListItem Text="Bulk" />
                        <asp:ListItem Text="Individual" />
                    </asp:DropDownList>
                </td>

                <td> <asp:Label Text="Student ID" ID="lblIndBillText" Visible="false" runat="server" /> </td>
                <td>
                    <asp:DropDownList ID="ddlStudentID" OnSelectedIndexChanged="ddlStudentID_SelectedIndexChanged" AutoPostBack="true" Visible="false" Height="22px" runat="server">
                </asp:DropDownList>
                </td>
            </tr>

            <tr runat="server" id="trIndBills" visible="false">
                
            </tr>
           
            <tr>
                <td>
                    <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click" />
                </td>

            </tr>
        </table>

        <table style="width: 100%" class="gridtable">

            <tr>
                <td colspan="5">
                    <asp:GridView ID="gridBill" CssClass="gridtable" Visible="true" runat="server" AutoGenerateColumns="False" 
                        EnableModelValidation="True" Width="100%" OnRowCommand="gridBill_RowCommand" 
                        OnRowDataBound="gridBill_RowDataBound">
                        <AlternatingRowStyle BackColor="#e2e2e2" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sn">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1%>"></asp:Label>
                                    <asp:Label ID="lblStudentID" runat="server" Visible="false" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="English Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblEdate" runat="server" Text='<%# Bind("edate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nepali Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblNdate" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fiscal Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblFiscalyear" runat="server" Text='<%# Bind("fiscalyear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INSTALLMENT_NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Bind("INSTALLMENT_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnLoad" runat="server" Text="Load" CommandName="Print" Enabled="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>


        <br />
        <br />
        <div id="hide" runat="server" visible="false">
            <table>
                <tr>

                    <td>
                        <asp:Button ID="btnPrint" runat="server" Text="Print All" OnClick="btnPrint_Click" /></td>
                    <td>
                        <asp:Label ID="lblInstNo" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>


            <div id="printall" style="width: 100%">
                <asp:Repeater ID="rptrTotalCollection" runat="server" OnItemDataBound="rptrTotalCollection_ItemDataBound">
                    <ItemTemplate>
                        <div id="divid" style="page-break-before: always; width: 148mm; padding: 5px; margin: 0; box-sizing: border-box">
                            <asp:Label ID="lblMasterBillId" runat="server" Text=' <%# DataBinder.Eval(Container, "DataItem.mbill_id")%>' Visible="false"></asp:Label>
                            <table style="width: 100%; height: 70%; background-image: url(../images/nnn.jpg)">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <asp:Label ID="lblCollegeName" runat="server" Font-Names="Neuropol" Style="font-size: 16px; font-weight: bold"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; height: 7mm; text-align: left;">
                                        <asp:Image ID="imgNccsLogo" runat="server" Width="120px" ImageUrl="~/images/nccs.png" Style="text-align: right" />
                                    </td>
                                    <td style="text-align: center; width:55%; height: 4mm;">

                                        <asp:Label ID="lblAddress" runat="server" Font-Names="Times New Roman" Style="font-size: 13px"></asp:Label>
                                        <br>
                                        <asp:Label ID="lblPhoneNo" runat="server" Font-Names="Times New Roman" Style="font-size: 13px"></asp:Label>
                                        <br>
                                        <asp:Label ID="lblEmail" runat="server" Font-Names="Times New Roman" Style="font-size: 13px"></asp:Label>
                                    </td>
                                    <td style="width: 25%; height: 7mm; text-align: right; vertical-align: top">

                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                        <asp:Image ImageUrl="" Width="200px" Height="40px" ID="imgStdBarcode" runat="server" />
                                        
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Image ImageUrl="" Width="200px" Height="40px" ID="imgBillBarcode"  runat="server" />
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" style="text-align: left;">Bill for Installment No:
                <asp:Label ID="lblInstallmentNo" runat="server" Style="font-weight: bold"></asp:Label>
                                    </td>
                                    <td style="text-align: left">

                                        <asp:Label ID="lblDD" runat="server">Date: </asp:Label>
                                        <asp:Label ID="lblDate" runat="server" Style="font-weight: bold; font-size: 11pt"></asp:Label>
                                        (<asp:Label ID="lblNepaliDate" runat="server" Style="font-weight: bold; font-size: 11pt"></asp:Label>)

                                    </td>


                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left;">Registration No:
                                <asp:Label ID="lblRegNo" runat="server" Style="font-weight: bold"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblBB" runat="server">Bill No: </asp:Label>
                                        &nbsp;<asp:Label ID="lblBillNo" runat="server" Style="font-weight: bold"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left;">Name:
                                    <asp:Label ID="lblStdName" runat="server" Style="font-weight: bold"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="text-align: left; ">
                                        <asp:Label ID="lblCC" runat="server">Batch: </asp:Label>
                                        <asp:Label ID="lblBatch" runat="server" Style="font-weight: bold"></asp:Label>-<asp:Label ID="lblSection" runat="server" Style="font-weight: bold"></asp:Label>

                                    </td>

                                </tr>
                                <tr style="height: 50%; width: 100%">
                                    <td colspan="3" style="vertical-align: top">

                                        <table id="description_table" style="width: 100%; height:70%; border-collapse: collapse; border: 1px solid black;" cellspacing="0" cellpadding="5px">
                                            <tr>
                                                <th style="border: 1px solid black; width: 5%">SN</th>
                                                <th style="border: 1px solid black;">Description</th>

                                                <th style="border: 1px solid black; width: 5%">Qty</th>
                                                <th style="border: 1px solid black; width: 15%">Amount</th>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn1" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes1" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty1" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt1" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn2" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes2" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty2" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn3" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes3" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty3" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt3" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn4" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes4" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty4" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt4" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn5" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes5" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty5" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt5" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn6" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes6" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty6" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt6" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn7" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes7" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty7" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt7" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn8" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes8" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty8" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt8" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn9" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes9" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty9" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt9" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn10" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes10" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty10" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt10" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn11" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes11" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty11" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt11" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblSn12" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: left;">
                                                    <asp:Label ID="lblDes12" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: center;">
                                                    <asp:Label ID="lblQty12" runat="server"></asp:Label>
                                                </td>
                                                <td style="border: 1px solid black; border-bottom: none; border-top: none; text-align: right;">
                                                    <asp:Label ID="lblAmt12" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid black; border-right: none; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; border-left: none; text-align: left; font-weight: bold">Total</td>

                                                <td style="border: 1px solid black; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; text-align: right;">
                                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-right: none; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; border-left: none; text-align: left; font-weight: bold">Discount</td>

                                                <td style="border: 1px solid black; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; text-align: right;">
                                                    <asp:Label ID="lblDiscount" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; border-right: none; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; border-left: none; text-align: left; font-weight: bold">Debit/Credit balance as per book</td>

                                                <td style="border: 1px solid black; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; text-align: right;">
                                                    <asp:Label ID="lblDbCr" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>

                                            <%--  <tr>
                                            <td style="border: 1px solid black; border-right: none; text-align: center;">&nbsp;</td>
                                            <td style="border: 1px solid black; border-left: none; text-align: left; font-weight: bold">Fine</td>
                                            <td style="border: 1px solid black; text-align: center;">&nbsp;</td>
                                            <td style="border: 1px solid black; text-align: center;">&nbsp;</td>
                                            <td style="border: 1px solid black; text-align: right;">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                            </td>
                                        </tr>--%>

                                            <tr>
                                                <td style="border: 1px solid black; border-right: none; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; border-left: none; text-align: left; font-weight: bold">Grand Total</td>

                                                <td style="border: 1px solid black; text-align: center;">&nbsp;</td>
                                                <td style="border: 1px solid black; text-align: right;">
                                                    <asp:Label ID="lblGTotal" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="5" style="text-align: left; border: 1px solid black"><b>Amount in words NRs.</b>&nbsp;
                                <asp:Label ID="lblAmountW" runat="server" Style="font-size: small;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td colspan="5">


                                                    <asp:Label ID="Label1" runat="server" Style="float: left; font-size: 14px" Text="Please bring this bill at the time of Payment."></asp:Label>
                                                    <%--<asp:Label ID="lblUserName" Font-Size="20px" runat="server" Style="float: right; padding-right: 10px; font-size: 14px"></asp:Label>--%>
                                                    <asp:Image ImageUrl="~/images/signature/sign.png" Width="80px" ImageAlign="Right" runat="server" />
                                                </td>

                                            </tr>

                                        </table>


                                    </td>

                                </tr>


                                <tr>
                                    <td colspan="3" style="font-size: 10px; text-align: justify">* Penalty charges are applicable on late payment of fees as per the college's fee payment policies.<br>
                                    </td>
                                </tr>



                            </table>

                        </div>


                        <div id="hidegrid" runat="server" visible="false">
                            <asp:GridView ID="gridBillDetail" runat="server" Visible="false" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridBillDetail_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Particular">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblParticularId" runat="server" Text='<%# Bind("PARTICULAR_NAME") %>'></asp:Label>--%>
                                            <asp:Label ID="lblParticular" runat="server" Text='<%# Bind("PARTICULAR_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmt" runat="server" Text='<%# Bind("amt") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblAmt2" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>

                    </ItemTemplate>

                </asp:Repeater>

            </div>

        </div>
    </div>
</asp:Content>

