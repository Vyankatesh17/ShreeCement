using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Passport : System.Web.UI.Page
{
    //Passport Details
    //Created By Manasi
    //31 Oct 2014  

    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable Dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                fillcompany();
                fillCountry();
                BindAllData();
            }
            else
            {
                string FolderPath = Server.MapPath("PassportDocs");
                MakeDirectoryIfExist(FolderPath);
                if (FUDoc.HasFile)
                {
                    string filename = Path.GetFileName(FUDoc.PostedFile.FileName);
                    string ext = Path.GetExtension(FUDoc.PostedFile.FileName);
                    FUDoc.SaveAs(Server.MapPath("PassportDocs/" + filename));
                    //   AttachPath = filename;
                    // image1.ImageUrl = "~/PassportDocs/" + AttachPath;
                    lblAttachPath.Text = filename;

                }
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }
    #region
    private void fillCountry()
    {
        var data = from dt in HR.CountryTBs
                   where dt.Status == 0
                   select new { dt.CountryId, dt.CountryName };
        if (data != null && data.Count() > 0)
        {

            ddlContry.DataSource = data;
            ddlContry.DataTextField = "CountryName";
            ddlContry.DataValueField = "CountryId";
            ddlContry.DataBind();
            ddlContry.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlContry.Items.Clear();
            ddlContry.DataSource = data;
            ddlContry.DataBind();
            ddlContry.Items.Insert(0, "--Select--");
        }

    }

    private void fillcompanySearch()
    {
        var data = from dt in HR.CompanyInfoTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlSort.DataSource = data;
            ddlSort.DataTextField = "CompanyName";
            ddlSort.DataValueField = "CompanyID";
            ddlSort.DataBind();
            ddlSort.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlSort.Items.Clear();
            ddlSort.DataSource = data;
            ddlSort.DataBind();
            ddlSort.Items.Insert(0, "--Select--");
        }
    }

    private void FillEmpSearch()
    {
        var data = from dt in HR.EmployeeTBs
                   where dt.RelivingStatus == null 
                   ///&& dt.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                   select new { dt.EmployeeId, EmpName = dt.FName + ' ' + dt.MName + ' ' + dt.Lname };
        if (data != null && data.Count() > 0)
        {

            ddlSort.DataSource = data;
            ddlSort.DataTextField = "EmpName";
            ddlSort.DataValueField = "EmployeeId";
            ddlSort.DataBind();
            ddlSort.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlSort.Items.Clear();
            ddlSort.DataSource = data;
            ddlSort.DataBind();
            ddlSort.Items.Insert(0, "--Select--");
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
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlCompany.Items.Clear();
            ddlCompany.DataSource = data;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
    }

    public void BindAllData()
    {
        var deptData = from d in HR.PassportTBs
                       join k in HR.CompanyInfoTBs on d.CompanyID equals k.CompanyId
                       join emp in HR.EmployeeTBs on d.EmpID equals emp.EmployeeId
                       join c in HR.CountryTBs on d.Country equals c.CountryId
                       where emp.RelivingStatus==null
                       select new { EmpName = emp.FName + ' ' + emp.MName + ' ' + emp.Lname, d.PassportID, d.PassportNo, d.NameinPassport, k.CompanyName, d.Address, c.CountryName, d.ExpiryDate, d.IssueDate };
        if (deptData.Count() > 0)
        {
            grdData.DataSource = deptData;
            grdData.DataBind();
            lblcnt.Text = deptData.Count().ToString();
        }
        else
        {
            grdData.DataSource = deptData;
            grdData.DataBind();
            lblcnt.Text = "0";
        }
    }


    private void FillEmp()
    {
        var data = from dt in HR.EmployeeTBs
                   where dt.RelivingStatus == null && dt.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                  //&& !(from o in HR.PassportTBs  select o.EmpID).Contains(dt.EmployeeId)   
                   select new { dt.EmployeeId, EmpName = dt.FName + ' ' + dt.MName + ' ' + dt.Lname };
        //var data = from dt in HR.EmployeeTBs
        //           where dt.RelivingStatus == null && dt.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
        //           select new { dt.EmployeeId, EmpName = dt.FName + ' ' + dt.MName + ' ' + dt.Lname };
        
        
        
        if (data != null && data.Count() > 0)
        {

            ddlEmp.DataSource = data;
            ddlEmp.DataTextField = "EmpName";
            ddlEmp.DataValueField = "EmployeeId";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlEmp.Items.Clear();
            ddlEmp.DataSource = data;
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, "--Select--");
        }
    }

    #endregion

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        BindAllData();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //txtPassportNo_TextChanged(sender, e);

        var dte = from p in HR.PassportTBs.Where(d => d.CompanyID == Convert.ToInt32(ddlCompany.SelectedValue) &&
               d.EmpID == Convert.ToInt32(ddlEmp.SelectedValue)
               && d.IssueDate >= Convert.ToDateTime(txtIssueDate.Text) 
               && d.ExpiryDate <= Convert.ToDateTime(txtExpiryDate.Text) 
               )
                  select p;
        if (dte.Count() > 0)
        {
            modpop.Message = "Passport Details Already Exist";
            modpop.ShowPopUp();

        }
        else
        {
            DataSet dsIssue = g.ReturnData1(@"SELECT [t0].[PassportID], [t0].[CompanyID], [t0].[EmpID], [t0].[NameinPassport], [t0].[PassportNo], [t0].[PlaceofIssue], [t0].[Country], [t0].[BirthPlace], [t0].[IssueDate], [t0].[ExpiryDate], [t0].[Address]
FROM [dbo].[PassportTB] AS [t0] WHERE ([t0].[CompanyID] = '"+Convert.ToInt32(ddlCompany.SelectedValue)+"') AND ([t0].[EmpID] = '"+Convert.ToInt32(ddlEmp.SelectedValue)+"') AND ([t0].[IssueDate] between '" + txtIssueDate.Text + "' and '" + txtIssueDate.Text + "' ) ");
            if (dsIssue.Tables[0].Rows.Count==0)
            {
                DataSet dsExpiry = g.ReturnData1(@"SELECT [t0].[PassportID], [t0].[CompanyID], [t0].[EmpID], [t0].[NameinPassport], [t0].[PassportNo], [t0].[PlaceofIssue], [t0].[Country], [t0].[BirthPlace], [t0].[IssueDate], [t0].[ExpiryDate], [t0].[Address]
FROM [dbo].[PassportTB] AS [t0] WHERE ([t0].[CompanyID] = '" + Convert.ToInt32(ddlCompany.SelectedValue) + "') AND ([t0].[EmpID] = '" + Convert.ToInt32(ddlEmp.SelectedValue) + "') AND ([t0].[ExpiryDate] between '" + txtExpiryDate.Text + "' and '" + txtExpiryDate.Text + "' ) ");

                if (dsExpiry.Tables[0].Rows.Count==0)
                {
                    if (isvalid())
                    {
                        #region
                        PassportTB pas = new PassportTB();
                        pas.Address = txtAddress.Text;
                        pas.BirthPlace = txtBirthPlace.Text;
                        pas.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                        pas.Country = Convert.ToInt32(ddlContry.SelectedValue);
                        pas.EmpID = Convert.ToInt32(ddlEmp.SelectedValue);
                        pas.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                        pas.IssueDate = Convert.ToDateTime(txtIssueDate.Text);
                        pas.NameinPassport = txtName.Text;
                        pas.PassportNo = txtPassportNo.Text;
                        HR.PassportTBs.InsertOnSubmit(pas);
                        HR.SubmitChanges();

                        DataTable dttt = (DataTable)ViewState["DT"];
                        if (dttt != null)
                        {
                            for (int i = 0; i < dttt.Rows.Count; i++)
                            {
                                PassportDocTB docs = new PassportDocTB();
                                docs.DocName = dttt.Rows[i]["DocName"].ToString();
                                docs.FileName = dttt.Rows[i]["FileName"].ToString();
                                docs.PassportID = pas.PassportID;
                                HR.PassportDocTBs.InsertOnSubmit(docs);
                                HR.SubmitChanges();

                            }
                        }
                        modpop.Message = "Passport Details Submitted Successfully";
                        modpop.ShowPopUp();

                        Clear();

                        #endregion
                    }
                }
                else
                {
                    modpop.Message = "Expiry Details Already Exist";
                    modpop.ShowPopUp();
                }
            }
            else
            {
                modpop.Message = "Issue Details Already Exist";
                modpop.ShowPopUp();
            }
        }

    }

    private bool isvalid()
    {
        DateTime dtcurrent = g.GetCurrentDateTime();
        if (Convert.ToDateTime(txtIssueDate.Text) < Convert.ToDateTime(dtcurrent.Date.ToShortDateString()))
        {
            modpop.Message = "Issue Date Should be Greater than Current";
            modpop.ShowPopUp();
            return false;
        }
        if (Convert.ToDateTime(txtIssueDate.Text) > Convert.ToDateTime(txtExpiryDate.Text))
        {
            modpop.Message = "Expiry Date Should be Greater than Issue Date";
            modpop.ShowPopUp();
            return false;
        }


        return true;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        btnsubmit.Visible = true;
        MultiView1.ActiveViewIndex = 0;
        txtAddress.Text = null;
        txtBirthPlace.Text = null;
        txtDocName.Text = null;
        txtExpiryDate.Text = null;
        txtIssueDate.Text = null;
        txtName.Text = null;
        txtPassportNo.Text = null;
        txtPlaceIssue.Text = null;
        lblAttachPath.Text = "";
        ddlCompany.SelectedIndex = 0;
        ddlContry.SelectedIndex = 0;
        ddlEmp.SelectedIndex = 0;
        Dt = null;
        fillcompany();
        fillCountry();
        BindAllData();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex!=0)
        {
            FillEmp();
        }
    }
    #region

    protected void btnaddEdu_Click(object sender, EventArgs e)
    {
        Dt.Clear();
        //ViewState["DT"] = Dt;
        int cnt = 0;
        if (ViewState["DT"] != null)
        {
            Dt = (DataTable)ViewState["DT"];
        }
        else
        {
            DataColumn DocName = Dt.Columns.Add("DocName");
            DataColumn FileName = Dt.Columns.Add("FileName");
        }

        DataRow dr = Dt.NewRow();
        dr[0] = txtDocName.Text;
        dr[1] = lblAttachPath.Text;

        if (Dt.Rows.Count > 0)
        {
            for (int f = 0; f < Dt.Rows.Count; f++)
            {
                string docname = Dt.Rows[f][0].ToString();
                string filename = Dt.Rows[f][1].ToString();

                if (docname == txtDocName.Text && filename == lblAttachPath.Text)
                {
                    cnt++;
                }
                else
                {

                }
            }
            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "Already Exist");
            }
            else
            {
                Dt.Rows.Add(dr);
                cleareducation();
            }
        }
        else
        {
            Dt.Rows.Add(dr);
            cleareducation();
        }

        ViewState["DT"] = Dt;
        grd.DataSource = Dt;
        grd.DataBind();


    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        Dt = (DataTable)ViewState["DT"];
        foreach (DataRow d in Dt.Rows)
        {
            if (d[0].ToString() == imgedit.CommandArgument)
            {
                txtDocName.Text = d["DocName"].ToString();
                lblAttachPath.Text = d["FileName"].ToString();
                d.Delete();
                Dt.AcceptChanges();
                break;
            }
        }
        grd.DataSource = Dt;
        grd.DataBind();


    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        Dt = (DataTable)ViewState["DT"];

        foreach (DataRow d in Dt.Rows)
        {
            if (d[0].ToString() == imgdelete.CommandArgument)
            {
                d.Delete();
                Dt.AcceptChanges();
                break;
            }
        }

        grd.DataSource = Dt;
        grd.DataBind();
        cleareducation();
    }
    private void cleareducation()
    {
        txtDocName.Text = null;
        lblAttachPath.Text = null;
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        #region
        if (ddlSearch.SelectedIndex == 0)
        {//All
            var deptData = from d in HR.PassportTBs
                           join k in HR.CompanyInfoTBs on d.CompanyID equals k.CompanyId
                           join emp in HR.EmployeeTBs on d.EmpID equals emp.EmployeeId
                           join c in HR.CountryTBs on d.Country equals c.CountryId
                           select new { EmpName = emp.FName + ' ' + emp.MName + ' ' + emp.Lname, d.PassportID, d.PassportNo, d.NameinPassport, k.CompanyName, d.Address, c.CountryName, d.ExpiryDate, d.IssueDate };
            if (deptData.Count() > 0)
            {
                grdData.DataSource = deptData;
                grdData.DataBind();
                lblcnt.Text = deptData.Count().ToString();
            }
            else
            {
                grdData.DataSource = deptData;
                grdData.DataBind();
                lblcnt.Text = "0";
            }
        }
        if (ddlSearch.SelectedIndex == 1)
        {//Comp
            var deptData = from d in HR.PassportTBs
                           join k in HR.CompanyInfoTBs on d.CompanyID equals k.CompanyId
                           join emp in HR.EmployeeTBs on d.EmpID equals emp.EmployeeId
                           join c in HR.CountryTBs on d.Country equals c.CountryId
                           where d.CompanyID == Convert.ToInt32(ddlSort.SelectedValue)
                           select new { EmpName = emp.FName + ' ' + emp.MName + ' ' + emp.Lname, d.PassportID, d.PassportNo, d.NameinPassport, k.CompanyName, d.Address, c.CountryName, d.ExpiryDate, d.IssueDate };
            if (deptData.Count() > 0)
            {
                grdData.DataSource = deptData;
                grdData.DataBind();
                lblcnt.Text = deptData.Count().ToString();
            }
            else
            {
                grdData.DataSource = deptData;
                grdData.DataBind();
                lblcnt.Text = "0";
            }
        }
        if (ddlSearch.SelectedIndex == 2)
        {//Emp
            var deptData = from d in HR.PassportTBs
                           join k in HR.CompanyInfoTBs on d.CompanyID equals k.CompanyId
                           join emp in HR.EmployeeTBs on d.EmpID equals emp.EmployeeId
                           join c in HR.CountryTBs on d.Country equals c.CountryId
                           where d.EmpID == Convert.ToInt32(ddlSort.SelectedValue)
                           select new { EmpName = emp.FName + ' ' + emp.MName + ' ' + emp.Lname, d.PassportID, d.PassportNo, d.NameinPassport, k.CompanyName, d.Address, c.CountryName, d.ExpiryDate, d.IssueDate };
            if (deptData.Count() > 0)
            {
                grdData.DataSource = deptData;
                grdData.DataBind();
                lblcnt.Text = deptData.Count().ToString();
            }
            else
            {
                grdData.DataSource = deptData;
                grdData.DataBind();
                lblcnt.Text = "0";
            }
        }
        #endregion

    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlSearch.SelectedIndex == 0)
        {//All
            BindAllData();
            trData.Visible = false;
        }
        if (ddlSearch.SelectedIndex == 1)
        {//Comp
            lblSearch.Text = "Select Company: ";
            fillcompanySearch();
            trData.Visible = true;
        }
        if (ddlSearch.SelectedIndex == 2)
        {//Emp
            lblSearch.Text = "Select Employee: ";
            FillEmpSearch();
            trData.Visible = true;
        }
        MultiView1.ActiveViewIndex = 0;
    }
    protected void imgview_Click(object sender, ImageClickEventArgs e)
    {
        string pid = "";
        MultiView1.ActiveViewIndex = 1;
        btnsubmit.Visible = false;
        ImageButton ImgView = (ImageButton)sender;
        pid = ImgView.CommandArgument;
        PassportTB pas = HR.PassportTBs.Where(d => d.PassportID == Convert.ToInt32(pid)).First();
        txtAddress.Text = pas.Address;
        txtBirthPlace.Text = pas.BirthPlace;
        fillcompany();
        ddlCompany.SelectedValue = pas.CompanyID.ToString();
        fillCountry();
        ddlContry.SelectedValue = pas.Country.ToString();
        FillEmp();
        ddlEmp.SelectedValue = pas.EmpID.ToString();
        DateTime dd = Convert.ToDateTime(pas.ExpiryDate.ToString());
        string s=   dd.ToString("MM/dd/yyyy");


        DateTime dd1 = Convert.ToDateTime(pas.IssueDate.ToString());
        string s1 = dd1.ToString("MM/dd/yyyy");

        txtExpiryDate.Text = s.ToString();
        txtIssueDate.Text = s1.ToString();
        txtName.Text = pas.NameinPassport;
        txtPassportNo.Text = pas.PassportNo;

        DataSet ds = g.ReturnData1("SELECT PassportDocTB.DocName, PassportDocTB.FileName FROM PassportDocTB LEFT OUTER JOIN PassportTB ON PassportDocTB.PassportID = PassportTB.PassportID where PassportTB.PassportID='"+Convert.ToInt32(pid)+"'");
       
            grd.DataSource = ds.Tables[0];
            grd.DataBind();
    }



    protected void txtPassportNo_TextChanged(object sender, EventArgs e)
    {
        if (txtIssueDate.Text!="" && txtExpiryDate.Text!="")
        {
            #region
        var dte = from p in HR.PassportTBs.Where(d => d.PassportNo == txtPassportNo.Text
              && d.IssueDate>= Convert.ToDateTime(txtIssueDate.Text)
                 && d.ExpiryDate <= Convert.ToDateTime(txtExpiryDate.Text) 
              )
                  select p;

        if (dte.Count()>0)
        {
            modpop.Message = "Passport No Already Exist";
            modpop.ShowPopUp();
        }
        else
        {

        }


        #endregion
        }
    }
}