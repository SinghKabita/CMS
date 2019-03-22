<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MarkSheetLedger.aspx.cs" Inherits="entryforms_result_MarkSheetLedger" Title="Untitled Page" %>

<%@ Register src="../../uc/result/MarksheetList.ascx" tagname="MarksheetList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MarksheetList ID="MarksheetList1" runat="server" />
</asp:Content>

