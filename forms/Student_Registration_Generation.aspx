<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Student_Registration_Generation.aspx.cs" Inherits="forms_Student_Registration_generation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:dropdownlist id="ddlFaculty" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlFaculty_SelectedIndexChanged"></asp:dropdownlist>
                </td>
            </tr>

            <tr>
                <td>Level
                </td>
                <td>
                    <asp:dropdownlist id="ddlLevel" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlLevel_SelectedIndexChanged"></asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td>Program
                </td>
                <td>
                    <asp:dropdownlist id="ddlProgram" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlProgram_SelectedIndexChanged"></asp:dropdownlist>
                </td>
            </tr>



            <tr id="Tr1" runat="server" visible="true">
                <td>Batch</td>
                <td>
                    <asp:dropdownlist runat="server" height="22px" id="ddlBatch" autopostback="True" onselectedindexchanged="ddlBatch_SelectedIndexChanged"></asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button id="btnView" runat="server" text="View" onclick="btnView_Click"></asp:button>
                </td>
                <td>Enter First Reg No:
                    <asp:label id="lblCode" runat="server" text="Label"></asp:label>
                    <asp:textbox id="txtRegno" runat="server"></asp:textbox>
                    <asp:button id="btnGenerate" runat="server" text="Generate" onclick="btnGenerate_Click"></asp:button>
                </td>
                <td>
                    <asp:button id="btnSave" runat="server" text="Add" onclick="btnSave_Click"></asp:button>
                </td>


            </tr>
            <tr runat="server" id="sem_shift" visible="false">
                <td>Semester
                
                    <asp:dropdownlist runat="server" height="22px" id="ddlSemester" autopostback="True"></asp:dropdownlist>
                </td>
                <%--<td>Shift
                
                    <asp:DropDownList runat="server" ID="ddlshift">
                        <asp:ListItem Text="M" Value="M" />
                        <asp:ListItem Text="D" Value="D" />
                        <asp:ListItem Text="E" Value="E" />
                    </asp:DropDownList>

                </td>--%>
            </tr>

        </table>

        <table style="width: 100%">
            <tr>
                <td>
                    <asp:gridview id="gridStudentTable" runat="server" width="60%" autogeneratecolumns="False" cssclass="gridtable"
                        enablemodelvalidation="True" onrowdatabound="gridStudentTable_RowDataBound" cellpadding="4" forecolor="#333333"
                        gridlines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblpkid" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                           <%-- <asp:TemplateField HeaderText="ADMISSION_NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmission_No" runat="server" Text='<%# Bind("ADMISSION_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkbox" Checked="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Registration No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegistration_No" runat="server"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:gridview>
                </td>

            </tr>
        </table>

        
    </div>


</asp:Content>

