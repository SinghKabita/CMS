using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class finance_report_SemDetailCollection : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    Receipt REnt = new Receipt();
    ReceiptService RSer = new ReceiptService();

    RECEIPT_DETAIL RDEnt = new RECEIPT_DETAIL();
    RECEIPT_DETAILService RDSer = new RECEIPT_DETAILService();

    STUDENT_PAY_SCHEDULE SPSEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSSer = new STUDENT_PAY_SCHEDULEService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    masterbill MBEnt = new masterbill();
    masterbillService MBSer = new masterbillService();

    HelperFunction hf = new HelperFunction();
    double paidAmt = 0.0;

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

    protected void loadGrid()
    {
        EntityList thelist = new EntityList();
        EntityList newlist = new EntityList();
        STDEnt = new HSS_STUDENT();
        STDEnt.PROGRAM = ddlProgram.SelectedValue;
        STDEnt.BAT_CH = ddlBatch.SelectedValue;
        STDEnt.STATUS = "1";
        thelist = STDSer.GetAll(STDEnt);
        if (thelist.Count > 0)
        {
            foreach (HSS_STUDENT std in thelist)
            {
                REnt = new Receipt();
                REnt.STUDENTID = std.STUDENT_ID;
                REnt = (Receipt)RSer.GetSingle(REnt);
                if (REnt != null)
                    newlist.Add(REnt);
            }
            gridSemDetailCollection.DataSource = newlist;
            gridSemDetailCollection.DataBind();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Student not Found");
        }

    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void CalculateSemAmt(string stdid, string sem)
    {
        paidAmt = 0.0;
        EntityList thelist = new EntityList();
        EntityList semAmtList = new EntityList();
        REnt = new Receipt();
        REnt.STUDENTID = stdid;
        thelist = RSer.GetAll(REnt);
        if (thelist.Count > 0)
        {
            foreach (Receipt re in thelist)
            {
                RDEnt = new RECEIPT_DETAIL();
                RDEnt.RECEIPT_SNO = re.SNO;
                RDEnt.SEMESTER = sem;
                semAmtList = RDSer.GetAll(RDEnt);
                if (semAmtList.Count > 0)
                {
                    foreach (RECEIPT_DETAIL rd in semAmtList)
                    {
                        paidAmt = paidAmt + Convert.ToDouble(rd.AMOUNT);
                    }
                }

            }
        }

    }

    protected void gridSemDetailCollection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region header
        if (e.Row.RowType == DataControlRowType.Header)
        {

        }
        #endregion

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentID = e.Row.FindControl("lblStudentID") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            STDEnt = new HSS_STUDENT();
            STDEnt.STUDENT_ID = lblStudentID.Text;
            STDEnt.STATUS = "1";
            STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
            if (STDEnt != null)
            {
                lblStudentName.Text = STDEnt.NAME_ENGLISH;
            }

            #region 1st sem
            Label lbl1stSemPK = e.Row.FindControl("lbl1stSemPK") as Label;
            Label lbl1stSemAmt = e.Row.FindControl("lbl1stSemAmt") as Label;
            Label lbl1stSemDisc = e.Row.FindControl("lbl1stSemDisc") as Label;
            Label lbl1stSemPaidAmt = e.Row.FindControl("lbl1stSemPaidAmt") as Label;

            EntityList sem1List = new EntityList();
            double sem1Amt = 0.0;
            double sem1Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "1";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl1stSemPK.Text = SMEnt.PK_ID;
                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem1List = SPSSer.GetAll(SPSEnt);
                if (sem1List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem1List)
                    {
                        sem1Amt = sem1Amt + Convert.ToDouble(sps.AMOUNT);
                        sem1Dis = sem1Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl1stSemAmt.Text = sem1Amt.ToString();
                    lbl1stSemDisc.Text = sem1Dis.ToString();
                }
                else
                {
                    lbl1stSemAmt.Text = "N/A";
                    lbl1stSemDisc.Text = "N/A";
                }

                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, lbl1stSemPK.Text);
                lbl1stSemPaidAmt.Text = paidAmt.ToString();
                #endregion
            }



            #endregion

            #region 2nd sem
            Label lbl2ndSemPK = e.Row.FindControl("lbl2ndSemPK") as Label;
            Label lbl2ndSemAmt = e.Row.FindControl("lbl2ndSemAmt") as Label;
            Label lbl2ndSemDisc = e.Row.FindControl("lbl2ndSemDisc") as Label;
            Label lbl2ndSemPaidAmt = e.Row.FindControl("lbl2ndSemPaidAmt") as Label;

            EntityList sem2List = new EntityList();
            double sem2Amt = 0.0;
            double sem2Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "2";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl2ndSemPK.Text = SMEnt.PK_ID;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem2List = SPSSer.GetAll(SPSEnt);
                if (sem2List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem2List)
                    {
                        sem2Amt = sem2Amt + Convert.ToDouble(sps.AMOUNT);
                        sem2Dis = sem2Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl2ndSemAmt.Text = sem2Amt.ToString();
                    lbl2ndSemDisc.Text = sem2Dis.ToString();
                }


                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, SMEnt.PK_ID);
                lbl2ndSemPaidAmt.Text = paidAmt.ToString();
                #endregion

            }
            #endregion

            #region 3rd sem

            Label lbl3rdSemPK = e.Row.FindControl("lbl3rdSemPK") as Label;
            Label lbl3rdSemAmt = e.Row.FindControl("lbl3rdSemAmt") as Label;
            Label lbl3rdSemDisc = e.Row.FindControl("lbl3rdSemDisc") as Label;
            Label lbl3rdSemPaidAmt = e.Row.FindControl("lbl3rdSemPaidAmt") as Label;

            EntityList sem3List = new EntityList();
            double sem3Amt = 0.0;
            double sem3Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "3";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl3rdSemPK.Text = SMEnt.PK_ID;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem3List = SPSSer.GetAll(SPSEnt);
                if (sem3List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem3List)
                    {
                        sem3Amt = sem3Amt + Convert.ToDouble(sps.AMOUNT);
                        sem3Dis = sem3Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl3rdSemAmt.Text = sem3Amt.ToString();
                    lbl3rdSemDisc.Text = sem3Dis.ToString();
                }


                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, SMEnt.PK_ID);
                lbl3rdSemPaidAmt.Text = paidAmt.ToString();
                #endregion
            }
            #endregion

            #region 4th sem
            Label lbl4thSemPK = e.Row.FindControl("lbl4thSemPK") as Label;
            Label lbl4thSemAmt = e.Row.FindControl("lbl4thSemAmt") as Label;
            Label lbl4thSemDisc = e.Row.FindControl("lbl4thSemDisc") as Label;
            Label lbl4thSemPaidAmt = e.Row.FindControl("lbl4thSemPaidAmt") as Label;

            EntityList sem4List = new EntityList();
            double sem4Amt = 0.0;
            double sem4Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "4";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl4thSemPK.Text = SMEnt.PK_ID;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem4List = SPSSer.GetAll(SPSEnt);
                if (sem4List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem4List)
                    {
                        sem4Amt = sem4Amt + Convert.ToDouble(sps.AMOUNT);
                        sem4Dis = sem4Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl4thSemAmt.Text = sem4Amt.ToString();
                    lbl4thSemDisc.Text = sem4Dis.ToString();
                }


                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, SMEnt.PK_ID);
                lbl4thSemPaidAmt.Text = paidAmt.ToString();
                #endregion
            }
            #endregion

            #region 5th sem
            Label lbl5thSemPK = e.Row.FindControl("lbl5thSemPK") as Label;
            Label lbl5thSemAmt = e.Row.FindControl("lbl5thSemAmt") as Label;
            Label lbl5thSemDisc = e.Row.FindControl("lbl5thSemDisc") as Label;
            Label lbl5thSemPaidAmt = e.Row.FindControl("lbl5thSemPaidAmt") as Label;

            EntityList sem5List = new EntityList();
            double sem5Amt = 0.0;
            double sem5Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "5";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl5thSemPK.Text = SMEnt.PK_ID;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem5List = SPSSer.GetAll(SPSEnt);
                if (sem5List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem5List)
                    {
                        sem5Amt = sem5Amt + Convert.ToDouble(sps.AMOUNT);
                        sem5Dis = sem5Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl5thSemAmt.Text = sem5Amt.ToString();
                    lbl5thSemDisc.Text = sem5Dis.ToString();
                }

                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, SMEnt.PK_ID);
                lbl5thSemPaidAmt.Text = paidAmt.ToString();
                #endregion
            }
            #endregion

            #region 6th sem
            Label lbl6thSemPK = e.Row.FindControl("lbl6thSemPK") as Label;
            Label lbl6thSemAmt = e.Row.FindControl("lbl6thSemAmt") as Label;
            Label lbl6thSemDisc = e.Row.FindControl("lbl6thSemDisc") as Label;
            Label lbl6thSemPaidAmt = e.Row.FindControl("lbl6thSemPaidAmt") as Label;

            EntityList sem6List = new EntityList();
            double sem6Amt = 0.0;
            double sem6Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "6";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl6thSemPK.Text = SMEnt.PK_ID;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem6List = SPSSer.GetAll(SPSEnt);
                if (sem6List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem6List)
                    {
                        sem6Amt = sem6Amt + Convert.ToDouble(sps.AMOUNT);
                        sem6Dis = sem6Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl6thSemAmt.Text = sem6Amt.ToString();
                    lbl6thSemDisc.Text = sem6Dis.ToString();
                }

                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, SMEnt.PK_ID);
                lbl6thSemPaidAmt.Text = paidAmt.ToString();
                #endregion
            }
            #endregion

            #region 7th sem
            Label lbl7thSemPK = e.Row.FindControl("lbl7thSemPK") as Label;
            Label lbl7thSemAmt = e.Row.FindControl("lbl7thSemAmt") as Label;
            Label lbl7thSemDisc = e.Row.FindControl("lbl7thSemDisc") as Label;
            Label lbl7thSemPaidAmt = e.Row.FindControl("lbl7thSemPaidAmt") as Label;

            EntityList sem7List = new EntityList();
            double sem7Amt = 0.0;
            double sem7Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "7";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl7thSemPK.Text = SMEnt.PK_ID;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem7List = SPSSer.GetAll(SPSEnt);
                if (sem7List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem7List)
                    {
                        sem7Amt = sem7Amt + Convert.ToDouble(sps.AMOUNT);
                        sem7Dis = sem7Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl7thSemAmt.Text = sem7Amt.ToString();
                    lbl7thSemDisc.Text = sem7Dis.ToString();
                }

                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, SMEnt.PK_ID);
                lbl7thSemPaidAmt.Text = paidAmt.ToString();
                #endregion
            }
            #endregion

            #region 8th sem
            Label lbl8thSemPK = e.Row.FindControl("lbl8thSemPK") as Label;
            Label lbl8thSemAmt = e.Row.FindControl("lbl8thSemAmt") as Label;
            Label lbl8thSemDisc = e.Row.FindControl("lbl8thSemDisc") as Label;
            Label lbl8thSemPaidAmt = e.Row.FindControl("lbl8thSemPaidAmt") as Label;

            EntityList sem8List = new EntityList();
            double sem8Amt = 0.0;
            double sem8Dis = 0.0;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SMEnt.SEMESTER = "8";
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lbl8thSemPK.Text = SMEnt.PK_ID;

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentID.Text;
                SPSEnt.SEMESTER = SMEnt.PK_ID;
                sem8List = SPSSer.GetAll(SPSEnt);
                if (sem8List.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in sem8List)
                    {
                        sem8Amt = sem8Amt + Convert.ToDouble(sps.AMOUNT);
                        sem8Dis = sem8Dis + Convert.ToDouble(sps.DISCOUNT);
                    }

                    lbl8thSemAmt.Text = sem8Amt.ToString();
                    lbl8thSemDisc.Text = sem8Dis.ToString();
                }

                #region to calculate paid amount in a sem
                CalculateSemAmt(lblStudentID.Text, SMEnt.PK_ID);
                lbl8thSemPaidAmt.Text = paidAmt.ToString();
                #endregion
            }
            #endregion

        }

    }
}