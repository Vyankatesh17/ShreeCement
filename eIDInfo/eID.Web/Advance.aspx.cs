using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Advance : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
                txtduedate.Text = g.GetCurrentDateTime().ToShortDateString();
                MultiView1.ActiveViewIndex = 0;
                BindAllData();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindAllData()
    {
        bool status ;
        status = g.CheckAdmin(Convert.ToInt32(Session["UserID"].ToString()));
       
           string query ="";
        if(status ==true)
        {
            query = "select FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId order by EA.EnterDate desc";
        }
        else
        {
            query = "select FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId where EA.employeeid=" + Session["UserID"].ToString() + " order by EA.EnterDate desc";
        }

        DataSet dsall = g.ReturnData1(query);
        grdAdvance.DataSource =dsall.Tables[0];
        grdAdvance.DataBind();
        lblcnt.Text = dsall.Tables[0].Rows.Count.ToString();

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
       
    }

    private void clear()
    {
        txtamount.Text = "";
        txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
        txtduedate.Text = g.GetCurrentDateTime().ToShortDateString();
        txtremarks.Text = "";
        BindAllData();
        MultiView1.ActiveViewIndex = 0;

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        EmployeeAdvanceTB EA = new EmployeeAdvanceTB();
        EA.EmployeeId = Convert.ToInt32(Session["UserId"]);
        EA.Amount = Convert.ToDecimal(txtamount.Text);
        EA.EnterDate = Convert.ToDateTime(txtDate.Text);
        EA.DueDate = Convert.ToDateTime(txtduedate.Text);
        EA.DeductionMonths = Convert.ToInt32(txtdeduction.Text);
        EA.Remarks = txtremarks.Text;
        EA.EmployeeStatus ="Approved";
        EA.DepartmentHeadStatus = "Pending";
        EA.ManagerStatus = "Pending";
        EA.HRStatus = "Pending";
        HR.EmployeeAdvanceTBs.InsertOnSubmit(EA);
        HR.SubmitChanges();

        EmployeeAdvanceApproveTB EAD = new EmployeeAdvanceApproveTB();
        EAD.AdvanceId = EA.AdvanceId;
        EAD.EmployeeId = Convert.ToInt32(Session["UserId"]);
        EAD.Amount = Convert.ToDecimal(txtamount.Text);
        EAD.Date = Convert.ToDateTime(txtDate.Text);
        EAD.DueDate = Convert.ToDateTime(txtduedate.Text);
        EAD.DeductionMonths = Convert.ToInt32(txtdeduction.Text);
        EAD.Remarks = txtremarks.Text;
        EAD.UserType = "Employee";
        EAD.Status = "Approved";
        HR.EmployeeAdvanceApproveTBs.InsertOnSubmit(EAD);
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "Advance Details Saved Successfully");

        clear();
        
    }
    protected void grd_Company_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAdvance.PageIndex = e.NewPageIndex;
        BindAllData();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void txtamount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtdeduction_TextChanged(object sender, EventArgs e)
    {

    }
}