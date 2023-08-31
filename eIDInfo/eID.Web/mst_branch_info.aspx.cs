using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mst_branch_info : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindBranchList();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        
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

    protected void lbtnEvent_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        MultiView1.ActiveViewIndex = 1;
        hfKey.Value = linkButton.CommandArgument;

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            OutDoorEntryTB outDoorEntryTB = db.OutDoorEntryTBs.Where(d => d.OddId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
            db.OutDoorEntryTBs.DeleteOnSubmit(outDoorEntryTB);
            gen.ShowMessage(this.Page, "OD Entry deleted..");
            BindBranchList();
        }
    }
    private void BindBranchList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.OutDoorEntryTBs
                        join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                        where d.EmployeeId == Convert.ToInt32(Session["UserId"].ToString())
                        select new
                        {
                            d.Description,
                            d.HrRemark,
                            d.HrStatus,
                            d.ManagerRemark,
                            d.ManagerStatus,
                            d.OddId,
                            d.FromDate,
                            d.ToDate,
                            d.TravelPlace,
                            d.TravelReason,
                            d.EmployeeId,
                            e.EmployeeNo,
                            EmpName = e.FName + " " + e.Lname
                        });

            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}