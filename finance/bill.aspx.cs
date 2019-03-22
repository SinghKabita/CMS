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
using System.IO;
using BarcodeLib;
using System.Drawing;
using System.Drawing.Imaging;

public partial class finance_bill : System.Web.UI.Page
{

    string temp;
    int monthid;
    // string formonth;

    string billmasterid;

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

    Classes CEnt = new Classes();
    ClassesService CSer = new ClassesService();

    //Particulars PEnt = new Particulars();
    //ParticularsService PSer = new ParticularsService();

    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

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
    HSS_NAMEService NSer = new HSS_NAMEService();

    PAY_SCHEDULE_INSTALLMENT PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
    PAY_SCHEDULE_INSTALLMENTService PSISer = new PAY_SCHEDULE_INSTALLMENTService();

    STUDENT_ACCOUNT SAEnt = new STUDENT_ACCOUNT();
    STUDENT_ACCOUNTService SASer = new STUDENT_ACCOUNTService();

    UserProfileEntity userprofile = new UserProfileEntity();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            loadLevel();
            txtDay.Text = hf.NepaliDay();
            txtMonth.Text = hf.NepaliMonth();
            txtYear.Text = hf.NepaliYear();
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
        ddlBatch1.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");


        ddlFaculty1.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty1.DataTextField = "FACULTY";
        ddlFaculty1.DataValueField = "PK_ID";
        ddlFaculty1.DataBind();
        ddlFaculty1.Items.Insert(0, "Select");


        ddlFaculty2.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty2.DataTextField = "FACULTY";
        ddlFaculty2.DataValueField = "PK_ID";
        ddlFaculty2.DataBind();
        ddlFaculty2.Items.Insert(0, "Select");
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
        ddlBatch1.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");


        ddlProgram1.DataSource = PSer.GetAll(PEnt);
        ddlProgram1.DataTextField = "PROGRAM_CODE";
        ddlProgram1.DataValueField = "PK_ID";
        ddlProgram1.DataBind();
        ddlProgram1.Items.Insert(0, "Select");

        ddlProgram2.DataSource = PSer.GetAll(PEnt);
        ddlProgram2.DataTextField = "PROGRAM_CODE";
        ddlProgram2.DataValueField = "PK_ID";
        ddlProgram2.DataBind();
        ddlProgram2.Items.Insert(0, "Select");
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

        ddlBatch1.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch1.DataTextField = "BATCH";
        ddlBatch1.DataValueField = "BATCH";
        ddlBatch1.DataBind();

        ddlBatch2.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch2.DataTextField = "BATCH";
        ddlBatch2.DataValueField = "BATCH";
        ddlBatch2.DataBind();
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


        ddlSemester1.DataSource = semList;
        ddlSemester1.DataTextField = "SEMESTER_CODE";
        ddlSemester1.DataValueField = "PK_ID";
        ddlSemester1.DataBind();
        ddlSemester1.Items.Insert(0, "Select");

