<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Download_Old_Qns.aspx.cs" Inherits="library_library_Download_Old_Qns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you really Download?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <table class="gridtable" style="width: 50%">
            <tr>
                <td>Year</td>
                <td colspan="2">
                    <asp:TextBox ID="txtYear" OnTextChanged="txtYear_TextChanged" AutoPostBack="true" runat="server" Height="22px" Style="font-size: medium"></asp:TextBox>
                    <asp:Label Text="in BS" ForeColor="RosyBrown" runat="server" />
                    <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtYear" runat="server" />
                    <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtYear" Type="Integer"
                        Operator="DataTypeCheck" ErrorMessage="Value must be numeric!" />

                </td>

            </tr>
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>

                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList></td>

            </tr>
            <tr>
                <td>Subject</td>
                <td>
                    <asp:DropDownList ID="ddlSubject" Height="22px" runat="server"></asp:DropDownList></td>


                <td>Exam Type</td>
                <td>
                    <asp:DropDownList ID="ddlExamType" Height="22px" runat="server"></asp:DropDownList></td>

            </tr>

            <tr>

                <td>
                    <asp:Button ID="btnView" runat="server" Height="22px" Text="View" OnClick="btnView_Click" /></td>


            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td style="width: 100%">

                    <asp:GridView ID="gridList" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" OnRowDataBound="gridList_RowDataBound" CellPadding="4" GridLines="None" AllowPaging="True" OnPageIndexChanging="gridList_PageIndexChanging" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="img" runat="server" Width="50px" Height="50px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>
                                    <%-- <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("File") %>'></asp:Label>--%>
                                    <asp:Label ID="lblFileName" Text='<%# Bind("TOPIC") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblSize" runat="server" Text='<%# Eval("SIZE") %>'></asp:Label>--%>
                                    <asp:Label ID="lblSize" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server"></asp:Label>
                                    <asp:Label ID="lblLinkName" runat="server" Text='<%# Bind("FILE_NAME") %>' Visible="false"></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:TemplateField>



                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--<asp:ImageButton ID="Button1" runat="server" CommandName="Download" Text="Download" CommandArgument='<%# Eval("File") %>' ImageUrl="~/images/fileicon/download.png" Width="50px" />--%>
                                    <asp:ImageButton ID="btnDownload" runat="server" ToolTip="Download" ImageUrl="~/images/fileicon/download.png" Width="40px" Height="40px" OnClientClick="Confirm();" OnClick="btnDownload_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
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

