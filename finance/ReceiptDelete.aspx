<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReceiptDelete.aspx.cs" Inherits="finance_ReceiptDelete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you really want to Delete?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Receipt Date</td>
                <td>
                    <asp:TextBox runat="server" ID="txtReceiptDate" AutoPostBack="True" Height="22px"
                        OnTextChanged="txtReceiptDate_TextChanged"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceiptDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td>Date (BS)</td>
                <td>
                    <asp:TextBox ID="txtDay" runat="server" Height="22px" Placeholder="DD" Width="30px" AutoPostBack="True" OnTextChanged="txtDay_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtMonth" runat="server" Height="22px" Placeholder="MM" Width="30px" AutoPostBack="True" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtYear" runat="server" Height="22px" Placeholder="YYYY" Width="50px" AutoPostBack="True" OnTextChanged="txtYear_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnView" Text="View" OnClick="btnView_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <table>
            <tr id="monthly_head" runat="server" visible="false">
                <td>
                    <h3 style="color: black">Monthly Receipt:</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView runat="server" CssClass="gridtable" AutoGenerateColumns="False"
                        EnableModelValidation="True" ID="gridReceiptList"
                        OnRowCommand="gridReceiptList_RowCommand">
                        <AlternatingRowStyle BackColor="#e2e2e2" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt No">
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("receiptno") %>'></asp:Label>
                                    <asp:Label ID="lblReceiptId" runat="server" Visible="false" Text='<%# Bind("receiptid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENTID") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch">
                                <ItemTemplate>

                                    <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("BATCHNO") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Installment No">
                                <ItemTemplate>

                                    <asp:Label ID="lblInstallment" runat="server" Text='<%# Bind("INSTALLMENT_NO") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("PAIDAMOUNT") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received By">
                                <ItemTemplate>
                                    <asp:Label ID="lblUser" runat="server" Text='<%# Bind("RECEIVED_BY") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete"
                                        CommandName="Remove"></asp:Button>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                </td>
            </tr>




            <tr>
                <td>
                    <asp:GridView runat="server" ID="gridMonth" AutoGenerateColumns="False" EnableModelValidation="True" Visible="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Months">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonths" runat="server" Text='<%# Bind("months") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

        <div style="display: none;">
            <asp:Button ID="btnDummy" Text="Dummy" runat="server" />
        </div>

        <cc1:ModalPopupExtender ID="GetRemarks_ModalPopupExtender" runat="server"
            BackgroundCssClass="modalBackground" CancelControlID="btnCancel"
            DynamicServicePath="" Enabled="True" PopupControlID="divGetRemarks" PopupDragHandleControlID="divGetRemarks"
            TargetControlID="btnDummy">
        </cc1:ModalPopupExtender>
        <div id="divGetRemarks" class="PrioritymodalPopup" style="width: 30%">

            <table class="gridtable">
                <tr>
                    <td colspan="2">
                        <h3 style="color: black">You are deleting</h3>
                    </td>
                </tr>
                <tr>
                    <td>Receipt No:
                    </td>
                    <td>

                        <asp:Label ID="lblReceiptNoP" runat="server" Text=""></asp:Label>

                        <asp:Label ID="lblReceiptIdP" runat="server" Visible="false"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>Student Id:
                    </td>
                    <td>

                        <asp:Label ID="lblStudentIdP" runat="server" Text=""></asp:Label>

                        &nbsp;(<asp:Label ID="lblBatchP" runat="server" Text=""></asp:Label>
                        )</td>
                </tr>
                <tr>
                    <td>Student Name:
                    </td>
                    <td>

                        <asp:Label ID="lblStudentNameP" runat="server" Text=""></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>Installment No:
                    </td>
                    <td>

                        <asp:Label ID="lblInstallmentNoP" runat="server" Text=""></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>Why are you deleting? :
                    </td>

                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtDeleteRemarks" runat="server" TextMode="MultiLine"
                            Width="100%" Height="50px"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td align="center" colspan="2" style="margin-left: 80px">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                    </td>
                </tr>


            </table>
        </div>
    </div>
</asp:Content>
