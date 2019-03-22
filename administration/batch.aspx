<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="batch.aspx.cs" Inherits="administration_batch" %>
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
                <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    <%-- <asp:ListItem Text="Master" value="Master"/>
                                <asp:ListItem Text="Bachelor" value="Bachelor" Selected="True"/>  --%>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Program
            </td>
            <td>
                <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" ></asp:DropDownList>
            </td>
        </tr>
            <tr>
                <td>Syllabus Year</td>
                <td>
                    <%--<asp:TextBox ID="txtSyllabusYear" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtSyllabusYear_TextChanged" ></asp:TextBox>--%>
                    <asp:DropDownList runat="server" ID="ddlSyllabusYr" AutoPostBack="True" OnSelectedIndexChanged="ddlSyllabusYr_SelectedIndexChanged" >
                        
                    </asp:DropDownList>

                </td>
            </tr>
    </table>

    <table class="gridtable">
        <tr>
            <td>
                <asp:GridView ID="gridBatch" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True"
                    OnRowCancelingEdit="gridBatch_RowCancelingEdit" OnRowDataBound="gridBatch_RowDataBound" OnRowEditing="gridBatch_RowEditing"
                    OnRowUpdating="gridBatch_RowUpdating">
                    <AlternatingRowStyle BackColor="#FFCCFF" />
                    <Columns>
                        <asp:TemplateField HeaderText="SN">
                            <ItemTemplate>
                                <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Batch">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBatchE" runat="server" Height="22px" Text='<%#Bind("BATCH") %>'></asp:TextBox>
                                <asp:Label ID="lblPKIDE" runat="server" Text='<%#Bind("BATCHID") %>' Visible="False"></asp:Label>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Batch<br />
                                <asp:TextBox ID="txtBatchH" runat="server" Height="22px"></asp:TextBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("BATCH") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="Semester">
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlSemesterE">
                                </asp:DropDownList>

                                <asp:Label ID="lblSemE" runat="server" Text='<%# Bind("SEMESTER") %>' Visible="false"></asp:Label>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Semester<br />
                                <asp:DropDownList runat="server" ID="ddlSemesterH">
                                </asp:DropDownList>

                            </HeaderTemplate>

                            <ItemTemplate>
                                <asp:Label ID="lblSemester" runat="server"></asp:Label>
                                <asp:Label ID="lblSem" runat="server" Text='<%# Bind("SEMESTER") %>' Visible="false"></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStatusE" runat="server" Height="22px">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblStatE" runat="server" Text='<%# Bind("ACTIVE") %>' Visible="False"></asp:Label>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Status<br />
                                <asp:DropDownList ID="ddlStatusH" runat="server" Height="22px">

                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                <asp:Label ID="lblStat" runat="server" Text='<%# Bind("ACTIVE") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Update" ImageUrl="~/images/icons/upload.png" />
                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Cancel" ImageUrl="~/images/icons/delete.gif" />
                            </EditItemTemplate>
                            <HeaderTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Edit" ImageUrl="~/images/icons/edit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
