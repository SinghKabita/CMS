using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Reflection;
using Entity.Components;
using Service.Components;
using DataAccess.Components;
using DataAccess.Framework;
using DataHelper.Framework;
using Oracle.DataAccess.Client;
using Entity.Framework;
using System.IO;
using System.Net;
using System.Xml.XPath;




/// <summary>
/// Summary description for HelperFunction
/// </summary>
public class HelperFunction
{
    Pages PGEnt = new Pages();
    PagesService PGSer = new PagesService();

    #region menu bar
    public DataTable getModule(string groupid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_useracces.getmodule";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = groupid;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }

    public DataTable getSubModule(string groupid, string moduleid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_useracces.getsubmodule";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = groupid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = moduleid;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }

    public DataTable getlinkname(string moduleid, string submoduleid, string groupid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_useracces.getlinknameparent";


        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = moduleid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = submoduleid;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = groupid;
        objCmd.Parameters.Add(_p3);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }

    #endregion

    public string ConvertDtToJSon(DataTable dt)
    {

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            { row.Add(col.ColumnName.Trim(), dr[col]); }
            rows.Add(row);
        }
        return serializer.Serialize(rows);



    }

    // function created by rohit................. to convert data table into json format
    public void DataTableToJSON(DataTable table, string filename)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            JSONString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        }

        System.IO.File.WriteAllText(@"F:\" + filename + ".txt", JSONString.ToString());


    }


