using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mst_employee_device_map : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["TenantId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDeviceList();
                BindMappedEmployeeList();
            }
        }
        BindJqFunctions();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDeviceList();
        BindDepartmentList();
        BindEmployeeList();
        BindMappedEmployeeList();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
        BindMappedEmployeeList();
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
    private void BindDeviceList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.DevicesTBs
                        where d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeviceId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
            lstFruits.DataSource = data;
            lstFruits.DataTextField = "DeviceName";
            lstFruits.DataValueField = "DeviceId";
            lstFruits.DataBind();
        }
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

        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindEmployeeList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        ddlEmployee.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0) && d.DeptId != null
                        && d.CompanyId == cId & d.DeptId == dId
                        select new
                        {
                            d.EmployeeId,
                            EmpName = d.FName + " " + d.Lname
                        }).Distinct();

            ddlEmployee.DataSource = data;
            ddlEmployee.DataTextField = "EmpName";
            ddlEmployee.DataValueField = "EmployeeId";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "--Select--");
        }
    }
    
    protected void btnImport_Click(object sender, EventArgs e)
    {
        int counter = 0,dCounter=0;

        if (IsValid)
        {
            string list = "";
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                db.CommandTimeout = 5 * 60;
                foreach (ListItem item in lstFruits.Items)
                {
                    if (item.Selected)
                    {
                        list += item.Text + " " + item.Value + ",";
                        MstMapEmpDeviceTB mstMap = new MstMapEmpDeviceTB();
                        mstMap.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        mstMap.DeviceId = Convert.ToInt32(item.Value);
                        mstMap.EmpId = Convert.ToInt32(ddlEmployee.SelectedValue);
                        mstMap.IsActive = 1;
                        mstMap.TenantId = Convert.ToString(Session["TenantId"]);
                        mstMap.CreatedDate = DateTime.Now;
                        db.MstMapEmpDeviceTBs.InsertOnSubmit(mstMap);
                        db.SubmitChanges();
                    }
                    dCounter++;
                }
            }
            if (dCounter > 0)
            {
                gen.ShowMessage(this.Page, " employees map with " + dCounter + " devices");
            }
        }
        
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindMappedEmployeeList();
    }
    private void BindMappedEmployeeList()
    {
        try {
            string query = @"SELECT        MED.MapId, D.DeviceAccountId, D.DeviceName, D.DeviceSerialNo, E.FName + ' ' + E.Lname AS EmpName, E.EmployeeNo AS DeviceCode, E.MachineID AS EmployeeNo
FROM            MstMapEmpDeviceTB AS MED INNER JOIN
                         EmployeeTB AS E ON MED.EmpId = E.EmployeeId INNER JOIN
                         DevicesTB AS D ON MED.DeviceId = D.DeviceId WHERE E.IsActive=1 AND MED.IsActive=1";

            if (ddlCompany.SelectedIndex > 0)
            {
                query += " AND E.CompanyId=" + ddlCompany.SelectedValue;
            }
            if (ddlDepartment.SelectedIndex > 0)
            {
                query += " AND E.DeptId=" + ddlDepartment.SelectedValue;
            }
            if (ddlEmployee.SelectedIndex > 0)
            {
                query += " AND E.EmployeeId=" + ddlEmployee.SelectedValue;
            }

            DataTable dataTable = gen.ReturnData(query);
            gvList.DataSource = dataTable;
            gvList.DataBind();

        }
        catch(Exception ex) { gen.ShowMessage(this, ex.Message); }
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
}