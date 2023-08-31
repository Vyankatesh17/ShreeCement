
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Web.Mail;
using System.Globalization;
/// <summary>
/// Summary description for Genreal
/// </summary>
public class Genreal
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    EliteDataDataContext elite = new EliteDataDataContext();

    string connection = ConfigurationManager.ConnectionStrings["eidinfo_hrmsConnectionString"].ConnectionString;
    //string eliteconnection = ConfigurationManager.ConnectionStrings["etimetracklite1ConnectionString"].ConnectionString;
    public Genreal()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DateTime GetDate(string fdate)
    {

        string[] strArray = fdate.Split('/');
        if (strArray.Length == 1)
        {
            strArray = fdate.Split('-');
        }

        int year = strArray[0].Length == 4 ? Convert.ToInt32(strArray[0]) : strArray[1].Length == 4 ? Convert.ToInt32(strArray[1]) : Convert.ToInt32(strArray[2]);
        int month = Convert.ToInt32(strArray[1]);
        int day = Convert.ToInt32(strArray[2]);

        string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames;

        string str = year.ToString() + monthNames[month - 1] + (object)day;
        string shortDate = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        return DateTime.Parse(Convert.ToDateTime(str).ToString(shortDate));
    }
    public DateTime GetStartOfMonth(DateTime DT)
    {
        return new DateTime(DT.Year, DT.Month, 1);
    }

    public DateTime EndOfMonth(DateTime DT)
    {
        DateTime firstDayOfTheMonth = new DateTime(DT.Year, DT.Month, 1);
        return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
    }

    public DateTime StartOfYear(DateTime DT)
    {
        DateTime FirstDayinYear = new DateTime(DT.Year, DT.Month, 1);
        return FirstDayinYear.AddYears(1).AddYears(-1);
    }

    public string DBgetConnectionString()
    {
        string strConnection;
        try
        {

            strConnection = ConfigurationManager.ConnectionStrings["eidinfo_hrmsConnectionString"].ConnectionString;
        }

        catch (Exception ex)
        {
            throw ex;
        }
        return strConnection;
    }

    public void ShowMessage(Page Page, string Message)
    {
        ScriptManager.RegisterClientScriptBlock(Page.Page, Page.GetType(), "alert", "javascript:alert('" + Message + "')", true);
    }

    public void ShowMessageRedirect(Page Page, string Message, string url)
    {
        ScriptManager.RegisterClientScriptBlock(Page.Page, Page.GetType(), "alert", "javascript:alert('" + Message + "');window.location.href='" + url + "';", true);
    }

    public DateTime GetCurrentDateTime()
    {

        DateTime dt = HR.ExecuteQuery<DateTime>("Select GETDATE()").First();
        return dt;
    }
    public string Getempname(int ScheduleId)
    {
        string empname1 = "";
        var empdata = from d in HR.SchedulePanelTBs
                      join dt in HR.EmployeeTBs on d.EmployeeId equals dt.EmployeeId
                      where d.ScheduleId == ScheduleId
                      select new { empname = dt.FName + " " + dt.Lname };

        if (empdata.Count() > 0)
        {
            foreach (var item in empdata)
            {
                if (empname1 == "")
                {
                    empname1 = item.empname;
                }
                else
                {
                    empname1 = empname1 + "," + item.empname;
                }

            }

        }
        return empname1;

    }
    public bool CheckAdmin(int EmplyeeId)
    {
        bool Status = false;

        RegistrationTB rg = HR.RegistrationTBs.Where(d => d.EmployeeId == EmplyeeId).First();
        if (rg.UserType == "Admin")
        {
            Status = true;
        }
        return Status;
    }
    public bool CheckSuperAdmin(string EmplyeeId)
    {
        bool Status = false;

        SystemUsersTB rg = HR.SystemUsersTBs.Where(d => d.UID == EmplyeeId).First();
        if (rg.UserRole == "SuperAdmin")
        {
            Status = true;
        }
        return Status;
    }
    public DataTable ReturnData(string query)
    {
        DataTable dt = new DataTable();
        SqlConnection conn;

        try
        {
            conn = new SqlConnection(connection);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            conn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //MessageBox.Show(ex.Message);
        }
        finally
        {
            conn = null;
        }
        return dt;
    }
    public DataSet ReturnData1(string query)
    {
        DataSet dt = new DataSet();
        try
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            conn.Close();
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }
        finally
        {
        }
        return dt;
    }

    public IQueryable<CountryTB> GetCountry()
    {
        IQueryable<CountryTB> DT = (from d in HR.CountryTBs where d.CountryId == 46 select d).AsQueryable();
        return DT;
    }


    public DataSet ProcdureWith3Param(string proc, string param1, string param2, string param3)
    {
        SqlConnection conn = new SqlConnection(connection);
        DataSet ds = new DataSet();
        try
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = proc; // stored proc name
            cmd.Parameters.AddWithValue("@param1", param1);
            cmd.Parameters.AddWithValue("@param2", param2);
            cmd.Parameters.AddWithValue("@param3", param3);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

        }

        catch (Exception ex)
        {
            conn.Close();

        }

        finally
        {

            conn.Close();
        }
        return ds;
    }

    public static void AuditApi(string fullApi, string api, string action, string status, string fromPage, string actionBy, string Description, string companyId, string tenantId,string ipAddress,string serialNo,string accountId)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                int cId = string.IsNullOrEmpty(companyId) ? 0 : Convert.ToInt32(companyId);
                MstApiAuditTB auditTB = new MstApiAuditTB();
                auditTB.Api = api;
                auditTB.ApiAction = action;
                auditTB.AuditDate = DateTime.Now;
                auditTB.ExecuteBy = actionBy;
                auditTB.ExecutePage = fromPage;
                auditTB.DeviceSerialNo = serialNo;
                auditTB.DeviceIpAddress = ipAddress;
                auditTB.DeviceAccountId = accountId;

                auditTB.FullApi = fullApi;
                auditTB.Status = status;
                auditTB.Description = Description;
                auditTB.CompanyId = cId;
                auditTB.TenantId = tenantId;
                db.MstApiAuditTBs.InsertOnSubmit(auditTB);
                db.SubmitChanges();
            }
        }
        catch (Exception ex) { }
    }



    public static void SMSAuditApi(int empcode, string empname, string contact, string status)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {                
                tbl_SMSAuditTB auditTB = new tbl_SMSAuditTB();
                auditTB.EmployeeCode = empcode;
                auditTB.EmployeeName = empname;
                auditTB.ContactNo = contact;                
                auditTB.SMSStatus = status;
                
                db.tbl_SMSAuditTBs.InsertOnSubmit(auditTB);
                db.SubmitChanges();
            }
        }
        catch (Exception ex) { }
    }


    public string GetesslDevice(string deviceaccountid)
    {
        string devstatus = "Offline";
        using (EliteDataDataContext elite = new EliteDataDataContext())
        {
            DateTime currentdate = DateTime.Now;
            var elitedeviceinfo = elite.Devices.Where(a => a.DeviceSName == deviceaccountid).FirstOrDefault();
            if (elitedeviceinfo != null)
            {
                DateTime dt = (DateTime)elitedeviceinfo.LastPing;

                double totaltime = (currentdate - dt).TotalSeconds;
                if (totaltime <= 60)
                {
                    devstatus = "Online";
                }
            }
        }
        return devstatus;


    }


   



  }