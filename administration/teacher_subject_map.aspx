<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="teacher_subject_map.aspx.cs" Inherits="administration_teacher_subject_map" %>

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
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Semester</td>
                <td>
                    <asp:DropDownList runat="server" Height="22px" ID="ddlSemester" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr runat="server" visible="true">
                <td>Batch</td>
                <td>
                    <asp:DropDownList runat="server" Height="22px" ID="ddlBatch" AutoPostBack="True" Enabled="false"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click"></asp:Button>
                </td>

            </tr>
        </table>
        <div id="hide" runat="server" visible="false">
            <table class="gridtable">
                <tr>
                    <td>Subject
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubject" runat="server" Height="22px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Teacher
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTeacher" runat="server" Height="22px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Add" OnClick="btnSave_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gridMapTable" runat="server" Width="60%" AutoGenerateColumns="False" CssClass="gridtable" EnableModelValidation="True" OnRowDataBound="gridMapTable_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubjectId" runat="server" Visible="false" Text='<%# Bind("subject_id") %>'></asp:Label>
                                        <asp:Label ID="lblSubjectName" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teacher">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTeacherId" runat="server" Visible="false" Text='<%# Bind("teacher_id") %>'></asp:Label>
                                        <asp:Label ID="lblTeacherName" runat="server" Text=""></asp:Label>
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
    </div>
</asp:Content>

