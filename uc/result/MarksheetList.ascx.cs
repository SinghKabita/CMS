using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Entity.Components;
using Service.Components;

public partial class uc_result_MarksheetList : System.Web.UI.UserControl
{
    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSection();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.SEMESTER=ddlClass.SelectedValue;
        CSEnt.SECTION=ddlSection.SelectedValue;
        CSEnt.YEAR = hf.CurrentYear(hf.NepaliMonth(), hf.NepaliYear());
        grdList.DataSource = CSSer.GetAll(CSEnt);
        grdList.DataBind();
        
    }
    protected void LoadSection()
    {
        SCEnt = new Section();
        ddlSection.DataSource = SCSer.GetAll(SCEnt);
        ddlSection.DataTextField="SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");
    }
    protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblName = e.Row.FindControl("lblName") as Label;

             lblName.Text = hf.getStudentName(lblStudentId.Text);
        }
    }
    protected void btnMarks_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)(((Button)sender).NamingContainer);
        string lblStudentId = ((Label)gr.FindControl("lblStudentId")).Text;
        string url = "marksheetreport.aspx?";
        url += "pclass=" + ddlClass.SelectedValue + "&pstudent=" + lblStudentId + "&psection=" + ddlSection.SelectedValue;

         Response.Redirect(url);
        //Response.Redirect(url);
    }
}
