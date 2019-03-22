<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Exam_type.aspx.cs" Inherits="exam_internalexam_Exam_type" %>

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
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFacultyH" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFacultyH_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Level<br />
                </td>
                <td>

                    <asp:DropDownList ID="ddlLevelH" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevelH_SelectedIndexChanged"></asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgramH" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgramH_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Exam Type
                </td>
                <td>
                    <asp:DropDownList ID="ddlExamTypeH" runat="server" Height="22px" Width="100%" Enabled="true"></asp:DropDownList>
                </td>
            </tr>

            <%--<tr>
                <td>Status
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatusH" runat="server" Height="22px" Width="100%" Enabled="true">
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>

            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                    
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>


        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridExamType" runat="server" OnRowDataBound="gridExamType_RowDataBound" CssClass="gridtable" OnRowCommand="gridExamType_RowCommand"
                        AutoGenerateColumns="False" EnableModelValidation="True">
                        <AlternatingRowStyle BackColor="#FFCCFF" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1%>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exam Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblpkid" runat="server" Text='<%# Bind("PKID") %>' Visible="false" />
                                    <asp:Label Text='<%# Bind("EXAM_TYPE_MASTERID") %>' runat="server" ID="lblExamType" Visible="false" />
                                    <asp:Label Text="" runat="server" ID="lblExmType" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                     
                                    <asp:DropDownList ID="ddlStatus" runat="server" Visible="false">
                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                   <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Bind("STATUS") %>'></asp:Label>
                                    <asp:Label ID="lblSts" runat="server" Visible="true" Text=''></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/images/icons/edit.png" ID="Edit" runat="server" CommandName="change" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            

                        </Columns>
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click" Visible="false"/>
                    </td>
                </tr>
        </table>
    </div>



</asp:Content>