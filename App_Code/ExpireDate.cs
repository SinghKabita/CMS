using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ExpireDate
/// </summary>
public class ExpireDate
{
    public ExpireDate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string CheckContractDate(string str)
    {
        string[] shortdate = str.Split(' ');
        string[] shortdates = shortdate[0].Split('/');
        string day = shortdates[0];
        string month = shortdates[1];
        string year = shortdates[2];
        int DD = Int32.Parse(day);
        //int test = Int32.Parse(day);
        int MM = Int32.Parse(month);
        int YY = Int32.Parse(year);
        YY = YY + 1;
        DD = DD - 1;
        if (DD == 0)
        {
            MM = MM - 1;
            if (MM == 1)
            {
                DD = 31;
            }
            else if (MM == 2)
            {
                DD = 28;
            }
            else if (MM == 3)
            {
                DD = 31;
            }
            else if (MM == 4)
            {
                DD = 30;
            }
            else if (MM == 5)
            {
                DD = 31;
            }
            else if (MM == 6)
            {
                DD = 30;
            }
            else if (MM == 7)
            {
                DD = 31;
            }
            else if (MM == 8)
            {
                DD = 30;
            }
            else if (MM == 9)
            {
                DD = 31;
            }
            else if (MM == 10)
            {
                DD = 30;
            }
            else if (MM == 11)
            {
                DD = 31;
            }
            else if (MM == 12)
            {
                DD = 30;

            }
            else if (MM == 0)
            {
                MM = 12;
                DD = 30;
                YY = YY - 1;
            }
        }
        string d = DD.ToString();
        string m = MM.ToString();
        string y = YY.ToString();
        string DATE = d + "/" + m + "/" + y;
        return DATE;
    }
}

