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
using System.IO;

public partial class finance_printbill : System.Web.UI.Page
{

    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    OFFICE OEnt = new OFFICE();
    officeService OSer = new officeService();

    Employees EMEnt = new Employees();
    EmployeesService EMSer = new EmployeesService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();


    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();


    Classes CEnt = new Classes();
    ClassesService CSer = new ClassesService();

    bill BEnt = new bill();
    billService BSer = new billService();

    masterbill MBEnt = new masterbill();
    masterbillService MBSer = new masterbillService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

    billprintstatus BPEnt = new billprintstatus();
    billprintstatusService BPSer = new billprintstatusService();
    

    HelperFunction hf = new HelperFunction();
    UserProfileEntity userprofile = new UserProfileEntity();

    EntityList theList = new EntityList();

    string mbillid;

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

        ddlSemester.Items.Insert(0, "Select");
    }

    protected void LoadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        BYEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }
    protected void LoadSemester()
    {

        SMEnt = new semester();
        SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
        ddlSemester.DataSource = SMSer.GetAll(SMEnt);
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void loadIndBillStudent()
    {
        EntityList theList = new EntityList();
        EntityList newList = new EntityList();
        BPEnt = new billprintstatus();
        BPEnt.SEMESTER = ddlSemester.SelectedValue;
        BPEnt.PROGRAM = ddlProgram.SelectedValue;
        theList = BPSer.GetAll(BPEnt);
        if (theList.Count > 0)
        {
            foreach (billprintstatus bps in theList)
            {
                if (bps.STUDENT_ID != "")
                {
                    STDEnt = new HSS_STUDENT();
                    STDEnt.STUDENT_ID = bps.STUDENT_ID;
                    STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                    if (STDEnt != null)
                        newList.Add(STDEnt);
                }
            }
        }
        ddlStudentID.DataSource = newList;
        ddlStudentID.DataTextField = "Student_ni";
        ddlStudentID.DataValueField = "STUDENT_ID";
        ddlStudentID.DataBind();
        ddlStudentID.Items.Insert(0, "Select");
    }

    protected void LoadGrid()
    {
        if (ddlProgram.SelectedValue != "Select" && ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")
        {
            gridBill.DataSource = hf.getbulk_inv_bill(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, ddlBillType.SelectedValue, ddlStudentID.SelectedValue);
            gridBill.DataBind();

            if (gridBill.Rows.Count > 0)
                gridBill.Visible = true;
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Bill not found");
                hide.Visible = false;
            }

        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Faculty, Program, Batch and Semester");
            gridBill.DataSource = null;
            gridBill.DataBind();
        }
    }

    protected void gridBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EntityList theList = new EntityList();
        EntityList masterbillList = new EntityList();
        if (e.CommandName.Equals("Print"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

            Label lblFiscalyear = gr.FindControl("lblFiscalyear") as Label;
            Label lblInstallmentNo = gr.FindControl("lblInstallmentNo") as Label;
            Label lblStudentID = gr.FindControl("lblStudentID") as Label;
            Label lblEdate = gr.FindControl("lblEdate") as Label;

            string[] engDate = lblEdate.Text.Split('/');

            string[] nepDate = hf.ConvertEnglishToNepali(engDate[1] + "/" + engDate[0] + "/" + engDate[2]);

            lblInstNo.Text = lblInstallmentNo.Text;

            MBEnt = new masterbill();
            MBEnt.BATCH = ddlBatch.SelectedValue;
            MBEnt.DAY = nepDate[0];
            MBEnt.MONTH = nepDate[1];
            MBEnt.YEAR = nepDate[2];
            if (lblStudentID.Text != "")
            {
                MBEnt.STUDENT_ID = lblStudentID.Text;
            }

            MBEnt.FISCALYEAR = lblFiscalyear.Text;
            MBEnt.INSTALLMENT = lblInstallmentNo.Text;

            theList = MBSer.GetAll(MBEnt);

            foreach (masterbill mb in theList)
            {
                CSEnt = new HSS_CURRENT_STUDENT();
                CSEnt.STUDENT_ID = mb.STUDENT_ID;

                CSEnt.STATUS = "1";
                CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
                if (CSEnt != null)
                {
                    SEnt = new HSS_STUDENT();
                    SEnt.STUDENT_ID = CSEnt.STUDENT_ID;

                    SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
                    if (SEnt != null)
                    {
                        masterbillList.Add(mb);
                    }

                }
            }

            if (masterbillList.Count > 0)
            {
                hide.Visible = true;
                rptrTotalCollection.DataSource = masterbillList;
                rptrTotalCollection.DataBind();
            }
            else
            {

                hide.Visible = false;
                HelperFunction.MsgBox(this, this.GetType(), "No Bill Found");
            }
        }
    }

    protected void rptrTotalCollection_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];

        #region defining all the labels and gridview used in bill
        Label lblMasterBillId = (Label)e.Item.FindControl("lblMasterBillId");
        Label lblCollegeName = (Label)e.Item.FindControl("lblCollegeName");
        Label lblAddress = (Label)e.Item.FindControl("lblAddress");
        Label lblPhoneNo = (Label)e.Item.FindControl("lblPhoneNo");
        Label lblEmail = (Label)e.Item.FindControl("lblEmail");

        Label lblBillNo = (Label)e.Item.FindControl("lblBillNo");
        Label lblInstallmentNo = (Label)e.Item.FindControl("lblInstallmentNo");
        Label lblDD = (Label)e.Item.FindControl("lblDD");
        Label lblDate = (Label)e.Item.FindControl("lblDate");
        Label lblNepaliDate = (Label)e.Item.FindControl("lblNepaliDate");
        Label lblRegNo = (Label)e.Item.FindControl("lblRegNo");
        Label lblSS = (Label)e.Item.FindControl("lblSS");

        Label lblSection = (Label)e.Item.FindControl("lblSection");
        Label lblStdName = (Label)e.Item.FindControl("lblStdName");


        Label lblCC = (Label)e.Item.FindControl("lblCC");

        Label lblBatch = (Label)e.Item.FindControl("lblBatch");

        GridView gridBillDetail = (GridView)e.Item.FindControl("gridBillDetail");

        Label lblDbCr = (Label)e.Item.FindControl("lblDbCr");

        Label lblTotal = (Label)e.Item.FindControl("lblTotal");

        Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");

        Label lblGTotal = (Label)e.Item.FindControl("lblGTotal");

        Label lblAmountW = (Label)e.Item.FindControl("lblAmountW");

        Label lblSn1 = (Label)e.Item.FindControl("lblSn1");
        Label lblSn2 = (Label)e.Item.FindControl("lblSn2");
        Label lblSn3 = (Label)e.Item.FindControl("lblSn3");
        Label lblSn4 = (Label)e.Item.FindControl("lblSn4");
        Label lblSn5 = (Label)e.Item.FindControl("lblSn5");
        Label lblSn6 = (Label)e.Item.FindControl("lblSn6");
        Label lblSn7 = (Label)e.Item.FindControl("lblSn7");
        Label lblSn8 = (Label)e.Item.FindControl("lblSn8");
        Label lblSn9 = (Label)e.Item.FindControl("lblSn9");
        Label lblSn10 = (Label)e.Item.FindControl("lblSn10");
        Label lblSn11 = (Label)e.Item.FindControl("lblSn11");
        Label lblSn12 = (Label)e.Item.FindControl("lblSn12");

        Label lblDes1 = (Label)e.Item.FindControl("lblDes1");
        Label lblDes2 = (Label)e.Item.FindControl("lblDes2");
        Label lblDes3 = (Label)e.Item.FindControl("lblDes3");
        Label lblDes4 = (Label)e.Item.FindControl("lblDes4");
        Label lblDes5 = (Label)e.Item.FindControl("lblDes5");
        Label lblDes6 = (Label)e.Item.FindControl("lblDes6");
        Label lblDes7 = (Label)e.Item.FindControl("lblDes7");
        Label lblDes8 = (Label)e.Item.FindControl("lblDes8");
        Label lblDes9 = (Label)e.Item.FindControl("lblDes9");
        Label lblDes10 = (Label)e.Item.FindControl("lblDes10");
        Label lblDes11 = (Label)e.Item.FindControl("lblDes11");
        Label lblDes12 = (Label)e.Item.FindControl("lblDes12");

        Label lblQty1 = (Label)e.Item.FindControl("lblQty1");
        Label lblQty2 = (Label)e.Item.FindControl("lblQty2");
        Label lblQty3 = (Label)e.Item.FindControl("lblQty3");
        Label lblQty4 = (Label)e.Item.FindControl("lblQty4");
        Label lblQty5 = (Label)e.Item.FindControl("lblQty5");
        Label lblQty6 = (Label)e.Item.FindControl("lblQty6");
        Label lblQty7 = (Label)e.Item.FindControl("lblQty7");
        Label lblQty8 = (Label)e.Item.FindControl("lblQty8");
        Label lblQty9 = (Label)e.Item.FindControl("lblQty9");
        Label lblQty10 = (Label)e.Item.FindControl("lblQty10");
        Label lblQty11 = (Label)e.Item.FindControl("lblQty11");
        Label lblQty12 = (Label)e.Item.FindControl("lblQty12");


        Label lblAmt1 = (Label)e.Item.FindControl("lblAmt1");
        Label lblAmt2 = (Label)e.Item.FindControl("lblAmt2");
        Label lblAmt3 = (Label)e.Item.FindControl("lblAmt3");
        Label lblAmt4 = (Label)e.Item.FindControl("lblAmt4");
        Label lblAmt5 = (Label)e.Item.FindControl("lblAmt5");
        Label lblAmt6 = (Label)e.Item.FindControl("lblAmt6");
        Label lblAmt7 = (Label)e.Item.FindControl("lblAmt7");
        Label lblAmt8 = (Label)e.Item.FindControl("lblAmt8");
        Label lblAmt9 = (Label)e.Item.FindControl("lblAmt9");
        Label lblAmt10 = (Label)e.Item.FindControl("lblAmt10");
        Label lblAmt11 = (Label)e.Item.FindControl("lblAmt11");
        Label lblAmt12 = (Label)e.Item.FindControl("lblAmt12");

        //Label lblUserName = (Label)e.Item.FindControl("lblUserName");

        Image imgStdBarcode = (Image)e.Item.FindControl("imgStdBarcode");
        Image imgBillBarcode = (Image)e.Item.FindControl("imgBillBarcode");

        #endregion

        #region to clear all label text

        lblDes1.Text = "";

        lblQty1.Text = "";
        lblAmt1.Text = "";

        lblDes2.Text = "";

        lblQty2.Text = "";
        lblAmt2.Text = "";

        lblDes3.Text = "";

        lblQty3.Text = "";
        lblAmt3.Text = "";

        lblDes4.Text = "";

        lblQty4.Text = "";
        lblAmt4.Text = "";

        lblDes5.Text = "";

        lblQty5.Text = "";
        lblAmt5.Text = "";

        lblDes6.Text = "";

        lblQty6.Text = "";
        lblAmt6.Text = "";

        lblDes7.Text = "";

        lblQty7.Text = "";
        lblAmt7.Text = "";

        lblDes8.Text = "";
        lblQty8.Text = "";
        lblAmt8.Text = "";

        lblDes9.Text = "";

        lblQty9.Text = "";
        lblAmt9.Text = "";

        lblDes10.Text = "";

        lblQty10.Text = "";
        lblAmt10.Text = "";

        lblDes11.Text = "";
        lblQty11.Text = "";
        lblAmt11.Text = "";

        lblDes12.Text = "";

        lblQty12.Text = "";
        lblAmt12.Text = "";

        #endregion


        #region to load the College Name and Address.
        NEnt = new HSS_NAME();

        NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
        lblCollegeName.Text = NEnt.NAME;
        lblAddress.Text = NEnt.ADRESS;
        lblPhoneNo.Text = NEnt.CONTACT;
        lblEmail.Text = NEnt.WEBSITE;
        #endregion

        #region to load other data from master table

        MBEnt = new masterbill();
        MBEnt.MBILL_ID = lblMasterBillId.Text;
        MBEnt = (masterbill)MBSer.GetSingle(MBEnt);
        if (MBEnt != null)
        {
            lblRegNo.Text = MBEnt.STUDENT_ID;

            lblInstallmentNo.Text = MBEnt.INSTALLMENT;
            lblDate.Text = MBEnt.E_DATE;
            lblNepaliDate.Text = MBEnt.DAY + "/" + MBEnt.MONTH + "/" + MBEnt.YEAR;

            lblBillNo.Text = MBEnt.BILLNO;

            CSEnt = new HSS_CURRENT_STUDENT();
            SEnt = new HSS_STUDENT();
            CSEnt.STUDENT_ID = lblRegNo.Text;
            SEnt.STUDENT_ID = lblRegNo.Text;
            SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
            if (SEnt != null)
            {
                lblStdName.Text = SEnt.NAME_ENGLISH;
            }
            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
            if (CSEnt != null)
            {
                lblSection.Text = CSEnt.SECTION;
                lblBatch.Text = CSEnt.BATCH;
            }
        }
        #endregion

        #region to load the bill detail in gridBillDetail
        gridBillDetail.DataSource = hf.getBillDetail(lblMasterBillId.Text);
        gridBillDetail.DataBind();
        //FillUpBill(mbilid);
        #endregion

        #region to fill up the bill

        double total = 0;
        int count = 1;
        foreach (GridViewRow gr in gridBillDetail.Rows)
        {
            Label lblParticular = gr.FindControl("lblParticular") as Label;

            Label lblQty = gr.FindControl("lblQty") as Label;
            Label lblAmt = gr.FindControl("lblAmt") as Label;
            Label lblAmount = gr.FindControl("lblAmt2") as Label;
            Label lblRemarks = gr.FindControl("lblRemarks") as Label;

            if (count == 1)
            {
                lblDes1.Text = lblParticular.Text;

                lblQty1.Text = lblQty.Text;
                lblAmt1.Text = lblAmount.Text;
                lblSn1.Text = "1.";
                lblSn2.Text = "&nbsp;";
                lblSn3.Text = "&nbsp;";
                lblSn4.Text = "&nbsp;";
                lblSn5.Text = "&nbsp;";
                lblSn6.Text = "&nbsp;";
                lblSn7.Text = "&nbsp;";
                lblSn8.Text = "&nbsp;";
                lblSn9.Text = "&nbsp;";
                lblSn10.Text = "&nbsp;";
                lblSn11.Text = "&nbsp;";
                lblSn12.Text = "&nbsp;";


                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 2)
            {
                lblDes2.Text = lblParticular.Text;

                lblQty2.Text = lblQty.Text;
                lblAmt2.Text = lblAmount.Text;
                lblSn2.Text = "2.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }

            if (count == 3)
            {
                lblDes3.Text = lblParticular.Text;

                lblQty3.Text = lblQty.Text;
                lblAmt3.Text = lblAmount.Text;
                lblSn3.Text = "3.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 4)
            {
                lblDes4.Text = lblParticular.Text;

                lblQty4.Text = lblQty.Text;
                lblAmt4.Text = lblAmount.Text;
                lblSn4.Text = "4.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 5)
            {
                lblDes5.Text = lblParticular.Text;

                lblQty5.Text = lblQty.Text;
                lblAmt5.Text = lblAmount.Text;
                lblSn5.Text = "5.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 6)
            {
                lblDes6.Text = lblParticular.Text;

                lblQty6.Text = lblQty.Text;
                lblAmt6.Text = lblAmount.Text;
                lblSn6.Text = "6.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 7)
            {
                lblDes7.Text = lblParticular.Text;

                lblQty7.Text = lblQty.Text;
                lblAmt7.Text = lblAmount.Text;
                lblSn7.Text = "7.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 8)
            {
                lblDes8.Text = lblParticular.Text;

                lblQty8.Text = lblQty.Text;
                lblAmt8.Text = lblAmount.Text;
                lblSn8.Text = "8.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 9)
            {
                lblDes9.Text = lblParticular.Text;

                lblQty9.Text = lblQty.Text;
                lblAmt9.Text = lblAmount.Text;
                lblSn9.Text = "9.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 10)
            {
                lblDes10.Text = lblParticular.Text;

                lblQty10.Text = lblQty.Text;
                lblAmt10.Text = lblAmount.Text;
                lblSn10.Text = "10.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 11)
            {
                lblDes11.Text = lblParticular.Text;

                lblQty11.Text = lblQty.Text;
                lblAmt11.Text = lblAmount.Text;
                lblSn11.Text = "11.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }
            if (count == 12)
            {
                lblDes12.Text = lblParticular.Text;

                lblQty12.Text = lblQty.Text;
                lblAmt12.Text = lblAmount.Text;
                lblSn12.Text = "12.";

                total = total + Convert.ToDouble(lblAmt.Text);
            }

            count++;
        }

        double gtotal = 0.0;
        double disc = 0.0;
        MBEnt = new masterbill();
        MBEnt.MBILL_ID = lblMasterBillId.Text;
        MBEnt = (masterbill)MBSer.GetSingle(MBEnt);
        if (MBEnt != null)
        {

            lblDbCr.Text = MBEnt.REMAINING_BALANCE;
            if (MBEnt.F_GRANDTOTAL != "")
                gtotal = Convert.ToDouble(MBEnt.F_GRANDTOTAL);
            else
                gtotal = 0;

            if (MBEnt.DISCOUNT != "")
            {
                disc = Convert.ToDouble(MBEnt.DISCOUNT);
            }
            else
            {
                disc = 0;
            }
        }

        lblTotal.Text = total.ToString("#0.00");

        if (Convert.ToDouble(lblDbCr.Text) < 0)
        {
            lblDbCr.Text = "(" + (Convert.ToDouble(lblDbCr.Text) * -1).ToString("#0.00") + ")";
        }

        lblDiscount.Text = disc.ToString("#0.00");

        lblGTotal.Text = gtotal.ToString("#0.00");

        lblAmountW.Text = hf.NumWordsWrapper(gtotal).ToUpper() + " ONLY";

        //lblUserName.Text = "Issued By: " + userprofile.EmployeeName;
        #endregion

        #region to load Student Barcode image
        string imgfolder = "";
        imgfolder = Server.MapPath(@"~/images/student_barcode/") + lblRegNo.Text + ".jpg";
        if (File.Exists(imgfolder))
        {
            imgStdBarcode.ImageUrl = @"~/images/student_barcode/" + lblRegNo.Text + ".jpg";
        }
        else
        {
            //imgStdBarcode.ImageUrl = @"~/images/Employee/male.jpg";
        }
        #endregion

        #region to load Bill Barcode
        string imgBillfolder = "";
        imgBillfolder = Server.MapPath(@"~/images/barcode_billPK/") + lblMasterBillId.Text + ".jpg";
        if (File.Exists(imgfolder))
        {
            imgBillBarcode.ImageUrl = @"~/images/barcode_billPK/" + lblMasterBillId.Text + ".jpg";

        }
        else
        {
            // imgBillBarcode.ImageUrl = @"~/images/Employee/male.jpg";
        }
        #endregion

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);

        #region to update status in billprintstatus table
        BPEnt = new billprintstatus();
        BPEnt.BATCH = ddlBatch.SelectedValue;
        BPEnt.PROGRAM = ddlProgram.SelectedValue;
        BPEnt.SEMESTER = ddlSemester.SelectedValue;
        BPEnt.INSTALLMENT_NO = lblInstNo.Text;

        BPEnt = (billprintstatus)BPSer.GetSingle(BPEnt);
        if (BPEnt != null)
        {
            BPEnt.PRINT_STATUS = "1";
            BPSer.Update(BPEnt);
        }

        LoadGrid();

        #endregion

    }

    protected void btnView_Click(object sender, EventArgs e)
    {

        LoadGrid();
    }
    protected void gridBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSn = e.Row.FindControl("lblSn") as Label;
            Button btnLoad = e.Row.FindControl("btnLoad") as Button;

            if (lblSn.Text == "1")
            {
                btnLoad.Enabled = true;
            }
        }

    }
    protected void gridBillDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblParticular = e.Row.FindControl("lblParticular") as Label;
            Label lblAmt2 = e.Row.FindControl("lblAmt2") as Label;


            string[] particularname = lblParticular.Text.Split('-');
            if (particularname[0] == "Disc. ")
            {
                lblAmt2.Text = "(" + (Convert.ToDouble(lblAmt2.Text) * -1) + ")";
            }

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
        gridBill.Visible = false;
        hide.Visible = false;
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            LoadSemester();

            gridBill.Visible = false;
            hide.Visible = false;
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");



        }
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }


    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillType.SelectedValue == "Individual")
        {
            trIndBills.Visible = true;
            lblIndBillText.Visible = true;
            ddlStudentID.Visible = true;
            gridBill.Visible = false;
            hide.Visible = false;
        }
        else
        {
            trIndBills.Visible = false;
            lblIndBillText.Visible = false;
            ddlStudentID.Visible = false;
            ddlStudentID.SelectedIndex = 0;
            gridBill.Visible = false;
            hide.Visible = false;
        }

    }

    protected void ddlStudentID_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridBill.Visible = false;
        hide.Visible = false;
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadIndBillStudent();
        gridBill.Visible = false;
        hide.Visible = false;
    }
}