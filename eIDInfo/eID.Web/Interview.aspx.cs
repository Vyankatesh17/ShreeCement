using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Interview : System.Web.UI.Page
{
    //Interview Details
    //Created By Manasi
    //13 Oct 2014

    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
               
                lblDate.Text = g.GetCurrentDateTime().ToShortDateString();
                MultiView1.ActiveViewIndex = 0;
                fillcompany();
                BindAllData();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    private void fillcompany()
    {
        var data = from dt in HR.CompanyInfoTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlCompany.Items.Clear();
            ddlCompany.DataSource = data;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
    }
    private void fillDept(int companyid)
    {
        var data = from dt in HR.MasterDeptTBs
                   where dt.Status == 0 && dt.CompanyId == companyid
                   select dt;
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
            ddldept.Items.Clear();
            ddldept.DataSource = data;
            ddldept.DataBind();
            ddldept.Items.Insert(0, "--Select--");
        }
    }
    public void BindAllData()
    {
        var deptData = from d in HR.InterviewTBs
                       join dtt in HR.MasterDeptTBs on d.DeptID equals dtt.DeptID
                       join dtDes in HR.MasterDesgTBs on d.positionDesignationID equals dtDes.DesigID
                       join c in HR.CompanyInfoTBs on d.CompanyID equals c.CompanyId
                       select new { d.CandidateName,d.Mobile, d.InterviewDate, d.InterviewerID, dtDes.DesigName,
                           dtt.DeptName, c.CompanyName,d.Status };
        if (deptData.Count() > 0)
        {
            grd_Interview.DataSource = deptData;
            grd_Interview.DataBind();
            lblcnt.Text = deptData.Count().ToString();
        }
        else
        {
            grd_Interview.DataSource = deptData;
            grd_Interview.DataBind();
            lblcnt.Text = "0";
        }
    }
    protected void OnClick_Edit(object sender, EventArgs e)
    {

        LinkButton Lnk = (LinkButton)sender;
        string interviewid = Lnk.CommandArgument;
        MultiView1.ActiveViewIndex = 1;
        InterviewTB MT = HR.InterviewTBs.Where(d => d.InterviewerID == Convert.ToInt32(interviewid)).First();
        lblid.Text = interviewid;

        lblDate.Text = MT.InterviewDate.ToString().Replace(" 12:00:00 AM","");
        fillcompany();
        ddlCompany.SelectedValue = MT.CompanyID.ToString();
        fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
        ddldept.SelectedValue = MT.DeptID.ToString();
        fillDesig(Convert.ToInt32(ddldept.SelectedValue));
        ddldesign.SelectedValue = MT.positionDesignationID.ToString();
        txtName.Text = MT.CandidateName;
        txtMob.Text = MT.Mobile;
        txtRefBy.Text = MT.RefeBy;
        txtEmail.Text = MT.Email;
        txtAddr.Text = MT.Address;
        btnsubmit.Text = "Update";

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var dte = from p in HR.InterviewTBs.Where(d => d.CandidateName == txtName.Text 
                && d.CompanyID == Convert.ToInt32(ddlCompany.SelectedValue) && d.Mobile== txtMob.Text &&
                d.DeptID == Convert.ToInt32(ddldept.SelectedValue) 
                && d.InterviewDate == Convert.ToDateTime(lblDate.Text)) select p;
            if (dte.Count() > 0)
            {
                modpop.Message = "Interview Details Already Exist";
                modpop.ShowPopUp();

            }
            else
            {
                InterviewTB MTB = new InterviewTB();
                MTB.Address = txtAddr.Text;
                MTB.CandidateName = txtName.Text;
                MTB.Email = txtEmail.Text;
                MTB.InterviewDate = Convert.ToDateTime(lblDate.Text);
                MTB.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                MTB.DeptID = Convert.ToInt32(ddldept.SelectedValue);
                MTB.positionDesignationID = Convert.ToInt32(ddldesign.SelectedValue);
                MTB.RefeBy = txtRefBy.Text;
                MTB.Status = "Fresh";
                MTB.Mobile = txtMob.Text;
                HR.InterviewTBs.InsertOnSubmit(MTB);
                HR.SubmitChanges();
                modpop.Message = "Submitted Successfully";
                modpop.ShowPopUp();
                Clear();
            }
        }
        else
        {
            var dte = from p in HR.InterviewTBs.Where(d => d.CandidateName == txtName.Text
                && d.InterviewerID != Convert.ToInt32(lblid.Text) && d.CompanyID == Convert.ToInt32(ddlCompany.SelectedValue) && d.Mobile== txtMob.Text &&
                            d.DeptID == Convert.ToInt32(ddldept.SelectedValue) 
                            && d.InterviewDate == Convert.ToDateTime(lblDate.Text)) select p;
             if (dte.Count() > 0)
             {
                 modpop.Message = "Interview Details Already Exist";
                 modpop.ShowPopUp();

             }
             else
             {
                 #region
     InterviewTB MTB = HR.InterviewTBs.Where(d => d.InterviewerID == Convert.ToInt32(lblid.Text)).First();
     MTB.Address = txtAddr.Text;
     MTB.CandidateName = txtName.Text;
     MTB.Email = txtEmail.Text;
     MTB.InterviewDate = Convert.ToDateTime(lblDate.Text);
     MTB.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
     MTB.DeptID = Convert.ToInt32(ddldept.SelectedValue);
     MTB.positionDesignationID = Convert.ToInt32(ddldesign.SelectedValue);
     MTB.RefeBy = txtRefBy.Text;
     MTB.Mobile = txtMob.Text;
     MTB.Status = "Fresh";
     HR.SubmitChanges();
     modpop.Message = "Updated Successfully";
     modpop.ShowPopUp();

     btnsubmit.Text = "Save";
     Clear();
     #endregion
             }
   }
    }
    public void Clear()
    {
        lblDate.Text = g.GetCurrentDateTime().ToShortDateString();
        txtAddr.Text = null;
        txtName.Text = null;
        txtEmail.Text = null;
        
        ddlCompany.Items.Clear();
        ddldept.Items.Clear();
        ddldesign.Items.Clear();
        txtRefBy.Text = null;
        txtMob.Text = null;
        BindAllData();
        fillcompany();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Clear();

        MultiView1.ActiveViewIndex = 1;

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
        }
        else
        {
            ddldept.Items.Clear();
            ddldept.Items.Insert(0, "--Select--");
        }
    }
    private void fillDesig(int p)
    {
        var data = from dt in HR.MasterDesgTBs
                   where dt.Status == 0 &&
                   dt.CompanyId == int.Parse(ddlCompany.SelectedValue)
                   && dt.DeptID == int.Parse(ddldept.SelectedValue)
                   select new { dt.DesigID, dt.DesigName };
        if (data != null && data.Count() > 0)
        {

            ddldesign.DataSource = data;
            ddldesign.DataTextField = "DesigName";
            ddldesign.DataValueField = "DesigID";
            ddldesign.DataBind();
            ddldesign.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldesign.Items.Clear();
            ddldesign.DataSource = data;
            ddldesign.DataBind();
            ddldesign.Items.Insert(0, "--Select--");
        }
    }
    protected void grd_Interview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Interview.PageIndex = e.NewPageIndex;
        BindAllData();
        grd_Interview.DataBind();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            fillDesig(Convert.ToInt32(ddldept.SelectedValue));
        }
        else if (ddlCompany.SelectedIndex > 0 && ddldept.SelectedIndex>0)
        {
            ddldesign.Items.Clear();
            ddldesign.Items.Insert(0, "--Select--");
        }

    }
  
 
   
}