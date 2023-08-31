using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for SPCommon
/// </summary>
public class SPCommon
{
    public SPCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<CompanyInfoTB> DDLCompanyBind(string uType,string tId, string comid)
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            List<CompanyInfoTB> finaldata = null;
            var data = (from d in db.CompanyInfoTBs where d.Status==1 select d).ToList();
            if (uType != "SuperAdmin")
            {
                finaldata = data.Where(d => d.TenantId == tId).ToList();
            }
            if(uType == "LocationAdmin")
            {
                finaldata = data.Where(d => d.CompanyId == Convert.ToInt32(comid)).ToList();
            }



            return finaldata;
        }
    }

    public static List<MasterDeptTB> DDLDepartmentBind(string companyname)
    {
        using (HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext())
        {
            //List<MasterDeptTB> finaldata = null;

            var data = (from dt in HR.CompanyInfoTBs
                        join dep in HR.MasterDeptTBs on dt.CompanyId equals dep.CompanyId
                        where dt.CompanyName == companyname
                        select dep).OrderBy(dt => dt.DeptName).ToList();

            //finaldata = data;

            return data;
        }
    }















}
public static class JSONHelper
{
    public static string ToJSON(this object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }

    public static string ToJSON(this object obj, int recursionDepth)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
        return serializer.Serialize(obj);
    }
}