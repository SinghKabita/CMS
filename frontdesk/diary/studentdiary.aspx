<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentdiary.aspx.cs" Inherits="frontdesk_diary_studentdiary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">

    <table class="gridtable">
           <tr>
             <td>
                 Faculty
             </td>
             <td>
                <asp:DropDownList ID="ddlFaculty" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
             </td>
             <td>Level
                </td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">                     
                    </asp:DropDownList>
                </td>  
             <td>
                 Program
             </td>
             <td>
                <asp:DropDownList ID="ddlProgram" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
             </td>

               <td>Semester</td>
           <td>
               <asp:DropDownList id="ddlSemester" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
           </td> 
                

                <%--<td>Batch</td>
            <td >
                <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>--%>
               <td id="Td1" runat="server" visible="false">
               Batch
           </td>
           <td id="Td2" runat="server" visible="false">
               <asp:DropDownList id="ddlBatch" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
               <asp:Label ID="lblPKIDU" runat="server" visible="false"></asp:Label>
           </td>

             </tr>
        <tr>

             <td>Student Id</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlStudentId" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
           
            <td><asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View All" /></td>
            <td>&nbsp;</td>
            <td>
               &nbsp; 
            </td>
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
          
        </tr>

    </table>

    <table style="width: 100%">
        <tr>
            <td>

                <asp:GridView ID="gridStudentDiary" runat="server" CssClass="gridtable" AutoGenerateColumns="False" 
                    EnableModelValidation="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" 
                    OnRowDataBound="gridStudentDiary_RowDataBound">
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
                               <asp:Image ID="imgregdno" runat="server" ImageUrl="~/images/icons/regdno.png"></asp:Image>:<asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>' style="font-weight:bold"></asp:Label><br>
                                 <asp:Image ID="imgStName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label><br>
                                 <asp:Image ID="imgStAdd" runat="server" ImageUrl="~/images/icons/address.png"></asp:Image>:<asp:Label ID="lblAddressT" runat="server" Text=""></asp:Label><br>
                                <asp:Image ID="imgStPhone" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("phone") %>'></asp:Label><br>
                              <asp:Image ID="imgStMobile" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblContactNo0" runat="server" Text='<%# Bind("mobile_1") %>'></asp:Label>

                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Father's Info">
                            <ItemTemplate>
                                 <asp:Image ID="imgFName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblFName" runat="server" Text=""></asp:Label><br>
                               <asp:Image ID="imgFPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblFContactNo" runat="server" Text=""></asp:Label><br>
                              <asp:Image ID="imgFMobile" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblFContactNo1" runat="server" Text=""></asp:Label><br>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblFContactNo2" runat="server" Text=""></asp:Label><br>
                              
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mother's Info">
                            <ItemTemplate>
                                 <asp:Image ID="imgMName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblMName" runat="server" Text=""></asp:Label><br>
                               <asp:Image ID="imgMPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblMContactNo" runat="server" Text=""></asp:Label><br>
                                <asp:Image ID="imgMMobile" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblMContactNo1" runat="server" Text=""></asp:Label><br />
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblMContactNo2" runat="server" Text=""></asp:Label>

                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Spouse's Info">
                            <ItemTemplate>
                                <asp:Image ID="imgSName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblSName" runat="server" Text=""></asp:Label><br>
                                <asp:Image ID="imgSPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblSContactNo" runat="server" Text=""></asp:Label><br>
                               <asp:Image ID="imgSMobile" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblSContactNo1" runat="server" Text=""></asp:Label><br />
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblSContactNo2" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Guardian's Info">
                            <ItemTemplate>
                                <asp:Image ID="imgGName" runat="server" ImageUrl="~/images/icons/user.png"></asp:Image>:<asp:Label ID="lblGName" runat="server" Text=""></asp:Label><br>
                                <asp:Image ID="imgGRelation" runat="server" ImageUrl="~/images/icons/relation.png"></asp:Image>:<asp:Label ID="lblGRelation" runat="server" Text=""></asp:Label><br>
                                <asp:Image ID="imgGPhone" runat="server" ImageUrl="~/images/icons/phone.png"></asp:Image>:<asp:Label ID="lblGContactNo" runat="server" Text=""></asp:Label><br>
                                <asp:Image ID="imgGMobile" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblGContactNo1" runat="server" Text=""></asp:Label><br />
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/images/icons/mobile.png"></asp:Image>:<asp:Label ID="lblGContactNo2" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />

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


</asp:Content>

