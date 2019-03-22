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
using Oracle.DataAccess.Client;




public class HolidayRep
{
    public void Process(string Proc, string _emp, string _year, string _month)
    {
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();

        objCmd.CommandText = Proc;

        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter p_empid = new OracleParameter("p_empid", OracleDbType.Varchar2);
        p_empid.Direction = ParameterDirection.Input;
        p_empid.Value = _emp;
        objCmd.Parameters.Add(p_empid);

        OracleParameter p_cyear = new OracleParameter("p_cyear", OracleDbType.Varchar2);
        p_cyear.Direction = ParameterDirection.Input;
        p_cyear.Value = _year;
        objCmd.Parameters.Add(p_cyear);

        OracleParameter p_cmonth = new OracleParameter("p_cmonth", OracleDbType.Varchar2);
        p_cmonth.Direction = ParameterDirection.Input;
        p_cmonth.Value = _month;
        objCmd.Parameters.Add(p_cmonth);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        cn.Open();
        objCmd.ExecuteReader();
        cn.Close();
    }
    public DataTable getHolidays(string emp)
    {
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        DataTable dt = new DataTable();
        //objCmd.CommandText = "Proc_Holidays";
        objCmd.CommandText = "proc_holidays_final";
        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter p_empid = new OracleParameter("p_empid", OracleDbType.Varchar2);
        p_empid.Direction = ParameterDirection.Input;
        p_empid.Value = emp;
        objCmd.Parameters.Add(p_empid);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        cn.Open();
        OracleDataReader dr;
        dr = objCmd.ExecuteReader();

        dt.Load(dr);
        cn.Close();
        return dt;
    }

}