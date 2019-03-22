using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class class_routine_reports_download_notes : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    notes_detail NDEnt = new notes_detail();
    notes_detailService NDSer = new notes_detailService();

    bool flag = false;
    string name = "";
    ArrayList alist = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
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
        ddlSubject.Items.Insert(0, "Select");

    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        ddlSubject.Items.Insert(0, "Select");

    }


    protected void LoadBatch()
    {
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void LoadSemester()
    {
        EntityList theList = new EntityList();
        EntityList semList = new EntityList();
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BTCSer.GetAll(BTCEnt);
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
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue != "Select")
        {
            LoadSubject();
        }
        else {
         
            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, "Select");
        }
    }

    protected void LoadSubject()
    {
        BTCEnt = new BatchYear();
        BTCEnt.BATCH = ddlBatch.SelectedValue;
        BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
        if (BTCEnt != null)
        {
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            SUBEnt.PROGRAM = ddlProgram.SelectedValue;
            SUBEnt.YEAR = BTCEnt.SYLLABUS_YEAR;
            SUBEnt.STATUS = "1";
            ddlSubject.DataSource = SUBSer.GetAll(SUBEnt);
            ddlSubject.DataTextField = "SUBJECT_NAME";
            ddlSubject.DataValueField = "PK_ID";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "Select");
        }
    }

    private string GetFileTypeByExtension(string extension)
    {


        switch (extension.ToLower())
        {
            case ".doc":
            case ".docx":
                return "Microsoft Word Document";

            case ".xlsx":
            case ".xls":
                return "Microsoft Excel Document";

            case ".ppt":
            case ".pptx":
                return "Powerpoint Document";

            case ".pdf":
                return "PDF Document";


            case ".txt":
                return "Text Document";

            case ".jpg":
            case ".gif":
            case ".png":
            case ".tif":
            case ".bmp":
                return "Image";

            case ".mp3":
            case ".m4a":
            case ".wav":
            case ".aui":
                return "Audio Document";

            case ".mp4":
            case ".3gp":
            case ".mpeg":
            case ".flv":
            case ".mkv":
                return "Video Document";

            default:
                return "Unknown";


        }
    }


   
    protected void gridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Image img = e.Row.FindControl("img") as System.Web.UI.WebControls.Image;
            Label lblSize = e.Row.FindControl("lblSize") as Label;
            Label lblType = e.Row.FindControl("lblType") as Label;
            
            Label lblLinkName = e.Row.FindControl("lblLinkName") as Label;


            //string strFileName = Server.MapPath(lblLinkName.ToString());


            try
            {

                FileInfo fileInfo = new FileInfo(lblLinkName.Text);

                string extension = fileInfo.Extension.ToLower();
                if (extension == ".jpg" || extension == ".png" || extension == ".bmp" || extension == ".gif" || extension == ".tif")
                {
                    img.ImageUrl = "~/Notes/" + fileInfo.Name;
                    //  alist.Add(fileInfo.Name);



                }
                else if (extension == ".m4a" || extension == ".mp3" || extension == ".avg")
                {
                    img.ImageUrl = "~/images/fileicon/m-icon.png";
                    //   alist.Add(fileInfo.Name);


                }
                else if (extension == ".txt")
                {
                    img.ImageUrl = "~/images/fileicon/text.png";
                    // alist.Add(fileInfo.Name);

                }
                else if (extension == ".htm" || extension == ".html")
                {
                    img.ImageUrl = "~/images/fileicon/html.png";
                    //  alist.Add(fileInfo.Name);


                }
                else if (extension == ".doc" || extension == ".docx")
                {
                    img.ImageUrl = "~/images/fileicon/doc.png";
                    /// alist.Add(fileInfo.Name);


                }
                else if (extension == ".xls" || extension == ".xlsx")
                {
                    img.ImageUrl = "~/images/fileicon/excel.png";
                    // alist.Add(fileInfo.Name);


                }

                else if (extension == ".ppt" || extension == ".pptx")
                {
                    img.ImageUrl = "~/images/fileicon/ppt.png";
                    // alist.Add(fileInfo.Name);


                }
                else if (extension == ".pdf")
                {
                    img.ImageUrl = "~/images/fileicon/pdf.png";
                    //alist.Add(fileInfo.Name);


                }

                else if (extension == ".mp4" || extension == ".mkv" || extension == ".vob" || extension == ".mpeg" || extension == ".flv" || extension == ".3gp")
                {
                    img.ImageUrl = "~/images/fileicon/Video.png";
                    // alist.Add(fileInfo.Name);


                }

                else
                {
                    img.ImageUrl = "~/images/fileicon/unknown.png";
                    // alist.Add(fileInfo.Name);


                }

                lblSize.Text = fileInfo.Length.ToString();

                if (Convert.ToDouble(lblSize.Text) / (1024) < 1024)
                {
                    lblSize.Text = (Convert.ToDouble(lblSize.Text) / (1024)).ToString("#0.00") + " KB";
                }
                else if (Convert.ToDouble(lblSize.Text) / (1024) > 1024)
                {
                    lblSize.Text = (Convert.ToDouble(lblSize.Text) / (1024 * 1024)).ToString("#0.00") + " MB";
                }

                lblType.Text = GetFileTypeByExtension(fileInfo.Extension);



            }
            catch { 
            
            }
            



        }
      


    }
    protected void gridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridList.PageIndex = e.NewPageIndex;

        LoadGallery();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadGallery();
    }

    protected void LoadGallery()
    {
        NDEnt = new notes_detail();
        NDEnt.PROGRAMID = ddlProgram.SelectedValue;
        NDEnt.SEMESTERID = ddlSemester.SelectedValue;
        NDEnt.SUBJECT_ID = ddlSubject.SelectedValue;
        
        gridList.DataSource = NDSer.GetAll(NDEnt);
        gridList.DataBind();

    }

    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
         GridViewRow gr = ((ImageButton)sender).Parent.Parent as GridViewRow;

         Label lblLinkName = gr.FindControl("lblLinkName") as Label;
         Label lblFileName = gr.FindControl("lblFileName") as Label;
         Label lblSize = gr.FindControl("lblSize") as Label;


         System.IO.FileInfo file = new System.IO.FileInfo(lblLinkName.Text);
         if (lblSize.Text != "")
         {

             try
             {
                 Response.Clear();
                 Response.ContentType = "application/octet-stream";
                 Response.AppendHeader("content-disposition", "attachment; filename="+lblFileName.Text+file.Extension);
                 Response.TransmitFile(lblLinkName.Text);
                 Response.End();
             }
             catch
             {
                 HelperFunction.MsgBox(this, this.GetType(), "File Not Found");

             }
         }
         else
         {
             HelperFunction.MsgBox(this, this.GetType(), "File Not Found");
         }
           
        }
        else { }
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

            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, "Select");


        }

    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadBatch();
            LoadSemester();

        }
        else
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, "Select");


        }
        if (ddlBatch.SelectedValue == "Select")
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, "Select");


        }
    }
}