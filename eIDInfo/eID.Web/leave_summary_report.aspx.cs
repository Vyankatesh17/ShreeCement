using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class leave_summary_report : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
        }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                fillcompany();
                BindLeaveNamesList();
                BindLeaveSummaryDetails();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;                    
                    employee.Visible = true;
                }


            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLeaveSummaryDetails();
    }
    private void BindLeaveNamesList()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            var LeaveTypeData = (from d in db.masterLeavesTBs
                                 join k in db.LeaveAllocationTBs on d.LeaveID equals k.LeaveID
                                 where d.Status == 1 && d.CompanyId==cId && d.TenantId==Convert.ToString(Session["TenantId"])
                                 select new
                                 {
                                     d.LeaveID,
                                     d.LeaveName
                                 }).Distinct();

            List<ListItem> list = new List<ListItem>();
            list.Add( new ListItem("--All--", "0"));
            foreach (var item in LeaveTypeData)
            {

                list.Add(new ListItem(item.LeaveName, item.LeaveID.ToString()));
            }

            list.Add(new ListItem("Loss of Pay", (LeaveTypeData.Count()+1).ToString()));
            ddlLeaveName.DataSource = list;
            ddlLeaveName.DataTextField = "Text";
            ddlLeaveName.DataValueField = "Value";
            ddlLeaveName.DataBind();
            //if (LeaveTypeData.Count() > 0)
            //{
            //    ddlLeaveName.DataSource = LeaveTypeData;
            //    ddlLeaveName.DataTextField = "LeaveName";
            //    ddlLeaveName.DataValueField = "LeaveID";
            //    ddlLeaveName.DataBind();
            //    ddlLeaveName.Items.Insert(LeaveTypeData.Count()+1, new ListItem("Loss of Pay", (LeaveTypeData.Count()+1).ToString()));
            //    ddlLeaveName.Items.Insert(0, new ListItem("--All--", "0"));
            //}
            //else
            //{
            //    ddlLeaveName.Items.Clear();
            //    ddlLeaveName.Items.Insert(0, new ListItem("--All--", "0"));
            //}
        }
    }
    private void BindLeaveSummaryDetails()
    {
        bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
        using (HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = (from m in db.LeaveAllocationTBs
                                 join n in db.masterLeavesTBs on m.LeaveID equals n.LeaveID
                                 join e in db.EmployeeTBs on m.EmployeeID equals e.EmployeeId
                                 where e.IsActive == true && ((DateTime)m.FromDateAllocation).Year == DateTime.Now.Year
                                  && ((DateTime)m.ToDateAllocation).Year == DateTime.Now.Year
                                  && m.PendingLeaves != 0 && n.TenantId == Convert.ToString(Session["TenantId"])
                        select new
                                 {
                                     n.LeaveName,
                                     m.LeaveID,
                                     LeaveBalance = m.PendingLeaves,
                                     EligibleLeaves = m.TotalAllocatedLeaves,
                                     TakenLeaves = m.TotalAllocatedLeaves - m.PendingLeaves,
                                     e.EmployeeId,
                                     e.FName,
                                     e.Lname,
                                     e.EmployeeNo,
                                     empName=e.FName+" "+e.Lname
                                 }).Distinct();
            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
                data = data.Where(d => d.EmployeeNo.Contains(txtEmployeeCode.Text)).Distinct();

            
            if (!string.IsNullOrEmpty(txtEmpName.Text))
            {
                data = data.Where(d => d.FName.Contains(txtEmpName.Text) || d.Lname.Contains(txtEmpName.Text) || d.empName.Contains(txtEmpName.Text)).Distinct();
            }

            if(ddlLeaveName.SelectedIndex>0)
            {
                data = data.Where(d => d.LeaveName == ddlLeaveName.SelectedItem.Text).Distinct();
            }
            if (Session["UserType"].ToString() == "User")
            {
                data =data.Where(d=>d.EmployeeId== Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            if (AdminStatus == false)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            gvDataList.DataSource = data;
            gvDataList.DataBind();

        }
    }
    private void fillcompany()
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
            ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvDataList.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLeaveNamesList();
    }
}