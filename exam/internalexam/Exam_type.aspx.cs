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

public partial class exam_internalexam_Exam_type : System.Web.UI.Page
{

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSrv = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSrv = new EXAM_TYPE_MASTERService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HelperFunction mf = new HelperFunction();

    EntityList theList = new EntityList();

    Boolean IsPageRefresh = false;

    DistributedTransaction DT = new DistributedTransaction();

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();

        }
    }

    protected void LoadFaculty()
    {
        FCEnt = new hss_faculty();
        ddlFacultyH.DataSource = FCSer.GetAll(FCEnt);
        ddlFacultyH.DataTextField = "FACULTY";
        ddlFacultyH.DataValueField = "PK_ID";
        ddlFacultyH.DataBind();
        ddlFacultyH.Items.Insert(0, "Select");
        ddlProgramH.Items.Insert(0, "Select");
        ddlExamTypeH.Items.Insert(0, "Select");

    }

    protected void LoadLevel()
    {
        LEnt = new HSS_LEVEL();
        ddlLevelH.DataSource = LSrv.GetAll(LEnt);
        ddlLevelH.DataTextField = "LEVEL_NAME";
        ddlLevelH.DataValueField = "LEVEL_NAME";
        ddlLevelH.DataBind();

    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFacultyH.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevelH.SelectedValue;
        ddlProgramH.DataSource = PSer.GetAll(PEnt);
        ddlProgramH.DataTextField = "PROGRAM_CODE";
        ddlProgramH.DataValueField = "PK_ID";
        ddlProgramH.DataBind();
        ddlProgramH.Items.Insert(0, "Select");

    }

    protected void LoadExamType()
    {
        ETMEnt = new EXAM_TYPE_MASTER();
        ddlExamTypeH.DataSource = ETMSrv.GetAll(ETMEnt);
        ddlExamTypeH.DataTextField = "EXAM_TYPE";
        ddlExamTypeH.DataValueField = "PKID";
        ddlExamTypeH.DataBind();
    }

    protected void LoadSemester()
    {
        //theList = new EntityList();
        //EntityList semList = new EntityList();
        //BEnt = new BatchYear();
        //BEnt.ACTIVE = "1";
        //BEnt.PROGRAM = ddlProgramH.SelectedValue;
        //theList = BSer.GetAll(BEnt);
        //#region to get the active Semester
        //foreach (BatchYear by in theList)
        //{
        //    SMEnt = new semester();
        //    SMEnt.PK_ID = by.SEMESTER;
        //    SMEnt = (semester)SMSer.GetSingle(SMEnt);
        //    if (SMEnt != null)
        //    {
        //        semList.Add(SMEnt);
        //    }
        //}
        //#endregion

        //ddlSemester.DataSource = semList;
        //ddlSemester.DataTextField = "SEMESTER_CODE";
        //ddlSemester.DataValueField = "PK_ID";
        //ddlSemester.DataBind();
        //ddlSemester.Items.Insert(0, "Select");
    }

    private void LoadData()
    {
        ETEnt = new EXAM_TYPE();
        ETEnt.PROGRAM = ddlProgramH.SelectedValue;
        ETEnt.EXAM_TYPE_MASTERID = ddlExamTypeH.SelectedValue;
        //ETEnt.STATUS = "1";
        gridExamType.DataSource = ETSrv.GetAll(ETEnt);

        gridExamType.DataBind();

        btnAdd.Visible = true;
    }


    protected void gridExamType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblpkid = (Label)e.Row.FindControl("lblpkid");
            Label lblExamType = (Label)e.Row.FindControl("lblExamType");
            Label lblExmType = (Label)e.Row.FindControl("lblExmType");
            Label lblSts = (Label)e.Row.FindControl("lblSts");

            DropDownList ddlLevelH = (DropDownList)e.Row.FindControl("ddlLevelH");
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");


            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = lblExamType.Text;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSrv.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                lblExmType.Text = ETMEnt.EXAM_TYPE;
            }

            ETEnt = new EXAM_TYPE();
            ETEnt.PKID = lblpkid.Text;
            ETEnt = (EXAM_TYPE)ETSrv.GetSingle(ETEnt);
            if (ETEnt != null)
            {
                if (ETEnt.STATUS == "1")
                {
                    lblSts.Text = "Available";
                }
                else
                {
                    lblSts.Text = "Not Available";
                }

            }
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {


        if (!IsPageRefresh)
        {
            ETEnt = new EXAM_TYPE();
            ETEnt.PROGRAM = ddlProgramH.SelectedValue;
            ETEnt.EXAM_TYPE_MASTERID = ddlExamTypeH.SelectedValue;
            //ETEnt.STATUS = ddlStatus.SelectedValue;
            ETEnt = (EXAM_TYPE)ETSrv.GetSingle(ETEnt);
            if (ETEnt == null)
            {
                #region insert

                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = ddlProgramH.SelectedValue;
                ETEnt.EXAM_TYPE_MASTERID = ddlExamTypeH.SelectedValue;
                ETEnt.STATUS = "1";
                ETSrv.Insert(ETEnt);
                #endregion

            }
            else
            {
                foreach (GridViewRow gr in gridExamType.Rows)
                {

                    DropDownList ddlStatus = gr.FindControl("ddlStatus") as DropDownList;
                    #region update
                    //ETEnt = new EXAM_TYPE();
                    //ETEnt.PROGRAM = ddlProgramH.SelectedValue;
                    //ETEnt.EXAMTYPE = ddlExamTypeH.SelectedValue;
                    //ETEnt = (EXAM_TYPE)ETSrv.GetSingle(ETEnt);

                    ETEnt.STATUS = ddlStatus.SelectedValue;
                    ETSrv.Update(ETEnt);
                    #endregion
                }
            }
            LoadData();
        }

    }

    protected void ddlFacultyH_SelectedIndexChanged(object sender, EventArgs e)
    {

        //GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;
        //DropDownList ddlProgramH = gr.FindControl("ddlProgramH") as DropDownList;
        //DropDownList ddlFacultyH = gr.FindControl("ddlFacultyH") as DropDownList;
        //PEnt = new program();
        //PEnt.FACULTY_ID = ddlFacultyH.SelectedValue;
        //ddlProgramH.DataSource = PSer.GetAll(PEnt);
        //ddlProgramH.DataTextField = "PROGRAM_CODE";
        //ddlProgramH.DataValueField = "PK_ID";
        //ddlProgramH.DataBind();

        if (ddlFacultyH.SelectedValue != "Select")
        {
            LoadProgram();
        }
        else
        {
            ddlProgramH.Items.Clear();
            ddlProgramH.Items.Insert(0, "Select");

        }

    }

    protected void ddlFacultyE_SelectedIndexChanged(object sender, EventArgs e)
    {

        //GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;
        //DropDownList ddlProgramE = gr.FindControl("ddlProgramE") as DropDownList;
        //DropDownList ddlFacultyE = gr.FindControl("ddlFacultyE") as DropDownList;

        //PEnt = new program();
        //PEnt.FACULTY_ID = ddlFacultyE.SelectedValue;
        //ddlProgramE.DataSource = PSer.GetAll(PEnt);
        //ddlProgramE.DataTextField = "PROGRAM_CODE";
        //ddlProgramE.DataValueField = "PK_ID";
        //ddlProgramE.DataBind();
    }
    protected void ddlLevelH_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgramH_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadExamType();
        LoadSemester();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadData();

    }


    protected void gridExamType_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("change"))
        {

            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;

            Label lblpkid = gr.FindControl("lblpkid") as Label;
            Label lblSts = gr.FindControl("lblSts") as Label;
            DropDownList ddlStatus = gr.FindControl("ddlStatus") as DropDownList;

            ddlStatus.Visible = true;
            lblSts.Visible = false;

            ETEnt = new EXAM_TYPE();
            ETEnt.PKID = lblpkid.Text;
            ETEnt = (EXAM_TYPE)ETSrv.GetSingle(ETEnt);
            if (ETEnt != null)
            {

                ddlStatus.SelectedValue = ETEnt.STATUS;

            }
        }
    }
}