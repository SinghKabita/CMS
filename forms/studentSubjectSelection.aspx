<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentSubjectSelection.aspx.cs" Inherits="forms_studentSubjectSelection" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>

                <td>Level<br />
                </td>
                <td>

                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                        <asp:ListItem Text="Master" Value="Master" />
                        <asp:ListItem Text="Bachelor" Value="Bachelor" Selected="True" />
                    </asp:DropDownList>
                </td>

                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>

            </tr>

            <tr>


                <td>Syllabus Year
                </td>
                <td>
                    <%--<asp:TextBox runat="server" ID="txtSyallabusYr" AutoPostBack="true" Width="100 px" OnTextChanged="txtSyallabusYr_TextChanged" />--%>

                     <asp:DropDownList runat="server" ID="ddlSyllabusYr" AutoPostBack="True" OnSelectedIndexChanged="ddlSyllabusYr_SelectedIndexChanged" >                       
                    </asp:DropDownList>

                </td>
                <td>Semester</td>
                <td>

                    <asp:DropDownList runat="server" ID="ddlSemester" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" Enabled="false">
                    </asp:DropDownList>
                </td>

            </tr>

            <tr>
                
                <td>Section</td>
                <td>
                    <asp:DropDownList runat="server" Height="22px" ID="ddlSection" AutoPostBack="True"></asp:DropDownList></td>
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

                        <asp:GridView ID="gridStudentSubjectSelection" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridStudentSubjectSelection_RowDataBound">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>' Width="100px"></asp:Label>
                                        <asp:Label ID="lblPkId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>' Width="200px"></asp:Label>
                                        <asp:Label ID="lblCompulsary" runat="server" Visible="false" Text='<%# Bind("COMPULSARY_SUBJECT") %>'></asp:Label>
                                        <asp:Label ID="lblElective" runat="server" Visible="false" Text='<%# Bind("ELECTIVE_SUBJECT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 1">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub1" runat="server" Height="22px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 2">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub2" runat="server" Height="22px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 3">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub3" runat="server" Height="22px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 4">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub4" runat="server" Height="22px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 5">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub5" runat="server" Height="22px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 6">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub6" runat="server" Height="22px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 7">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub7" runat="server" Height="22px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </td>

                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Button runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

