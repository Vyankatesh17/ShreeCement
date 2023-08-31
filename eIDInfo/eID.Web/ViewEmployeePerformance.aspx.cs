using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewEmployeePerformance : System.Web.UI.Page
{
    /// <summary>
    ///  View Employee Performance Form
    ///  Created Date :19/12/2014
    ///  Created By Abdul Rahman
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int id = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    id =Convert.ToInt32(Request.QueryString["id"].ToString());
                 FillEmployee();
                 fillevaluater();
                txtdateeval.Attributes.Add("readonly", "readonly");
                Txtdate.Attributes.Add("readonly", "readonly");
                txtdateeval.Attributes.Add("readonly", "readonly");
                txtdate1.Attributes.Add("readonly", "readonly");
                txtdate2.Attributes.Add("readonly", "readonly");
                fillAlldetails();
                    
                }
                else
                {
                    Response.Redirect("EmployePerformEvolution.aspx");
                }
                

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
   
    private void FillEmployee()
    {
        try
        {
            var Empdata = (from d in HR.EmployeeTBs

                           select new { d.EmployeeId, EmpName = d.FName + " " + d.Lname }).OrderBy(s => s.EmpName);


            ddlEmp.DataSource = Empdata;
            ddlEmp.DataValueField = "EmployeeId";
            ddlEmp.DataTextField = "EmpName";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, "--Select--");

        }

        catch (Exception ex)
        {
            g.ShowMessage(this.Page, ex.Message);
        }
    }
    private void fillevaluater()
    {
        try
        {
             var Evaluaterdata = (from d in HR.EmployeeTBs

                        select new { d.EmployeeId, EmpName = d.FName + " " + d.Lname }).OrderBy(s => s.EmpName);


            ddlevaluater.DataSource = Evaluaterdata;
            ddlevaluater.DataValueField = "EmployeeId";
            ddlevaluater.DataTextField = "EmpName";
            ddlevaluater.DataBind();
            ddlevaluater.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    private void fillAlldetails()
    {
        try
        {
            EmployePerformance ep = HR.EmployePerformances.Where(s => s.EvolutionID == id).First();
            ddlEmp.SelectedValue = ep.EmpID.ToString();
            DateTime Evoluatedate = Convert.ToDateTime(ep.DateofEval);
            txtdateeval.Text = Evoluatedate.ToString("MM/dd/yyyy");
            txtEmpPosition.Text = ep.EmpPosition;
            ddlevaluater.SelectedValue = ep.EvaluaterID.ToString();
            if(ep.EvalType=="60 Days")
            {
            rdbDays.SelectedIndex=0;
            }
            if (ep.EvalType == "6 Month")
            {
                rdbDays.SelectedIndex = 1;
            }
            else
            {
                rdbDays.SelectedIndex =2;
            }
            RadioButtonList1.SelectedValue = ep.knowWork;
            RadioButtonList2.SelectedValue = ep.CustomResponse;
            RadioButtonList3.SelectedValue = ep.quality;
            RadioButtonList4.SelectedValue = ep.job;
            RadioButtonList5.SelectedValue = ep.attendence;

            RadioButtonList6.SelectedValue = ep.writtenoral;
            RadioButtonList7.SelectedValue = ep.decision;
            RadioButtonList8.SelectedValue = ep.industry;
            RadioButtonList9.SelectedValue = ep.coworker;
            RadioButtonList10.SelectedValue = ep.punctual;
            RadioButtonList11.SelectedValue = ep.quantity;
            RadioButtonList12.SelectedValue = ep.supervisor;

            txtsupervisorcomment.Text = ep.SupervisorComment;
            txtEmployeeComent.Text = ep.EmployeeComments;
            lblenpnameSing.Text = ep.EmployeeSignature;
            TextBox4.Text = ep.SupervisorSignature;

            DateTime EmpSigndate = Convert.ToDateTime(ep.EmpSingDate);
            Txtdate.Text = EmpSigndate.ToString("MM/dd/yyyy");

            DateTime SupSigndate = Convert.ToDateTime(ep.SupervisorSignDate);
            txtdate1.Text = SupSigndate.ToString("MM/dd/yyyy");

            Text5.Text = ep.Dept_ManagerSing;
            DateTime MangSigndate = Convert.ToDateTime(ep.Dept_ManagerSingDate);
            txtdate2.Text = MangSigndate.ToString("MM/dd/yyyy");

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }



    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployePerformEvolution.aspx");
    }
}