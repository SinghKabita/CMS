<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DropStudent_Record.aspx.cs" Inherits="administration_reports_DropStudent_Record" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>

                <td>Level
                </td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

                <tr>
                    
                    <td runat="server" >Batch</td>
                    <td runat="server" >
                        <asp:DropDownList runat="server" Height="22px" ID="ddlBatch" AutoPostBack="True" ></asp:DropDownList>
                    </td>

                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnView" runat="server" Text="View" Style="height: 22px;" OnClick="btnView_Click" />
                    </td>
                </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="griStudentList" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Semester">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("SEMESTER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drop Date (AD)">
                                <ItemTemplate>
                                    <asp:Label ID="lblEngdate" runat="server" Text='<%# Bind("engdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drop Date (BS)">
                                <ItemTemplate>
                                    <asp:Label ID="lblNepDate" runat="server" Text='<%# Bind("nepdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fincancial Clearance">
                                <ItemTemplate>
                                    <asp:Label ID="lblFncClear" runat="server" Text='<%# Bind("FINCANCIAL_CLEARANCE_STATUS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate>
                                    <asp:Label ID="lblReason" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approve By">
                                <ItemTemplate>
                                    <asp:Label ID="lblApproveBy" runat="server" Text='<%# Bind("APPROVE_BY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="lblUser" runat="server" Text='<%# Bind("USER_ID") %>'></asp:Label>
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

