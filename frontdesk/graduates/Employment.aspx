<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employment.aspx.cs" Inherits="frontdesk_graduates_Employment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Batch
                </td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="lblPKIDU" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Student
                </td>
                <td>
                    <asp:DropDownList ID="ddlStudentId" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Organization
                </td>
                <td>
                    <asp:TextBox ID="txtOrganization" runat="server" Height="22px" Width="100%"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>From Date
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td>Date(BS)</td>
                <td>
                    <asp:TextBox ID="txtFDay" runat="server" Height="22px" Placeholder="DD" Width="30px" AutoPostBack="True" OnTextChanged="txtFDay_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtFMonth" runat="server" Height="22px" Placeholder="MM" Width="30px" AutoPostBack="True" OnTextChanged="txtFMonth_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtFYear" runat="server" Height="22px" Placeholder="YYYY" Width="50px" AutoPostBack="True" OnTextChanged="txtFYear_TextChanged"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>To Date
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td>Date(BS)</td>
                <td>
                    <asp:TextBox ID="txtTDay" runat="server" Height="22px" Placeholder="DD" Width="30px" AutoPostBack="True" OnTextChanged="txtTDay_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtTMonth" runat="server" Height="22px" Placeholder="MM" Width="30px" AutoPostBack="True" OnTextChanged="txtTMonth_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtTYear" runat="server" Height="22px" Placeholder="YYYY" Width="50px" AutoPostBack="True" OnTextChanged="txtTYear_TextChanged"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>Position
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server" Height="22px" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Currently Working
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtnStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>

                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
                </td>
                <td></td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridEmployment" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridEmployment_RowDataBound" OnRowCommand="gridEmployment_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch">
                                <HeaderTemplate>
                                    Batch
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("batch") %>'></asp:Label>
                                    <asp:Label ID="lblPKID" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <HeaderTemplate>
                                    Student Id
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <HeaderTemplate>
                                    Name
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Organization">
                                <HeaderTemplate>
                                    Organization
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOrganization" runat="server" Text='<%# Bind("Organization") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="From Date">
                                <HeaderTemplate>
                                    From Date
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" runat="server" Text='<%# Bind("From_DATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date">
                                <HeaderTemplate>
                                    To Date
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblToDate" runat="server" Text='<%# Bind("To_DATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Position">
                                <HeaderTemplate>
                                    Position
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPosition" runat="server" Text='<%# Bind("POSITION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currently Working">
                                <HeaderTemplate>
                                    Currently Working
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrentlyWorking" runat="server" Text='<%# Bind("WORKING_STATUS") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" CommandName="Change" runat="server" Text="" ImageUrl="~/images/icons/edit.png"></asp:ImageButton>
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

