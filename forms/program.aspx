<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="program.aspx.cs" Inherits="forms_program" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridProgarm" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" OnRowCancelingEdit="gridProgarm_RowCancelingEdit" OnRowDataBound="gridProgarm_RowDataBound" OnRowEditing="gridProgarm_RowEditing" OnRowUpdating="gridProgarm_RowUpdating">
                        <AlternatingRowStyle BackColor="#FFCCFF" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Faculty">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlFacultyE" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic" 
                                    ValidationGroup="g1" runat="server" ControlToValidate="ddlFacultyE"
                                    Text="*" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>

                                    <asp:Label ID="lblFacultyE" runat="server" Text='<%# Bind("FACULTY_ID") %>' Visible="False"></asp:Label>
                                    <asp:Label ID="lblPKIDU" runat="server" Text='<%# Bind("PK_ID") %>' Visible="False"></asp:Label>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Faculty
                                    <br />

                                    <asp:DropDownList ID="ddlFacultyH" runat="server">
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID1" Display="Dynamic" 
                                    ValidationGroup="g1" runat="server" ControlToValidate="ddlFacultyH"
                                    Text="*" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFaculty" runat="server"></asp:Label>
                                    <asp:Label ID="lblFac" runat="server" Text='<%# Bind("FACULTY_ID") %>' Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Level">
                                <EditItemTemplate>
                                   
                                    <asp:DropDownList ID="ddlLevelE" runat="server" Height="22px" AutoPostBack="True">
                                    </asp:DropDownList>

                                    <asp:Label ID="lblLevelE" runat="server" Text='<%# Bind("PROGRAM_LEVEL") %>' visible="false"></asp:Label>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Level
                                    <br />                                  
                                     <asp:DropDownList ID="ddlLevelH" runat="server" Height="22px" AutoPostBack="True">                                      
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:Label ID="lblLevel" runat="server" Text='<%# Bind("PROGRAM_LEVEL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Program Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProgramCodeE" runat="server" Text='<%# Bind("PROGRAM_CODE") %>' Width="80px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="id1" runat="server" 
                                 ControlToValidate="txtProgramCodeE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>--%>

                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Program Code<br />
                                    <asp:TextBox ID="txtProgramCodeH" runat="server" Width="80px"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="id2" runat="server" 
                                 ControlToValidate="txtProgramCodeH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>--%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProgramCode" runat="server" Text='<%# Bind("PROGRAM_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="RESULT TYPE">
                                <EditItemTemplate>
                                   
                                    <asp:DropDownList ID="ddlResultTypeE" runat="server" Height="22px" AutoPostBack="True">  
                                         <asp:ListItem Text="GPA" Selected="true" />
                                        <asp:ListItem Text="Percentage" />                                    
                                    </asp:DropDownList>

                                    <asp:Label ID="lblResultTypeE" runat="server" Text='<%# Bind("RESULT_TYPE") %>' visible="false"></asp:Label>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    RESULT TYPE 
                                    <br />                                  
                                     <asp:DropDownList ID="ddlResultTypeH" runat="server" Height="22px" AutoPostBack="True">  
                                         <asp:ListItem Text="GPA" Selected="true" />
                                        <asp:ListItem Text="Percentage" />                                    
                                    </asp:DropDownList>

                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:Label ID="lblResultType" runat="server" Text='<%# Bind("RESULT_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Program Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProgramNameE" runat="server" Text='<%# Bind("PROGRAM_NAME") %>' Width="250px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="id4" runat="server" 
                                 ControlToValidate="txtProgramNameE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>--%>
                               
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Program Name<br />
                                    <asp:TextBox ID="txtProgramNameH" runat="server" Width="250px"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="id4" runat="server" 
                                 ControlToValidate="txtProgramNameH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                               --%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProgramName" runat="server" Text='<%# Bind("PROGRAM_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/icons/upload.png" CommandName="Update" />
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/icons/delete.gif" CommandName="Cancel" />
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>
