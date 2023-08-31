using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Work_From_Home : System.Web.UI.Page
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
                fillcompany();
                BindWorkfromhomeList();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        if (gvDataList.Rows.Count > 0)
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    WorkFromHomeTB workfromhome = new WorkFromHomeTB();
                    workfromhome.Description = txtDetails.Text;
                    workfromhome.HR_Status = "Pending";
                    workfromhome.Manager_Status = "Pending";
                    workfromhome.FromDate = Convert.ToDateTime(txtFromDate.Text);
                    workfromhome.ToDate = Convert.ToDateTime(txtToDate.Text);                   
                    workfromhome.Reason = txtTravelReason.Text;
                    workfromhome.EmployeeId = Session["UserType"].ToString() == "User" ? Convert.ToInt32(Convert.ToString(Session["EmpId"])) : Convert.ToInt32(ddlEmployee.SelectedValue);
                    workfromhome.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    workfromhome.TenantId = Convert.ToString(Session["TenantId"]);
                    db.WorkFromHomeTBs.InsertOnSubmit(workfromhome);
                    db.SubmitChanges();

                    //#region Send mail to department head
                    //SendMail(outDoor.EmployeeId.Value, outDoor.FromDate.Value.ToShortDateString(), outDoor.ToDate.Value.ToShortDateString(), outDoor.FromTime.Value.ToString(), outDoor.ToTime.Value.ToString(), outDoor.TravelReason, outDoor.TravelPlace);
                    //#endregion

                    gen.ShowMessage(this.Page, "Work From home saved successfully..");
                    txtDetails.Text = txtFromDate.Text = txtToDate.Text  = txtTravelReason.Text = "";
                    ddlCompany.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { throw ex; }
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }


    private void BindWorkfromhomeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.WorkFromHomeTBs
                        join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                        where d.TenantId == Convert.ToString(Session["TenantId"])
                        select new
                        {
                            d.Description,
                            d.HR_Remark,
                            d.HR_Status,
                            d.Manager_Remark,
                            d.Manager_Status,
                            d.Work_From_Home_Id,
                            d.FromDate,
                            d.ToDate,
                            d.CompanyId,
                            d.Reason,
                            d.EmployeeId,
                            e.EmployeeNo,
                            EmpName = e.FName + " " + e.Lname
                        });
            if (Session["UserType"].ToString().Equals("User"))
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"].ToString())).Distinct();
            }
            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
    }

    protected void lbtnEvent_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        hfKey.Value = linkButton.CommandArgument;

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            WorkFromHomeTB workfromhome = db.WorkFromHomeTBs.Where(d => d.Work_From_Home_Id == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
            db.WorkFromHomeTBs.DeleteOnSubmit(workfromhome);
            db.SubmitChanges();
            gen.ShowMessage(this.Page, "Work From Home Entry deleted..");
            BindWorkfromhomeList();
        }
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
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
    private void BindEmployeeList()
    {
        ddlEmployee.Items.Clear();
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true && (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.CompanyId == cId
                        select new { EmpName = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.ManagerID }).Distinct();
            if (Session["UserType"].ToString().Equals("User"))
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            if (data != null && data.Count() > 0)
            {
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    data = data.Where(d => d.ManagerID == manId || d.EmployeeId == manId).Distinct();
                }
                ddlEmployee.DataSource = data;
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["UserType"].ToString().Equals("User"))
            {
                ddlEmployee.SelectedValue = Session["EmpId"].ToString();
            }
        }
    }

















}