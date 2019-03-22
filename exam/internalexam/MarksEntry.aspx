<%@ Page Language="C#" MaintainScrollPositionOnPostback="true"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MarksEntry.aspx.cs" Inherits="exam_internalexam_MarksEntry" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
    <table class="gridtable">
        
        <tr>
            <td>Faculty</td>
            <td>
                <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Level</td>
            <td>
                <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
             <td>Program</td>
            <td>
                <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" runat="server" Height="22px" AutoPostBack="True" >
                </asp:DropDownList>
            </td>
            
        </tr>
         <tr>
            
             <td>Semester</td>
            <td>
                <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            
             <td>Section</td>
            <td>
                <asp:DropDownList ID="ddlsection" runat="server" Height="22px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True"  Visible="false">
                </asp:DropDownList>
            </td>

        </tr>

        <tr>
            <td>Exam Type</td>
            <td>
                <asp:DropDownList ID="ddlExamType" runat="server" Height="22px" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>

        <tr>
            <td>Subject
            </td>
            <td>
                <asp:DropDownList ID="ddlSubject" runat="server" Height="22px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" Style="height: 26px" />
            </td>
        </tr>
    </table>

    <table style="width: 100%">
        <tr>
            <td>

                <asp:GridView ID="grdMarksEntry" runat="server" AutoGenerateColumns="False" 
                    CssClass="gridtable"
                    OnRowDataBound="grdMarksEntry_RowDataBound" CellPadding="5" Visible="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Id">
                            <ItemTemplate>
                                <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Full Marks">
                            <ItemTemplate>
                                <asp:Label ID="lblFullMarks" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pass Marks">
                            <ItemTemplate>
                                <asp:Label ID="lblPassMarks" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Obtain Marks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtObtainMarks" runat="server" Width="40px" Height="22px" MaxLength="5"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                
                                <asp:CheckBox Text="Absent" runat="server" id="chkAbs" Visible="true" AutoPostBack="true" OnCheckedChanged="chkAbs_CheckedChanged" />
                                <asp:CheckBox Text="Expel" runat="server" id="chkExpel" Visible="true" AutoPostBack="true" OnCheckedChanged="chkExpel_CheckedChanged"  />

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

                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Style="height: 26px" />

            </td>
        </tr>
    </table>    
    </div>
</asp:Content>