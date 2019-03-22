<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LogEntry.aspx.cs" Inherits="frontdesk_visitors_LogEntry" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Date</td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>Name</td>
                <td colspan="4">
                    <asp:TextBox ID="txtVisitorsName" runat="server" Width="100%" Height="22px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Organization</td>
                <td colspan="4">
                    <asp:TextBox ID="txtOrganization" runat="server" Width="100%" Height="22px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Purpose</td>
                <td colspan="4">
                    <asp:TextBox ID="txtPurpose" runat="server" Width="100%" Height="55px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Phone No</td>
                <td>
                    <asp:TextBox ID="txtPhoneNo" runat="server" Height="22px"></asp:TextBox>
                </td>
                <td></td>
                <td>Vehicle No</td>
                <td>
                    <asp:TextBox ID="txtVehicleNo" runat="server" Height="22px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Time In</td>
                <td>
                    <asp:TextBox ID="txtTimeIn" runat="server" Height="22px"></asp:TextBox>
                </td>
                <td></td>
                <td>Time Out</td>
                <td>
                    <asp:TextBox ID="txtTimeOut" runat="server" Height="22px"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="griLogList" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date AD">
                                <HeaderTemplate>
                                    Date (AD)
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("VISIT_DATE") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date BS">
                                <HeaderTemplate>
                                    Date (BS)
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDay" runat="server" Text='<%# Bind("VISIT_DAY") %>'></asp:Label>
                                    /<asp:Label ID="lblMonth" runat="server" Text='<%# Bind("VISIT_MONTH") %>'></asp:Label>
                                    /<asp:Label ID="lblYear" runat="server" Text='<%# Bind("VISIT_YEAR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Visitor's Name">
                                <HeaderTemplate>
                                    Visitor's Name
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblVisitorName" runat="server" Text='<%# Bind("VISITORS_NAME") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Organization">
                                <HeaderTemplate>
                                    Organization
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOrganization" runat="server" Height="22px" Text='<%# Bind("ORGANIZATION") %>'></asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purpose">
                                <HeaderTemplate>
                                    Purpose
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPurpose" runat="server" Height="22px" Text='<%# Bind("PURPOSE") %>'></asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone No">
                                <HeaderTemplate>
                                    Phone No
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Bind("CONTACT_NO") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vechicle No">
                                <HeaderTemplate>
                                    Vechicle No
                                </HeaderTemplate>

                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Bind("VECHICLE_NO") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time In">
                                <ItemTemplate>
                                    <asp:Label ID="lblTimeIn" runat="server" Text='<%# Bind("TIME_IN") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time Out">
                                <ItemTemplate>
                                    <asp:Label ID="lblTimeOut" runat="server" Text='<%# Bind("TIME_OUT") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
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
