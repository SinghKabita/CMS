using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;
using System.Collections;


public partial class finance_report_studentpaymenthistory : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    STUDENT_PAY_SCHEDULE SPSEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSSer = new STUDENT_PAY_SCHEDULEService();

    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

    Receipt RCPEnt = new Receipt();
    ReceiptService RCPSer = new ReceiptService();

    admissionfee ADMEnt = new admissionfee();
    admissionService ADMSer = new admissionService();

    masterbill MBEnt = new masterbill();
    masterbillService MBSer = new masterbillService();

    bill BLEnt = new bill();
    billService BLSer = new billService();

    Amount_collection AMTEnt = new Amount_collection();
    Amount_collectionService AMTSer = new Amount_collectionService();


    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    Withdraw WDEnt = new Withdraw();
    WithdrawService WDSer = new WithdrawService();


    HSS_NAME NAMEEnt = new HSS_NAME();
    HSS_NAMEService NAMESer = new HSS_NAMEService();


    HelperFunction hf = new HelperFunction();

    string admissionid = "";
    string annualfeeid = "";

    double fgtotal = 0.0;
    double rcptotal = 0.0;
    double dueamount = 0.0;
    double total = 0;
    double receiptotal = 0;
    double totalW = 0;
    double receiptotalW = 0;
    string stdID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            stdID = Request.QueryString.Get("studentId");
            if (stdID != "")
            {
                LoadView(stdID);
            }
            LoadFaculty();
            loadLevel();

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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");


    }

    protected void loadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.SEMESTER = ddlSemester.SelectedValue;

        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();


    }

    protected void LoadSemester()
    {
        EntityList theList = new EntityList();
        EntityList semList = new EntityList();
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        theList = BYSer.GetAll(BYEnt);
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

    protected void LoadStudentName()
    {

        ddlStudent.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "");
        ddlStudent.DataTextField = "student_name";
        ddlStudent.DataValueField = "STUDENT_ID";
        ddlStudent.DataBind();
        ddlStudent.Items.Insert(0, "Select");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadView(ddlStudent.SelectedValue);
    }

    protected void LoadView(string stdID)
    {
        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.STUDENT_ID = stdID;

        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null && stdID != null)
        {
            hide.Visible = true;
            LoadAllData(stdID);
            imgStudent.ImageUrl = "~/images/bachelorstudent/" + stdID + ".jpg";
        }
        else
        {
            //hide.Visible = true;
            //HelperFunction.MsgBox(this, this.GetType(), "Invalid Student Id");
        }
    }

    protected void LoadAllData(string stdid)
    {
        LoadStudentInfo(stdid);

        LoadPaySchedule(stdid);

        LoadReceipt(stdid);
        LoadWithdraw(stdid);
    }

    protected void LoadStudentInfo(string stdid)
    {
        STEnt = new HSS_STUDENT();
        STEnt.STUDENT_ID = stdid;
        STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
        if (STEnt != null)
        {
            lblStudentName.Text = STEnt.NAME_ENGLISH;
            lblStudentId.Text = stdid;
        }

        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.STUDENT_ID = stdid;
        CSEnt.STATUS = "1";
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {
            lblBatch.Text = CSEnt.BATCH;

            SMEnt = new semester();
            SMEnt.PK_ID = ddlSemester.SelectedValue;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {

                lblSemester.Text = SMEnt.SEMESTER_CODE + " " + CSEnt.SECTION;
            }
        }
    }

    protected void LoadPaySchedule(string stdid)
    {
        gridPaySchedule.DataSource = hf.getindividualpayschedule(stdid);
        gridPaySchedule.DataBind();


        gridBill.DataSource = hf.getindividualbill(lblBatch.Text, stdid);
        gridBill.DataBind();

        if (gridPaySchedule.Rows.Count > 0)
        {
            div_pay_schedule.Visible = true;
        }
        else
        {
            div_pay_schedule.Visible = false;
        }

    }

    protected void LoadReceipt(string stdid)
    {

        RCPEnt = new Receipt();
        RCPEnt.STUDENTID = stdid;

        // SPSEnt.SECTION = ddlSection.SelectedValue;

        gridReceipt.DataSource = RCPSer.GetAll(RCPEnt);
        gridReceipt.DataBind();

        if (gridReceipt.Rows.Count == 0)
        {
            div_receipt.Visible = false;
            gridReceipt.DataSource = null;
            gridReceipt.DataBind();
        }
        else
        {
            div_receipt.Visible = true;
        }

    }

    protected void LoadWithdraw(string stdid)
    {

        WDEnt = new Withdraw();
        WDEnt.STUDENTID = stdid;

        // SPSEnt.SECTION = ddlSection.SelectedValue;

        gridWithdraw.DataSource = WDSer.GetAll(WDEnt);
        gridWithdraw.DataBind();

        if (gridWithdraw.Rows.Count == 0)
        {
            div_withdraw.Visible = false;
            gridWithdraw.DataSource = null;
            gridWithdraw.DataBind();
        }
        else
        {
            div_withdraw.Visible = true;
        }

    }

    protected void gridPaySchedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;
            lblTotal.Text = Convert.ToDouble(lblTotal.Text).ToString("#0.00");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblGrandTotal = e.Row.FindControl("lblGrandTotal") as Label;



            foreach (GridViewRow row in gridPaySchedule.Rows)
            {


                Label lblTotal = row.FindControl("lblTotal") as Label;



                total = total + (Convert.ToDouble(lblTotal.Text));


            }

            lblGrandTotal.Text = (total).ToString("#0.00");


        }
    }

    protected void gridReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblReceiptNo = e.Row.FindControl("lblReceiptNo") as Label;
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;

            lblReceiptNo.Text = Convert.ToDouble(lblReceiptNo.Text).ToString("000000");
            lblAmount.Text = Convert.ToDouble(lblAmount.Text).ToString("#0.00");
            receiptotal = receiptotal + Convert.ToDouble(lblAmount.Text);


            Label lblSemesterR = e.Row.FindControl("lblSemesterR") as Label;
            Label lblSemCode = e.Row.FindControl("lblSemCode") as Label;

            SMEnt = new semester();
            SMEnt.PK_ID = lblSemesterR.Text;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lblSemCode.Text = SMEnt.SEMESTER_CODE;
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;
            lblTotal.Text = receiptotal.ToString("#0.00");
        }
    }

    protected void gridBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotalAmt = e.Row.FindControl("lblTotalAmt") as Label;
            lblTotalAmt.Text = Convert.ToDouble(lblTotalAmt.Text).ToString("#0.00");

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

            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }

        if (ddlProgram.SelectedValue == "Select")
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }

    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();

    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudentName();
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
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBatch();
        LoadStudentName();
    }

    protected void gridWithdraw_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblReceiptNo = e.Row.FindControl("lblReceiptNo") as Label;
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;

            //lblReceiptNo.Text = Convert.ToDouble(lblReceiptNo.Text).ToString("000000");
            lblAmount.Text = Convert.ToDouble(lblAmount.Text).ToString("#0.00");
            receiptotalW = receiptotalW + Convert.ToDouble(lblAmount.Text);


            Label lblSem = e.Row.FindControl("lblSem") as Label;
            Label lblSemCodeW = e.Row.FindControl("lblSemCodeW") as Label;

            SMEnt = new semester();
            SMEnt.PK_ID = lblSem.Text;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lblSemCodeW.Text = SMEnt.SEMESTER_CODE;
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;
            lblTotal.Text = receiptotalW.ToString("#0.00");

        }

    }
}