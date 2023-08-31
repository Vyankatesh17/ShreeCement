using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class shift_change_manually : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDepartmentList();
                BindEmployeeList();
                BindShiftList();
                //txtFromDate.Attributes.Add("readonly", "readonly");
                //txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindShiftList();
    }
    
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try {
            using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
            {
                lblDate.Text = txtFromDate.Text;
                DateTime from = Genreal.GetDate(txtFromDate.Text);
                int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
                int eId = ddlEmployee.SelectedIndex > 0 ? Convert.ToInt32(ddlEmployee.SelectedValue) : 0;
                var data = db.RosterDetailsTBs.Where(d => d.CompanyId == cId && d.EmpID == eId&&d.Date.Value.Date==from.Date && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
                if (data != null)
                {
                    lblCurrentShift.Text = data.Type;
                    btnAssignShift.Visible=ddlShift.Visible = true;
                }
                else
                {
                    lblCurrentShift.Text = "No Shift";
                    gen.ShowMessage(this.Page, string.Format(@"Shift not assigned for selected date"));
                    btnAssignShift.Visible = ddlShift.Visible = false;
                }
            }
        }
        catch(Exception ex) { }
    }
    private void BindCompanyList()
    {
        try
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
        catch (Exception ex) { }
    }
    private void BindDepartmentList()
    {
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs where d.Status == 1 && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select new { d.DeptName, d.DeptID }).Distinct();

            if (data.Count() > 0)
            {
                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }


    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId }).OrderBy(d => d.name);
            if (data != null && data.Count() > 0)
            {
                ddlEmployee.DataSource = data;
                ddlEmployee.DataTextField = "name";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }

    }
    private void BindShiftList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlShift.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterShiftTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.Shift, d.ShiftID }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlShift.DataSource = data;
                ddlShift.DataTextField = "Shift";
                ddlShift.DataValueField = "ShiftID";
                ddlShift.DataBind();
            }
            ddlShift.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void btnAssignShift_Click(object sender, EventArgs e)
    {
               
        try
        {
            if (btnAssignShift.Visible==true)
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    DateTime from = Genreal.GetDate(lblDate.Text);
                    int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
                    int eId = ddlEmployee.SelectedIndex > 0 ? Convert.ToInt32(ddlEmployee.SelectedValue) : 0;
                    RosterDetailsTB data = db.RosterDetailsTBs.Where(d => d.CompanyId == cId && d.EmpID == eId && d.Date.Value.Date == from.Date && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
                    data.ShiftId = Convert.ToInt32(ddlShift.SelectedValue);
                    data.Type = ddlShift.SelectedItem.Text;
                    db.SubmitChanges();

                    gen.ShowMessage(this.Page, string.Format(@"Shift assigned sucessfully.."));
                    ddlCompany.SelectedIndex = ddlDepartment.SelectedIndex = ddlEmployee.SelectedIndex = ddlShift.SelectedIndex = 0;
                    txtFromDate.Text = lblCurrentShift.Text =lblDate.Text= "";

                    btnAssignShift.Visible = ddlShift.Visible = false;
                }
            }
            else
            {
                gen.ShowMessage(this.Page, "Please select current shift first");
            }
        }
        catch (Exception ex) { gen.ShowMessage(this.Page, ex.Message); }
       
    }
}