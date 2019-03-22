using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using DataHelper.Framework;
using System.Collections;
using System.Data;
using BarcodeLib;
using System.Drawing;
using System.Drawing.Imaging;


public partial class finance_individualprintbill : System.Web.UI.Page
{

    string temp;
    int monthid;
    // string formonth;

    string billmasterid;

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    Classes CEnt = new Classes();
    ClassesService CSer = new ClassesService();

    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

    PAY_SCHEDULE_INSTALLMENT PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
    PAY_SCHEDULE_INSTALLMENTService PSISer = new PAY_SCHEDULE_INSTALLMENTService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    EntityList particularList = new EntityList();
    EntityList mparticularList = new EntityList();

    EntityList studentList = new EntityList();
    EntityList currentstudentList = new EntityList();
    EntityList bilstatList = new EntityList();

    STUDENT_PAY_SCHEDULE SPEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSer = new STUDENT_PAY_SCHEDULEService();

    scholorship_discount SCDEnt = new scholorship_discount();
    scholorship_discountService SCDSer = new scholorship_discountService();

    masterbill MBEnt = new masterbill();
    masterbillService MBSer = new masterbillService();

    bill BEnt = new bill();
    billService BSer = new billService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    admissionfee ADEnt = new admissionfee();
    admissionService ADSer = new admissionService();

    billstatus BLSEnt = new billstatus();
    billstatusService BLSSer = new billstatusService();

    billprintstatus BPEnt = new billprintstatus();
    billprintstatusService BPSer = new billprintstatusService();

    HelperFunction hf = new HelperFunction();

    DataTable ddt = new DataTable();
    HSS_NAME NEnt = new HSS_NAME();


    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    sms_record SMSEnt = new sms_record();
    sms_recordService SMSSer = new sms_recordService();

    STUDENT_ACCOUNT SAEnt = new STUDENT_ACCOUNT();
    STUDENT_ACCOUNTService SASer = new STUDENT_ACCOUNTService();

    HSS_NAMEService NSer = new HSS_NAMEService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    UserProfileEntity userprofile = new UserProfileEntity();



    double Gtotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            LoadMisc();

