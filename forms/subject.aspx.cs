using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forms_subject : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    SYLLABUS_YEAR SYEnt = new SYLLABUS_YEAR();
    SYLLABUS_YEARService SYSrv = new SYLLABUS_YEARService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    EntityList theList = new EntityList();

    HelperFunction hf = new HelperFunction();

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
        ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
        ddlProgram.Items.Insert(0, "Select");
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void LoadLevel()
    {
        LEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LSrv.GetAll(LEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();

    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadSyllabusYr()
    {
        SYEnt = new SYLLABUS_YEAR();
        SYEnt.PROGRAM = ddlProgram.SelectedValue;

        ddlSyllabusYr.DataSource = SYSrv.GetAll(SYEnt);
        ddlSyllabusYr.DataTextField = "YEAR";
        ddlSyllabusYr.DataValueField = "YEAR";
        ddlSyllabusYr.DataBind();
        ddlSyllabusYr.Items.Insert(0, "Select");

    }

    protected void LoadBatch()
    {
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void LoadSemester()
    {
        SMEnt = new semester();
        SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        SMEnt.SYLLABUS_YEAR = ddlSyllabusYr.SelectedValue;
        ddlSemester.DataSource = SMSer.GetAll(SMEnt);
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void LoadGrid()
    {

        SUBEnt = new HSS_SUBJECT();
        SUBEnt.PROGRAM = ddlProgram.SelectedValue;
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        SUBEnt.YEAR = ddlSyllabusYr.SelectedValue;
        gridSubject.DataSource = SUBSer.GetAll(SUBEnt);
        gridSubject.DataBind();

        if (gridSubject.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            a1.Add(SUBEnt);
            gridSubject.DataSource = a1;
            gridSubject.DataBind();
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue != "Select")
        {
            LoadGrid();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Semester");
            gridSubject.DataSource = null;
            gridSubject.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridSubject.HeaderRow;

        TextBox txtSubjectCodeH = (TextBox)row.FindControl("txtSubjectCodeH");
        TextBox txtSubjectNameH = (TextBox)row.FindControl("txtSubjectNameH");
        TextBox txtOrderByH = (TextBox)row.FindControl("txtOrderByH");
        DropDownList ddlSubjectTypeH = (DropDownList)row.FindControl("ddlSubjectTypeH");
        DropDownList ddlRemarksH = (DropDownList)row.FindControl("ddlRemarksH");
        DropDownList ddlStatusH = (DropDownList)row.FindControl("ddlStatusH");
        TextBox txtCreditH = (TextBox)row.FindControl("txtCreditH");
        TextBox txtClassHourH = (TextBox)row.FindControl("txtClassHourH");
        TextBox txtCourseCodeH = (TextBox)row.FindControl("txtCourseCodeH");

        SUBEnt = new HSS_SUBJECT();
        SUBEnt.SUBJECT_CODE = txtSubjectCodeH.Text;
        SUBEnt.SUBJECT_NAME = txtSubjectNameH.Text;
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        SUBEnt.PROGRAM = ddlProgram.SelectedValue;
        SUBEnt.OPT = ddlSubjectTypeH.SelectedValue;
        SUBEnt.REMARKS = ddlRemarksH.SelectedValue;
        SUBEnt.Order_by = txtOrderByH.Text;
        SUBEnt.YEAR = ddlSyllabusYr.SelectedValue;
        SUBEnt.CREDIT = txtCreditH.Text;
        SUBEnt.CLASS_HOUR = txtClassHourH.Text;
        SUBEnt.COURSE_CODE = txtCourseCodeH.Text;
        SUBEnt.STATUS = ddlStatusH.SelectedValue;

        SUBSer.Insert(SUBEnt);
        LoadGrid();

    }
    protected void gridSubject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblSubTyp = e.Row.FindControl("lblSubTyp") as Label;
            Label lblSubjectType = e.Row.FindControl("lblSubjectType") as Label;
            Label lblSubjectRemarks = e.Row.FindControl("lblSubjectRemarks") as Label;
            Label lblSubRem = e.Row.FindControl("lblSubRem") as Label;
            Label lblStatusV = e.Row.FindControl("lblStatusV") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;


            if (lblSubTyp.Text == "C")
            {
                lblSubjectType.Text = "Compulsary";
            }
            else if (lblSubTyp.Text == "E")
            {
                lblSubjectType.Text = "Elective";
            }


            if (lblSubRem.Text == "T")
            {
                lblSubjectRemarks.Text = "Theory";
            }
            else if (lblSubRem.Text == "P")
            {
                lblSubjectRemarks.Text = "Practical";
            }


            if (lblStatus.Text == "1")
            {
                lblStatusV.Text = "Active";
            }
            else
            {
                lblStatusV.Text = "Inactive";
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            DropDownList ddlSubjectTypeE = e.Row.FindControl("ddlSubjectTypeE") as DropDownList;
            DropDownList ddlRemarksE = e.Row.FindControl("ddlRemarksE") as DropDownList;
            DropDownList ddlStatusE = e.Row.FindControl("ddlStatusE") as DropDownList;
            Label lblSubTypE = e.Row.FindControl("lblSubTypE") as Label;
            Label lblSubRemE = e.Row.FindControl("lblSubRemE") as Label;
            Label lblStatusE = e.Row.FindControl("lblStatusE") as Label;

            ddlSubjectTypeE.SelectedValue = lblSubTypE.Text;
            ddlRemarksE.SelectedValue = lblSubRemE.Text;
            ddlStatusE.SelectedValue = lblStatusE.Text;
        }
    }
    protected void gridSubject_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridSubject.EditIndex = e.NewEditIndex;
        LoadGrid();
    }
    protected void gridSubject_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridSubject.EditIndex = -1;
        LoadGrid();
    }
    protected void gridSubject_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridSubject.Rows[e.RowIndex];
        Label lblPKIDE = (Label)row.FindControl("lblPKIDE");
        TextBox txtSubjectCodeE = (TextBox)row.FindControl("txtSubjectCodeE");

        TextBox txtSubjectNameE = (TextBox)row.FindControl("txtSubjectNameE");
        TextBox txtOrderByE = (TextBox)row.FindControl("txtOrderByE");
        DropDownList ddlSubjectTypeE = (DropDownList)row.FindControl("ddlSubjectTypeE");
        DropDownList ddlRemarksE = (DropDownList)row.FindControl("ddlRemarksE");
        DropDownList ddlStatusE = (DropDownList)row.FindControl("ddlStatusE");

        TextBox txtCreditE = (TextBox)row.FindControl("txtCreditE");
        TextBox txtClassHourE = (TextBox)row.FindControl("txtClassHourE");
        TextBox txtCourseCodeE = (TextBox)row.FindControl("txtCourseCodeE");

        SUBEnt = new HSS_SUBJECT();
        SUBEnt.PK_ID = lblPKIDE.Text;
        SUBEnt.SUBJECT_CODE = txtSubjectCodeE.Text;
        SUBEnt.SUBJECT_NAME = txtSubjectNameE.Text;
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        SUBEnt.PROGRAM = ddlProgram.SelectedValue;
        SUBEnt.OPT = ddlSubjectTypeE.SelectedValue;
        SUBEnt.REMARKS = ddlRemarksE.SelectedValue;
        SUBEnt.Order_by = txtOrderByE.Text;
        SUBEnt.YEAR = ddlSyllabusYr.SelectedValue;
        SUBEnt.CREDIT = txtCreditE.Text;
        SUBEnt.CLASS_HOUR = txtClassHourE.Text;
        SUBEnt.COURSE_CODE = txtCourseCodeE.Text;
        SUBEnt.STATUS = ddlStatusE.SelectedValue;

        SUBSer.Update(SUBEnt);
        gridSubject.EditIndex = -1;
        LoadGrid();



    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select")
        {
            LoadProgram();
        }
        else
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, "Select");


        }
        if (ddlProgram.SelectedValue == "Select")
        {


            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {

        LoadSemester();

    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSyllabusYr();


    }

    protected void ddlSyllabusYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
}