<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentlist.aspx.cs" Inherits="administration_reports_studentlist" %>

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

                <td>Student Id</td>
                <td>
                    <asp:DropDownList ID="ddlStudentId" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged">
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
                    <asp:GridView ID="gridStudentDiary" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridStudentDiary_RowDataBound" OnRowCommand="gridStudentDiary_RowCommand">
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
                            <asp:TemplateField HeaderText="Student's Info">
                                <ItemTemplate>
                                    <asp:Label ID="lblpkid" runat="server" Text='<%# Bind("PK_ID") %>' style="font-weight:bold" Visible="false"></asp:Label><br>
                                    <asp:Image ID="imgregdno" runat="server" ImageUrl="~/images/icons/regdno.png"></asp:Image>:<asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>' Style="font-weight: bold"></asp:Label><br>
                                    <asp:Image ID="imgStName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("name_english") %>'></asp:Label><br>
                                    <asp:Image ID="imgStAdd" runat="server" ImageUrl="~/images/icons/address.png"></asp:Image>:<asp:Label ID="lblAddressT" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgStPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("phone") %>'></asp:Label><br>
                                    <asp:Image ID="imgStMobile1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo1" runat="server" Text='<%# Bind("mobile_1") %>'></asp:Label>
                                    <asp:Image ID="imgStMobile2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo2" runat="server" Text='<%# Bind("mobile_2") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Father's Info">
                                <ItemTemplate>
                                    <asp:Image ID="imgFName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblFName" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgFPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblFContactNo" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgFMobile1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblFContactNo1" runat="server" Text=""></asp:Label><br>
                                     <asp:Image ID="imgFMobile2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblFContactNo2" runat="server" Text=""></asp:Label><br>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mother's Info">
                                <ItemTemplate>
                                    <asp:Image ID="imgMName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblMName" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgMPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblMContactNo" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgMMobile1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblMContactNo1" runat="server" Text=""></asp:Label><br />
                                     <asp:Image ID="imgMMobile2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblMContactNo2" runat="server" Text=""></asp:Label><br>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Spouse's Info">
                                <ItemTemplate>
                                    <asp:Image ID="imgSName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblSName" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgSPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblSContactNo" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgSMobile1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblSContactNo1" runat="server" Text=""></asp:Label><br />
                                    <asp:Image ID="imgSMobile2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblSContactNo2" runat="server" Text=""></asp:Label><br>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Guardian's Info">
                                <ItemTemplate>
                                    <asp:Image ID="imgGName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblGName" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgGRelation" runat="server" ImageUrl="~/images/icons/relation.png"></asp:Image>:<asp:Label ID="lblGRelation" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgGPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblGContactNo" runat="server" Text=""></asp:Label><br>
                                    <asp:Image ID="imgGMobile1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblGContactNo1" runat="server" Text=""></asp:Label>
                                    <asp:Image ID="imgGMobile2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblGContactNo2" runat="server" Text=""></asp:Label>
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



