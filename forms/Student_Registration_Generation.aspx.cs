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
using System.Data;
using BarcodeLib;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class forms_Student_Registration_generation : System.Web.UI.Page
{

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSrv = new HSS_STUDENTService();

    HSS_EDUCATION_DETAIL EDEnt = new HSS_EDUCATION_DETAIL();
    HSS_EDUCATION_DETAILService EDSrv = new HSS_EDUCATION_DETAILService();

    HSS_ATTACHMENTS AEnt = new HSS_ATTACHMENTS();
    HSS_ATTACHMENTSService ASrv = new HSS_ATTACHMENTSService();

    information_alert IEEnt = new information_alert();
    information_alertService IESrv = new information_alertService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSrv = new HSS_CURRENT_STUDENTService();

    OFFICE OfcEnt = new OFFICE();
    officeService OfcSrv = new officeService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    HelperFunction hf = new HelperFunction();

    DistributedTransaction DT = new DistributedTransaction();

    EntityList theList = new EntityList();

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

    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
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
        BTEnt.BATCH = ddlBatch.SelectedValue;
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
        if (BTEnt != null)
        {
            SMEnt = new semester();
            SMEnt.PK_ID = BTEnt.SEMESTER;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                semList.Add(SMEnt);
            }
        }

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();

    }

    protected void LoadData()
    {
        theList = new EntityList();
        EntityList newList = new EntityList();
        STDEnt = new HSS_STUDENT();
        STDEnt.PROGRAM = ddlProgram.SelectedValue;
        STDEnt.BAT_CH = ddlBatch.SelectedValue;
        theList = STDSrv.GetAll(STDEnt);
        foreach (HSS_STUDENT hs in theList)
        {
            if (hs.STUDENT_ID == "")
                newList.Add(hs);
        }
        gridStudentTable.DataSource = newList;
        gridStudentTable.DataBind();

    }

    protected void clear()
    {

        foreach (GridViewRow gr in gridStudentTable.Rows)
        {

            Label lblRegistration_No = gr.FindControl("lblRegistration_No") as Label;
            lblRegistration_No.Text = "";
        }

        ddlFaculty.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;

        ddlBatch.SelectedIndex = 0;
        txtRegno.Text = "";


    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
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


        }
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadBatch();
            //LoadSemester();

        }
        //else
        //{
        //    ddlBatch.Items.Clear();
        //    ddlBatch.Items.Insert(0, "Select");

        //}
    }

    protected void gridStudentTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblRegistration_No = e.Row.FindControl("lblRegistration_No") as Label;
            CheckBox chkbox = e.Row.FindControl("chkbox") as CheckBox;

            STDEnt = new HSS_STUDENT();
            STDEnt.PROGRAM = ddlProgram.SelectedValue;
            STDEnt.BAT_CH = ddlBatch.SelectedValue;
            STDEnt = (HSS_STUDENT)STDSrv.GetSingle(STDEnt);
            if (STDEnt != null)
            {
                lblRegistration_No.Text = STDEnt.STUDENT_ID;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DT = new DistributedTransaction();
        foreach (GridViewRow gr in gridStudentTable.Rows)
        {

            Label lblRegistration_No = gr.FindControl("lblRegistration_No") as Label;
            Label lblpkid = gr.FindControl("lblpkid") as Label;
            CheckBox chkbox = gr.FindControl("chkbox") as CheckBox;

            STDEnt = new HSS_STUDENT();
            STDEnt.STUDENT_ID = lblRegistration_No.Text;
            chkbox.Checked = true;
            STDEnt = (HSS_STUDENT)STDSrv.GetSingle(STDEnt);
            if (STDEnt == null)
            {
                STDEnt = new HSS_STUDENT();
                STDEnt.PK_ID = lblpkid.Text;
                STDEnt = (HSS_STUDENT)STDSrv.GetSingle(STDEnt, DT);
                if (STDEnt != null)
                {
                    STDEnt.STUDENT_ID = lblRegistration_No.Text;
                    STDSrv.Update(STDEnt, DT);

                    EntityList newList = new EntityList();
                    EDEnt = new HSS_EDUCATION_DETAIL();
                    EDEnt.STUDENT_PKID = lblpkid.Text;

                    theList = EDSrv.GetAll(EDEnt, DT);

                    foreach (HSS_EDUCATION_DETAIL hs in theList)
                    {
                        hs.STUDENT_ID = lblRegistration_No.Text;
                        EDSrv.Update(hs, DT);
                    }

                    AEnt = new HSS_ATTACHMENTS();
                    AEnt.STUDENT_PKID = lblpkid.Text;
                    AEnt = (HSS_ATTACHMENTS)ASrv.GetSingle(AEnt, DT);
                    if (AEnt != null)
                    {

                        AEnt.STUDENT_ID = lblRegistration_No.Text;
                        ASrv.Update(AEnt, DT);
                    }

                    IEEnt = new information_alert();
                    IEEnt.STUDENT_PKID = lblpkid.Text;
                    IEEnt = (information_alert)IESrv.GetSingle(IEEnt, DT);
                    if (IEEnt != null)
                    {

                        IEEnt.STUDENTID = lblRegistration_No.Text;
                        IESrv.Update(IEEnt, DT);
                    }

                    CSEnt = new HSS_CURRENT_STUDENT();
                    CSEnt.STUDENT_ID = lblRegistration_No.Text;
                    CSEnt.YEAR = hf.NepaliYear();
                    CSEnt.SEMESTER = ddlSemester.SelectedValue;
                    CSEnt.SECTION = "";
                    CSEnt.STATUS = "1";
                    //CSEnt.SHIFT = ddlshift.SelectedValue;
                    CSEnt.BATCH = ddlBatch.SelectedValue;
                    CSSrv.Insert(CSEnt, DT);

                    BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
                               {
                                   IncludeLabel = true,
                                   Alignment = AlignmentPositions.CENTER,
                                   Width = 200,
                                   Height = 50,
                                   RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                                   BackColor = Color.White,
                                   ForeColor = Color.Black,

                               };

                    Bitmap bitmap = new Bitmap(barcode.Encode(TYPE.CODE128B, lblRegistration_No.Text));

                    bitmap.Save(Server.MapPath("~/images/student_barcode/" + lblRegistration_No.Text + ".jpg"), ImageFormat.Jpeg);

                }
            }
            else
            {

                HelperFunction.MsgBox(this, this.GetType(), "already exist");
            }
        }

        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Successfull");
            gridStudentTable.Visible = false;

            clear();
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Save");
        }
        DT.Dispose();
        sem_shift.Visible = false;
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        OfcEnt = new OFFICE();
        OfcEnt = (OFFICE)OfcSrv.GetSingle(OfcEnt);
        if (OfcEnt != null)
        {

            lblCode.Text = OfcEnt.OFFICECODE + ddlProgram.SelectedItem.ToString();
            sem_shift.Visible = true;

            LoadData();
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        double regno = 0;
        try
        {
            regno = Convert.ToDouble(txtRegno.Text);
            foreach (GridViewRow gr in gridStudentTable.Rows)
            {

                Label lblRegistration_No = gr.FindControl("lblRegistration_No") as Label;
                CheckBox chkbox = gr.FindControl("chkbox") as CheckBox;
                if (chkbox.Checked == true)
                {
                    lblRegistration_No.Text = lblCode.Text + regno.ToString("000");
                    regno++;
                }
                else
                {
                    lblRegistration_No.Text = "";

                }
            }

        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Enter Number");
        }


        //  LoadData();

    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
}