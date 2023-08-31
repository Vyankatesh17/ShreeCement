using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRReport : System.Web.UI.Page
{
    //InterviewReport 
    //Manasi
    //12:00 PM

    HrPortalDtaClassDataContext HR=new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {

                DateTime dtf = g.GetStartOfMonth(DateTime.Now.Date);
                DateTime dtT = g.EndOfMonth(DateTime.Now.Date);
                txtFdate.Text =dtf.ToShortDateString();
                txtTdate.Text = dtT.ToShortDateString();
                fillcompany();
                fillpositions();
                BindData();
                
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    private void fillpositions()
    {
        if (ddlComp.SelectedIndex==0)
        {
            ddlDesig.Items.Clear();
            ddlDesig.DataSource = null;
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, "All");
        }
        else
        {
            #region
            var data = from dt in HR.MasterDesgTBs
                       where dt.Status == 0 &&
                       dt.CompanyId == int.Parse(ddlComp.SelectedValue)
                       select new { dt.DesigID, dt.DesigName };
            if (data != null && data.Count() > 0)
            {

                ddlDesig.DataSource = data;
                ddlDesig.DataTextField = "DesigName";
                ddlDesig.DataValueField = "DesigID";
                ddlDesig.DataBind();
                ddlDesig.Items.Insert(0, "All");
            }
            else
            {
                ddlDesig.Items.Clear();
                ddlDesig.DataSource = data;
                ddlDesig.DataBind();
                ddlDesig.Items.Insert(0, "All");
            }


        #endregion
            
        }
    }

    private void fillcompany()
    {
        var data = from dt in HR.CompanyInfoTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlComp.DataSource = data;
            ddlComp.DataTextField = "CompanyName";
            ddlComp.DataValueField = "CompanyID";
            ddlComp.DataBind();
            ddlComp.Items.Insert(0, "All");
        }
        else
        {
            ddlComp.Items.Clear();
            ddlComp.DataSource = data;
            ddlComp.DataBind();
            ddlComp.Items.Insert(0, "All");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        var deptData = from d in HR.InterviewTBs
                       join dtt in HR.MasterDeptTBs on d.DeptID equals dtt.DeptID
                       join dtDes in HR.MasterDesgTBs on d.positionDesignationID equals dtDes.DesigID
                       join c in HR.CompanyInfoTBs on d.CompanyID equals c.CompanyId
                       where d.InterviewDate >= Convert.ToDateTime(txtFdate.Text) && d.InterviewDate <= Convert.ToDateTime(txtTdate.Text)
                       select new
                       {
                           d.CandidateName,
                           d.Mobile,
                           d.positionDesignationID,
                           d.InterviewDate,
                           d.InterviewerID,
                           d.CompanyID,
                           dtDes.DesigName,
                           dtt.DeptName,
                           c.CompanyName,d.Status
                       };
        if (deptData.Count() > 0)
        {
            if (ddlDesig.SelectedIndex == 0 && ddlComp.SelectedIndex!=0)
            {//ALL Positions
                deptData = deptData.Where(d => d.CompanyID == int.Parse(ddlComp.SelectedValue));
            }
            else if (ddlComp.SelectedIndex == 0 && ddlDesig.SelectedIndex > 0)
            {//ALL Companys && single Positions
                deptData = deptData.Where(d => d.positionDesignationID == int.Parse(ddlDesig.SelectedValue));
            }
            else if (ddlComp.SelectedIndex > 0 && ddlDesig.SelectedIndex > 0)
            {//Single Company && single Position
                deptData = deptData.Where(d => d.positionDesignationID == int.Parse(ddlDesig.SelectedValue) && d.CompanyID == int.Parse(ddlComp.SelectedValue));
            }
            grd_Interview.DataSource = deptData;
            grd_Interview.DataBind();
           // lblcnt.Text = deptData.Count().ToString();
        }
        else
        {
            grd_Interview.DataSource = null;
            grd_Interview.DataBind();
           // lblcnt.Text = "0";
        }
    }
    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpositions();
        
    }
    protected void grd_Interview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Interview.PageIndex = e.NewPageIndex;
        BindData();
    }
}