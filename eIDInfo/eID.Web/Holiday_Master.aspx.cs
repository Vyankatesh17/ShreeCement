using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;


public partial class Holiday_Master : System.Web.UI.Page
{
    HrPortalDtaClassDataContext EX = new HrPortalDtaClassDataContext();
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
                
                MultiView1.ActiveViewIndex = 0;
                BindGrid();
                fillcompany();
                //string date = DateTime.Now.ToString("MM/dd/yyyy");
                //dtpholydate.Text = date;
                //dtpholydate.Attributes.Add("ReadOnly", "ReadOnly");

            }

        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (grd_Holiday.Rows.Count > 0)
            grd_Holiday.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
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
    private void BindGrid()
    {
        var Data = (from d in EX.HoliDaysMasters
                    join c in EX.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                    where d.Status == 1 && d.TenantId==Convert.ToString(Session["TenantId"])
                    select new { d.HolidaysID, d.HoliDaysName, d.Date, d.HolidayType,d.LeaveType,c.CompanyName }).OrderBy(d => d.Date);
        if (ddlsort.SelectedIndex == 1 && txtyear.Text!="")
        {
            Data = Data.Where(s => s.Date.Value.Year == Convert.ToInt32(txtyear.Text)).OrderBy(d => d.Date);
        }
        if (Data != null && Data.Count() > 0)
        {
            grd_Holiday.DataSource = Data;
            grd_Holiday.DataBind();
           
        }
        else
        {
            grd_Holiday.DataSource = null;
            grd_Holiday.DataBind();
        }
       
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dateTime = Genreal.GetDate(dtpholydate.Text);
            //g.ShowMessage(this.Page, dtpholydate.Text);
            Console.WriteLine(dtpholydate.Text);
            // Save And Update........
            if (btnsubmit.Text == "Save")
            {
                try
                {
                    var dt = from p in EX.HoliDaysMasters.Where(d => d.Date.Value.Date == dateTime.Date&&d.CompanyId==Convert.ToInt32(ddlCompany.SelectedValue)&&d.TenantId==Convert.ToString(Session["TenantId"])) select p;
                    if (dt.Count() > 0)
                    {
                        g.ShowMessage(this.Page, "Holiday Details Already Exist");
                    }
                    else
                    {
                        HoliDaysMaster ct = new HoliDaysMaster();
                        ct.HoliDaysName = txtholidayname.Text;
                        ct.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        ct.Status = 1;

                        ct.Date = dateTime;
                        ct.TenantId = Convert.ToString(Session["TenantId"]);
                        EX.HoliDaysMasters.InsertOnSubmit(ct);
                        EX.SubmitChanges();
                        g.ShowMessage(this.Page, "Submitted Successfully");
                        Clear();
                        BindGrid();

                    }

                }
                catch (Exception ex)
                {
                    Clear();
                    g.ShowMessage(this.Page, "Invalid Year");

                }

            }
            else
            {
                var dt = from p in EX.HoliDaysMasters.Where(d => d.Date.Value.Date == dateTime.Date && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"]) && d.HolidaysID != Convert.ToInt32(lblid.Text)) select p;
                if (dt.Count() > 0)
                {
                    g.ShowMessage(this.Page, "Holiday Details Already Exist");

                }
                else
                {
                    HoliDaysMaster ct = EX.HoliDaysMasters.Where(d => d.HolidaysID == Convert.ToInt32(lblid.Text)).First();
                    ct.HoliDaysName = txtholidayname.Text;
                    ct.Status = 1;
                    ct.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    ct.TenantId = Convert.ToString(Session["TenantId"]);
                    ct.Date = dateTime;
                    EX.SubmitChanges();
                    g.ShowMessage(this.Page, "Updated Successfully");
                    btnsubmit.Text = "Save";
                    Clear();
                    BindGrid();
                }
            }
        }
        catch (Exception ex) { g.ShowMessage(this.Page, ex.Message); }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        // Edit.........
        MultiView1.ActiveViewIndex = 1;
        ImageButton Lnk = (ImageButton)sender;
        string HolyId = Lnk.CommandArgument;
        lblid.Text = HolyId;

        HoliDaysMaster mt = EX.HoliDaysMasters.Where(d => d.HolidaysID == Convert.ToInt32(HolyId)).First();
        txtholidayname.Text = mt.HoliDaysName;
        ddlCompany.SelectedValue = string.IsNullOrEmpty(mt.CompanyId.ToString()) ? "0" : mt.CompanyId.Value.ToString();
        dtpholydate.Text = Convert.ToString(mt.Date).Replace("12:00:00 AM", "");
       
        btnsubmit.Text = "Update";

    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        // Delete.....
        try
        {
            ImageButton Lnk = (ImageButton)sender;
            int id = Convert.ToInt32(Lnk.CommandArgument);
            var dt = from p in EX.HoliDaysMasters
                     where p.HolidaysID == id 
                     select p;
            EX.HoliDaysMasters.DeleteAllOnSubmit(dt);
            EX.SubmitChanges();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "sticky('success','City Details Deleted Successfully');", true);
            BindGrid();
        }
        catch (Exception)
        {
            g.ShowMessage(this.Page, "Details Is In Use...");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "sticky('notice','This City Details Is Already In Use.');", true);
        }
    }
    public void Clear()
    {
        BindGrid();
        txtholidayname.Text = null;
        dtpholydate.Text = "";
       
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
       // dtpholydate.Text = g.GetCurrentDateTime().ToShortDateString();
    }
    protected void ddlsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsort.SelectedIndex == 0)
        {
            txtyear.Visible = false;
            txtyear.Text = "";
            //BindGrid();
        }
        else if (ddlsort.SelectedIndex == 1)
        {
            txtyear.Visible = true;
            //BindGrid();
            
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void grd_Holiday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Holiday.Rows.Count > 0)
        {
            grd_Holiday.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Holiday.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        g.ShowMessage(this.Page, dtpholydate.Text);
    }
}