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

public partial class finance_StudentPaySchedule : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

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

    HSS_STUDENT SIEnt = new HSS_STUDENT();
    HSS_STUDENTService SISer = new HSS_STUDENTService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HelperFunction hf = new HelperFunction();
    EntityList theList = new EntityList();

    DistributedTransaction DT = new DistributedTransaction();

    FEE_STRUCTURE FSEnt = new FEE_STRUCTURE();
    FEE_STRUCTUREService FSSer = new FEE_STRUCTUREService();

    STUDENT_PAY_SCHEDULE SPSEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSSer = new STUDENT_PAY_SCHEDULEService();

    PAY_SCHEDULE_INSTALLMENT PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
    PAY_SCHEDULE_INSTALLMENTService PSISrv = new PAY_SCHEDULE_INSTALLMENTService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    static double particulartotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            LoadMonth();
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
    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.ACTIVE = "1";
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }

    protected void LoadMonth()
    {

        EntityList theList = new EntityList();
        EntityList newList = new EntityList();
        MEnt = new Months();

        theList = MSer.GetAll(MEnt);
        foreach (Months m in theList)
        {
            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.INSTALLMENT_NO = txtInstallMentNo.Text;
            SPSEnt.SEMESTER = ddlSemester.SelectedValue;
            SPSEnt.FOR_MONTH = m.MONTHID;
            SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt);
            if (SPSEnt == null)
            {
                newList.Add(m);
            }
        }
        ddlStartingMonth.DataSource = newList;
        ddlStartingMonth.DataTextField = "MONTHNAME";
        ddlStartingMonth.DataValueField = "MONTHID";
        ddlStartingMonth.DataBind();

    }

    protected void LoadData()
    {

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        double totalInstallmentAmount = 0;
        double totalSemesterAmount = 0;

        foreach (GridViewRow gr in gridParticulars.Rows)
        {
            TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;

            try
            {

                totalSemesterAmount = totalSemesterAmount + Convert.ToDouble(txtAmount.Text);
            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "fee structure is not generated.Please generate fee structure.");
            }
        }


        foreach (GridViewRow gr in gridMonthSelect.Rows)
        {
            TextBox txtAmountPerInstallment = gr.FindControl("txtAmountPerInstallment") as TextBox;

            try
            {
                totalInstallmentAmount = totalInstallmentAmount + Convert.ToDouble(txtAmountPerInstallment.Text);
            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Please Enter Number only in Amount");
            }
        }


        if (totalSemesterAmount == totalInstallmentAmount)
        {

            DT = new DistributedTransaction();
            EntityList theList = new EntityList();

            //CSEnt = new HSS_CURRENT_STUDENT();
            //CSEnt.BATCH = ddlBatch.SelectedValue;
            //CSEnt.STATUS = "1";
            //theList = CSSer.GetAll(CSEnt);

            SIEnt = new HSS_STUDENT();
            SIEnt.BAT_CH = ddlBatch.SelectedValue;
            SIEnt.PROGRAM = ddlProgram.SelectedValue;
            SIEnt.STATUS = "1";
            theList = SISer.GetAll(SIEnt);

            foreach (HSS_STUDENT hs in theList)
            {
                foreach (GridViewRow gr in gridParticulars.Rows)
                {
                    Label lblParticularId = gr.FindControl("lblParticularId") as Label;

                    foreach (GridViewRow grd in gridMonthSelect.Rows)
                    {
                        Label lblInstallmentNo = grd.FindControl("lblInstallmentNo") as Label;
                        DropDownList ddlMonth = grd.FindControl("ddlMonth") as DropDownList;
                        TextBox txtYear = grd.FindControl("txtYear") as TextBox;

                        TextBox txtAmountPerInstallment = grd.FindControl("txtAmountPerInstallment") as TextBox;

                        SPSEnt = new STUDENT_PAY_SCHEDULE();
                        SPSEnt.STUDENT_ID = hs.STUDENT_ID;
                        //SPSEnt.INSTALLMENT_NO = lblInstallmentNo.Text;
                        SPSEnt.INSTALLMENT_NO = (Convert.ToDouble(hf.getCumInstNo(ddlProgram.SelectedValue, ddlBatch.SelectedValue)) + Convert.ToDouble(lblInstallmentNo.Text)).ToString();
                        SPSEnt.PARTICULARS = lblParticularId.Text;
                        SPSEnt.FOR_MONTH = ddlMonth.SelectedItem.ToString();
                        SPSEnt.SEMESTER = ddlSemester.SelectedValue;
                        SPSEnt.YEAR = txtYear.Text;
                        SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt, DT);
                        if (SPSEnt == null)
                        {
                            SPSEnt = new STUDENT_PAY_SCHEDULE();
                            SPSEnt.STUDENT_ID = hs.STUDENT_ID;
                            //SPSEnt.INSTALLMENT_NO = lblInstallmentNo.Text;
                            SPSEnt.INSTALLMENT_NO = (Convert.ToDouble(hf.getCumInstNo(ddlProgram.SelectedValue, ddlBatch.SelectedValue)) + Convert.ToDouble(lblInstallmentNo.Text)).ToString();
                            SPSEnt.PARTICULARS = lblParticularId.Text;
                            SPSEnt.AMOUNT = txtAmountPerInstallment.Text;
                            SPSEnt.STATUS = "0";
                            SPSEnt.DISCOUNT = "0";
                            SPSEnt.FOR_MONTH = ddlMonth.SelectedItem.ToString();
                            SPSEnt.SEMESTER = ddlSemester.SelectedValue;
                            SPSEnt.YEAR = txtYear.Text;
                            SPSSer.Insert(SPSEnt, DT);
                        }
                    }
                }
            }

            #region to insert in pay_schedule_installment
            foreach (GridViewRow grd in gridMonthSelect.Rows)
            {
                Label lblInstallmentNo = grd.FindControl("lblInstallmentNo") as Label;
                DropDownList ddlMonth = grd.FindControl("ddlMonth") as DropDownList;
                TextBox txtYear = grd.FindControl("txtYear") as TextBox;
                TextBox txtAmountPerInstallment = grd.FindControl("txtAmountPerInstallment") as TextBox;

                PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
                PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
                PSIEnt.BATCH = ddlBatch.SelectedValue;
                PSIEnt.SEMESTERID = ddlSemester.SelectedValue;
                PSIEnt.INST_NO = lblInstallmentNo.Text;
                PSIEnt.INST_MONTH = ddlMonth.SelectedValue;
                PSIEnt.INST_AMOUNT = txtAmountPerInstallment.Text;
                PSIEnt.YEAR = txtYear.Text;
                PSIEnt.BILL_STATUS = "0";
                PSIEnt.CUM_INST_NO = (Convert.ToDouble(hf.getCumInstNo(ddlProgram.SelectedValue, ddlBatch.SelectedValue)) + Convert.ToDouble(lblInstallmentNo.Text)).ToString();

                PSISrv.Insert(PSIEnt, DT);
            }
            #endregion

            if (DT.HAPPY == true)
            {
                DT.Commit();
                HelperFunction.MsgBox(this, this.GetType(), "Successful");
            }
            else
            {
                DT.Abort();
                HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved");
            }
            DT.Dispose();
            btnGenerate.Visible = false;
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Amount Not Matched.");

        }
    }

    protected string getForMonthofInstallment(string installmentno)
    {
        string monthname = "";
        foreach (GridViewRow gr in gridMonthSelect.Rows)
        {
            Label lblInstallmentNo = gr.FindControl("lblInstallmentNo") as Label;
            DropDownList ddlMonth = gr.FindControl("ddlMonth") as DropDownList;

            if (lblInstallmentNo.Text == installmentno)
            {
                monthname = ddlMonth.SelectedItem.ToString();
            }
        }

        return monthname;
    }

    protected void btnShowParticular_Click(object sender, EventArgs e)
    {
        ddlStartingMonth.SelectedIndex = 0;
        txtStartingYear.Text = "";
        PMEnt = new Particulars_Main();
        PMEnt.ONETIME = "0";
        gridParticulars.DataSource = PMSer.GetAll(PMEnt);
        gridParticulars.DataBind();

        particulartotal = 0;

        foreach (GridViewRow gr in gridParticulars.Rows)
        {
            TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;
            if (txtAmount.Text != "")
            {
                particulartotal = particulartotal + Convert.ToDouble(txtAmount.Text);
            }
        }

        ArrayList alist = new ArrayList();
        for (int i = 1; i <= Convert.ToInt32(txtInstallMentNo.Text); i++)
        {
            alist.Add(1);
        }

        gridMonthSelect.DataSource = alist;
        gridMonthSelect.DataBind();

        btnGenerate.Visible = true;
        tr_monthselection.Visible = true;

        gridParticulars.Visible = true;
        gridMonthSelect.Visible = true;
    }

    protected void gridParticulars_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblParticularId = e.Row.FindControl("lblParticularId") as Label;
            TextBox txtAmount = e.Row.FindControl("txtAmount") as TextBox;
            FSEnt = new FEE_STRUCTURE();
            FSEnt.PROGRAM = ddlProgram.SelectedValue;
            FSEnt.BATCH = ddlBatch.SelectedValue;
            FSEnt.SEMESTER = ddlSemester.SelectedValue;
            FSEnt.PARTICULAR_ID = lblParticularId.Text;

            FSEnt = (FEE_STRUCTURE)FSSer.GetSingle(FSEnt);
            if (FSEnt != null)
            {
                txtAmount.Text = FSEnt.AMOUNT;
            }

        }
    }
    protected void gridMonthSelect_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMonth = e.Row.FindControl("ddlMonth") as DropDownList;
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;

            MEnt = new Months();
            ddlMonth.DataSource = MSer.GetAll(MEnt);
            ddlMonth.DataTextField = "MONTHNAME";
            ddlMonth.DataValueField = "MONTHID";
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, "Select");

            // lblAmount.Text = particulartotal.ToString();

        }
    }
    protected void ddlStartingMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStartingMonth.SelectedValue != "Select" && txtStartingYear.Text != "")
        {
            LoadMonthYear();
        }
    }

    protected void txtStartingYear_TextChanged(object sender, EventArgs e)
    {
        if (ddlStartingMonth.SelectedValue != "Select" && txtStartingYear.Text != "")
        {
            LoadMonthYear();
        }
    }

    protected void LoadMonthYear()
    {
        foreach (GridViewRow gr in gridMonthSelect.Rows)
        {
            DropDownList ddlMonth = gr.FindControl("ddlMonth") as DropDownList;
            TextBox txtYear = gr.FindControl("txtYear") as TextBox;

            if (ddlStartingMonth.SelectedValue != "Select")
            {
                if (gr.RowIndex == 0)
                {
                    ddlMonth.SelectedValue = ddlStartingMonth.SelectedValue;
                    txtYear.Text = txtStartingYear.Text;
                }
                else
                {
                    GridViewRow row = gridMonthSelect.Rows[gr.RowIndex - 1];
                    DropDownList ddlMonth1 = row.FindControl("ddlMonth") as DropDownList;
                    TextBox txtYear1 = row.FindControl("txtYear") as TextBox;

                    int prevmonth = Convert.ToInt32(ddlMonth1.SelectedValue);
                    int prevyear = Convert.ToInt32(txtYear1.Text);

                    int currentmonth = prevmonth + 1;
                    if (currentmonth <= 12)
                    {

                        ddlMonth.SelectedValue = (currentmonth).ToString();
                        txtYear.Text = txtYear1.Text;
                    }
                    else
                    {
                        ddlMonth.SelectedValue = (1).ToString();
                        txtYear.Text = (prevyear + 1).ToString();

                    }
                }

            }
        }
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
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadBatch();
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
        LoadMonth();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {

        PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
        PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
        PSIEnt.BATCH = ddlBatch.SelectedValue;
        PSIEnt.SEMESTERID = ddlSemester.SelectedValue;
        theList = PSISrv.GetAll(PSIEnt);
        if (theList.Count != 0)
        {

            gridInstallment.DataSource = PSISrv.GetAll(PSIEnt);
            gridInstallment.DataBind();

            gridInstallment.Visible = true;
            InstNo.Visible = false;
            ShowBtn.Visible = false;
            gridParticulars.Visible = false;
            gridMonthSelect.Visible = false;
            tr_monthselection.Visible = false;
            btnGenerate.Visible = false;
        }
        else
        {
            gridInstallment.Visible = false;
            InstNo.Visible = true;
            ShowBtn.Visible = true;
            gridParticulars.Visible = false;
            gridMonthSelect.Visible = false;
            tr_monthselection.Visible = false;
            btnGenerate.Visible = false;
            txtInstallMentNo.Text = "";

        }


    }

    protected void gridInstallment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;

            MEnt = new Months();
            MEnt.MONTHID = lblMonth.Text;
            MEnt = (Months)MSer.GetSingle(MEnt);
            if (MEnt != null)
            {
                lblMonthName.Text = MEnt.MONTHNAME;
            }

        }

    }
}