using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalaryIncrement : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindGridView();
                BindAllEmployee();
                txtActionDate.Text = g.GetCurrentDateTime().ToShortDateString();
                txtActionDate.Attributes.Add("readonly", "readonly");
                txtEffectDate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindGridView()
    {
        var SalaryIncrementData = from dt in HR.SalaryPromotionTBs
                                  join dts in HR.EmployeeTBs on dt.EmployeeId equals dts.EmployeeId
                                  join dept in HR.MasterDeptTBs on dt.DeptId equals dept.DeptID
                                  join desi in HR.MasterDesgTBs on dt.DesigId equals desi.DesigID
                                  select new
                                  {
                                      dt.SalPromId,
                                      dt.EmployeeId,
                                      Name = dts.FName + ' ' + dts.Lname,
                                      PromotionIncrement = dt.PromotionIncrement == 0 ? "Promotion" : dt.PromotionIncrement == 1 ? "Increment" : "Both",
                                      dept.DeptName,
                                      desi.DesigName,

                                      dt.ActionDate,
                                      dt.WithEffectDate,
                                      // dt.DeptId,
                                      dt.DesigId
                                      //Status = dt.Status == 0 ? "Active" : "In Active"

                                  };
        if (SalaryIncrementData.Count() > 0)
        {
            if (divEmpTextBoxSerach.Visible == true)
            {
                SalaryIncrementData = SalaryIncrementData.Where(d => d.Name == txtEmployeeName.Text);
            }
            grdIncrement.DataSource = SalaryIncrementData;
            grdIncrement.DataBind();
            lblcnt.Text = SalaryIncrementData.Count().ToString();
        }
        else
        {
            grdIncrement.DataSource = null;
            grdIncrement.DataBind();
            lblcnt.Text = "0";
        }
    }

    private void BindAllEmployee()
    {
        var data = (from dt in HR.EmployeeTBs
                    where dt.RelivingStatus == null
                    select new
                    {
                        dt.EmployeeId,
                        Name = dt.FName + ' ' + dt.MName + ' ' + dt.Lname
                    }).OrderBy(d => d.Name);
        if (data.Count() > 0)
        {
            ddlEmployee.DataSource = data;
            ddlEmployee.DataTextField = "Name";
            ddlEmployee.DataValueField = "EmployeeId";
            ddlEmployee.DataBind();

        }
        else
        {
            ddlEmployee.DataSource = null;
            ddlEmployee.DataBind();
        }
        ddlEmployee.Items.Insert(0, "--Select--");

    }

    private void FillDepartment()
    {
        var data = from dt in HR.MasterDeptTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {
            ddlDepartment.DataSource = data;
            ddlDepartment.DataTextField = "DeptName";
            ddlDepartment.DataValueField = "DeptID";
            ddlDepartment.DataBind();



        }
        else
        {
            ddlDepartment.DataSource = null;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Clear();
            ddlEmployee.Items.Clear();
        }
        ddlDepartment.Items.Insert(0, "--Select--");
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        lbBasicSalary.Visible = false;
        lbLast.Visible = false;
        MultiView1.ActiveViewIndex = 1;
    }
    protected void ddlsortby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsortby.SelectedIndex > 0)
        {
            divEmpTextBoxSerach.Visible = true;
        }
        else
        {
            divEmpTextBoxSerach.Visible = false;
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedIndex > 0)
        {
            FillDepartment();
            FilllDesignation();
            FillGrade();
            GetSalaryDetail();
        }
        else
        {
            ddlDepartment.Items.Clear();
            ddlDesignation.Items.Clear();
            ddlGrade.Items.Clear();
            lbLast.Visible = lbBasicSalary.Visible = false;
        }
    }

    private void GetSalaryDetail()
    {
        DataTable dtbasicamt = g.ReturnData("select top 1 amount From SalaryProcessDetailsTB where EmployeeId='" + Convert.ToInt32(ddlEmployee.SelectedValue) + "' and Componentid='Basic' order by Salaryid desc");
        if (dtbasicamt.Rows.Count > 0)
        {
            lbBasicSalary.Text = dtbasicamt.Rows[0]["amount"].ToString();
            lbBasicSalary.Visible = true;
            lbLast.Visible = true;
        }
    }

    private void FillGrade()
    {
        var dt = from p in HR.GradeTBs select new { p.GradeID, p.GradeName };
        ddlGrade.DataSource = dt;
        ddlGrade.DataValueField = "GradeID";
        ddlGrade.DataTextField = "GradeName";
        ddlGrade.DataBind();
        ddlGrade.Items.Insert(0, "--Select--");
    }

    private void FilllDesignation()
    {
        var data = from dt in HR.MasterDesgTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlDesignation.DataSource = data;
            ddlDesignation.DataTextField = "DesigName";
            ddlDesignation.DataValueField = "DesigID";
            ddlDesignation.DataBind();
        }
        else
        {
            ddlDesignation.DataSource = null;
            ddlDesignation.DataBind();
            ddlDesignation.Items.Clear();
        }
        ddlDesignation.Items.Insert(0, "--Select--");
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            SalaryPromotionTB INC = new SalaryPromotionTB();
            INC.PromotionIncrement = rdIncrementStatus.SelectedIndex;
            INC.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
            INC.ActionDate = Convert.ToDateTime(txtActionDate.Text);
            if (txtEffectDate.Text != "")
            {
                INC.WithEffectDate = Convert.ToDateTime(txtEffectDate.Text);
            }
            INC.Grade = ddlGrade.SelectedItem.Text;
            INC.DesigId = Convert.ToInt32(ddlDesignation.SelectedValue);
            INC.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);

            HR.SalaryPromotionTBs.InsertOnSubmit(INC);
            HR.SubmitChanges();
            g.ShowMessage(Page, "Salary Save Successfully...");
            MultiView1.ActiveViewIndex = 0;
            clearAllControl();
            BindAllEmployee();
        }
    }

    private void clearAllControl()
    {
        ddlEmployee.SelectedIndex = 0;
        txtEffectDate.Text = string.Empty;
        ddlGrade.Items.Clear();
        ddlDepartment.Items.Clear();
        ddlDesignation.Items.Clear();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetEmployeeList(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> EmployeeList = (from d in hr.EmployeeTBs
                                        .Where(r => (r.FName + " " + r.Lname).Contains(prefixText) && r.RelivingStatus == null)
                                     select d.FName + " " + d.Lname).Distinct().ToList();
        return EmployeeList;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        divEmpTextBoxSerach.Visible = false;
        txtEmployeeName.Text = string.Empty;
        ddlsortby.SelectedIndex = 0;
        BindGridView();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ddlEmployee.SelectedIndex = 0;
        ddlGrade.Items.Clear();
        ddlDepartment.Items.Clear();
        ddlDesignation.Items.Clear();
        txtEffectDate.Text = string.Empty;
        MultiView1.ActiveViewIndex = 0;
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ImageButton Lnk = (ImageButton)sender;
        string PromotionID = Lnk.CommandArgument;
        EditId.Text = PromotionID;
        SalaryPromotionTB mt = HR.SalaryPromotionTBs.Where(s => s.SalPromId == Convert.ToInt32(PromotionID)).First();
        ddlEmployee.SelectedValue = Convert.ToString(mt.EmployeeId);
        txtActionDate.Text = Convert.ToDateTime(mt.ActionDate).ToShortDateString();
        if (mt.WithEffectDate.Value != null)
        {
            txtEffectDate.Text = Convert.ToDateTime(mt.WithEffectDate).ToShortDateString();

        }
        ddlDepartment.SelectedValue = Convert.ToString(mt.DeptId);
        ddlGrade.SelectedValue = Convert.ToString(mt.Grade);
        ddlDesignation.SelectedValue = Convert.ToString(mt.DesigId);
    }
}