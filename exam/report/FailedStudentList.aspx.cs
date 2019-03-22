using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class exam_report_FailedStudentList : System.Web.UI.Page
{
    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    EXAM_MARKS EMEnt = new EXAM_MARKS();
    EXAM_MARKSService EMSer = new EXAM_MARKSService();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSer = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSrv = new EXAM_TYPE_MASTERService();

    HSS_SUBJECT SEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SSer = new HSS_SUBJECTService();



    Section SecEnt = new Section();
    SectionService SecSer = new SectionService();

    hss_faculty FCTEnt = new hss_faculty();
    hss_facultyService FCTSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    full_pass_marks FPMEnt = new full_pass_marks();
    full_pass_marksService FPMSer = new full_pass_marksService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            loadLevel();
            LoadBatch();

            btnPrint.Visible = false;
        }
    }



    protected void LoadFaculty()
    {
        FCTEnt = new hss_faculty();
        ddlFaculty.DataSource = FCTSer.GetAll(FCTEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
        ddlProgram.Items.Insert(0, "Select");

    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
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
    }


    protected void LoadExamType()
    {
        ddlExamType.DataSource = hf.getExamType(ddlProgram.SelectedValue);
        ddlExamType.DataTextField = "EXAM_TYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();
    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.SEMESTER = ddlSemester.SelectedValue;
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void LoadSemester()
    {
        EntityList theList = new EntityList();
        EntityList semList = new EntityList();
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BTSer.GetAll(BTEnt);
        #region to get the active Semester
        foreach (BatchYear by in theList)
        {
            SMEnt = new semester();
            SMEnt.PK_ID = by.SEMESTER;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                semList.Add(SMEnt);
            }
        }
        #endregion

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void LoadSection()
    {
        SecEnt = new Section();
        ddlSection.DataSource = SecSer.GetAll(SecEnt);
        ddlSection.DataTextField = "SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");
    }

    protected void LoadSubject()
    {

        SUBEnt = new HSS_SUBJECT();
        SUBEnt.PROGRAM = ddlProgram.SelectedValue;
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlSubject.DataSource = SUBSer.GetAll(SUBEnt);
        ddlSubject.DataTextField = "SUBJECT_NAME";
        ddlSubject.DataValueField = "PK_ID";
        ddlSubject.DataBind();
        ddlSubject.Items.Insert(0, "Select");
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadSection();
        LoadSubject();
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadBatch();
        LoadSemester();
        LoadExamType();
    }

    protected void LoadInfo()
    {
        string semester = "";
        string sem = "";
        string bt;
        string[] batch;
        NEnt = new HSS_NAME();
        NEnt = (HSS_NAME)NSer.GetSingle(NEnt);

        if (NEnt != null)
        {
            lblCollegeName.Text = NEnt.NAME;
        }

        lblProgram.Text = ddlProgram.SelectedItem + " " + ddlSemester.SelectedItem.ToString() + semester + sem + " Semester (" + ddlBatch.SelectedValue + " Batch)";
        lblSection.Text = "Section " + "'" + ddlSection.SelectedItem + "'";
        lblSubjectP.Text = ddlSubject.SelectedItem.ToString();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {

        EntityList theList = new EntityList();
        EntityList newList = new EntityList();
        EMEnt = new EXAM_MARKS();
        EMEnt.SEMESTER = ddlSemester.SelectedValue;
        EMEnt.SUBJECT = ddlSubject.SelectedValue;
        EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

        theList = EMSer.GetAll(EMEnt);
        foreach (EXAM_MARKS em in theList)
        {
            if (em.REMARKS != "")
            {
                CSEnt = new HSS_CURRENT_STUDENT();
                CSEnt.STUDENT_ID = em.STUDENT_ID;
                CSEnt.SECTION = ddlSection.SelectedValue;
                CSEnt.YEAR = hf.NepaliYear();
                CSEnt.STATUS = "1";
                CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
                if (CSEnt != null)
                {
                    newList.Add(em);
                }
            }
        }
        gridFailedStudent.DataSource = newList;
        gridFailedStudent.DataBind();

        btnPrint.Visible = true;
        LoadInfo();
    }

    protected void gridFailedStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentID = e.Row.FindControl("lblStudentID") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;
            Label lblRemarks = e.Row.FindControl("lblRemarks") as Label;

            STDEnt = new HSS_STUDENT();
            STDEnt.STUDENT_ID = lblStudentID.Text;
            STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
            if (STDEnt != null)
            {
                lblStudentName.Text = STDEnt.NAME_ENGLISH;
            }

            if (lblRemarks.Text == "F")
            {
                lblRemarks.Text = "Fail";
            }
            else if (lblRemarks.Text == "A")
            {
                lblRemarks.Text = "Absent";
            }
            else if (lblRemarks.Text == "E")
            {
                lblRemarks.Text = "Expel";
            }

        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
}