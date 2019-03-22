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
using System.IO;

public partial class administration_reports_studentlist : System.Web.UI.Page
{

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSTEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSTSer = new HSS_CURRENT_STUDENTService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    FAMILY_RELATIONS FREnt = new FAMILY_RELATIONS();
    FAMILY_RELATIONSService FRSrv = new FAMILY_RELATIONSService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    ADDRESS AEnt = new ADDRESS();
    ADDRESSService ASrv = new ADDRESSService();

    HelperFunction hf = new HelperFunction();

    string imgfolder;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();

            string program = "";
            program = (Request.QueryString.Get("program"));
            if (program != null)
            {
                PEnt = new program();
                PEnt.PK_ID = program;
                PEnt = (program)PSer.GetSingle(PEnt);
                if (PEnt != null)
                {
                    LoadFaculty();
                    ddlFaculty.SelectedValue = PEnt.FACULTY_ID;

                    PEnt = new program();
                    PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
                    PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
                    ddlProgram.DataSource = PSer.GetAll(PEnt);
                    ddlProgram.DataTextField = "PROGRAM_CODE";
                    ddlProgram.DataValueField = "PK_ID";
                    ddlProgram.DataBind();
                    ddlProgram.SelectedValue = PEnt.PK_ID;

                }

                theList = new EntityList();
                EntityList semList = new EntityList();
                BTEnt = new BatchYear();
                BTEnt.ACTIVE = "1";
                BTEnt.PROGRAM = program;

                theList = BTSer.GetAll(BTEnt);
                #region to get the active Semester
                foreach (BatchYear by in theList)
                {
                    SMEnt = new semester();
                    SMEnt.PK_ID = by.SEMESTER;
                    SMEnt.SYLLABUS_YEAR = by.SYLLABUS_YEAR;
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
                ddlSemester.SelectedValue = SMEnt.SEMESTER_CODE;

                LoadBatch();
                LoadStudentId();

            }
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

    protected void LoadStudentId()
    {
        if (ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")


            ddlStudentId.DataSource = hf.getStudentInfo(ddlBatch.SelectedValue, ddlSemester.SelectedValue);
        ddlStudentId.DataTextField = "NAME";
        ddlStudentId.DataValueField = "STUDENT_ID";
        ddlStudentId.DataBind();
        ddlStudentId.Items.Insert(0, "Select");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        gridStudentDiary.DataSource = getStudentDiary(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "", null);
        gridStudentDiary.DataBind();

    }

    private IDbDataParameter[] CreateParmans(string program, string batch, string semester, string section, string studentid)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_PROGRAM", program));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_BATCH", batch));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_semester", semester));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_SECTION", section));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_STUDENT_ID", studentid));

        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getStudentDiary(string program, string batch, string semester, string section, string studentid)// getDSR->function name
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_SELECT.getstudentdiary", System.Data.CommandType.StoredProcedure, CreateParmans(program, batch, semester, section, studentid));
        }
        catch
        {
        }
        return DT;
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {

            LoadStudentId();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlStudentId.Items.Clear();
            ddlStudentId.Items.Insert(0, "Select");
        }
    }

    protected void ddlStudentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridStudentDiary.DataSource = getStudentDiary(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "", ddlStudentId.SelectedValue);
        gridStudentDiary.DataBind();
    }

    protected void gridStudentDiary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgStudent = e.Row.FindControl("imgStudent") as Image;
            Label lblpkid = e.Row.FindControl("lblpkid") as Label;
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;

            Label lblFName = e.Row.FindControl("lblFName") as Label;
            Label lblFContactNo = e.Row.FindControl("lblFContactNo") as Label;
            Label lblFContactNo1 = e.Row.FindControl("lblFContactNo1") as Label;
            Label lblFContactNo2 = e.Row.FindControl("lblFContactNo2") as Label;

            Label lblMName = e.Row.FindControl("lblMName") as Label;
            Label lblMContactNo = e.Row.FindControl("lblMContactNo") as Label;
            Label lblMContactNo1 = e.Row.FindControl("lblMContactNo1") as Label;
            Label lblMContactNo2 = e.Row.FindControl("lblMContactNo2") as Label;

            Label lblSName = e.Row.FindControl("lblSName") as Label;
            Label lblSContactNo = e.Row.FindControl("lblSContactNo") as Label;
            Label lblSContactNo1 = e.Row.FindControl("lblSContactNo1") as Label;
            Label lblSContactNo2 = e.Row.FindControl("lblSContactNo2") as Label;

            Label lblGName = e.Row.FindControl("lblGName") as Label;
            Label lblGContactNo = e.Row.FindControl("lblGContactNo") as Label;
            Label lblGContactNo1 = e.Row.FindControl("lblGContactNo1") as Label;
            Label lblGContactNo2 = e.Row.FindControl("lblGContactNo2") as Label;
            Label lblGRelation = e.Row.FindControl("lblGRelation") as Label;

            Label lblAddressT = e.Row.FindControl("lblAddressT") as Label;


            if (!string.IsNullOrEmpty(lblStudentId.Text))
            {
                imgfolder = Server.MapPath(@"~/images/bachelorstudent/") + lblStudentId.Text + ".jpg";
                if (File.Exists(imgfolder))
                {
                    imgStudent.ImageUrl = "~/images/bachelorstudent/" + lblStudentId.Text + ".jpg";

                }
                else
                {
                    STEnt = new HSS_STUDENT();
                    STEnt.STUDENT_ID = lblStudentId.Text;
                    STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
                    if (STEnt != null)
                    {

                        if (STEnt.GENDER.Trim() == "M")
                        {
                            imgStudent.ImageUrl = "~/images/bachelorstudent/male.jpg";
                        }
                        if (STEnt.GENDER.Trim() == "F")
                        {
                            imgStudent.ImageUrl = "~/images/bachelorstudent/female.jpeg";
                        }
                    }
                }

                    FREnt = new FAMILY_RELATIONS();
                    FREnt.RELATION_OF_ID = lblpkid.Text;
                    FREnt.RELATION = "Father";
                    FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
                    if (FREnt != null)
                    {
                        lblFName.Text = FREnt.R_NAME;
                        lblFContactNo.Text = FREnt.PHONE;
                        lblFContactNo1.Text = FREnt.MOBILE1;
                        lblFContactNo2.Text = FREnt.MOBILE2;
                    }

                    FREnt = new FAMILY_RELATIONS();
                    FREnt.RELATION_OF_ID = lblpkid.Text;
                    FREnt.RELATION = "Mother";
                    FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
                    if (FREnt != null)
                    {
                        lblMName.Text = FREnt.R_NAME;
                        lblMContactNo.Text = FREnt.PHONE;
                        lblMContactNo1.Text = FREnt.MOBILE1;
                        lblMContactNo2.Text = FREnt.MOBILE2;
                    }

                    FREnt = new FAMILY_RELATIONS();
                    FREnt.RELATION_OF_ID = lblpkid.Text;
                    FREnt.RELATION = "Spouse";
                    FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
                    if (FREnt != null)
                    {
                        lblSName.Text = FREnt.R_NAME;
                        lblSContactNo.Text = FREnt.PHONE;
                        lblSContactNo1.Text = FREnt.MOBILE1;
                        lblSContactNo2.Text = FREnt.MOBILE2;
                    }

                    FREnt = new FAMILY_RELATIONS();
                    FREnt.RELATION_OF_ID = lblpkid.Text;
                    FREnt.IS_GUARDIAN = "TRUE";
                    FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
                    if (FREnt != null)
                    {
                        lblGName.Text = FREnt.R_NAME;
                        lblGRelation.Text = FREnt.RELATION;
                        lblGContactNo.Text = FREnt.PHONE;
                        lblGContactNo1.Text = FREnt.MOBILE1;
                        lblGContactNo2.Text = FREnt.MOBILE2;
                    }

                    AEnt = new ADDRESS();
                    AEnt.ADDRESS_OF_ID = lblpkid.Text;
                    AEnt.ADDRESS_TYPE = "Both";
                    AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
                    if (AEnt != null)
                    {
                        lblAddressT.Text = AEnt.STREET_NAME;
                    }

                    AEnt = new ADDRESS();
                    AEnt.ADDRESS_OF_ID = lblpkid.Text;
                    AEnt.ADDRESS_TYPE = "C";
                    AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
                    if (AEnt != null)
                    {
                        lblAddressT.Text = AEnt.STREET_NAME;
                    }

                }
            }
    }
    protected void gridStudentDiary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("View"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;

            Response.Redirect("Student_Detail.aspx?studentId=" + lblStudentId.Text);
        }


        if (e.CommandName.Equals("Edit"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;

            Response.Redirect("~/forms/Student_info.aspx?studentId=" + lblStudentId.Text);
        }


    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadStudentId();
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

            ddlStudentId.Items.Clear();
            ddlStudentId.Items.Insert(0, "Select");
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

            ddlStudentId.Items.Clear();
            ddlStudentId.Items.Insert(0, "Select");

        }
    }
}