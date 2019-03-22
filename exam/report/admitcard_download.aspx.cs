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

public partial class class_routine_reports_download_notes : System.Web.UI.Page
{

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    admit_card ADMEnt = new admit_card();
    admit_cardService ADMSer = new admit_cardService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    bool flag = false;
    string name = "";
    ArrayList alist = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBatch();
            LoadSemester();
         
        }
    }
    protected void LoadBatch()
    {
        BTCEnt = new BatchYear();
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
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

            ddlSemester.DataSource = SMSer.GetAll(SMEnt);
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");
        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
       
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

            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;



            SEnt = new HSS_STUDENT();
            SEnt.STUDENT_ID = lblStudentId.Text;
            SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
            if (SEnt != null)
            {
                lblStudentName.Text = SEnt.NAME_ENGLISH;
            }

            //string strFileName = Server.MapPath(lblLinkName.ToString());


            try
            {

                FileInfo fileInfo = new FileInfo(lblLinkName.Text);

                string url=lblLinkName.Text.Replace("C:\\inetpub\\wwwroot\\CMS","~");
                img.ImageUrl = url;
               
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

        ADMEnt = new admit_card();
        ADMEnt.BATCH = ddlBatch.SelectedValue;
        ADMEnt.SEMESTER = ddlSemester.SelectedValue;

        gridList.DataSource = ADMSer.GetAll(ADMEnt);
        gridList.DataBind();

    }

    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
         GridViewRow gr = ((ImageButton)sender).Parent.Parent as GridViewRow;

         Label lblLinkName = gr.FindControl("lblLinkName") as Label;
         Label lblStudentId = gr.FindControl("lblStudentId") as Label;
         Label lblSize = gr.FindControl("lblSize") as Label;


         System.IO.FileInfo file = new System.IO.FileInfo(lblLinkName.Text);
         if (lblSize.Text != "")
         {

             try
             {
                 Response.Clear();
                 Response.ContentType = "application/octet-stream";
                 Response.AppendHeader("content-disposition", "attachment; filename=" + lblStudentId.Text + file.Extension);
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
}