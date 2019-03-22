using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.IO;
using System.Drawing.Imaging;
using BarcodeLib;
using Entity.Components;
using Service.Components;
using Entity.Framework;
using System.Collections;

public partial class library_masterdata_Barcode_Generate : System.Web.UI.Page
{


    book BKEnt = new book();
    bookService BKSer = new bookService();

    booktype BTEnt = new booktype();
    booktypeService BTSer = new booktypeService();

    bookdetails BDEnt = new bookdetails();
    bookdetailsService BDSer = new bookdetailsService();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBookType();
            LoadBookName();


        }

    }



    protected void LoadBookType()
    {
        BTEnt = new booktype();
        ddlBookType.DataSource = BTSer.GetAll(BTEnt);
        ddlBookType.DataTextField = "Booktype";
        ddlBookType.DataValueField = "Booktypeid";
        ddlBookType.DataBind();

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        EntityList theList = new EntityList();
        int from_booknumber = Convert.ToInt32(txtFromNumber.Text);
        int to_booknumber = Convert.ToInt32(txtToNumber.Text);

        if (from_booknumber <= to_booknumber)
        {

            for (int i = from_booknumber; i <= to_booknumber; i++)
            {

                BDEnt = new bookdetails();
                BDEnt.Bookid = ddlBookName.SelectedValue;
                BDEnt.Booknumber = i.ToString("000");
                BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
                if (BDEnt != null)
                {
                    theList.Add(BDEnt);
                }
            }

        }

        BDEnt = new bookdetails();
        gridBarcode.DataSource = theList;
        gridBarcode.DataBind();

        if (theList.Count > 0)
        {



            // Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;

            //Bitmap bitmap=new Bitmap(barcode.Draw(txtNSBN.Text, 80));
            //bitmap.Save("E:\\button.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            Array.ForEach(Directory.GetFiles(@"C:\inetpub\wwwroot\college\images\barcode\"), File.Delete);

            if (from_booknumber <= to_booknumber)
            {

                for (int i = from_booknumber; i <= to_booknumber; i++)
                {

                    BDEnt = new bookdetails();
                    BDEnt.Bookid = ddlBookName.SelectedValue;
                    BDEnt.Booknumber = i.ToString("000");
                    BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
                    if (BDEnt != null)
                    {
                        BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
                        {
                            IncludeLabel = true,
                            Alignment = AlignmentPositions.CENTER,
                            Width = 200,
                            Height = 100,
                            RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                            BackColor = Color.White,
                            ForeColor = Color.Black,

                        };

                        Bitmap bitmap = new Bitmap(barcode.Encode(TYPE.CODE128B, BDEnt.NSBN));

                        bitmap.Save(Server.MapPath("~/images/barcode/" + BDEnt.NSBN + ".jpg"), ImageFormat.Jpeg);

                    }
                }
            }



            string nsbnlist = "";
            int count = 0;

            foreach (GridViewRow gr in gridBarcode.Rows)
            {

                Label lblNSBN = gr.FindControl("lblNSBN") as Label;
                if (count < 5)
                {
                    nsbnlist += lblNSBN.Text + ",";
                    count++;
                }
                if (count == 5)
                {
                    nsbnlist = nsbnlist.Substring(0, nsbnlist.Length - 1);
                    nsbnlist += ";";
                    count = 0;
                }
            }
            nsbnlist = nsbnlist.Substring(0, nsbnlist.Length - 1);



            DataTable tbl = new DataTable();
            tbl.Columns.Add("Col1");
            tbl.Columns.Add("Col2");
            tbl.Columns.Add("Col3");
            tbl.Columns.Add("Col4");
            tbl.Columns.Add("Col5");

            string[] array = nsbnlist.Split(';');

            foreach (string s in array)
            {
                DataRow row = tbl.NewRow();
                string[] numb = s.Split(',');
                try
                {
                    if (numb.Length == 5)
                    {
                        row["Col1"] = numb[0];
                        row["Col2"] = numb[1];
                        row["Col3"] = numb[2];
                        row["Col4"] = numb[3];
                        row["Col5"] = numb[4];

                        tbl.Rows.Add(row);
                    }
                    if (numb.Length == 4)
                    {
                        row["Col1"] = numb[0];
                        row["Col2"] = numb[1];
                        row["Col3"] = numb[2];
                        row["Col4"] = numb[3];


                        tbl.Rows.Add(row);
                    }
                    if (numb.Length == 3)
                    {
                        row["Col1"] = numb[0];
                        row["Col2"] = numb[1];
                        row["Col3"] = numb[2];


                        tbl.Rows.Add(row);
                    }
                    if (numb.Length == 2)
                    {
                        row["Col1"] = numb[0];
                        row["Col2"] = numb[1];



                        tbl.Rows.Add(row);
                    }
                    if (numb.Length == 1)
                    {
                        row["Col1"] = numb[0];




                        tbl.Rows.Add(row);
                    }

                }
                catch { }
            }

            gridView.DataSource = tbl;
            gridView.DataBind();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Books are Not Available");
        }

    }


    protected void ddlBookType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBookName();
    }

    protected void LoadBookName()
    {
        BKEnt = new book();
        BKEnt.Booktypeid = ddlBookType.SelectedValue;
        ddlBookName.DataSource = BKSer.GetAll(BKEnt);
        ddlBookName.DataTextField = "Bookname";
        ddlBookName.DataValueField = "Bookid";
        ddlBookName.DataBind();
    }

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string lblNSBN = "lblNSBN";
            string img = "img";
            string lblHeader = "lblHeader";
            for (int i = 1; i <= 5; i++)
            {
                Label lblNSBNn = e.Row.FindControl(lblNSBN + i.ToString()) as Label;
                Label lblHeadern = e.Row.FindControl(lblHeader + i.ToString()) as Label;
                System.Web.UI.WebControls.Image imgn = e.Row.FindControl(img + i.ToString()) as System.Web.UI.WebControls.Image;

                if (lblNSBNn.Text != "")
                {
                    imgn.Visible = true;
                    lblHeadern.Visible = true;
                    imgn.ImageUrl = "~/images/barcode/" + lblNSBNn.Text + ".jpg";
                }

                else
                {
                    lblHeadern.Visible = false;
                    imgn.Visible = false;
                }
            }




        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
}