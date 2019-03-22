<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentlist_ungenerated.aspx.cs" Inherits="administration_reports_StudentList_ungenerated" %>


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
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="text-align: left">Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" Enabled="false">
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View All" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 40px">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

        </table>
       <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridStudentListUngenerated" runat="server" CssClass="gridtable" AutoGenerateColumns="False"
                        EnableModelValidation="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="gridStudentListUngenerated_RowDataBound" OnRowCommand="gridStudentListUngenerated_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1%>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Photo">
                                <ItemTemplate>
                                    <asp:Label id="lblpkid" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"/>
                                    <asp:Image ID="imgStudent" runat="server" Height="100px" Width="90px"></asp:Image>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student's Detail">
                                <ItemTemplate>

                                   
                                    <asp:Image ID="imgStName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label><br />
                                                                                                                             
                                    <asp:Image ID="imgStAdd" runat="server" ImageUrl="~/images/icons/address.png"></asp:Image>:<asp:Label ID="lblAddressT" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgStPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("PHONE") %>'></asp:Label><br/>
                                    <asp:Image ID="imgStMobile1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo1" runat="server" Text='<%# Bind("MOBILE_1") %>'></asp:Label><br />
                                    <asp:Image ID="imgStMobile2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo2" runat="server" Text='<%# Bind("MOBILE_2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />

                            </asp:TemplateField>


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="View" CommandName="View"></asp:Button>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit"></asp:Button>
                                    
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
