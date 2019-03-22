<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentpaymenthistory.aspx.cs" Inherits="finance_report_studentpaymenthistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .form {
            overflow: hidden;
            background-color: #ddd;
            padding: 1em 1em 1em 1em;
            font-size: .9em;
            border: 1px solid black;
            box-sizing: border-box;
            min-width: 270px;
            margin-bottom: 10px;
        }

        .sub-container {
            background-color: #fff;
            border: 1px solid black;
            margin: auto;
            padding: 8px;
            box-sizing: border-box;
            margin-bottom: 30px;
            box-shadow: 0 0 5px black;
        }

        .sub-container-heading {
            height: 30px;
            border-bottom: 1px solid black;
            font-size: 18px;
            line-height: 30px;
            text-align: left;
            font-weight: bold;
            padding: 0 5px;
            box-sizing: border-box;
            margin-bottom: 10px;
        }

        .row {
            font-size: 20px;
            box-sizing: border-box;
            text-align: left;
            font-weight: bold;
            padding: 0 10px;
          
            margin-bottom: 10px;

        }

        .label {
            text-align: left;
            position: relative;
            display: block; /*background-color: tomato;*/
            padding: .25em;
            float: left;
            box-sizing: border-box;
            margin-right: 1em;
            margin-bottom: 1em;
            width: 100%;
            min-width: 200px; /*max-width:calc(100% / 2);*/
            font-weight: bold;
            box-sizing: border-box;
            color: black;
        }


        .form > .row > .col1 {
            width: calc((100%  / 1) - 1em);
        }

        .row > .col2 {
            width: calc((100%  / 2) - 1em);
        }

        .row > .col3 {
            width: calc((100%  / 3) - 1em);
        }

        .row > .col4 {
            width: calc((100%  / 4) - 1em);
            top: 1px;
            left: 0px;
        }

        .row > .col5 {
            width: calc((100%  / 5) - 1em);
            top: 1px;
            left: 0px;
        }
    </style>

    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" Height="22px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                 <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Style="height: 22px; width: 90px;" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" Height="22px" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

           
            <tr>
                <td>Semester
                </td>
                <td>
                    <asp:DropDownList ID="ddlSemester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" runat="server" Height="22px"></asp:DropDownList>                   
                </td>
                <td id="Td1" runat="server" visible="true">Batch</td>
                <td>
                    <asp:DropDownList runat="server" Visible="true" Enabled="false" ID="ddlBatch" Height="22px" Style="margin-left: 0px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td>
                    Student ID
                </td>
                <td>

                <asp:DropDownList ID="ddlStudent" runat="server" Height="22px" AutoPostBack="True" >
                    </asp:DropDownList>
                    </td>

                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                </td>

            </tr>
        </table>
        <div id="hide" class="form" runat="server" visible="false">
            <div id="student-detail" class="sub-container">
                <div class="sub-container-heading">Student Detail</div>
                <div class="row" style="font-size: 14px;">

                    <table style="width: 95%; font-weight: bold" class="gridtable">
                        <tr>
                            <td>Student Id:</td>
                            <td>Name:</td>
                            <td>Semester:</td>
                            <td>Batch:</td>
                            <td rowspan="3">
                                <asp:Image ID="imgStudent" runat="server" Height="100px" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblStudentId" runat="server" Height="22px" Width="60%"
                                    Style="top: 1px; left: 0px;"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblStudentName" runat="server" Height="22px" Width="100%" Style="display: inline-block; top: 0px; left: 0px;"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblSemester" runat="server" Height="22px" Width="80%"
                                    Style="display: inline-block; top: 421px; left: 669px;"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblBatch" runat="server" Height="22px" Width="80%" Style="display: inline-block"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                    </table>
                </div>

            </div>
            <div id="div_pay_schedule" runat="server" class="sub-container" style="width: 40%; display: inline-block; float: left">
                <div class="sub-container-heading">
                    Student Payment Schedule
                </div>
                <div class="row">
                    <label class="label col1">
                        <asp:GridView ID="gridPaySchedule" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Style="margin-top: 0px" Width="100%" OnRowDataBound="gridPaySchedule_RowDataBound"
                            ShowFooter="True" CellPadding="10" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inst. No">
                                    <FooterTemplate>
                                        <b>Total</b>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Bind("INSTALLMENT_NO") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AMT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("DISC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total">
                                    <FooterTemplate>

                                        <asp:Label ID="lblGrandTotal" runat="server" Style="font-weight: 700"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("amt_discount") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </label>
                </div>
            </div>

            <div id="div1" runat="server" class="sub-container" style="width: 58%; display: inline-block; float: right">
                <div class="sub-container-heading">
                    Bill Generated
                </div>
                <div class="row">
                    <label class="label col1">
                        <asp:GridView ID="gridBill" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%"
                             CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridBill_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sn">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1%>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="MBillID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMBillID" runat="server" Visible="true" Text='<%# Bind("MBILL_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BILLNO") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="English Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEdate" runat="server" Text='<%# Bind("e_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Nep Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNdate" runat="server" Text='<%# Bind("n_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inst">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("INSTALLMENT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillAmt" runat="server" Text='<%# Bind("GRANDTOTAL") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Disc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDisc" runat="server" Text='<%# Bind("DISCOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rem Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemainingAmt" runat="server" Text='<%# Bind("REMAINING_BALANCE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmt" runat="server" Text='<%# Bind("F_GRANDTOTAL") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </label>
                </div>
            </div>

            <div id="div_receipt" runat="server" class="sub-container" style="width: 100%; display: inline-block; float: left">
                <div class="sub-container-heading">
                    Student Receipt History
                </div>
                <div class="row">
                    <label class="label col1">
                        <asp:GridView ID="gridReceipt" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Style="margin-top: 3px" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                            OnRowDataBound="gridReceipt_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Sem">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSemesterR" runat="server" Text='<%# Bind("SEMESTER") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSemCode" runat="server" ></asp:Label>
                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date (AD)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateEn" runat="server" Text='<%# Bind("RECEIPTDATEEN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date (BS)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay" runat="server" Text='<%# Bind("DAY") %>'></asp:Label>
                                        <asp:Label ID="lll" runat="server" Text="/"></asp:Label>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Month") %>'></asp:Label>
                                        <asp:Label ID="lll0" runat="server" Text="/"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("RECEIPTNO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="For Months">
                                    <ItemTemplate>
                                        <asp:Label ID="lblForMonths" runat="server" Text='<%# Bind("FORMONTH") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Installment No">
                                    <FooterTemplate>
                                        Total
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Bind("INSTALLMENT_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("PAIDAMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px"/>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </label>
                </div>
            </div>


            <div id="div_withdraw" runat="server" class="sub-container" style="width: 100%; display: inline-block; float: left">
                <div class="sub-container-heading">
                    Student WithDraw
                </div>
                <div class="row">
                    <label class="label col1">
                        <asp:GridView ID="gridWithdraw" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Style="margin-top: 3px" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                            OnRowDataBound="gridWithdraw_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sem">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSem" runat="server" Text='<%# Bind("SEMESTER") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSemCodeW" Text="" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                         
                                <asp:TemplateField HeaderText="Date (AD)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateEn" runat="server" Text='<%# Bind("RECEIPTDATEEN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date (BS)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay" runat="server" Text='<%# Bind("DAY") %>'></asp:Label>
                                        <asp:Label ID="lll" runat="server" Text="/"></asp:Label>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Month") %>'></asp:Label>
                                        <asp:Label ID="lll0" runat="server" Text="/"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("RECEIPTNO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <FooterTemplate>
                                        Total
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("PAIDAMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px"/>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </label>
                </div>
            </div>
        </div>

    </div>
</asp:Content>