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

public partial class forms_studentSubjectSelection : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    program PEnt = new program();
    programService PSer = new programService();

    SYLLABUS_YEAR SYEnt = new SYLLABUS_YEAR();
    SYLLABUS_YEARService SYSrv = new SYLLABUS_YEARService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();
    DistributedTransaction DT = new DistributedTransaction();
    double totalsubject = 0.0;

    string sub1 = "";
    string sub2 = "";
    string sub3 = "";
    string sub4 = "";
    string sub5 = "";
    string sub6 = "";
    string sub7 = "";

    static string syllabus_year = "";



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadProgram();
            LoadLevel();
            LoadSection();
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
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
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
        BTEnt.SYLLABUS_YEAR = ddlSyllabusYr.SelectedValue;
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
        SCEnt = new Section();
        ddlSection.DataSource = SCSer.GetAll(SCEnt);
        ddlSection.DataTextField = "SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");

    }

    protected void Clear()
    {
        ddlFaculty.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        gridStudentSubjectSelection.DataSource = hf.getstudentforsubject(ddlProgram.SelectedValue, ddlSemester.SelectedValue, ddlBatch.SelectedValue, ddlSection.SelectedValue);
        gridStudentSubjectSelection.DataBind();

        if (gridStudentSubjectSelection.Rows.Count != 0)
        {
            detail.Visible = true;
        }
        else
        {
            //detail.Visible = false;
        }
    }
    protected void gridStudentSubjectSelection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            EntityList theList = new EntityList();
            EntityList aList = new EntityList();
            Label lblCompulsary = (Label)e.Row.FindControl("lblCompulsary");
            Label lblElective = (Label)e.Row.FindControl("lblElective");

            DropDownList ddlSub1 = (DropDownList)e.Row.FindControl("ddlSub1");
            DropDownList ddlSub2 = (DropDownList)e.Row.FindControl("ddlSub2");
            DropDownList ddlSub3 = (DropDownList)e.Row.FindControl("ddlSub3");
            DropDownList ddlSub4 = (DropDownList)e.Row.FindControl("ddlSub4");
            DropDownList ddlSub5 = (DropDownList)e.Row.FindControl("ddlSub5");
            DropDownList ddlSub6 = (DropDownList)e.Row.FindControl("ddlSub6");
            DropDownList ddlSub7 = (DropDownList)e.Row.FindControl("ddlSub7");



            totalsubject = Convert.ToDouble(lblCompulsary.Text) + Convert.ToDouble(lblElective.Text);

            if (totalsubject == 6)
            {
                gridStudentSubjectSelection.Columns[9].Visible = false;
            }
            if (totalsubject == 5)
            {
                gridStudentSubjectSelection.Columns[9].Visible = false;
                gridStudentSubjectSelection.Columns[8].Visible = false;
            }
            if (totalsubject == 4)
            {
                gridStudentSubjectSelection.Columns[9].Visible = false;
                gridStudentSubjectSelection.Columns[8].Visible = false;
                gridStudentSubjectSelection.Columns[7].Visible = false;
            }

            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PROGRAM = ddlProgram.SelectedValue;
            SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            SUBEnt.YEAR = syllabus_year;
            SUBEnt.STATUS = "1";
            theList = SUBSer.GetAll(SUBEnt);
            #region if all subject is compulsary
            if (lblCompulsary.Text == "7")
            {

                ddlSub1.Enabled = false;
                ddlSub2.Enabled = false;
                ddlSub3.Enabled = false;
                ddlSub4.Enabled = false;
                ddlSub5.Enabled = false;
                ddlSub6.Enabled = false;
                ddlSub7.Enabled = false;


                ddlSub1.DataSource = theList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.SelectedIndex = 0;

                ddlSub2.DataSource = theList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.SelectedIndex = 1;

                ddlSub3.DataSource = theList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.SelectedIndex = 2;


                ddlSub4.DataSource = theList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.SelectedIndex = 3;


                ddlSub5.DataSource = theList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.SelectedIndex = 4;


                ddlSub6.DataSource = theList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.SelectedIndex = 5;


                ddlSub7.DataSource = theList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.SelectedIndex = 6;

            }
            #endregion

            #region if 6 subject compulsary and 1 elective
            else if (lblCompulsary.Text == "6")
            {
                ddlSub1.Enabled = false;
                ddlSub2.Enabled = false;
                ddlSub3.Enabled = false;
                ddlSub4.Enabled = false;
                ddlSub5.Enabled = false;
                ddlSub6.Enabled = false;


                ddlSub1.DataSource = theList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.SelectedIndex = 0;

                ddlSub2.DataSource = theList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.SelectedIndex = 1;

                ddlSub3.DataSource = theList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.SelectedIndex = 2;

                ddlSub4.DataSource = theList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.SelectedIndex = 3;

                ddlSub5.DataSource = theList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.SelectedIndex = 4;

                ddlSub6.DataSource = theList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.SelectedIndex = 5;

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.OPT = "E";
                SUBEnt.STATUS = "1";
                aList = SUBSer.GetAll(SUBEnt);

                ddlSub7.DataSource = aList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.Items.Insert(0, "Select");
            }
            #endregion

            #region if 5 subject compulsary and 2 elective
            else if (lblCompulsary.Text == "5")
            {
                ddlSub1.Enabled = false;
                ddlSub2.Enabled = false;
                ddlSub3.Enabled = false;
                ddlSub4.Enabled = false;
                ddlSub5.Enabled = false;
                
                ddlSub1.DataSource = theList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.SelectedIndex = 0;

                ddlSub2.DataSource = theList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.SelectedIndex = 1;

                ddlSub3.DataSource = theList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.SelectedIndex = 2;

                ddlSub4.DataSource = theList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.SelectedIndex = 3;

                ddlSub5.DataSource = theList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.SelectedIndex = 4;

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.OPT = "E";
                SUBEnt.STATUS = "1";
                aList = SUBSer.GetAll(SUBEnt);

                ddlSub6.DataSource = aList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.Items.Insert(0, "Select");

                ddlSub7.DataSource = aList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.Items.Insert(0, "Select");
            }
            #endregion

            #region if 4 subject compulsary and 3 elective
            else if (lblCompulsary.Text == "4")
            {
                ddlSub1.Enabled = false;
                ddlSub2.Enabled = false;
                ddlSub3.Enabled = false;
                ddlSub4.Enabled = false;


                ddlSub1.DataSource = theList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.SelectedIndex = 0;

                ddlSub2.DataSource = theList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.SelectedIndex = 1;

                ddlSub3.DataSource = theList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.SelectedIndex = 2;

                ddlSub4.DataSource = theList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.SelectedIndex = 3;

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.OPT = "E";
                SUBEnt.STATUS = "1";
                aList = SUBSer.GetAll(SUBEnt);

                ddlSub5.DataSource = aList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.Items.Insert(0, "Select");

                ddlSub6.DataSource = aList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.Items.Insert(0, "Select");

                ddlSub7.DataSource = aList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.Items.Insert(0, "Select");
            }
            #endregion

            #region if 3 subject compulsary and 4 elective
            else if (lblCompulsary.Text == "3")
            {
                ddlSub1.Enabled = false;
                ddlSub2.Enabled = false;
                ddlSub3.Enabled = false;


                ddlSub1.DataSource = theList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.SelectedIndex = 0;

                ddlSub2.DataSource = theList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.SelectedIndex = 1;

                ddlSub3.DataSource = theList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.SelectedIndex = 2;

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.OPT = "E";
                SUBEnt.STATUS = "1";
                aList = SUBSer.GetAll(SUBEnt);

                ddlSub4.DataSource = aList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.Items.Insert(0, "Select");

                ddlSub5.DataSource = aList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.Items.Insert(0, "Select");

                ddlSub6.DataSource = aList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.Items.Insert(0, "Select");

                ddlSub7.DataSource = aList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.Items.Insert(0, "Select");
            }
            #endregion

            #region if 2 subject compulsary and 5 elective
            else if (lblCompulsary.Text == "2")
            {
                ddlSub1.Enabled = false;
                ddlSub2.Enabled = false;


                ddlSub1.DataSource = theList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.SelectedIndex = 0;

                ddlSub2.DataSource = theList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.SelectedIndex = 1;

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.OPT = "E";
                SUBEnt.STATUS = "1";
                aList = SUBSer.GetAll(SUBEnt);

                ddlSub3.DataSource = aList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.Items.Insert(0, "Select");

                ddlSub4.DataSource = aList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.Items.Insert(0, "Select");

                ddlSub5.DataSource = aList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.Items.Insert(0, "Select");

                ddlSub6.DataSource = aList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.Items.Insert(0, "Select");

                ddlSub7.DataSource = aList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.Items.Insert(0, "Select");
            }
            #endregion

            #region if 1 subject compulsary and 6 elective
            else if (lblCompulsary.Text == "1")
            {
                ddlSub1.Enabled = false;


                ddlSub1.DataSource = theList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.SelectedIndex = 0;

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.OPT = "E";
                SUBEnt.STATUS = "1";
                aList = SUBSer.GetAll(SUBEnt);

                ddlSub2.DataSource = aList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.Items.Insert(0, "Select");

                ddlSub3.DataSource = aList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.Items.Insert(0, "Select");

                ddlSub4.DataSource = aList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.Items.Insert(0, "Select");

                ddlSub5.DataSource = aList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.Items.Insert(0, "Select");

                ddlSub6.DataSource = aList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.Items.Insert(0, "Select");

                ddlSub7.DataSource = aList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.Items.Insert(0, "Select");
            }
            #endregion

            #region if all subject is elective
            else if (lblElective.Text == "7")
            {

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.OPT = "E";
                SUBEnt.STATUS = "1";
                aList = SUBSer.GetAll(SUBEnt);

                ddlSub1.DataSource = aList;
                ddlSub1.DataTextField = "SUBJECT_NAME";
                ddlSub1.DataValueField = "PK_ID";
                ddlSub1.DataBind();
                ddlSub1.Items.Insert(0, "Select");

                ddlSub2.DataSource = aList;
                ddlSub2.DataTextField = "SUBJECT_NAME";
                ddlSub2.DataValueField = "PK_ID";
                ddlSub2.DataBind();
                ddlSub2.Items.Insert(0, "Select");

                ddlSub3.DataSource = aList;
                ddlSub3.DataTextField = "SUBJECT_NAME";
                ddlSub3.DataValueField = "PK_ID";
                ddlSub3.DataBind();
                ddlSub3.Items.Insert(0, "Select");

                ddlSub4.DataSource = aList;
                ddlSub4.DataTextField = "SUBJECT_NAME";
                ddlSub4.DataValueField = "PK_ID";
                ddlSub4.DataBind();
                ddlSub4.Items.Insert(0, "Select");

                ddlSub5.DataSource = aList;
                ddlSub5.DataTextField = "SUBJECT_NAME";
                ddlSub5.DataValueField = "PK_ID";
                ddlSub5.DataBind();
                ddlSub5.Items.Insert(0, "Select");

                ddlSub6.DataSource = aList;
                ddlSub6.DataTextField = "SUBJECT_NAME";
                ddlSub6.DataValueField = "PK_ID";
                ddlSub6.DataBind();
                ddlSub6.Items.Insert(0, "Select");

                ddlSub7.DataSource = aList;
                ddlSub7.DataTextField = "SUBJECT_NAME";
                ddlSub7.DataValueField = "PK_ID";
                ddlSub7.DataBind();
                ddlSub7.Items.Insert(0, "Select");
            }
            #endregion

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();

        foreach (GridViewRow gr in gridStudentSubjectSelection.Rows)
        {
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblPkId = gr.FindControl("lblPkId") as Label;

            DropDownList ddlSub1 = gr.FindControl("ddlSub1") as DropDownList;
            DropDownList ddlSub2 = gr.FindControl("ddlSub2") as DropDownList;
            DropDownList ddlSub3 = gr.FindControl("ddlSub3") as DropDownList;
            DropDownList ddlSub4 = gr.FindControl("ddlSub4") as DropDownList;
            DropDownList ddlSub5 = gr.FindControl("ddlSub5") as DropDownList;
            DropDownList ddlSub6 = gr.FindControl("ddlSub6") as DropDownList;
            DropDownList ddlSub7 = gr.FindControl("ddlSub7") as DropDownList;

            CSEnt = new HSS_CURRENT_STUDENT();
            CSEnt.STUDENT_ID = lblStudentId.Text;
            CSEnt.SEMESTER = ddlSemester.SelectedValue;
            CSEnt.SECTION = ddlSection.SelectedItem.Text;
            CSEnt.STATUS = "1";

            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
            if (CSEnt != null)
            {
                #region to check an get the selected subject pkid
                if (ddlSub1.SelectedValue == "Select")
                {
                    sub1 = "";
                }
                else if (ddlSub1.SelectedValue != "Select")
                {
                    sub1 = ddlSub1.SelectedValue;
                }

                if (ddlSub2.SelectedValue == "Select")
                {
                    sub2 = "";
                }
                else if (ddlSub2.SelectedValue != "Select")
                {
                    sub2 = ddlSub2.SelectedValue;
                }

                if (ddlSub3.SelectedValue == "Select")
                {
                    sub3 = "";
                }
                else if (ddlSub3.SelectedValue != "Select")
                {
                    sub3 = ddlSub3.SelectedValue;
                }

                if (ddlSub4.SelectedValue == "Select")
                {
                    sub4 = "";
                }
                else if (ddlSub4.SelectedValue != "Select")
                {
                    sub4 = ddlSub4.SelectedValue;
                }

                if (ddlSub5.SelectedValue == "Select")
                {
                    sub5 = "";
                }
                else if (ddlSub5.SelectedValue != "Select")
                {
                    sub5 = ddlSub5.SelectedValue;
                }

                if (ddlSub6.SelectedValue == "Select")
                {
                    sub6 = "";
                }
                else if (ddlSub6.SelectedValue != "Select")
                {
                    sub6 = ddlSub6.SelectedValue;
                }

                if (ddlSub7.SelectedValue == "Select")
                {
                    sub7 = "";
                }
                else if (ddlSub7.SelectedValue != "Select")
                {
                    sub7 = ddlSub7.SelectedValue;
                }
                #endregion


                CSEnt.PK_ID = lblPkId.Text;
                CSEnt.SUBJ1 = sub1;
                CSEnt.SUBJ2 = sub2;
                CSEnt.SUBJ3 = sub3;
                CSEnt.SUBJ4 = sub4;
                CSEnt.SUBJ5 = sub5;
                CSEnt.SUBJ6 = sub6;
                CSEnt.SUBJ7 = sub7;


                CSSer.Update(CSEnt, DT);

            }
        }


        if (DT.HAPPY == true)
        {
            DT.Commit();

            HelperFunction.MsgBox(this, this.GetType(), "Subject Selection is Successfull");
            //gridStudentSubjectSelection.Visible = false;
            //btnSave.Visible = false;


        }
        else
        {
            DT.Abort();

            HelperFunction.MsgBox(this, this.GetType(), "Sorry!!! Something Goes Wrong. Try Again");


        }
        DT.Dispose();


    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
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
        LoadSyllabusYr();

    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }

    protected void ddlSyllabusYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
}