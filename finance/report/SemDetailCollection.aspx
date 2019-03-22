<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SemDetailCollection.aspx.cs" Inherits="finance_report_SemDetailCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-20">
        <div class="row">
            <div class="col-md-5 form-group">
                <table class="gridtable">
                    <tr>
                        <td>Faculty
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
                            <asp:DropDownList ID="ddlBatch" runat="server" Height="22px">
                            </asp:DropDownList>
                        </td>

                        <td>
                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <table class="gridtable" style="width: 100%">
        <tr>
            <td style="vertical-align: top; width: 20%">

                <asp:GridView ID="gridSemDetailCollection" AlternatingRowStyle-BackColor="#99ccff" AutoGenerateColumns="false" runat="server"
                    OnRowDataBound="gridSemDetailCollection_RowDataBound" >
                    
                    <Columns>
                        <asp:TemplateField HeaderText="SN">
                            <ItemTemplate>
                                <asp:Label Text='<%# Container.DataItemIndex+1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID">
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("STUDENTID") %>' ID="lblStudentID" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lblStudentName" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="1st Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl1stSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl1stSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl1stSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl1stSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="2nd Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl2ndSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl2ndSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl2ndSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl2ndSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="3rd Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl3rdSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl3rdSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl3rdSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl3rdSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="4th Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl4thSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl4thSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl4thSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl4thSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5th Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl5thSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl5thSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl5thSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl5thSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="6th Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl6thSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl6thSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl6thSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl6thSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="7th Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl7thSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl7thSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl7thSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl7thSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="8th Sem Amt">
                            <ItemTemplate>
                                <asp:Label Text="" ID="lbl8thSemPK" Visible="false" runat="server" />
                                <asp:Label Text='' ID="lbl8thSemAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl8thSemDisc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount">
                            <ItemTemplate>
                                <asp:Label Text='' ID="lbl8thSemPaidAmt" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </td>
        </tr>
    </table>


</asp:Content>

