using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_attendance : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                //ddlMonth.SelectedIndex = DateTime.Now.Month;
                //BindYear();
                BindCompanyList();
                BindDepartmentList();
                BindEmployeeList();
                BindDeviceList();
                //BindDataGridView();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;
                    department.Visible = true;
                    employee.Visible = true;
                    device.Visible = true;
                }

            }
        }
        if (gvList.Rows.Count > 0)
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindDataGridView();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try 
        {
            BindDataGridView();
        }
        catch(Exception ex) { }
    }
    private void BindEmployeeList()
    {
        bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && (d.RelivingStatus == null||d.RelivingStatus==0) && d.DeptId != null
                        && d.CompanyId==cId & d.DeptId==dId
                        select new
                        {
                            d.EmployeeId,
                            EmpName = d.FName + " " + d.Lname
                        }).Distinct();
            if (AdminStatus == false)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            ddlEmployee.DataSource = data;
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "--Select--");
        }
    }
    private void BindDataGridView()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int eId = ddlEmployee.SelectedIndex > 0 ? Convert.ToInt32(ddlEmployee.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            int deviceId = ddlDevice.SelectedIndex > 0 ? Convert.ToInt32(ddlDevice.SelectedValue) : 0;
            //var data = (from d in db.DeviceLogsTBs
            //            join dv in db.DevicesTBs on d.DeviceAccountId equals Convert.ToInt32( dv.DeviceAccountId)
            //            join e in db.EmployeeTBs on d.EmpID equals e.EmployeeId
            //            where e.IsActive == true && d.AttendDate.Value.Year==Convert.ToInt32(ddlYear.SelectedValue) && d.AttendDate.Value.Month==ddlMonth.SelectedIndex
            //            && d.TenantId==Convert.ToString(Session["TenantId"])
            //            select new
            //            {
            //                d.CompanyId,
            //                EmpName = e.FName + " " + e.Lname,
            //                e.EmployeeNo,
            //                e.EmployeeId,
            //                e.MachineID,
            //                d.AccessCardNo,
            //                Date=d.AttendDate.Value,
            //                d.DeviceAccountId,
            //                dv.DeviceSerialNo,
            //                dv.DeviceName,
            //                d.PunchStatus,
            //                Time = d.ATime.Value,
            //                e.DeptId,
            //                d.CurrentTemp
            //            }).Distinct();

            var data = (from d in db.DeviceLogsTBs
                        join dv in db.DevicesTBs on d.DeviceAccountId equals Convert.ToInt32(dv.DeviceAccountId)
                        join e in db.EmployeeTBs on d.EmpID equals e.EmployeeId
                        where e.IsActive == true 
                        && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new
                        {
                            d.CompanyId,
                            EmpName = e.FName + " " + e.Lname,
                            e.EmployeeNo,
                            e.EmployeeId,
                            e.MachineID,
                            d.AccessCardNo,
                            Date = d.AttendDate.Value,
                            d.DeviceAccountId,
                            dv.DeviceSerialNo,
                            dv.DeviceName,
                            d.PunchStatus,
                            Time = d.ATime.Value,
                            e.DeptId,
                            d.CurrentTemp,
                            d.AttendDate,
                            dv.DeviceId
                        }).Distinct();


            if (Session["UserType"].ToString() == "User")
            {
                eId = Convert.ToInt32(Session["EmpId"]);
            }
            if (cId > 0)
            {
                data = data.Where(d => d.CompanyId == cId).Distinct();
            }
            if (dId > 0)
            {
                data = data.Where(d => d.DeptId == dId).Distinct();
            }
            if (eId > 0)
            {
                data = data.Where(d => d.EmployeeId == eId).Distinct();
            }
            //if (!string.IsNullOrEmpty(txtEmpCode.Text))
            //{
            //    data = data.Where(d => d.EmployeeNo.Contains(txtEmpCode.Text)).Distinct();
            //}
            if (AdminStatus == false)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                data = data.Where(d => d.AttendDate >= Convert.ToDateTime(txtFromDate.Text) && d.AttendDate <= Convert.ToDateTime(txtToDate.Text)).Distinct();
            }
            if (deviceId > 0)
            {                
                data = data.Where(d => d.DeviceId == deviceId).Distinct();
            }
            gvList.DataSource = data;
            gvList.DataBind();

        }
    }
    private void BindCompanyList()
    {
        ddlCompany.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindDepartmentList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeptName, d.DeptID }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
            //ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    //private void BindYear()
    //{
    //    ddlYear.Items.Clear();
    //    int year = DateTime.Now.AddYears(-75).Year;
    //    for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
    //    {
    //        ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //    }
    //    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    //}

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindEmployeeList();
        BindDeviceList();
    }

    private void BindDeviceList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlDevice.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.DevicesTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeviceName, d.DeviceId }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlDevice.DataSource = data;
                ddlDevice.DataTextField = "DeviceName";
                ddlDevice.DataValueField = "DeviceId";
                ddlDevice.DataBind();
            }
            //ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        ddlDevice.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvList.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
}