using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mst_leave_info : System.Web.UI.Page
{
    #region Variable Declaration
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion
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
                try
                {
                    fillcompany();
                    BindAllLeave();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (grd_Leave.Rows.Count > 0)
            grd_Leave.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }
    #region Methods
    private void fillcompany()
    {
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
    }
    private void BindAllLeave()
    {
        try
        {
            var deptData = (from d in HR.masterLeavesTBs
                            join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                            where d.TenantId==Convert.ToString(Session["TenantId"])
                           select new
                           {
                               d.LeaveID,
                               d.LeaveName,
                               d.YearlyLimit,
                               d.LeaveTypeSName,
                               c.CompanyName,
                               Status = d.Status == 1 ? "Active" : "In Active"
                           }).Distinct();
            if (deptData.Count() > 0)
            {
                grd_Leave.DataSource = deptData;
                grd_Leave.DataBind();
            }
            else
            {
                grd_Leave.DataSource = null;
                grd_Leave.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void Clear()
    {
        try
        {
            txtleavename.Text = null;
            txtmaxday.Text = null;
            MultiView1.ActiveViewIndex = 0;
            btnsubmit.Text = "Save";
            BindAllLeave();
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Events
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(txtmaxday.Text) > 365)
            {
                txtmaxday.Text = "";
                g.ShowMessage(this.Page, "Invalid Days");
            }
            else
            {
                #region
                if (btnsubmit.Text == "Save")
                {   //check Exits
                    var dte = from p in HR.masterLeavesTBs.Where(d => d.LeaveName == txtleavename.Text && d.CompanyId==Convert.ToInt32(ddlCompany.SelectedValue)) select p;
                    if (dte.Count() > 0)
                    {
                        txtleavename.Text = "";
                        g.ShowMessage(this.Page, "Leave Details Already Exists");
                    }
                    else
                    {//Save Code
                        masterLeavesTB leave = new masterLeavesTB();
                        leave.LeaveName = txtleavename.Text;
                        leave.YearlyLimit = int.Parse(txtmaxday.Text);
                        leave.Status = 1;
                        leave.LeaveTypeSName = txtShortName.Text;
                        leave.IsCarryForward = chkCarryforward.Checked ? true : false;
                        if (chkCarryforward.Checked)
                        {
                            leave.CarryForwardLimit = string.IsNullOrEmpty(txtCarryforwardLimit.Text) ? 0 : Convert.ToInt32(txtCarryforwardLimit.Text);
                        }
                        leave.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        leave.TenantId = Convert.ToString(Session["TenantId"]);
                        HR.masterLeavesTBs.InsertOnSubmit(leave);
                        HR.SubmitChanges();
                        g.ShowMessage(Page, "Submitted Successfully");
                        Clear();
                    }
                }
                else
                {//check Exits
                    var dte = from p in HR.masterLeavesTBs.Where(d => d.LeaveName == txtleavename.Text && d.CompanyId==Convert.ToInt32(ddlCompany.SelectedValue) && d.LeaveID != Convert.ToInt32(lblLeaveid.Text)) select p;
                    if (dte.Count() > 0)
                    {
                        txtleavename.Text = "";
                        g.ShowMessage(this.Page, "Leave Name Already Exist");
                    }
                    else
                    {//Update Code
                        masterLeavesTB leave = HR.masterLeavesTBs.Where(s => s.LeaveID == Convert.ToInt32(lblLeaveid.Text)).First();
                        leave.LeaveName = txtleavename.Text;
                        leave.YearlyLimit = int.Parse(txtmaxday.Text);
                        leave.Status =1;
                        leave.TenantId = Convert.ToString(Session["TenantId"]);
                        HR.SubmitChanges();
                        g.ShowMessage(this.Page, "Updated Successfully");
                        Clear();
                    }
                }
                #endregion
            }
        }
        catch (Exception)
        {
            throw;
        }


    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 1;
            ImageButton Lnk = (ImageButton)sender;
            string LeaveID = Lnk.CommandArgument;
            lblLeaveid.Text = LeaveID;

            masterLeavesTB leave = HR.masterLeavesTBs.Where(s => s.LeaveID == Convert.ToInt32(LeaveID)).First();
            txtleavename.Text = leave.LeaveName;
            txtmaxday.Text = leave.YearlyLimit.ToString();
            btnsubmit.Text = "Update";
            ddlCompany.SelectedValue = leave.CompanyId.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void grd_Leave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
        grd_Leave.PageIndex = e.NewPageIndex;

        BindAllLeave();
          }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        { 
        MultiView1.ActiveViewIndex = 1;

        txtleavename.Text = null;
        txtmaxday.Text = null;
      
        BindAllLeave();
        btnsubmit.Text = "Save";

            
        }
        catch (Exception)
        {
            throw;
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void chkCarr_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (chkCarr.SelectedIndex == 0)
        //{

        //}   
    }

    #endregion


    protected void grd_Leave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Leave.Rows.Count > 0)
        {
            grd_Leave.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Leave.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }
}