    public void DataTableToXML(DataTable table, string filename)
    {
        DataSet dataSet = new DataSet();


        // Add datatable to dataset object
        dataSet.Tables.Add(table);

        // Save to a file
        dataSet.WriteXml(@"F:\" + filename + ".xml");

    }



    private static Hashtable m_executingPages = new Hashtable();





    public HelperFunction()
    {
        // TODO: Add constructor logic here
        //       
    }





    public static void MsgBox(Control updatePanUsed, Type UpdatePanType, string alert)
    {
        Guid gMessage = Guid.NewGuid();
        string script = @"alert('" + alert + "');";
        ScriptManager.RegisterStartupScript(updatePanUsed, UpdatePanType, gMessage.ToString(), script, true);
    }

    public static void OpenNewWindow(Control updatePanUsed, Type UpdatePanType, string url)
    {
        Guid gMessage = Guid.NewGuid();
        string script = @"window.open('" + url + "','','location=0,status=0,scrollbars=1,resizable=1 ');";
        ScriptManager.RegisterStartupScript(updatePanUsed, UpdatePanType, gMessage.ToString(), script, true);
    }

    public static void Show(string sMessage)
    {
        // If this is the first time a page has called this method then
        if (!m_executingPages.Contains(HttpContext.Current.Handler))
        {
            // Attempt to cast HttpHandler as a Page.
            Page executingPage = HttpContext.Current.Handler as Page;
            if (executingPage != null)
            {
                // Create a Queue to hold one or more messages.
                Queue messageQueue = new Queue();
                // Add our message to the Queue
                messageQueue.Enqueue(sMessage);
                // Add our message queue to the hash table. Use our page reference
                // (IHttpHandler) as the key.
                m_executingPages.Add(HttpContext.Current.Handler, messageQueue);
                // Wire up Unload event so that we can inject 
                // some JavaScript for the alerts.
                executingPage.Unload += new EventHandler(ExecutingPage_Unload);
            }
        }
        else
        {
            // If were here then the method has allready been 
            // called from the executing Page.
            // We have allready created a message queue and stored a
            // reference to it in our hastable. 
            Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];
            // Add our message to the Queue
            queue.Enqueue(sMessage);
        }
    }
    private static void ExecutingPage_Unload(object sender, EventArgs e)
    {
        // Get our message queue from the hashtable

        // Our page has finished rendering so lets output the
        // JavaScript to produce the alert's

        Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];
        if (queue != null)
        {
            StringBuilder sb = new StringBuilder();
            // How many messages have been registered?
            int iMsgCount = queue.Count;
            // Use StringBuilder to build up our client slide JavaScript.
            sb.Append("<script language='javascript'>");
            // Loop round registered messages
            string sMsg;
            while (iMsgCount-- > 0)
            {
                sMsg = (string)queue.Dequeue();
                sMsg = sMsg.Replace("\n", "\\n");
                sMsg = sMsg.Replace("\"", "'");
                sb.Append(@"alert( """ + sMsg + @""" );");
            }
            // Close our JS
            sb.Append(@"</script>");
            // Were done, so remove our page reference from the hashtable
            m_executingPages.Remove(HttpContext.Current.Handler);
            // Write the JavaScript to the end of the response stream.
            HttpContext.Current.Response.Write(sb.ToString());
        }
    }


    //by nibesh
    public string EnryptString(string strEncrypted)
    {
        byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
        string encrypted = Convert.ToBase64String(b);
        return encrypted;
    }
    public string DecryptString(string encrString)
    {
        byte[] b;
        string decrypted;
        try
        {
            b = Convert.FromBase64String(encrString);
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
        }
        catch (FormatException fe)
        {
            decrypted = "";
        }
        return decrypted;
    }


    public string CheckDate(string str)
    {
        string[] shortdate = str.Split(' ');
        return shortdate[0];
    }
    public string NepaliMonths1EnglishDate(string NepaliYear, string NepaliMonth)
    {
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        short NYear = short.Parse(NepaliYear);
        short NMonth = short.Parse(NepaliMonth);
        short NDay = 1;
        nepalidate.SetNepaliDate(NYear, NMonth, NDay);
        DateTime EngDate = nepalidate.GetEnglishDate();
        return EngDate.GetDateTimeFormats()[7].ToString();

    }

    public int NepaliMonthsLastDay(string NepaliYear, string NepaliMonth)
    {
        Boolean Found = false;
        int i = 32;
        short day = 32;
        short month = Convert.ToInt16(NepaliMonth);
        short year = Convert.ToInt16(NepaliYear);
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        while (Found == false)
        {
            try
            {
                nepalidate.SetNepaliDate(year, month, day);
                Found = true;
            }
            catch
            {
                i = i - 1;
                day = (short)i;
            }
        }
        return day;

    }
    public void moveToNextControl(Control control)
    {
        int counter = 0;
        foreach (Control c in control.Controls)
        {
            if (c.Controls.Count > 0)
            {
                moveToNextControl(c);
            }
            else if (c is TextBox)
            {
                ((TextBox)(c)).Attributes.Add("onkeydown", "moveToNext(this,false," + counter + ")");

            }
            else if (c is Button)
            {
                ((Button)(c)).UseSubmitBehavior = false;

            }
            else if (c is RadioButton)
            {
                ((RadioButton)(c)).Attributes.Add("onkeydown", "moveToNext(this,false)");
            }
            else if (c is CheckBox)
            {
                ((CheckBox)(c)).Attributes.Add("onkeydown", "moveToNext(this,false)");
            }
            else if (c is DropDownList)
            {
                ((DropDownList)(c)).Attributes.Add("onkeydown", "moveToNext(this,false)");
            }
            else if (c is ListBox)
            {
                ((ListBox)(c)).Attributes.Add("onkeydown", "moveToNext(this,false)");
            }
        }
    }
    public string checkFiscalYear(string month, string year)
    {
        string FY = "";
        try
        {
            int MM, CY, AY;

            MM = Int32.Parse(month);
            CY = Int32.Parse(year);
            if (MM < 4)
            {
                AY = CY - 1;
                FY = AY.ToString() + "/" + CY.ToString().Substring(2, 2);

            }
            else if (MM >= 4)
            {
                AY = CY + 1;
                FY = CY.ToString() + "/" + AY.ToString().Substring(2, 2);
            }
            else
            { }
        }
        catch
        {

        }
        return FY;
    }

    public string CurrentYear(string month, string year)
    {
        string FY = "";
        try
        {
            int MM, CY, AY;

            MM = Int32.Parse(month);
            CY = Int32.Parse(year);
            if (MM < 3)
            {
                AY = CY - 1;
                FY = AY.ToString() + "_" + CY.ToString().Substring(2, 2);

            }
            else if (MM >= 3)
            {
                AY = CY + 1;
                FY = CY.ToString() + "_" + AY.ToString().Substring(2, 2);
            }
            else
            { }
        }
        catch
        {

        }
        return FY;
    }



    public string ConvertNepaliTOEnglish(string day, string month, string year)
    {
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        try
        {
            nepalidate.SetNepaliDate(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
            DateTime ENGDate = nepalidate.GetEnglishDate();
            return ENGDate.ToString("dd.MMM.yyyy");
        }
        catch
        {
            try
            {
                nepalidate.SetNepaliDate(Convert.ToInt16((year)), Convert.ToInt16((month)), Convert.ToInt16((day)));
                DateTime ENGDate = nepalidate.GetEnglishDate();
                return ENGDate.ToString("dd.MMM.yyyy");

            }
            catch
            {
                return "";
            }
            return "";
        }

    }

    public string ConvertNepaliTOEnglishDate(string day, string month, string year)
    {
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        try
        {
            nepalidate.SetNepaliDate(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
            DateTime ENGDate = nepalidate.GetEnglishDate();
            return ENGDate.ToString("dd/MM/yyyy");
        }
        catch
        {
            try
            {
                nepalidate.SetNepaliDate(Convert.ToInt16((year)), Convert.ToInt16((month)), Convert.ToInt16((day)));
                DateTime ENGDate = nepalidate.GetEnglishDate();
                return ENGDate.ToString("dd/MM/yyyy");

            }
            catch
            {
                return "";
            }
            return "";
        }

    }
    public DateTime ConvertNepaliTOEnglishDateTime(string day, string month, string year)
    {

        DateTime ENGDate = new DateTime();
        try
        {
            NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();

            nepalidate.SetNepaliDate(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
            ENGDate = nepalidate.GetEnglishDate();
            return ENGDate;
        }
        catch
        {
            return ENGDate;
        }

    }
    public string ConvertNepaliTOEnglishShortMonth(string day, string month, string year)
    {


        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        nepalidate.SetNepaliDate(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
        DateTime ENGDate = nepalidate.GetEnglishDate();
        return ENGDate.GetDateTimeFormats()[7].ToString();
    }
    public string ConvertNepaliTOEnglishMonthChar(string day, string month, string year)
    {


        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        nepalidate.SetNepaliDate(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
        DateTime ENGDate = nepalidate.GetEnglishDate();
        return ENGDate.GetDateTimeFormats()[7].ToString();

    }

    public Boolean ValidateDate(string day, string month, string year)
    {
        string strDate = "";

        strDate = this.ConvertNepaliTOEnglish(day, month, year);
        if (strDate == "")
            return false;
        else
            return true;
    }

    public Boolean ValidateDateEnglish(string day, string month, string year)
    {
        DateTime strDate;
        try
        {
            strDate = Convert.ToDateTime(month + "/" + day + "/" + year);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public Boolean ValidateDouble(string Value)
    {
        try
        {
            double output = Convert.ToDouble(Value);
            return true;
        }
        catch
        {

            if (ValidateInt(Value) == true)
            {
                return true;
            }

            return false;
        }
    }
    public Boolean ValidateInt(string Value)
    {
        try
        {
            double output = Convert.ToInt64(Value);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public string getStudentName(string studentid)
    {
        string studentname = "";
        HSS_STUDENT SEnt = new HSS_STUDENT();
        HSS_STUDENTService SSer = new HSS_STUDENTService();

        SEnt.STUDENT_ID = studentid;

        SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
        if (SEnt != null)
        {

            studentname = SEnt.NAME_ENGLISH;
        }
        return studentname;

    }

    public string GetSingleNepaliDate(string EnglishDate)
    {
        short Nyear;
        short Nmonth;
        short Nday;
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        DateTime englishDate = new DateTime();
        try
        {
            englishDate = Convert.ToDateTime(EnglishDate);
        }
        catch
        {
            englishDate = DateTime.Today;
        }
        nepalidate.SetEnglishDate(englishDate.Date);
        nepalidate.GetNepaliDate(out Nyear, out Nmonth, out Nday);
        string NepaliDate = Nday.ToString() + "/" + Nmonth.ToString() + "/" + Nyear.ToString();
        return NepaliDate;
    }
    public string GetSingleNepaliDateNew(string EnglishDate)
    {
        short Nyear;
        short Nmonth;
        short Nday;
        string[] splitdate = EnglishDate.Split('/');
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        DateTime englishDate = new DateTime();
        try
        {
            englishDate = new DateTime(Convert.ToInt16(splitdate[2]), Convert.ToInt16(splitdate[1]), Convert.ToInt16(splitdate[0]));
        }
        catch
        {
            englishDate = DateTime.Today;
        }
        nepalidate.SetEnglishDate(englishDate.Date);
        nepalidate.GetNepaliDate(out Nyear, out Nmonth, out Nday);
        string NepaliDate = Nday.ToString() + "/" + Nmonth.ToString() + "/" + Nyear.ToString();
        return NepaliDate;
    }
    public string NepaliDay()
    {
        short Nyear;
        short Nmonth;
        short Nday;
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        DateTime englishDate = new DateTime();
        englishDate = DateTime.Today;
        nepalidate.SetEnglishDate(englishDate.Date);
        nepalidate.GetNepaliDate(out Nyear, out Nmonth, out Nday);
        return Nday.ToString();
    }
    public string NepaliMonth()
    {
        short Nyear;
        short Nmonth;
        short Nday;
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        DateTime englishDate = new DateTime();
        englishDate = DateTime.Today;
        nepalidate.SetEnglishDate(englishDate.Date);
        nepalidate.GetNepaliDate(out Nyear, out Nmonth, out Nday);
        return Nmonth.ToString();
    }
    public string NepaliYear()
    {
        short Nyear;
        short Nmonth;
        short Nday;
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        DateTime englishDate = new DateTime();
        englishDate = DateTime.Today;
        nepalidate.SetEnglishDate(englishDate.Date);
        nepalidate.GetNepaliDate(out Nyear, out Nmonth, out Nday);

        return Nyear.ToString();
    }
    public string CheckDemandDate(string str)
    {
        //string[] shortdate = str.Split(' ');
        string[] shortdates = str.Split('/');
        string day = shortdates[0];
        string month = shortdates[1];
        string year = shortdates[2];
        int DD = Int32.Parse(day);
        int MM = Int32.Parse(month);
        int YY = Int32.Parse(year);

        if (DD <= 7)
        {
            MM = MM - 1;
            if (MM == 1)
            {
                DD = 31 - (7 - DD);
            }
            else if (MM == 2)
            {
                DD = 28 - (7 - DD);
            }
            else if (MM == 3)
            {
                DD = 31 - (7 - DD);
            }
            else if (MM == 4)
            {
                DD = 30 - (7 - DD);
            }
            else if (MM == 5)
            {
                DD = 31 - (7 - DD);
            }
            else if (MM == 6)
            {
                DD = 30 - (7 - DD);
            }
            else if (MM == 7)
            {
                DD = 31 - (7 - DD);
            }
            else if (MM == 8)
            {
                DD = 30 - (7 - DD);
            }
            else if (MM == 9)
            {
                DD = 31 - (7 - DD);
            }
            else if (MM == 10)
            {
                DD = 30 - (7 - DD);
            }
            else if (MM == 11)
            {
                DD = 31 - (7 - DD);
            }
            else if (MM == 12)
            {
                DD = 30 - (7 - DD);

            }
            else if (MM == 0)
            {
                MM = 12;
                DD = 30 - (7 - DD);
                YY = YY - 1;
            }
        }
        else
        {
            DD = DD - 7;
        }
        string d = DD.ToString();
        string m = MM.ToString();
        string y = YY.ToString();
        string DATE = d + "/" + m + "/" + y;
        return DATE;
    }

    //...............function created by rohit.............
    public string GetTodayDate()
    {
        string day = NepaliDay();
        string month = NepaliMonth();
        string year = NepaliYear();
        string todaydate = ConvertNepaliTOEnglishDate(day, month, year);
        return todaydate;

    }



    public string NepaliMonths(string calMth)
    {
        //current month name
        string MonthName = "";
        if (calMth == "01")
        {
            MonthName = "वैशाख";
        }
        else if (calMth == "02")
        {
            MonthName = "जेठ";
        }
        else if (calMth == "03")
        {
            MonthName = "असार";
        }
        else if (calMth == "04")
        {
            MonthName = "साउन";
        }
        else if (calMth == "05")
        {
            MonthName = "भदौ";
        }
        else if (calMth == "06")
        {
            MonthName = "असोज";
        }
        else if (calMth == "07")
        {
            MonthName = "कार्तिक";
        }
        else if (calMth == "08")
        {
            MonthName = "मङ्‌सिर";
        }
        else if (calMth == "09")
        {
            MonthName = "पुस";
        }
        else if (calMth == "10")
        {
            MonthName = "माघ";
        }
        else if (calMth == "11")
        {
            MonthName = "फागुन";
        }
        else if (calMth == "12")
        {
            MonthName = "चैत";
        }
        return MonthName;
    }
    public string SplitNepaliDateToEnglish(string nepalidate)
    {
        string[] shortdates = nepalidate.Split('/');
        string day = shortdates[0];
        string month = shortdates[1];
        string year = shortdates[2];
        string englishdate;
        englishdate = ConvertNepaliTOEnglish(day, month, year);
        return englishdate;
    }


    //To convert number to words
    #region Dec - Arrays
    private string[] arrOnes = new string[] {
            "Zero",
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine"
        };
    private string[] arrMisc = new string[] {
            "",
            "Eleven",
            "Twelve",
            "Thirteen",
            "Fourteen",
            "Fifteen",
            "Sixteen",
            "Seventeen",
            "Eighteen",
            "Nineteen",
        };
    private string[] arrTens = new string[] {
            "",
            "Ten",
            "Twenty",
            "Thirty",
            "Forty",
            "Fifty",
            "Sixty",
            "Seventy",
            "Eighty",
            "Ninety",
        };

    #endregion

    #region Constants
    private const string HUNDRED_TEXT = " Hundred";
    private const string THOUSAND_TEXT = " Thousand";
    private const string LAKH_TEXT = " Lakh";
    private const string CORORE_TEXT = " Crore";
    private const string ARAB_TEXT = " Arab";
    private const string KHARAB_TEXT = " Kharab";
    private const string AND_TEXT = " and ";
    private const string POINT_TEXT = " point ";
    private const string TOO_HIGH_TEXT = "Too high value";

    private const int ONE = 1;
    private const int TEN = 2;
    private const int HUNDRED = 3;
    private const int THOUSAND = 4;
    private const int TENTHOUSAND = 5;
    private const int LAKH = 6;
    private const int TENLAKH = 7;
    private const int CORORE = 8;
    private const int TENCORORE = 9;
    private const int ARAB = 10;
    private const int TENARAB = 11;
    private const int KHARAB = 12;
    private const int TENKHARAB = 13;
    private const int MAX_ALLOWED_LENGTH = TENKHARAB;

    public const int ERR_TOOLENGTH = -1;
    public const int ERR_INPUT = -2;
    public const int RET_SUCCESS = 0;
    #endregion

    public int ConvertNumberToText(String sNumber, out string sResult)
    {
        sResult = "";
        string[] str = sNumber.Split(new char[] { '.' });
        string sStrAfterDecimal = str.Length > 1 ? str[1] : "";
        string sStrBeforeDecimal = str.Length >= 1 ? str[0] : "";

        if (sStrBeforeDecimal.Length > MAX_ALLOWED_LENGTH)
        {
            sResult = TOO_HIGH_TEXT;
            return ERR_TOOLENGTH;
        }

        if (GetNumberText(sStrBeforeDecimal, false, ref sResult) == RET_SUCCESS)
        {
            //if (sStrAfterDecimal != "")
            //{
            if (sStrAfterDecimal != "00" & sStrAfterDecimal != "")
            {
                sResult += AND_TEXT;
                if (GetNumberText(sStrAfterDecimal, true, ref sResult) != RET_SUCCESS)
                {
                    sResult = "Error while computing..";
                    return ERR_INPUT;
                }
            }
            //}
            sResult += " Only";
            return RET_SUCCESS;
        }
        else
        {
            sResult = "Error while computing..";
            return ERR_INPUT;
        }
    }

    private string GetTwoDigitString(string str)
    {
        if (str == "00") return "";
        string retString = "";
        if (str[1] == '0')
            retString += arrTens[int.Parse(str[0].ToString())];
        else if (str[0] == '1')
            retString += arrMisc[int.Parse(str[1].ToString())];
        else
        {
            retString += arrTens[int.Parse(str[0].ToString())] + " ";
            retString += arrOnes[int.Parse(str[1].ToString())];
        }
        return retString;
    }

    // To Convert English Number to nepali unicode
    public String getNepaliText(String item)
    {
        String str = item;
        String newStr = "";
        int len = str.Length;
        int i;
        for (i = 0; i < len; i++)
        {
            switch (str.Substring(i, 1))
            {
                case "0":
                    newStr = newStr + "\u0966";
                    break;

                case "1":
                    newStr = newStr + "\u0967";
                    break;

                case "2":
                    newStr = newStr + "\u0968";
                    break;

                case "3":
                    newStr = newStr + "\u0969";
                    break;

                case "4":
                    newStr = newStr + "\u096a";
                    break;

                case "5":
                    newStr = newStr + "\u096b";
                    break;

                case "6":
                    newStr = newStr + "\u096c";
                    break;

                case "7":
                    newStr = newStr + "\u096d";
                    break;

                case "8":
                    newStr = newStr + "\u096e";
                    break;

                case "9":
                    newStr = newStr + "\u096f";
                    break;

                case "/":
                    newStr = newStr + "/";
                    break;

                default:
                    newStr = newStr + str.Substring(i, 1);
                    break;
            }
        }
        return newStr;
    }

    // To Convert English Number to nepali unicode end

    // To Convert  nepali unicode to English Number 
    public String getEnglishText(String item)
    {
        String str = item;
        String newStr = "";
        int len = str.Length;
        int i;
        for (i = 0; i < len; i++)
        {
            switch (str.Substring(i, 1))
            {
                case "\u0966":
                    newStr = newStr + "0";
                    break;

                case "\u0967":
                    newStr = newStr + "1";
                    break;

                case "\u0968":
                    newStr = newStr + "2";
                    break;

                case "\u0969":
                    newStr = newStr + "3";
                    break;

                case "\u096a":
                    newStr = newStr + "4";
                    break;

                case "\u096b":
                    newStr = newStr + "5";
                    break;

                case "\u096c":
                    newStr = newStr + "6";
                    break;

                case "\u096d":
                    newStr = newStr + "7";
                    break;

                case "\u096e":
                    newStr = newStr + "8";
                    break;

                case "\u096f":
                    newStr = newStr + "9";
                    break;

                case "/":
                    newStr = newStr + "/";
                    break;

                default:
                    newStr = newStr + str.Substring(i, 1);
                    break;
            }
        }
        return newStr;
    }

    public static void EmptyGridFix(GridView grdView)
    {

        if (grdView.Rows.Count == 0 && grdView.DataSource != null)
        {
            System.Data.DataTable dt = null;
            // need to clone sources otherwise it will be indirectly adding to the original source
            if (grdView.DataSource is DataSet)
            {
                dt = ((DataSet)grdView.DataSource).Tables[0].Clone();
            }
            else if (grdView.DataSource is System.Data.DataTable)
            {
                dt = ((System.Data.DataTable)grdView.DataSource).Clone();
            }
            if (dt == null)
            {
                return;
            }
            dt.Rows.Add(dt.NewRow()); // add empty row
            grdView.DataSource = dt;
            grdView.DataBind();
            // hide row
            grdView.Rows[0].Visible = false;
            grdView.Rows[0].Controls.Clear();
        }


        // normally executes at all postbacks
        if (grdView.Rows.Count == 1 && grdView.DataSource == null)
        {
            bool bIsGridEmpty = true;
            // check first row that all cells empty
            for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
            {
                if (grdView.Rows[0].Cells[i].Text != string.Empty)
                {
                    bIsGridEmpty = false;
                }
            }
            // hide row
            if (bIsGridEmpty)
            {
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }
        }
    }

    public void GetPageName(Label lblPageName, String page)
    {
        string CurrentModule = HttpContext.Current.Session["UserModule"].ToString();
        System.IO.FileInfo oInfo = new System.IO.FileInfo(page);
        int startIndex = Convert.ToInt16(oInfo.ToString().IndexOf('/', 1));
        string newPage = oInfo.ToString().Substring(startIndex + 1);
        //Pages pageEntity = new Pages();
        //PagesService pageService = new PagesService();
        //pageEntity.PAGENAME = newPage;
        //pageEntity = (Pages)pageService.GetSingle(pageEntity);
        //lblPageName.Text = pageEntity.LINKNAME;
        try
        {
            lblPageName.Text = Convert.ToString(DataAccessHelper.ExecuteScalar("PKJ_Select.selectPageTitle", CommandType.StoredProcedure, CreatePageTitleParmans(newPage, CurrentModule)));
        }
        catch { }
    }
    private IDbDataParameter[] CreatePageTitleParmans(string pagename, string module_id)
    {
        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        cmdParams.Add(DataAccessFactory.CreateDataParameter("page_name", pagename));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("module_id", module_id));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));
        return cmdParams.ToArray();
    }

    private IDbDataParameter[] CreateNullParmans()
    {
        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));
        return cmdParams.ToArray();
    }

    public int GetNumberText(string str, bool isAfterDecimal, ref string retStr)
    {
        bool addAndString = false;
        try
        {
            if (isAfterDecimal && str.Length >= 1)
            {
                if (str[0] == '0')
                    retStr += arrOnes[int.Parse(str[1].ToString())];
                else if (str[0] == '1')
                    retStr += arrMisc[int.Parse(str[1].ToString())];
                else
                {
                    retStr += arrTens[int.Parse(str[0].ToString())] + " ";
                    if (str.Length > 1)
                        retStr += arrOnes[int.Parse(str[1].ToString())];
                }
                retStr += " Paisa ";
                //for (int i = 0; i < str.Length; i++)
                //    retStr += arrOnes[int.Parse(str[i].ToString())] + " ";
                //retStr = retStr.Remove(retStr.Length - 1);
            }
            else
            {
                while (str.Length > 0)
                {
                    switch (str.Length)
                    {
                        case ONE:
                            retStr += arrOnes[int.Parse(str[0].ToString())];
                            str = "";
                            break;
                        case TEN:
                            {
                                string temp = GetTwoDigitString(str);
                                if (temp != "")
                                    retStr += addAndString ? AND_TEXT + temp : temp;
                                str = "";
                            }
                            break;
                        case HUNDRED:
                            if (str[0] != '0')
                                retStr += string.Format("{0}{1}", arrOnes[int.Parse(str[0].ToString())], HUNDRED_TEXT);

                            if (str.Substring(1) != "00")
                                addAndString = true;
                            str = str.Remove(0, 1);
                            break;
                        case THOUSAND:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], THOUSAND_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENTHOUSAND:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, THOUSAND_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        case LAKH:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], LAKH_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENLAKH:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, LAKH_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        case CORORE:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], CORORE_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENCORORE:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, CORORE_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;


                        // changes by binod
                        case ARAB:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], ARAB_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENARAB:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, ARAB_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        case KHARAB:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], KHARAB_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENKHARAB:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, KHARAB_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        //changes end

                        default:
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return ERR_INPUT;
        }
        return RET_SUCCESS;
    }
    public int GetNumberTextDollar(string str, bool isAfterDecimal, ref string retStr)
    {
        bool addAndString = false;
        try
        {
            if (isAfterDecimal && str.Length >= 1)
            {
                if (str[0] == '0')
                    retStr += arrOnes[int.Parse(str[1].ToString())];
                else if (str[0] == '1')
                    retStr += arrMisc[int.Parse(str[1].ToString())];
                else
                {
                    retStr += arrTens[int.Parse(str[0].ToString())] + " ";
                    retStr += arrOnes[int.Parse(str[1].ToString())];
                }
                retStr += " Cents ";
                //for (int i = 0; i < str.Length; i++)
                //    retStr += arrOnes[int.Parse(str[i].ToString())] + " ";
                //retStr = retStr.Remove(retStr.Length - 1);
            }
            else
            {
                while (str.Length > 0)
                {
                    switch (str.Length)
                    {
                        case ONE:
                            retStr += arrOnes[int.Parse(str[0].ToString())];
                            str = "";
                            break;
                        case TEN:
                            {
                                string temp = GetTwoDigitString(str);
                                if (temp != "")
                                    retStr += addAndString ? AND_TEXT + temp : temp;
                                str = "";
                            }
                            break;
                        case HUNDRED:
                            if (str[0] != '0')
                                retStr += string.Format("{0}{1}", arrOnes[int.Parse(str[0].ToString())], HUNDRED_TEXT);

                            if (str.Substring(1) != "00")
                                addAndString = true;
                            str = str.Remove(0, 1);
                            break;
                        case THOUSAND:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], THOUSAND_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENTHOUSAND:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, THOUSAND_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        case LAKH:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], LAKH_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENLAKH:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, LAKH_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        case CORORE:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], CORORE_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENCORORE:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, CORORE_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;


                        // changes by binod
                        case ARAB:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], ARAB_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENARAB:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, ARAB_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        case KHARAB:
                            retStr += string.Format("{0}{1} ", arrOnes[int.Parse(str[0].ToString())], KHARAB_TEXT);
                            str = str.Remove(0, 1);
                            break;
                        case TENKHARAB:
                            {
                                string temp = GetTwoDigitString(str.Substring(0, 2));
                                if (temp != "")
                                    retStr += string.Format("{0}{1} ", temp, KHARAB_TEXT);
                                str = str.Remove(0, 2);
                            }
                            break;
                        //changes end

                        default:
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return ERR_INPUT;
        }
        return RET_SUCCESS;
    }

    public static DataTable ListToDataTable<T>(IList<T> list)
    {
        DataTable dt = new DataTable();

        foreach (PropertyInfo info in typeof(T).GetProperties())
        {
            dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
        }
        foreach (T t in list)
        {
            DataRow row = dt.NewRow();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                row[info.Name] = info.GetValue(t, null);
            }
            dt.Rows.Add(row);
        }
        return dt;
    }
    public static DataTable EntityListToDataTable<T>(IList list)
    {
        DataTable dt = new DataTable();

        foreach (System.Reflection.PropertyInfo info in typeof(T).GetProperties())
        {
            dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
        }
        foreach (T t in list)
        {
            DataRow row = dt.NewRow();
            foreach (System.Reflection.PropertyInfo info in typeof(T).GetProperties())
            {
                row[info.Name] = info.GetValue(t, null);
            }
            dt.Rows.Add(row);
        }
        return dt;
    }


    public string getEnglishMonth(string Month)
    {
        if (Month == "1")
            return "January";
        else if (Month == "2")
            return "Febuary";
        else if (Month == "3")
            return "March";
        else if (Month == "4")
            return "April";
        else if (Month == "5")
            return "May";
        else if (Month == "6")
            return "June";
        else if (Month == "7")
            return "July";
        else if (Month == "8")
            return "August";
        else if (Month == "9")
            return "September";
        else if (Month == "10")
            return "October";
        else if (Month == "11")
            return "November";
        else if (Month == "12")
            return "December";
        else
            return "";
    }

    public string getNepaliMonth(string Month)
    {
        if (Month == "1")
            return "Baisakh";
        else if (Month == "2")
            return "Jestha";
        else if (Month == "3")
            return "Asadh";
        else if (Month == "4")
            return "Shrawan";
        else if (Month == "5")
            return "Bhadra";
        else if (Month == "6")
            return "Ashoj";
        else if (Month == "7")
            return "Kartik";
        else if (Month == "8")
            return "Mangshir";
        else if (Month == "9")
            return "Poush";
        else if (Month == "10")
            return "Magh";
        else if (Month == "11")
            return "Falgun";
        else if (Month == "12")
            return "Chaitra";
        else
            return "";
    }



    public string GetEmployeeName(string EmployeeCode, string OfficeCode, Boolean PDOAuthorization)
    {
        string EmployeeName = "";
        if (PDOAuthorization == false)
        {
            Employees entEmployee = new Employees();
            EmployeesService srvEmp = new EmployeesService();
            if (EmployeeCode.Trim() != "")
            {
                entEmployee.EMPLOYEEID = EmployeeCode;
                entEmployee = (Employees)srvEmp.GetSingle(entEmployee);
                if (entEmployee != null)
                    EmployeeName = entEmployee.FIRSTNAME + " " + entEmployee.LASTNAME;
            }
            else
            {
                EmployeeName = "N/A";
            }
        }
        else
        {
            Employees entEmployee = new Employees();
            EmployeesService srvEmp = new EmployeesService();
            entEmployee.EMPLOYEEID = EmployeeCode;
            entEmployee = (Employees)srvEmp.GetSingle(entEmployee);
            if (entEmployee != null)
            {
            }
        }

        return EmployeeName;
    }
    public string getEmployeeName(string EmpId)
    {
        if (EmpId == "")
            return "";
        string name = "";
        Employees EEnt = new Employees();
        EmployeesService ESrv = new EmployeesService();

        EEnt.EMPLOYEEID = EmpId;
        EEnt = (Employees)ESrv.GetSingle(EEnt);
        if (EEnt != null)
        {
            name = EEnt.FIRSTNAME + " " + EEnt.LASTNAME;
        }
        return name;
    }

    public string getEmployeeNamebyIDNo(string ID_NO)
    {
        if (ID_NO == "")
            return "";
        string name = "";
        Employees EEnt = new Employees();
        EmployeesService ESrv = new EmployeesService();

        EEnt.ID = ID_NO;
        EEnt = (Employees)ESrv.GetSingle(EEnt);
        if (EEnt != null)
        {
            name = EEnt.FIRSTNAME + " " + EEnt.LASTNAME;
        }
        return name;
    }
    public string[] ConvertEnglishToNepali(string EnglishDate)
    {
        short Nyear;
        short Nmonth;
        short Nday;
        NCCSNepaliDateLib.NepaliDateClass nepalidate = new NCCSNepaliDateLib.NepaliDateClass();
        DateTime englishDate = new DateTime();
        try
        {
            englishDate = Convert.ToDateTime(EnglishDate);
        }
        catch
        {
            englishDate = DateTime.Today;
        }
        nepalidate.SetEnglishDate(englishDate.Date);
        nepalidate.GetNepaliDate(out Nyear, out Nmonth, out Nday);
        string[] NepaliDate = { Nday.ToString(), Nmonth.ToString(), Nyear.ToString() };
        return NepaliDate;
    }

    public string getEmployeeIDbyIDNo(string ID_NO)
    {
        if (ID_NO == "")
            return "";
        string name = "";
        Employees EEnt = new Employees();
        EmployeesService ESrv = new EmployeesService();

        EEnt.ID = ID_NO;
        EEnt = (Employees)ESrv.GetSingle(EEnt);
        if (EEnt != null)
        {
            name = EEnt.EMPLOYEEID;
        }
        return name;
    }

    //This function will export the repeater to excel
    public void ExportToExcel(Repeater repeater, string excelFileName, string extraHeader, HttpResponse Response)
    {
        Response.Clear();
        if (excelFileName == "")
            excelFileName = "ExcelReport";
        string headerContent = "attachment;filename=" + excelFileName + ".xls";
        Response.AddHeader("content-disposition", headerContent);

        Response.Charset = "";

        // If you want the option to open the Excel file without saving than

        // comment out the line below

        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        if (extraHeader != "")
        {
            string header = "<center><h3>" + extraHeader + "</h3></center> <br/><br/>";
            htmlWrite.Write(header);
        }

        repeater.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }
    public void ExportGridToExcel(GridView grid, string excelFileName, string extraHeader, HttpResponse Response)
    {
        Response.Clear();
        if (excelFileName == "")
            excelFileName = "ExcelReport";
        string headerContent = "attachment;filename=" + excelFileName + ".xls";
        Response.AddHeader("content-disposition", headerContent);

        Response.Charset = "";

        // If you want the option to open the Excel file without saving than

        // comment out the line below

        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        if (extraHeader != "")
        {
            string header = "<center><h3>" + extraHeader + "</h3></center> <br/><br/>";
            htmlWrite.Write(header);
        }

        for (int rowPos = 0; rowPos < grid.Rows.Count; ++rowPos)
        {
            for (int colPos = 0; colPos < grid.Rows[rowPos].Cells.Count; ++colPos)
            {
                grid.Rows[rowPos].Cells[colPos].Attributes.Add("class", "NumberString");
            }
        }
        grid.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    public void ExportPanelToExcel(Panel pnl, string excelFileName, string extraHeader, HttpResponse Response)
    {
        Response.Clear();
        if (excelFileName == "")
            excelFileName = "ExcelReport";
        string headerContent = "attachment;filename=" + excelFileName + ".xls";
        Response.AddHeader("content-disposition", headerContent);

        Response.Charset = "";

        // If you want the option to open the Excel file without saving than

        // comment out the line below

        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        if (extraHeader != "")
        {
            string header = "<center><h3>" + extraHeader + "</h3></center> <br/><br/>";
            htmlWrite.Write(header);
        }

        pnl.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }


    public Boolean ISDateAfter(string Day, String Month, string Year, string Day1, string Month1, string Year1)
    {
        string EngDate = "";
        DateTime EngDateCompDate;
        string EngDate1 = "";
        DateTime EngDateCompDate1;
        try
        {
            EngDate = this.ConvertNepaliTOEnglishMonthChar(Day, Month, Year);
        }
        catch
        {
            try
            {
                EngDate = this.ConvertNepaliTOEnglishMonthChar((Day), (Month), (Year));
            }
            catch { }
        }
        EngDateCompDate = Convert.ToDateTime(EngDate);

        if (Day1 == "")
        {
            if (EngDateCompDate.Date > System.DateTime.Today.Date)
                return true;
            else
                return false;
        }
        else
        {
            try
            {
                EngDate1 = this.ConvertNepaliTOEnglishMonthChar(Day1, Month1, Year1);
            }
            catch
            {
                EngDate1 = this.ConvertNepaliTOEnglishMonthChar((Day1), (Month1), (Year1));
            }
            EngDateCompDate1 = Convert.ToDateTime(EngDate1);
            if (EngDateCompDate.Date >= EngDateCompDate1.Date)
                return true;
            else
                return false;
        }

    }


    public Boolean ISDateAfterEnglish(string Day, String Month, string Year, string Day1, string Month1, string Year1)
    {
        DateTime EngDateCompDate;
        DateTime EngDateCompDate1;
        EngDateCompDate = Convert.ToDateTime(Month + "/" + Day + "/" + Year);
        if (Day1 == "")
        {
            if (EngDateCompDate.Date >= System.DateTime.Today.Date)
                return true;
            else
                return false;
        }
        else
        {
            EngDateCompDate1 = Convert.ToDateTime(Month1 + "/" + Day1 + "/" + Year1);
            if (EngDateCompDate.Date >= EngDateCompDate1.Date)
                return true;
            else
                return false;
        }

    }
    public Boolean ISDateAfterEnglishChalan(string Day, String Month, string Year, string Day1, string Month1, string Year1)
    {
        DateTime EngDateCompDate;
        DateTime EngDateCompDate1;
        EngDateCompDate = Convert.ToDateTime(Month + "/" + Day + "/" + Year);
        if (Day1 == "")
        {
            if (EngDateCompDate.Date > System.DateTime.Today.Date)
                return true;
            else
                return false;
        }
        else
        {
            EngDateCompDate1 = Convert.ToDateTime(Month1 + "/" + Day1 + "/" + Year1);
            if (EngDateCompDate.Date > EngDateCompDate1.Date)
                return true;
            else
                return false;
        }

    }

    #region to convert the number into words

    public String NumWordsWrapper(double n)
    {
        string words = "";
        double intPart;
        double decPart = 0;
        if (n == 0)
            return "zero";
        try
        {
            string[] splitter = n.ToString().Split('.');
            intPart = double.Parse(splitter[0]);
            decPart = double.Parse(splitter[1]);
        }
        catch
        {
            intPart = n;
        }

        words = NumWords(intPart);

        if (decPart > 0)
        {
            if (words != "")
                words += " and ";
            int counter = decPart.ToString().Length;
            switch (counter)
            {
                case 1: words += NumWords(decPart) + " tenths"; break;
                case 2: words += NumWords(decPart) + " hundredths"; break;
                case 3: words += NumWords(decPart) + " thousandths"; break;
                case 4: words += NumWords(decPart) + " ten-thousandths"; break;
                case 5: words += NumWords(decPart) + " hundred-thousandths"; break;
                case 6: words += NumWords(decPart) + " millionths"; break;
                case 7: words += NumWords(decPart) + " ten-millionths"; break;
            }
        }
        return words;
    }

    static String NumWords(double n) //converts double to words
    {
        string[] numbersArr = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
        string[] suffixesArr = new string[] { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
        string words = "";

        bool tens = false;

        if (n < 0)
        {
            words += "negative ";
            n *= -1;
        }

        int power = (suffixesArr.Length + 1) * 3;

        while (power > 3)
        {
            double pow = Math.Pow(10, power);
            if (n >= pow)
            {
                if (n % pow > 0)
                {
                    words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + ", ";
                }
                else if (n % pow == 0)
                {
                    words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                }
                n %= pow;
            }
            power -= 3;
        }
        if (n >= 1000)
        {
            if (n % 1000 > 0) words += NumWords(Math.Floor(n / 1000)) + " thousand  ";
            else words += NumWords(Math.Floor(n / 1000)) + " thousand";
            n %= 1000;
        }
        if (0 <= n && n <= 999)
        {
            if ((int)n / 100 > 0)
            {
                words += NumWords(Math.Floor(n / 100)) + " hundred";
                n %= 100;
            }
            if ((int)n / 10 > 1)
            {
                if (words != "")
                    words += " ";
                words += tensArr[(int)n / 10 - 2];
                tens = true;
                n %= 10;
            }

            if (n < 20 && n > 0)
            {
                if (words != "" && tens == false)
                    words += " ";
                words += (tens ? "-" + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                n -= Math.Floor(n);
            }
        }

        return words;

    }

    #endregion



    public string GetReceitpMonth(string batch, string studentid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.selectReceiptMonth";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = studentid;
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public DataTable getTotalMonthly(string batch, string studentid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getTotalMonthly";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = batch;
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }

    public DataTable getstudentforsection(string program_id, string batch, string semester_id)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getstudentforsection";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program_id;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = batch;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = semester_id;
        objCmd.Parameters.Add(_p3);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }


    public DataTable DueBookList(string batch, string level, string program, string semester, string todaydate, int days)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.duebooklist";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = level;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = todaydate;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = days;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = program;
        objCmd.Parameters.Add(_p4);

        OracleParameter _p5 = new OracleParameter();
        _p5.Direction = ParameterDirection.Input;
        _p5.Value = semester;
        objCmd.Parameters.Add(_p5);

        OracleParameter _p6 = new OracleParameter();
        _p6.Direction = ParameterDirection.Input;
        _p6.Value = batch;
        objCmd.Parameters.Add(_p6);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getstudentforsubject(string program_id, string semester_id, string batch, string section)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getstudentforsubject";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program_id;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester_id;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = batch;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = section;
        objCmd.Parameters.Add(_p4);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }


    public string getMaxReceiptNo(string fiscalyear)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getMaxReceiptNo";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;
        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fiscalyear;
        objCmd.Parameters.Add(_p1);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;

    }


    public DataTable getExamType(string programid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();


        OracleConnection conn = new OracleConnection
(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getExamType";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = programid;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }


    public DataTable getGrandTotal(string studentid, string batch, string clas, string mbillid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getGrandTotal";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = batch;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = clas;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = mbillid;
        objCmd.Parameters.Add(_p4);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }



    public DataTable getMaxQty(string mbillid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getMaxQuantity";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = mbillid;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }


    public DataTable selectstudentinfo(string program, string batch, string semester, string section)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.selectstudentinfo";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = batch;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = semester;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = section;
        objCmd.Parameters.Add(_p4);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getDroppedStudent(string batch)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getDroppedStudent";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public DataTable getAllnotices(string program, string semester, string fdate, string tdate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getAllnotices";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = fdate;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = tdate;
        objCmd.Parameters.Add(_p4);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }



    public DataTable GetContactList(string semester, string section, string batch, string group)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.GetContactList";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = semester;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = section;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = batch;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = group;
        objCmd.Parameters.Add(_p4);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getcontactofStudent(string student_id, string section, string batch, string group)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getcontactofStudent";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = student_id;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = section;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = batch;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = group;
        objCmd.Parameters.Add(_p4);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public string getcontactofStudentSingle(string student_id, string section, string batch, string group)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getcontactofStudent";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = student_id;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = section;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = batch;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = group;
        objCmd.Parameters.Add(_p4);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            try
            {
                return dtable.Rows[0][0].ToString();
            }
            catch
            {
                return null;
            }
        else
            return null;


    }



    public DataTable SemesterLeaveRecord(string sub1, string sub2, string sub3, string sub4, string sub5, string sub6, string fdate, string tdate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.semesterleaverecord";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = sub1;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = sub2;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = sub3;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = sub4;
        objCmd.Parameters.Add(_p4);

        OracleParameter _p5 = new OracleParameter();
        _p5.Direction = ParameterDirection.Input;
        _p5.Value = sub5;
        objCmd.Parameters.Add(_p5);

        OracleParameter _p6 = new OracleParameter();
        _p6.Direction = ParameterDirection.Input;
        _p6.Value = sub6;
        objCmd.Parameters.Add(_p6);

        OracleParameter _p7 = new OracleParameter();
        _p7.Direction = ParameterDirection.Input;
        _p7.Value = fdate;
        objCmd.Parameters.Add(_p7);

        OracleParameter _p8 = new OracleParameter();
        _p8.Direction = ParameterDirection.Input;
        _p8.Value = tdate;
        objCmd.Parameters.Add(_p8);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable DueBookList(string todaydate, int days)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.duebooklist";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = todaydate;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = days;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }
    public string getMaxBookTypeId()
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getMaxBookTypeId";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public string getMaxBookCode(string booktypeid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getMaxBookCode";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = booktypeid;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public string getMaxBookNumber(string bookid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getMaxBookNumber";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = bookid;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public DataTable getStd_Sem_Sub(string program_id, string syllabus_year)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();
        DataTable dtable1 = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getStd_Sem_Sub";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program_id;
        objCmd.Parameters.Add(_p1);


        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = syllabus_year;
        objCmd.Parameters.Add(_p2);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable.Rows.Count > 0)
        {
            return dtable;
        }
        else
            return null;
    }

    public string getSyllabusYearfromBatch(string program, string batch)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getSyllabusYearfromBatch";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = batch;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable.Rows.Count > 0)
            return dtable.Rows[0][0].ToString();
        else
            return "";
    }

    public DataTable getremainingbalofallstudent(string batch, string program)
    {

        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getremainingbalofallstudent";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = program;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
        {
            return dtable;

        }

        else
        {
            return null;
        }


    }

    public DataTable getSemesterAttendance(string batch, string semester, string student_id)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getSemesterAttendance";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        if (student_id != "")
        {
            _p3.Direction = ParameterDirection.Input;
            _p3.Value = student_id;
        }
        objCmd.Parameters.Add(_p3);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getTeacherInfo()
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getTeacherInfo";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getStaffInfo()
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getStaffInfo";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }



    public DataTable getreceiptlist(string receiptdate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getreceiptlist";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = receiptdate;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public string selectmaxempid()
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.selectmaxempid";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }


    public Boolean checkPageAccess(string pageurl, string groupid)
    {
        string pageid = "";
        PGEnt = new Pages();
        PGEnt.PAGENAME = pageurl;
        PGEnt = (Pages)PGSer.GetSingle(PGEnt);
        if (PGEnt != null)
        {
            pageid = PGEnt.ID;

            string permission = getPagePermission(pageid, groupid);

            if (permission == "00001111")
            {
                return true;
            }
            else
            {
                return false;

            }

        }
        else
        {

            return false;
        }


    }

    public string getPagePermission(string pageid, string groupid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_select.getPagePermission";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = pageid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = groupid;
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }


    public DataTable getBillGenParticulars(string program, string from_inst, string to_inst, string batch, string studentid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getBillGenParticulars";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = from_inst;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = to_inst;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = batch;
        objCmd.Parameters.Add(_p4);

        OracleParameter _p5 = new OracleParameter();
        _p5.Direction = ParameterDirection.Input;
        _p5.Value = studentid;
        objCmd.Parameters.Add(_p5);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public string getMasterBillTotal(string student_id, string sem, string from_inst, string to_inst)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getMasterBillTotal";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = student_id;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        if (sem != "")
        {
            _p2.Direction = ParameterDirection.Input;
            _p2.Value = sem;
        }
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = from_inst;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = to_inst;
        objCmd.Parameters.Add(_p4);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public DataTable getBillDetailToInsert(string student_id, string sem, string from_inst, string to_inst)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getBillDetailToInsert";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = student_id;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        if (sem != "")
        {
            _p2.Direction = ParameterDirection.Input;
            _p2.Value = sem;
        }
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = from_inst;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = to_inst;
        objCmd.Parameters.Add(_p4);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public string getremainingbalanceofstudent(string studentid)
    {
        string balance = "";
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getremainingbalanceofstudent";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
        {
            balance = dtable.Rows[0][0].ToString();
            if (balance != "")
            {
                return balance;

            }
            else
            {
                return "0";
            }
        }


        else
            return "0";


    }

    

    public string getlatestadvanceofstudent(string studentid)
    {
        string balance = "";
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getlatestadvanceofstudent";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
        {
            try
            {
                balance = dtable.Rows[0][0].ToString();

                if (balance != "")
                {
                    return balance;

                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return "0";
            }
        }


        else
            return "0";


    }

    public DataTable getremainingbalofallstudent(string batch)
    {

        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getremainingbalofallstudent";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
        {
            return dtable;

        }

        else
        {
            return null;
        }


    }

    public string getmaxinstallmentno(string batch)
    {
        string balance = "";
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getmaxinstallmentno";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
        {
            balance = dtable.Rows[0][0].ToString();
            if (balance != "")
            {
                return balance;

            }
            else
            {
                return "0";
            }
        }


        else
            return "0";


    }


    public string getCumInstNo(string programid, string batch)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getCUM_INST_NO";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = programid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = batch;
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;

    }



    public DataTable getbulk_inv_bill(string program, string batch, string semester, string billType, string stdid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "PKJ_REPORTS.getbulk_inv_bill";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = batch;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = semester;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = billType;
        objCmd.Parameters.Add(_p4);

        OracleParameter _p5 = new OracleParameter();

        if (stdid != "Select")
        {
            _p5.Direction = ParameterDirection.Input;
            _p5.Value = stdid;

        }
        else
        {
            _p5.Direction = ParameterDirection.Input;
            _p5.Value = "";
        }
        objCmd.Parameters.Add(_p5);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getBillDetail(string mbillid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getBillDetail";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = mbillid;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public DataTable getInstallmentStructure(string from_inst, string to_inst, string student_id)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getInstallmentStructure";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = from_inst;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = to_inst;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = student_id;
        objCmd.Parameters.Add(_p3);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getindividualbill(string batch, string std_id)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getindividualbill";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = std_id;
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public DataTable getDailyCollection(string datefrom, string dateto, string fiscal_year, string prog)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.dailycollection";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = datefrom;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = dateto;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = fiscal_year;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        if (prog != "Select")
            _p4.Value = prog;
        else
            _p4.Value = null;
        objCmd.Parameters.Add(_p4);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getDailyWithdraw(string datefrom, string dateto, string fiscal_year)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.dailywithdraw";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = datefrom;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = dateto;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = fiscal_year;
        objCmd.Parameters.Add(_p3);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getindividualpayschedule(string student_id)
    {

        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getindividualpayschedule";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = student_id;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
        {
            return dtable;

        }

        else
        {
            return null;
        }


    }


    public DataTable GETATTENDANCEDETAIL(string fdate, string tdate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.GETATTENDANCEDETAIL";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fdate;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = tdate;
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable GETATTENDANCEREPORT(string division, string fdate, string tdate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getattendancereport";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = division;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = fdate;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = tdate;
        objCmd.Parameters.Add(_p3);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable GETATTENDANCE(string fdate, string tdate, string empid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.GETATTENDANCE";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fdate;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = tdate;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = empid;
        objCmd.Parameters.Add(_p3);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public string getMainProductCode()
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getMainProductCode";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public DataTable getTotalAttendance(string studentid, string semester, string subject)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getTotalAttendance";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        if (subject != "")
        {
            _p3.Direction = ParameterDirection.Input;
            _p3.Value = subject;
        }
        objCmd.Parameters.Add(_p3);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getIndividualTeacherClass(string teacher, string fromdate, string todate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getIndividualTeacherClass";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = teacher;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = fromdate;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = todate;
        objCmd.Parameters.Add(_p3);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public string getProductTypeCode(string mainproduct_code)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getProductTypeCode";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = mainproduct_code;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public string getProductCode(string product_typeid)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getProductCode";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = product_typeid;
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable.Rows[0][0].ToString();
        else
            return null;


    }

    public DataTable getStudentInfo(string batch, string semester)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getStudentInfo";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getAllotmentforIssue(string batch, string semester, string student_id)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getAllotmentforIssue";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = student_id;
        objCmd.Parameters.Add(_p3);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public string getSyllabusYear(string program, string year)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getSyllabusYear";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = program;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = year;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable.Rows.Count > 0)
            return dtable.Rows[0][0].ToString();
        else
            return "";
    }


    public DataTable getPurchaseReport(string fromdate, string todate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getPurchaseReport";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fromdate;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = todate;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getIssueReport(string fromdate, string todate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getIssueReport";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fromdate;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = todate;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getAdjustmentReport(string fromdate, string todate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getAdjustmentReport";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fromdate;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = todate;
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getStockReport(string fiscalyear)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getStockReport";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fiscalyear;
        objCmd.Parameters.Add(_p1);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getLogBookReport(string VName, string fromdate, string todate)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getLogBookReport";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = VName;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = fromdate;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = todate;
        objCmd.Parameters.Add(_p3);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getEnquiryReport(string fromdate, string todate, string interest, string phoneenq, string visitenq)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getEnquiryReport";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = fromdate;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = todate;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = interest;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = phoneenq;
        objCmd.Parameters.Add(_p4);

        OracleParameter _p5 = new OracleParameter();
        _p5.Direction = ParameterDirection.Input;
        _p5.Value = visitenq;
        objCmd.Parameters.Add(_p5);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getCGPAReportEight(string batch, string sem1, string sem2, string sem3, string sem4, string sem5, string sem6, string sem7, string sem8)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getCGPAReportEight";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = sem1;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = sem2;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = sem3;
        objCmd.Parameters.Add(_p4);

        OracleParameter _p5 = new OracleParameter();
        _p5.Direction = ParameterDirection.Input;
        _p5.Value = sem4;
        objCmd.Parameters.Add(_p5);


        OracleParameter _p6 = new OracleParameter();
        _p6.Direction = ParameterDirection.Input;
        _p6.Value = sem5;
        objCmd.Parameters.Add(_p6);

        OracleParameter _p7 = new OracleParameter();
        _p7.Direction = ParameterDirection.Input;
        _p7.Value = sem6;
        objCmd.Parameters.Add(_p7);

        OracleParameter _p8 = new OracleParameter();
        _p8.Direction = ParameterDirection.Input;
        _p8.Value = sem7;
        objCmd.Parameters.Add(_p8);

        OracleParameter _p9 = new OracleParameter();
        _p9.Direction = ParameterDirection.Input;
        _p9.Value = sem8;
        objCmd.Parameters.Add(_p9);



        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public DataTable getRemBal(string studentid, string batch)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getRemBal";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        if (studentid != "")
        {
            _p1.Direction = ParameterDirection.Input;
            _p1.Value = studentid;
        }
        objCmd.Parameters.Add(_p1);


        OracleParameter _p2 = new OracleParameter();
        if (batch != "")
        {
            _p2.Direction = ParameterDirection.Input;
            _p2.Value = batch;
        }
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
        {
            return dtable;
        }
        else
            return null;
    }

    public string getRemBalN(string studentid, string batch)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();
        DataTable dtable1 = new DataTable();
        //dtable1.Rows[0][0] = "0";

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getRemBal";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        if (studentid != "")
        {
            _p1.Direction = ParameterDirection.Input;
            _p1.Value = studentid;
        }
        objCmd.Parameters.Add(_p1);


        OracleParameter _p2 = new OracleParameter();
        if (batch != "")
        {
            _p2.Direction = ParameterDirection.Input;
            _p2.Value = batch;
        }
        objCmd.Parameters.Add(_p2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable.Rows.Count > 0)
        {
            return dtable.Rows[0][1].ToString();
        }
        else
            return "0";
    }


    public DataTable getCGPAReportSixth(string batch, string sem1, string sem2, string sem3, string sem4, string sem5, string sem6)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getCGPAReportSixth";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = batch;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = sem1;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = sem2;
        objCmd.Parameters.Add(_p3);

        OracleParameter _p4 = new OracleParameter();
        _p4.Direction = ParameterDirection.Input;
        _p4.Value = sem3;
        objCmd.Parameters.Add(_p4);

        OracleParameter _p5 = new OracleParameter();
        _p5.Direction = ParameterDirection.Input;
        _p5.Value = sem4;
        objCmd.Parameters.Add(_p5);


        OracleParameter _p6 = new OracleParameter();
        _p6.Direction = ParameterDirection.Input;
        _p6.Value = sem5;
        objCmd.Parameters.Add(_p6);

        OracleParameter _p7 = new OracleParameter();
        _p7.Direction = ParameterDirection.Input;
        _p7.Value = sem6;
        objCmd.Parameters.Add(_p7);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }



    public DataTable getInstallmentNo(string studentid, string semester)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getinstallmentno_fine";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        if (semester != "")
        {
            _p2.Direction = ParameterDirection.Input;
            _p2.Value = semester;
        }
        objCmd.Parameters.Add(_p2);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }


    public DataTable getInstallmentNoForBill(string studentid, string semester)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getInstallmentNoForBill";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);




        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    public DataTable getsem_from_installmentno(string studentid, string semester)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getsem_from_installmentno";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        if (semester != "")
        {
            _p2.Direction = ParameterDirection.Input;
            _p2.Value = semester;
        }
        objCmd.Parameters.Add(_p2);




        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }

    public DataTable getsem_from_installmentnoAll(string semester)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getsem_from_installmentnoAll";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;


        OracleParameter _p1 = new OracleParameter();
        if (semester != "")
        {
            _p1.Direction = ParameterDirection.Input;
            _p1.Value = semester;
        }
        objCmd.Parameters.Add(_p1);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }

    public DataTable getsem_inst_amt(string progarmid, string fromInst, string toInst)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getsem_inst_amt";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = progarmid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = fromInst;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = toInst;
        objCmd.Parameters.Add(_p3);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;

    }

    public DataTable getScholorshipDiscount(string studentid, string semester, string installment)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.getScholorshipDiscount";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = installment;
        objCmd.Parameters.Add(_p3);


        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }


    public string SendSms(string smsnumber, string message)
    {
        string status = "";

        try
        {
            if (smsnumber != "")
            {

                WebClient wbClient = new WebClient();

                //  string smsurl = "http://smsprima.com/api/api/index?username=ncft&password=sms@12345&sender=SMSService&destination=" + smsnumber + "&type=1&message=" + message;

                //  wbClient.OpenRead(smsurl);
                //  {
                status = "Success";


                // }
            }
        }
        catch (Exception ex)
        {
            status = "Failed";

        }
        return status;

    }



    public DataTable GetTransposData(DataTable org, int TransposeColSt, int TransposeColBase, int TransHeadingCol, int ValCol1, int ValCol2)
    {
        DataTable Original = org;
        DataTable TrnasposeDT = new DataTable();
        DataRow TrnasposeRow;

        #region Preparing the New Matrix Table Structure
        // Get the Non Transpose Columns to be Added
        ArrayList NonTransposeCols = new ArrayList();
        ArrayList TransposeCols = new ArrayList();
        string strPrevTransposeBase = "";
        for (int i = 0; i <= TransposeColSt; i++)
        {
            NonTransposeCols.Add(org.Columns[i].ColumnName);
        }


        for (int i = 0; i <= NonTransposeCols.Count - 1; i++)
        {
            TrnasposeDT.Columns.Add(NonTransposeCols[i].ToString());
        }
        // Getting the Transpose Columns Heading

        if (ValCol2 == null)
        {
            foreach (DataRow dr in org.Rows)
            {
                if (TrnasposeDT.Columns.Contains(dr[TransHeadingCol].ToString()))
                    continue;
                TrnasposeDT.Columns.Add(dr[TransHeadingCol].ToString());
                strPrevTransposeBase = dr[TransposeColBase].ToString();
            }
        }
        else
        {
            foreach (DataRow dr in org.Rows)
            {
                if (dr[ValCol1].ToString() != "0")
                {
                    if (TrnasposeDT.Columns.Contains(dr[TransHeadingCol].ToString()))
                        continue;
                    TrnasposeDT.Columns.Add(dr[TransHeadingCol].ToString());
                    strPrevTransposeBase = dr[TransposeColBase].ToString();
                }
            }
            foreach (DataRow dr in org.Rows)
            {
                if (dr[ValCol2].ToString() != "0")
                {
                    if (TrnasposeDT.Columns.Contains(dr[TransHeadingCol].ToString()))
                        continue;
                    TrnasposeDT.Columns.Add(dr[TransHeadingCol].ToString());
                    strPrevTransposeBase = dr[TransposeColBase].ToString();
                }
            }
        }

        #endregion

        #region Adding the values into the New Data Table
        int rowCounter = 0;
        strPrevTransposeBase = "";
        TrnasposeRow = TrnasposeDT.NewRow();

        DataRow dtNext = null;
        if (org.Rows.Count > 1)
            dtNext = org.Rows[1];
        for (rowCounter = 0; rowCounter < org.Rows.Count - 1; rowCounter++)
        {
            DataRow dr = org.Rows[rowCounter];
            #region Working for non transpose Data
            if (strPrevTransposeBase != dtNext[TransposeColBase].ToString())
            {
                if (strPrevTransposeBase != "")
                {
                    TrnasposeDT.Rows.Add(TrnasposeRow);
                    TrnasposeRow = TrnasposeDT.NewRow();
                }
                // New Data row is to be creaded. Add new row
                for (int i = 0; i < NonTransposeCols.Count; i++)
                {
                    TrnasposeRow[i] = dr[i].ToString();
                }
                // Add the value row as well
                if (dr[ValCol1].ToString() == "0" || dr[ValCol1].ToString() == "")
                    TrnasposeRow[dr[TransHeadingCol].ToString()] = dr[ValCol2].ToString();
                else
                    TrnasposeRow[dr[TransHeadingCol].ToString()] = dr[ValCol1].ToString();
            }
            else
            {
                // New Data row is not required. Add the Transpose Columns Values
                if (dr[ValCol1].ToString() == "0" || dr[ValCol1].ToString() == "")
                    TrnasposeRow[dr[TransHeadingCol].ToString()] = dr[ValCol2].ToString();
                else
                    TrnasposeRow[dr[TransHeadingCol].ToString()] = dr[ValCol1].ToString();
            }
            //if (strPrevTransposeBase != "")
            //{
            //    //if (strPrevTransposeBase != dtNext[TransposeColBase].ToString())
            //    //{
            //    //    if (TrnasposeRow != null)
            //    //    {
            //    //        TrnasposeDT.Rows.Add(TrnasposeRow);
            //    //        strPrevTransposeBase = "";
            //    //    }
            //    //    strPrevTransposeBase = dr[TransposeColBase].ToString();
            //    //}
            //}
            //else
            //{
            strPrevTransposeBase = dr[TransposeColBase].ToString();
            dtNext = org.Rows[rowCounter + 1];
            //}
        }
        return TrnasposeDT;
        #endregion
        #endregion
    }

    public string[] NepaliDateDifference(int SD, int SM, int SY, int ED, int EM, int EY)
    {
        string[] datediff = { "0", "0", "0" };
        string SED, SEM, SEY;
        SED = SD.ToString();
        SEM = SM.ToString();
        SEY = SY.ToString();
        if (ED - SD < 0)
        {
            string date;
            EM = EM - 1;
            if (EM <= 0)
            {
                EM += 12;
                EY -= 1;
            }
            SEM = EM.ToString();

            date = this.ConvertNepaliTOEnglish("32", SEM, SEY);
            if (date != "")
                datediff[0] = (ED + 32 - SD).ToString();
            else
            {
                date = this.ConvertNepaliTOEnglish("31", SEM, SEY);
                if (date != "")
                    datediff[0] = (ED + 31 - SD).ToString();
                else
                {
                    date = this.ConvertNepaliTOEnglish("30", SEM, SEY);
                    if (date != "")
                        datediff[0] = (ED + 30 - SD).ToString();
                    else
                    {
                        date = this.ConvertNepaliTOEnglish("29", SEM, SEY);
                        if (date != "")
                            datediff[0] = (ED + 29 - SD).ToString();
                    }

                }
            }
            if (EM - SM < 0)
            {
                datediff[1] = (EM + 12 - SM).ToString();
                datediff[2] = (EY - SY - 1).ToString();
            }
            else
            {
                datediff[1] = (EM - SM).ToString();
                datediff[2] = (EY - SY).ToString();
            }



        }
        else
        {
            datediff[0] = (ED - SD).ToString();

            if (EM - SM < 0)
            {
                datediff[1] = (EM + 12 - SM).ToString();
                datediff[2] = (EY - SY - 1).ToString();
            }
            else
            {
                datediff[1] = (EM - SM).ToString();
                datediff[2] = (EY - SY).ToString();
            }
        }
        return datediff;
    }

}

//----------------------************************-----------------------------**********************----------------------------

public class EditVoucherEventArgs : EventArgs
{
    private DistributedTransaction InnerDT;
    private string InnerVMID;
    private string InnerRef;

    public EditVoucherEventArgs()
    {
        InnerDT = null;
        InnerVMID = "";
        InnerRef = "";
    }


    public DistributedTransaction DT
    {
        get
        {
            return InnerDT;
        }
        set { InnerDT = value; }
    }


    public string VMID
    {
        get
        {
            return InnerVMID;
        }
        set { InnerVMID = value; }
    }

    public string Refrence
    {
        get
        {
            return InnerRef;
        }
        set { InnerRef = value; }
    }
}

public class GridDecorator
{
    public static void MergeRows(GridView gridView, int TillCol)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int cellIndex = 0; cellIndex < TillCol; cellIndex++)
            {
                if (row.Cells[cellIndex].Text == previousRow.Cells[cellIndex].Text)
                {
                    row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                    previousRow.Cells[cellIndex].Visible = false;
                }
            }
        }
    }
}

