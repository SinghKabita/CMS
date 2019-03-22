using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using System.Collections;

public partial class administration_Semester : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    HelperFunction hf = new HelperFunction();

    semester SEnt = new semester();
    semesterService SSrv = new semesterService();

    Boolean IsPageRefresh = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
        }
    }


    protected void clear()
    {
        ddlFaculty.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        //ddlSemesterCode.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtCompulsarySub.Text = "";
        txtElectiveSub.Text = "";
        txtSyllabusYear.Text = "";
        lblID.Text = "";
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

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void loadGrid()
    {
        SEnt = new semester();
        SEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        SEnt.SEMESTER_CODE = ddlSemester.SelectedItem.ToString();
        SEnt.SYLLABUS_YEAR = txtSyllabusYear.Text;
        gridSemester.DataSource = SSrv.GetAll(SEnt);
        gridSemester.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsPageRefresh)
        {
            SEnt = new semester();
            SEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SEnt.SEMESTER_CODE = ddlSemester.SelectedItem.ToString();
            SEnt.SYLLABUS_YEAR = txtSyllabusYear.Text;
            SEnt = (semester)SSrv.GetSingle(SEnt);

            if (SEnt == null)
            {
                #region to insert

                SEnt = new semester();
                SEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                SEnt.SEMESTER = ddlSemester.SelectedValue;
                SEnt.COMPULSARY_SUBJECT = txtCompulsarySub.Text;
                SEnt.ELECTIVE_SUBJECT = txtElectiveSub.Text;
                SEnt.SYLLABUS_YEAR = txtSyllabusYear.Text;
                SEnt.SEMESTER_CODE = ddlSemester.SelectedItem.ToString();
                SSrv.Insert(SEnt);

                #endregion
            }

            else
            {
                #region to update

                    SEnt.COMPULSARY_SUBJECT = txtCompulsarySub.Text;
                    SEnt.ELECTIVE_SUBJECT = txtElectiveSub.Text;
                   
                    SSrv.Update(SEnt);
                
                #endregion
            }
        }
        loadGrid();
    }

    protected void gridSemester_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblProgramIDG = e.Row.FindControl("lblProgramID") as Label;
            Label lblProgramNameG = e.Row.FindControl("lblProgramNameG") as Label;

            PEnt = new program();
            PEnt.PK_ID = ddlProgram.SelectedValue;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {
                lblProgramNameG.Text = PEnt.PROGRAM_CODE;
            }
        }
    }

    protected void gridSemester_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("change"))
        {

            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;

            Label lblPKG = gr.FindControl("lblPKG") as Label;
            Label lblCompulsarySubjectG = gr.FindControl("lblCompulsarySubjectG") as Label;
            Label lblElectiveSubjectG = gr.FindControl("lblElectiveSubjectG") as Label;
            Label lblSyllabusYearG = gr.FindControl("lblSyllabusYearG") as Label;

            lblID.Text = lblPKG.Text;

            SEnt = new semester();
            SEnt.PK_ID = lblPKG.Text;
            SEnt = (semester)SSrv.GetSingle(SEnt);
            if (SEnt != null)
            {

                ddlProgram.SelectedValue = SEnt.PROGRAM_ID;
                //ddlSemesterCode.SelectedValue = SEnt.SEMESTER_CODE;
                ddlSemester.SelectedValue = SEnt.SEMESTER;
                txtCompulsarySub.Text = SEnt.COMPULSARY_SUBJECT;
                txtElectiveSub.Text = SEnt.ELECTIVE_SUBJECT;
                txtSyllabusYear.Text = SEnt.SYLLABUS_YEAR;
                txtSyllabusYear.Enabled = false;

            }
        }
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        loadGrid();
        txtCompulsarySub.Text = "";
        txtElectiveSub.Text = "";
        txtSyllabusYear.Text = "";
    }

}