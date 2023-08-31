using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class setup_salary_desigwise : System.Web.UI.Page 
{
    public bool salary_flag = false;
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

    Genreal g = new Genreal();
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["desig"]))
                {
                    Response.Redirect("MasterDesignation.aspx");
                }
                else
                {
                    GetDesignationDetails();
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
             
    }

    public void GetDesignationDetails()
    {
        int desigId = Convert.ToInt32(Request.QueryString["desig"]);
        var deptData = (from d in HR.MasterDesgTBs
                        where d.DesigID == desigId
                        select new
                        {
                            d.DesigID,
                            d.CompanyId,
                            d.DesigName,
                            d.TenantId,
                            Status = d.Status == 1 ? "Active" : "In Active"
                        }).FirstOrDefault();

        txtdesigname.Text = deptData != null ? deptData.DesigName : "";
        hfKey.Value = Request.QueryString["desig"].ToString();
        if (deptData != null)
        {
            var chkExists = HR.DesigwiseSalaryTBs.FirstOrDefault(d => d.DesigId == Convert.ToInt32(hfKey.Value) && d.IsActive == true);
            if (chkExists != null)
            {
                btnsubmit.Text = "Update";
            }
        }
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnsubmit.Text == "Update")
            {
                var desigwiseSalary = HR.DesigwiseSalaryTBs.FirstOrDefault(d => d.DesigId == Convert.ToInt32(hfKey.Value) && d.IsActive == true);

                desigwiseSalary.SalaryPerDay = string.IsNullOrEmpty(txtDailySalary.Text) ? 0 : Convert.ToDecimal(txtDailySalary.Text);
                HR.SubmitChanges();
                g.ShowMessageRedirect(this.Page, "Designation wise salary details updated successfully", "MasterDesignation.aspx");
            }
            else
            {
                DesigwiseSalaryTB desigwise = new DesigwiseSalaryTB();
                desigwise.DesigId = Convert.ToInt32(hfKey.Value);
                desigwise.CreatedBy = Convert.ToInt32(Session["EmpId"]);
                desigwise.CreatedDate = DateTime.Now;
                desigwise.SalaryPerDay = string.IsNullOrEmpty(txtDailySalary.Text) ? 0 : Convert.ToDecimal(txtDailySalary.Text);
                desigwise.IsActive = true;
                desigwise.TenantId = Convert.ToString(Session["TenantId"]);
                HR.DesigwiseSalaryTBs.InsertOnSubmit(desigwise);
                HR.SubmitChanges();
                g.ShowMessageRedirect(this.Page, "Designation wise salary details saved successfully", "MasterDesignation.aspx");
                //modpop.Message = "Submitted Successfully";
                //modpop.ShowPopUp();

                Clear();
            }
        }
        catch(Exception ex)
        {
            g.ShowMessage(this.Page, $"Something went wrong while {btnsubmit.Text} details");
        }
    }

    public void Clear()
    {
        txtdesigname.Text = null;
        lbldesid.Text = "";
        btnsubmit.Text = "Save";

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterDesignation.aspx");
    }

}