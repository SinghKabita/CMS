<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="totaldue.aspx.cs" Inherits="finance_report_totaldue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" AutoPostBack="true" Height="22px" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" Height="22px" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" Height="22px" runat="server">
                    </asp:DropDownList>
                </td>
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
                    <asp:GridView ID="gridRemainingBalance" runat="server" Width="70%" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridRemainingBalance_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                                    <asp:LinkButton ID="lnkbtnStudentId" runat="server" OnClick="lnkbtnStudentId_Click" Text='<%# Bind("STUDENT_ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Due Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
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
