<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TeacheProgMapping.aspx.cs" Inherits="administration_TeacheProgMapping" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty</td>

                <td>

                    <asp:DropDownList ID="ddlFaculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" runat="server" Height="22px"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator0"
                        Display="Dynamic" runat="server"
                        ControlToValidate="ddlFaculty"
                        InitialValue="Select" ErrorMessage="Please Select one">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Level
                </td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" Height="22px"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        Display="Dynamic" runat="server"
                        ControlToValidate="ddlLevel"
                        InitialValue="Select" ErrorMessage="Please Select one" />
                </td>
            </tr>
            <tr>
                <td>Program</td>
                <td>

                    <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" runat="server" Height="22px"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        Display="Dynamic" runat="server"
                        ControlToValidate="ddlProgram"
                        InitialValue="Select" ErrorMessage="Please Select one" />
                </td>
            </tr>
        </table>

        <table class="gridtable">
            <tr>
                <td>
                    <asp:GridView ID="gridTeacherProgMap" OnRowDataBound="gridTeacherProgMap_RowDataBound" CssClass="gridtable" Width="500px"
                        AutoGenerateColumns="false" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowCancelingEdit="gridTeacherProgMap_RowCancelingEdit" OnRowEditing="gridTeacherProgMap_RowEditing" OnRowUpdating="gridTeacherProgMap_RowUpdating">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    <asp:Label ID="lblPKIDE" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Teacher">
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlTeacherE">
                                    </asp:DropDownList>

                                    <asp:Label ID="lblTeacherE" runat="server" Visible="true"></asp:Label>
                                    <asp:Label ID="lblTeachE" Text='<%# Bind("TEACHERID") %>' runat="server" Visible="false" />

                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Teacher<br />
                                    <asp:DropDownList runat="server" ID="ddlTeacherH" Width="200px">
                                    </asp:DropDownList>
                                </HeaderTemplate>

                                <ItemTemplate>

                                    <asp:Label ID="lblTeacher" runat="server" Text='<%# Bind("TEACHERID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblTeacherName" runat="server" Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Available">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStatusE" runat="server" Height="22px">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem Value="1">Available</asp:ListItem>
                                        <asp:ListItem Value="0">Not Available</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblStatE" runat="server" Text='<%# Bind("STATUS") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>

                                <HeaderTemplate>
                                    Status<br />
                                    <asp:DropDownList ID="ddlStatusH" runat="server" Height="22px">

                                        <asp:ListItem Value="1">Available</asp:ListItem>
                                        <asp:ListItem Value="0">Not Available</asp:ListItem>
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    <asp:Label ID="lblStat" runat="server" Text='<%# Bind("STATUS") %>' Visible="False"></asp:Label>
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
