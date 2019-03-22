<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SMSReminder.aspx.cs" Inherits="finance_SMSReminder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td colspan="2">
                    <h3 style="color: black; text-align: left">SMS Reminder</h3>
                </td>
            </tr>
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
                
                <td>Batch
                    &nbsp;<asp:DropDownList runat="server" ID="ddlBatch" Height="22px" Style="margin-left: 0px"></asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:RadioButtonList ID="rbtnGroup" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Both</asp:ListItem>
                        <asp:ListItem Value="Primary">Primary Number</asp:ListItem>
                        <asp:ListItem Value="Secondary">Secondary Number</asp:ListItem>
                    </asp:RadioButtonList>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnView" Text="View" OnClick="btnView_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gridRemainingBalance" CssClass="gridtable" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridRemainingBalance_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remaining Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
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
            <tr>
                <td>
                    <asp:Button ID="btnSend" Text="Send" runat="server" OnClick="btnSend_Click" /></td>
            </tr>

        </table>

        <table>
            <tr>
                <td>

                    <asp:GridView ID="gridSmsNoList" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Visible="false">
                        <Columns>
                            <asp:TemplateField HeaderText="SMSNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblSMSNo" runat="server" Text='<%# bind("smsno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
