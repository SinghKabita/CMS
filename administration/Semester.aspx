<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Semester.aspx.cs" Inherits="administration_Semester" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container mt-20">
        <div class="row">
            <div class="col-md-5 form-group">
                <table class="gridtable">
                    <tr>
                        <td>Faculty
                        </td>
                        <td>
                            <asp:Label Text="" runat="server" ID="lblID" Visible="false" />
                            <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Level
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>Program
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>Semester 
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSemester">
                                <asp:ListItem Value="1" Text="I" />
                                <asp:ListItem Value="2" Text="II" />
                                <asp:ListItem Value="3" Text="III" />
                                <asp:ListItem Value="4" Text="IV" />
                                <asp:ListItem Value="5" Text="V" />
                                <asp:ListItem Value="6" Text="VI" />
                                <asp:ListItem Value="7" Text="VII" />
                                <asp:ListItem Value="8" Text="VIII" />

                            </asp:DropDownList>
                        </td>
                        <td>
                          <asp:Button Text="View" runat="server" ID="btnView" OnClick="btnView_Click"/>  
                        </td>
                    </tr>

                    <tr>
                        <td>Compulsary Subject
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCompulsarySub" />
                        </td>
                    </tr>

                    <tr>
                        <td>Elective Subject
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtElectiveSub" />
                        </td>
                    </tr>

                    <tr>
                        <td>Syllabus Year
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSyllabusYear" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button Text="Save" runat="server" ID="btnSave" OnClick="btnSave_Click" />
                        </td>

                    </tr>


                </table>
            </div>


            <div class="col-md-7">
                <asp:GridView ID="gridSemester" CssClass="table table-bordered table-hover table-striped" OnRowDataBound="gridSemester_RowDataBound"
                    runat="server" AutoGenerateColumns="false"
                    OnRowCommand="gridSemester_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                SN
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text='<%# Container.DataItemIndex+1 %>' ID="lblSN" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Program 
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("PK_ID") %>' ID="lblPKG" runat="server" Visible="false" />
                                <asp:Label Text='<%# Bind("PROGRAM_ID") %>' ID="lblProgramIDG" runat="server" Visible="false" />
                                <asp:Label Text='' ID="lblProgramNameG" runat="server" Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Semester Code
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:Label Text='<%# Bind("SEMESTER_CODE") %>' ID="lblSemesterCodeG" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Semester
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:Label Text='<%# Bind("SEMESTER") %>' ID="lblSemesterG" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Compulsary Subject
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:Label Text='<%# Bind("COMPULSARY_SUBJECT") %>' ID="lblCompulsarySubjectG" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Elective Subject
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:Label Text='<%# Bind("ELECTIVE_SUBJECT") %>' ID="lblElectiveSubjectG" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Syllabus Year
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:Label Text='<%# Bind("SYLLABUS_YEAR") %>' ID="lblSyllabusYearG" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/images/icons/edit.png" ID="Edit" runat="server" CommandName="change" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
