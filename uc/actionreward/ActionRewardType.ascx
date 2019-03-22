<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActionRewardType.ascx.cs" Inherits="uc_test_ActionReward" %>

<table class="gridresult">
    <tr>
        <td>
            
            <asp:Label ID="lblStudentId" runat="server" Text="Student Id"></asp:Label>
            
        </td>
        <td >
            <asp:TextBox ID="txtStudentId" runat="server" AutoPostBack="True" OnTextChanged="txtStudentId_TextChanged"></asp:TextBox>
        </td>
        
    </tr>
    </table>
<br/>
<div id="detaildiv" runat="server" visible="false">
   <table class="gridresult">
    <tr>
        <td>
            <asp:Label ID="lblStudentName" runat="server" Text="Student Name"></asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtStudentName" runat="server"></asp:TextBox>
            <asp:Label ID="lblPKIDU" runat="server" Visible="False"></asp:Label>
        </td>
      
    </tr>
    <tr>
        <td >
            <asp:Label ID="lblProgram" runat="server" Text="Program"></asp:Label>
        </td>
        <td >
            <asp:Label ID="lblPrg" runat="server"></asp:Label>
                -&quot;<asp:Label ID="lblSem" runat="server"></asp:Label>
                &quot;</td>
        
    </tr>
    <tr>
        <td >
            <asp:Label ID="lblActionReward" runat="server" Text="Action/Reward"></asp:Label>
        </td>
        <td >
            <asp:DropDownList ID="ddlActionReward" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActionReward_SelectedIndexChanged">
                <asp:ListItem>-SELECT-</asp:ListItem>
                <asp:ListItem>Action</asp:ListItem>
                <asp:ListItem>Remarks</asp:ListItem>
            </asp:DropDownList>
        </td>
       
         
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Action/Reward Type" style="text-align: left"></asp:Label>
        </td>
         <td style="text-align: left">
            <asp:DropDownList ID="ddlActionRewardType" runat="server" style="text-align: left">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="lblNote" runat="server" Text="Note"></asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Height="60px"></asp:TextBox>
        </td>
       
       
    </tr>
    <tr>
        <td class="auto-style4">
            <asp:Label ID="lblEntrydate" runat="server" Text="Entry Date"></asp:Label>
        </td>
        <td class="auto-style4">
            <asp:TextBox ID="txtDay1" runat="server" Width="30px"></asp:TextBox>
            
            /<asp:TextBox ID="txtMonth1" runat="server" Width="30px"></asp:TextBox>
            /<asp:TextBox ID="txtYear1" runat="server" Width="60px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Label ID="lblDateFormat1" runat="server" Text="[DD / MM / YYYY]"></asp:Label></td>
        
    </tr>
  <%--  <tr id="effectivefrom_row" runat="server">
        <td >
            <asp:Label ID="lblEffectiveFrom" runat="server" Text="Effective From"></asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtDay2" runat="server" Width="30px"></asp:TextBox>
            
            /<asp:TextBox ID="txtMonth2" runat="server" Width="30px"></asp:TextBox>
            /<asp:TextBox ID="txtYear2" runat="server" Width="60px"></asp:TextBox>
        &nbsp;<asp:Label ID="lblDateFormat2" runat="server" Text="[DD / MM / YYYY]"></asp:Label></td>
        
       
    </tr>
    <tr id="effectiveto_row" runat="server">
        <td >
            <asp:Label ID="lblEffectiveTo" runat="server" Text="Effective To"></asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtDay3" runat="server" Width="30px"></asp:TextBox>
            
            /<asp:TextBox ID="txtMonth3" runat="server" Width="30px"></asp:TextBox>
            /<asp:TextBox ID="txtYear3" runat="server" Width="60px"></asp:TextBox>
        &nbsp;<asp:Label ID="lblDateFormat3" runat="server" Text="[DD / MM / YYYY]"></asp:Label></td>
       
       
    </tr>--%>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </td>
       
    </tr>
</table>

<table>
    <tr>
        <td> </td></tr>
            <tr>
        <td>
            <asp:GridView ID="gridActionReward" runat="server" cssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridActionReward_RowDataBound" OnRowCommand="gridActionReward_RowCommand" OnRowDeleting="gridActionReward_RowDeleting">
                <AlternatingRowStyle BackColor="#FFCCFF" />
                <Columns>
                    <asp:TemplateField HeaderText="SN">
                        <ItemTemplate>
                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                             <asp:Label ID="lblPKID" runat="server" Text='<%# bind("PKID") %>' Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Id">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentId" runat="server" Text='<%# bind("STUDENTID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action/Reward">
                        <ItemTemplate>
                            <asp:Label ID="lblActionReward" runat="server" Text='<%# bind("ACTION_REWARD_TYPE1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:Label ID="lblDetail" runat="server" Text='<%# bind("DETAIL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entry Date">
                        <ItemTemplate>
                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# bind("ENTRYDATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Effective To">
                        <ItemTemplate>
                            <asp:Label ID="lblEffectiveTo" runat="server" Text='<%# bind("EFFECTIVETO_DATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                      <asp:TemplateField>
                        
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnEdit" runat="server" CommandName="Change" ImageUrl="~/images/icons/edit.png" />
                            <asp:ImageButton ID="imgBtnDelete" runat="server" CommandName="Delete" ImageUrl="~/images/icons/deletes.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

    </div>


