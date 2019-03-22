<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="boardgradesheet_download.aspx.cs" Inherits="class_routine_reports_download_notes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you really Download?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

     <style>
        .enlarge:hover {

transform:scale(5,5);
-webkit-transform:scale(5,5);
 -moz-transform:scale(5,5);

 -webkit-transition: 0.5s ease-in-out;
  -moz-transition: 0.5s ease-in-out;
  transition:0.5s ease-in-out;

transform-origin:0 0;
-webkit-transform-origin:0 0;
-moz-transform-origin:0 0;
}
        </style>
     <table class="gridtable" >
        <tr>
            <td>Batch</td>
            <td><asp:DropDownList id="ddlBatch" runat="server" Height="22px" style="font-size:medium" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList></td>

        </tr>
         <tr>
            <td>Semester</td>
            <td><asp:DropDownList id="ddlSemester" runat="server" Height="22px" style="font-size:medium"></asp:DropDownList></td>

        </tr>
        
         <tr>
            
            <td> <asp:Button ID="btnView" runat="server" Height="22px" Text="View" OnClick="btnView_Click" /></td>
             
             
        </tr>
         </table>
    <table style="width:100%">
        <tr>
            <td style="width:100%">

                <asp:GridView ID="gridList" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" OnRowDataBound="gridList_RowDataBound" CellPadding="4" GridLines="None" AllowPaging="True" OnPageIndexChanging="gridList_PageIndexChanging" ForeColor="#333333">
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
             <asp:TemplateField HeaderText="SN">
                <ItemTemplate>
                    <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                 <itemstyle HorizontalAlign="Center" width="1%" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="img" runat="server" CssClass="enlarge" Width="50px" Height="50px" />
                </ItemTemplate>
                 <itemstyle HorizontalAlign="Center" width="5%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Student Id">
                <ItemTemplate>
                   <%-- <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("File") %>'></asp:Label>--%>
                    <asp:Label ID="lblStudentId" runat="server" Text='<%# bind("STUDENT_ID") %>'></asp:Label>
                </ItemTemplate>
                <itemstyle HorizontalAlign="Center"/>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Student Name">
                  <ItemTemplate>
            
                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>
            <asp:TemplateField HeaderText="Size">
                <ItemTemplate>
                    <%--<asp:Label ID="lblSize" runat="server" Text='<%# Eval("SIZE") %>'></asp:Label>--%>
                     <asp:Label ID="lblSize" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                 <itemstyle HorizontalAlign="Center" width="10%" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:Label ID="lblType" runat="server"></asp:Label>
                    <asp:Label ID="lblLinkName" runat="server" Text='<%# bind("FILE_NAME") %>' Visible="false"></asp:Label>
                  
                </ItemTemplate>
                  <itemstyle HorizontalAlign="Center" width="20%" />
            </asp:TemplateField>
           
           
            <asp:TemplateField>
                <ItemTemplate>
                    <%--<asp:ImageButton ID="Button1" runat="server" CommandName="Download" Text="Download" CommandArgument='<%# Eval("File") %>' ImageUrl="~/images/fileicon/download.png" Width="50px" />--%>
                    <asp:ImageButton ID="btnDownload" runat="server" ToolTip="Download" ImageUrl="~/images/fileicon/download.png" Width="40px" height="40px" onClientClick="Confirm();" OnClick="btnDownload_Click"/>
                </ItemTemplate>
                <itemstyle HorizontalAlign="Center" width="3%" />
            </asp:TemplateField>
        </Columns>
          <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>

            </td>
        </tr>
        </table>
</asp:Content>

