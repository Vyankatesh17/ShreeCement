using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BonusCalculation : System.Web.UI.Page
{
    /// <summary>
    ///  Bonus calculation Form
    ///  Created date :16/12/2014
    ///  Created By Abdul Rahman
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable DTAddDetails = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                fillCompany();
                bindEmployeeListGrid();
                lbldate.Text = DateTime.UtcNow.ToShortDateString();

                txtfromdate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");
                DataSet ds = g.ReturnData1("declare @yyyy int; set @yyyy = '" + DateTime.Now.Year + "';SELECT convert(varchar,DATEADD(yyyy, @yyyy - 1900, 0), 101) AS StartDate,  convert(varchar,DATEADD(yyyy, @yyyy - 1899, -1), 101) AS EndDate");
                txtfromdate.Text = ds.Tables[0].Rows[0][0].ToString();
                txtTodate.Text = ds.Tables[0].Rows[0][1].ToString();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindEmployeeListGrid()
    {
        DataTable dtfetchemplist = g.ReturnData("");
    }

    private void fillCompany()
    {
        try
        {
            var fetchcomp = from d in HR.CompanyInfoTBs
                            where d.Status == 0
                            select new { d.CompanyId, d.CompanyName };

            ddlcompany.DataSource = fetchcomp;
            ddlcompany.DataTextField = "CompanyName";
            ddlcompany.DataValueField = "CompanyId";
            ddlcompany.DataBind();
            ddlcompany.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
        calculateFun();
    }

    private void bindgrid()
    {
        try
        {
            if (ddlcompany.SelectedIndex > 0)
            {
              #region fill all employee Company wise
            DataTable Dtemp = g.ReturnData("select   SP.SalProcessId,Em.CompanyId, Em.DeptId , Em.Grade,SP.EmployeeId,EM.Fname+' '+EM.Lname as EmpName,DT.DeptName,SP.GrossSalary,(select amount From SalaryProcessDetailsTB where Componentid='Basic' and EmpSalaryProcessid=SP.SalProcessId  and EmpSalaryProcessid is not null) as BasicAmt from salaryProcessTB  SP Left outer join EmployeeTB EM on SP.EmployeeID=Em.EmployeeId Left outer join MasterDeptTB DT on Em.DeptId=DT.DeptID where Em.CompanyId='" + Convert.ToInt32(ddlcompany.SelectedValue) + "' and Em.RelivingStatus is null  and EM.EmployeeId not in (select BonusCalculatedTB.Employee_ID from BonusCalculatedTB) order by SP.SalProcessId DESC");
            ViewState["DTAddDetails"] = null;
            if (Dtemp.Rows.Count > 0)
            {
                for (int i = 0; i < Dtemp.Rows.Count; i++)
                {
                    int cnt = 0;
                    if (ViewState["DTAddDetails"] != null)
                    {
                        DTAddDetails = (DataTable)ViewState["DTAddDetails"];
                    }
                    else
                    {
                        DataColumn EmpName = DTAddDetails.Columns.Add("EmpName");
                        DataColumn DeptName = DTAddDetails.Columns.Add("DeptName");
                        DataColumn BasicAmt = DTAddDetails.Columns.Add("BasicAmt");
                        DataColumn GrossSalary = DTAddDetails.Columns.Add("GrossSalary");
                        DataColumn Absolute = DTAddDetails.Columns.Add("Absolute");
                        DataColumn Proposed = DTAddDetails.Columns.Add("Proposed");
                        DataColumn ProposedInc = DTAddDetails.Columns.Add("ProposedInc");
                        DataColumn Additional = DTAddDetails.Columns.Add("Additional");
                        DataColumn TotalInc = DTAddDetails.Columns.Add("TotalInc");
                        DataColumn GradeType = DTAddDetails.Columns.Add("Grade_Type");
                        DataColumn EmployeeId = DTAddDetails.Columns.Add("EmployeeId");
                        DataColumn DeptId = DTAddDetails.Columns.Add("DeptId");
                        DataColumn Grade = DTAddDetails.Columns.Add("Grade");
                    }
                    if (Dtemp.Rows[i]["Grade"].ToString() != "--Select--")
                    {
                        DataTable dtgradtype = g.ReturnData("Select Grade_Type,Grade_In_Percentage,Grade_In_Value from GradeMasterWithCompanyWiseTB where Company_ID='" + Convert.ToInt32(ddlcompany.SelectedValue) + "' and Grade='" + Convert.ToInt32(Dtemp.Rows[i]["Grade"].ToString()) + "'");
                        if (dtgradtype.Rows.Count > 0)
                        {
                            lblabsolute.Text = dtgradtype.Rows[0]["Grade_In_Value"].ToString();
                            if (lblabsolute.Text == "")
                            {
                                lblabsolute.Text = "0";
                                lblProposed.Text = dtgradtype.Rows[0]["Grade_In_Percentage"].ToString();
                            }
                            else
                            {
                                lblabsolute.Text = dtgradtype.Rows[0]["Grade_In_Value"].ToString();
                                lblProposed.Text = "0";
                            }


                        }
                        else
                        {
                            lblabsolute.Text = "0";
                            lblProposed.Text = "0";
                        }
                        DataRow dr = DTAddDetails.NewRow();
                        dr[0] = Dtemp.Rows[i]["EmpName"].ToString();
                        dr[1] = Dtemp.Rows[i]["DeptName"].ToString();
                        if (Dtemp.Rows[i]["BasicAmt"].ToString() == "")
                        {
                            dr[2] = "0";
                        }
                        else
                        {
                            dr[2] = Dtemp.Rows[i]["BasicAmt"].ToString();
                        }
                        if (Dtemp.Rows[i]["GrossSalary"].ToString() == "")
                        {
                            dr[3] = "0";
                        }
                        else
                        {
                            dr[3] = Dtemp.Rows[i]["GrossSalary"].ToString();
                        }
                        dr[4] = lblabsolute.Text;
                        dr[5] = lblProposed.Text;
                        dr[6] = "0";
                        dr[7] = "0";
                        dr[8] = "0";
                        if (dtgradtype.Rows.Count > 0)
                        {
                            dr[9] = dtgradtype.Rows[0]["Grade_Type"].ToString();
                        }
                        else
                        {
                            dr[9] = "0";
                        }
                        dr[10] = Dtemp.Rows[i]["EmployeeId"].ToString();
                        dr[11] = Dtemp.Rows[i]["DeptId"].ToString();
                        dr[12] = Dtemp.Rows[i]["Grade"].ToString();

                        for (int j = 0; j < DTAddDetails.Rows.Count; j++)
                        {
                            if (DTAddDetails.Rows[j]["EmployeeId"].ToString() == Dtemp.Rows[i]["EmployeeId"].ToString())
                            {
                                cnt++;
                            }
                        }
                        if (cnt == 0)
                        {
                            DTAddDetails.Rows.Add(dr);
                        }

                        ViewState["DTAddDetails"] = DTAddDetails;

                    }
                }
                grd_employee.DataSource = DTAddDetails;
                grd_employee.DataBind();
                lblcnt.Text = DTAddDetails.Rows.Count.ToString();
                trchecked.Visible = true;
                trbtn.Visible = true;
            }
            else
            {
                grd_employee.DataSource = null;
                grd_employee.DataBind();
                lblcnt.Text = "0";
                ViewState["DTAddDetails"] = null;
                trchecked.Visible = false;
                trbtn.Visible = false;
            }
            #endregion
            }
            else
            {
                grd_employee.DataSource = null;
                grd_employee.DataBind();
                lblcnt.Text = "0";
                ViewState["DTAddDetails"] = null;
                trchecked.Visible = false;
                trbtn.Visible = false;
            }
        }
        catch (Exception EX)
        {

            g.ShowMessage(this.Page, EX.Message);
        }
    }
    protected void btncalculate_Click(object sender, EventArgs e)
    {
        calculateFun();
        
    }

    private void calculateFun()
    {
        try
        {
            if (ViewState["DTAddDetails"] != null)
            {
                DataTable takeallEmp = (DataTable)ViewState["DTAddDetails"];
           
            if (takeallEmp.Rows.Count > 0)
            {
                for (int i = 0; i < takeallEmp.Rows.Count; i++)
                {
                    CheckBox chhk = (CheckBox)grd_employee.Rows[i].FindControl("chkemp");
                    Label lblPropInc = (Label)grd_employee.Rows[i].FindControl("lblproposedincgrd");
                    Label lblTotInc = (Label)grd_employee.Rows[i].FindControl("lblTotalIncgrd");
                    TextBox txtaddInc = (TextBox)grd_employee.Rows[i].FindControl("txtAdditional");


                    if (chhk.Checked == true)
                    {
                        if (takeallEmp.Rows[i]["Grade_Type"].ToString() == "0")
                        {
                            //Generate Bonus From Basic salary 
                            lblPropInc.Text = (Convert.ToDecimal(takeallEmp.Rows[i]["BasicAmt"].ToString()) * Convert.ToDecimal(takeallEmp.Rows[i]["Proposed"].ToString()) / 100).ToString();
                        }

                        if (takeallEmp.Rows[i]["Grade_Type"].ToString() == "1")
                        {
                            //Generate Bonus From Gross salary 
                            lblPropInc.Text = (Convert.ToDecimal(takeallEmp.Rows[i]["GrossSalary"].ToString()) * Convert.ToDecimal(takeallEmp.Rows[i]["Proposed"].ToString()) / 100).ToString();
                        }
                        if (takeallEmp.Rows[i]["Grade_Type"].ToString() == "2")
                        {
                            //Generate Bonus From Asolute Value 
                            lblPropInc.Text = (Convert.ToDecimal(takeallEmp.Rows[i]["Absolute"].ToString())).ToString();
                        }
                        decimal proInc = Convert.ToDecimal(lblPropInc.Text);
                        decimal tot = Convert.ToDecimal(txtaddInc.Text);
                        lblTotInc.Text = (proInc + tot).ToString();

                    }
                }
            }
            }

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcompany.SelectedIndex > 0)
            {
                int ctn = 0;
             DataTable takeallEmp =(DataTable)ViewState["DTAddDetails"];
             if (takeallEmp.Rows.Count > 0)
             {
                 for (int j = 0; j < takeallEmp.Rows.Count; j++)
                 {
                     CheckBox chhk1 = (CheckBox)grd_employee.Rows[j].FindControl("chkemp");
                     Label lblTotInc1 = (Label)grd_employee.Rows[j].FindControl("lblTotalIncgrd");
                     //for (int k = 0; k < grd_employee.Rows.Count; k++)
                     //{
                         if (chhk1.Checked == true)
                         {
                             ctn++;
                         }
                     }

                    if (ctn > 0)
                    {
                         for (int i = 0; i < takeallEmp.Rows.Count; i++)
                       {
                     CheckBox chhk = (CheckBox)grd_employee.Rows[i].FindControl("chkemp");
                     Label lblPropInc = (Label)grd_employee.Rows[i].FindControl("lblproposedincgrd");
                     Label lblTotInc = (Label)grd_employee.Rows[i].FindControl("lblTotalIncgrd");
                     TextBox txtaddInc = (TextBox)grd_employee.Rows[i].FindControl("txtAdditional");
                       #region save 
                     if (chhk.Checked == true)
                     {
                         BonusCalculatedTB bn = new BonusCalculatedTB();
                         bn.Company_ID = Convert.ToInt32(ddlcompany.SelectedValue);
                         bn.Employee_ID =Convert.ToInt32(takeallEmp.Rows[i]["EmployeeId"].ToString());
                         bn.BonusCreated_Date = Convert.ToDateTime(lbldate.Text);
                         bn.FromDate = Convert.ToDateTime(txtfromdate.Text);
                         bn.ToDate = Convert.ToDateTime(txtTodate.Text);

                         bn.Department_ID = Convert.ToInt32(takeallEmp.Rows[i]["DeptId"].ToString());
                         if (takeallEmp.Rows[i]["Grade_Type"].ToString()=="0")
                         {
                             bn.Type_ofBonus = "% Of Basic";
                         }
                         if (takeallEmp.Rows[i]["Grade_Type"].ToString() == "1")
                         {
                             bn.Type_ofBonus = "% Of Gross";
                         }
                         if (takeallEmp.Rows[i]["Grade_Type"].ToString() == "2")
                         {
                             bn.Type_ofBonus = "Absolute Value";
                         }
                             bn.BasicSalary = Convert.ToDecimal(takeallEmp.Rows[i]["BasicAmt"].ToString());
                             bn.GrossSalary = Convert.ToDecimal(takeallEmp.Rows[i]["GrossSalary"].ToString());
                             bn.Asolute_Value = Convert.ToDecimal(takeallEmp.Rows[i]["Absolute"].ToString());
                             bn.Proposed_Percentage = Convert.ToDecimal(takeallEmp.Rows[i]["Proposed"].ToString());
                             bn.ProposedInc = Convert.ToDecimal(lblPropInc.Text);
                             bn.AdditionalValue = Convert.ToDecimal(txtaddInc.Text);
                             bn.TotalValue = Convert.ToDecimal(lblTotInc.Text);
                             bn.Status = 0;
                             bn.User_ID = Convert.ToInt32(Session["UserId"]);
                             HR.BonusCalculatedTBs.InsertOnSubmit(bn);
                             HR.SubmitChanges();

                     }
                   }
                         g.ShowMessage(this.Page, "Bonus Details Saved Successfully");
                       #endregion

                    }
                    else
                    {
                        g.ShowMessage(this.Page, "Please Checked Check Boxes");
                    }
                 }
             }
            bindgrid();
            }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        int count = grd_employee.Rows.Count;
        int i = 0;

        if (chkall.Checked == true)
        {
            for (i = 0; i < count; i++)
            {
                CheckBox Check = (CheckBox)grd_employee.Rows[i].FindControl("chkemp");
                Check.Checked = true;

            }
        }
        else
        {
            for (i = 0; i < count; i++)
            {
                CheckBox Check = (CheckBox)grd_employee.Rows[i].FindControl("chkemp");
                Check.Checked = false;

            }
        }
    }


    protected void txtAdditional_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grd_employee.Rows.Count; i++)
            {
                CheckBox chhk = (CheckBox)grd_employee.Rows[i].FindControl("chkemp");
                Label lblPropInc = (Label)grd_employee.Rows[i].FindControl("lblproposedincgrd");
                Label lblTotInc = (Label)grd_employee.Rows[i].FindControl("lblTotalIncgrd");
                TextBox txtaddInc = (TextBox)grd_employee.Rows[i].FindControl("txtAdditional");

                decimal proInc = Convert.ToDecimal(lblPropInc.Text);
                decimal tot = Convert.ToDecimal(txtaddInc.Text);
                lblTotInc.Text = (proInc + tot).ToString();
            }

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void txtfromdate_TextChanged(object sender, EventArgs e)
    {
        if (txtfromdate.Text != "" && txtTodate.Text != "")
        {

            if (Convert.ToDateTime(txtfromdate.Text) > Convert.ToDateTime(txtTodate.Text))
            {
                g.ShowMessage(this, "You Can Not Select From Date Greater Than To Date");
                txtfromdate.Text = "";
            }
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        if (txtfromdate.Text != "" && txtTodate.Text != "")
        {
            if (Convert.ToDateTime(txtfromdate.Text) > Convert.ToDateTime(txtTodate.Text))
            {
                g.ShowMessage(this, "You Can Not Select To Date Less Than From Date");
                txtTodate.Text = "";
            }
        }
    }
}