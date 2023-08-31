using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            CompareValidator2.ValueToCompare = DateTime.Now.ToShortDateString();
            if (!Page.IsPostBack)
            {
                txtDate.Text = g.GetCurrentDateTime().ToShortDateString();  
                fillDept();
                fillemp();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
    }

    private void fillDept()
    {
        var data = from dt in HR.MasterDeptTBs
                   orderby dt.DeptName ascending
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlDept.DataSource = data;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DeptID";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "--Select--");
        }
    }
    private void fillemp()
    {
        var data = from dt in HR.EmployeeTBs
                   orderby dt.FName ascending
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlEmp.DataSource = data;
            ddlEmp.DataTextField = "FName";
            ddlEmp.DataValueField = "EmployeeId";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, "--Select--");
        }
    }
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        tblTraining tbt = new tblTraining();
        tbt.deptID = Convert.ToInt32(ddlDept.SelectedValue);
        tbt.EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
        tbt.TrainingDate = Convert.ToDateTime(txtDate.Text);
        tbt.TrainingTime = txtTime.Text;
        tbt.TrainingSubject = txtSubject.Text;
        tbt.Trainer = txtTrainer.Text;
        tbt.Status = "pending";
        HR.tblTrainings.InsertOnSubmit(tbt);
        HR.SubmitChanges();
        clearFields();
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
"alert('Training Details Saved Successfully....'); window.location='" +
Request.RawUrl + "';", true);
    }
    private void clearFields()
    {
        ClearInputs(Page.Controls);
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
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }
}