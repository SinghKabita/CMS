<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Subject_Detail.aspx.cs" Inherits="human_resource_reports_Subject_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="container">

        <%--  <asp:Label Text="lblEmpID" runat="server" />--%>

        <asp:GridView ID="gridSubject" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True"
             OnRowDataBound="gridSubject_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None"
             OnRowCommand="gridSubject_RowCommand">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="SN">
                    <ItemTemplate>
                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="Program">
                    <ItemTemplate>
                        <asp:Label ID="lblProgram" runat="server" Text=''></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semester">
                    <ItemTemplate>
                        <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("SEMESTER") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblSemesterN" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Batch">
                    <ItemTemplate>
                        <asp:Label ID="lblBatch" runat="server" Text='<%# Bind("BATCH") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubjectID" runat="server" Text='<%# Bind("SUBJECT_ID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblSubjectName" runat="server" Text=''></asp:Label>
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

