<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StudentPaySchedule.aspx.cs" Inherits="finance_StudentPaySchedule" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-20">
        <div class="row">
            <div class="col-md-5 form-group">
                <table class="gridtable" style="width: 100%">
                    <tr>
                        <td style="vertical-align: top; width: 20%">
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
                                    <td>
                                        <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                                    </td>
                                </tr>

                                <tr runat="server" visible="false" id="InstNo">
                                    <td>Installment No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInstallMentNo" Height="22px" runat="server" Text="" Width="50px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false" id="ShowBtn">
                                    <td colspan="2">
                                        <asp:Button ID="btnShowParticular" runat="server" Text="Show Particulars" OnClick="btnShowParticular_Click" />
                                    </td>
                                </tr>
                            </table>

                        </td>

                        <td style="vertical-align: top; width: 60%; padding-right: 20%">
                            <table style="width: 100%" class="gridtable">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gridParticulars" Width="100%" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                            OnRowDataBound="gridParticulars_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SN">
                                                    <HeaderTemplate>
                                                        SN
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particular">
                                                    <HeaderTemplate>
                                                        Particular
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParticularId" runat="server" Visible="false" Text='<%#Bind("MAIN_ID") %>'>'></asp:Label>
                                                        <asp:Label ID="lblParticularName" runat="server" Text='<%#Bind("PARTICULAR_NAME") %>'>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <HeaderTemplate>
                                                        Amount
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" Text="" Height="22px" Style="font-size: 15px" ReadOnly="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                         
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>

                                <tr id="tr_monthselection" runat="server" visible="false">
                                    <td>Starting Month:&nbsp;
                                <asp:DropDownList ID="ddlStartingMonth" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlStartingMonth_SelectedIndexChanged">
                                </asp:DropDownList>

                                        &nbsp;Year:&nbsp;
                                <asp:TextBox ID="txtStartingYear" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtStartingYear_TextChanged">
                                </asp:TextBox>


                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:GridView ID="gridMonthSelect" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None" OnRowDataBound="gridMonthSelect_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Inst No">
                                                    <HeaderTemplate>
                                                        Inst No
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstallmentNo" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Month">
                                                    <HeaderTemplate>
                                                        Month
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" Height="22px" Width="150px" AutoPostBack="True" Enabled="True">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Year">
                                                    <HeaderTemplate>
                                                        Year
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtYear" runat="server" Height="22px" Width="60px" ReadOnly="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Amount">
                                                    <HeaderTemplate>
                                                        Amount
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountPerInstallment" runat="server" Height="22px"></asp:TextBox>
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
                                    </td>
                                </tr>

                                <tr>

                                    <td>
                                        <asp:Button ID="btnGenerate" Text="Generate" runat="server" AutoPostBack="true" OnClick="btnGenerate_Click" Visible="false"></asp:Button>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>

                    <tr>
                         <asp:GridView ID="gridInstallment" runat="server" Width="100%" CssClass="table table-bordered table-hover table-striped"  AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" 
                        ForeColor="#333333" GridLines="None" OnRowDataBound="gridInstallment_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Inst No">
                                <HeaderTemplate>
                                    Inst No
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInstallmentNo" runat="server" Text='<%#Bind("INST_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cum Inst No">
                                <HeaderTemplate>
                                    Cum Inst No
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCumInstNo" runat="server" Text='<%#Bind("CUM_INST_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Month">
                                <HeaderTemplate>
                                    Month
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" runat="server" Text='<%#Bind("INST_MONTH") %>' visible="false"></asp:Label>
                                    <asp:Label ID="lblMonthName" Text="" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Year">
                                <HeaderTemplate>
                                    Year
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Bind("YEAR") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Amount">
                                <HeaderTemplate>
                                    Amount
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Bind("INST_AMOUNT") %>' />
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
                    </tr>
                </table>
            </div>

             <div class="col-md-7">

                <table class="gridtable">
                   
                </table>

            </div>
        </div>
    </div>

</asp:Content>

