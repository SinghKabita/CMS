<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="advancecollection.aspx.cs" Inherits="finance_advancecollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px">
                    </asp:DropDownList></td>

                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" /></td>
            </tr>
        </table>

        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridAdvCollection" runat="server" Width="70%" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridAdvCollection_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch">
                                <ItemTemplate>
                                    <asp:Label ID="lblBatch" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnStudentId" runat="server" Text='<%# BIND("STUDENT_ID") %>' OnClick="lnkbtnStudentId_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Advance">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdvance" runat="server" Text='<%# bind("amount") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
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

