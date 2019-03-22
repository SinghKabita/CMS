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

public partial class finance_OneTimeEntry : System.Web.UI.Page
{

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    FEE_STRUCTURE FSEnt = new FEE_STRUCTURE();
    FEE_STRUCTUREService FSSer = new FEE_STRUCTUREService();

    STUDENT_PAY_SCHEDULE SPSEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSSer = new STUDENT_PAY_SCHEDULEService();

    PAY_SCHEDULE_INSTALLMENT PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
    PAY_SCHEDULE_INSTALLMENTService PSISrv = new PAY_SCHEDULE_INSTALLMENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    DistributedTransaction DT = new DistributedTransaction();

    HelperFunction hf = new HelperFunction();

    EntityList mparticularList = new EntityList();

    EntityList theList = new EntityList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            //LoadBatch();
            loadOneTimeParticular();
            //LoadMonth();
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

    protected void LoadInstallmentNo()
    {
        theList = new EntityList();

        PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
        PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
        PSIEnt.BATCH = ddlBatch.SelectedValue;
        PSIEnt.SEMESTERID = ddlSemester.SelectedValue;
        theList = PSISrv.GetAll(PSIEnt);
        EntityList newList = new EntityList();

        if (theList.Count != 0)
        {
            foreach (PAY_SCHEDULE_INSTALLMENT psy in theList)
            {
                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.INSTALLMENT_NO = psy.INST_NO;
                if (SPSEnt != null)
                {
                    newList.Add(psy);

                    ddlInstallment.DataSource = newList;
                    ddlInstallment.DataTextField = "INST_NO";
                    ddlInstallment.DataValueField = "INST_NO";
                    ddlInstallment.DataBind();
                    ddlInstallment.Items.Insert(0, "Select");
                }
                //gridParticulars.Visible = true;
                //btnSave.Visible = true;
            }

        }
        else
        {
            newList = new EntityList();
            ddlInstallment.DataSource = newList;
            ddlInstallment.DataTextField = "INST_NO";
            ddlInstallment.DataValueField = "INST_NO";
            ddlInstallment.DataBind();
            ddlInstallment.Items.Insert(0, "Select");
            txtMonth.Text = "";
            txtYear.Text = "";
            chkParticular.ClearSelection();

            gridParticulars.Visible = false;
            btnSave.Visible = false;

        }

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
    }

    protected void LoadMonth()
    {

        PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
        PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
        PSIEnt.BATCH = ddlBatch.SelectedValue;
        PSIEnt.SEMESTERID = ddlSemester.SelectedValue;
        //PSIEnt.INST_MONTH = ddlInstallment.SelectedValue;
        theList = PSISrv.GetAll(PSIEnt);
        foreach (PAY_SCHEDULE_INSTALLMENT psy in theList)
        {
            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.INSTALLMENT_NO = ddlInstallment.SelectedValue;
            SPSEnt.SEMESTER = ddlSemester.SelectedValue;
            SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt);
            if (SPSEnt != null)
            {
                txtMonth.Text = SPSEnt.FOR_MONTH;
                txtYear.Text = SPSEnt.YEAR;
            }
        }
    }
    protected void loadOneTimeParticular()
    {
        PMEnt = new Particulars_Main();
        PMEnt.ONETIME = "1";
        chkParticular.DataSource = PMSer.GetAll(PMEnt);
        chkParticular.DataTextField = "PARTICULAR_NAME";
        chkParticular.DataValueField = "MAIN_ID";
        chkParticular.DataBind();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in chkParticular.Items)
        {
            if (item.Selected)
            {
                PMEnt = new Particulars_Main();

                PMEnt.MAIN_ID = item.Value;
                PMEnt = (Particulars_Main)PMSer.GetSingle(PMEnt);

                if (PMEnt != null)
                {

                    mparticularList.Add(PMEnt);
                }
            }

        }

        gridParticulars.DataSource = mparticularList;
        gridParticulars.DataBind();

        btnSave.Visible = true;

    }
    protected void gridParticulars_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblParticularId = e.Row.FindControl("lblParticularId") as Label;
            TextBox txtAmount = e.Row.FindControl("txtAmount") as TextBox;
            Label lblParticularName = e.Row.FindControl("lblParticularName") as Label;
            DropDownList ddlInstallment = e.Row.FindControl("ddlInstallment") as DropDownList;


            FSEnt = new FEE_STRUCTURE();
            FSEnt.PARTICULAR_ID = lblParticularId.Text;
            FSEnt.BATCH = ddlBatch.SelectedValue;
            FSEnt.PROGRAM = ddlProgram.SelectedValue;
            FSEnt.SEMESTER = ddlSemester.SelectedValue;
            FSEnt = (FEE_STRUCTURE)FSSer.GetSingle(FSEnt);
            if (FSEnt != null)
            {
                txtAmount.Text = FSEnt.AMOUNT;
            }


            PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
            PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
            PSIEnt.BATCH = ddlBatch.SelectedValue;
            PSIEnt.SEMESTERID = ddlSemester.SelectedValue;

            if (lblParticularName.Text == "Miscellaneous")
            {
                txtAmount.Enabled = true;
            }
            else
            {
                txtAmount.Enabled = false;
            }


        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DT = new DistributedTransaction();
        EntityList theList = new EntityList();
        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.BATCH = ddlBatch.SelectedValue;
        CSEnt.STATUS = "1";
        theList = CSSer.GetAll(CSEnt);

        foreach (HSS_CURRENT_STUDENT cs in theList)
        {
            foreach (GridViewRow gr in gridParticulars.Rows)
            {
                Label lblParticularId = gr.FindControl("lblParticularId") as Label;
                TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = cs.STUDENT_ID;
                SPSEnt.INSTALLMENT_NO = ddlInstallment.SelectedValue;
                SPSEnt.PARTICULARS = lblParticularId.Text;
                SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt, DT);
                if (SPSEnt == null)
                {
                    if (txtAmount.Text != "")
                    {
                        SPSEnt = new STUDENT_PAY_SCHEDULE();
                        SPSEnt.STUDENT_ID = cs.STUDENT_ID;
                        SPSEnt.INSTALLMENT_NO = ddlInstallment.SelectedValue;
                        SPSEnt.PARTICULARS = lblParticularId.Text;
                        SPSEnt.AMOUNT = txtAmount.Text;
                        SPSEnt.DISCOUNT = "0";
                        SPSEnt.FOR_MONTH = txtMonth.Text;
                        SPSEnt.STATUS = "0";
                        SPSEnt.SEMESTER = ddlSemester.SelectedValue;
                        SPSEnt.YEAR = txtYear.Text;

                        SPSSer.Insert(SPSEnt, DT);
                    }
                }
            }
        }

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
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
    protected void ddlInstallment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMonth();
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
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadInstallmentNo();

    }
}