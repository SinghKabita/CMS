using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;
using System.Collections;
using Entity.Framework;
public partial class exam_report_CGPAList : System.Web.UI.Page
{
    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBatch();


        }
    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        EntityList theList = new EntityList();
        string sem = "";
      
    
        if (ddlBatch.SelectedValue != "Select")
        {
            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SMEnt = new semester();
                SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                theList = SMSer.GetAll(SMEnt);
                foreach (semester sm in theList)
                {
                    sem += sm.PK_ID + ",";
                   
                }
                sem = sem.Substring(0, sem.Length - 1);
            }

            if (Convert.ToDouble(ddlBatch.SelectedValue) < 15)
            {
                string[] semList = sem.Split(',');
                
                gridCGPAList.DataSource = hf.getCGPAReportSixth(ddlBatch.SelectedValue, semList[0], semList[1], semList[2], semList[3], semList[4], semList[5]);
                gridCGPAList.DataBind();

                gridCGPAList.Columns[9].Visible = false;
                gridCGPAList.Columns[10].Visible = false;




            }
            else 
            {
                string[] semList = sem.Split(',');
                gridCGPAList.DataSource = hf.getCGPAReportEight(ddlBatch.SelectedValue, semList[0], semList[1], semList[2], semList[3], semList[4], semList[5], semList[6], semList[7]);
                gridCGPAList.DataBind();

                gridCGPAList.Columns[9].Visible = true;
                gridCGPAList.Columns[10].Visible = true;
            }

            lblBatch.Text = ddlBatch.SelectedValue + " Batch";

        }

        if (gridCGPAList.Rows.Count > 0)
        {
            hide.Visible = true;
        }
        else {
            hide.Visible = false;
        }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
    protected void gridCGPAList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            SEnt = new HSS_STUDENT();
            SEnt.STUDENT_ID = lblStudentId.Text;
            SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
            if (SEnt != null)
            {
                lblStudentName.Text = SEnt.NAME_ENGLISH;
            }
        }
    }
}