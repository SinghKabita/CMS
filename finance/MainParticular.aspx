<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MainParticular.aspx.cs" Inherits="finance_MainParticular" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <asp:GridView ID="gridMainParticular" runat="server" CssClass="gridtable" AutoGenerateColumns="False"
            EnableModelValidation="True" OnRowDataBound="gridMainParticular_RowDataBound" OnRowCancelingEdit="gridMainParticular_RowCancelingEdit" OnRowEditing="gridMainParticular_RowEditing" OnRowUpdating="gridMainParticular_RowUpdating">
            <AlternatingRowStyle BackColor="#FFCCFF" />
            <Columns>
                <asp:TemplateField HeaderText="Sn">
                    <ItemTemplate>
                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Particular Name">

                    <EditItemTemplate>
                        <asp:Label ID="lblMainIDE" runat="server" Text='<%# Bind("MAIN_ID") %>'  visible="false"/>
                        <asp:TextBox ID="txtParticularNameE" runat="server" Text='<%# Bind("PARTICULAR_NAME") %>' Width="185px"></asp:TextBox>
                    </EditItemTemplate>

                    <HeaderTemplate>
                        <asp:Label ID="lblParticularName" runat="server" Text="Particular Name"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtParticularName" runat="server"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblMainID" runat="server" Text='<%# Bind("MAIN_ID") %>'  visible="false"/>--%>
                        <asp:Label ID="lblParticular" runat="server" Text='<%# Bind("PARTICULAR_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="One Time">

                   

                    <HeaderTemplate>
                        <asp:Label ID="lblOneTime" runat="server" Text="One Time"></asp:Label>
                        <br />
                        <asp:CheckBox ID="chkOneTime" runat="server" />
                    </HeaderTemplate>
                    
                    <ItemTemplate>
                        <asp:CheckBox ID="chkOneTimeI" runat="server" Enabled="false" />
                        <asp:Label ID="lblOneTime" runat="server" Text='<%# Bind("onetime") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/icons/upload.png" CommandName="Update" />
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/icons/delete.gif" CommandName="Cancel" />
                    </EditItemTemplate>

                    <HeaderTemplate>
                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="Edit" />
                    </ItemTemplate>

                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
