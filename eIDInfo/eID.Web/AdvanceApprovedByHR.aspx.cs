using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AdvanceApprovedByHR : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable dtaddadvance;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
                txtduedate.Text = g.GetCurrentDateTime().ToShortDateString();
                MultiView1.ActiveViewIndex = 0;
                notapproved();
                fillYear(); rdpending_CheckedChanged(sender,e);
                dtaddadvance = new DataTable();
                DataColumn mid = dtaddadvance.Columns.Add("mid");
                DataColumn mname = dtaddadvance.Columns.Add("mname");
                DataColumn year = dtaddadvance.Columns.Add("year");
                DataColumn amount = dtaddadvance.Columns.Add("amount");

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindAllData()
    {
        DataSet dsall = g.ReturnData1("select AdvanceId,FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId order by EA.EnterDate desc");
        grdadvance.DataSource = dsall.Tables[0];
        grdadvance.DataBind();
        lblcnt.Text = dsall.Tables[0].Rows.Count.ToString();


    }
    protected void rdpending_CheckedChanged(object sender, EventArgs e)
    {
        if (rdpending.SelectedIndex==0)
        {
            notapproved();
        }
        else
        {
            approved();
        }
       
    }
    protected void rbApproved_CheckedChanged(object sender, EventArgs e)
    {

        

    }
    protected void lnkApproved_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (sender) as LinkButton;
        string id = lnk.CommandArgument.ToString();
        ViewState["adid"] = id.ToString();
        DataSet dsall = g.ReturnData1("select top 1  EA.AdvanceId,Et.DeductionMonths,E1.FName + ' '+E1.Lname 'Name',E.FName + ' '+E.Lname 'ApprovedName',ET.Amount,CONVERT(varchar, EA.Date,101) Date,EA.Amount 'Approvedamt',EA.Remarks,CONVERT(varchar, ET.DueDate,101) DueDate,EA.Status from EmployeeAdvanceApproveTB EA  left outer join EmployeeAdvanceTB ET  on ET.AdvanceId=EA.AdvanceId left outer join EmployeeTB E  on EA.EmployeeId=E.EmployeeId    left outer join EmployeeTB E1  on Et.EmployeeId=E1.EmployeeId where EA.Advanceid=" + id + " order by AdvanceApprovedId desc");
        dtprevdetails.DataSource = dsall.Tables[0];
        dtprevdetails.DataBind();

        string type = ViewState["type"].ToString();
        t1.Visible = false;
        if (type == "HR")
        {
            //DataSet dsmonth = g.ReturnData1("select monthname from Monthtb");
            //grdpayment.DataSource = dsmonth.Tables[0];
            //grdpayment.DataBind();
            ViewState["dtadvance"] = null;
            grdpayment.DataSource = null;
            grdpayment.DataBind();
            t1.Visible = true;
        }

        mod.Show();
    }

    public void approved()
    {

        // Approved Advance

        DataSet dscheck = g.ReturnData1("select DesigName from EmployeeTB E left outer join MasterDesgTB M on E.DesgId=M.DesigID where EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "'");

      
            ViewState["type"] = "HR";
    
        string type = ViewState["type"].ToString();
        string query = "";
       
            query = "select AdvanceId,FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId where EA.ManagerStatus = 'Approved' and EA.DepartmentHeadStatus='Approved' and EA.HRStatus='Approved' order by EA.EnterDate desc";
       
        DataSet dsapproved = g.ReturnData1(query);

        grdadvance.DataSource = dsapproved.Tables[0];
        grdadvance.DataBind();
        lblcnt.Text = dsapproved.Tables[0].Rows.Count.ToString();

        for (int i = 0; i < grdadvance.Rows.Count; i++)
        {
            LinkButton lblEdit1 = (LinkButton)grdadvance.Rows[i].FindControl("lnkApproved");
            lblEdit1.Enabled = false;


        }

    }
    public void notapproved()
    {

        // Not Approved Advance
        int EmpId12 = Convert.ToInt32(Session["UserId"]);


        // For Getting Department Wise  Manager

        List<int> EmpList = new List<int>();

        //int dept1 = Convert.ToInt32(empData1.Value.ToString());
        int ManagerId = Convert.ToInt32(Session["UserId"]);

    SHankar:


        DataSet ds = g.ReturnData1(" select e1.EmployeeId,e2.EmployeeId  from EmployeeTB e1 , EmployeeTB e2 where e1.EmployeeId=e2.ReportingStatus ");


        DataSet ds1 = g.ReturnData1("Select EmployeeId,ReportingStatus from EmployeeTB where ReportingStatus=" + ManagerId.ToString());
        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
        {
            EmpList.Add(Convert.ToInt32(ds1.Tables[0].Rows[i][0]));
        }
        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
        {

            DataSet dst = g.ReturnData1("Select EmployeeId,ReportingStatus from EmployeeTB where ReportingStatus=" + ds1.Tables[0].Rows[i][0].ToString());
            ManagerId = Convert.ToInt32(ds1.Tables[0].Rows[i][0]);
            goto SHankar;
        }



        DataSet dscheck = g.ReturnData1("select DesigName from EmployeeTB E left outer join MasterDesgTB M on E.DesgId=M.DesigID where EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "'");

     
            ViewState["type"] = "HR";
            var CompanyData = (from r in HR.EmployeeAdvanceTBs
                               join gt in HR.EmployeeTBs on r.EmployeeId equals gt.EmployeeId
                               where r.ManagerStatus == "Approved" && r.DepartmentHeadStatus == "Approved" && r.HRStatus == "Pending" // && EmpList.Contains(Convert.ToInt32(r.EmployeeId))
                               select new
                               {
                                   r.AdvanceId,
                                   r.EnterDate,
                                   r.Amount,
                                   r.Remarks,
                                   r.DueDate,
                                   Name = gt.FName + " " + gt.Lname,
                                   //  Name = (from p in HR.EmployeeTBs where p.EmployeeId == r.employeeID  select p.FName + " " + p.MName + " " + p.Lname).First(),
                                   r.ManagerStatus,
                                   r.DepartmentHeadStatus,
                                   r.HRStatus

                               });


            // DataSet dspending = g.ReturnData1("select AdvanceId,FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId where EA.ManagerStatus = 'Pending' and EA.DepartmentHeadStatus='Pending' and EA.HRStatus='Pending' and Ea.Employeeid in (Select EmployeeId from EmployeeTB where ReportingStatus=" + ManagerId.ToString() + ")");



            grdadvance.DataSource = CompanyData;
            grdadvance.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();
        
        for (int i = 0; i < grdadvance.Rows.Count; i++)
        {
            LinkButton lblEdit1 = (LinkButton)grdadvance.Rows[i].FindControl("lnkApproved");
            lblEdit1.Enabled = true;


        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();

    }
    private void fillYear()
    {
        //int i = int.Parse(DateTime.Now.AddYears(0).Date.Year.ToString());
        //for (int j = 0; j <= 50; j++)
        //{

        //    ddlYears.Items.Add(new ListItem(i.ToString(), string.Empty));
        //    i++;

        //}

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {


        if (ViewState["dtadvance"] != null)
        {
            dtaddadvance = (DataTable)ViewState["dtadvance"];
            DataRow dr = dtaddadvance.NewRow();

            //dr[0] = ddlMonths.SelectedValue;
            //dr[1] = ddlMonths.SelectedItem.Text;
            //dr[2] = ddlYears.SelectedItem.Text;
            //dr[3] = txtreceived.Text;


            dtaddadvance.Rows.Add(dr);

        }
        else
        {

            dtaddadvance = new DataTable();
            DataColumn mid = dtaddadvance.Columns.Add("mid");
            DataColumn mname = dtaddadvance.Columns.Add("mname");
            DataColumn year = dtaddadvance.Columns.Add("year");
            DataColumn amount = dtaddadvance.Columns.Add("amount");


            DataRow dr = dtaddadvance.NewRow();

            //dr[0] = ddlMonths.SelectedValue;
            //dr[1] = ddlMonths.SelectedItem.Text;
            //dr[2] = ddlYears.SelectedItem.Text;
            //dr[3] = txtreceived.Text;


            dtaddadvance.Rows.Add(dr);
        }


        grdpayment.DataSource = dtaddadvance;
        grdpayment.DataBind();
       
        ViewState["dtadvance"] = dtaddadvance;

        mod.Show();
    }
    private void clear()
    {
        txtamount.Text = "";
        txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
        txtduedate.Text = g.GetCurrentDateTime().ToShortDateString();
        txtremarks.Text = "";
        approved();
        notapproved();
        mod.Hide();

    }

    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        DataSet dscheck = g.ReturnData1("select Amount from EmployeeAdvanceTB where AdvanceId='" + ViewState["adid"] + "'");

        if (dscheck.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToDecimal(txtamount.Text) > Convert.ToDecimal(dscheck.Tables[0].Rows[0]["Amount"].ToString()))
            {
                g.ShowMessage(this.Page, "Please Enter correct Amount");
                txtamount.Text = dscheck.Tables[0].Rows[0]["Amount"].ToString();
            }
            else
            {

            }
        }
        mod.Show();
    }

    protected void On_Click_QtyCal(object sender, EventArgs e)
    {
        decimal total = 0;
        for (int i = 0; i < grdpayment.Rows.Count; i++)
        {
             TextBox txtr = (TextBox)grdpayment.Rows[i].FindControl("txtreceived");

         ///   string txtr = grdpayment.Rows[i].Cells[3].Text;
            total = total + Convert.ToDecimal(txtr.Text);
        }

        DataSet dscheck = g.ReturnData1("select Amount from EmployeeAdvanceTB where AdvanceId='" + ViewState["adid"] + "'");

        if (dscheck.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToDecimal(total) > Convert.ToDecimal(dscheck.Tables[0].Rows[0]["Amount"].ToString()))
            {
                g.ShowMessage(this.Page, "Please Enter correct Amount");
                //txtamount.Text = dscheck.Tables[0].Rows[0]["Amount"].ToString();
                for (int i = 0; i < grdpayment.Rows.Count; i++)
                {
                    TextBox txtr = (TextBox)grdpayment.Rows[i].FindControl("txtreceived");
                    txtr.Text = "0";
                }
            }
            else
            {

            }
        }
        mod.Show();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        EmployeeAdvanceTB EA = HR.EmployeeAdvanceTBs.Where(d => d.AdvanceId == Convert.ToInt32(ViewState["adid"])).First();

        string empid = "";
        DataSet dsemp = g.ReturnData1("select EmployeeId from EmployeeAdvanceTB where AdvanceId=" + EA.AdvanceId + "");
        if (dsemp.Tables[0].Rows.Count > 0)
        {
            empid = dsemp.Tables[0].Rows[0]["EmployeeId"].ToString();
        }


        string type = ViewState["type"].ToString();
       
            EA.ManagerStatus = "Approved";
            EA.DepartmentHeadStatus = "Approved";
            EA.HRStatus = "Approved";
            HR.SubmitChanges();


            EmployeeAdvanceApproveTB EAD = new EmployeeAdvanceApproveTB();
            EAD.AdvanceId = EA.AdvanceId;
            EAD.EmployeeId = Convert.ToInt32(Session["UserId"]);
            EAD.Amount = Convert.ToDecimal(txtamount.Text);
            EAD.Date = Convert.ToDateTime(txtDate.Text);
            EAD.DeductionMonths = Convert.ToInt32(txtdeduction.Text);
            EAD.DueDate = Convert.ToDateTime(txtduedate.Text);
            EAD.Remarks = txtremarks.Text;
            EAD.UserType = "HR";
            EAD.Status = "Approved";
            HR.EmployeeAdvanceApproveTBs.InsertOnSubmit(EAD);
            HR.SubmitChanges();

            for (int i = 0; i < grdpayment.Rows.Count; i++)
            {
                EmployeeAdvanceSlotTB GB = new EmployeeAdvanceSlotTB();
                GB.AdvanceId = EA.AdvanceId;
                GB.EmployeeId = Convert.ToInt32(empid);

                 TextBox txtamt = (TextBox)grdpayment.Rows[i].FindControl("txtreceived");



                GB.FinalRemark = txtremarks.Text;
                GB.ApprovedAmount = Convert.ToDecimal(txtamount.Text);

                GB.Monthid = Convert.ToInt32(grdpayment.Rows[i].Cells[0].Text);
                GB.Year = Convert.ToInt32(grdpayment.Rows[i].Cells[2].Text);
                GB.Month = Convert.ToString(grdpayment.Rows[i].Cells[1].Text);
                GB.MonthPay = Convert.ToDecimal(txtamt.Text);
                GB.Status = "Pending";
                if (GB.MonthPay > 0)
                {
                    HR.EmployeeAdvanceSlotTBs.InsertOnSubmit(GB);
                    HR.SubmitChanges();
                }           

        }



        clear();

    }

    protected void txtdeduction_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int count = Convert.ToInt32(txtdeduction.Text);
            DataSet dspay = g.ReturnData1("select Monthid,MonthName from MonthTB where Monthid > DATEPART(MM,GETDATE())");
            if (dspay.Tables[0].Rows.Count > 0)
            {

                count = count - dspay.Tables[0].Rows.Count;

              //  DataSet dscheck = g.ReturnData1("select Monthid mid,MonthName mname,(DATEPART(YYYY,getdate())) year," + Math.Round(Convert.ToDecimal(txtamount.Text) / Convert.ToDecimal(txtdeduction.Text)).ToString() + " amount from MonthTB where Monthid > DATEPART(MM,GETDATE()) union all  select Monthid mid ,MonthName mname,(DATEPART(YYYY,getdate())+1) year," + Math.Round(Convert.ToDecimal(txtamount.Text) / Convert.ToDecimal(txtdeduction.Text)).ToString() + " amount from MonthTB where Monthid <= '" + count + "'");
                DataSet dscheck = g.ReturnData1("select * from (select ROW_NUMBER() OVER (ORDER BY Monthid) AS ID1, Monthid mid,MonthName mname,(DATEPART(YYYY,getdate())) year," + Math.Round(Convert.ToDecimal(txtamount.Text) / Convert.ToDecimal(txtdeduction.Text)).ToString() + " amount from MonthTB where Monthid > DATEPART(MM,GETDATE()) union all  select ROW_NUMBER() OVER (ORDER BY Monthid) AS ID1, Monthid mid ,MonthName mname,(DATEPART(YYYY,getdate())+1) year," + Math.Round(Convert.ToDecimal(txtamount.Text) / Convert.ToDecimal(txtdeduction.Text)).ToString() + " amount from MonthTB where Monthid <= '" + count + "' ) t where t.id1 < = '" + txtdeduction.Text + "'");
                grdpayment.DataSource = dscheck.Tables[0];
                grdpayment.DataBind();
                lblcnt.Text = dscheck.Tables[0].Rows.Count.ToString();
                mod.Show();
            }
        }
        catch
        {

        }
    }
}