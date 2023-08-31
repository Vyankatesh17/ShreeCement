using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrainingRequest : System.Web.UI.Page
{
    //Developer Name :Manasi
    //Creation Date : 17 Dec 2014
    //Page : Training Request


    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                mult.ActiveViewIndex = 0;
                BindGridData();
                txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    private void BindGridData()
    {
        bool admin = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        if (admin)
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 0
                      select new { p.TraingID, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {
                grdTraining.DataSource = dt;
                grdTraining.DataBind();
                lblcnt.Text = dt.Count().ToString();
            }
            else
            {
                lblcnt.Text = "0";
                grdTraining.DataSource = null;
                grdTraining.DataBind();
            }
        }
        else
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 0 && p.EmpID == Convert.ToInt32(Session["UserId"])
                      select new { p.TraingID, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {
                grdTraining.DataSource = dt;
                grdTraining.DataBind();
                lblcnt.Text = dt.Count().ToString();
            }
            else
            {
                lblcnt.Text = "0";
                grdTraining.DataSource = null;
                grdTraining.DataBind();
            }
        }






       
    }

    protected void Message(string msg)
    {
        string rmsg = "<script language='javascript'>window.alert('" + msg + "')</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", rmsg, false);
    }
    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
    }
    private void clearFields()
    {
        BindGridData();
        ClearInputs(Page.Controls);
        txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
        rbPersonal.Checked = true; rbTeam.Checked = false; 
        mult.ActiveViewIndex = 0;

    }
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        var dt = from p in HR.TrainingTBs.Where(d => d.EmpID == Convert.ToInt32(Session["UserId"]) && d.ReqDate == Convert.ToDateTime(txtDate.Text) && d.Title == txtTitle.Text) select p;
        if (dt.Count() > 0)
        {
            Message("Training Request Already Sent");

        }
        else
        {
            TrainingTB tr = new TrainingTB();
            tr.EmpID = Convert.ToInt32(Session["UserId"]);
            tr.ReqDate = Convert.ToDateTime(txtDate.Text);
            tr.Title = txtTitle.Text;
            tr.CourseContent = txtContent.Text;
            tr.Description = txtDesc.Text;
            tr.RHApproval = 0;
            if (rbPersonal.Checked)
            {
                tr.Type = rbPersonal.Text;
            }
            else if (rbTeam.Checked)
            {
                tr.Type = rbTeam.Text;
            }
            HR.TrainingTBs.InsertOnSubmit(tr);
            HR.SubmitChanges();
            clearFields();
            Message("Data Saved Successfully");
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingRequest.aspx");
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        mult.ActiveViewIndex = 1;
    }
    protected void grdTraining_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTraining.PageIndex = e.NewPageIndex;
        BindGridData();
    }
}