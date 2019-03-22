<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="staff_list.aspx.cs" Inherits="administration_reports_staff_list" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Division              
                    <asp:DropDownList ID="ddlDivision" runat="server" Height="22px" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="trAcademic" visible="false">
                <td>Level
                     <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                     </asp:DropDownList>
                </td>

                <td>Program                
                        <asp:DropDownList ID="ddlProgram" runat="server" Height="22px"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View All" />
                </td>
                <td>&nbsp;</td>
            </tr>

        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridEmployeeDiary" runat="server" CssClass="gridtable" AutoGenerateColumns="False"
                        EnableModelValidation="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="gridEmployeeDiary_RowDataBound" OnRowCommand="gridEmployeeDiary_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1%>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Photo">
                                <ItemTemplate>
                                    <asp:Image ID="imgStudent" runat="server" Height="100px" Width="90px"></asp:Image>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee's Info">
                                <ItemTemplate>

                                    <asp:Image ID="imgregdno" runat="server" ImageUrl="~/images/icons/regdno.png"></asp:Image>:<asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EMPLOYEEID") %>' Style="font-weight: bold"></asp:Label><br>
                                    <asp:Image ID="imgStName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("FIRSTNAME") %>'></asp:Label>
                                                                                                                             <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label><br>
                                    <asp:Image ID="imgStAdd" runat="server" ImageUrl="~/images/icons/address.png"></asp:Image>:<asp:Label ID="lblAddressT" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgStPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("PHONE") %>'></asp:Label><br>
                                    <asp:Image ID="imgStMobile1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo1" runat="server" Text='<%# Bind("MOBILE1") %>'></asp:Label>
                                    <asp:Image ID="imgStMobile2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo2" runat="server" Text='<%# Bind("MOBILE2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />

                            </asp:TemplateField>


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="View" CommandName="View"></asp:Button>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit"></asp:Button>
                                     <asp:Button ID="btnViewSubject" runat="server" Text="View Subject" CommandName="ViewSubject"></asp:Button>
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

