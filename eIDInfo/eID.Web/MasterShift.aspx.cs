using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterShift : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                fillCompany();               

                BindAllEmployeeShiftData();
                txtintimehours.Text = "00:00 AM";
                txtouttime.Text = "00:00 PM";
                txtlatemarks.Text = "00:00 AM";
                txtovertime.Text = "09:00";       
                lbldefualttime.Text = "00.00 AM";
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (grd_Empshift.Rows.Count > 0)
            grd_Empshift.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    private void BindAllEmployeeShiftData()
    {
        string query = "";
        int companyid = Convert.ToInt32(Session["CompanyID"]);
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
             query = string.Format(@"SELECT        ST.ShiftID, CT.CompanyName, ST.Shift, ST.Intime, ST.Outtime, ST.Latemark, ST.overtime, ST.starttime,
CASE WHEN IsDefault=1 THEN 'Yes' ELSE 'No' END AS IsDefault
FROM            MasterShiftTB AS ST INNER JOIN
                         CompanyInfoTB AS CT ON CT.CompanyId = ST.CompanyId
WHERE        (CT.TenantId = '{0}' And CT.CompanyId = '{1}')
ORDER BY ST.ShiftID DESC", Convert.ToString(Session["TenantId"]), companyid);
        }
        else
        {
             query = string.Format(@"SELECT        ST.ShiftID, CT.CompanyName, ST.Shift, ST.Intime, ST.Outtime, ST.Latemark, ST.overtime, ST.starttime,
CASE WHEN IsDefault=1 THEN 'Yes' ELSE 'No' END AS IsDefault
FROM            MasterShiftTB AS ST INNER JOIN
                         CompanyInfoTB AS CT ON CT.CompanyId = ST.CompanyId
WHERE        (CT.TenantId = '{0}')
ORDER BY ST.ShiftID DESC", Convert.ToString(Session["TenantId"]));
        }

       

        DataTable dataTable = g.ReturnData(query);

        grd_Empshift.DataSource = dataTable;
        grd_Empshift.DataBind();
    }
    
    private void fillCompany()
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


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
        // TimeSpan tin =  inhours 
            int max = Convert.ToInt32(txtEndTime.Text);
        if (max > 960)
        {
            g.ShowMessage(this.Page, "Max 16 hours will be allowed for shift end");
            txtEndTime.Text = "";
            txtEndTime.Focus();
        }
        else
        {
            if (btnsubmit.Text == "Save")
            {
                var dataexist = from d in HR.MasterShiftTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.Shift == ddlshift.Text) select d;

                if (dataexist.Count() == 0)
                {
                    MasterShiftTB ES = new MasterShiftTB();
                    ES.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    ES.Shift = ddlshift.Text;
                    ES.Intime = txtintimehours.Text;
                    ES.Outtime = txtouttime.Text;
                    ES.Latemark = txtlatemarks.Text;
                    ES.overtime = txtovertime.Text;
                    ES.TenantId = Convert.ToString(Session["TenantId"]);
                    //  ES.starttime = txtstarttime.Text;
                    

                    ES.LateMarkStart = TimeSpan.Parse(txtlatemarks.Text);
                    ES.LateMarkEnd = TimeSpan.Parse(txtLateMarkEnd.Text);

                    ES.EarlyMarkStart = TimeSpan.Parse(txtearlymarkstart.Text);
                    ES.LateMarkEnd = TimeSpan.Parse(txtearlyend.Text);

                    ES.PunchStart = Convert.ToInt32(txtBeginTime.Text);
                    ES.PunchEnd = Convert.ToInt32(txtEndTime.Text);
                    //ES.MinimumTime = Convert.ToInt32(txtlessdifftime.Text);
                    ES.Shifthours = TimeSpan.Parse(txtshifthours.Text);
                    ES.OTStartAfter = string.IsNullOrEmpty(txtOTHours.Text) ? TimeSpan.Parse(txtouttime.Text) : TimeSpan.Parse(txtOTHours.Text);
                    var sdata = (from d in HR.MasterShiftTBs where d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"]) select d).Distinct();
                    if (sdata.Count() == 0)
                    {                        
                        ES.IsDefault = true;
                    }
                    else
                    {
                        ES.IsDefault = false;
                    }

                    HR.MasterShiftTBs.InsertOnSubmit(ES);
                    HR.SubmitChanges();


                    g.ShowMessage(this.Page, "Employee Shift Added Successfully!!");
                    Clear();
                    MultiView1.ActiveViewIndex = 0;
                }
                else
                {
                    g.ShowMessage(this.Page, "Employee Shift Already Exists!!!");
                }

            }
            else
            {
                var dataexist = from d in HR.MasterShiftTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.Shift == ddlshift.Text && d.ShiftID == Convert.ToInt32(lblheadid.Text)) select d;

                if (dataexist.Count() > 0)
                {
                    MasterShiftTB ES = HR.MasterShiftTBs.Where(d => d.ShiftID == Convert.ToInt32(lblheadid.Text)).First();
                    ES.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    ES.Shift = ddlshift.Text;
                    ES.Intime = txtintimehours.Text;
                    ES.Outtime = txtouttime.Text;
                    ES.Latemark = txtlatemarks.Text;
                    ES.overtime = txtovertime.Text;

                    ES.TenantId = Convert.ToString(Session["TenantId"]);

                    ES.LateMarkStart = TimeSpan.Parse(txtlatemarks.Text);
                    ES.LateMarkEnd = TimeSpan.Parse(txtLateMarkEnd.Text);

                    ES.EarlyMarkStart = TimeSpan.Parse(txtearlymarkstart.Text);
                    ES.EarlyMarkEnd = TimeSpan.Parse(txtearlyend.Text);

                    ES.PunchStart = Convert.ToInt32(txtBeginTime.Text);
                    ES.PunchEnd = Convert.ToInt32(txtEndTime.Text);
                    //ES.MinimumTime = Convert.ToInt32(txtlessdifftime.Text);
                    ES.Shifthours = TimeSpan.Parse(txtshifthours.Text);
                    ES.OTStartAfter = string.IsNullOrEmpty(txtOTHours.Text) ? TimeSpan.Parse(txtouttime.Text) : TimeSpan.Parse(txtOTHours.Text);

                    HR.SubmitChanges();
                    g.ShowMessage(this.Page, "Employee Shift updated Successfully!!");
                    Clear();
                    MultiView1.ActiveViewIndex = 0;
                }
                else
                {
                    var dataexist1 = from d in HR.MasterShiftTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.Shift == ddlshift.Text && d.ShiftID != Convert.ToInt32(lblheadid.Text)) select d;

                    if (dataexist1.Count() > 0)
                    {
                        g.ShowMessage(this.Page, "Employee Shift Already Exists!!!");
                    }
                    else
                    {
                        MasterShiftTB ES = HR.MasterShiftTBs.Where(d => d.ShiftID == Convert.ToInt32(lblheadid.Text)).First();
                        ES.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        ES.Shift = ddlshift.Text;
                        ES.Intime = txtintimehours.Text;
                        ES.Outtime = txtouttime.Text;
                        ES.Latemark = txtlatemarks.Text;
                        ES.overtime = txtovertime.Text;

                        ES.TenantId = Convert.ToString(Session["TenantId"]);
                        ES.LateMarkStart = TimeSpan.Parse(txtlatemarks.Text);
                        ES.LateMarkEnd = TimeSpan.Parse(txtLateMarkEnd.Text);

                        ES.EarlyMarkStart = TimeSpan.Parse(txtearlymarkstart.Text);
                        ES.EarlyMarkEnd = TimeSpan.Parse(txtearlyend.Text);

                        ES.PunchStart = Convert.ToInt32(txtBeginTime.Text);
                        ES.PunchEnd = Convert.ToInt32(txtEndTime.Text);
                        //ES.MinimumTime = Convert.ToInt32(txtlessdifftime.Text);
                        ES.Shifthours = TimeSpan.Parse(txtshifthours.Text);
                        ES.OTStartAfter = string.IsNullOrEmpty(txtOTHours.Text) ? TimeSpan.Parse(txtouttime.Text) : TimeSpan.Parse(txtOTHours.Text);

                        HR.SubmitChanges();
                        g.ShowMessage(this.Page, "Employee Shift updated Successfully!!");
                        Clear();
                        MultiView1.ActiveViewIndex = 0;
                    }
                }
            }
        }
    }
    private void Clear()
    {
         ddlCompany.SelectedIndex = 0;
         ddlshift.Text="";
        txtintimehours.Text = "00:00 AM";
        txtouttime.Text = "00:00 PM";
        txtlatemarks.Text = "00:00 AM";
      //  txtstarttime.Text = "00:00 PM";
        txtovertime.Text = "09:00";
        txtamount.Text = "";
        txtdifftime.Text = "";
        txtintimehours.Text = "";
        txtshifthours.Text = "";

        txtearlymarkstart.Text = "00:00 AM";
        txtearlyend.Text = "00:00 AM";
       
        BindAllEmployeeShiftData();
        btnsubmit.Text = "Save";
        MultiView1.ActiveViewIndex = 1;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ddlCompany.SelectedIndex = 0;
        ddlshift.Text="";
        //ddlempname.SelectedIndex = 0;
        txtintimehours.Text = "00:00 AM";
        txtouttime.Text = "00:00 PM";
        txtlatemarks.Text = "00:00 AM";
        txtLateMarkEnd.Text = "00:00 AM";
        //  txtstarttime.Text = "00:00 PM";
        txtamount.Text = "";
        txtdifftime.Text = "";
        txtintimehours.Text = "";
        txtshifthours.Text = "";
        txtBeginTime.Text = "";
        txtEndTime.Text = "";
        txtovertime.Text = "00:00 AM";
        txtOTHours.Text = "00:00 AM";
        BindAllEmployeeShiftData();
        btnsubmit.Text = "Save";
    }
    protected void lnk_Click(object sender, EventArgs e)
    {
       

    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = sender as ImageButton;
        lblheadid.Text = lnk.CommandArgument.ToString();
        MasterShiftTB MTB = HR.MasterShiftTBs.Where(d => d.ShiftID == Convert.ToInt32(lnk.CommandArgument.ToString())).First();
        fillCompany();
        ddlCompany.SelectedValue = MTB.CompanyId.ToString();
            
        ddlshift.Text = MTB.Shift;
        txtintimehours.Text = MTB.Intime.ToString();
        txtouttime.Text = MTB.Outtime.ToString();
        txtlatemarks.Text = MTB.Latemark.ToString();
        txtovertime.Text = MTB.overtime.ToString();
      
        // n changes 23 March 2015
        txtshifthours.Text = MTB.Shifthours.ToString();
        txtOTHours.Text = MTB.OTStartAfter.HasValue ? MTB.OTStartAfter.ToString() : "00:00 AM";
       txtdifftime.Text = MTB.DifferenceTime.ToString();
       txtlessdifftime.Text = MTB.MinimumTime.ToString();
       txtamount.Text = MTB.OTAmount.ToString();

        if(MTB.EarlyMarkStart != null)
        {
            txtearlymarkstart.Text = MTB.EarlyMarkStart.Value.ToString();
        }

        if (MTB.EarlyMarkEnd != null)
        {
            txtearlyend.Text = MTB.EarlyMarkEnd.Value.ToString();
        }
        

        txtlatemarks.Text = MTB.LateMarkStart.Value.ToString();
        txtLateMarkEnd.Text = MTB.LateMarkEnd.Value.ToString();

        txtBeginTime.Text = MTB.PunchStart.Value.ToString();
        txtEndTime.Text = MTB.PunchEnd.Value.ToString();
       
        MultiView1.ActiveViewIndex = 1;
        btnsubmit.Text = "Update";
    }


    protected void txtEndTime_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtEndTime.Text))
        {
            int max = Convert.ToInt32(txtEndTime.Text);
            if (max > 960)
            {
                g.ShowMessage(this.Page, "Max 16 hours will be allowed for shift end");
                txtEndTime.Text = "";
                txtEndTime.Focus();
            }
        }
    }

    protected void grd_Empshift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Empshift.Rows.Count > 0)
        {
            grd_Empshift.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Empshift.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        var data = HR.MasterShiftTBs.Where(d=> d.IsDefault==true&& d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
        if (data != null)
        {
            txtDefaultMsg.Text = "Your default shift is : " + data.Shift;
        }
        else
        {
            txtDefaultMsg.Text = "";
        }
    }

    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = sender as ImageButton;
        lblheadid.Text = lnk.CommandArgument.ToString();
        MasterShiftTB MTB = HR.MasterShiftTBs.Where(d => d.ShiftID == Convert.ToInt32(lnk.CommandArgument.ToString())).First();

       
        HR.MasterShiftTBs.DeleteOnSubmit(MTB);
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "Shift Delete Successfully.....");
        Response.Redirect("MasterShift.aspx");




    }
}