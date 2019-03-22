using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;

public partial class administration_IDCard : System.Web.UI.Page
{

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBatch();
            LoadSemester();

        }

    }

    protected void LoadSemester()
    {
        BEnt = new BatchYear();
        BEnt.BATCH = ddlBatch.SelectedValue;
        BEnt = (BatchYear)BSer.GetSingle(BEnt);
        if (BEnt != null)
        {
            SMEnt = new semester();
            SMEnt.SYLLABUS_YEAR = BEnt.SYLLABUS_YEAR;
          
            ddlSemester.DataSource = SMSer.GetAll(SMEnt);
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");
        }
    }

    protected void LoadBatch()
    {
        BEnt = new BatchYear();
        ddlBatch.DataSource = BSer.GetAll(BEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        EntityList theList = new EntityList();
        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.BATCH = ddlBatch.SelectedValue;
        CSEnt.SEMESTER = ddlSemester.SelectedValue;
        CSEnt.STATUS = "1";
        theList = CSSer.GetAll(CSEnt);

        if (theList.Count > 0)
        {
            rptrAllIdCard.DataSource = theList;
            rptrAllIdCard.DataBind();
        }

    }
    protected void rptrAllIdCard_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblIssueOn = (Label)e.Item.FindControl("lblIssueOn");
        Label lblValidTill = (Label)e.Item.FindControl("lblValidTill");

           Label lblRegdno = (Label)e.Item.FindControl("lblRegdno");
        Label lblName = (Label)e.Item.FindControl("lblName");
        Label lblDOB = (Label)e.Item.FindControl("lblDOB");
        Label lblContact = (Label)e.Item.FindControl("lblContact");
        Image imgStudent = (Image)e.Item.FindControl("imgStudent");


        SEnt = new HSS_STUDENT();
        SEnt.STUDENT_ID = lblRegdno.Text;
        SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
        if (SEnt != null)
        {
            lblName.Text = SEnt.NAME_ENGLISH.ToUpper();
            lblDOB.Text = SEnt.DOB_BS;
            lblContact.Text = SEnt.MOBILE_1;
            imgStudent.ImageUrl= "~/images/bachelorstudent/" + lblRegdno.Text + ".jpg";

        }



        lblIssueOn.Text = txtIssueDate.Text;
        lblValidTill.Text = txtValidTill.Text;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
}