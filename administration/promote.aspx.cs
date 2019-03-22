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
using System.Configuration;

using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Oracle.DataAccess.Client;

public partial class administration_promote : System.Web.UI.Page
{

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    hss_section SCEnt = new hss_section();
    hss_sectionService SCSer = new hss_sectionService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    HelperFunction hf = new HelperFunction();
    DataTable dt = new DataTable();

    double totalsubject = 0.0;
    static double semester = 0;

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

    protected void btnPromote_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();

        foreach (GridViewRow gr in gridPromotion.Rows)
        {
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblStudentName = gr.FindControl("lblStudentName") as Label;
            Label lblSem = gr.FindControl("lblSem") as Label;
            Label lblSection = gr.FindControl("lblSection") as Label;
            Label lblSemester = gr.FindControl("lblSemester") as Label;
            DropDownList ddlSub1 = gr.FindControl("ddlSub1") as DropDownList;
            DropDownList ddlSub2 = gr.FindControl("ddlSub2") as DropDownList;
            DropDownList ddlSub3 = gr.FindControl("ddlSub3") as DropDownList;
            DropDownList ddlSub4 = gr.FindControl("ddlSub4") as DropDownList;
            DropDownList ddlSub5 = gr.FindControl("ddlSub5") as DropDownList;
            DropDownList ddlSub6 = gr.FindControl("ddlSub6") as DropDownList;
            DropDownList ddlSub7 = gr.FindControl("ddlSub7") as DropDownList;


            if ((Convert.ToDouble(lblSemester.Text) + 1) < 9)
            {
                CSEnt = new HSS_CURRENT_STUDENT();
                CSEnt.STUDENT_ID = lblStudentId.Text;
                CSEnt.YEAR = hf.NepaliYear();
                CSEnt.SEMESTER = (Convert.ToDouble(lblSemester.Text) + 1).ToString();
                CSEnt.SECTION = lblSection.Text;
                CSEnt.STATUS = "1";

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

                CSEnt.SUBJ1 = sub1;
                CSEnt.SUBJ2 = sub2;
                CSEnt.SUBJ3 = sub3;
                CSEnt.SUBJ4 = sub4;
                CSEnt.SUBJ5 = sub5;
                CSEnt.SUBJ6 = sub6;
                CSEnt.SUBJ7 = sub7;
                CSEnt.SHIFT = "M";
                CSEnt.BATCH = ddlBatch.SelectedValue;


                CSSer.Insert(CSEnt, DT);

            }
        }

        foreach (GridViewRow gr in gridPromotion.Rows)
        {
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblSemester = gr.FindControl("lblSemester") as Label;


            CSEnt = new HSS_CURRENT_STUDENT();
            CSEnt.STUDENT_ID = lblStudentId.Text;
            CSEnt.SEMESTER = lblSemester.Text;
            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
            if (CSEnt != null)
            {
                if ((Convert.ToDouble(lblSemester.Text)) < 8)
                {
                    CSEnt.STATUS = "0";
                    CSSer.Update(CSEnt, DT);
                }
                else if ((Convert.ToDouble(lblSemester.Text)) == 8)
                {
                    CSEnt.STATUS = "0";
                    CSEnt.REMARKS = "PASSED OUT";
                    CSSer.Update(CSEnt, DT);
                }
            }
        }


        if (DT.HAPPY == true)
        {
            DT.Commit();

            HelperFunction.MsgBox(this, this.GetType(), "Students are Promoted Successfully");

        }
        else
        {
            DT.Abort();

            HelperFunction.MsgBox(this, this.GetType(), "Sorry!!! Student Promotion is not success");


        }
        DT.Dispose();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        gridPromotion.DataSource = getStudentData(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue);
        gridPromotion.DataBind();

