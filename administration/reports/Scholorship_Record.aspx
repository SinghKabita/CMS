<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Scholorship_Record.aspx.cs" Inherits="administration_reports_Scholorship_Record" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
           <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Level<br />
                </td>
                <td>

                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Semester</td>
                <td>

                    <asp:DropDownList runat="server" ID="ddlSemester" Height="22px" AutoPostBack="True" >
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Style="height: 25px;" Text="View" OnClick="btnView_Click" />
                </td>
            </tr>

        </table>

        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" ID="gridScholorship" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridScholorship_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Bind("PK_ID") %>'></asp:Label>
                                    <asp:Label ID="lblSemester" runat="server" Visible="false" Text='<%# Bind("SEMESTER_ID") %>'></asp:Label>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>

                                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>

                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>

                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
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
        </table>
    </div>
</asp:Content>


