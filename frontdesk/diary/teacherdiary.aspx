<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="teacherdiary.aspx.cs" Inherits="frontdesk_diary_teacherdiary" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <asp:GridView ID="gridTeacher" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True"
             OnRowDataBound="gridTeacher_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None"
             OnRowCommand="gridTeacher_RowCommand">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="SN">
                    <ItemTemplate>
                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Photo">
                    <ItemTemplate>
                        <asp:Image ID="imgTeacher" runat="server" Height="100px" Width="90px"></asp:Image>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teacher Name">
                    <ItemTemplate>
                        <asp:Label ID="lblTeacherName" runat="server" Text='<%# Bind("FIRSTNAME") %>'></asp:Label>
                        <asp:Label ID="lblEmployeeId" runat="server" Text='<%# Bind("EMPLOYEEID") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact No">
                    <ItemTemplate>
                        <asp:Label ID="lblContact" runat="server" Text='<%# Bind("MOBILE1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="View"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnViewSubject" runat="server" Text="View Subject" CommandName="ViewSubject" ></asp:Button>
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
    </div>
</asp:Content>



