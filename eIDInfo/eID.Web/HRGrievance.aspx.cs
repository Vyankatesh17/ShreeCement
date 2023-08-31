using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRGrievance : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                
              
                bindgrid();

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindgrid()
    {
        var griddata = from dt in HR.ComplaintTBs
                       join d in HR.EmployeeTBs on dt.EmpId equals d.EmployeeId
                       select new { dt.ComplaintId,EmployeeName=d.FName+" "+d.Lname, dt.ComplaintCode, dt.Date, dt.Title, dt.Description, dt.Status, HRRemark = dt.HRRemark == null ? "Pending" : dt.HRRemark };
        if (griddata.Count() > 0)
        {
            grd_Complaint.DataSource = griddata;
            grd_Complaint.DataBind();
            lblcnt.Text = griddata.Count().ToString();

        }
        else
        {
            grd_Complaint.DataSource = null;
            grd_Complaint.DataBind();
            lblcnt.Text = griddata.Count().ToString();
        }
    }
    protected void grd_Complaint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Complaint.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        lblid.Text = imgedit.CommandArgument;
        tblStatus.Visible = true;
        modRelive.Show();
    }
    protected void btnpopsave_Click1(object sender, EventArgs e)
    {
        ComplaintTB CT = HR.ComplaintTBs.Where(d=>d.ComplaintId==Convert.ToInt32(lblid.Text)).First();
        CT.HRRemark = txtremark.Text;
        CT.Status = ddlstatus.SelectedItem.Text;
        HR.SubmitChanges();
        txtremark.Text = "";
        ddlstatus.SelectedIndex = 0;        
//        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
//"alert('HR Grievance Saved Successfully'); window.location='" +
//Request.RawUrl + "';", true);
       //Message("HR Grievance Saved Successfully");
        
    }
    protected void Message(string msg)
    {
        string rmsg = "<script language='javascript'>window.alert('" + msg + "')</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", rmsg, false);
    }
}