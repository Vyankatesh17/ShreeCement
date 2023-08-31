using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mst_weekoff_setting : System.Web.UI.Page
{
    #region Variable Declartion
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    DataTable DTInfo = new DataTable();
    #endregion
   
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            if (!IsPostBack)
            {
                fillcompany();

                FillAllData();

                MultiView1.ActiveViewIndex = 0;

                txtFromDate.Attributes.Add("Readonly", "ReadOnly");
                txtFromDate.Text = g.GetCurrentDateTime().ToShortDateString();

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void FillAllData()
    {
        string query = @"select WeeklyOffid, CompanyName,TrackHolidays,Days ,CONVERT(varchar, Effectdate,101) Date from  WeeklyOffTB WO left outer join CompanyInfoTB MC on MC.CompanyId=WO.CompanyID where MC.status=1";

        if (Session["UserType"].ToString() != "SuperAdmin")
        {
            query += " AND MC.TenantId='" + Session["TenantId"].ToString() + "'";
        }

        query += " order by WeeklyOffid desc";
        DataSet dsweekoffdata = g.ReturnData1(query);
        if (dsweekoffdata.Tables[0].Rows.Count > 0)
        {
            grdweekoffdata.DataSource = dsweekoffdata.Tables[0];
            grdweekoffdata.DataBind();            
        }
        else
        {
            grdweekoffdata.DataSource = null;
            grdweekoffdata.DataBind();           
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)ViewState["DTINFO"];
        if (ViewState["DTINFO"] != null)
        {
            if (btnSubmit.Text == "Submit")
            {

            }
            else
            {

            }



            if (btnSubmit.Text == "Submit")
            {
                DTInfo = ViewState["DTINFO"] as DataTable;

                if (ddlCompany.SelectedIndex != 0)
                {
                    //DataSet dscomp = g.ReturnData1("select CompanyId from CompanyInfoTB where status=0 ");
                    //for (int i = 0; i < dscomp.Tables[0].Rows.Count; i++)
                    //{
                    DataSet dscheck = g.ReturnData1("select * from Weeklyofftb where Companyid='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'");

                    if (dscheck.Tables[0].Rows.Count <= 0)
                    {
                        for (int j = 0; j < DTInfo.Rows.Count; j++)
                        {
                            WeeklyOffTB BT = new WeeklyOffTB();
                            BT.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                            BT.TrackHolidays = DTInfo.Rows[j][2].ToString();
                            BT.Days = DTInfo.Rows[j][3].ToString();
                            BT.Effectdate = Convert.ToDateTime(DTInfo.Rows[j][4].ToString());
                            BT.TenantId = Session["TenantId"].ToString();
                            hr.WeeklyOffTBs.InsertOnSubmit(BT);
                            hr.SubmitChanges();
                        }

                    }
                    else
                    {
                        g.ShowMessage(this.Page, "Weekly off details already present for selected company!!!!");

                    }

                    g.ShowMessage(this.Page, "Weekly Off Details Saved Successfully!!!!");
                    Clear();
                    FillAllData();
                }
            }
            else
            {

                if (ViewState["DTINFO"] != null)
                {
                    string itemdelet = "delete from Weeklyofftb where Companyid='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'";
                    DataSet ds = g.ReturnData1(itemdelet);
                    DTInfo = ViewState["DTINFO"] as DataTable;
                    for (int i = 0; i < DTInfo.Rows.Count; i++)
                    {
                        WeeklyOffTB WT = new WeeklyOffTB();
                        WT.CompanyID = Convert.ToInt32(DTInfo.Rows[i]["CompanyId"].ToString());
                        WT.TrackHolidays = DTInfo.Rows[i]["TrackHolidays"].ToString();
                        WT.Days = DTInfo.Rows[i]["Days"].ToString();
                        WT.Effectdate = Convert.ToDateTime(DTInfo.Rows[i]["Date"].ToString());
                        WT.TenantId = Session["TenantId"].ToString();
                        hr.WeeklyOffTBs.InsertOnSubmit(WT);
                        hr.SubmitChanges();

                    }
                    ViewState["DTINFO"] = null;
                    grdweekoffdata.DataSource = null;
                    grdweekoffdata.DataBind();
                    g.ShowMessage(this.Page, "Weekly Off Details Updated Successfully!!!!");
                    Clear();
                    FillAllData();
                }
                else
                {
                    g.ShowMessage(this.Page, "Please Add Weekly Off Details");
                }


            }
            //}



        }
        else
        {
            g.ShowMessage(this.Page, "Please Add Weekly Off Details");
        }
    }

    private void Clear()
    {
        ddlCompany.SelectedIndex = 0;
        txtFromDate.Text = g.GetCurrentDateTime().ToShortDateString();
        ddsatoff.SelectedIndex = 0;
        dddays.SelectedIndex = 0;
        FillDaysData();
        ViewState["DTINFO"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        btnSubmit.Text = "Submit";
        MultiView1.ActiveViewIndex = 0;
    }
    protected void Oncheckentry_Changed(object sender, EventArgs e)
    {

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            FillDaysData();
            checkcompany();
        }

    }

    private void FillDaysData()
    {
        //DataSet dsdays = g.ReturnData1(" SELECT 'Monday' DayN  union all SELECT  ' Tuesday' DayN union all SELECT 'Wednesday'DayN  union all SELECT  'Thursday' DayN union all SELECT  'Friday' DayN union all SELECT   'Saturday' DayN union all SELECT    'Sunday' DayN");
        //grdoff.DataSource = dsdays.Tables[0];
        //grdoff.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        btnSubmit.Text = "Submit";
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ddlCompany.Enabled = true;
        ddlCompany.SelectedIndex = 0;
        txtFromDate.Text = g.GetCurrentDateTime().ToShortDateString();
        ddsatoff.SelectedIndex = 0;
        dddays.SelectedIndex = 0;
        FillDaysData();
        ViewState["DTINFO"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void ImgCitAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlCompany.Text.Trim()))
        {
            int cnt = 0;


            if (ViewState["DTINFO"] != null)
            {
                DTInfo = (DataTable)ViewState["DTINFO"];
            }
            else
            {
                DataColumn Companyid = DTInfo.Columns.Add("CompanyId");
                DataColumn CompanyName = DTInfo.Columns.Add("CompanyName");
                DataColumn Holidays = DTInfo.Columns.Add("TrackHolidays");
                DataColumn Days = DTInfo.Columns.Add("Days");
                DataColumn Date = DTInfo.Columns.Add("Date");

            }

            DataRow dr = DTInfo.NewRow();

            dr[0] = ddlCompany.SelectedValue;
            dr[1] = ddlCompany.SelectedItem.Text;
            dr[2] = ddsatoff.Text;
            dr[3] = dddays.Text;
            dr[4] = txtFromDate.Text;


            if (DTInfo.Rows.Count > 0)
            {
                for (int f = 0; f < DTInfo.Rows.Count; f++)
                {

                    string u2 = DTInfo.Rows[f][0].ToString();
                    string off = DTInfo.Rows[f][2].ToString();
                    string days = DTInfo.Rows[f][3].ToString();

                    if (u2 == Convert.ToString(ddlCompany.SelectedValue) && days == dddays.Text)
                    //  if (u2 == Convert.ToString (ddlCompany.SelectedValue) && off == ddsatoff.Text && days==dddays.Text)
                    {
                        cnt++;

                    }
                    else
                    {

                    }



                }
                if (cnt > 0)
                {
                    // g.ShowMessage(this.Page, "Already Exist");
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','');", true);
                    g.ShowMessage(this.Page, "Already Exist on Same Day You Can Edit Weekly Off Details");
                }
                else
                {
                    DTInfo.Rows.Add(dr);
                    ClearDropdowns();
                }
            }
            else
            {
                DTInfo.Rows.Add(dr);
                ClearDropdowns();
            }

            ViewState["DTINFO"] = DTInfo;

            GridView1.DataSource = DTInfo;
            GridView1.DataBind();
            GridView1.Visible = true;
        }
        else
        {
            g.ShowMessage(this.Page, "Please Enter Mandotory Fields");
            ddlCompany.Focus();
        }

    }

    private void checkcompany()
    {
        try
        {
            DataTable dt = g.ReturnData("select wt.CompanyID, MC.CompanyName,wt.TrackHolidays,wt.Days, Convert(varchar,wt.Effectdate,101) AS Date from WeeklyOffTB WT left outer join CompanyInfoTB MC  on MC.CompanyId=WT.CompanyID  where wt.CompanyID='" + Convert.ToInt32(ddlCompany.SelectedValue) + "' order by Date desc");
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                ViewState["DTINFO"] = dt;
                 btnSubmit.Text = "Update";
                ddlCompany.Enabled = false;
          }
            else
	        {
                 GridView1.DataSource = null;
                GridView1.DataBind();
                ViewState["DTINFO"] = null;
	        }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void ClearDropdowns()
    {
        //  ddlCompany.Enabled = true;
        // ddlCompany.SelectedIndex = 0;
        ddsatoff.SelectedIndex = 0;
        dddays.SelectedIndex = 0;
        txtFromDate.Attributes.Add("Readonly", "ReadOnly");
        txtFromDate.Text = g.GetCurrentDateTime().ToShortDateString();

        ViewState["DTINFO"] = null;

    }
    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkk = (ImageButton)sender;
        string Id = lnkk.CommandArgument;

        DTInfo = (DataTable)ViewState["DTINFO"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;

        //foreach (DataRow d in DTInfo.Rows)
        //{
        //    if (d[0].ToString() == lnkk.CommandArgument)
        //    {

        ddlCompany.SelectedValue = DTInfo.Rows[row]["CompanyId"].ToString();
        ddlCompany.SelectedItem.Text = DTInfo.Rows[row]["CompanyName"].ToString();
        ddsatoff.SelectedItem.Text = DTInfo.Rows[row]["TrackHolidays"].ToString();
        dddays.SelectedItem.Text = DTInfo.Rows[row]["Days"].ToString();
        txtFromDate.Text = DTInfo.Rows[row]["Date"].ToString();
        DTInfo.Rows[row].Delete();
        DTInfo.AcceptChanges();

        // break;
        //    }
        //}

        GridView1.DataSource = DTInfo;
        GridView1.DataBind();
       
        if (DTInfo.Rows.Count > 0)
        {
            ViewState["DTINFO"] = DTInfo;
        }
        else
        {
            ViewState["DTINFO"] = null;
        }
    }
    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkk2 = (ImageButton)sender;
        string Id1 = lnkk2.CommandArgument;
        DTInfo = (DataTable)ViewState["DTINFO"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;

        //foreach (DataRow d in DTInfo.Rows)
        //{
        //    if (d[0].ToString() == lnkk2.CommandArgument)
        //    {
        DTInfo.Rows[row].Delete();
        DTInfo.AcceptChanges();

        //        break;
        //    }
        //}

        GridView1.DataSource = DTInfo;
        GridView1.DataBind();
       
        if (DTInfo.Rows.Count>0)
        {
              ViewState["DTINFO"] = DTInfo;
        }
        else
        {
            ViewState["DTINFO"] = null;
        }
        
    }

    protected void grdweekoffdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdweekoffdata.PageIndex = e.NewPageIndex;
        FillAllData();
    }

    protected void grdweekoffdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdweekoffdata.Rows.Count > 0)
        {
            grdweekoffdata.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grdweekoffdata.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }
}