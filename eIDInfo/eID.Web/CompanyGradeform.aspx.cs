using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CompanyGradeform : System.Web.UI.Page
{
    /// <summary>
    ///  Company Grade Master Form
    ///  Created Date : 16/12/2014
    ///  Created By Abdul Rahman
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
               fillCompany();
              trgradePercentage.Visible = true;
              bindgrdofgrade();
              FillGrade();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
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

    private void bindgrdofgrade()
    {
        try
        {
            DataTable dtfetchgrdvalue = g.ReturnData("Select cg.Grade_ID, gt.GradeName AS Grade, case when cg.Grade_Type=0 OR cg.Grade_Type=1 then 'Percentage' else 'Value' end AS Grade_Type, case when cg.Grade_In_Percentage IS NULL then '0' else cg.Grade_In_Percentage end AS Grade_In_Percentage, case when cg.Grade_In_Value IS NULL then '0' else cg.Grade_In_Value end AS Grade_In_Value, c.CompanyName from  GradeMasterWithCompanyWiseTB cg Left outer join CompanyInfoTB c on c.CompanyId= cg.Company_ID Left outer Join GradeTB gt on gt.GradeID=cg.Grade");
            if (dtfetchgrdvalue.Rows.Count > 0)
            {
                grd_companyGrad.DataSource = dtfetchgrdvalue;
                grd_companyGrad.DataBind();
                lblcnt.Text = dtfetchgrdvalue.Rows.Count.ToString();
            }
            else
            {
                grd_companyGrad.DataSource = null;
                grd_companyGrad.DataBind();
                lblcnt.Text = "0";
            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    public void fillCompany()
    {
        try
        {
            var fetchcomp = (from d in HR.CompanyInfoTBs
                            where d.Status == 0
                             select new { d.CompanyId, d.CompanyName }).OrderBy(d => d.CompanyName);

            ddlcompany.DataSource = fetchcomp;
            ddlcompany.DataTextField = "CompanyName";
            ddlcompany.DataValueField = "CompanyId";
            ddlcompany.DataBind();
            ddlcompany.Items.Insert(0,"--Select--");

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }

      
    }
   protected void rd_gradeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_gradeType.SelectedIndex==0 || rd_gradeType.SelectedIndex==1)
        {
            trgradePercentage.Visible = true;
            trgradevalue.Visible = false;
            txtgradevalue.Text = "";
        }
        if (rd_gradeType.SelectedIndex==2)
        {
            trgradevalue.Visible = true;
            trgradePercentage.Visible = false;
            txtgradeInPercentage.Text = "";
        }
    }


    #region save & Update
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnsubmit.Text=="Save")
            {
            
            #region save
          var checkdata = from p in HR.GradeMasterWithCompanyWiseTBs.Where(d => d.Company_ID ==Convert.ToInt32(ddlcompany.SelectedValue) && d.Grade ==Convert.ToInt32(ddlGrade.SelectedValue)) select p;
          if (checkdata.Count() > 0)
          {
            g.ShowMessage(this.Page, "This Details Already Exist");
         }
          else
          {
              GradeMasterWithCompanyWiseTB d = new GradeMasterWithCompanyWiseTB();
              d.Company_ID = Convert.ToInt32(ddlcompany.SelectedValue);
              d.Grade = Convert.ToInt32(ddlGrade.SelectedValue);
              d.Grade_Type = Convert.ToInt32(rd_gradeType.SelectedValue); //  gradeType 0=%of basic, 1=% of gross, 2= absolute value

              if (txtgradeInPercentage.Text !="")
              {
                d.Grade_In_Percentage = Convert.ToDecimal(txtgradeInPercentage.Text);
              }
              else
              {
                  d.Grade_In_Percentage = null;
              }
              if (txtgradevalue.Text != "")
              {
                  d.Grade_In_Value = Convert.ToDecimal(txtgradevalue.Text);
              }
              else
              {
                  d.Grade_In_Value = null;
              }
              d.Status = 0;
              d.User_Id = Convert.ToInt32(Session["UserId"]);

              HR.GradeMasterWithCompanyWiseTBs.InsertOnSubmit(d);
              HR.SubmitChanges();
              g.ShowMessage(this.Page, "Company Grade Details Saved Successfully");
              clearfields();
          }
           #endregion

            }
            else
            {
                 var checkdata = from p in HR.GradeMasterWithCompanyWiseTBs.Where(d => d.Company_ID ==Convert.ToInt32(ddlcompany.SelectedValue) && d.Grade ==Convert.ToInt32(ddlGrade.SelectedValue) && d.Grade_ID==Convert.ToInt32(lblEditid.Text)) select p;
                 if (checkdata.Count() > 0)
                 {
                     updatecode();
                    
                 }
                 else
                 {
                     var checkdata1 = from p in HR.GradeMasterWithCompanyWiseTBs.Where(d => d.Company_ID ==Convert.ToInt32(ddlcompany.SelectedValue) && d.Grade ==Convert.ToInt32(ddlGrade.SelectedValue) && d.Grade_ID !=Convert.ToInt32(lblEditid.Text)) select p;
                     if (checkdata1.Count() > 0)
                     {
                         g.ShowMessage(this.Page, "This Details Already Exist");
                     }
                     else
                     {
                        updatecode();
                     }
                    }
            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void updatecode()
    {
        try
        {
            GradeMasterWithCompanyWiseTB d = HR.GradeMasterWithCompanyWiseTBs.Where(s => s.Grade_ID == Convert.ToInt32(lblEditid.Text)).First();
            d.Company_ID = Convert.ToInt32(ddlcompany.SelectedValue);
            d.Grade = Convert.ToInt32(ddlGrade.SelectedValue);
            d.Grade_Type = Convert.ToInt32(rd_gradeType.SelectedValue);

            if (txtgradeInPercentage.Text != "")
            {
                d.Grade_In_Percentage = Convert.ToDecimal(txtgradeInPercentage.Text);
            }
            else
            {
                d.Grade_In_Percentage = null;
            }
            if (txtgradevalue.Text != "")
            {
                d.Grade_In_Value = Convert.ToDecimal(txtgradevalue.Text);
            }
            else
            {
                d.Grade_In_Value = null;
            }
            d.Status = 0;
            d.User_Id = Convert.ToInt32(Session["UserId"]);
            HR.SubmitChanges();
            g.ShowMessage(this.Page, "Company Grade Details Updated Successfully");
            clearfields();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void clearfields()
    {
        ddlGrade.SelectedIndex = 0;
        txtgradeInPercentage.Text = "";
        txtgradevalue.Text = "";
        rd_gradeType.SelectedIndex = 0;
        rd_status.SelectedIndex = 0;
        bindgrdofgrade();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";
        fillCompany();
    }
  #endregion
    protected void btncancel_Click(object sender, EventArgs e)
    {
        
        clearfields();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ImageButton Lnk = (ImageButton)sender;
        lblEditid.Text  = Lnk.CommandArgument;
       

        //int deptid = lbldeptid.Text;
        GradeMasterWithCompanyWiseTB mt = HR.GradeMasterWithCompanyWiseTBs.Where(s => s.Grade_ID == Convert.ToInt32(lblEditid.Text)).First();
       // fillCompany();
        ddlcompany.SelectedValue =Convert.ToInt32(mt.Company_ID).ToString();

        ddlGrade.SelectedValue = mt.Grade.ToString();
        if (mt.Grade_In_Percentage==null)
        {
            trgradePercentage.Visible = false;
        }
        else
        {
            trgradePercentage.Visible = true;
            trgradevalue.Visible = false;
            txtgradeInPercentage.Text = mt.Grade_In_Percentage.ToString();
        }

        if (mt.Grade_In_Value==null)
        {
            trgradevalue.Visible = false;
        }
        else
        {
            trgradevalue.Visible = true;
            trgradePercentage.Visible = false;
            txtgradevalue.Text = mt.Grade_In_Value.ToString();
        }
        rd_gradeType.SelectedIndex = Convert.ToInt32(mt.Grade_Type);
        rd_status.SelectedIndex = Convert.ToInt32(mt.Status);
        btnsubmit.Text = "Update";
    }
    protected void grd_companyGrad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_companyGrad.PageIndex = e.NewPageIndex;
        bindgrdofgrade();
    }

    int grade, inpgrade, invalugrad = 0;
  
    protected void txtgradeInPercentage_TextChanged(object sender, EventArgs e)
    {
        if (txtgradeInPercentage.Text != "")
        {
            inpgrade = Convert.ToInt32(txtgradeInPercentage.Text);
            if (inpgrade == 0)
            {
                txtgradeInPercentage.Text = "";
                g.ShowMessage(this.Page, "Grade Percentage Value Is Not Correct");
            }
            else
            {

            }
        }
    }
    protected void txtgradevalue_TextChanged(object sender, EventArgs e)
    {
        if (txtgradevalue.Text != "")
        {
            invalugrad = Convert.ToInt32(txtgradevalue.Text);
            if (invalugrad == 0)
            {
                txtgradevalue.Text = "";
                g.ShowMessage(this.Page, "Grade Value Is Not Correct");
            }
            else
            {

            }
        }
    }
}