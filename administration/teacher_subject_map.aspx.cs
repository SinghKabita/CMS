using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;
using Entity.Framework;

public partial class administration_teacher_subject_map : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    TEACHER_SUBJECT_MAPPING TSMEnt = new TEACHER_SUBJECT_MAPPING();
    TEACHER_SUBJECT_MAPPINGService TSMSer = new TEACHER_SUBJECT_MAPPINGService();

    TEACHERPROGRAMMAPPING TPMEnt = new TEACHERPROGRAMMAPPING();
    TEACHERPROGRAMMAPPINGService TPMSrv = new TEACHERPROGRAMMAPPINGService();

    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

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

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();      
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
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
   
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadSubject();
        LoadTeacher();
        LoadData();
        hide.Visible = true;
    }

    protected void LoadData()
    {
        TSMEnt = new TEACHER_SUBJECT_MAPPING();
        TSMEnt.BATCH = ddlBatch.SelectedValue;
        TSMEnt.SEMESTER = ddlSemester.SelectedValue;
        gridMapTable.DataSource = TSMSer.GetAll(TSMEnt);
        gridMapTable.DataBind();
    }

    protected void LoadTeacher()
    {

        theList = new EntityList();
        EntityList semList = new EntityList();

        TPMEnt = new TEACHERPROGRAMMAPPING();
        TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
        theList=TPMSrv.GetAll(TPMEnt);
        foreach (TEACHERPROGRAMMAPPING tpm in theList)
        {
            EMPEnt = new Employees();
            EMPEnt.EMPLOYEEID = tpm.TEACHERID;
            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
            if (EMPEnt != null)
            {
                semList.Add(EMPEnt);
            }
        }
            ddlTeacher.DataSource = semList;
            ddlTeacher.DataTextField = "FULLNAME";
            ddlTeacher.DataValueField = "EMPLOYEEID";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, "Select");

        //ddlTeacher.DataSource = hf.getTeacherInfo();
        //ddlTeacher.DataTextField = "FULL_NAME";
        //ddlTeacher.DataValueField = "EMPLOYEEID";
        //ddlTeacher.DataBind();
        //ddlTeacher.Items.Insert(0, "Select");
    }

    protected void LoadSubject()
    {
        BTEnt = new BatchYear();
        BTEnt.BATCH = ddlBatch.SelectedValue;

        BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
        if (BTEnt != null)
        {
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PROGRAM = ddlProgram.SelectedValue;
            SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
            SUBEnt.STATUS = "1";
            ddlSubject.DataSource = SUBSer.GetAll(SUBEnt);
            ddlSubject.DataTextField = "SUBJECT_NAME";
            ddlSubject.DataValueField="PK_ID";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "Select");
        }
    }


    protected void gridMapTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblSubjectName = e.Row.FindControl("lblSubjectName") as Label;

            Label lblTeacherId = e.Row.FindControl("lblTeacherId") as Label;
            Label lblTeacherName = e.Row.FindControl("lblTeacherName") as Label;


            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PK_ID = lblSubjectId.Text;
            SUBEnt.STATUS = "1";
            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                lblSubjectName.Text = SUBEnt.SUBJECT_NAME;
            }

            EMPEnt = new Employees();
            EMPEnt.EMPLOYEEID = lblTeacherId.Text;
            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
            if (EMPEnt != null)
            {
                lblTeacherName.Text = EMPEnt.FIRSTNAME + " " + EMPEnt.LASTNAME;
            }         
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlTeacher.SelectedValue != "Select" && ddlSubject.SelectedValue != "")
        {
            TSMEnt = new TEACHER_SUBJECT_MAPPING();
            TSMEnt.BATCH = ddlBatch.SelectedValue;
            TSMEnt.SEMESTER = ddlSemester.SelectedValue;
            TSMEnt.SUBJECT_ID = ddlSubject.SelectedValue;
            TSMEnt.TEACHER_ID = ddlTeacher.SelectedValue;
            TSMEnt = (TEACHER_SUBJECT_MAPPING)TSMSer.GetSingle(TSMEnt);
            if (TSMEnt == null)
            {
                TSMEnt = new TEACHER_SUBJECT_MAPPING();
                TSMEnt.BATCH = ddlBatch.SelectedValue;
                TSMEnt.SEMESTER = ddlSemester.SelectedValue;
                TSMEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                TSMEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                TSMSer.Insert(TSMEnt);
                HelperFunction.MsgBox(this, this.GetType(), "Successfull");
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Sorry You Can't Insert Same Data Again.Previously Added");
            }
            LoadData();
        }
        else { 
        
        }

    }

    //protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlBatch.SelectedValue != "Select")
    //    {
    //        LoadSemester();

    //    }
    //    else {
    //        ddlSemester.Items.Clear();
    //        ddlSemester.Items.Insert(0, "Select");
        
    //    }
    //}

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();

    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();
        }
        else
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }
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