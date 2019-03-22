<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Scholorship.aspx.cs" Inherits="administration_Scholorship" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
     <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Style="height: 22px;" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>

                </td>

                <td>Level
                </td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Style="height: 22px; width: 100%;" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Style="height: 22px;" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td id="Td1" style="height: 37px" runat="server">Batch</td>
                <td id="Td2" style="height: 37px" runat="server">
                    <asp:DropDownList ID="ddlBatch" runat="server" Style="height: 22px;" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="lblPKIDU" runat="server" Visible="true"></asp:Label>
                </td>

                <td style="height: 37px">Semester</td>
                <td style="height: 37px">
                    <asp:DropDownList ID="ddlSemester" runat="server" Style="height: 22px;" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>

                </td>

                <%--<td>Section</td>
                <td>
                    <asp:DropDownList ID="ddlSection" runat="server" Style="height: 22px;" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                </td>--%>

                <td>Student Id</td>
                <td>
                    <asp:DropDownList ID="ddlStudentId" AutoPostBack="True" Style="height: 22px;" runat="server" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged"></asp:DropDownList>
                    <asp:TextBox ID="txtRegNo" runat="server" OnTextChanged="txtRegNo_TextChanged" AutoPostBack="true" Height="22px" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtName" runat="server" Width="400px" Visible="false"
                        OnTextChanged="txtName_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>
            <%--   <tr>
           <td>Scholorship Type</td>
           <td>
               <asp:DropDownList id="ddlScholorshipType" runat="server" style="height:22px; font-size:medium" AutoPostBack="True" OnSelectedIndexChanged="ddlScholorshipType_SelectedIndexChanged">
                  
                   <asp:ListItem>Semester Wise</asp:ListItem>
                   <asp:ListItem>Installment Wise</asp:ListItem>
               </asp:DropDownList>
           </td>
       </tr>--%>
            <%--  <tr id="tr_semester" runat="server">
           <td>Semester</td>
           <td>
               <asp:DropDownList id="ddlSemester" runat="server" style="height:22px; font-size:medium" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
           </td>
       </tr>--%>

            <tr>
                <td>Intallment No</td>
                <td>
                    <asp:DropDownList ID="ddlInstallmentNo" runat="server" Style="height: 22px;" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td  style="text-align: center">
                    <asp:Button ID="btnView" runat="server" Style="height: 25px;" Text="View" OnClick="btnView_Click" />
                    &nbsp;
              
                </td>
                
            </tr>

        </table>
      
       
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False"
                        EnableModelValidation="True" ID="gridScholorship" CellPadding="40" ForeColor="#333333" GridLines="None" 
                        OnRowCommand="gridScholorship_RowCommand" OnRowDataBound="gridScholorship_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Bind("PK_ID") %>'></asp:Label>
                                    <asp:Label ID="lblSemester" runat="server" Visible="false" Text='<%# Bind("SEMESTER_ID") %>'></asp:Label>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>

                                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>

                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>

                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Bind("installment_no") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandName="Change" ImageUrl="~/images/icons/edit.png" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
       <br /><br /><br />
          <table style="width: 60%">
            <tr id="tblScholar_dis" runat="server">
                 <td colspan="2">
                     Scholorship/Discount Headings
                 </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 204px">
                  
                    <asp:GridView ID="gridParticulars" runat="server" Width="100%" AutoGenerateColumns="False"
                        EnableModelValidation="True" OnRowDataBound="gridParticulars_RowDataBound" CellPadding="5"
                        ForeColor="#333333" GridLines="None" CellSpacing="0">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Particular">
                                <ItemTemplate>
                                    <asp:Label ID="lblParticularId" runat="server" Text='<%# Bind("PARTICULARS") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblParticular" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fee">
                                <ItemTemplate>
                                    <asp:Label ID="lblFee" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server"  Width="80px" Text='<%# Bind("DISCOUNT") %>' Height="22px"></asp:TextBox>
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                           
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                       <br />
                </td>
            </tr>
              <tr ID="tblDescription" runat="server">

                  <td>
                       Description:
                       <br />
                    <asp:DropDownList ID="ddlStatus" Style="height: 22px;" runat="server">
                        <asp:ListItem Value="Scholarship">Scholarship</asp:ListItem>
                        <asp:ListItem Value="Discount">Discount</asp:ListItem>
                    </asp:DropDownList>
                  </td>

                  <td style="text-align:right">
                      &nbsp;</td>
              </tr>
            <tr id="tbltxtDescription" runat="server">
                <td colspan="2" >

                    <asp:TextBox ID="txtDescription" Style="height: 45px; width: 100%;" runat="server" TextMode="Multiline"></asp:TextBox>
                </td>
            </tr>
        </table>
          <asp:Button ID="btnSave" runat="server" Style="height: 25px;" Text="Save" OnClick="btnSave_Click" />
    </div>
</asp:Content>









