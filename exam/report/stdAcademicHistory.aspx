<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="stdAcademicHistory.aspx.cs" Inherits="exam_report_stdAcademicHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-20">
        <div class="row">
            <div class="col-md-12 form-group">
                <table class="gridtable">
                    <tr>
                        <td>Faculty
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
                        <td style="text-align: left">Batch</td>
                        <td>
                            <asp:DropDownList ID="ddlBatch" runat="server" Height="22px">
                            </asp:DropDownList>
                        </td>

                        <td>
                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View" />
                        </td>
                    </tr>
                </table>

                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gridStdAcadHistory" runat="server" CssClass="gridtable" AutoGenerateColumns="False"
                                OnRowDataBound="gridStdAcadHistory_RowDataBound"
                                OnRowCommand="gridStdAcadHistory_RowCommand"
                                EnableModelValidation="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
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

                                            <asp:Label ID="lblpkid" runat="server" Text='<%# Bind("PK_ID") %>' Style="font-weight: bold" Visible="false"></asp:Label></br>
                                            <asp:Image ID="imgregdno" runat="server" ImageUrl="~/images/icons/regdno.png"></asp:Image>:<asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>' Style="font-weight: bold"></asp:Label></br>
                                            <asp:Image ID="imgStName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label></br>
                                            <asp:Image ID="imgStAdd" runat="server" ImageUrl="~/images/icons/address.png"></asp:Image>:<asp:Label ID="lblAddressT" runat="server" Text=""></asp:Label></br>
                                            <asp:Image ID="imgStMobile" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo0" runat="server" Text='<%# Bind("mobile_1") %>'></asp:Label></br>
                                            <asp:Image ID="imgGPA" runat="server" ImageUrl="~/images/icons/gpa_icon.png"></asp:Image>:<asp:Label ID="lblCGPA" runat="server" Text=''></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnView" runat="server" Text="View Detail" CommandName="View"></asp:Button>
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
    </div>
</asp:Content>

