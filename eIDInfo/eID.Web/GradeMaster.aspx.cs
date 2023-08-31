using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GradeMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string path = "";
    DataTable DTDocument = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                bindAllData();
            }
            else
            {

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindAllData()
    {
        var dt = (from p in HR.GradeTBs select new { Level = p.Levels,p.GradeName, p.GradeID, p.Description }).OrderBy(d=>d.GradeName);
        if (dt.Count() > 0)
        {
            GridViewUpload.DataSource = dt;
            GridViewUpload.DataBind();
        }
        else
        {
            GridViewUpload.DataSource = null;
            GridViewUpload.DataBind();
        }
    }

    private void ClearTextBoxOFDocDetails()
    {
        txtdescrip.Text = "";
        ddlGrade.SelectedIndex = 0;
        lstLevel.SelectedIndex = 0;
        btnsubmit.Text = "Save";
        bindAllData();
        lblid.Text = "0";
    }
  
    protected void imgExpedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        lblid.Text = Lnk.CommandArgument;
        GradeTB mt = HR.GradeTBs.Where(s => s.GradeID == Convert.ToInt32(lblid.Text)).First();
        txtdescrip.Text = mt.Description;
        ddlGrade.SelectedIndex = mt.GradeID;
        lstLevel.SelectedValue = mt.Levels;
        btnsubmit.Text = "Update";
        
    }
    protected void btnsubmit_Click1(object sender, EventArgs e)
    {

        if (btnsubmit.Text=="Save")
        {
            #region SAVE
            var dt = from p in HR.GradeTBs.Where(d => d.GradeName == int.Parse(ddlGrade.SelectedValue) && d.Levels == lstLevel.SelectedItem.Text) select new { p.GradeName, p.GradeID };
            if (dt.Count() > 0)
            {
                g.ShowMessage(this.Page, "This Details Already Exists");
            }
            else
            {
                var dtd = from p in HR.GradeTBs.Where(d => d.GradeName == int.Parse(ddlGrade.SelectedValue)) select new { p.GradeName, p.GradeID };
                if (dtd.Count() > 0)
                {
                    g.ShowMessage(this.Page, "This Details Already Exists");
                }
                else
                {
                    GradeTB grd = new GradeTB();
                    grd.GradeName = int.Parse(ddlGrade.SelectedValue);
                    grd.Description = txtdescrip.Text;
                    grd.Levels = lstLevel.SelectedItem.Text;
                    HR.GradeTBs.InsertOnSubmit(grd);
                    HR.SubmitChanges();
                    ClearTextBoxOFDocDetails();
                    bindAllData();
                    g.ShowMessage(this.Page, "Submitted Suceessfully....!!");
                }
            }
            #endregion
        }
        else
        {
            #region UPDATE
            var dt = from p in HR.GradeTBs.Where(d => d.GradeName == int.Parse(ddlGrade.SelectedValue) && d.GradeID != Convert.ToInt32(lblid.Text) && d.Levels == lstLevel.SelectedItem.Text) select new { p.GradeName, p.GradeID };
            if (dt.Count() > 0)
            {
                g.ShowMessage(this.Page, "This Details Already Exists");
            }
            else
            {
                var dtd = from p in HR.GradeTBs.Where(d => d.GradeName == int.Parse(ddlGrade.SelectedValue) && d.GradeID != Convert.ToInt32(lblid.Text)) select new { p.GradeName, p.GradeID };
                if (dtd.Count() > 0)
                {
                    g.ShowMessage(this.Page, "This Details Already Exists");
                }
                else
                {
                    GradeTB grd = HR.GradeTBs.Where(s => s.GradeID == Convert.ToInt32(lblid.Text)).First();
                    grd.GradeName = int.Parse(ddlGrade.SelectedValue);
                    grd.Description = txtdescrip.Text;
                    grd.Levels = lstLevel.SelectedItem.Text;
                    HR.SubmitChanges();
                    ClearTextBoxOFDocDetails();
                    bindAllData();
                    g.ShowMessage(this.Page, "Updated Successfully....!!");
                }
            }
            #endregion
        }
        


    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ClearTextBoxOFDocDetails();
    }
    protected void GridViewUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewUpload.PageIndex = e.NewPageIndex;
        bindAllData();
    }
}