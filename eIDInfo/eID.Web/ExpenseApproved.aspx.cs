using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AdvanceApproved : System.Web.UI.Page
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
                notapproved(); 
                rdpending_CheckedChanged(sender, e);
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    private void BindAllData()
    {
        DataSet dsall = g.ReturnData1("select EA.ExpensedetailID,FName + ' '+Lname 'Name',MA.ExpenseType,EA.ExpenseDate,EA.Amount,EA .Remarks,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeExpenseTB EA left outer join MasterExpenceTB MA on MA.ExpenseId=EA.ExpenseTypeID left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId order by EA.ExpenseDate desc ");
        grdadvance.DataSource = dsall.Tables[0];
        grdadvance.DataBind();
        lblcnt.Text = dsall.Tables[0].Rows.Count.ToString();

    }
    protected void rdpending_CheckedChanged(object sender, EventArgs e)
    {
        if (rdpending.SelectedIndex == 0)
        {
            notapproved();
        }
        else
        {
            approved();
        }
        //notapproved();
    }
    protected void rbApproved_CheckedChanged(object sender, EventArgs e)
    {

        approved();

    }
    protected void lnkApproved_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (sender) as LinkButton;
        string id = lnk.CommandArgument.ToString();
        ViewState["adid"] = id.ToString();
        DataSet dsall = g.ReturnData1("select top 1  EA.ExpenseApprovedId,E1.FName + ' '+E1.Lname 'Name',E.FName + ' '+E.Lname   'ApprovedName',ET.Amount,CONVERT(varchar, EA.Date,101) Date,EA.Amount 'Approvedamt',ME.ExpenseType,  EA.Remarks,EA.Status from   EmployeeExpenseApproveTB EA   left outer join EmployeeExpenseTB ET  on ET.ExpensedetailID=EA.ExpenseID      left outer join MasterExpenceTB ME  on ME.ExpenseId=ET.ExpenseTypeID    left outer join EmployeeTB E  on EA.EmployeeId=E.EmployeeId       left outer join EmployeeTB E1  on Et.EmployeeId=E1.EmployeeId where EA.ExpenseID='"+id+"' order by ExpenseApprovedId desc");
        dtprevdetails.DataSource = dsall.Tables[0];
        dtprevdetails.DataBind();

        dtprevdetails.Visible = true;
        string type =  ViewState["type"].ToString();

    

        mod.Show();
    }


    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //ImageButton lnkbtn = sender as ImageButton;
            //GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //string filePath = grd_Document.DataKeys[gvrow.RowIndex].Value.ToString();

            ImageButton Event = (ImageButton)sender;
            string filePath = Event.CommandArgument;

            //var Data = (from d in log.CustomerDocumentTBs
            //            select d);

            var data = from dt in HR.EmployeeExpenseTBs
                       where dt.ExpensedetailID == Convert.ToInt32(filePath)
                       select new { dt.AttachmentPath, dt.ExpensedetailID };
            foreach (var item in data)
            {
                lbup.Text = item.AttachmentPath;
            }

            string name = (string)Event.CommandArgument;
            System.IO.DirectoryInfo dinfo = new System.IO.DirectoryInfo(Server.MapPath("~/ExpenseDoc/"));

            if (System.IO.File.Exists(System.IO.Path.Combine(dinfo.FullName, lbup.Text)))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(System.IO.Path.Combine(dinfo.FullName, lbup.Text));
                //  they clicked on a file, download it
                //  to there PC
                this.Response.Clear();
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                this.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                this.Response.ContentType = "application/octet-stream";
                this.Response.WriteFile(fileInfo.FullName);
                this.Response.End();
            }


            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + lbup.Text);
            //Response.TransmitFile(Server.MapPath("~/WeeklyScanDOCAttachFile/" + lbup.Text));
            //Response.Flush();
            //Response.End();
        }
        catch (Exception ex)
        {

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','" + ex.Message + "');", true);
        }
    }

    public void approved()
    {

        // Approved Advance

        DataSet dscheck = g.ReturnData1("select DesigName from EmployeeTB E left outer join MasterDesgTB M on E.DesgId=M.DesigID where EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "'");

        if (dscheck.Tables[0].Rows[0]["DesigName"].ToString() == "Team Leader")
        {
            ViewState["type"] = "Team Leader";
        }
        else if (dscheck.Tables[0].Rows[0]["DesigName"].ToString() == "Operations Manager")
        {
            ViewState["type"] = "Operations Manager";
        }
        else
        {
            ViewState["type"] = "HR";
        }
        string type = ViewState["type"].ToString();
        string query = "";
        if (type == "Team Leader")
        {

            query = "select EA.ExpensedetailID,FName + ' '+Lname 'Name',MA.ExpenseType,EA.ExpenseDate,EA.Amount,EA .Remarks,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeExpenseTB EA left outer join MasterExpenceTB MA on MA.ExpenseId=EA.ExpenseTypeID left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId  where EA.DepartmentHeadStatus = 'Approved' order by EA.ExpenseDate desc";

        }
        if (type == "Head")
        {
            query = "select EA.ExpensedetailID,FName + ' '+Lname 'Name',MA.ExpenseType,EA.ExpenseDate,EA.Amount,EA .Remarks,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeExpenseTB EA left outer join MasterExpenceTB MA on MA.ExpenseId=EA.ExpenseTypeID left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId  where EA.ManagerStatus = 'Approved' and EA.DepartmentHeadStatus='Approved' order by EA.ExpenseDate desc ";
        }

        if (type == "HR")
        {
            query = "select EA.ExpensedetailID,FName + ' '+Lname 'Name',MA.ExpenseType,EA.ExpenseDate,EA.Amount,EA .Remarks,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeExpenseTB EA left outer join MasterExpenceTB MA on MA.ExpenseId=EA.ExpenseTypeID left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId  where EA.ManagerStatus = 'Approved' and EA.DepartmentHeadStatus='Approved' and EA.HRStatus='Approved' order by EA.ExpenseDate desc";
        }
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

        if (dscheck.Tables[0].Rows[0]["DesigName"].ToString() == "Team Leader")
        {
            ViewState["type"] = "Team Leader";

            var CompanyData = (from r in HR.EmployeeExpenseTBs
                               join gt in HR.EmployeeTBs on r.EmployeeID equals gt.EmployeeId
                               where r.ManagerStatus == "Pending" && r.DepartmentHeadStatus == "Pending" && r.HRStatus == "Pending" && EmpList.Contains(Convert.ToInt32(r.EmployeeID))
                               select new
                               {
                                   r.ExpensedetailID,
                                   r.ExpenseDate,
                                   r.Amount,
                                   r.Remarks,                                  
                                   Name = gt.FName + " " + gt.Lname,
                                   r.MasterExpenceTB.ExpenseType,
                                   //  Name = (from p in HR.EmployeeTBs where p.EmployeeId == r.employeeID  select p.FName + " " + p.MName + " " + p.Lname).First(),
                                   r.ManagerStatus,
                                   r.DepartmentHeadStatus,
                                   r.HRStatus

                               });


            // DataSet dspending = g.ReturnData1("select AdvanceId,FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId where EA.ManagerStatus = 'Pending' and EA.DepartmentHeadStatus='Pending' and EA.HRStatus='Pending' and Ea.Employeeid in (Select EmployeeId from EmployeeTB where ReportingStatus=" + ManagerId.ToString() + ")");



            grdadvance.DataSource = CompanyData;
            grdadvance.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();
        }
        else if (dscheck.Tables[0].Rows[0]["DesigName"].ToString() == "Operations Manager")
        {
            ViewState["type"] = "Operations Manager";
            var CompanyData = (from r in HR.EmployeeExpenseTBs
                               join gt in HR.EmployeeTBs on r.EmployeeID equals gt.EmployeeId
                               where r.DepartmentHeadStatus == "Approved" && r.ManagerStatus == "Pending" && r.HRStatus == "Pending" && EmpList.Contains(Convert.ToInt32(r.EmployeeID))
                               select new
                               {
                                   r.ExpensedetailID,
                                   r.ExpenseDate,
                                   r.Amount,
                                   r.Remarks,
                                   Name = gt.FName + " " + gt.Lname,
                                   r.MasterExpenceTB.ExpenseType,
                                   //  Name = (from p in HR.EmployeeTBs where p.EmployeeId == r.employeeID  select p.FName + " " + p.MName + " " + p.Lname).First(),
                                   r.ManagerStatus,
                                   r.DepartmentHeadStatus,
                                   r.HRStatus

                               });


            // DataSet dspending = g.ReturnData1("select AdvanceId,FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId where EA.ManagerStatus = 'Pending' and EA.DepartmentHeadStatus='Pending' and EA.HRStatus='Pending' and Ea.Employeeid in (Select EmployeeId from EmployeeTB where ReportingStatus=" + ManagerId.ToString() + ")");



            grdadvance.DataSource = CompanyData;
            grdadvance.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();
        }
        else
        {
            ViewState["type"] = "HR";
            var CompanyData = (from r in HR.EmployeeExpenseTBs
                               join gt in HR.EmployeeTBs on r.EmployeeID equals gt.EmployeeId                              
                               where r.ManagerStatus == "Approved" && r.DepartmentHeadStatus == "Approved" && r.HRStatus == "Pending"  //&& EmpList.Contains(Convert.ToInt32(r.EmployeeID))
                               select new
                               {
                                   r.ExpensedetailID,
                                   r.ExpenseDate,
                                   r.Amount,
                                   r.Remarks,
                                   Name = gt.FName + " " + gt.Lname,
                                   r.MasterExpenceTB.ExpenseType,
                                //  ExpenseType =  dt.ExpenseType,
                                   //  Name = (from p in HR.EmployeeTBs where p.EmployeeId == r.employeeID  select p.FName + " " + p.MName + " " + p.Lname).First(),
                                   r.ManagerStatus,
                                   r.DepartmentHeadStatus,
                                   r.HRStatus

                               });


            // DataSet dspending = g.ReturnData1("select AdvanceId,FName + ' '+Lname 'Name',EA.EnterDate,EA.Amount,EA.Remarks,EA.DueDate,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeAdvanceTB EA left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId where EA.ManagerStatus = 'Pending' and EA.DepartmentHeadStatus='Pending' and EA.HRStatus='Pending' and Ea.Employeeid in (Select EmployeeId from EmployeeTB where ReportingStatus=" + ManagerId.ToString() + ")");



            grdadvance.DataSource = CompanyData;
            grdadvance.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();
        }
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
        DataSet dscheck = g.ReturnData1("select Amount from EmployeeExpenseTB where ExpensedetailID='" + ViewState["adid"] + "'");

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

   
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        EmployeeExpenseTB EA = HR.EmployeeExpenseTBs.Where(d => d.ExpensedetailID == Convert.ToInt32(ViewState["adid"])).First();

        string empid = "";
        DataSet dsemp = g.ReturnData1("select EmployeeId from EmployeeExpenseTB where ExpensedetailID=" + EA.ExpensedetailID + "");
        if (dsemp.Tables[0].Rows.Count > 0)
        {
            empid = dsemp.Tables[0].Rows[0]["EmployeeId"].ToString();
        }


        string type = ViewState["type"].ToString();
        if (type == "Team Leader")
        {
            EA.DepartmentHeadStatus = "Approved";
            HR.SubmitChanges();

            EmployeeExpenseApproveTB EAD = new EmployeeExpenseApproveTB();
            EAD.ExpenseID = EA.ExpensedetailID;
            EAD.EmployeeId = Convert.ToInt32(Session["UserId"]);
            EAD.Amount = Convert.ToDecimal(txtamount.Text);
            EAD.Date = Convert.ToDateTime(txtDate.Text);
            EAD.DueDate = Convert.ToDateTime(txtduedate.Text);
            EAD.Remarks = txtremarks.Text;
            EAD.UserType = "Team Leader";
            EAD.Status = "Approved";
            HR.EmployeeExpenseApproveTBs.InsertOnSubmit(EAD);
            HR.SubmitChanges();


        }
        if (type == "Operations Manager")
        {
            EA.ManagerStatus = "Approved";
            EA.DepartmentHeadStatus = "Approved";
            HR.SubmitChanges();


            EmployeeExpenseApproveTB EAD = new EmployeeExpenseApproveTB();
            EAD.ExpenseID = EA.ExpensedetailID;
            EAD.EmployeeId = Convert.ToInt32(Session["UserId"]);
            EAD.Amount = Convert.ToDecimal(txtamount.Text);
            EAD.Date = Convert.ToDateTime(txtDate.Text);
            EAD.DueDate = Convert.ToDateTime(txtduedate.Text);
            EAD.Remarks = txtremarks.Text;
            EAD.UserType = "Operations Manager";
            EAD.Status = "Approved";
            HR.EmployeeExpenseApproveTBs.InsertOnSubmit(EAD);
            HR.SubmitChanges();
        }

        if (type == "HR")
        {
            EA.ManagerStatus = "Approved";
            EA.DepartmentHeadStatus = "Approved";
            EA.HRStatus = "Approved";
            HR.SubmitChanges();


            EmployeeExpenseApproveTB EAD = new EmployeeExpenseApproveTB();
            EAD.ExpenseID = EA.ExpensedetailID;
            EAD.EmployeeId = Convert.ToInt32(Session["UserId"]);
            EAD.Amount = Convert.ToDecimal(txtamount.Text);
            EAD.Date = Convert.ToDateTime(txtDate.Text);
            EAD.DueDate = Convert.ToDateTime(txtduedate.Text);
            EAD.Remarks = txtremarks.Text;
            EAD.UserType = "HR";
            EAD.Status = "Approved";
            HR.EmployeeExpenseApproveTBs.InsertOnSubmit(EAD);
            HR.SubmitChanges();


            var datasplit = txtduedate.Text.Split('/');
            string MM = datasplit[0].ToString();
            string dd= datasplit[1].ToString();
            string YY = datasplit[2].ToString();

            EmployeeExpenseFinalTB EEF = new EmployeeExpenseFinalTB();
         
            EEF.EmployeeId = Convert.ToInt32(empid);
            EEF.Amount = Convert.ToDecimal(txtamount.Text);
            EEF.Date = Convert.ToDateTime(txtduedate.Text);
            EEF.MonthiD = Convert.ToInt32(MM);
            EEF.Year = Convert.ToInt32(YY);

            EEF.Status = "Pending";
            HR.EmployeeExpenseFinalTBs.InsertOnSubmit(EEF);
            HR.SubmitChanges();
          

        }



        clear();

    }
}