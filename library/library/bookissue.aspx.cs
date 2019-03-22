using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.Collections;
using Entity.Framework;

public partial class library_library_bookissue : System.Web.UI.Page
{

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    HSS_TEACHER TEnt = new HSS_TEACHER();
    HSS_TEACHERService TSer = new HSS_TEACHERService();

    // staff STEnt = new staff();
    // staffService STSer = new staffService();

    booktype BTEnt = new booktype();
    booktypeService BTSer = new booktypeService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    book BEnt = new book();
    bookService BSer = new bookService();

    bookdetails BDEnt = new bookdetails();
    bookdetailsService BDSer = new bookdetailsService();

    bookissue BISEnt = new bookissue();
    bookissueService BISSer = new bookissueService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT SIEnt = new HSS_STUDENT();
    HSS_STUDENTService SISer = new HSS_STUDENTService();

    UserProfileEntity userProfileEnt = new UserProfileEntity();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    BOOKSHELFCOMPART BSCEnt = new BOOKSHELFCOMPART();
    BOOKSHELFCOMPARTService BSCSer = new BOOKSHELFCOMPARTService();

    BOOKSHELF BSEnt = new BOOKSHELF();
    BOOKSHELFService BSSer = new BOOKSHELFService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStdBarcode.Focus();
            LoadProgram();

