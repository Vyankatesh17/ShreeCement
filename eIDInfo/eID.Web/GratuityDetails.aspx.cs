using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GratuityDetails : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable DTAddDetails = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
           // Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
              

                bindcompany();

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
   
    public void bindcompany()
    {
      
        var data = (from dt in HR.CompanyInfoTBs
                    where dt.Status == 0
                    select dt).OrderBy(d => d.CompanyName);
        if (data != null && data.Count() > 0)
        {

            ddlcompany.DataSource = data;
            ddlcompany.DataTextField = "CompanyName";
            ddlcompany.DataValueField = "CompanyId";
            ddlcompany.DataBind();
            ddlcompany.Items.Insert(0, "--Select--");
        }
    }
    protected void grd_Gratuity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Gratuity.PageIndex = e.NewPageIndex;
        DTAddDetails = (DataTable)ViewState["DTAddDetails"];
        grd_Gratuity.DataSource = DTAddDetails;
        grd_Gratuity.DataBind();

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }

    private void bindgrid()
    {
        DataTable Dtemp = g.ReturnData("Select FName+' '+Lname as EmpName,EmployeeId,Convert(varchar,DOJ,101) as DOJ,datepart(yyyy,DOJ) as year,datepart(mm,DOJ) as month,((datepart(yyyy,getdate()) - datepart(yyyy,DOJ)) * 12) +datepart(mm,getdate())  - datepart(mm,DOJ) as Monthcount from EmployeeTB where RelivingStatus is null and CompanyId='" + Convert.ToInt32(ddlcompany.SelectedValue) + "'");
        ViewState["DTAddDetails"] = null;
        if (Dtemp.Rows.Count>0)
        {
            for (int i = 0; i < Dtemp.Rows.Count; i++)
            {

                if (ViewState["DTAddDetails"] != null)
                {
                    DTAddDetails = (DataTable)ViewState["DTAddDetails"];
                }
                else
                {
                    DataColumn EmpName = DTAddDetails.Columns.Add("EmpName");
                    DataColumn year = DTAddDetails.Columns.Add("year");
                    DataColumn month = DTAddDetails.Columns.Add("Monthcount");
                    DataColumn DayBasic = DTAddDetails.Columns.Add("DayBasic");
                    DataColumn GratuityAmt = DTAddDetails.Columns.Add("GratuityAmt");
                    DataColumn DOJ = DTAddDetails.Columns.Add("DOJ");
                }
               
                DataTable dtbasicamt = g.ReturnData("select top 1 amount From SalaryProcessDetailsTB where EmployeeId='" + Convert.ToInt32(Dtemp.Rows[i]["EmployeeId"].ToString()) + "' and Componentid='Basic' order by Salaryid desc");
                if (dtbasicamt.Rows.Count>0)
                {
                    lblbasicAmt.Text = dtbasicamt.Rows[0]["amount"].ToString();
                    if (lblbasicAmt.Text == "")
                    {
                        lblbasicAmt.Text = "0";
                    }
                }
                else
                {
                    lblbasicAmt.Text = "0";
                }
                decimal basicAmtup = (Convert.ToDecimal(lblbasicAmt.Text) * 60) / 100;
                float dtmonthcount = float.Parse(Dtemp.Rows[i]["Monthcount"].ToString());
                float dtyear = dtmonthcount / 12;
                decimal year1 = Math.Round(Convert.ToDecimal(dtyear), 2);
                DataTable dtcheck = g.ReturnData("select *from GratuityTB where '" + dtyear + "' between FromYears and ToYears");
                
                //var dtbasicday = from dt in HR.GratuityTBs
                //                 where dt.FromYears <= dtyear && dt.ToYears >= dtyear
                //                 select new { dt.BasicDays};
                if (dtcheck.Rows.Count > 0)
                {
                    
                    lblbasicday.Text = dtcheck.Rows[0]["BasicDays"].ToString();
                    
                }
                else
                {
                    lblbasicday.Text = "0";
                }

                float parDaybasic = (float.Parse(Convert.ToString(basicAmtup)) * 12) / 360;
                float gratuityAmt = parDaybasic * (float.Parse(lblbasicday.Text) * float.Parse(Convert.ToString(year1)));

                DataRow dr = DTAddDetails.NewRow();
                dr[0] = Dtemp.Rows[i]["EmpName"].ToString();
                dr[1] = year1;
                dr[2] = Dtemp.Rows[i]["Monthcount"].ToString();
                dr[3] = lblbasicday.Text;
                dr[4] = gratuityAmt;
                dr[5] = Dtemp.Rows[i]["DOJ"].ToString();
                DTAddDetails.Rows.Add(dr);
                ViewState["DTAddDetails"] = DTAddDetails;
                
            }
            grd_Gratuity.DataSource = DTAddDetails;
            grd_Gratuity.DataBind();
            lblcnt.Text = DTAddDetails.Rows.Count.ToString();
        }
        else
        {
            grd_Gratuity.DataSource = null;
            grd_Gratuity.DataBind();
            lblcnt.Text = "0";
            ViewState["DTAddDetails"] = null;
        }

    }
}