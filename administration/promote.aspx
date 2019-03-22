<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="promote.aspx.cs" Inherits="administration_promote" %>

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

            <tr runat="server" visible="false">
                <td>Batch</td>
                <td>
                    <asp:DropDownList runat="server" Height="22px" ID="ddlBatch" AutoPostBack="True"></asp:DropDownList></td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                </td>
                <td>
                    <asp:Button ID="btnPromote" runat="server" OnClick="btnPromote_Click" Text="Promote" />
                </td>
            </tr>
        </table>
        <div id="detail" runat="server" visible="false">
            <table>
                <tr>
                    <td>

                        <asp:GridView ID="gridPromotion" runat="server" AutoGenerateColumns="False" CssClass="gridtable" 
                            EnableModelValidation="True" OnRowDataBound="gridPromotion_RowDataBound">
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
                                        <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SECTION") %>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>' Width="200px"></asp:Label>
                                        <asp:Label ID="lblCompulsary" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblElective" runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Semester">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("SEMESTER") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="lblSem" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 1">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub1" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 2">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub2" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 3">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub3" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 4">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub4" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 5">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub5" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 6">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub6" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub 7">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSub7" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>

