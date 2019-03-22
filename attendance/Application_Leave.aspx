<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Application_Leave.aspx.cs" Inherits="attendance_Application_Leave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"></asp:DropDownList>
                </td>       
           <td>
                 Program
             </td>
            <td>
             
                <asp:DropDownList ID="ddlProgram" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
            </td>
             </tr>
       <tr>

           <td>Semester</td>
           <td>
               <asp:DropDownList id="ddlSemester" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
           </td>  

           <td runat="server" visible="false">
               Batch
           </td>
           <td runat="server" visible="false">
               <asp:DropDownList id="ddlBatch" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
               <asp:Label ID="lblPKIDU" runat="server" visible="false"></asp:Label>
           </td>
            <td>Section</td>
           <td>
               <asp:DropDownList id="ddlSection" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
           </td>
               <td>Student Id</td>
           <td>
               <asp:DropDownList id="ddlStudentId" AutoPostBack="True" height="22px" runat="server" OnSelectedIndexChanged="ddlStudentId_SelectedIndexChanged"></asp:DropDownList>
           </td>
           
           
           </tr>
          <tr>       
              
       </tr>

       
        
        <tr>
           <td>Application Date</td>
           <td>
               <asp:TextBox id="txtApplicationDate" style="height:22px; width:100%;" runat="server"></asp:TextBox>
               <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtApplicationDate"
                    Enabled="True" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
           </td>
            <td></td>
             <td></td>
          </tr>
          <tr>
            <td>Leave From Date</td>
           <td>
               <asp:TextBox id="txtLeaveFromDate" style="height:22px; width:100%;" runat="server" AutoPostBack="True" OnTextChanged="txtLeaveFromDate_TextChanged"></asp:TextBox>
               <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLeaveFromDate"
                    Enabled="True" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
           </td>
            
                <td>Leave To Date</td>
           <td>
               <asp:TextBox id="txtLeaveToDate" style="height:22px;" runat="server" AutoPostBack="True" OnTextChanged="txtLeaveToDate_TextChanged"></asp:TextBox>
               <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtLeaveToDate"
                    Enabled="True" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
               &nbsp;
                 <asp:Label ID="lblNoofDays" runat="server"></asp:Label>
           </td>


       </tr>
           <tr>
           <td>Approved By</td>
           <td>
               <asp:TextBox id="txtApprovedBy" style="height:22px; width:100%;" runat="server"></asp:TextBox>
           </td>
               <td></td>
             <td></td>
       </tr>
        <tr>
           <td>Description</td>
           <td colspan="4">
               <asp:TextBox id="txtDescription" style="height:65px; width:100%;" runat="server" Textmode="Multiline"></asp:TextBox>
           </td>
       </tr>
        
       <tr>
           <td>
               <asp:Button ID="btnSave" runat="server" style="height:25px;" Text="Save" OnClick="btnSave_Click" />
           </td>
       </tr>
      
       </table>

    <table style="width:100%">
        <tr>
            <td>
                 <asp:gridview runat="server" width="100%" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" ID="gridStudentLeave" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gridStudentLeave_RowCommand" OnRowDataBound="gridStudentLeave_RowDataBound">
                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                     <Columns>
                         <asp:TemplateField HeaderText="SN">
                             <ItemTemplate>
                                 <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Student Id">
                              <ItemTemplate>
                                   <asp:Label ID="lblPKID" runat="server" visible="false" Text='<%# Bind("PK_ID") %>'></asp:Label>
                                  <asp:Label ID="lblSemester" runat="server" visible="false" Text='<%# Bind("SEMESTER_ID") %>'></asp:Label>
                                 <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Student Name">
                              <ItemTemplate>
                                  
                                 <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                        
                          <asp:TemplateField HeaderText="Application Date">
                             <ItemTemplate>
                                  
                                 <asp:Label ID="lblApplicationDate" runat="server" Text='<%# Bind("application_date") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Leave From Date">
                             <ItemTemplate>
                                  
                                 <asp:Label ID="lblLeaveFromDate" runat="server" Text='<%# Bind("leave_from_date") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>

                            <asp:TemplateField HeaderText="Leave To Date">
                             <ItemTemplate>
                                  
                                 <asp:Label ID="lblLeaveToDate" runat="server" Text='<%# Bind("leave_to_date") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Description">
                             <ItemTemplate>
                                  
                                 <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Approved By">
                             <ItemTemplate>
                                  
                                 <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Bind("approved_by") %>'></asp:Label>
                                  <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Bind("no_of_days") %>' Visible="false"></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:ImageButton ID="imgEdit" runat="server" CommandName="Change" ImageUrl="~/images/icons/edit.png" />
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