<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="Exam_FM_PM.aspx.cs" Inherits="exam_internalexam_Exam_FM_PM" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .enlarge:hover {
            transform: scale(5,5);
            -webkit-transform: scale(5,5);
            -moz-transform: scale(5,5);
            -webkit-transition: 0.5s ease-in-out;
            -moz-transition: 0.5s ease-in-out;
            transition: 0.5s ease-in-out;
            transform-origin: 0 0;
            -webkit-transform-origin: 0 0;
            -moz-transform-origin: 0 0;
        }
    </style>
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
            <td>Level</td>
            <td>
                <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>Program</td>
            <td>
                <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Exam Type</td>
            <td>
                <asp:DropDownList ID="ddlExamType" runat="server" Height="22px" AutoPostBack="True" >
                </asp:DropDownList>
            </td>
        </tr>

         <tr>
            <td>Syllabus Year</td>
            <td>
               <%-- <asp:TextBox runat="server" ID="txtYear" OnTextChanged="txtYear_TextChanged" AutoPostBack="True" />--%>
                <asp:DropDownList runat="server" ID="ddlSyllabusYr" AutoPostBack="True" OnSelectedIndexChanged="ddlSyllabusYr_SelectedIndexChanged" >           
                    </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Semester</td>
            <td>
                <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Button Text="View" runat="server" id="btnView" OnClick="btnView_Click"/>
            </td>
        </tr>

    </table>
    <br />

    <table style="width: 100%">
        <tr>
            <td>              
                    <asp:GridView ID="gridExamFMPM" runat="server" Width="100%" OnRowDataBound="gridExamFMPM_RowDataBound"          
                    AutoGenerateColumns="False" CellPadding="6" GridLines="None" AllowPaging="True" CssClass="gridtable"
                     PageSize="20" EnableModelValidation="True"
                    ForeColor="#333333" >


                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="SN.">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Subject">
                            
                            <ItemTemplate>
                                 <asp:Label ID="lblPKID" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false" ></asp:Label>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>' ></asp:Label>
                               
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="FM_Theory">                           
                            <ItemTemplate>
                                 <asp:TextBox runat="server"  ID="txtFMTheory" Height="22px" Width="30%" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PM_Theory">
                            
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtPMTheory"  Height="22px" Width="30%"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="FM_Practical">
                            
                            <ItemTemplate>
                                 <asp:TextBox runat="server"  ID="txtFMPractical" Height="22px" Width="30%"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="PM_Practical">
                            
                            <ItemTemplate>
                                 <asp:TextBox runat="server"  ID="txtPMPractical" Height="22px" Width="30%"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                       
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle HorizontalAlign="Center" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        
                </asp:GridView>

                 <asp:Button ID="btnAdd" runat="server" Text="Add"  Visible="false" OnClick="btnAdd_Click"/>

            </td>
        </tr>
    </table>

    </div>


</asp:Content>