            txtDay.Text = hf.NepaliDay();
            txtMonth.Text = hf.NepaliMonth();
            txtYear.Text = hf.NepaliYear();

        }
    }


    protected void LoadMisc()
    {
        if (ddlStudent.SelectedValue != "Select")
        {
            SPEnt = new STUDENT_PAY_SCHEDULE();
            SPEnt.STUDENT_ID = ddlStudent.SelectedValue;
            //SPEnt.INSTALLMENT_NO = ddlInstallment.SelectedValue;
            //SPEnt.SEMESTER = ddlSemester.SelectedValue;
            theList = SPSer.GetAll(SPEnt);

            foreach (STUDENT_PAY_SCHEDULE SPS in theList)
            {
                if (SPS.PARTICULARS == "17")
                {
                    newList.Add(SPS);
                }
            }

            gridMisc.DataSource = newList;
            gridMisc.DataBind();

            if (gridMisc.Rows.Count == 0)
            {

                SPEnt = new STUDENT_PAY_SCHEDULE();
                ArrayList alist = new ArrayList();
                alist.Add(SPEnt);
                gridMisc.DataSource = alist;
                gridMisc.DataBind();
            }
        }
        else
        {
            gridMisc.DataSource = null;
            gridMisc.DataBind();

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

        ddlFaculty1.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty1.DataTextField = "FACULTY";
        ddlFaculty1.DataValueField = "PK_ID";
        ddlFaculty1.DataBind();

    }

    protected void LoadLevel()
    {
        LEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LSrv.GetAll(LEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();

        ddlLevel1.DataSource = LSrv.GetAll(LEnt);
        ddlLevel1.DataTextField = "LEVEL_NAME";
        ddlLevel1.DataValueField = "LEVEL_NAME";
        ddlLevel1.DataBind();
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

        ddlProgram1.DataSource = PSer.GetAll(PEnt);
        ddlProgram1.DataTextField = "PROGRAM_CODE";
        ddlProgram1.DataValueField = "PK_ID";
        ddlProgram1.DataBind();
        ddlProgram1.Items.Insert(0, "Select");

    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.BATCH = ddlBatch.SelectedValue;
        BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
        if (BTEnt != null)
        {
            SMEnt = new semester();
            SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
            SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
            theList = SMSer.GetAll(SMEnt);

            foreach (semester sem in theList)
            {
                semList.Add(sem);
            }
        }

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");


        ddlSemester1.DataSource = semList;
        ddlSemester1.DataTextField = "SEMESTER_CODE";
        ddlSemester1.DataValueField = "PK_ID";
        ddlSemester1.DataBind();
        ddlSemester1.Items.Insert(0, "Select");

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
        ddlBatch.Items.Insert(0, "Select");

        ddlBatch1.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch1.DataTextField = "BATCH";
        ddlBatch1.DataValueField = "BATCH";
        ddlBatch1.DataBind();
        ddlBatch1.Items.Insert(0, "Select");
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

    protected void LoadInstallment(string studentid, string semester)
    {

        //ddlInstallment.DataSource = hf.getInstallmentNo(studentid, semester);
        //ddlInstallment.DataTextField = "INSTALLMENT_NO";
        //ddlInstallment.DataValueField = "INSTALLMENT_NO";
        //ddlInstallment.DataBind();
        //ddlInstallment.Items.Insert(0, "Select");

        ddlFromInst.DataSource = hf.getsem_from_installmentno(studentid, semester);
        ddlFromInst.DataTextField = "SemInst";
        ddlFromInst.DataValueField = "INSTALLMENT_NO";
        ddlFromInst.DataBind();


        ddlToInst.DataSource = hf.getsem_from_installmentno(studentid, "");
        ddlToInst.DataTextField = "SemInst";
        ddlToInst.DataValueField = "INSTALLMENT_NO";
        ddlToInst.DataBind();


    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        LoadGridParticular();
    }

    protected void LoadGridParticular()
    {


        gridBillParicular.DataSource = hf.getsem_inst_amt(ddlProgram.SelectedValue, ddlFromInst.SelectedValue, ddlToInst.SelectedValue);
        gridBillParicular.DataBind();

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
        MBEnt = new masterbill();
        EntityList currentstudentTempList = new EntityList();
        EntityList payScheduleList = new EntityList();
        EntityList billdetailList = new EntityList();

        DistributedTransaction DT = new DistributedTransaction();
        CSEnt = new HSS_CURRENT_STUDENT();

        ArrayList alist = new ArrayList();
        ArrayList blist = new ArrayList();

        #region

        payScheduleList = new EntityList();
        billdetailList = new EntityList();
        alist = new ArrayList();
        blist = new ArrayList();

        #region  for inserting data in master bill with student id

        EntityList theList = new EntityList();

        MBEnt = new masterbill();
        MBEnt.STUDENT_ID = ddlStudentId.SelectedValue;
        theList = MBSer.GetAll(MBEnt);

        foreach (masterbill mb in theList)
        {
            if (mb.STATUS != "2")
            {

                mb.STATUS = "2";

                MBSer.Update(mb, DT);
            }
        }

        MBEnt = new masterbill();
        MBEnt.BATCH = ddlBatch.SelectedValue;
        MBEnt.DAY = txtDay.Text;
        MBEnt.MONTH = txtMonth.Text;
        MBEnt.YEAR = txtYear.Text;
        MBEnt.FISCALYEAR = hf.checkFiscalYear(txtMonth.Text, txtYear.Text);
        MBEnt.E_DATE = hf.ConvertNepaliTOEnglishDate(txtDay.Text, txtMonth.Text, txtYear.Text);
        MBEnt.STATUS = "0";
        MBEnt.STUDENT_ID = ddlStudentId.SelectedValue;
        MBEnt.GRANDTOTAL = hf.getMasterBillTotal(ddlStudentId.SelectedValue, "", ddlFromInst.SelectedValue, ddlToInst.SelectedValue);
        MBEnt.FOR_MONTHS = txtForMonth.Text;
        if (ddlFromInst.SelectedValue != ddlToInst.SelectedValue)
        {
            MBEnt.INSTALLMENT = ddlFromInst.SelectedValue + "-" + ddlToInst.SelectedValue;
        }
        else if (ddlFromInst.SelectedValue == ddlToInst.SelectedValue)
        {
            MBEnt.INSTALLMENT = ddlFromInst.SelectedValue;
        }
        MBEnt.PROGRAM = ddlProgram1.SelectedValue;
        MBEnt.SEMESTER = ddlSemester.SelectedValue;
        MBEnt.BILL_BY = userprofile.UserName;
        MBEnt.BILL_TIME = DateTime.Now.ToString("HH:mm:ss tt");


        #region to insert discount in master_bill
        double tot_disc = 0.0;
        int from_int = Convert.ToInt16(ddlFromInst.SelectedValue);
        int to_int = Convert.ToInt16(ddlToInst.SelectedValue);
        for (; from_int <= to_int; from_int++)
        {
            SCDEnt = new scholorship_discount();
            //SCDEnt.SEMESTER_ID = ddlSemester1.SelectedValue;
            SCDEnt.STUDENT_ID = ddlStudentId.SelectedValue;
            SCDEnt.INSTALLMENT_NO = from_int.ToString();
            SCDEnt = (scholorship_discount)SCDSer.GetSingle(SCDEnt);
            if (SCDEnt != null)
            {
                tot_disc = tot_disc + Convert.ToDouble(SCDEnt.AMOUNT);
            }
        }
        MBEnt.DISCOUNT = tot_disc.ToString();
        #endregion

        #region calculating remaining balance

        double remaining = 0;

        SAEnt = new STUDENT_ACCOUNT();
        SAEnt.STUDENT_ID = ddlStudentId.SelectedValue;
        SAEnt = (STUDENT_ACCOUNT)SASer.GetSingle(SAEnt);
        if (SAEnt != null)
        {

            //DataTable dt = new DataTable();
            //dt = hf.getRemBal(ddlStudentId.SelectedValue, "");
            //remaining = Convert.ToDouble(dt.Rows[0][1].ToString());

            remaining = Convert.ToDouble(hf.getRemBalN(ddlStudentId.SelectedValue, "").ToString());

        }

        MBEnt.REMAINING_BALANCE = (remaining).ToString("#0.00");
        MBEnt.F_GRANDTOTAL = ((remaining) - Convert.ToDouble(MBEnt.DISCOUNT) + Convert.ToDouble(hf.getMasterBillTotal(ddlStudentId.SelectedValue, "", ddlFromInst.SelectedValue, ddlToInst.SelectedValue))).ToString();

        #endregion

        if (hf.getMasterBillTotal(ddlStudentId.SelectedValue, "", ddlFromInst.SelectedValue, ddlToInst.SelectedValue) != "0")
        {

            billmasterid = MBSer.Insert(MBEnt, DT).ToString();

            #region to generate barcode of bill PKID
            //Array.ForEach(Directory.GetFiles(@"C:\inetpub\wwwroot\college\images\barcode_billPK\"), File.Delete);
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

            Bitmap bitmap = new Bitmap(barcode.Encode(TYPE.CODE128B, billmasterid));

            bitmap.Save(Server.MapPath("~/images/barcode_billPK/" + billmasterid + ".jpg"), ImageFormat.Jpeg);
            #endregion

            gridBillDetail.DataSource = hf.getBillDetailToInsert(ddlStudentId.SelectedValue, ddlSemester.SelectedValue, ddlFromInst.SelectedValue, ddlToInst.SelectedValue);
            gridBillDetail.DataBind();

            foreach (GridViewRow gr in gridBillDetail.Rows)
            {
                Label lblParticularId = gr.FindControl("lblParticularId") as Label;

                Label lblQty = gr.FindControl("lblQty") as Label;
                Label lblAmount = gr.FindControl("lblAmount") as Label;
                Label lblRemarks = gr.FindControl("lblRemarks") as Label;

                BEnt = new bill();
                BEnt.MBILL_ID = billmasterid;
                BEnt.PARTICULARS = lblParticularId.Text;

                BEnt.QUANTITY = lblQty.Text;

                //BEnt.TOTAL = lblAmount.Text;

                double total = 0.0;
                int from_install = Convert.ToInt16(ddlFromInst.SelectedValue);
                int to_install = Convert.ToInt16(ddlToInst.SelectedValue);
                for (; from_install <= to_install; from_install++)
                {
                    SPEnt = new STUDENT_PAY_SCHEDULE();
                    SPEnt.PARTICULARS = lblParticularId.Text;
                    SPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                    SPEnt.INSTALLMENT_NO = from_install.ToString();
                    SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
                    if (SPEnt != null)
                    {
                        total = total + Convert.ToDouble(SPEnt.AMOUNT);
                    }
                }
                BEnt.TOTAL = total.ToString();


                BEnt.STATUS = "0";
                BEnt.REMARKS = lblRemarks.Text;
                BSer.Insert(BEnt, DT);
            }


            #region to insert in student_account

            SAEnt = new STUDENT_ACCOUNT();
            SAEnt.STUDENT_ID = ddlStudentId.SelectedValue;
            SAEnt.MBILL_ID = billmasterid;
            SAEnt.BILL_AMOUNT = (Convert.ToDouble(MBEnt.GRANDTOTAL) - Convert.ToDouble(MBEnt.DISCOUNT)).ToString();

            SASer.Insert(SAEnt, DT);

            #endregion


            #endregion

            #region to update status of student_pay_schedule to 1, # Status: 1 is bill generated

            if (ddlFromInst.SelectedValue != ddlToInst.SelectedValue)
            {
                for (int i = Convert.ToInt32(ddlFromInst.SelectedValue); i <= Convert.ToInt32(ddlToInst.SelectedValue); i++)
                {

                    SPEnt = new STUDENT_PAY_SCHEDULE();
                    EntityList SPSList = new EntityList();
                    SPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                    SPEnt.INSTALLMENT_NO = i.ToString();
                    //SPEnt.SEMESTER = ddlSemester1.SelectedValue;
                    SPSList = SPSer.GetAll(SPEnt, DT);

                    foreach (STUDENT_PAY_SCHEDULE sp in SPSList)
                    {
                        if (sp.STATUS == "0")
                        {
                            sp.STATUS = "1";
                            SPSer.Update(sp, DT);
                        }
                    }

                }
            }
            else if (ddlFromInst.SelectedValue == ddlToInst.SelectedValue)
            {
                MBEnt.INSTALLMENT = ddlFromInst.SelectedValue;
                SPEnt = new STUDENT_PAY_SCHEDULE();
                EntityList SPSList = new EntityList();
                SPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                SPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
                SPEnt.SEMESTER = ddlSemester1.SelectedValue;
                SPSList = SPSer.GetAll(SPEnt, DT);

                foreach (STUDENT_PAY_SCHEDULE sp in SPSList)
                {
                    if (sp.STATUS == "0")
                    {
                        sp.STATUS = "1";
                        SPSer.Update(sp, DT);
                    }
                }
            }
            #endregion
        }

        #endregion

        #region to insert in billprintstatus table
        BPEnt = new billprintstatus();

        BPEnt.DAY = txtDay.Text;
        BPEnt.MONTH = txtMonth.Text;
        BPEnt.YEAR = txtYear.Text;
        BPEnt.FISCALYEAR = hf.checkFiscalYear(txtMonth.Text, txtYear.Text);
        BPEnt.E_DATE = hf.ConvertNepaliTOEnglish(txtDay.Text, txtMonth.Text, txtYear.Text);
        BPEnt.PRINT_STATUS = "0";
        BPEnt.BATCH = ddlBatch.SelectedValue;
        BPEnt.SEMESTER = ddlSemester.SelectedValue;
        if (ddlFromInst.SelectedValue != ddlToInst.SelectedValue)
        {
            BPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue + "-" + ddlToInst.SelectedValue;
        }
        else if (ddlFromInst.SelectedValue == ddlToInst.SelectedValue)
        {
            BPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
        }

        BPEnt.PROGRAM = ddlProgram.SelectedValue;
        BPEnt.STUDENT_ID = ddlStudentId.SelectedValue;

        BPSer.Insert(BPEnt, DT);
        #endregion

        if (DT.HAPPY == true)
        {
            DT.Commit();

            HelperFunction.MsgBox(this, this.GetType(), "Individual Bill Generated Successfully");

            //LoadSMSForBill();
            //  GenerateSMSFormat();
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Individual Bill Not Generated");
        }
        DT.Dispose();
        btnGenerate.Visible = false;
        misc_div.Visible = true;
        billgen_div.Visible = false;
    }

    protected void LoadSMSForBill()
    {
        //gridSMSForBill.DataSource = hf.SmsForBill(txtDay.Text, txtMonth.Text, txtYear.Text, ddlBatch.SelectedValue);
        //gridSMSForBill.DataBind();

    }

    protected void GenerateSMSFormat()
    {
        if (gridSMSForBill.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gridSMSForBill.Rows)
            {
                Label lblName = gr.FindControl("lblName") as Label;
                Label lblMonth = gr.FindControl("lblMonth") as Label;
                Label lblAmount = gr.FindControl("lblAmount") as Label;
                Label lblPrevious = gr.FindControl("lblPrevious") as Label;
                Label lblTotal = gr.FindControl("lblTotal") as Label;
                Label lblSMSNumber = gr.FindControl("lblSMSNumber") as Label;


                string message = "";

                message = "Dear " + lblName.Text + ", your fee bill has been issued." + Environment.NewLine + "Month: " + lblMonth.Text + Environment.NewLine + "Amount: " + lblAmount.Text + Environment.NewLine + "Previous due: " + lblPrevious.Text + Environment.NewLine + "Total Amount: " + lblTotal.Text + Environment.NewLine + "- NCFT";

                string status = hf.SendSms(lblSMSNumber.Text, message);

                if (status == "Success")
                {
                    SMSEnt = new sms_record();
                    SMSEnt.FACULTY = "1";
                    SMSEnt.PROGRAM = "1";
                    SMSEnt.SEMESTER = "";
                    SMSEnt.SECTION = "";
                    SMSEnt.MESSAGE = message;
                    SMSEnt.PHONE_NUMBERS = lblSMSNumber.Text;
                    SMSEnt.SEND_DATE = System.DateTime.Today.ToString("dd/MM/yyyy");
                    userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
                    SMSEnt.SEND_BY = userprofile.UserName;
                    SMSEnt.SEND_MODULE = "Account";

                    SMSSer.Insert(SMSEnt);
                }
                else
                {

                }


            }
        }

    }

    protected void printbill()
    {

    }

    protected void gridFineCalc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            Label lblAmount = e.Row.FindControl("lblAmount") as Label;
            Label lblAdvance = e.Row.FindControl("lblAdvance") as Label;
            Label lblRemaining = e.Row.FindControl("lblRemaining") as Label;

            SEnt = new HSS_STUDENT();
            SEnt.STUDENT_ID = lblStudentId.Text;
            SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
            if (SEnt != null)
            {
                lblStudentName.Text = SEnt.NAME_ENGLISH;
            }
        }
    }

    protected void LoadStudentName()
    {

        ddlStudent.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "");
        ddlStudent.DataTextField = "student_name";
        ddlStudent.DataValueField = "STUDENT_ID";
        ddlStudent.DataBind();
        ddlStudent.Items.Insert(0, "Select");

        ddlStudentId.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "");
        ddlStudentId.DataTextField = "student_name";
        ddlStudentId.DataValueField = "STUDENT_ID";
        ddlStudentId.DataBind();
        ddlStudentId.Items.Insert(0, "Select");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridMisc.HeaderRow;
        TextBox txtParticularH = (TextBox)row.FindControl("txtParticularH");
        TextBox txtAmountH = (TextBox)row.FindControl("txtAmountH");

        if (ddlStudent.SelectedValue != "Select")
        {
            try
            {
                double amount = Convert.ToDouble(txtAmountH.Text);

                SPEnt = new STUDENT_PAY_SCHEDULE();
                SPEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SPEnt.SEMESTER = ddlSemester.SelectedValue;

                SPEnt.REMARKS = txtParticularH.Text;
                SPEnt.AMOUNT = txtAmountH.Text;
                SPEnt.DISCOUNT = "0";
                SPEnt.STATUS = "0";
                SPEnt.YEAR = txtYear.Text;
                SPEnt.PARTICULARS = "17";

                PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
                PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
                PSIEnt.BATCH = ddlBatch.SelectedValue;
                PSIEnt.SEMESTERID = ddlSemester.SelectedValue;
                //PSIEnt.INST_NO = ddlInstallment.SelectedValue;
                PSIEnt = (PAY_SCHEDULE_INSTALLMENT)PSISer.GetSingle(PSIEnt);
                if (PSIEnt != null)
                {
                    MEnt = new Months();
                    MEnt.MONTHID = PSIEnt.INST_MONTH;
                    MEnt = (Months)MSer.GetSingle(MEnt);
                    if (MEnt != null)
                    {

                        SPEnt.FOR_MONTH = MEnt.MONTHNAME;
                    }
                }

                SPSer.Insert(SPEnt);
                LoadMisc();

            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Enter Number Only in Amount Field");
            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Student and Installment No");
        }
    }

    //protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadMisc();

    //}
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select" && ddlProgram.SelectedValue != "Select" && ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")
        {

            ddlFaculty1.SelectedValue = ddlFaculty.SelectedValue;
            ddlProgram1.SelectedValue = ddlProgram.SelectedValue;
            ddlBatch1.SelectedValue = ddlBatch.SelectedValue;
            ddlSemester1.SelectedValue = ddlSemester.SelectedValue;

            ddlStudentId.SelectedValue = ddlStudent.SelectedValue;

            if (ddlStudent.SelectedValue != "Select")
            {
                LoadInstallment(ddlStudent.SelectedValue, ddlSemester.SelectedValue);
                getForMonth();
            }
            misc_div.Visible = false;
            billgen_div.Visible = true;
            btnGenerate.Visible = true;

        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Faculty,Program, Batch and Semester for Further Continue");
        }
    }
    protected void gridMisc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridMisc.EditIndex = e.NewEditIndex;
        LoadMisc();
    }
    protected void gridMisc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridMisc.Rows[e.RowIndex];
        Label lblPkId = (Label)row.FindControl("lblPkId");
        TextBox txtParticularE = (TextBox)row.FindControl("txtParticularE");
        TextBox txtAmountE = (TextBox)row.FindControl("txtAmountE");

        try
        {
            double amt = Convert.ToDouble(txtAmountE.Text);
            SPEnt = new STUDENT_PAY_SCHEDULE();
            SPEnt.PK_ID = lblPkId.Text;
            SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
            if (SPEnt != null)
            {

                SPEnt.REMARKS = txtParticularE.Text;
                SPEnt.AMOUNT = txtAmountE.Text;


                SPSer.Update(SPEnt);
            }

            gridMisc.EditIndex = -1;
            LoadMisc();
        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Number only in Amount Field");
        }
    }
    protected void gridMisc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridMisc.EditIndex = -1;
        LoadMisc();
    }
    protected void gridMisc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = gridMisc.Rows[e.RowIndex];
        Label lblPkId = (Label)row.FindControl("lblPkId");

        SPEnt = new STUDENT_PAY_SCHEDULE();
        SPEnt.PK_ID = lblPkId.Text;
        SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
        if (SPEnt != null)
        {
            SPSer.Delete(SPEnt);
        }
        gridMisc.EditIndex = -1;
        LoadMisc();

    }
    protected void ddlBatch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudentName();

    }
    protected void ddlInstallment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMisc();
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
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadStudentName();
    }
    //protected void ddlStudent_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    if (ddlStudent.SelectedValue != "Select")
    //    {
    //        LoadInstallment(ddlStudent.SelectedValue, ddlSemester.SelectedValue);
    //        LoadMisc();
    //    }
    //}
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();

    }
    protected void ddlInstallment_SelectedIndexChanged1(object sender, EventArgs e)
    {
        LoadMisc();

    }
    protected void ddlStudentId_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlStudent.SelectedValue != "Select")
        {

            SPEnt = new STUDENT_PAY_SCHEDULE();
            SPEnt.STUDENT_ID = ddlStudent.SelectedValue;
            SPEnt.SEMESTER = ddlSemester.SelectedValue;
            SPEnt.STATUS = "0";
            SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
            if (SPEnt != null)
            {
                LoadInstallment(ddlStudent.SelectedValue, ddlSemester.SelectedValue);
                //getForMonth();
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Bill Already Generated");
            }
        }
    }
    protected void ddlFromInst_SelectedIndexChanged(object sender, EventArgs e)
    {
        getForMonth();
    }
    protected void ddlToInst_SelectedIndexChanged(object sender, EventArgs e)
    {
        getForMonth();
    }
    protected void getForMonth()
    {

        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.STUDENT_ID = ddlStudent.SelectedValue;
        CSEnt.BATCH = ddlBatch.SelectedValue;
        CSEnt.STATUS = "1";
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {

            if (Convert.ToDouble(ddlFromInst.SelectedValue) <= Convert.ToDouble(ddlToInst.SelectedValue))
            {
                if (ddlFromInst.SelectedValue == ddlToInst.SelectedValue)
                {

                    PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
                    PSIEnt.CUM_INST_NO = ddlFromInst.SelectedValue;
                    PSIEnt = (PAY_SCHEDULE_INSTALLMENT)PSISer.GetSingle(PSIEnt);
                    if (PSIEnt != null)
                    {


                        SPEnt = new STUDENT_PAY_SCHEDULE();
                        SPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                        SPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
                        SPEnt.SEMESTER = ddlSemester1.SelectedValue;
                        SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
                        if (SPEnt != null)
                        {
                            txtForMonth.Text = SPEnt.FOR_MONTH;
                        }
                    }
                }
                else
                {
                    PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
                    PSIEnt.CUM_INST_NO = ddlFromInst.SelectedValue;
                    PSIEnt = (PAY_SCHEDULE_INSTALLMENT)PSISer.GetSingle(PSIEnt);
                    if (PSIEnt != null)
                    {

                        SPEnt = new STUDENT_PAY_SCHEDULE();
                        SPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                        SPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
                        SPEnt.SEMESTER = ddlSemester1.SelectedValue;
                        SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
                        if (SPEnt != null)
                        {
                            txtForMonth.Text = SPEnt.FOR_MONTH;
                        }
                        SPEnt = new STUDENT_PAY_SCHEDULE();
                        SPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                        SPEnt.INSTALLMENT_NO = ddlToInst.SelectedValue;
                        SPEnt.SEMESTER = ddlSemester1.SelectedValue;
                        SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
                        if (SPEnt != null)
                        {
                            txtForMonth.Text += "-" + SPEnt.FOR_MONTH;
                        }
                    }
                }

            }
            else
            {

                HelperFunction.MsgBox(this, this.GetType(), "From Installment Cannot be greater than To Installment");
                ddlFromInst.SelectedIndex = 0;
                ddlToInst.SelectedIndex = 0;
                getForMonth();
            }
        }
    }

}
