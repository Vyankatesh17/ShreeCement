using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeEveryMonthFinalSetting : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable PaymentsData = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            fillcompany();
            fillYear();


        }
    }
    private void fillcompany()
    {
        ddlCompany.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));


        //var data = from dt in HR.CompanyInfoTBs
        //           where dt.Status == 0
        //           select dt;
        //if (data != null && data.Count() > 0)
        //{

        //    ddlCompany.DataSource = data;
        //    ddlCompany.DataTextField = "CompanyName";
        //    ddlCompany.DataValueField = "CompanyId";
        //    ddlCompany.DataBind();
        //}
        //else
        //{
        //    ddlCompany.DataSource = null;
        //    ddlCompany.DataBind();
        //}
        //ddlCompany.Items.Insert(0, "--Select--");
    }
    private void fillYear()
    {
        int i = int.Parse(DateTime.Now.AddYears(-1).Date.Year.ToString());

        DataTable dtadd = new DataTable();
        dtadd.Columns.Add("Year");

        for (int j = 0; j <= 10; j++)
        {
            DataRow dr = dtadd.NewRow();
            dr[0] = i.ToString();

            i++;
            dtadd.Rows.Add(dr);

        }

        ddyear.DataSource = dtadd;
        ddyear.DataTextField = "Year";
        ddyear.DataValueField = "Year";
        ddyear.DataBind();
        ddyear.Items.Insert(0, "--Select--");

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtamount.Text))
        {
            if (Convert.ToDecimal(txtamount.Text) > 0)
            {
                if (ViewState["PaymentsData"] != null)
                {

                    PaymentsData = (DataTable)ViewState["PaymentsData"];


                }
                else
                {
                    PaymentsData = new DataTable();
                    DataColumn Account = new DataColumn("Type"); PaymentsData.Columns.Add(Account);

                    DataColumn Remarks = new DataColumn("Componentname"); PaymentsData.Columns.Add(Remarks);
                    DataColumn Amount = new DataColumn("Amount"); PaymentsData.Columns.Add(Amount);
                    //lblremtotal.Text = txtAmount.Text;

                }
                //    hfSelectedValue.Value = ddltoaccount.Items[0].Value;
                //   SalaryComponentTB HTC = HR.SalaryComponentTBs.Where(d => d.ComponentName == txtcomponentname.Text).First();
                var dataexist = from d in HR.SalaryComponentTBs.Where(d => d.ComponentName == txtcomponentname.Text) select d;

                string Val = "";
                if (dataexist.Count() > 0)
                {
                    Val = dataexist.First().ComponentName;
                }
                else
                {
                    Val = txtcomponentname.Text;
                }
                DataRow[] DRExist = PaymentsData.Select("Componentname='" + Val + "'");

                if (DRExist.Length > 0)
                {
                    //DRExist[0][1] = Convert.ToDecimal(DRExist[0][1]) + Convert.ToDecimal(txtdivideamounts.Text);
                }
                else
                {
                    DataRow DR = PaymentsData.NewRow();
                    DR[0] = ddearded.SelectedItem.Text;
                    DR[1] = txtcomponentname.Text;
                    DR[2] = txtamount.Text;
                    PaymentsData.Rows.Add(DR);
                }
                ViewState["PaymentsData"] = PaymentsData;

                grdadd.DataSource = PaymentsData;
                grdadd.DataBind();
                txtamount.Text = "0";
                txtcomponentname.Text = null;
                ddearded.SelectedIndex = 0;
            }

        }
    }
    private void bindemp()
    {
        var dt = from p in HR.EmployeeTBs where p.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select new { p.EmployeeId, name = p.FName + ' ' + p.Lname };
        ddEmp.DataSource = dt;
        ddEmp.DataTextField = "name";
        ddEmp.DataValueField = "EmployeeId";
        ddEmp.DataBind();
        ddEmp.Items.Insert(0, "--Select--");
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            bindemp();
        }
    }
    protected void ddEmp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndele = (ImageButton)sender;
        PaymentsData = (DataTable)ViewState["PaymentsData"];
        foreach (DataRow d in PaymentsData.Rows)
        {
            if (d[1].ToString() == btndele.CommandArgument)
            {
                d.Delete();

                PaymentsData.AcceptChanges();
                break;
            }
        }
        grdadd.DataSource = PaymentsData;
        grdadd.DataBind();
    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndele = (ImageButton)sender;
        PaymentsData = (DataTable)ViewState["PaymentsData"];
        foreach (DataRow d in PaymentsData.Rows)
        {
            if (d[1].ToString() == btndele.CommandArgument)
            {

                ddearded.SelectedItem.Text = d[0].ToString();
                txtcomponentname.Text = d[1].ToString();
                txtamount.Text = d[2].ToString();
                d.Delete();

                PaymentsData.AcceptChanges();
                break;
            }
        }
        grdadd.DataSource = PaymentsData;
        grdadd.DataBind();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        var EmpSalaryProcessid =(from S in HR.SalaryProcessDetailsTBs
                where S.EmployeeId == Convert.ToInt32(ddEmp.SelectedValue)
                    && S.SalaryDate == Convert.ToDateTime("01/" + Convert.ToInt32(ddmonth.SelectedItem.Value) + "/" + Convert.ToInt32(ddyear.SelectedItem.Text))
                                  select new { S.EmpSalaryProcessid }).ToList().Distinct().First();
        int SalayrProID = Convert.ToInt32(EmpSalaryProcessid.EmpSalaryProcessid) ;
        for (int i = 0; i < grdadd.Rows.Count; i++)
        {
            SalaryProcessDetailsTB Sp = new SalaryProcessDetailsTB();
            Sp.SalaryDate = Convert.ToDateTime( "01/"+Convert.ToInt32(ddmonth.SelectedItem.Value) +"/"+ Convert.ToInt32(ddyear.SelectedItem.Text));
            Sp.EmployeeId = Convert.ToInt32(ddEmp.SelectedValue);
            Sp.EmpSalaryProcessid = SalayrProID;
            Sp.ComponentType = grdadd.Rows[i].Cells[0].Text;
            Sp.Componentid = grdadd.Rows[i].Cells[1].Text;
            Sp.amount = grdadd.Rows[i].Cells[2].Text;
            //Sp.Flag = true;
            Sp.MonthId = Convert.ToInt32(ddmonth.SelectedItem.Value);
            Sp.YearId = Convert.ToInt32(ddyear.SelectedItem.Text);
            HR.SalaryProcessDetailsTBs.InsertOnSubmit(Sp);
            HR.SubmitChanges();          
        }
        g.ShowMessage(this.Page, "Record Added");
        Clear();



    }

    private void Clear()
    {
        ddlCompany.SelectedIndex = 0;
        ddmonth.SelectedIndex = 0;
        ddearded.SelectedIndex = 0;
        txtcomponentname.Text = "";
        txtamount.Text = "0";
        ddEmp.SelectedIndex = 0;
        ddyear.SelectedIndex = 0;
        ViewState["PaymentsData"] = null;
        PaymentsData = null;
        grdadd.DataSource = null;
        grdadd.DataBind();
    }

    protected void ShowComponent_Click(object sender, EventArgs e)
    {

    }
}