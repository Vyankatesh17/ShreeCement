using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class birthday_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridviewData();
        }
    }

    protected void gvBirthdayList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBirthdayList.PageIndex = e.NewPageIndex;
        BindGridviewData();
    }

    private void BindGridviewData()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.EmployeeTBs
                            join d1 in db.MasterDeptTBs on d.DeptId equals d1.DeptID
                            join d2 in db.MasterDesgTBs on d.DesgId equals d2.DesigID
                            where d.IsActive == true && d.BirthDate.Value.Date == DateTime.Now.Date && d.TenantId==Convert.ToString(Session["TenantId"])
                            select new
                            {
                                EmpName = d.FName + " " + d.Lname,
                                EmpNo = d.EmployeeNo,
                                d.MachineID,
                                d.BirthDate,
                                d1.DeptName,
                                d2.DesigName
                            }).Distinct();

                gvBirthdayList.DataSource = data;
                gvBirthdayList.DataBind();
            }
        }
        catch (Exception ex) { }
    }
}