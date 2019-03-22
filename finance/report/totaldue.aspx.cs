using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using System.Collections;
using Entity.Framework;
using Service.Components;

using DataAccess.Components;
using DataAccess.Framework;
using DataHelper.Framework;

using System.Text;

public partial class finance_report_totaldue : System.Web.UI.Page
{
    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSTEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSTSer = new HSS_CURRENT_STUDENTService();

    program PEnt = new program();
    programService PSer = new programService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    HelperFunction hf = new HelperFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLevel();
            loadFaculty();
            loadProgram();
            loadBatch();
        }
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "PK_ID";
        ddlLevel.DataBind();
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    

    protected void loadFaculty()
    {
        FCEnt = new hss_faculty();
        ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgram();
        
    }

    protected void loadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();

    }

    protected void loadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBatch();
    }

    protected void LoadData()
    {
        gridRemainingBalance.DataSource = hf.getremainingbalofallstudent(ddlBatch.SelectedValue,ddlProgram.SelectedValue);
        gridRemainingBalance.DataBind();
    }





    protected void gridRemainingBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            LinkButton lnkbtnStudentId = e.Row.FindControl("lnkbtnStudentId") as LinkButton;

            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;
            Label lblBatch = e.Row.FindControl("lblBatch") as Label;
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;


            lblAmount.Text = Convert.ToDouble(lblAmount.Text).ToString("#0.00");

            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = lnkbtnStudentId.Text;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            if (STEnt != null)
            {
                lblStudentName.Text = STEnt.NAME_ENGLISH;
            }
            lblBatch.Text = ddlBatch.SelectedValue;

        }
    }
    protected void lnkbtnStudentId_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((LinkButton)sender).Parent.Parent as GridViewRow;
        LinkButton lnkbtnStudentId = (LinkButton)gr.FindControl("lnkbtnStudentId");
        Label lblBatch = (Label)gr.FindControl("lblBatch");
        Response.Redirect("studentpaymenthistory.aspx?studentId=" + lnkbtnStudentId.Text);
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    
}