        ddlSemester2.DataSource = semList;
        ddlSemester2.DataTextField = "SEMESTER_CODE";
        ddlSemester2.DataValueField = "PK_ID";
        ddlSemester2.DataBind();
        ddlSemester2.Items.Insert(0, "Select");
    }
    protected void LoadMisc()
    {
        SPEnt = new STUDENT_PAY_SCHEDULE();
        SPEnt.STUDENT_ID = ddlStudent.SelectedValue;
        SPEnt.INSTALLMENT_NO = ddlInstallment.SelectedValue;
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
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void LoadInstallment(string semester)
    {

        ddlFromInst.DataSource = hf.getsem_from_installmentnoAll(semester);
        ddlFromInst.DataTextField = "SemInst";
        ddlFromInst.DataValueField = "INSTALLMENT_NO";
        ddlFromInst.DataBind();

        ddlToInst.DataSource = hf.getsem_from_installmentnoAll(semester);
        ddlToInst.DataTextField = "SemInst";
        ddlToInst.DataValueField = "INSTALLMENT_NO";
        ddlToInst.DataBind();

    }

    protected void getForMonth()
    {
        CSEnt = new HSS_CURRENT_STUDENT();

        CSEnt.BATCH = ddlBatch.SelectedValue;
        CSEnt.STATUS = "1";
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {

            if (ddlFromInst.SelectedValue == ddlToInst.SelectedValue)
            {
                SPEnt = new STUDENT_PAY_SCHEDULE();
                SPEnt.STUDENT_ID = CSEnt.STUDENT_ID;
                SPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
                SPEnt.SEMESTER = ddlSemester.SelectedValue;
                theList = SPSer.GetAll(SPEnt);
                foreach (STUDENT_PAY_SCHEDULE SPS in theList)
                {
                    if (SPS.FOR_MONTH != "")
                    {
                        txtForMonth.Text = SPS.FOR_MONTH;
                    }
                }


            }
            else
            {
                SPEnt = new STUDENT_PAY_SCHEDULE();
                SPEnt.STUDENT_ID = CSEnt.STUDENT_ID;
                SPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
                SPEnt.SEMESTER = ddlSemester.SelectedValue;
                SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
                if (SPEnt != null)
                {
                    txtForMonth.Text = SPEnt.FOR_MONTH;
                }
                SPEnt = new STUDENT_PAY_SCHEDULE();
                SPEnt.STUDENT_ID = CSEnt.STUDENT_ID;
                SPEnt.INSTALLMENT_NO = ddlToInst.SelectedValue;
                SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
                if (SPEnt != null)
                {
                    txtForMonth.Text += "-" + SPEnt.FOR_MONTH;
                }
            }
        }
    }
    protected void LoadFineCalc()
    {

        gridFineCalc.DataSource = hf.getRemBal("", ddlBatch.SelectedValue);
        gridFineCalc.DataBind();
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (ddlFromInst.SelectedValue != "Select" && ddlToInst.SelectedValue != "Select")
        {
            if (Convert.ToDouble(ddlFromInst.SelectedValue) <= Convert.ToDouble(ddlToInst.SelectedValue))
            {
                LoadGridParticular();
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "From Installment Cannot be greater than To Installment");
                gridBillParicular.DataSource = null;
                gridBillParicular.DataBind();
            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select From and To Installment");

            gridBillParicular.DataSource = null;
            gridBillParicular.DataBind();
        }
    }
    protected void LoadGridParticular()
    {
        EntityList stdList = new EntityList();
        string studentid = "";
        SEnt = new HSS_STUDENT();

        SEnt.PROGRAM = ddlProgram2.SelectedValue;
        SEnt.BAT_CH = ddlBatch.SelectedValue;
        stdList = SSer.GetAll(SEnt);
        foreach (HSS_STUDENT si in stdList)
        {
            CSEnt = new HSS_CURRENT_STUDENT();
            CSEnt.STATUS = "1";
            CSEnt.SEMESTER = ddlSemester2.SelectedValue;
            CSEnt.STUDENT_ID = si.STUDENT_ID;
            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
            if (CSEnt != null)
            {
                studentid = CSEnt.STUDENT_ID;
                break;
            }
        }

        EntityList theList = new EntityList();

        PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
        PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
        PSIEnt.BATCH = ddlBatch.SelectedValue;
        PSIEnt.SEMESTERID = ddlSemester.SelectedValue;
        PSIEnt.BILL_STATUS = "0";
        theList = PSISer.GetAll(PSIEnt);
        foreach (PAY_SCHEDULE_INSTALLMENT psi in theList)
        {

            newList.Add(psi);
        }

        gridBillParicular.DataSource = newList;
        gridBillParicular.DataBind();

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];

        EntityList payScheduleList = new EntityList();
        EntityList billdetailList = new EntityList();
        EntityList StdPaySchList = new EntityList();
        EntityList ValidStudentList = new EntityList();

        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.BATCH = ddlBatch.SelectedValue;
        CSEnt.SEMESTER = ddlSemester.SelectedValue;
        CSEnt.STATUS = "1";
        currentstudentList = CSSer.GetAll(CSEnt);

        foreach (HSS_CURRENT_STUDENT cs in currentstudentList)
        {
            SPEnt = new STUDENT_PAY_SCHEDULE();
            SPEnt.STUDENT_ID = cs.STUDENT_ID;
            SPEnt.SEMESTER = ddlSemester.SelectedValue;
            SPEnt.INSTALLMENT_NO = ddlToInst.SelectedValue;
            SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
            if (SPEnt != null)
            {
                if (SPEnt.STATUS == "0")
                    ValidStudentList.Add(cs);
            }

            #region to change status of old bills to status 2 so that old bill will not be valis for payment
            SPEnt = new STUDENT_PAY_SCHEDULE();
            SPEnt.STUDENT_ID = cs.STUDENT_ID;
            SPEnt.SEMESTER = ddlSemester.SelectedValue;
            SPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
            SPEnt = (STUDENT_PAY_SCHEDULE)SPSer.GetSingle(SPEnt);
            if (SPEnt != null)
            {
                if (SPEnt.STATUS == "0")
                {
                    MBEnt = new masterbill();
                    MBEnt.STUDENT_ID = cs.STUDENT_ID;
                    MBEnt.SEMESTER = ddlSemester.SelectedValue;
                    theList = MBSer.GetAll(MBEnt);

                    foreach (masterbill mb in theList)
                    {
                        if (mb.STATUS != "2")
                        {
                            mb.STATUS = "2";
                            MBSer.Update(mb, DT);
                        }
                    }

                }
            }

            #endregion

        }

        #region  for inserting data in master bill with student id
        foreach (HSS_CURRENT_STUDENT cs in ValidStudentList)
        {
            payScheduleList = new EntityList();
            billdetailList = new EntityList();

            MBEnt = new masterbill();
            MBEnt.BATCH = ddlBatch.SelectedValue;
            MBEnt.DAY = txtDay.Text;
            MBEnt.MONTH = txtMonth.Text;
            MBEnt.YEAR = txtYear.Text;
            MBEnt.FISCALYEAR = hf.checkFiscalYear(txtMonth.Text, txtYear.Text);
            MBEnt.E_DATE = hf.ConvertNepaliTOEnglishDate(txtDay.Text, txtMonth.Text, txtYear.Text);
            MBEnt.STATUS = "0";
            MBEnt.STUDENT_ID = cs.STUDENT_ID;
            MBEnt.GRANDTOTAL = hf.getMasterBillTotal(cs.STUDENT_ID, ddlSemester.SelectedValue, ddlFromInst.SelectedValue, ddlToInst.SelectedValue);
            MBEnt.FOR_MONTHS = txtForMonth.Text;
            if (ddlFromInst.SelectedValue != ddlToInst.SelectedValue)
            {
                MBEnt.INSTALLMENT = ddlFromInst.SelectedValue + "-" + ddlFromInst.SelectedValue;
            }
            else if (ddlFromInst.SelectedValue == ddlToInst.SelectedValue)
            {
                MBEnt.INSTALLMENT = ddlFromInst.SelectedValue;
            }
            MBEnt.PROGRAM = ddlProgram2.SelectedValue;
            MBEnt.SEMESTER = ddlSemester2.SelectedValue;
            MBEnt.BILL_BY = userprofile.UserName;
            MBEnt.BILL_TIME = DateTime.Now.ToString("HH:mm:ss");
            #region to calculate discount for master_bill
            SCDEnt = new scholorship_discount();
            SCDEnt.SEMESTER_ID = cs.SEMESTER;
            SCDEnt.STUDENT_ID = cs.STUDENT_ID;
            SCDEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
            SCDEnt = (scholorship_discount)SCDSer.GetSingle(SCDEnt);
            if (SCDEnt != null)
            {
                MBEnt.DISCOUNT = SCDEnt.AMOUNT;
            }
            else
            {
                MBEnt.DISCOUNT = "0.00";
            }
            #endregion


            #region calculating previous debit or credit balance

            double remaining = 0;
            SAEnt = new STUDENT_ACCOUNT();
            SAEnt.STUDENT_ID = cs.STUDENT_ID;
            SAEnt = (STUDENT_ACCOUNT)SASer.GetSingle(SAEnt);
            if (SAEnt != null)
            {
                DataTable dt = new DataTable();
                dt = hf.getRemBal(cs.STUDENT_ID, "");
                try
                {
                    remaining = Convert.ToDouble(dt.Rows[0][1].ToString());
                }
                catch { }
            }
            MBEnt.REMAINING_BALANCE = (remaining).ToString("#0.00");
            #endregion


            MBEnt.F_GRANDTOTAL = ((remaining) - Convert.ToDouble(MBEnt.DISCOUNT) + Convert.ToDouble(hf.getMasterBillTotal(cs.STUDENT_ID, ddlSemester.SelectedValue, ddlFromInst.SelectedValue, ddlToInst.SelectedValue))).ToString();


            if (hf.getMasterBillTotal(cs.STUDENT_ID, ddlSemester.SelectedValue, ddlFromInst.SelectedValue, ddlToInst.SelectedValue) != "0")
            {
                billmasterid = MBSer.Insert(MBEnt, DT).ToString();

                #region to generate barcode of bill PKID
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

                #region to insert bill deatil in bill table

                gridBillDetail.DataSource = hf.getBillDetailToInsert(cs.STUDENT_ID, ddlSemester.SelectedValue, ddlFromInst.SelectedValue, ddlToInst.SelectedValue);
                gridBillDetail.DataBind();

                foreach (GridViewRow gr in gridBillDetail.Rows)
                {
                    Label lblParticularId = gr.FindControl("lblParticularId") as Label;
                    //Label lblFee = gr.FindControl("lblFee") as Label;
                    Label lblQty = gr.FindControl("lblQty") as Label;
                    Label lblTotal = gr.FindControl("lblTotal") as Label;
                    Label lblRemarks = gr.FindControl("lblRemarks") as Label;

                    BEnt = new bill();
                    BEnt.MBILL_ID = billmasterid;
                    BEnt.PARTICULARS = lblParticularId.Text;
                    //BEnt.FEE = lblFee.Text;
                    BEnt.QUANTITY = lblQty.Text;
                    BEnt.TOTAL = lblTotal.Text;
                    BEnt.STATUS = "0";
                    BEnt.REMARKS = lblRemarks.Text;
                    BSer.Insert(BEnt, DT);
                }

                #endregion

                #region to insert in student_account

                SAEnt = new STUDENT_ACCOUNT();
                SAEnt.STUDENT_ID = cs.STUDENT_ID;
                SAEnt.MBILL_ID = billmasterid;
                SAEnt.BILL_AMOUNT = MBEnt.GRANDTOTAL;

                SASer.Insert(SAEnt, DT);

                #endregion

                #region to update status of student_pay_schedule to 1, # Status: 1 is bill generated


                if (ddlFromInst.SelectedValue != ddlToInst.SelectedValue)
                {
                    for (int i = Convert.ToInt32(ddlFromInst.SelectedValue); i <= Convert.ToInt32(ddlToInst.SelectedValue); i++)
                    {
                        SPEnt = new STUDENT_PAY_SCHEDULE();
                        EntityList theNewList = new EntityList();
                        SPEnt.STUDENT_ID = cs.STUDENT_ID;
                        SPEnt.INSTALLMENT_NO = i.ToString();

                        theNewList = SPSer.GetAll(SPEnt, DT);

                        foreach (STUDENT_PAY_SCHEDULE sp in theNewList)
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
                    SPEnt.STUDENT_ID = cs.STUDENT_ID;
                    SPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;

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
        BPEnt.SEMESTER = ddlSemester2.SelectedValue;
        if (ddlFromInst.SelectedValue != ddlToInst.SelectedValue)
        {
            BPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue + "-" + ddlToInst.SelectedValue;
        }
        else if (ddlFromInst.SelectedValue == ddlToInst.SelectedValue)
        {
            BPEnt.INSTALLMENT_NO = ddlFromInst.SelectedValue;
        }

        BPEnt.PROGRAM = ddlProgram2.SelectedValue;

        BPSer.Insert(BPEnt, DT);
        #endregion

        #region to insert in billstatus table and to insert the formonth in masterbill


        for (int j = Convert.ToInt32(ddlFromInst.SelectedValue); j <= Convert.ToInt32(ddlToInst.SelectedValue); j++)
        {
            BLSEnt = new billstatus();

            BLSEnt.BATCH = ddlBatch.SelectedValue;

            BLSEnt.FISCALYEAR = hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear());
            BLSEnt.SEMESTER = ddlSemester2.SelectedValue;
            BLSEnt.INSTALLMENT_NO = j.ToString();
            BLSEnt.PROGRAM = ddlProgram2.SelectedValue;
            BLSSer.Insert(BLSEnt, DT);
        }

        #endregion

        #region to update status in pay_schedule_installment
        int fromInst, toInst;
        fromInst = Convert.ToInt32(ddlFromInst.SelectedValue);
        toInst = Convert.ToInt32(ddlToInst.SelectedValue);
        for (; fromInst <= toInst; fromInst++)
        {
            PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
            PSIEnt.PROGRAMID = ddlProgram2.SelectedValue;
            PSIEnt.SEMESTERID = ddlSemester2.SelectedValue;
            PSIEnt.BATCH = ddlBatch2.SelectedValue;
            PSIEnt.INST_NO = fromInst.ToString();
            PSIEnt = (PAY_SCHEDULE_INSTALLMENT)PSISer.GetSingle(PSIEnt);
            if (PSIEnt != null)
            {
                PSIEnt.BILL_STATUS = "1";
                PSISer.Update(PSIEnt, DT);
            }
        }

        #endregion

        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Bill Generated Successfully");
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Bill Not Generated");
        }

        DT.Dispose();
        btnGenerate.Visible = false;
    }

    //protected void gridBillDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblParticularId = e.Row.FindControl("lblParticularId") as Label;
    //        Label lblParticularName = e.Row.FindControl("lblParticularName") as Label;
    //        PMEnt = new Particulars_Main();
    //        PMEnt.MAIN_ID = lblParticularId.Text;
    //        PMEnt = (Particulars_Main)PMSer.GetSingle(PMEnt);
    //        if (PMEnt != null)
    //            lblParticularName.Text = PMEnt.PARTICULAR_NAME;
    //    }

    //}

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadInstallment(ddlSemester.SelectedValue);
    }
    protected void gridFineCalc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

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


        ddlStudent.DataSource = hf.selectstudentinfo(ddlProgram1.SelectedValue, ddlBatch1.SelectedValue, ddlSemester1.SelectedValue, "");
        ddlStudent.DataTextField = "student_name";
        ddlStudent.DataValueField = "STUDENT_ID";
        ddlStudent.DataBind();
        ddlStudent.Items.Insert(0, "Select");
    }
    protected void btnSaveContinue_Click(object sender, EventArgs e)
    {
        Boolean flag = false;
        if (ddlFaculty.SelectedValue != "Select" && ddlProgram.SelectedValue != "Select" && ddlBatch1.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")
        {
            ddlFaculty1.SelectedValue = ddlFaculty.SelectedValue;
            ddlFaculty2.SelectedValue = ddlFaculty.SelectedValue;

            ddlProgram1.SelectedValue = ddlProgram.SelectedValue;
            ddlProgram2.SelectedValue = ddlProgram.SelectedValue;

            ddlBatch2.SelectedValue = ddlBatch1.SelectedValue;
            ddlBatch.SelectedValue = ddlBatch1.SelectedValue;

            ddlSemester1.SelectedValue = ddlSemester.SelectedValue;
            ddlSemester2.SelectedValue = ddlSemester.SelectedValue;

            LoadStudentName();
            LoadInstallment(ddlSemester.SelectedValue);
            DistributedTransaction DT = new DistributedTransaction();
            foreach (GridViewRow gr in gridFineCalc.Rows)
            {
                Label lblStudentId = gr.FindControl("lblStudentId") as Label;

                TextBox txtFine = gr.FindControl("txtFine") as TextBox;

                if (txtFine.Text != "" && txtFine.Text != "0")
                {
                    try
                    {
                        double amount = Convert.ToDouble(txtFine.Text);

                        SPEnt = new STUDENT_PAY_SCHEDULE();
                        SPEnt.STUDENT_ID = lblStudentId.Text;
                        //SPEnt.INSTALLMENT_NO = hf.getinstallmentno_fine(lblStudentId.Text);
                        SPEnt.INSTALLMENT_NO = "2";
                        SPEnt.AMOUNT = txtFine.Text;
                        SPEnt.STATUS = "0";
                        SPEnt.DISCOUNT = "0";
                        SPEnt.SEMESTER = ddlSemester.SelectedValue;
                        PMEnt = new Particulars_Main();
                        PMEnt.PARTICULAR_NAME = "Fine";
                        PMEnt = (Particulars_Main)PMSer.GetSingle(PMEnt, DT);
                        if (PMEnt != null)
                        {
                            SPEnt.PARTICULARS = PMEnt.MAIN_ID;
                        }

                        SPSer.Insert(SPEnt, DT);
                    }
                    catch
                    {
                        DT.HAPPY = false;
                        flag = true;

                    }
                }
            }
            if (DT.HAPPY == true)
            {
                DT.Commit();
                fine_div.Visible = false;
                misc_div.Visible = true;
            }
            else
            {
                DT.Abort();
                if (flag == true)
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Enter Number only in Fine Field");
                }
                else
                {

                    HelperFunction.MsgBox(this, this.GetType(), "Sorry something goes wrong");
                }
            }
            DT.Dispose();
        }
        else
        {

            HelperFunction.MsgBox(this, this.GetType(), "Please Select Faculty, Program, Batch and Semester");
        }



    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select" && ddlProgram.SelectedValue != "Select" && ddlBatch1.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")
        {
            LoadFineCalc();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Faculty, Program, Batch and Semester");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridMisc.HeaderRow;
        TextBox txtParticularH = (TextBox)row.FindControl("txtParticularH");
        TextBox txtAmountH = (TextBox)row.FindControl("txtAmountH");

        if (ddlStudent.SelectedValue != "Select" && ddlInstallment.SelectedValue != "Select")
        {
            try
            {
                double amount = Convert.ToDouble(txtAmountH.Text);

                SPEnt = new STUDENT_PAY_SCHEDULE();
                SPEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SPEnt.INSTALLMENT_NO = ddlInstallment.SelectedValue;
                SPEnt.SEMESTER = ddlSemester1.SelectedValue;

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
                PSIEnt.INST_NO = ddlInstallment.SelectedValue;
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
    protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStudent.SelectedValue != "Select" && ddlInstallment.SelectedValue != "Select")
        {
            LoadMisc();
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        misc_div.Visible = false;
        billgen_div.Visible = true;
        getForMonth();
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
    protected void txtToInst_TextChanged(object sender, EventArgs e)
    {
        getForMonth();
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

            ddlBatch1.Items.Clear();
            ddlBatch1.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }

        if (ddlProgram.SelectedValue == "Select")
        {
            ddlBatch1.Items.Clear();
            ddlBatch1.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();
            //loadBatch();

        }
        else
        {
            ddlBatch1.Items.Clear();
            ddlBatch1.Items.Insert(0, "Select");


            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }

    }
    protected void ddlBatch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch1.SelectedValue != "Select")
        {
            LoadSemester();

        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");



        }
    }
    protected void ddlInstallment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStudent.SelectedValue != "Select" && ddlInstallment.SelectedValue != "Select")
        {
            LoadMisc();
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
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBatch();
    }


    protected void gridBillDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblParticularId = e.Row.FindControl("lblParticularId") as Label;
            Label lblParticular = e.Row.FindControl("lblParticular") as Label;

            PMEnt = new Particulars_Main();
            PMEnt.MAIN_ID = lblParticularId.Text;

            PMEnt = (Particulars_Main)PMSer.GetSingle(PMEnt);
            if (PMEnt != null)
            {
                lblParticular.Text = PMEnt.PARTICULAR_NAME;
            }


        }

    }
}