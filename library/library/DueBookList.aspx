<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DueBookList.aspx.cs" Inherits="library_library_DueBookList" %>

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
                    <asp:DropDownList ID="ddlLevel" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" 
                        AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Semester
                </td>
                <td>
                    <asp:DropDownList ID="ddlSemester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                        AutoPostBack="true" runat="server">
                    </asp:DropDownList>

                </td>
                 <td>
                    <asp:DropDownList ID="ddlBatch" placeholder="Batch" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtNumberOfDays" runat="server" Height="22px"></asp:TextBox>&nbsp;Days
                </td>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click"></asp:Button>
                </td>

            </tr>
            <tr>
                <td colspan="4">
                    <asp:RequiredFieldValidator CssClass="navbar-right" ErrorMessage="Select Semester" InitialValue="Select" ControlToValidate="ddlSemester" runat="server" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>

                    <asp:GridView ID="gridBookDue" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch">
                                <ItemTemplate>
                                    <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("BATCH") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Semester">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("SEMESTER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SECTION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("ISSUETO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Book Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblBookName" runat="server" Text='<%# Bind("BOOKNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NSBN">
                                <ItemTemplate>
                                    <asp:Label ID="lblBookNumber" runat="server" Text='<%# Bind("NSBN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblIssueDate" runat="server" Text='<%# Bind("ISSUEDATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" OnClick="btnSendSMS_Click"></asp:Button>
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

