<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="bill.aspx.cs" Inherits="finance_bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">

        <div id="fine_div" style="width: 80%" runat="server">
            <table class="gridtable">
                <tr>
                    <td colspan="2">
                        <h3 style="color: black; text-align: left">Fine Calculation</h3>
                    </td>
                </tr>
                <tr>
                    <td>Faculty</td>
                    <td>
                        <asp:dropdownlist id="ddlFaculty" height="22px" runat="server" autopostback="true" onselectedindexchanged="ddlFaculty_SelectedIndexChanged">
                        </asp:dropdownlist>
                    </td>
                </tr>
                <tr>

                    <td>Level</td>
                    <td>
                        <asp:dropdownlist id="ddlLevel" runat="server" style="height: 22px; width: 90px;" autopostback="True" onselectedindexchanged="ddlLevel_SelectedIndexChanged">
                        </asp:dropdownlist>
                    </td>
                </tr>
                <tr>
                    <td>Program</td>
                    <td>
                        <asp:dropdownlist id="ddlProgram" height="22px" runat="server" autopostback="true"
                            onselectedindexchanged="ddlProgram_SelectedIndexChanged">
                        </asp:dropdownlist>
                    </td>
                </tr>
                <tr>
                    <td>Semester
                    </td>
                    <td>
                        <asp:dropdownlist id="ddlSemester" onselectedindexchanged="ddlSemester_SelectedIndexChanged" autopostback="true" runat="server" height="22px"></asp:dropdownlist>
                        &nbsp;
                    <asp:button runat="server" id="btnView" text="View" onclick="btnView_Click" />
                    </td>
                </tr>
                <tr runat="server" visible="false">
                    <td id="Td1" runat="server" visible="true">Batch</td>
                    <td>
                        <asp:dropdownlist runat="server" visible="true" enabled="false" id="ddlBatch1" height="22px" style="margin-left: 0px" autopostback="True" onselectedindexchanged="ddlBatch1_SelectedIndexChanged"></asp:dropdownlist>

                    </td>

                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:gridview id="gridFineCalc" cssclass="gridtable" runat="server" width="100%" autogeneratecolumns="False" 
                            enablemodelvalidation="True" cellpadding="4" forecolor="#333333" gridlines="None" 
                            OnRowDataBound="gridFineCalc_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remaining Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fine">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFine" Height="22px" runat="server"></asp:TextBox>
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
                <tr>
                    <td>
                        <asp:button id="btnSaveContinue" text="Save & Continue" runat="server" onclick="btnSaveContinue_Click" />
                    </td>
                </tr>

            </table>


        </div>

        <div id="misc_div" runat="server" visible="false">
            <table class="gridtable">
                <tr>
                    <td>Faculty</td>
                    <td>
                        <asp:dropdownlist id="ddlFaculty1" height="22px" runat="server" enabled="False"></asp:dropdownlist>
                    </td>
                    <td>Program</td>
                    <td>
                        <asp:dropdownlist id="ddlProgram1" height="22px" runat="server" enabled="False"></asp:dropdownlist>
                    </td>
                </tr>
                <tr>
                    <td>Semester</td>
                    <td>
                        <asp:dropdownlist id="ddlSemester1" height="22px" runat="server" enabled="False"></asp:dropdownlist>
                    </td>
                    <td id="Td2" runat="server" visible="true">Batch</td>
                    <td>
                        <asp:dropdownlist id="ddlBatch2" visible="true" height="22px" runat="server" enabled="False"></asp:dropdownlist>
                    </td>
                </tr>
                <tr>
                    <td>Student</td>
                    <td>
                        <asp:dropdownlist id="ddlStudent" height="22px" runat="server" autopostback="True" onselectedindexchanged="ddlStudent_SelectedIndexChanged"></asp:dropdownlist>

                    </td>
                    <td>Installment No</td>
                    <td>
                        <asp:dropdownlist id="ddlInstallment" height="22px" runat="server" autopostback="True" onselectedindexchanged="ddlInstallment_SelectedIndexChanged"></asp:dropdownlist>
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
                        <asp:gridview id="gridMisc" runat="server" width="100%" autogeneratecolumns="False" cellpadding="4" enablemodelvalidation="True" forecolor="#333333" gridlines="None" style="margin-bottom: 0px"
                            onrowcancelingedit="gridMisc_RowCancelingEdit"
                            onrowdeleting="gridMisc_RowDeleting"
                            onrowediting="gridMisc_RowEditing" onrowupdating="gridMisc_RowUpdating">
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
                        </asp:gridview>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>

                <tr>
                    <td>

                        <asp:button id="btnNext" runat="server" text="Next" onclick="btnNext_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="billgen_div" runat="server" visible="false">
            <table class="gridtable">
                <tr>
                    <td style="text-align: left">Bill Date (BS)</td>
                    <td colspan="3">
                        <asp:textbox id="txtDay" runat="server" height="22px" width="25px"></asp:textbox>
                        /
               
               
                <asp:textbox id="txtMonth" runat="server" height="22px" width="25px"></asp:textbox>
                        /
                  <asp:textbox id="txtYear" runat="server" height="22px" width="50"></asp:textbox>
                        &nbsp; <span style="color: #CC0000">(DD/MM/YYYY)</span></td>

                </tr>
                <tr>
                    <td>Faculty</td>
                    <td>
                        <asp:dropdownlist id="ddlFaculty2" runat="server" height="22px" style="font-size: 15px" autopostback="True" enabled="False">
                        </asp:dropdownlist>
                    </td>
                    <td>Program</td>
                    <td>
                        <asp:dropdownlist id="ddlProgram2" runat="server" height="22px" style="font-size: 15px" autopostback="True" enabled="False">
                        </asp:dropdownlist>
                    </td>
                </tr>
                <tr>

                    <td>Semester</td>
                    <td>
                        <asp:dropdownlist id="ddlSemester2" runat="server" height="22px" style="font-size: 15px" autopostback="True" enabled="False">
                        </asp:dropdownlist>
                    </td>
                    <td id="Td3" runat="server" visible="true">Batch</td>
                    <td>
                        <asp:dropdownlist id="ddlBatch" visible="true" runat="server" height="22px" style="font-size: 15px" autopostback="True" onselectedindexchanged="ddlBatch_SelectedIndexChanged" enabled="False">
                        </asp:dropdownlist>
                    </td>
                </tr>


                <tr>
                    <td colspan="4" style="font-weight: bold">Create Bill For Installment</td>

                </tr>
                <tr>
                    <td>From Inst.</td>
                    <td>
                        <asp:dropdownlist runat="server" id="ddlFromInst" height="22px" autopostback="True" onselectedindexchanged="ddlFromInst_SelectedIndexChanged" enabled="False"></asp:dropdownlist>
                    </td>
                    <td>To Inst.</td>
                    <td>
                        <asp:dropdownlist runat="server" id="ddlToInst" height="22px" autopostback="True" onselectedindexchanged="ddlToInst_SelectedIndexChanged"></asp:dropdownlist>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">For Month &nbsp;&nbsp;
                  <asp:textbox runat="server" id="txtForMonth" height="22px" enabled="false"></asp:textbox>
                    </td>

                </tr>

                <tr>
                    <td colspan="4">
                        <asp:button id="btnCalculate" runat="server" text="Calculate" onclick="btnCalculate_Click" />
                    </td>

                </tr>
            </table>
            <table style="width: 50%">
                <tr>
                    <td>
                        <asp:gridview id="gridBillParicular" runat="server" cssclass="gridtable" width="100%" autogeneratecolumns="False" enablemodelvalidation="True">
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Width="5px" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Installment No.">
                                    <ItemTemplate>

                                        <asp:Label ID="lblInstNo" runat="server" Width="5px" Text='<%# Bind("INST_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="TOTAL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("INST_AMOUNT") %>' ></asp:Label>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>                             
                            </Columns>
                        </asp:gridview>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:button id="btnGenerate" text="Generate" runat="server" onclick="btnGenerate_Click" />
                    </td>
                </tr>

                <tr id="Tr1" runat="server" visible="true">
                    <td>
                        <asp:gridview id="gridBillDetail" runat="server" width="100%" autogeneratecolumns="False" enablemodelvalidation="True" OnRowDataBound="gridBillDetail_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Particular">
                                    <ItemTemplate>

                                        <asp:Label ID="lblParticularId" runat="server" Text='<%# Bind("PARTICULARS") %>' Visible="false"></asp:Label>
                                        <asp:Label Text="" runat="server" ID="lblParticular"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>

                                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>

                                        <asp:Label ID="lbldiscount" runat="server" Text='<%# Bind("DISCOUNT") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:gridview>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:gridview id="gridBarcode" visible="true" autogeneratecolumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label Text='<%# Bind("MBILL_ID") %>' ID="lblMBILL_ID" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:gridview>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>



