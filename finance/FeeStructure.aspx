<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FeeStructure.aspx.cs" Inherits="finance_FeeStructure" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">

            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Level
                </td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">                       
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr id="Tr1" runat="server" visible="true">
                <td>Batch</td>
                <td>
                    <asp:DropDownList runat="server" Height="22px" ID="ddlBatch" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Semester</td>
                <td>
                    <asp:DropDownList runat="server" Height="22px" ID="ddlSemester" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td>
                    <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click" />
                </td>
            </tr>
            

        </table>

        <table>
            <tr>
                <td>
                    <asp:GridView ID="gridParticulars" CssClass="gridtable" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridParticulars_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Particular">
                                <ItemTemplate>
                                    <asp:Label ID="lblPKID" runat="server" Visible="false" Text=""></asp:Label>
                                    <asp:Label ID="lblParticularId" runat="server" Visible="false" Text='<%#Bind("MAIN_ID") %>'>'></asp:Label>
                                    <asp:Label ID="lblParticularName" runat="server" Text='<%#Bind("PARTICULAR_NAME") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Text="" Height="22px" Style="font-size: 15px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </td>

            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnGenerate" runat="server" Text="Save" OnClick="btnGenerate_Click" Visible="false" /></td>
            </tr>

        </table>
    </div>
</asp:Content>