            LoadSection();
            LoadIssueToType();
            LoadTeacher();
            // LoadStaff();
            userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
            txtIssueBy.Text = userProfileEnt.EmployeeName;
            txtReceiveBy.Text = userProfileEnt.EmployeeName;
            ddlIssueToType.SelectedIndex = 0;

        }
    }


    protected void LoadProgram()
    {
        PEnt = new program();
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
        BTCEnt = new BatchYear();
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }


    protected void LoadSemester()
    {
        BTCEnt = new BatchYear();
        BTCEnt.BATCH = ddlBatch.SelectedValue;
        BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
        if (BTCEnt != null)
        {
            SMEnt = new semester();
            SMEnt.SYLLABUS_YEAR = BTCEnt.SYLLABUS_YEAR;
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            ddlSemester.DataSource = SMSer.GetAll(SMEnt);
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");
        }
    }

    protected void LoadSection()
    {
        SCEnt = new Section();
        ddlSection.DataSource = SCSer.GetAll(SCEnt);
        ddlSection.DataTextField = "SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");
    }

    protected void LoadTeacher()
    {

        //ddlTeacher.DataSource = hf.getTeacherInfo();
        //ddlTeacher.DataTextField = "FULL_NAME";
        //ddlTeacher.DataValueField = "EMPLOYEEID";
        //ddlTeacher.DataBind();
        //ddlTeacher.Items.Insert(0, "Select");

    }



    protected void LoadIssuedTable()
    {

        BISEnt = new bookissue();

        String issuetotype = ddlIssueToType.SelectedValue;
        if (issuetotype == "Teacher")
        {
            // BISEnt.Issueto = ddlTeacher.SelectedValue;


        }
        //else if (issuetotype == "Staff")
        //{
        //    BISEnt.Issueto = ddlStaff.SelectedValue;
        //}
        else if (issuetotype == "Student")
        {
            BISEnt.Issueto = ddlStudentName.SelectedValue;
        }

        BISEnt.Issueto_type = ddlIssueToType.SelectedValue;
        BISEnt.Status = "0";
        gridIssued.DataSource = BISSer.GetAll(BISEnt);
        gridIssued.DataBind();

        if (gridIssued.Rows.Count == 0)
        {
            gridIssued.Visible = false;
        }
        else
        {
            gridIssued.Visible = true;
        }




    }



    protected void ToIssue()
    {
        BDEnt = new bookdetails();
        BDEnt.Status = "1";
        gridToIssue.DataSource = BDSer.GetAll(BDEnt);
        gridToIssue.DataBind();

        if (gridToIssue.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            BDEnt = new bookdetails();
            a1.Add(BDEnt);

            gridToIssue.DataSource = a1;
            gridToIssue.DataBind();
        }
    }


    protected void ToReceive()
    {
        BISEnt = new bookissue();

        String issuetotype = ddlIssueToType.SelectedValue;
        if (issuetotype == "Teacher")
        {
            //  BISEnt.Issueto = ddlTeacher.SelectedValue;


        }
        //else if (issuetotype == "Staff")
        //{
        //    BISEnt.Issueto = ddlStaff.SelectedValue;
        //}
        else if (issuetotype == "Student")
        {
            BISEnt.Issueto = ddlStudentName.SelectedValue;
        }
        BISEnt.Issueto_type = ddlIssueToType.SelectedValue;
        BISEnt.Status = "0";



        gridToReceive.DataSource = BISSer.GetAll(BISEnt);
        gridToReceive.DataBind();




        if (gridToReceive.Rows.Count == 0)
        {


            gridToReceive.DataSource = null;
            gridToReceive.DataBind();
        }
    }

    public void LoadIssueToType()
    {
        String issuetotype = ddlIssueToType.SelectedValue;


        if (issuetotype.Equals("Student"))
        {
            lblSemester.Visible = true;
            ddlSemester.Visible = true;
            // lblIssueTo.Visible = false;
            //   ddlTeacher.Visible = false;
            // ddlStaff.Visible = false;
            lblSection.Visible = true;
            ddlSection.Visible = true;
            lblStudentName.Visible = true;
            ddlStudentName.Visible = true;
            lblBatch.Visible = true;
            ddlBatch.Visible = true;

        }

        else if (issuetotype.Equals("Teacher"))
        {
            //  lblIssueTo.Visible = true;
            //   ddlTeacher.Visible = true;
            lblSemester.Visible = false;
            ddlSemester.Visible = false;
            // ddlStaff.Visible = false;
            lblSection.Visible = false;
            ddlSection.Visible = false;
            lblStudentName.Visible = false;
            ddlStudentName.Visible = false;
            txtStudentName.Visible = false;
            lblBatch.Visible = false;
            ddlBatch.Visible = false;
            // ddlStudentName.SelectedIndex = 0;

        }
        //else if (issuetotype.Equals("Staff"))
        //{
        //    lblIssueTo.Visible = true;
        //   // ddlStaff.Visible = true;
        //    lblClass.Visible = false;
        //    ddlClass.Visible = false;
        //    ddlTeacher.Visible = false;
        //    lblSection.Visible = false;
        //    ddlSection.Visible = false;
        //    lblStudentName.Visible = false;
        //    ddlStudentName.Visible = false;

        //}

    }


    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadIssuedTable();
    }

    protected void ddlIssueToType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadIssueToType();
    }




    protected void ddlBookName_SelectedIndexChanged(object sender, EventArgs e)
    {

        // btnPopup_ModalPopupExtender.Show();
    }



    protected void btnIssue_Click1(object sender, EventArgs e)
    {
        if (ddlStudentName.SelectedValue != "Select" && ddlStudentName.SelectedValue != "")
        {
            issue_div.Visible = true;
            receive_div.Visible = false;

            gridToIssue.Visible = false;
            txtEDate.Text = hf.GetTodayDate();
            txtNSBN.Focus();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Borrower Name");
        }

    }


    protected void btnReceive_Click(object sender, EventArgs e)
    {
        issue_div.Visible = false;
        receive_div.Visible = true;

        txtRDate.Text = hf.GetTodayDate();
        ToReceive();

    }

    protected void btnIssue1_Click(object sender, EventArgs e)
    {
        BISEnt = new bookissue();

        string[] NDate = hf.ConvertEnglishToNepali(txtEDate.Text);
        BISEnt.Issuedate = txtEDate.Text;
        BISEnt.Issueday = NDate[0];
        BISEnt.Issuemonth = NDate[1];
        BISEnt.Issueyear = NDate[2];
        BISEnt.Issueto_type = ddlIssueToType.SelectedValue;
        BISEnt.Issuedby = txtIssueBy.Text;

        String issuetotype = ddlIssueToType.SelectedValue;
        if (issuetotype == "Teacher")
        {
            //  BISEnt.Issueto = ddlTeacher.SelectedValue;
        }
        //else if (issuetotype == "Staff")
        //{
        //    BISEnt.Issueto = ddlStaff.SelectedValue;
        //}
        else if (issuetotype == "Student")
        {
            BISEnt.Issueto = ddlStudentName.SelectedValue;
        }


        foreach (GridViewRow row in gridToIssue.Rows)
        {

            Label lblBookDetailId = row.FindControl("lblBookDetailId") as Label;
            Label lblNSBN = row.FindControl("lblNSBN") as Label;
            Label lblBookNumber = row.FindControl("lblBookNumber") as Label;
            Label lblRemarks = row.FindControl("lblRemarks") as Label;
            Label lblStatus = row.FindControl("lblStatus") as Label;


            BISEnt.NSBN = lblNSBN.Text;

            BISEnt.Remarks = txtRemarks.Text;
            BISEnt.Status = "0";

            if (BISSer.Insert(BISEnt) == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Issue Record Inserted Successfully')", true);
                BDEnt = new bookdetails();
                BDEnt.Bookdetailid = lblBookDetailId.Text;
                BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
                if (BDEnt != null)
                {
                    BDEnt.Status = "0";
                    BDSer.Update(BDEnt);

                }
                issue_div.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Issue Record Insertion is not Successfull')", true);


            }





        }


        LoadIssuedTable();

    }



    protected void gridToIssue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblBookId = e.Row.FindControl("lblBookId") as Label;
            Label lblBookName = e.Row.FindControl("lblBookName") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;
            Label lblStat = e.Row.FindControl("lblStat") as Label;
            Label lblIssuable = e.Row.FindControl("lblIssuable") as Label;
            Label lblIssueType = e.Row.FindControl("lblIssueType") as Label;

            String Status = lblStatus.Text;

            BEnt = new book();


            BEnt.Bookid = lblBookId.Text;
            BEnt = (book)BSer.GetSingle(BEnt);

            if (BEnt != null)
            {
                lblBookName.Text = BEnt.Bookname;
                if (BEnt.Issuable == "0")
                {
                    lblIssuable.Text = "No";
                }
                else
                {
                    lblIssuable.Text = "Yes";
                    lblIssueType.Text = BEnt.Issuetype;
                }

            }

            if (Status == "1")
            {
                lblStat.Text = "Available";

            }
            else if (Status == "0")
            {
                lblStat.Text = "Unavailable";
            }

        }
    }


    protected void gridIssued_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblDate = e.Row.FindControl("lblDate") as Label;

            Label lblBookName = e.Row.FindControl("lblBookName") as Label;
            Label lblNSBN = e.Row.FindControl("lblNSBN") as Label;
            Label lblMasterBookIssueId = e.Row.FindControl("lblMasterBookIssueId") as Label;
            Label lblIssuable = e.Row.FindControl("lblIssuable") as Label;
            Label lblIssueType = e.Row.FindControl("lblIssueType") as Label;

            BISEnt = new bookissue();
            BEnt = new book();
            BTEnt = new booktype();

            //BEnt = new book();
            //BEnt.Bookid = lblNSBN.Text;
            //BEnt = (book)BSer.GetSingle(BEnt);

            //if (BEnt != null)
            //{
            //    lblBookName.Text = BEnt.Bookname;
            //}



            BISEnt.Masterbookissueid = lblMasterBookIssueId.Text;


            BISEnt = (bookissue)BISSer.GetSingle(BISEnt);


            if (BISEnt != null)
            {

                lblDate.Text = BISEnt.Issuedate;



                //BEnt.Bookid = lblBookId.Text;
                //BEnt = (book)BSer.GetSingle(BEnt);
                //if (BEnt != null)
                //{

                //    lblBookTypeId.Text = BEnt.Booktypeid;
                //    BTEnt.Booktypeid = lblBookTypeId.Text;
                //    BTEnt = (booktype)BTSer.GetSingle(BTEnt);

                //    if (BTEnt != null)
                //    {
                //        lblBookType.Text = BTEnt.Booktype;

                //    }


                //}


            }

            BDEnt = new bookdetails();
            BDEnt.NSBN = lblNSBN.Text;
            BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
            if (BDEnt != null)
            {
                BEnt = new book();
                BEnt.Bookid = BDEnt.Bookid;
                BEnt = (book)BSer.GetSingle(BEnt);
                if (BEnt != null)
                {
                    lblBookName.Text = BEnt.Bookname;
                    if (BEnt.Issuable == "1")
                    {
                        lblIssuable.Text = "Yes";
                    }
                    else
                    {
                        lblIssuable.Text = "No";
                    }
                    lblIssueType.Text = BEnt.Issuetype;
                }
            }
        }

    }

    protected void gridToReceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblNSBN = e.Row.FindControl("lblNSBN") as Label;
            Label lblBookName = e.Row.FindControl("lblBookName") as Label;
            Label lblShelf = e.Row.FindControl("lblShelf") as Label;
            Label lblCompart = e.Row.FindControl("lblCompart") as Label;

            CheckBox chkReceive = e.Row.FindControl("chkReceive") as CheckBox;


            BDEnt = new bookdetails();
            BDEnt.NSBN = lblNSBN.Text;
            BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
            if (BDEnt != null)
            {
                BSCEnt = new BOOKSHELFCOMPART();
                BSCEnt.PK_ID = BDEnt.COMPARTID;
                BSCEnt = (BOOKSHELFCOMPART)BSCSer.GetSingle(BSCEnt);
                if (BSCEnt != null)
                {
                    lblCompart.Text = BSCEnt.COMPARTNO;
                    BSEnt = new BOOKSHELF();
                    BSEnt.PK_ID = BSCEnt.SHELFID;
                    BSEnt = (BOOKSHELF)BSSer.GetSingle(BSEnt);
                    if (BSEnt != null)
                    {
                        lblShelf.Text = BSEnt.SHELFNO;
                    }
                }

                BEnt = new book();
                BEnt.Bookid = BDEnt.Bookid;
                BEnt = (book)BSer.GetSingle(BEnt);
                if (BEnt != null)
                {
                    lblBookName.Text = BEnt.Bookname;
                }
            }

        }
    }
    protected void btnReceive1_Click(object sender, EventArgs e)
    {

        BISEnt = new bookissue();



        foreach (GridViewRow row in gridToReceive.Rows)
        {
            CheckBox chkReceive = row.FindControl("chkReceive") as CheckBox;
            Label lblDate = row.FindControl("lblDate") as Label;
            Label lblNSBN = row.FindControl("lblNSBN") as Label;

            Label lblBookDetailId = row.FindControl("lblBookDetailId") as Label;
            Label lblStatus = row.FindControl("lblStatus") as Label;
            Label lblMasterBookIssueId = row.FindControl("lblMasterBookIssueId") as Label;
            if (chkReceive.Checked == true)
            {

                string[] NRDate = hf.ConvertEnglishToNepali(txtRDate.Text);
                BISEnt.Masterbookissueid = lblMasterBookIssueId.Text;
                BISEnt.Receivedate = txtRDate.Text;
                BISEnt.Receiveday = NRDate[0];
                BISEnt.Receivemonth = NRDate[1];
                BISEnt.Receiveyear = NRDate[2];
                BISEnt.NSBN = lblNSBN.Text;
                BISEnt.Issueto_type = ddlIssueToType.SelectedValue;
                BISEnt.Issuedby = txtIssueBy.Text;
                BISEnt.Receivedby = txtReceiveBy.Text;

                string[] NIDate = hf.ConvertEnglishToNepali(lblDate.Text);
                BISEnt.Issuedate = lblDate.Text;
                BISEnt.Issueday = NIDate[0];
                BISEnt.Issuemonth = NIDate[1];
                BISEnt.Issueyear = NIDate[2];

                String issuetotype = ddlIssueToType.SelectedValue;
                if (issuetotype == "Teacher")
                {
                    // BISEnt.Issueto = ddlTeacher.SelectedValue;
                }
                //else if (issuetotype == "Staff")
                //{
                //    BISEnt.Issueto = ddlStaff.SelectedValue;
                //}
                else if (issuetotype == "Student")
                {
                    BISEnt.Issueto = ddlStudentName.SelectedValue;
                }



                BISEnt.Status = "1";



                BDEnt = new bookdetails();
                BDEnt.NSBN = lblNSBN.Text;

                BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);

                if (BDEnt != null)
                {


                    BDEnt.Status = "1";
                    BDSer.Update(BDEnt);
                }

                if (BISSer.Update(BISEnt) >= 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Received Record Inserted Successfully')", true);
                    receive_div.Visible = false;

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Received Record Insertion is not Successfull')", true);

                }
            }
        }
        LoadIssuedTable();



    }

    //protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadIssuedTable();
    //}


    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadStudent();

    }

    protected void LoadStudent()
    {


        ddlStudentName.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, ddlSection.SelectedValue);
        ddlStudentName.DataTextField = "student_name";
        ddlStudentName.DataValueField = "STUDENT_ID";
        ddlStudentName.DataBind();
        ddlStudentName.Items.Insert(0, "Select");
    }

    protected void ddlStudentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadIssuedTable();

        SIEnt = new HSS_STUDENT();
        SIEnt.STUDENT_ID = ddlStudentName.SelectedValue;

        SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
        if (SIEnt != null)
        {
            txtStudentName.Visible = true;
            txtStudentName.Text = SIEnt.NAME_ENGLISH;

        }
    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudent();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        issue_div.Visible = false;

    }
    protected void btnCancelReceive_Click(object sender, EventArgs e)
    {
        receive_div.Visible = false;
    }
    protected void txtNSBN_TextChanged(object sender, EventArgs e)
    {
        BDEnt = new bookdetails();
        BDEnt.NSBN = txtNSBN.Text;
        BDEnt.Status = "1";
        gridToIssue.DataSource = BDSer.GetAll(BDEnt);
        gridToIssue.DataBind();

        if (gridToIssue.Rows.Count == 0)
        {
            txtNSBN.Text = "";
            txtNSBN.Focus();
            gridToIssue.DataSource = null;
            gridToIssue.DataBind();
            HelperFunction.MsgBox(this, this.GetType(), "Currently This Book is Not Available");
        }
        gridToIssue.Visible = true;
        BDEnt = new bookdetails();
        BDEnt.NSBN = txtNSBN.Text;
        BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
        if (BDEnt != null)
        {
            BEnt = new book();
            BEnt.Bookid = BDEnt.Bookid;
            BEnt = (book)BSer.GetSingle(BEnt);
            if (BEnt != null)
            {
                if (BEnt.Issuable == "0")
                {
                    btnIssue1.Enabled = false;
                }
                else
                {
                    btnIssue1.Enabled = true;
                }
            }
        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            LoadSemester();
            LoadStudent();
        }
        else
        {

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlStudentName.Items.Clear();
            ddlStudentName.Items.Insert(0, "Select");

        }
    }


    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadBatch();

        }
        else
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlStudentName.Items.Clear();
            ddlStudentName.Items.Insert(0, "Select");
        }
        if (ddlBatch.SelectedValue == "Select")
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlStudentName.Items.Clear();
            ddlStudentName.Items.Insert(0, "Select");
        }
    }

    protected void txtStdBarcode_TextChanged(object sender, EventArgs e)
    {
        STDEnt = new HSS_STUDENT();
        STDEnt.STUDENT_ID = txtStdBarcode.Text;
        STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
        if (STDEnt != null)
        {
            LoadProgram();
            ddlProgram.SelectedValue = STDEnt.PROGRAM;
            LoadBatch();
            ddlBatch.SelectedValue = STDEnt.BAT_CH;
        }

        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.STUDENT_ID = txtStdBarcode.Text;
        CSEnt.YEAR = hf.NepaliYear();
        CSEnt.STATUS = "1";
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {
            ddlSection.SelectedValue = CSEnt.SECTION;

            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                LoadSemester();
                ddlSemester.SelectedValue = CSEnt.SEMESTER;
            }
            LoadStudent();
            ddlStudentName.SelectedValue = CSEnt.STUDENT_ID;
            LoadIssuedTable();
        }


    }
}