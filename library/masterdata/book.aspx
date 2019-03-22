<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="book.aspx.cs" Inherits="library_masterdata_book" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <table class="gridtable">
            <tr>
                <td style="height: 24px">Book Type</td>
                <td style="height: 24px">
                    <asp:DropDownList ID="ddlBookType" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBookType_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
        </table>
        <asp:GridView ID="gridBook" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gridBook_RowDataBound"
            OnRowCancelingEdit="gridBook_RowCancelingEdit" OnRowEditing="gridBook_RowEditing" OnRowUpdating="gridBook_RowUpdating"
            OnRowDeleting="gridBook_RowDeleting" CellPadding="4" GridLines="None" AllowPaging="True" OnPageIndexChanging="gridBook_PageIndexChanging"
            PageSize="20" EnableModelValidation="True"
            ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="SN.">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Book Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBookCodeE" Width="50px" Height="22px" runat="server" Text='<%# Bind("BOOKCODE") %>' Enabled="false"></asp:TextBox>
                        <asp:Label ID="lblBookIdU" runat="server" Text='<%# Bind("BOOKID") %>' Visible="False"></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Book Code<br />
                        <asp:TextBox ID="txtBookCodeH" Width="50px" Height="22px" runat="server" Enabled="false"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBookCode" Width="50px" runat="server" Text='<%# Bind("BOOKCODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Book Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBookNameE" runat="server" Height="22px" Text='<%# Bind("BOOKNAME") %>' Width="300px"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Book Name<br />
                        <asp:TextBox ID="txtBookNameH" runat="server" Height="22px" Width="300px"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBookName" runat="server" Text='<%# Bind("BOOKNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Book Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBookCategoryE" runat="server" Width="100px" Height="22px">
                            <asp:ListItem Value="C">Course Book</asp:ListItem>
                            <asp:ListItem Value="R">Reference Book</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblBookCategoryE" runat="server" Text='<%# Bind("BOOKCATEGORY") %>' Visible="false"></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Category<br />
                        <asp:DropDownList ID="ddlBookCategoryH" runat="server" Width="100px" Height="22px">
                            <asp:ListItem Value="C">Course Book</asp:ListItem>
                            <asp:ListItem Value="R">Reference Book</asp:ListItem>
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBookCat" runat="server" Text='<%# Bind("BOOKCATEGORY") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblBookCategory" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Issuable">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlIssuableE" OnSelectedIndexChanged="ddlIssuableE_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="48px" Height="22px">
                            <asp:ListItem Value="1" Text="Yes" />
                            <asp:ListItem Value="0" Text="No" />
                        </asp:DropDownList>
                        <asp:Label Text='<%# Bind("ISSUABLE") %>' ID="lblIssuableE" Visible="false" runat="server" />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Issuable <br />
                        <asp:DropDownList ID="ddlIssuableH" OnSelectedIndexChanged="ddlIssuableH_SelectedIndexChanged" runat="server" Width="48px" Height="22px" AutoPostBack="true">
                            
                            <asp:ListItem Value="1" Text="Yes" />
                            <asp:ListItem Value="0" Text="No" />
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label Text='<%# Bind("ISSUABLE") %>' ID="lblIssuable" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Issue Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlIssueTypeE" runat="server">
                            <asp:ListItem Text="Long-Term" />
                            <asp:ListItem Text="Short-Term" />
                        </asp:DropDownList>
                        <asp:Label Text='<%# Bind("ISSUETYPE") %>' ID="lblIssueTypeE" Visible="false" runat="server" />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Issue Type<br />
                        <asp:DropDownList ID="ddlIssueTypeH" runat="server">
                            <asp:ListItem Text="Long-Term" />
                            <asp:ListItem Text="Short-Term" />
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label Text='<%# Bind("ISSUETYPE") %>' ID="lblIssueType" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Published By">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPublishedByE" runat="server" Text='<%# Bind("PUBLISHEDBY") %>' Width="250px" Height="22px"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Published By<br />
                        <asp:TextBox ID="txtPublishedByH" runat="server" Width="250px" Height="22px"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublishedBy" runat="server" Text='<%# Bind("PUBLISHEDBY") %>' Width="250px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Published Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPublishedDateE" runat="server" Text='<%# Bind("PUBLISHEDDATE") %>' Height="22px"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Published Date<br />
                        <asp:TextBox ID="txtPublishedDateH" runat="server" Height="22px" Width="100px" ></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublishedDate" runat="server" Text='<%# Bind("PUBLISHEDDATE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Author Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAuthorNameE" runat="server" Text='<%# Bind("AUTHORNAME") %>' Height="22px"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        Author Name<br />
                        <asp:TextBox ID="txtAuthorNameH" runat="server" Height="22px"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAuthorName" runat="server" Text='<%# Bind("AUTHORNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:ImageButton ID="btnUpdate" runat="server" CommandName="update" ImageUrl="~/images/icons/upload.png" ToolTip="Update" />
                        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnDelete" runat="server" CommandName="delete" ImageUrl="~/images/icons/deletes.png" ToolTip="Delete" />
                        &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnCancel" runat="server" CommandName="cancel" ImageUrl="~/images/icons/cancel.png" ToolTip="Cancel" />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="edit" Height="16px" />
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="Silver" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

        </asp:GridView>
</asp:Content>