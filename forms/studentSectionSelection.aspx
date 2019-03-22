<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentSectionSelection.aspx.cs" Inherits="forms_studentSectionSelection" %>

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

                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"></asp:DropDownList>
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
                <td>Semester</td>
                <td>

                    <asp:DropDownList runat="server" ID="ddlSemester" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div id="detail" runat="server" visible="false">
            <table>
                <tr>
                    <td style="margin-left: 40px">

                        <asp:GridView ID="gridStudentSectionSelection" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridStudentSectionSelection_RowDataBound">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
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
                                <asp:TemplateField HeaderText="Student Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSection" runat="server">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblSection" runat="server" Visible="false" Text='<%# Bind("SECTION") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

