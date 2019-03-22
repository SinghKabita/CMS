<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SemesterAttendance.aspx.cs" Inherits="attendance_reports_SemesterAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <table class="gridtable">
       <tr>
             <td>
                 Faculty
             </td>
             <td>
                <asp:DropDownList ID="ddlFaculty" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" ></asp:DropDownList>
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
             </tr>
         <tr>
              <td>Semester</td>
           
             <td> <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList></td>

            <td runat="server" visible="false" >Batch</td>
            <td runat="server" visible="false"> <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" ></asp:DropDownList></td>
       
             <td> <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" /></td>
            
            
             </tr>
      
    </table>
    <table style="width:100%">
        <tr>
            <td><asp:GridView ID="gridSemesterAttendance" CssClass="gridtable" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridSemesterAttendance_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="SN">
                        <ItemTemplate>
                            <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Id">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name">
                         <ItemTemplate>
                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Present">
                         <ItemTemplate>
                            <asp:Label ID="lblTotalPresent" runat="server" Text='<%# Bind("Total_Present") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Absent">
                         <ItemTemplate>
                            <asp:Label ID="lblTotalAbsent" runat="server" Text='<%# Bind("Total_Absent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leave">
                         <ItemTemplate>
                            <asp:Label ID="lblTotalLeave" runat="server" Text='<%# Bind("Total_Leave") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Class">
                         <ItemTemplate>
                            <asp:Label ID="lblTotalDays" runat="server" Text='<%# Bind("TotalDays") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Present %">
                          <ItemTemplate>
                            <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Marks">
                          <ItemTemplate>
                            <asp:Label ID="lblMarks" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView></td>
        </tr>
    </table>
    </div>
</asp:Content>

