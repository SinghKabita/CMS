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

public partial class administration_reports_StudentList_ungenerated : System.Web.UI.Page
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
            string batch = "";

            program = (Request.QueryString.Get("program"));
            if (program != null)
            {
                string[] splittedTopicNames = program.Split('?');

                program = splittedTopicNames[0];
                batch = splittedTopicNames[1];

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
                loadGrid(program, batch);
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
        //if (ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")


        //    ddlStudentId.DataSource = hf.getStudentInfo(ddlBatch.SelectedValue, ddlSemester.SelectedValue);
        //ddlStudentId.DataTextField = "NAME";
        //ddlStudentId.DataValueField = "STUDENT_ID";
        //ddlStudentId.DataBind();
        //ddlStudentId.Items.Insert(0, "Select");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        theList = new EntityList();
        STEnt = new HSS_STUDENT();
        EntityList newlist = new EntityList();
        STEnt.PROGRAM = ddlProgram.SelectedValue;
        STEnt.BAT_CH = ddlBatch.SelectedValue;
        theList = STSer.GetAll(STEnt);
        foreach (HSS_STUDENT hs in theList)
        {
            if (hs.STUDENT_ID == "")
            {
                newlist.Add(hs);
            }
        }
        gridStudentListUngenerated.DataSource = newlist;
        gridStudentListUngenerated.DataBind();
    }

    protected void loadGrid(string program, string batch)
    {
        //theList = new EntityList();
        //STEnt = new HSS_STUDENT();
        //EntityList newlist = new EntityList();
        //STEnt.PROGRAM = program;
        ////STEnt.BAT_CH = batch;
        //theList = STSer.GetAll(STEnt);
        //foreach (HSS_STUDENT hs in theList)
        //{
        //    if (hs.STUDENT_ID == "")
        //    {
        //        newlist.Add(hs);
        //    }
        //}
        //gridStudentListUngenerated.DataSource = newlist;
        //gridStudentListUngenerated.DataBind();


        theList = new EntityList();
        STEnt = new HSS_STUDENT();
        EntityList newlist = new EntityList();
        STEnt.PROGRAM = program;
        STEnt.BAT_CH = batch;
        theList = STSer.GetAll(STEnt);
        foreach (HSS_STUDENT hs in theList)
        {
            if (hs.STUDENT_ID == "")
            {
                newlist.Add(hs);
            }
        }
        gridStudentListUngenerated.DataSource = newlist;
        gridStudentListUngenerated.DataBind();

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


        }
    }

    protected void ddlStudentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gridStudentDiary.DataSource = getStudentDiary(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "", ddlStudentId.SelectedValue);
        //gridStudentDiary.DataBind();
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


        }
    }

    protected void gridStudentListUngenerated_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgStudent = e.Row.FindControl("imgStudent") as Image;
            Label lblpkid = e.Row.FindControl("lblpkid") as Label;
            Label lblAddressT = e.Row.FindControl("lblAddressT") as Label;

            if (!string.IsNullOrEmpty(lblpkid.Text))
            {
                imgfolder = Server.MapPath(@"~/images/bachelorstudent/") + lblpkid.Text + ".jpg";
                if (File.Exists(imgfolder))
                {
                    imgStudent.ImageUrl = "~/images/bachelorstudent/" + lblpkid.Text + ".jpg";

                }
                else
                {
                    STEnt = new HSS_STUDENT();
                    STEnt.PK_ID = lblpkid.Text;
                    STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
                    if (STEnt != null)
                    {

                        if (STEnt.GENDER.Trim() == "M")
                        {
                            imgStudent.ImageUrl = "~/images/user/male.jpg";
                        }
                        if (STEnt.GENDER.Trim() == "F")
                        {
                            imgStudent.ImageUrl = "~/images/user/female.jpeg";
                        }
                    }

                }

                #region when both address are same
                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = lblpkid.Text;
                AEnt.ADDRESS_TYPE = "Both";
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
                if (AEnt != null)
                {
                    lblAddressT.Text = AEnt.STREET_NAME;

                }
                #endregion

                #region when address type is Contact
                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = lblpkid.Text;
                AEnt.ADDRESS_TYPE = "C";
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
                if (AEnt != null)
                {
                    lblAddressT.Text = AEnt.STREET_NAME;

                }
                #endregion

            }
        }
    }
    protected void gridStudentListUngenerated_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("View"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblpkid = gr.FindControl("lblpkid") as Label;

            Response.Redirect("Ungenerated_stdDetail.aspx?pkId=" + lblpkid.Text);
        }


        if (e.CommandName.Equals("Edit"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblpkid = gr.FindControl("lblpkid") as Label;

            Response.Redirect("~/forms/ungeneratedStd_info.aspx?pkId=" + lblpkid.Text);
        }


    }
}