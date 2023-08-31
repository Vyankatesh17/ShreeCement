using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mst_roster_list : System.Web.UI.Page
{
    Genreal gen = new Genreal(); DataTable dtMonthData;
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
        if (!string.IsNullOrEmpty(Convert.ToString(Session["TenantId"])))
        {
            if (!IsPostBack)
            {
                BindDataGridViewList();
                BindCompanyList();
                BindDepartmentList();
                BindShiftList();
                BindEmployeeList();
               
            }
            BindJqFunctions();
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindShiftList();
        BindEmployeeList();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                if (datalistdisplay.Items.Count > 0)
                {
                    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                    {
                        string list = "";
                        foreach (ListItem empitem in lstFruits.Items)
                        {
                            if (empitem.Selected)
                            {
                                list += empitem.Text + " " + empitem.Value + ",";

                                RosterMasterTB roster = new RosterMasterTB();
                                roster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                roster.EmpID = Convert.ToInt32(empitem.Value);
                                roster.FromDate = Genreal.GetDate(txtFromDate.Text);
                                roster.ToDate = Genreal.GetDate(txtToDate.Text);
                                roster.Year = Genreal.GetDate(txtToDate.Text).Year.ToString();
                                roster.TenantId = Convert.ToString(Session["TenantId"]);
                                roster.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                                db.RosterMasterTBs.InsertOnSubmit(roster);
                                db.SubmitChanges();
                                foreach (DataListItem item in datalistdisplay.Items)
                                {
                                    Label objLabel = item.FindControl("lbldays") as Label;
                                    Label lblDate = item.FindControl("lbldate") as Label;
                                    Label lblMonth = item.FindControl("lblMonth") as Label;
                                    Label lblYear = item.FindControl("lblYear") as Label;
                                    DropDownList subobjHD = item.FindControl("ddlist") as DropDownList;
                                    RosterDetailsTB detailsTB = new RosterDetailsTB();

                                    DateTime dateTime = new DateTime(Convert.ToInt32(lblYear.Text), Convert.ToInt32(lblMonth.Text), Convert.ToInt32(objLabel.Text));
                                    var checkExists = db.RosterDetailsTBs.Where(d => d.EmpID == Convert.ToInt32(empitem.Value) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                                      && d.Date.Value.Date == dateTime).FirstOrDefault();

                                    if (checkExists != null)
                                    {
                                        detailsTB = db.RosterDetailsTBs.Where(d => d.RosterDetailID == checkExists.RosterDetailID).FirstOrDefault();
                                    }

                                    detailsTB.RosterId = roster.RosterId;
                                    detailsTB.EmpID = Convert.ToInt32(empitem.Value);
                                    detailsTB.Day = Convert.ToInt32(objLabel.Text.ToString());
                                    detailsTB.Type = subobjHD.SelectedItem.Text.ToString();
                                    detailsTB.Date = dateTime;
                                    detailsTB.ShiftId = Convert.ToInt32(subobjHD.SelectedValue);
                                    detailsTB.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                    detailsTB.TenantId = Convert.ToString(Session["TenantId"]);
                                    if (checkExists == null)
                                    {
                                        db.RosterDetailsTBs.InsertOnSubmit(detailsTB);
                                    }
                                    db.SubmitChanges();
                                }
                            }
                        }

                        gen.ShowMessage(this.Page, "Shift roster generated and saved successfully..");
                        txtFromDate.Text = txtToDate.Text = "";
                        //ddlCompany.SelectedIndex = ddlDepartment.SelectedIndex = ddlEmployee.SelectedIndex = ddlShift.SelectedIndex = 0;
                        MultiView1.ActiveViewIndex = 0;
                        GenerateRoster();
                        BindDataGridViewList();
                    }
                }
                else
                {
                    gen.ShowMessage(this.Page, "Please genretae roster first..");
                }
            }
        }
        catch(Exception ex)
        {
            gen.ShowMessage(this.Page, ex.Message);
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
    private void BindEmployeeList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;        
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0) && d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        && d.DeptId==dId
                        select new { name = d.FName + ' ' + d.Lname, d.EmployeeId }).OrderBy(d => d.name);
            //if (data != null && data.Count() > 0)
            //{
                lstFruits.DataSource = data;
                lstFruits.DataTextField = "name";
                lstFruits.DataValueField = "EmployeeId";
                lstFruits.DataBind();
            //}
            //ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
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
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void BindDataGridViewList()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = (from r in db.RosterMasterTBs
                        join e in db.EmployeeTBs on r.EmpID equals e.EmployeeId
                        join c in db.CompanyInfoTBs on r.CompanyId equals c.CompanyId
                        join d in db.MasterDeptTBs on r.DeptId equals d.DeptID
                        where e.IsActive == true && d.TenantId==Convert.ToString(Session["TenantId"])
                        select new
                        {
                            EmpName = e.FName + " " + e.Lname,
                            r.RosterId,
                            r.FromDate,
                            r.ToDate,
                            e.EmployeeNo,
                            c.CompanyName,
                            d.DeptName
                        }).Distinct().OrderByDescending(d=>d.RosterId);
            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try {
            GenerateRoster();
        }
        catch(Exception ex) { }
    }

    protected void datalistdisplay_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var myDropDownList = e.Item.FindControl("ddlist") as DropDownList;
            int currentItemID = int.Parse(this.datalistdisplay.DataKeys[e.Item.ItemIndex].ToString());

            myDropDownList.DataSource = GetDDLDataSource();

            myDropDownList.DataTextField = "Shift";
            myDropDownList.DataValueField = "ShiftID";
            myDropDownList.DataBind();

            myDropDownList.Items.Insert(0, new ListItem("WO", "0"));

            myDropDownList.SelectedValue = ddlShift.SelectedValue;
        }
    }
    private DataTable GetDDLDataSource()
    {
        string query = string.Format(@"select ShiftID,Shift from MasterShiftTB where CompanyID={0} and TenantId='{1}'", ddlCompany.SelectedValue, Convert.ToString(Session["TenantId"]));
        DataTable dataTable = gen.ReturnData(query);
        return dataTable;
    }
    private void GenerateRoster()
    {
        DateTime startDate = Genreal.GetDate(txtFromDate.Text);
        DateTime endDate = Genreal.GetDate(txtToDate.Text);

        //if (endDate.Subtract(startDate).TotalDays > 30)
        //{
        //    gen.ShowMessage(this.Page, "Please select between one month");
        //}
        //else
        //{
            dtMonthData = new DataTable();
            DataColumn DayNo = new DataColumn("DayNo");
            DataColumn DayName = new DataColumn("DayName");
            DataColumn Date = new DataColumn("Date");
            dtMonthData.Columns.Add(DayNo);
            dtMonthData.Columns.Add(DayName);
            dtMonthData.Columns.Add(Date);
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                DataRow dr = dtMonthData.NewRow();
                dr[0] = day.Day;
                dr[1] = day.ToString("dddd");
                dr[2] = day;
                dtMonthData.Rows.Add(dr);
            }
        //}

        datalistdisplay.DataSource = dtMonthData;
        datalistdisplay.DataBind();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }

    protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
}