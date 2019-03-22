using DataHelper.Framework;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forms_studentSectionSelection : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

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

    protected void LoadBatch()
    {
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.PROGRAM = ddlProgram.SelectedValue;
        BEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BSer.GetAll(BEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BSer.GetAll(BEnt);
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
        gridStudentSectionSelection.DataSource = hf.getstudentforsection(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue);
        gridStudentSectionSelection.DataBind();
        if (gridStudentSectionSelection.Rows.Count != 0)
        {
            detail.Visible = true;
        }
        else
        {
            detail.Visible = false;
        }

    }
    protected void gridStudentSectionSelection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlSection = (DropDownList)e.Row.FindControl("ddlSection");
            Label lblSection = (Label)e.Row.FindControl("lblSection");

            SCEnt = new Section();

            ddlSection.DataSource = SCSer.GetAll(SCEnt);
            ddlSection.DataTextField = "SECTION";
            ddlSection.DataValueField = "SECTION";
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, "Select");
            ddlSection.SelectedValue = lblSection.Text;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gr in gridStudentSectionSelection.Rows)
        {
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            DropDownList ddlSection = gr.FindControl("ddlSection") as DropDownList;

            CSEnt = new HSS_CURRENT_STUDENT();
            CSEnt.STUDENT_ID = lblStudentId.Text;
            CSEnt.SEMESTER = ddlSemester.SelectedValue;

            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
            if (CSEnt != null)
            {
                CSEnt.SECTION = ddlSection.SelectedValue;
                CSSer.Update(CSEnt, DT);
            }
        }


        if (DT.HAPPY == true)
        {
            DT.Commit();

            HelperFunction.MsgBox(this, this.GetType(), "Students Section Updated Successfully");
        }
        else
        {
            DT.Abort();

            HelperFunction.MsgBox(this, this.GetType(), "Sorry Student Section Update is not success");
        }
        DT.Dispose();
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
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            LoadSemester();
        
        
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }
}