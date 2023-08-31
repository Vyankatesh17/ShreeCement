using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ClearanceCerticateDepartmentWise : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable Dt = new DataTable();
    int totalleaves;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!Page.IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;

                BindAllData();

                fillcompany();          
            
            }
        }
        else
        {


        }
    }

    private void BindAllData()
    {
        DataSet dsalldata = g.ReturnData1("select ClearCertificatemasterID,FName+' '+Lname 'Name',EnterDate,DepartmentName from ClearCertificateMasterTB CM  left outer join EmployeeTB ET on ET.EmployeeId=CM.EmployeeID order by EnterDate desc");

        if (dsalldata.Tables[0].Rows.Count > 0)
        {
            grdalldata.DataSource = dsalldata.Tables[0];
            grdalldata.DataBind();
            lblcnt.Text = dsalldata.Tables[0].Rows.Count.ToString();
        }
    }
    private void fillcompany()
    {
        var data = from dt in HR.CompanyInfoTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
    }
    private void bindemp()
    {
        var dt = from p in HR.EmployeeTBs where p.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select new { p.EmployeeId, name = p.FName + ' ' + p.MName + ' ' + p.Lname };
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
    protected void dddepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dddepartment.SelectedItem.Value == "Department Head")
        {
            DataSet dsdepartment = g.ReturnData1("select 'Current Pending Task' A union all  select 'Hand Over' A union all select 'Documents' A union all select 'Files(Soft & Hard)' A  ");
            grdheaddepartment.DataSource = dsdepartment.Tables[0];
            grdheaddepartment.DataBind();
            lblcnt.Text = dsdepartment.Tables[0].Rows.Count.ToString();
        }
        else if (dddepartment.SelectedItem.Value == "Accounts & Finance")
        {
            DataSet dsdepartment = g.ReturnData1("select 'Company Loan' A union all  select 'Advance' A union all  select 'Finance Liabilities' A union all select 'Bill Liability' A ");
            grdheaddepartment.DataSource = dsdepartment.Tables[0];
            grdheaddepartment.DataBind();
            lblcnt.Text = dsdepartment.Tables[0].Rows.Count.ToString();
        }
        else if (dddepartment.SelectedItem.Value == "Admin")
        {
            DataSet dsdepartment = g.ReturnData1("select 'Stationary' A union all  select 'Files' A union all select 'ID Card Access card ,(Other Assets)' A union all select 'Sim Card , Visiting Card' A union all select 'Dongle , Laptop' ");
            grdheaddepartment.DataSource = dsdepartment.Tables[0];
            grdheaddepartment.DataBind();
            lblcnt.Text = dsdepartment.Tables[0].Rows.Count.ToString();
        }

        else if (dddepartment.SelectedItem.Value == "IT")
        {
            DataSet dsdepartment = g.ReturnData1("select 'Laptop & Accessories' A union all  select 'Mobile & Accessories' A union all select 'Login ID' A union all select 'Pen Drive' A union all select 'CDs & Floppies'");
            grdheaddepartment.DataSource = dsdepartment.Tables[0];
            grdheaddepartment.DataBind();
            lblcnt.Text = dsdepartment.Tables[0].Rows.Count.ToString();
        }
        else if (dddepartment.SelectedItem.Value == "HR")
        {
            DataSet dsdepartment = g.ReturnData1("select 'Salary' A union all  select 'Documents' A union all select 'Forms' A union all select 'Miscellanious' A ");
            grdheaddepartment.DataSource = dsdepartment.Tables[0];
            grdheaddepartment.DataBind();
            lblcnt.Text = dsdepartment.Tables[0].Rows.Count.ToString();
        }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (btnSubmit.Text == "Submit")
        {
            ClearCertificateMasterTB CTM = new ClearCertificateMasterTB();
            CTM.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
            CTM.EmployeeID = Convert.ToInt32(ddEmp.SelectedValue);
            CTM.DepartmentName = dddepartment.SelectedItem.Text;
            CTM.EnterDate = DateTime.Now;
            
            CTM.UserID = Convert.ToInt32(Session["UserId"]);
            HR.ClearCertificateMasterTBs.InsertOnSubmit(CTM);
            HR.SubmitChanges();

            for (int ii = 0; ii < grdheaddepartment.Rows.Count; ii++)
            {
                string description = grdheaddepartment.Rows[ii].Cells[0].Text;
                DropDownList status = (DropDownList)grdheaddepartment.Rows[ii].FindControl("ddstatus");
                TextBox comments = (TextBox)grdheaddepartment.Rows[ii].FindControl("txtcomments");

                ClearanceCertificateDetailsTB CT = new ClearanceCertificateDetailsTB();

                CT.ClearMasterID = CTM.ClearCertificatemasterID;
                CT.DepartmentName = dddepartment.SelectedItem.Text;

                CT.Description = description.ToString();
                CT.Comments = comments.Text.ToString();
                CT.FinalApprovedStatus = status.SelectedItem.Text;
                CT.UserId = Convert.ToInt32(Session["UserId"]);
                HR.ClearanceCertificateDetailsTBs.InsertOnSubmit(CT);
                HR.SubmitChanges();

                var dataexist = from d in HR.ClearanceCertificateFinalTBs where d.EmployeeID == CTM.EmployeeID select d;
                if (dataexist.Count() == 0)
                {
                    ClearanceCertificateFinalTB CCT = new ClearanceCertificateFinalTB();

                    CCT.EmployeeID = CTM.EmployeeID;
                    CCT.CompanyID = CTM.CompanyID;

                    if (dddepartment.SelectedItem.Text == "Department Head")
                    {
                        CCT.DepartmentHeadclearnaceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.DeparmentAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.DeparmentAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Accounts & Finance")
                    {
                        CCT.AccountsclearanceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AccountsAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AccountsAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Admin")
                    {
                        CCT.AdminClearnaceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AdminAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AdminAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "IT")
                    {
                        CCT.ITClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.ITAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.ITAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "HR")
                    {
                        CCT.HRClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.HRAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.HRAllStatus = "Approved";
                        }

                    }
                a:
                    HR.ClearanceCertificateFinalTBs.InsertOnSubmit(CCT);
                    HR.SubmitChanges();

                }
                else
                {
                    ClearanceCertificateFinalTB CCT = HR.ClearanceCertificateFinalTBs.Where(d => d.EmployeeID == CTM.EmployeeID).First();

                    CCT.EmployeeID = CTM.EmployeeID;
                    CCT.CompanyID = CTM.CompanyID;

                    if (dddepartment.SelectedItem.Text == "Department Head")
                    {
                        CCT.DepartmentHeadclearnaceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.DeparmentAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.DeparmentAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Accounts & Finance")
                    {
                        CCT.AccountsclearanceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AccountsAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AccountsAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Admin")
                    {
                        CCT.AdminClearnaceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AdminAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AdminAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "IT")
                    {
                        CCT.ITClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.ITAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.ITAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "HR")
                    {
                        CCT.HRClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.HRAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.HRAllStatus = "Approved";
                        }

                    }
                a:
                   
                    HR.SubmitChanges();
                }


            }
            g.ShowMessage(this.Page, "Record Inserted"); clear();


        }
        else
        {
            ClearCertificateMasterTB CTM = HR.ClearCertificateMasterTBs.Where(d => d.ClearCertificatemasterID == Convert.ToInt32(lblclearid.Text)).First();
            CTM.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
            CTM.EmployeeID = Convert.ToInt32(ddEmp.SelectedValue);
            CTM.DepartmentName = dddepartment.SelectedItem.Text;
            CTM.EnterDate = DateTime.Now;

            CTM.UserID = Convert.ToInt32(Session["UserId"]);

            HR.SubmitChanges();

            var deletedata = from d in HR.ClearanceCertificateDetailsTBs where d.ClearMasterID == CTM.ClearCertificatemasterID select d;
            HR.ClearanceCertificateDetailsTBs.DeleteAllOnSubmit(deletedata);
            HR.SubmitChanges();

            for (int ii = 0; ii < grdheaddepartment.Rows.Count; ii++)
            {
                string description = grdheaddepartment.Rows[ii].Cells[0].Text;
                DropDownList status = (DropDownList)grdheaddepartment.Rows[ii].FindControl("ddstatus");
                TextBox comments = (TextBox)grdheaddepartment.Rows[ii].FindControl("txtcomments");

                ClearanceCertificateDetailsTB CT = new ClearanceCertificateDetailsTB();

                CT.ClearMasterID = CTM.ClearCertificatemasterID;
                CT.DepartmentName = dddepartment.SelectedItem.Text;

                CT.Description = description.ToString();
                CT.Comments = comments.Text.ToString();
                CT.FinalApprovedStatus = status.SelectedItem.Text;
                CT.UserId = Convert.ToInt32(Session["UserId"]);
                HR.ClearanceCertificateDetailsTBs.InsertOnSubmit(CT);
                HR.SubmitChanges();


                var dataexist = from d in HR.ClearanceCertificateFinalTBs where d.EmployeeID == CTM.EmployeeID select d;
                if (dataexist.Count() == 0)
                {
                    ClearanceCertificateFinalTB CCT = new ClearanceCertificateFinalTB();

                    CCT.EmployeeID = CTM.EmployeeID;
                    CCT.CompanyID = CTM.CompanyID;

                    if (dddepartment.SelectedItem.Text == "Department Head")
                    {
                        CCT.DepartmentHeadclearnaceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.DeparmentAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.DeparmentAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Accounts & Finance")
                    {
                        CCT.AccountsclearanceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AccountsAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AccountsAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Admin")
                    {
                        CCT.AdminClearnaceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AdminAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AdminAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "IT")
                    {
                        CCT.ITClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.ITAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.ITAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "HR")
                    {
                        CCT.HRClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.HRAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.HRAllStatus = "Approved";
                        }

                    }
                a:
                    HR.ClearanceCertificateFinalTBs.InsertOnSubmit(CCT);
                    HR.SubmitChanges();

                }
                else
                {
                    ClearanceCertificateFinalTB CCT = HR.ClearanceCertificateFinalTBs.Where(d => d.EmployeeID == CTM.EmployeeID).First();

                    CCT.EmployeeID = CTM.EmployeeID;
                    CCT.CompanyID = CTM.CompanyID;

                    if (dddepartment.SelectedItem.Text == "Department Head")
                    {
                        CCT.DepartmentHeadclearnaceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.DeparmentAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.DeparmentAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Accounts & Finance")
                    {
                        CCT.AccountsclearanceID = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AccountsAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AccountsAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "Admin")
                    {
                        CCT.AdminClearnaceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.AdminAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.AdminAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "IT")
                    {
                        CCT.ITClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.ITAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.ITAllStatus = "Approved";
                        }
                    }
                    else if (dddepartment.SelectedItem.Text == "HR")
                    {
                        CCT.HRClearanceId = CT.ClearanceDepartmentHeadID;
                        if (status.SelectedItem.Text == "Pending")
                        {
                            CCT.HRAllStatus = "Pending";
                            goto a;
                        }
                        else
                        {
                            CCT.HRAllStatus = "Approved";
                        }

                    }
                a:

                    HR.SubmitChanges();
                }


            }
            g.ShowMessage(this.Page, "Record Updated"); clear();
        }
        
    }

    private void clear()
    {
        dddepartment.SelectedIndex = 0;
        ddlCompany.SelectedIndex = 0;
       // ddEmp.SelectedIndex = 0;
        grdheaddepartment.DataSource = null;
        grdheaddepartment.DataBind();
        BindAllData();
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void ddEmp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkApproved_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (sender) as LinkButton;
        string clearid = lnk.CommandArgument;
        lblclearid.Text = clearid;
        DataSet dsalldata = g.ReturnData1("SELECT     ClearCertificatemasterID,CompanyID, EmployeeID, EnterDate,CM.DepartmentName, Description 'A',Comments,FinalApprovedStatus Status,CM.UserID FROM         ClearCertificateMasterTB AS CM left outer join ClearanceCertificateDetailsTB ET on ET.ClearMasterID=CM.ClearCertificatemasterID where ClearCertificatemasterID='" + clearid + "'");
        if (dsalldata.Tables[0].Rows.Count > 0)
        {
            fillcompany();
            ddlCompany.SelectedValue = Convert.ToString(dsalldata.Tables[0].Rows[0]["CompanyID"]);

            bindemp();
            ddEmp.SelectedValue = Convert.ToString(dsalldata.Tables[0].Rows[0]["EmployeeID"]);

            dddepartment.SelectedValue = Convert.ToString(dsalldata.Tables[0].Rows[0]["DepartmentName"]);

            grdheaddepartment.DataSource = dsalldata.Tables[0];
            grdheaddepartment.DataBind();

            for (int i = 0; i < grdheaddepartment.Rows.Count; i++)
            {
                TextBox txtcomments = (TextBox)grdheaddepartment.Rows[i].Cells[1].FindControl("txtcomments");
                txtcomments.Text = dsalldata.Tables[0].Rows[i]["Comments"].ToString();

                DropDownList DRP = (DropDownList)grdheaddepartment.Rows[i].Cells[2].FindControl("ddstatus");

                if ("Pending" == Convert.ToString(dsalldata.Tables[0].Rows[i]["Status"].ToString()))
                {
                    DRP.SelectedIndex = 0;
                }
                else
                {
                    DRP.SelectedIndex = 1;
                    DRP.Enabled = false;
                }
              
            

            }
                 
        }
        btnSubmit.Text = "Update";
        MultiView1.ActiveViewIndex = 1;

    }
}