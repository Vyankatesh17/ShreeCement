using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WorkFromHome_Approve_Manager : System.Web.UI.Page
{

    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillcompany();
            BindApproveByManagerList();
        }
        if (gvWorkFromHome.Rows.Count > 0)
            gvWorkFromHome.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    protected void lbtnApprove_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        hfKey.Value = linkButton.CommandArgument;
        MultiView1.ActiveViewIndex = 1;

        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.WorkFromHomeTBs
                            join emp in db.EmployeeTBs on d.EmployeeId equals emp.EmployeeId
                            where d.Work_From_Home_Id == Convert.ToInt32(linkButton.CommandArgument)
                            select new
                            {
                                d.Work_From_Home_Id,
                                d.Manager_Status,
                                d.FromDate,
                                d.ToDate,
                                d.Reason,
                                d.Description,
                                EmpName = emp.FName + " " + emp.Lname,
                                d.Manager_Remark
                            }).FirstOrDefault();

                lblEmpName.Text = data.EmpName;
                lblFromdate.Text = data.FromDate.Value.Date.ToString("MM/dd/yyyy");
                lblTodate.Text = data.ToDate.Value.Date.ToString("MM/dd/yyyy");
                lblReason.Text = data.Reason;
            }
        }
        catch (Exception ex) { }
    }

    protected void lbtnReject_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                WorkFromHomeTB applicationsTB = db.WorkFromHomeTBs.Where(d => d.Work_From_Home_Id == Convert.ToInt32(linkButton.CommandArgument)).FirstOrDefault();
                applicationsTB.Manager_Status = "Reject";
                db.SubmitChanges();

                gen.ShowMessage(this.Page, "Work From Home Rejected..");

                // Send mail to employee code here

                BindApproveByManagerList();
            }
        }
        catch (Exception ex) { }
    }

    private void BindApproveByManagerList()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.WorkFromHomeTBs
                            join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                            where e.IsActive == true && d.Manager_Status == "Pending" && d.HR_Status == "Pending" && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                            select new
                            {
                                d.Work_From_Home_Id,
                                d.Manager_Status,
                                d.FromDate,
                                d.ToDate,                                
                                d.Reason,
                                d.Description,
                                EmpName = e.FName + " " + e.Lname,
                                e.EmployeeNo,
                                e.MachineID,
                                e.ReportingStatus,
                                e.EmployeeId
                            }).Distinct();
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    data = data.Where(d => d.ReportingStatus == manId).Distinct();
                }
                gvWorkFromHome.DataSource = data;
                gvWorkFromHome.DataBind();
            }
        }
        catch (Exception ex) { }
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                WorkFromHomeTB applicationsTB = db.WorkFromHomeTBs.Where(d => d.Work_From_Home_Id == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.Manager_Status = "Approve";
                applicationsTB.Manager_Remark = txtReason.Text;
                db.SubmitChanges();


                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindApproveByManagerList();
                gen.ShowMessage(this.Page, "Work From Home Approved Successfully..");
            }
        }
        catch (Exception ex) { }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                WorkFromHomeTB applicationsTB = db.WorkFromHomeTBs.Where(d => d.Work_From_Home_Id == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.Manager_Status = "Reject";
                applicationsTB.Manager_Remark = txtReason.Text;
                db.SubmitChanges();

                gen.ShowMessage(this.Page, "Work From Home Rejected..");

                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindApproveByManagerList();
            }
        }
        catch (Exception ex) { throw ex; }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindApproveByManagerList();
    }
    private void fillcompany()
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


}