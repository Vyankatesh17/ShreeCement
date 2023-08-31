using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sys_settings : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                //txtFromDate.Attributes.Add("readonly", "readonly");
                //txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try {
            if (IsValid)
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    SystemSettingsTB system = new SystemSettingsTB();
                    var chkExists = db.SystemSettingsTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
                    if (chkExists != null)
                    {
                        system = chkExists;
                    }
                    system.LatemarkAllowed = Convert.ToInt32(txtLatemarkAllowed.Text);
                    system.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    system.TenantId = Convert.ToString(Session["TenantId"]);
                    char CL= rbCLA.SelectedIndex == 0 ? 'H' : 'F';//.ToString() ;                   
                    system.ConsiderLatemark = CL;
                    string AC = rbAttendance.SelectedIndex == 0 ? "ShiftGroup" :"RoasterGroup";
                    system.AttandanceCalculation = AC;
                    string WOC = rbWeeklyoff.SelectedIndex == 0 ? "CompanyWise" : "EmployeeWise";
                    system.WeeklyOff_Calculation = WOC;
                    string OT = rbOtCalculation.SelectedIndex == 0 ? "OTInTime" : "OTInDays";
                    system.OT_Calculation = OT;
                    if (chkqrcode.Checked == true)
                    {
                        system.IsQRCodePresent = true;
                    }
                    else
                    {
                        system.IsQRCodePresent = false;
                    }
                    if (rbOtCalculation.SelectedIndex > 0)
                    {
                        system.Half_Day_OT_Hours = TimeSpan.Parse(txthalfdayhours.Text);
                        system.Full_Day_OT_Hours = TimeSpan.Parse(txtfulldayhours.Text);
                    }                   
                    if (chkExists == null)
                    {
                        db.SystemSettingsTBs.InsertOnSubmit(system);
                    }
                    db.SubmitChanges();

                    gen.ShowMessage(this.Page, "Setting saved successfully..");
                    txtLatemarkAllowed.Text = "";
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

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = db.SystemSettingsTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
            if (data != null)
            {
                txtLatemarkAllowed.Text = data.LatemarkAllowed.Value.ToString();
                rbCLA.SelectedIndex = data.ConsiderLatemark.ToString().Equals('H') ? 0 : 1;
                rbAttendance.SelectedIndex = data.AttandanceCalculation.ToString().Equals("ShiftGroup") ?  0: 1;
                rbWeeklyoff.SelectedIndex = data.WeeklyOff_Calculation.ToString().Equals("CompanyWise") ? 0 : 1;
                rbOtCalculation.SelectedIndex = data.OT_Calculation.ToString().Equals("OTInTime") ? 0 : 1;
                txthalfdayhours.Text = data.Half_Day_OT_Hours.ToString();
                txtfulldayhours.Text = data.Full_Day_OT_Hours.ToString();
                if(data.IsQRCodePresent == true)
                {
                    chkqrcode.Checked = true;
                }
                else
                {
                    chkqrcode.Checked = false;
                }              

            }
            else
            {
                txtLatemarkAllowed.Text = "0";
            }
        }
    }
}