<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="yearlydistribution.aspx.cs" Inherits="finance_report_yearlydistribution" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        #center-content table {
            width: 80%;
        }




        #center-content td {
            padding: 10px;
        }
    </style>

    <div class="container">
        <p class="style2" style="font-weight: bold; font-size: large">
            Yearly Collection
        </p>
        <asp:Repeater ID="rptrTotalCollection" runat="server"
            OnItemDataBound="rptrTotalCollection_ItemDataBound">
            <ItemTemplate>
                <table border="1" cellspacing="0">
                    <tr style="background-color: #8E3663; color: White">
                        <th>&nbsp;
                        </th>
                        <th>Amount
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:ImageButton ID="imgbtn" runat="server" ImageUrl="~/images/icons/Plus.gif" OnClick="imgbtn_Click" />
                            Total Collection Till date
                        </td>
                        <td id="tdAmount" runat="server" style="text-align: right">

                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AMOUNT")%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%">
                            <div id="DivYearCollection" runat="server" visible="false">
                                <table border="1" cellspacing="0">
                                    <tr style="background-color: #8E3663; color: White">
                                        <th style="width: 300px;">&nbsp
                                        </th>
                                        <th>Amount
                                        </th>
                                    </tr>
                                    <asp:Repeater ID="rptrYearCollection" runat="server" OnItemDataBound="rptrYearCollection_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: left">
                                                    <%-- <asp:ImageButton ID="imgbtnYearCollection" runat="server" ImageUrl="~/images/icons/plus.gif"
                                                    ToolTip='<%# DataBinder.Eval(Container, "DataItem.YEAR")%>' OnClick="imgbtnClass_Click" />--%>
                                                Total Collection for YEAR <b>
                                                    <%# DataBinder.Eval(Container, "DataItem.FISCALYEAR")%></b>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblBatchWiseTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AMOUNT")%>'></asp:Label>
                                                </td>
                                            </tr>
                                            <%--  <tr>
                                            <td colspan="5">
                                                <div id="DivClassCollection" runat="server" visible="false">
                                                    <table border="1" cellspacing="0" >
                                                        <tr style="background-color: #8E3663; color: White">
                                                            <th style="width: 300px;">
                                                                &nbsp
                                                            </th>
                                                            <th>
                                                                Amount
                                                            </th>
                                                        </tr>
                                                        <asp:Repeater ID="rptrClassCollection" runat="server" onitemdatabound="rptrClassCollection_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <%--<asp:ImageButton ID="imgbtnClassCollection" runat="server" ImageUrl="~/images/icons/plus.gif"
                                                                            ToolTip='<%# DataBinder.Eval(Container, "DataItem.SEMESTER")%>' OnClick="imgbtnMainLedger_Click" />
                                                                        Total Collection for Class <b>
                                                                            <%# DataBinder.Eval(Container, "DataItem.SEMESTER")%></b>
                                                                    </td>
                                                                    <td style="text-align:right">
                                                                          <asp:Label ID="lblClassWiseTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AMOUNT")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                              
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>--%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

