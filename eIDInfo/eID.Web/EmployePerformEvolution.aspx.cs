using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class EmployePerformEvolution : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    private int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                loadEMp();
                DataBind();
                txtdateeval.Text = g.GetCurrentDateTime().ToShortDateString();
                txtdate2.Text = g.GetCurrentDateTime().ToShortDateString();
                txtdate1.Text = g.GetCurrentDateTime().ToShortDateString();
                Txtdate.Text = g.GetCurrentDateTime().ToShortDateString();


                FillEmployee();
                txtdateeval.Attributes.Add("readonly", "readonly");
                Txtdate.Attributes.Add("readonly", "readonly");
                txtdateeval.Attributes.Add("readonly", "readonly");
                txtdate1.Attributes.Add("readonly", "readonly");
                txtdate2.Attributes.Add("readonly", "readonly");
                
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    private void loadEMp()
    {
        //var data = (from d in ex.EmployeeTBs
                  
        //           select new { d.EmployeeId, EmpName = d.FName + " " + d.Lname }).OrderBy(s=>s.EmpName);

        //if (data.Count() > 0 && data != null)
        //{
        //    ddEmployee.DataSource = data;
        //    ddEmployee.DataValueField = "EmployeeId";
        //    ddEmployee.DataTextField = "EmpName";
        //    ddEmployee.DataBind();
        //    ddEmployee.Items.Insert(0, "--Select--");
        //}
    }
    private void FillEmployee()
    {
        try
        {

            //bool status = g.CheckSupperVisor(Convert.ToInt32(Session["UserId"]));
            //   if (status == true)
            //   {
            //var data = from d in ex.ReportingHeadTBs
            // where d.Reporting_Head_Name_ID == Convert.ToInt32(Session["UserId"])
            // && d.EmployeeRegistrationTB1.Employee_Status==1 && d.EmployeeRegistrationTB1.TerminationStatus==0
            // select new {d.Employee_ID,EmpName=d.EmployeeRegistrationTB1.F_Name+" "+ d.EmployeeRegistrationTB1.M_Name+" "+ d.EmployeeRegistrationTB1.L_Name };

            //if (data.Count() > 0 && data !=null)
            //{
            //    ddlEmp.DataSource = data;
            //    ddlEmp.DataValueField = "Employee_ID";
            //    ddlEmp.DataTextField = "EmpName";
            //    ddlEmp.DataBind();
            //    ddlEmp.Items.Insert(0, "--Select--");
            //}
            //else
            //{
            //    ddlEmp.DataSource = data;
            //    ddlEmp.DataValueField = "Employee_ID";
            //    ddlEmp.DataTextField = "EmpName";
            //    ddlEmp.DataBind();
            //    ddlEmp.Items.Insert(0, "--Select--");
            //}

            // } DO not delete


            var data = (from d in HR.EmployeeTBs
                     
                       select new { d.EmployeeId, EmpName = d.FName + " " + d.Lname }).OrderBy(s=>s.EmpName);

          
                ddlEmp.DataSource = data;
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

    //private void FillEmployee()
    //{
    //    try 
    //{	        
		
    //         bool status = g.CheckSupperVisor(Convert.ToInt32(Session["UserId"]));
    //            if (status == true)
    //            {
    //                var data = from d in ex.ReportingHeadTBs
    //                 where d.Reporting_Head_Name_ID == Convert.ToInt32(Session["UserId"])
    //                 && d.EmployeeRegistrationTB1.Employee_Status==1 && d.EmployeeRegistrationTB1.TerminationStatus==0
    //                 select new {d.Employee_ID,EmpName=d.EmployeeRegistrationTB1.F_Name+" "+ d.EmployeeRegistrationTB1.M_Name+" "+ d.EmployeeRegistrationTB1.L_Name };

    //                if (data.Count() > 0 && data !=null)
    //                {
    //                     ddlEmp.DataSource = data;
    //                     ddlEmp.DataValueField = "Employee_ID";
    //                    ddlEmp.DataTextField = "EmpName";
    //                    ddlEmp.DataBind();
    //                    ddlEmp.Items.Insert(0, "--Select--");
    //                }
    //                else
    //                {
    //                    ddlEmp.DataSource = data;
    //                    ddlEmp.DataValueField = "Employee_ID";
    //                    ddlEmp.DataTextField = "EmpName";
    //                    ddlEmp.DataBind();
    //                    ddlEmp.Items.Insert(0, "--Select--");
    //                }

    //            }

       

    //}
	
    //catch (Exception ex)
    //    {
    //      System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','"+ex.Message+"');", true);
    //    }
    //}
   
    private void Bind()
    {
        try
        {
            var emp = from d in HR.EmployeeTBs
                      select new
                      {
                          EmployeeName = d.FName + " " + d.Lname,
                      };
            if (emp != null && emp.Count() > 0)
            {
                foreach (var item in emp)
                {
                    txtEmpName.Text = item.EmployeeName;

                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void txtEmpName_TextChanged(object sender, EventArgs e)
    {
        Binds();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext ex = new HrPortalDtaClassDataContext();
        List<string> Name = (from d in ex.EmployeeTBs

                             
                             select d.FName + ' ' + d.Lname).Distinct().ToList();


        return Name.ToArray();
        
    }
    public void Clear()
    {
        MultiView1.ActiveViewIndex = 0;
        DataBind();
        txtEmpName.Text = null;
        txtdateeval.Text = null;
        txtEmpPosition.Text = null;
        //txtEvaluatorName.Text = null;
        txtsupervisorcomment.Text = "";
        txtEmployeeComent.Text = "";
        ////TextBox3.Text = "";
        TextBox4.Text = "";
        Text5.Text = "";
        txtdate1.Text = "";
        txtdate2.Text="";
        Txtdate.Text="";
        rdbDays.SelectedIndex = 0;
        RadioButtonList1.SelectedIndex = 0;
        RadioButtonList2.SelectedIndex = 0;
        RadioButtonList3.SelectedIndex = 0;
        RadioButtonList4.SelectedIndex = 0;
        RadioButtonList5.SelectedIndex = 0;
        RadioButtonList6.SelectedIndex = 0;
        RadioButtonList7.SelectedIndex = 0;
        RadioButtonList8.SelectedIndex = 0;
        RadioButtonList9.SelectedIndex = 0;
        RadioButtonList10.SelectedIndex = 0;
        RadioButtonList11.SelectedIndex = 0;
        RadioButtonList12.SelectedIndex = 0;
        FillEmployee();
        loadEMp();
    }
    protected void btnsave_Click2(object sender, EventArgs e)
    {

        var dt = from p in HR.EmployePerformances.Where(d => d.EmpID ==Convert.ToInt32(ddlEmp.SelectedValue)  && d.DateofEval==Convert.ToDateTime(txtdateeval.Text)) select p;
        if (dt.Count() > 0)
        {
            g.ShowMessage(this.Page, "This Details Already Exist");
        }
        else
        {
            EmployePerformance EP = new EmployePerformance();
            EP.EmpName = ddlEmp.SelectedItem.Text;
            //EP.CreatedBy = Convert.ToInt32(ddEmployee.SelectedValue);
            EP.DateofEval = Convert.ToDateTime(txtdateeval.Text);
            EP.EmpPosition = txtEmpPosition.Text;
            EP.EvaluaterID = Convert.ToInt32(ddlevaluater.SelectedValue);
            EP.EvaluatorName = ddlevaluater.SelectedItem.Text;
            EP.EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
            EP.EmpIDandDate = Convert.ToInt32(ddlEmp.SelectedValue);
            if (rdbDays.SelectedIndex == 0)
            {
                EP.EvalType = " 60 Days ";
            }
            if (rdbDays.SelectedIndex == 1)
            {
                EP.EvalType = " 6 Month ";
            }
            if (rdbDays.SelectedIndex == 2)
            {
                EP.EvalType = "Year ";
            }
            EP.knowWork = RadioButtonList1.SelectedItem.Value;
            EP.CustomResponse = RadioButtonList2.SelectedItem.Value;
            EP.quality = RadioButtonList3.SelectedItem.Value;
            EP.job = RadioButtonList4.SelectedItem.Value;
            EP.attendence = RadioButtonList5.SelectedItem.Value;
            EP.writtenoral = RadioButtonList6.SelectedItem.Value;
            EP.decision = RadioButtonList7.SelectedItem.Value;
            EP.industry = RadioButtonList8.SelectedItem.Value;
            EP.coworker = RadioButtonList9.SelectedItem.Value;
            EP.punctual = RadioButtonList10.SelectedItem.Value;
            EP.quantity = RadioButtonList11.SelectedItem.Value;
            EP.supervisor = RadioButtonList12.SelectedItem.Value;
            EP.SupervisorComment = txtsupervisorcomment.Text;
            EP.EmployeeComments = txtEmployeeComent.Text;
            EP.EmployeeSignature = lblenpnameSing.Text;
            EP.SupervisorSignature = TextBox4.Text;
            EP.EmpSingDate=Convert.ToDateTime(Txtdate.Text);
            EP.SupervisorSignDate=Convert.ToDateTime(txtdate1.Text);
            EP.Dept_ManagerSing=Text5.Text;
            EP.Dept_ManagerSingDate = Convert.ToDateTime(txtdate2.Text);
            EP.Status=0;
            EP.User_ID = Convert.ToInt32(Session["UserId"]);
            HR.EmployePerformances.InsertOnSubmit(EP);
            HR.SubmitChanges();
            Clear();
        }
    }
    protected void btncancel_Click1(object sender, EventArgs e)
    {
        Clear();
    }
    private void Binds()
    {
        try
        {
            var Empdata = from d in HR.EmployeeTBs
                          
                          select new
                          {
                              
                              EmployeeName = d.FName + " " + d.Lname,
                          };
            if (Empdata != null && Empdata.Count() > 0)
            {
                foreach (var item in Empdata)
                {
                    txtEmpName.Text = item.EmployeeName;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void DataBind()
    {
        var data = from d in HR.EmployePerformances
                   select new
                   {
                       d.EmpName,
                       d.DateofEval,
                       d.EmpPosition,
                       d.EvaluatorName,
                       d.EvalType,
                       d.EvolutionID
                   };
        if (data != null && data.Count() > 0)
        {
            grd_EmpPerformance.DataSource = data;
            grd_EmpPerformance.DataBind();
        }
        else
        {
            grd_EmpPerformance.DataSource = null;
            grd_EmpPerformance.DataBind();
        }
    }
    protected void grd_EmpPerformance_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void grd_EmpPerformance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_EmpPerformance.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string ss = "window.open('EmployeePerformanceEvalViewer.aspx?Type=All','mywindow','width=1000,height=700,left=200,top=1,screenX=100,screenY=100,toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,copyhistory=yes,resizable=no')";
        string script = "<script language='javascript'>" + ss + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUpWindow", script, false);
       // Response.Redirect("PerformanceEvaluationForm.aspx");
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        { // creted by abdul Rahman Date : 19/12/2014
            if (ddlEmp.SelectedIndex > 0)
            {
                var Evaluaterdata = (from d in HR.EmployeeTBs
                                     where d.EmployeeId != Convert.ToInt32(ddlEmp.SelectedValue)
                                     select new { d.EmployeeId, EmpName = d.FName + " " + d.Lname }).OrderBy(s => s.EmpName);
                
                if (Evaluaterdata.Count() > 0)
                {
                   
                    ddlevaluater.DataSource = Evaluaterdata;
                    ddlevaluater.DataValueField = "EmployeeId";
                    ddlevaluater.DataTextField = "EmpName";
                    ddlevaluater.DataBind();
                    ddlevaluater.Items.Insert(0, "--Select--");
                }
                else
                {
                   
                    ddlevaluater.DataSource = null;
                    ddlevaluater.DataBind();
                    ddlevaluater.Items.Clear();
                }
            }
            else
            {
                ddlevaluater.DataSource = null;
                ddlevaluater.DataBind();
                ddlevaluater.Items.Clear();
            }


            var data = (from d in HR.EmployeeTBs
                        join dp in HR.MasterDesgTBs on d.DesgId equals dp.DesigID
                       where  d.EmployeeId == Convert.ToInt32(ddlEmp.SelectedValue)
                        select new { d.EmployeeId, EmpName = d.FName + " " + d.Lname,
                                    dp.DesigName

                        
                        }).OrderBy(s => s.EmpName);

            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    lblenpnameSing.Text = item.EmpName;
                    txtEmpPosition.Text=item.DesigName;
                }
            }
            else
            {
                lblenpnameSing.Text = "";
                txtEmpPosition.Text="";
            }
            
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void View_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        lbleditId.Text = img.CommandArgument.ToString();
        Session["ViewId"] = img.CommandArgument.ToString();
        Response.Redirect("ViewEmployeePerformance.aspx?id=" + img.CommandArgument);
    }
}