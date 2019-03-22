using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class attendance_reports_Semester_Leave_Record : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    student_leave STLVEnt = new student_leave();
    student_leaveService STLVSer = new student_leaveService();

    scholorship_discount SCDEnt = new scholorship_discount();
    scholorship_discountService SCDSer = new scholorship_discountService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    HelperFunction hf = new HelperFunction();


    static string sub1 = "", sub2 = "", sub3 = "", sub4 = "", sub5 = "", sub6 = "";
    static string sub1name = "", sub2name = "", sub3name = "", sub4name = "", sub5name = "", sub6name = "";
    static int count = 0;
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
        theList = new EntityList();
        EntityList semList = new EntityList();
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        theList = BTCSer.GetAll(BTCEnt);
        #region to get the active Semester
        foreach (BatchYear by in theList)
        {
            SMSEnt = new semester();
            SMSEnt.PK_ID = by.SEMESTER;
            SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
            if (SMSEnt != null)
            {
                semList.Add(SMSEnt);
            }
        }
        #endregion

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        count = 0;
        EntityList theList = new EntityList();

        SUBEnt = new HSS_SUBJECT();
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        SUBEnt.PROGRAM = ddlProgram.SelectedValue;
        SUBEnt.STATUS = "1";
        theList = SUBSer.GetAll(SUBEnt);
        foreach (HSS_SUBJECT sub in theList)
        {
            count++;

            if (count == 1)
            {
                sub1 = sub.PK_ID;
                sub1name = sub.SUBJECT_NAME;
            }
            if (count == 2)
            {
                sub2 = sub.PK_ID;
                sub2name = sub.SUBJECT_NAME;
            }

            if (count == 3)
            {
                sub3 = sub.PK_ID;
                sub3name = sub.SUBJECT_NAME;
            }
            if (count == 4)
            {
                sub4 = sub.PK_ID;
                sub4name = sub.SUBJECT_NAME;
            }
            if (count == 5)
            {
                sub5 = sub.PK_ID;
                sub5name = sub.SUBJECT_NAME;
            }
            if (count == 6)
            {
                sub6 = sub.PK_ID;
                sub6name = sub.SUBJECT_NAME;
            }
        }
        gridSemesterLeaveRecord.DataSource = hf.SemesterLeaveRecord(sub1, sub2, sub3, sub4, sub5, sub6, txtFromdate.Text, txtToDate.Text);
        gridSemesterLeaveRecord.DataBind();
    }
    protected void gridSemesterLeaveRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (count == 4)
        {
            gridSemesterLeaveRecord.Columns[7].Visible = false;
            gridSemesterLeaveRecord.Columns[8].Visible = false;
        }
        if (count == 5)
        {
            gridSemesterLeaveRecord.Columns[7].Visible = true;
            gridSemesterLeaveRecord.Columns[8].Visible = false;
        }
        else if (count > 5)
        {
            gridSemesterLeaveRecord.Columns[7].Visible = true;
            gridSemesterLeaveRecord.Columns[8].Visible = true;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = lblStudentId.Text;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            if (STEnt != null)
            {
                lblStudentName.Text = STEnt.NAME_ENGLISH;
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Label lblSub1Name = e.Row.FindControl("lblSub1Name") as Label;
            Label lblSub2Name = e.Row.FindControl("lblSub2Name") as Label;
            Label lblSub3Name = e.Row.FindControl("lblSub3Name") as Label;
            Label lblSub4Name = e.Row.FindControl("lblSub4Name") as Label;
            Label lblSub5Name = e.Row.FindControl("lblSub5Name") as Label;
            Label lblSub6Name = e.Row.FindControl("lblSub6Name") as Label;

            lblSub1Name.Text = sub1name;
            lblSub2Name.Text = sub2name;
            lblSub3Name.Text = sub3name;
            lblSub4Name.Text = sub4name;
            lblSub5Name.Text = sub5name;
            lblSub6Name.Text = sub6name;

        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            LoadSemester();
        }
        else
        {

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlFaculty.SelectedValue != "Select")
        //{
        LoadProgram();
        //}
        //else
        //{
        //    ddlProgram.Items.Clear();
        //    ddlProgram.Items.Insert(0, "Select");
        //}
        //if (ddlProgram.SelectedValue == "Select")
        //{
        //    ddlBatch.Items.Clear();
        //    ddlBatch.Items.Insert(0, "Select");

        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");
        //}
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();
        }
        //else
        //{
        //    ddlBatch.Items.Clear();
        //    ddlBatch.Items.Insert(0, "Select");

        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");

        //}
        //if (ddlBatch.SelectedValue == "Select")
        //{
        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");
        //}
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
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

            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");


        }
    }
}