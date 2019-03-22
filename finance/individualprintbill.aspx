<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="individualprintbill.aspx.cs" Inherits="finance_individualprintbill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="misc_div" runat="server" visible="true" class="container">
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
                 <td style="text-align: left">Batch</td>
                 <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList></td>
               
               

                <td>Student Id</td>
                <td>
                    <asp:DropDownList ID="ddlStudent" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" visible="false" >
                <td>Installment No</td>
                <td>
                    <asp:DropDownList ID="ddlInstallment" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInstallment_SelectedIndexChanged1"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="gridtable">
            <tr>
                <td style="font-size: 16px; font-weight: bold">Other Heading
                </td>
            </tr>

            <tr>
                <td>
                    <asp:GridView ID="gridMisc" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" 
                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" 
                        Style="margin-bottom: 0px" 
                        OnRowCancelingEdit="gridMisc_RowCancelingEdit"
                         OnRowDeleting="gridMisc_RowDeleting"
                        OnRowEditing="gridMisc_RowEditing" 
                        OnRowUpdating="gridMisc_RowUpdating">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <EditItemTemplate>
                                    <asp:Label ID="lblPkId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Particular">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtParticularE" runat="server" Height="22px" Width="200px" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Particular<br>
                                    <asp:TextBox ID="txtParticularH" runat="server" Height="22px" Width="200px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblParticular" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAmountE" runat="server" Height="22px" Width="120px" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Amount<br>
                                    <asp:TextBox ID="txtAmountH" runat="server" Height="22px" Width="120px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnUpdate" runat="server" CommandName="update" ImageUrl="~/images/icons/upload.png" ToolTip="Update" />
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="delete" ImageUrl="~/images/icons/deletes.png" ToolTip="Delete" />
                                    <asp:ImageButton ID="btnCancel" runat="server" CommandName="cancel" ImageUrl="~/images/icons/cancel.png" ToolTip="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    &nbsp;<asp:ImageButton ID="btnEdit" runat="server" CommandName="edit" ImageUrl="~/images/icons/edit.png" ToolTip="Edit" />
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <br>
                                    <asp:Button ID="btnAdd" runat="server" Height="22px" Text="Add" OnClick="btnAdd_Click"></asp:Button>
                                </HeaderTemplate>
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
            <tr>
                <td></td>
            </tr>

            <tr>
                <td>

                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="billgen_div" runat="server" visible="false" class="container">
        <table class="gridtable">
            <tr>
                <td style="text-align: left">Bill Date (BS)</td>
                <td colspan="3">
                    <asp:TextBox ID="txtDay" runat="server" Height="22px" Width="25px"></asp:TextBox>/
               
               
                <asp:TextBox ID="txtMonth" runat="server" Height="22px" Width="25px"></asp:TextBox>
                    /
                  <asp:TextBox ID="txtYear" runat="server" Height="22px" Width="50"></asp:TextBox>
                    &nbsp; <span style="color: #CC0000">(DD/MM/YYYY)</span></td>

            </tr>
            <tr>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty1" runat="server" Height="22px" Style="font-size: 15px" AutoPostBack="True" Enabled="False">
                    </asp:DropDownList></td>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel1" runat="server" Height="22px" Style="font-size: 15px" AutoPostBack="True" Enabled="False">
                    </asp:DropDownList></td>

                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram1" runat="server" Height="22px" Style="font-size: 15px" AutoPostBack="True" Enabled="False">
                    </asp:DropDownList></td>
            </tr>
            <tr>

                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester1" runat="server" Height="22px" Style="font-size: 15px" AutoPostBack="True" Enabled="False">
                    </asp:DropDownList></td>
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch1" runat="server" Height="22px" Style="font-size: 15px" AutoPostBack="True" Enabled="False">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>Student</td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlStudentId" runat="server" Height="22px" Style="font-size: 15px" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>


            <tr>
                <td colspan="4" style="font-weight: bold">Create Bill For Installment</td>

            </tr>
            <tr>
                <td>From Inst.</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlFromInst" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFromInst_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>To Inst.</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlToInst" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlToInst_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="4">For Month &nbsp;&nbsp;
                  <asp:TextBox runat="server" ID="txtForMonth" Height="22px" Enabled="false"></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td colspan="4">
                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                </td>

            </tr>
        </table>
        <table style="width: 50%">
            <tr>
                <td>
                   <asp:GridView ID="gridBillParicular" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False"
                         EnableModelValidation="True" >
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Width="5px" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Semester">
                                <ItemTemplate>

                                    <asp:Label ID="lblSem" runat="server" Width="5px" Text='<%# Bind("SEMESTER_CODE") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Installment No.">
                                <ItemTemplate>

                                    <asp:Label ID="lblInstNo" runat="server" Width="5px" Text='<%# Bind("CUM_INST_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="TOTAL">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("INST_AMOUNT") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnGenerate" Text="Generate" runat="server" OnClick="btnGenerate_Click" /></td>
            </tr>

            <tr id="Tr1" runat="server" visible="false">
                <td>
                    <asp:GridView ID="gridBillDetail" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Particular">
                                <ItemTemplate>

                                    <asp:Label ID="lblParticularId" runat="server" Text='<%# Bind("PARTICULARS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>

                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("DISCOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="tr_smsList" runat="server" visible="false">
                <td>
                    <asp:GridView ID="gridSMSForBill" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True">
                        <Columns>

                            <asp:TemplateField HeaderText="NAME">
                                <ItemTemplate>

                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MONTH">
                                <ItemTemplate>

                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("MONTH") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AMOUNT">
                                <ItemTemplate>

                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PREVIOUS">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrevious" runat="server" Text='<%# Bind("previous_due") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TOTAL">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMSNUMBER">
                                <ItemTemplate>
                                    <asp:Label ID="lblSMSNumber" runat="server" Text='<%# Bind("SMSNUMBER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