        if (gridPromotion.Rows.Count != 0)
        {
            detail.Visible = true;
        }
        else
        {
            detail.Visible = false;

        }
    }

    private IDbDataParameter[] CreateParmans(string program, string batch, string semester)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_program", program));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_BATCH", batch));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_semester", semester));
        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getStudentData(string program, string batch, string semester)// getDSR->function name
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_SELECT.getstudentforpromote", System.Data.CommandType.StoredProcedure, CreateParmans(program, batch, semester));
        }
        catch
        {
        }
        return DT;
    }

    protected void gridPromotion_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            EntityList theList = new EntityList();
            EntityList aList = new EntityList();
            Label lblCompulsary = (Label)e.Row.FindControl("lblCompulsary");
            Label lblElective = (Label)e.Row.FindControl("lblElective");
            Label lblSemester = (Label)e.Row.FindControl("lblSemester");
            Label lblSem = (Label)e.Row.FindControl("lblSem");

            DropDownList ddlSub1 = (DropDownList)e.Row.FindControl("ddlSub1");
            DropDownList ddlSub2 = (DropDownList)e.Row.FindControl("ddlSub2");
            DropDownList ddlSub3 = (DropDownList)e.Row.FindControl("ddlSub3");
            DropDownList ddlSub4 = (DropDownList)e.Row.FindControl("ddlSub4");
            DropDownList ddlSub5 = (DropDownList)e.Row.FindControl("ddlSub5");
            DropDownList ddlSub6 = (DropDownList)e.Row.FindControl("ddlSub6");
            DropDownList ddlSub7 = (DropDownList)e.Row.FindControl("ddlSub7");



            semester = Convert.ToDouble(lblSemester.Text);
            semester = semester + 1;
            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.PK_ID = semester.ToString();
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lblSem.Text = SMEnt.SEMESTER_CODE;
                lblCompulsary.Text = SMEnt.COMPULSARY_SUBJECT;
                lblElective.Text = SMEnt.ELECTIVE_SUBJECT;

                //}

                totalsubject = Convert.ToDouble(lblCompulsary.Text) + Convert.ToDouble(lblElective.Text);

                if (totalsubject == 6)
                {
                    gridPromotion.Columns[10].Visible = false;

                    gridPromotion.Columns[9].Visible = true;
                    gridPromotion.Columns[8].Visible = true;
                    gridPromotion.Columns[7].Visible = true;
                    gridPromotion.Columns[6].Visible = true;
                }
                if (totalsubject == 5)
                {
                    gridPromotion.Columns[10].Visible = false;
                    gridPromotion.Columns[9].Visible = false;

                    gridPromotion.Columns[8].Visible = true;
                    gridPromotion.Columns[7].Visible = true;
                    gridPromotion.Columns[6].Visible = true;
                }
                if (totalsubject == 4)
                {
                    gridPromotion.Columns[10].Visible = false;
                    gridPromotion.Columns[9].Visible = false;
                    gridPromotion.Columns[8].Visible = false;

                    gridPromotion.Columns[7].Visible = true;
                    gridPromotion.Columns[6].Visible = true;
                }

                if (totalsubject == 3)
                {
                    gridPromotion.Columns[10].Visible = false;
                    gridPromotion.Columns[9].Visible = false;
                    gridPromotion.Columns[8].Visible = false;
                    gridPromotion.Columns[7].Visible = false;

                    gridPromotion.Columns[6].Visible = true;
                }
                if (totalsubject == 2)
                {
                    gridPromotion.Columns[10].Visible = false;
                    gridPromotion.Columns[9].Visible = false;
                    gridPromotion.Columns[8].Visible = false;
                    gridPromotion.Columns[7].Visible = false;
                    gridPromotion.Columns[6].Visible = false;
                }

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PROGRAM = ddlProgram.SelectedValue;
                SUBEnt.SEMESTER = semester.ToString();
                SUBEnt.YEAR = syllabus_year;
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
                    SUBEnt.PROGRAM = "1";
                    SUBEnt.SEMESTER = semester.ToString();
                    SUBEnt.OPT = "E";
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
                    SUBEnt.PROGRAM = "1";
                    SUBEnt.SEMESTER = semester.ToString();
                    SUBEnt.OPT = "E";
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
                    SUBEnt.PROGRAM = "1";
                    SUBEnt.SEMESTER = semester.ToString();
                    SUBEnt.OPT = "E";
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
                    SUBEnt.PROGRAM = "1";
                    SUBEnt.SEMESTER = semester.ToString();
                    SUBEnt.OPT = "E";
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
                    SUBEnt.PROGRAM = "1";
                    SUBEnt.SEMESTER = semester.ToString();
                    SUBEnt.OPT = "E";
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
                    SUBEnt.PROGRAM = "1";
                    SUBEnt.SEMESTER = semester.ToString();
                    SUBEnt.OPT = "E";
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
                    SUBEnt.PROGRAM = "1";
                    SUBEnt.SEMESTER = semester.ToString();
                    SUBEnt.OPT = "E";
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

    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLevel();
        LoadProgram();

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
}