using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// developer Name : Shrikant Patil
/// Date : 29th Sept 2015
/// Description : To save/update and display roster details
/// Page Name : rosterMaster.aspx
/// Table Name : RosterMasterTB & RosterDetailsTB
/// </summary>
public partial class rosterMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable dtMonthData;
    static DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    ddlmonth.SelectedIndex = DateTime.Now.Month;
                    BindYear();
                    fillcompany();
                    fillDataList();
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void BindYear()
    {
        ddlyear.Items.Clear();
        int year = DateTime.Now.AddYears(-75).Year;
        for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
        {
            ddlyear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCompany.SelectedIndex > 0)
            {
                fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void dddept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldept.SelectedIndex > 0)
            {
                fillDesignation(Convert.ToInt32(ddldept.SelectedValue));
            }
        }
        catch (Exception ex)
        {
        }
    }
    // when i click on search button it display's all the days and shift option
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            dtMonthData = new DataTable();
            DataColumn drmondays = new DataColumn("Days");
            dtMonthData.Columns.Add(drmondays.ToString());
            int k = System.DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedValue), Convert.ToInt32(ddlmonth.SelectedValue));
            for (int j = 1; j <= k; j++)
            {
                DataRow dr = dtMonthData.NewRow();
                dr[0] = j;
                dtMonthData.Rows.Add(dr);
            }
            datalistdisplay.DataSource = dtMonthData;
            datalistdisplay.DataBind();
            div.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }
    // submit code
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnsubmit.Text == "Save")  // if btn text = save then it goes inside the loop otherwsie it will update the roster details
            {
                var dataexist = from d in HR.RosterMasterTBs.Where(d => d.MonthID == Convert.ToInt32(ddlmonth.SelectedValue) && d.Year == ddlyear.SelectedItem.Text && d.EmpID == Convert.ToInt32(ddlemployee.SelectedValue)) select d;
                                if (dataexist.Count() == 0)
                {
                    RosterMasterTB RST = new RosterMasterTB();
                    RST.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    RST.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                    RST.DesgId = Convert.ToInt32(ddldesg.SelectedValue);
                    RST.EmpID = Convert.ToInt32(ddlemployee.SelectedValue);
                    RST.Month = ddlmonth.Text;
                    RST.MonthID = Convert.ToInt32(ddlmonth.SelectedValue);
                    RST.Year = ddlyear.Text;
                    HR.RosterMasterTBs.InsertOnSubmit(RST);
                    HR.SubmitChanges();
                    foreach (DataListItem item in datalistdisplay.Items)
                    {
                        Label objLabel = item.FindControl("lbldays") as Label;
                        DropDownList subobjHD = item.FindControl("ddlist") as DropDownList;
                        RosterDetailsTB RSTDetails = new RosterDetailsTB();
                        RSTDetails.RosterId = RST.RosterId;
                        RSTDetails.EmpID = Convert.ToInt32(ddlemployee.SelectedValue);
                        RSTDetails.Day = Convert.ToInt32(objLabel.Text.ToString());
                        RSTDetails.Type = subobjHD.Text.ToString();
                        HR.RosterDetailsTBs.InsertOnSubmit(RSTDetails);
                        HR.SubmitChanges();
                    }
                    g.ShowMessage(this.Page, "Data submitted successfully.");
                    clear();
                }
                else
                {
                    g.ShowMessage(this.Page, "Data already exist.");
                }
            }
            else
            {
                RosterMasterTB RST = HR.RosterMasterTBs.Where(d => d.RosterId == Convert.ToInt32(lblrosterid.Text)).First();
                RST.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                RST.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                RST.DesgId = Convert.ToInt32(ddldesg.SelectedValue);
                RST.EmpID = Convert.ToInt32(ddlemployee.SelectedValue);
                RST.Month = ddlmonth.Text;
                RST.MonthID = Convert.ToInt32(ddlmonth.SelectedValue);
                RST.Year = ddlyear.Text;
                HR.SubmitChanges();
                DataTable dtdelete = g.ReturnData("delete from RosterDetailsTB where rosterid='" + lblrosterid.Text + "'");
                foreach (DataListItem item in datalistdisplay.Items)
                {
                    Label objLabel = item.FindControl("lbldays") as Label;
                    DropDownList subobjHD = item.FindControl("ddlist") as DropDownList;
                    RosterDetailsTB RSTDetails = new RosterDetailsTB();
                    RSTDetails.RosterId = RST.RosterId;
                    RSTDetails.EmpID = Convert.ToInt32(ddlemployee.SelectedValue);
                    RSTDetails.Day = Convert.ToInt32(objLabel.Text.ToString());
                    RSTDetails.Type = subobjHD.Text.ToString();
                    HR.RosterDetailsTBs.InsertOnSubmit(RSTDetails);
                    HR.SubmitChanges();
                }
                g.ShowMessage(this.Page, "Data updated successfully.");
                clear();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void dddesg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldesg.SelectedIndex > 0)
            {
                fillEmployee(Convert.ToInt32(ddldesg.SelectedItem.Value));
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton img = (ImageButton)sender;
            string id = img.CommandArgument.ToString();
            lblrosterid.Text = id;
            var rosterdata = (from d in HR.RosterMasterTBs where d.RosterId == Convert.ToInt32(id) select d).ToList();
            fillcompany();
            ddlCompany.SelectedValue = rosterdata.First().CompanyId.ToString();
            fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
            ddldept.SelectedValue = rosterdata.First().DeptId.ToString();
            fillDesignation(Convert.ToInt32(ddldept.SelectedValue));
            ddldesg.SelectedValue = rosterdata.First().DesgId.ToString();
            fillEmployee(Convert.ToInt32(ddldesg.SelectedValue));
            ddlemployee.SelectedValue = rosterdata.First().EmpID.ToString();
            ddlmonth.SelectedValue = rosterdata.First().MonthID.ToString();
            ddlyear.SelectedValue = rosterdata.First().Year;
            ddlCompany.Enabled = false;
            ddldept.Enabled = false;
            ddldesg.Enabled = false;
            ddlemployee.Enabled = false;
            ddlmonth.Enabled = false;
            ddlyear.Enabled = false;
                        var rosterdetailsdata = (from d in HR.RosterDetailsTBs where d.RosterId == Convert.ToInt32(id) select d).ToList();
            dtMonthData = new DataTable();
            DataColumn drmondays = new DataColumn("Days");
            dtMonthData.Columns.Add(drmondays.ToString());
            int k = System.DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedValue), Convert.ToInt32(ddlmonth.SelectedValue));
            for (int j = 1; j <= k; j++)
            {
                DataRow dr = dtMonthData.NewRow();
                dr[0] = j;
                dtMonthData.Rows.Add(dr);
            }
            datalistdisplay.DataSource = dtMonthData;
            datalistdisplay.DataBind();
            int i = 0;
            foreach (DataListItem dlistItem in datalistdisplay.Items)
            {
                DropDownList ddl = (DropDownList)datalistdisplay.Items[dlistItem.ItemIndex].FindControl("ddlist");
                ddl.SelectedValue = rosterdetailsdata.ElementAt(i).Type.ToString();
                i++;
            }
            div.Visible = true;
            btnsubmit.Text = "Update";
            MultiView1.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
        }
    }
    protected void grd_roster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_roster.PageIndex = e.NewPageIndex;
            fillDataList();
        }
        catch (Exception ex)
        {
        }
    }
    private void clear()
    {
        try
        {
            ddlCompany.SelectedIndex = 0;
            ddldept.Items.Clear();
            ddldesg.Items.Clear();
            ddlmonth.SelectedIndex = 0;
            ddlyear.SelectedIndex = 0;
            ddlemployee.Items.Clear();
            dtMonthData = null;
            datalistdisplay.DataSource = dtMonthData;
            datalistdisplay.DataBind();
            btnsubmit.Text = "Save";
            ddlCompany.Enabled = true;
            ddldept.Enabled = true;
            ddldesg.Enabled = true;
            ddlemployee.Enabled = true;
            ddlmonth.Enabled = true;
            ddlyear.Enabled = true;
            fillDataList();
            MultiView1.ActiveViewIndex = 0;
        }
        catch(Exception ex)
        {
        }
    }
    // bind all datalist details
    private void fillDataList()
    {
        try
        {
            var rosterdata = (from d in HR.RosterDetailsSp() select d).ToList();
            if (rosterdata.Count() > 0)
            {
                grd_roster.DataSource = rosterdata;
                grd_roster.DataBind();
                lblcnt.Text = rosterdata.Count().ToString();
            }
        }
        catch(Exception ex)
        {
        }
    }
    // bind company details 
    private void fillcompany()
    {
        try
        {
            var data = (from dt in HR.CompanyInfoTBs
                        where dt.Status == 1
                        select dt).OrderBy(d => d.CompanyName);
            if (data != null && data.Count() > 0)
            {
                ddlCompany.DataSource = data;
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "CompanyId";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        { }
    }
    // bind department 
    private void fillDept(int companyid)
    {
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 1 && dt.CompanyId == companyid
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {
            ddldept.DataSource = data;
            ddldept.DataTextField = "DeptName";
            ddldept.DataValueField = "DeptID";
            ddldept.DataBind();
            ddldept.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldept.DataSource = null;
            ddldept.DataBind();
            ddldept.Items.Clear();
        }
    }
    // bind designation 
    private void fillDesignation(int deptid)
    {
        try
        {
            var data = (from dt in HR.MasterDesgTBs
                        where dt.Status == 1 && dt.DeptID == deptid
                        select dt).OrderBy(d => d.DesigName);
            if (data != null && data.Count() > 0)
            {
                ddldesg.DataSource = data;
                ddldesg.DataTextField = "DesigName";
                ddldesg.DataValueField = "DesigID";
                ddldesg.DataBind();
                ddldesg.Items.Insert(0, "--Select--");
            }
            else
            {
                ddldesg.DataSource = null;
                ddldesg.DataBind();
                ddldesg.Items.Clear();
            }
        }
        catch
        {
        }
    }
    //bind employee  details
    private void fillEmployee(int desgnid)
    {
        var data = (from dt in HR.EmployeeTBs
                    where (dt.RelivingStatus == null||dt.RelivingStatus==0) && dt.DesgId == desgnid
                    select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId }).OrderBy(d => d.name);
        if (data != null && data.Count() > 0)
        {
            ddlemployee.DataSource = data;
            ddlemployee.DataTextField = "name";
            ddlemployee.DataValueField = "EmployeeId";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlemployee.DataSource = null;
            ddlemployee.DataBind();
            ddlemployee.Items.Clear();
        }
    }
}