using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vacancies : System.Web.UI.Page
{
    /// <summary>
    /// Changes by Abdul Rahman Add Vacancy Form
    /// Change Date : 18/11/2014
    /// </summary>
   

    #region Declear variable
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext ex = new HrPortalDtaClassDataContext();
    DataTable dt1 = new DataTable();
    string path = "";
 #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                MultiView1.ActiveViewIndex = 0;
                Bindalldata();
                bindCompany();
                bindemp();
                fillcity();
                ddlDesign.Items.Insert(0, "--Select--");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void fillcity()
    {
        try
        {
            var dtcity = from d in ex.CityTBs
                         select new
                         {
                             d.CityId,
                             d.CityName
                         };
            if (dtcity.Count() > 0)
            {
                ddlcity.DataSource = dtcity;
                ddlcity.DataTextField = "CityName";
                ddlcity.DataValueField = "CityId";
                ddlcity.DataBind();
            }
            else
            {
                ddlcity.DataBind();
                ddlcity.DataSource = null;
            }

        }
        catch (Exception ex)
        {
            
           g.ShowMessage(this.Page, ex.Message);
        }
    }
    #region MeTHODS
    private void BindDept()
    {
        try
        {
            var dt = from p in ex.MasterDeptTBs
                     where p.Status == 0 && p.CompanyId==Convert.ToInt32(ddlComp.SelectedValue)
                     select new { p.DeptID,p.DeptName};
            if (dt.Count() > 0)
            {
                ddlDept.DataSource = dt;
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "DeptID";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlDept.Items.Clear();
                ddlDept.DataSource = null;
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
   
    private void bindemp()
    {
        try
        {
            if (ddlComp.SelectedIndex !=-1)
            {
             var dt = from p in ex.EmployeeTBs where p.RelivingStatus==null && p.CompanyId==Convert.ToInt32(ddlComp.SelectedValue)
                     select new { Name = p.FName + ' ' + p.MName + ' ' + p.Lname, p.EmployeeId };
            if (dt.Count() > 0)
            {
                lstEmp.DataSource = dt;
                lstEmp.DataTextField = "Name";
                lstEmp.DataValueField = "EmployeeId";
                lstEmp.DataBind();
               /// lstEmp.Items.Insert(0, "--Select--");
            }
            else
            {
                lstEmp.Items.Clear();
                lstEmp.DataSource = null;
                lstEmp.DataBind();
               // lstEmp.Items.Insert(0, "--Select--");
            }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    private void clear()
    {
        txtCode.Text = "";
        txtDesc.Text = "";
        txtExpr.Text = "";
        txtOther.Text = "";
        txtPlace.Text = "";
        txtQuota.Text = "";
        txtSalary.Text = "";
        txtSkills.Text = "";
        txtTitle.Text = "";
        tdOther.Visible = false;
        //ddlComp.SelectedIndex = -1;
        //ddlDept.SelectedIndex = -1;
        //ddlDesign.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 0;
        ddlComp.Items.Clear();
        ddlDept.Items.Clear();
        ddlDesign.Items.Clear();
        Bindalldata();
        bindCompany();
        bindemp();
        for (int i = 0; i < lstQualifi.Items.Count; i++)
        {
            lstQualifi.Items[i].Selected = false;
        }

    }
    private void bindCompany()
    {
        try
        {
            var dt = from p in ex.CompanyInfoTBs
                     select new { p.CompanyName, p.CompanyId };
            if (dt.Count() > 0)
            {
                ddlComp.DataSource = dt;
                ddlComp.DataTextField = "CompanyName";
                ddlComp.DataValueField = "CompanyId";
                ddlComp.DataBind();
                //ddlComp.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlComp.Items.Clear();
                ddlComp.DataSource = null;
                ddlComp.DataBind();
              // ddlComp.Items.Insert(0, "--Select--");
            }

            if (ddlComp.SelectedIndex !=-1)
            {
            
            var dtdept = from p in ex.MasterDeptTBs
                     where p.Status == 0 && p.CompanyId == Convert.ToInt32(ddlComp.SelectedValue)
                     select new { p.DeptID, p.DeptName };
            if (dt.Count() > 0)
            {
                ddlDept.DataSource = dtdept;
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "DeptID";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlDept.Items.Clear();
                ddlDept.DataSource = null;
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "--Select--");
            }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void Bindalldata()
    {
        try
        {
            //var dt = from p in ex.VancancyTBs
            //          join Dept in ex.MasterDeptTBs on p.DeptID equals Dept.DeptID
            //          join Design in ex.MasterDesgTBs on p.DesignID equals Design.DesigID
            //          join c in ex.CompanyInfoTBs on p.CompanyID equals c.CompanyId
            //          select new {p.Code,c.Address,c.ContactNo,c.Email,Dept.DeptName,Design.DesigName,
                          
            //              p.EmployeeType, c.CompanyName };
            //if (dt.Count() > 0)
            //{
            //    grdAtt.DataSource = dt;
            //    grdAtt.DataBind();
            //    lblcnt.Text = dt.Count().ToString();
            //}
            //else
            //{
            //    lblcnt.Text = "0";
            //    grdAtt.DataSource = null;
            //    grdAtt.DataBind();
            //}

            string query = @"Select CompanyInfoTB.CompanyName,MasterDeptTB.DeptName,MasterDesgTB.DesigName,Title as VacancyName, Quota,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=VT.VacancyID ) as NOofInterview,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=VT.VacancyId ) as SelectedCandidate, VacancyStatus from VancancyTB VT Left outer join CompanyInfoTB on VT.CompanyID=CompanyInfoTB.CompanyID left outer join MasterDeptTB on VT.DeptID=MasterDeptTB.DeptID left outer join MasterDesgTB on VT.DesignID=MasterDesgTB.DesigID where 1=1";
            if(!string.IsNullOrEmpty(txtcompany.Text))
            {
                query += " and companyname like '" + txtcompany.Text + "'";
            }
            if(!string.IsNullOrEmpty(txtvacancy.Text))
            {
                query += " And VT.Title='" + txtvacancy.Text + "'";
            }
            if(!string.IsNullOrEmpty(txtdepart.Text))
            {
                query += " And DeptName like '" + txtdepart.Text + "%'";
            }
            DataTable dtfecthvacancyDetails = g.ReturnData(query);
            grdAtt.DataSource = dtfecthvacancyDetails;
            grdAtt.DataBind();
            if (dtfecthvacancyDetails.Rows.Count > 0)
            {
                lblcnt.Text = dtfecthvacancyDetails.Rows.Count.ToString();
            }
            else
            {
                lblcnt.Text = "0";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        Bindalldata();
    }
    #endregion
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {

         var dt = from p in ex.VancancyTBs.Where(d => d.CompanyID ==Convert.ToInt32(ddlComp.SelectedValue)
             && d.DesignID==Convert.ToInt32(ddlDesign.SelectedValue) && d.Code==txtCode.Text) select p;
          if (dt.Count() > 0)
          {
              g.ShowMessage(this.Page, "Vacancy Code Already Exist");
          }
          else
          {
             #region
        VancancyTB v = new VancancyTB();
        v.Code = txtCode.Text;
        v.CompanyID = Convert.ToInt32(ddlComp.SelectedValue);
        v.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
        v.DesignID = Convert.ToInt32(ddlDesign.SelectedValue);
        v.EmployeeType = rbType.SelectedIndex.ToString();
        v.VacancyDate = Convert.ToDateTime(DateTime.Now.ToString());
        v.Descript = txtDesc.Text;
        v.Expr = txtExpr.Text + "-" + txtExprMax.Text;
        if (tdOther.Visible)
        {
            v.Other = txtOther.Text;
        }
        v.Place = ddlcity.SelectedItem.Text;
        v.City_Place=Convert.ToInt32(ddlcity.SelectedValue);
        v.Quota = txtQuota.Text;
        string s = "";
        for (int i = 0; i < lstQualifi.Items.Count; i++)
        {
            if (lstQualifi.Items[i].Selected)
            {
                s = lstQualifi.Items[i].Text + ',' + s;
            }
        }

       
        v.Qualification = s;
        v.Salary = txtSalary.Text;
        v.Skills = txtSkills.Text;
        v.Title = txtTitle.Text;
        v.VacancyStatus = "Pending";
        ex.VancancyTBs.InsertOnSubmit(v);
        ex.SubmitChanges();

       


        // Apporved by Save........
              for (int i = 0; i < lstEmp.Items.Count; i++)
              {
                  if (lstEmp.Items[i].Selected)
                  {
                      VacDetTB va = new VacDetTB();

                      va.Approvedby = Convert.ToInt32(lstEmp.Items[i].Value);                   
                      va.VacID = v.VacancyID;
                      ex.VacDetTBs.InsertOnSubmit(va);
                      ex.SubmitChanges();
                  }
			}

              // Document Save........

              if (ViewState["dt1"] != null)
              {
                  dt1 = ViewState["dt1"] as DataTable;

                  for (int i = 0; i < dt1.Rows.Count; i++)
                  {
                      VacancyDocumentTB D = new VacancyDocumentTB();
                      D.Vacancy_Id = v.VacancyID;
                      D.Document_Name = dt1.Rows[i][0].ToString();
                      D.Document_Path = dt1.Rows[i][1].ToString();
                      ex.VacancyDocumentTBs.InsertOnSubmit(D);
                      ex.SubmitChanges();

                  }
                  ViewState["dt1"] = null;
                  grdnewdoc.DataSource = null;
                  grdnewdoc.DataBind();
              }





        g.ShowMessage(this.Page, "Details Submitted Successfully....");
        clear();
         #endregion
          }

        }
    }

    private bool IsValid()
    {
        if (lstEmp.SelectedIndex == -1)
            return false;
        else
        {
            return true;
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ddlComp.Focus();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Vacancies.aspx");
    }
    protected void grdAtt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAtt.PageIndex = e.NewPageIndex;
        Bindalldata();
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDept.SelectedIndex > 0)
            {
                var dt = from p in ex.MasterDesgTBs
                         where p.DeptID == Convert.ToInt32(ddlDept.SelectedValue)
                         where p.Status == 0
                         select new { p.DesigID, p.DesigName };
                if (dt.Count() > 0)
                {
                    ddlDesign.DataSource = dt;
                    ddlDesign.DataTextField = "DesigName";
                    ddlDesign.DataValueField = "DesigID";
                    ddlDesign.DataBind();
                    ddlDesign.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddlDesign.Items.Clear();
                    ddlDesign.DataSource = null;
                    ddlDesign.DataBind();
                    ddlDesign.Items.Insert(0, "--Select--");
                }
            }


            
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlComp.SelectedIndex !=-1)
        {
            BindDept();
            bindemp();
        }
    }
    protected void ddlDesign_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbType.SelectedIndex==2)
        {
            tdOther.Visible = true;
        }
        else
        {
            tdOther.Visible = false;
        }
    }

    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }
    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        UploadDocmentfun();
    }

    private void UploadDocmentfun()
    {
        try
        {
            if (FileUploadDocu.HasFile)
            {
                int cnt = 0;
                string dir = Server.MapPath("~/Recruitment/VacancyDocument");
                string[] files;
                int numFiles;
                files = Directory.GetFiles(dir);
                numFiles = files.Length;

                string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                string filename = Path.GetFileName(FileUploadDocu.FileName);
                string[] filename1 = filename.Split('.');
                for (int i = 0; i < numFiles; i++)
                {
                    if (File.Exists(Server.MapPath("~/Recruitment/VacancyDocument/" + filename1[0] + cnt + "." + filename1[1])))
                    {
                        cnt++;
                    }
                }
                FileUploadDocu.SaveAs(Request.PhysicalApplicationPath + "./Recruitment/VacancyDocument/" + filename1[0] + cnt + "." + filename1[1]);
                path = filename1[0] + cnt + "." + filename1[1];
                lblpath.Text = path;


            }
            else
            {
                // g.ShowMessage(this.Page, "Please Upload File");
                // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Please Upload File');", true);
            }

            if (lblpath.Text == "")
            {
                g.ShowMessage(this.Page, "Please Upload File");
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Please Upload File');", true);
            }
            else
            {
                int cnt = 0;
                if (ViewState["dt1"] != null)
                {
                    dt1 = (DataTable)ViewState["dt1"];
                }
                else
                {
                    DataColumn DocumentName = dt1.Columns.Add("Document_Name");
                    DataColumn Path1 = dt1.Columns.Add("Document_Path");
                }
                DataRow dr = dt1.NewRow();
                dr[0] = txtdocname.Text;
                dr[1] = lblpath.Text;
                if (dt1.Rows.Count > 0)
                {
                    for (int f = 0; f < dt1.Rows.Count; f++)
                    {

                        string u2 = dt1.Rows[f][0].ToString();
                        if (u2 == txtdocname.Text)
                        {
                            cnt++;

                        }
                        else
                        {

                        }
                    }
                    if (cnt > 0)
                    {
                        g.ShowMessage(this.Page, "Document Name Already Exist");
                        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Already Exist');", true);


                    }
                    else
                    {
                        dt1.Rows.Add(dr);
                        txtdocname.Text = "";
                        lblpath.Text = "";
                    }
                }
                else
                {
                    dt1.Rows.Add(dr);
                    txtdocname.Text = "";
                    lblpath.Text = "";
                }

                ViewState["dt1"] = dt1;

                grdnewdoc.DataSource = dt1;
                grdnewdoc.DataBind();

            }

        }
        catch (Exception ex)
        {
            g.ShowMessage(this.Page, ex.Message);
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','" + ex.Message + "');", true);
        }
    }
    protected void Imgbtnedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton EditDocu = (ImageButton)sender;
        dt1 = (DataTable)ViewState["dt1"];
        foreach (DataRow d in dt1.Rows)
        {
            if (d[0].ToString() == EditDocu.CommandArgument)
            {

                txtdocname.Text = d[0].ToString();
                lblpath.Text = d[1].ToString();
                d.Delete();
                dt1.AcceptChanges();
                break;
            }
        }
        grdnewdoc.DataSource = dt1;
        grdnewdoc.DataBind();
        ViewState["dt1"] = dt1;
    }
    protected void Imgbtndelet_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton DeleteDocu = (ImageButton)sender;
        dt1 = (DataTable)ViewState["dt1"];
        foreach (DataRow d in dt1.Rows)
        {
            if (d[0].ToString() == DeleteDocu.CommandArgument)
            {
                d.Delete();
                dt1.AcceptChanges();
                break;
            }
        }
        grdnewdoc.DataSource = dt1;
        grdnewdoc.DataBind();
        ViewState["dt1"] = dt1;
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListCompany(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> company = (from d in hr.CompanyInfoTBs
                                  .Where(r => r.CompanyName.Contains(prefixText))
                                select d.CompanyName).Distinct().ToList();
        return company;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListVacancy(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> vacancy = (from d in hr.VancancyTBs
                                  .Where(r => r.Title.Contains(prefixText) && r.CompanyID == Convert.ToInt32(contextKey))
                                select d.Title).Distinct().ToList();
        return vacancy;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListDepartment(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> department = (from d in hr.MasterDeptTBs
                                  .Where(r => r.DeptName.Contains(prefixText) && r.CompanyId == Convert.ToInt32(contextKey))
                                select d.DeptName).Distinct().ToList();
        return department;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListDesig(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> desig = (from d in hr.MasterDesgTBs
                                  .Where(r => r.DesigName.Contains(prefixText) && r.DeptID == Convert.ToInt32(contextKey))
                                   select d.DesigName).Distinct().ToList();
        return desig;
    }


    #region fetch Id for all searching TextBox Like CompanyId, deparmentId..
    protected void txtcompany_TextChanged(object sender, EventArgs e)
    {
        if (txtcompany.Text != "")
        {
            var findcompanyID = from d in ex.CompanyInfoTBs
                                where d.CompanyName == txtcompany.Text
                                select new { d.CompanyId };
            if (findcompanyID.Count() > 0)
            {
                foreach (var item in findcompanyID)
                {
                    lblcompaniId.Text = item.CompanyId.ToString();
                    txtdepart_AutoCompleteExtender.ContextKey = lblcompaniId.Text;
                    txtvacancy_AutoCompleteExtender.ContextKey = lblcompaniId.Text;
                }
            }
            else
            {
                lblcompaniId.Text = "";
            }
            txtvacancy.Focus();
        }
    }
    
    protected void txtdepart_TextChanged(object sender, EventArgs e)
    {
        if (txtdepart.Text != "")
        {
            var finddeptID = from d in ex.MasterDeptTBs
                             where d.DeptName == txtdepart.Text
                             select new { d.DeptID };
            if (finddeptID.Count() > 0)
            {
                foreach (var item in finddeptID)
                {
                    lbldeptId.Text = item.DeptID.ToString();
                    txtdesig_AutoCompleteExtender.ContextKey = lbldeptId.Text;
                }
            }
            else
            {
                lbldeptId.Text = "";
            }
            txtdepart.Focus();
        }
    }
    protected void txtdesig_TextChanged(object sender, EventArgs e)
    {
        if (txtdesig.Text != "")
        {
            var finddesigID = from d in ex.MasterDesgTBs
                              where d.DesigName == txtdesig.Text
                             select new { d.DesigID };
            if (finddesigID.Count() > 0)
            {
                foreach (var item in finddesigID)
                {
                    lbldesigID.Text = item.DesigID.ToString();
                }
            }
            else
            {
                lbldesigID.Text = "";
            }
        }
    }
    #endregion



    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        var codedata = from d in ex.VancancyTBs
                       where d.Code == txtCode.Text
                       select new { d.Code };
        if (codedata.Count() > 0)
        {
            txtCode.Text = "";
            g.ShowMessage(this.Page, "Vacancy Code already Exist");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtcompany.Text = txtvacancy.Text = txtdepart.Text = string.Empty;
        txtcompany.Focus();
        Bindalldata();
       
    }
}