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


public partial class finance_FeeStructure : System.Web.UI.Page
{

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

    FEE_STRUCTURE FSEnt = new FEE_STRUCTURE();
    FEE_STRUCTUREService FSSer = new FEE_STRUCTUREService();

    EntityList theList = new EntityList();

    DistributedTransaction DT = new DistributedTransaction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            //LoadGrid();
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
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        BYEnt.BATCH = ddlBatch.SelectedValue;
        BYEnt = (BatchYear)BYSer.GetSingle(BYEnt);
        if (BYEnt != null)
        {
            SMEnt = new semester();
            SMEnt.PROGRAM_ID = BYEnt.PROGRAM;
            SMEnt.SYLLABUS_YEAR = BYEnt.SYLLABUS_YEAR;
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

    protected void LoadGrid()
    {
        EntityList theList = new EntityList();
        EntityList particularList = new EntityList();
        PMEnt = new Particulars_Main();
        theList = PMSer.GetAll(PMEnt);
        foreach (Particulars_Main pm in theList)
        {
            //if(pm.ONETIME!="2")
            //{
            //    particularList.Add(pm);
            //}



            //if (pm.ONETIME == "0")
            //{
            particularList.Add(pm);
            //}
        }


        gridParticulars.DataSource = particularList;
        gridParticulars.DataBind();

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DT = new DistributedTransaction();
        foreach (GridViewRow gr in gridParticulars.Rows)
        {
            Label lblParticularId = gr.FindControl("lblParticularId") as Label;
            Label lblPKID = gr.FindControl("lblPKID") as Label;

            TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;

            if (lblPKID.Text != "")
            {
                FSEnt = new FEE_STRUCTURE();
                FSEnt.PK_ID = lblPKID.Text;
                FSEnt = (FEE_STRUCTURE)FSSer.GetSingle(FSEnt, DT);
                if (FSEnt != null)
                {

                    FSEnt.AMOUNT = txtAmount.Text;
                    FSSer.Update(FSEnt, DT);
                }
            }
            else
            {
                if (txtAmount.Text != "")
                {
                    FSEnt = new FEE_STRUCTURE();
                    FSEnt.PROGRAM = ddlProgram.SelectedValue;
                    FSEnt.BATCH = ddlBatch.SelectedValue;
                    FSEnt.SEMESTER = ddlSemester.SelectedValue;
                    FSEnt.PARTICULAR_ID = lblParticularId.Text;
                    FSEnt.AMOUNT = txtAmount.Text;
                    FSSer.Insert(FSEnt, DT);
                }
                else
                {
                    FSEnt = new FEE_STRUCTURE();
                    FSEnt.PROGRAM = ddlProgram.SelectedValue;
                    FSEnt.BATCH = ddlBatch.SelectedValue;
                    FSEnt.SEMESTER = ddlSemester.SelectedValue;
                    FSEnt.PARTICULAR_ID = lblParticularId.Text;
                    FSEnt.AMOUNT = "0";
                    FSSer.Insert(FSEnt, DT);

                }


            }
        }
        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Successfull");
            clearFields();
            gridParticulars.Visible = false;
            btnGenerate.Visible = false;
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Save");
        }
        DT.Dispose();


    }

    protected void clearFields()
    {
        foreach (GridViewRow gr in gridParticulars.Rows)
        {

            TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;
            txtAmount.Text = "";
        }
    }

    protected void gridParticulars_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPKID = e.Row.FindControl("lblPKID") as Label;
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
                lblPKID.Text = FSEnt.PK_ID;
                txtAmount.Text = FSEnt.AMOUNT;
            }
            else
            {
                lblPKID.Text = "";
                txtAmount.Text = "";

            }
        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            LoadSemester();

        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
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
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();


    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    protected void btnView_Click(object sender, EventArgs e)
    {
        EntityList theList = new EntityList();

        PMEnt = new Particulars_Main();
        theList = PMSer.GetAll(PMEnt);
        if (theList.Count > 0)
        {
            gridParticulars.Visible = true;
            LoadGrid();
            btnGenerate.Visible = true;
        }
        else
        {
            btnGenerate.Visible = false;
        }
    }
}