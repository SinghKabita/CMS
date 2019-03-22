<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TU_Result.aspx.cs" Inherits="exam_boardexam_SGPA_Calculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">

            <tr>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Batch
                </td>
                <td>
                    <asp:DropDownList ID="ddlBatch" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label Text="" runat="server" ID="lblBatch" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>Semester
                </td>
                <td>
                    <asp:DropDownList ID="ddlSemester" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Student
                </td>
                <td>
                    <asp:DropDownList ID="ddlStudent" Height="22px" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click"></asp:Button>
                </td>
            </tr>

        </table>
        <div runat="server" visible="false" id="GPAGrid">
        <table style="width: 80%">
            <tr>
                <td>
                    <asp:GridView ID="gridCalculator" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridCalculator_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Result">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlResult" OnSelectedIndexChanged="ddlResult_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Text="Pass" />
                                        <asp:ListItem Text="Fail" />
                                        <asp:ListItem Text="Absent" />
                                        <asp:ListItem Text="Partial" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="" >
                                <ItemTemplate>
                                    <asp:CheckBox Text="Theory" runat="server" id="chkTheory" Visible="false"  />
                                    <asp:CheckBox Text="Practical" runat="server" id="chkPractical" Visible="false"   />                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Points">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPoint" Width="40px" Text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grade">
                                <ItemTemplate>
                                    <asp:Label Text="" runat="server" ID="lblGrade" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Credit Points">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                </FooterTemplate>

                                <FooterStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
            <tr id="tr_totalsgpa" runat="server" visible="false">
                <td style="text-align: right; padding-right: 60px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px">SGPA &nbsp;
                <asp:Label ID="lblTotalSGPA" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
            </td>

        </table>
        </div>

        <div runat="server" visible="false" id="PerGrid">
        <table style="width: 80%">
            <tr>
                <td>
                    <asp:GridView ID="gridPercentage" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True"
                         CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridPercentage_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Result">

                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlResult" OnSelectedIndexChanged="ddlResult_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Text="Pass" />
                                        <asp:ListItem Text="Fail" />
                                        <asp:ListItem Text="Absent" />
                                        <asp:ListItem Text="Partial" />
                                    </asp:DropDownList>

                                </ItemTemplate>


                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="" >

                                <ItemTemplate>
                                    <asp:CheckBox Text="Theory" runat="server" id="chkTheory" Visible="false"  />
                                    <asp:CheckBox Text="Practical" runat="server" id="chkPractical" Visible="false"   />
                                  
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Points">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPoint" Width="40px" Text="0" AutoPostBack="True" 
                                         OnTextChanged="txtPoint_TextChanged" />
                                    <asp:Label Text="" runat="server" ID="lblGrade" Visible="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
            <tr id="tr1" runat="server" visible="false">
                <td style="text-align: right; padding-right: 60px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px">SGPA &nbsp;
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
            </td>

        </table>
        </div>

    </div>

</asp:Content>



