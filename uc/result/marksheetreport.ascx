<%@ Control Language="C#" AutoEventWireup="true" CodeFile="marksheetreport.ascx.cs"
    Inherits="uc_NewFolder1_marksheetreport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .auto-style1
    {
        font-size: xx-small;
    }

    .auto-style2
    {
        font-size: 8pt;
    }
</style>
<script language="javascript">
    function printdiv(printpage) {
        var headstr = "<html><head><title></title></head><style type='text/css'>#div_print    {  font-size: 5px !important;  } </style><body>";
        var footstr = "</body>";
        var newstr = document.all.item(printpage).innerHTML;
        var oldstr = document.body.innerHTML;
        document.body.innerHTML = headstr + newstr + footstr;
        window.print();
        document.body.innerHTML = oldstr;
        return false;
    }
</script>


<input name="b_print" type="button" onclick="printdiv('div_print');" value="Print" /><br />
<div id="div_print">
    <table border="0">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: auto; text-align: left;">
                                        <asp:Image ID="imgNccsLogo" runat="server" Height="90px" ImageUrl="~/images/nccshs.jpg" />
                                    </td>
                                    <td colspan="2" style="width: auto; text-align: center">
                                        <asp:Label ID="lblCollegeName" runat="server" Style="font-family: 'Neuropol'; font-weight: 700; font-size: large;"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblAddress" runat="server" Style="font-size: medium"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblPhoneNo" runat="server" Style="font-size: medium"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblEmail" runat="server" Style="font-size: small"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: auto; text-align: right;">
                                        <asp:Image ID="imgISO" runat="server" Height="80px" ImageUrl="~/images/isologo.jpg" />
                                    </td>
                                </tr>
                                <tr>

                                    <td colspan="4" style="text-align: center">
                                        <asp:Label ID="lblMrkSht" runat="server" Text="MARK-SHEET" Style="font-weight: 700; font-size: x-large;"></asp:Label>
                                    </td>

                                </tr>
                            </table>
                        </td>

                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td rowspan="2">
                                <asp:Label ID="lblDt" runat="server" Text="Date: " Visible="False"></asp:Label>
                                <asp:Label ID="lblDate" runat="server" Style="text-align: right" EnableTheming="False"></asp:Label>
                                <br />
                                <asp:Image ID="imgStudent" runat="server" Height="100px" Visible="False" />
                            </td>
                        </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <b>
                                <asp:Label ID="lblRegNoI" runat="server" Text="REG NO: " Visible="False"></asp:Label>
                                <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblStNameI" runat="server" Text="STUDENT NAME: " Visible="False"></asp:Label>
                                <asp:Label ID="lblStName" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblClassI" runat="server" Text="CLASS: " Visible="False"></asp:Label>
                                <asp:Label ID="lblClass" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblSecI" runat="server" Text="SECTION: " Visible="False"></asp:Label>
                                <asp:Label ID="lblSection" runat="server"></asp:Label>
                            </b>&nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" headers="5">
                            <asp:GridView ID="gridMrkSht" runat="server" AutoGenerateColumns="False"
                                EnableModelValidation="True" OnRowDataBound="gridMrkSht_RowDataBound" ShowFooter="True"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOPT" runat="server" Text='<%# bind("OPT") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblOption" runat="server" Style="font-weight: 700; font-size: x-small;"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subjects">
                                        <FooterTemplate>
                                            <b><span>Total</span>
                                                <hr />
                                            </b><span>Percentage</span><b><br />
                                                <hr />
                                            </b><span>Remarks</span><b><br />
                                                <hr />
                                                <!--</b><span >Grade</span><b><br class="auto-style16" />
                                                <hr /> -->
                                            </b><span>Rank </span>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            Subject
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            &nbsp;<asp:Label ID="lblSubjectName" runat="server" Text='<%# BIND("SUBJECT") %>'
                                                Style="font-size: medium; font-weight: 700"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1st Class Test">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotale1" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblPercente1" runat="server"></asp:Label>
                                            <asp:Label ID="lblPrce1" runat="server" Text="%" Visible="False"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblRemarkse1" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <!-- <asp:Label ID="lblGradee1" runat="server"></asp:Label> 
                                            <br />
                                            <hr />-->
                                            <asp:Label ID="lblRanke1" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblFCTH" runat="server" Font-Bold="True" Text="1st Class Test"></asp:Label>
                                            <br />
                                            <hr />
                                            <span class="auto-style1">(Th)FM: </span>
                                            <asp:Label ID="lblFMTHE1" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">PM : </span>
                                            <asp:Label ID="lblPMTHE1" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">(Pr) FM: </span>
                                            <asp:Label ID="lblFMPRE1" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span>&nbsp;<span class="auto-style1">PM : </span></span>
                                            <asp:Label ID="lblPMPRE1" runat="server" CssClass="auto-style1"></asp:Label>
                                        </HeaderTemplate>
                                        <HeaderStyle Font-Size="Small" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFCT" runat="server" Text='<%# BIND("EXTYPE1") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblFCTMark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1st Term Exam">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotale2" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblPercente2" runat="server"></asp:Label>
                                            <asp:Label ID="lblPrce2" runat="server" Text="%" Visible="False"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblRemarkse2" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <!--<asp:Label ID="lblGradee2" runat="server"></asp:Label>
                                            <br />
                                            <hr />-->
                                            <asp:Label ID="lblRanke2" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblFTH" runat="server" Font-Bold="True" Text="First Terminal"></asp:Label>
                                            <br />
                                            <hr />
                                            <span class="auto-style1">(Th)FM: </span>
                                            <asp:Label ID="lblFMTHE2" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMTHE2" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">(Pr) FM: </span>
                                            <asp:Label ID="lblFMPRE2" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMPRE2" runat="server" CssClass="auto-style1"></asp:Label>
                                        </HeaderTemplate>
                                        <HeaderStyle Font-Size="Small" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFT" runat="server" Text='<%# BIND("EXTYPE2") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblFTMark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="2nd Class Test">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotale3" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblPercente3" runat="server"></asp:Label>
                                            <asp:Label ID="lblPrce3" runat="server" Text="%" Visible="False"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblRemarkse3" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <!-- <asp:Label ID="lblGradee3" runat="server"></asp:Label> 
                                            <br />
                                            <hr />-->
                                            <asp:Label ID="lblRanke3" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSCTH" runat="server" Font-Bold="True" Text="2nd Class Test"></asp:Label>
                                            <br />
                                            <hr />
                                            <span class="auto-style2">(Th)FM: </span>
                                            <asp:Label ID="lblFMTHE3" runat="server" CssClass="auto-style2"></asp:Label>
                                            <br>
                                            <span class="auto-style2">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMTHE3" runat="server" CssClass="auto-style2"></asp:Label>
                                            <br>
                                            <span class="auto-style2">(Pr) FM: </span>
                                            <asp:Label ID="lblFMPRE3" runat="server" CssClass="auto-style2"></asp:Label>
                                            <br>
                                            <span class="auto-style2">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMPRE3" runat="server" CssClass="auto-style2"></asp:Label>
                                        </HeaderTemplate>
                                        <HeaderStyle Font-Size="Small" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSCT" runat="server" Text='<%# BIND("EXTYPE3") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblSCTMark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mid Term Exam">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotale4" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblPercente4" runat="server"></asp:Label>
                                            <asp:Label ID="lblPrce4" runat="server" Text="%" Visible="False"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblRemarkse4" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <!-- <asp:Label ID="lblGradee4" runat="server"></asp:Label> 
                                            <br />
                                            <hr />-->
                                            <asp:Label ID="lblRanke4" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblMTH" runat="server" Font-Bold="True" Text="Mid Terminal"></asp:Label>
                                            <br />
                                            <hr />
                                            <span class="auto-style2">(Th)FM: </span>
                                            <asp:Label ID="lblFMTHE4" runat="server" CssClass="auto-style2"></asp:Label>
                                            <br>
                                            <span class="auto-style2">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMTHE4" runat="server" CssClass="auto-style2"></asp:Label>
                                            <br>
                                            <span class="auto-style2">(Pr) FM: </span>
                                            <asp:Label ID="lblFMPRE4" runat="server" CssClass="auto-style2"></asp:Label>
                                            <br>
                                            <span class="auto-style2">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMPRE4" runat="server" CssClass="auto-style2"></asp:Label>
                                        </HeaderTemplate>
                                        <HeaderStyle Font-Size="Small" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMT" runat="server" Text='<%# BIND("EXTYPE4") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblMTMark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Final Exam">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotale5" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblPercente5" runat="server"></asp:Label>
                                            <asp:Label ID="lblPrce5" runat="server" Text="%" Visible="False"></asp:Label>
                                            <br />
                                            <hr />
                                            <asp:Label ID="lblRemarkse5" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                            <!--  <asp:Label ID="lblGradee5" runat="server"></asp:Label> 
                                            <br />
                                            <hr />-->
                                            <asp:Label ID="lblRanke5" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblFNTH" runat="server" Font-Bold="True" Text="Final Terminal"></asp:Label>
                                            <br />
                                            <hr />
                                            <span class="auto-style1">(Th)FM: </span>
                                            <asp:Label ID="lblFMTHE5" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMTHE5" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">(Pr) FM: </span>
                                            <asp:Label ID="lblFMPRE5" runat="server" CssClass="auto-style1"></asp:Label>
                                            <br>
                                            <span class="auto-style1">&nbsp;PM : </span>
                                            <asp:Label ID="lblPMPRE5" runat="server" CssClass="auto-style1"></asp:Label>
                                        </HeaderTemplate>
                                        <HeaderStyle Font-Size="Small" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFNT" runat="server" Font-Size="Small" Text='<%# BIND("EXTYPE5") %>'
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="lblFNTMark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BorderWidth="0px" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="imga1" runat="server" Height="50px" Visible="False" Width="90px" />
                            <br>
                            <asp:Label ID="lblP1" runat="server" Text="________________________" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="imga2" runat="server" Height="50px" Visible="False" Width="90px" />
                            <br>
                            <asp:Label ID="lblP2" runat="server" Text="________________________" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="imga3" runat="server" Height="50px" Visible="False" Width="90px" />
                            <br>
                            <asp:Label ID="lblP3" runat="server" Text="________________________" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="imga4" runat="server" Height="50px" Visible="False" Width="90px" /><br>
                            <asp:Label ID="lblP4" runat="server" Text="________________________" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPost1" runat="server" Visible="False"></asp:Label>
                            <br />
                            <asp:Label ID="lblName1" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPost2" runat="server" Visible="False"></asp:Label>
                            <br />
                            <asp:Label ID="lblName2" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPost3" runat="server" Visible="False"></asp:Label>
                            <br />
                            <asp:Label ID="lblName3" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPost4" runat="server" Visible="False"></asp:Label>
                            <br />
                            <asp:Label ID="lblName4" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
