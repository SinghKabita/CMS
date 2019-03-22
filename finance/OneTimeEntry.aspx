<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OneTimeEntry.aspx.cs" Inherits="finance_OneTimeEntry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
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
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList></td>

            </tr>


            <tr>
                <td>Installment                                                                                                                              
                </td>
                <td>
                    <asp:DropDownList ID="ddlInstallment" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlInstallment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>Month</td>
                <td>
                    <asp:TextBox ID="txtMonth" runat="server" Enabled="false" Height="22px"></asp:TextBox></td>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Enabled="false" Height="22px"></asp:TextBox></td>
            </tr>



        </table>
        <br>
        <table class="gridtable">


            <tr>
                <td>
                    <asp:CheckBoxList ID="chkParticular" runat="server" AppendDataBoundItems="True" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True">
                    </asp:CheckBoxList>

                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                    &nbsp;&nbsp;
               
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gridParticulars" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridParticulars_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Particulars">
                                <ItemTemplate>
                                    <asp:Label ID="lblParticularId" runat="server" Text='<%# Bind("MAIN_ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblParticularName" runat="server" Text='<%# Bind("PARTICULAR_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Text="" Height="22px" Style="font-size: 14px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
            </tr>
        </table>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" visible="false"/>
    </div>
</asp:Content>
