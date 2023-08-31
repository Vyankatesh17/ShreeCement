using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Recruitment_ApproveVacancyForm : System.Web.UI.Page
{
    /// <summary>
    /// Approve Form
    /// Created By Abdul Rahman
    /// Change Date : 19/11/2014
    /// </summary>


    #region Declear variable
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    DataTable dt1 = new DataTable();
    string path = "";
   // int id;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
              Page.Form.Attributes.Add("enctype", "multipart/form-data");
              MultiView1.ActiveViewIndex = 0;
              
             // fetchvacancydt();
              bindgrdvacancy();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindgrdvacancy()
    {
        try
        {
            bool admin=g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
            if (admin==true)
            {
                DataTable dtfecthvacancyDetails = g.ReturnData("Select VT.VacancyId, VT.VacancyStatus, CompanyInfoTB.CompanyName,MasterDeptTB.DeptName, VT.DesignID as VacancyName , Title as DesigName, Quota,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=VT.VacancyID ) as NOofInterview,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=VT.VacancyId ) as SelectedCandidate, VacancyStatus from VancancyTB VT Left outer join CompanyInfoTB on VT.CompanyID=CompanyInfoTB.CompanyID left outer join MasterDeptTB on VT.DeptID=MasterDeptTB.DeptID Where  VT.VacancyStatus <>'Approved' ");
            if (dtfecthvacancyDetails.Rows.Count > 0)
            {
                 grdAtt.DataSource = dtfecthvacancyDetails;
                 grdAtt.DataBind();
                 lblcnt.Text = dtfecthvacancyDetails.Rows.Count.ToString();
            }
            else
            {
                grdAtt.DataSource = null;
                grdAtt.DataBind();
                lblcnt.Text = "0";
            }
            }
            else
            {
                DataTable dtfecthvacancyDetails = g.ReturnData("Select VT.VacancyId, VT.VacancyStatus, CompanyInfoTB.CompanyName,MasterDeptTB.DeptName,Title As DesigName , DesignID as VacancyName, Quota,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=VT.VacancyID ) as NOofInterview,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=VT.VacancyId ) as SelectedCandidate, VacancyStatus from VancancyTB VT Left outer join CompanyInfoTB on VT.CompanyID=CompanyInfoTB.CompanyID left outer join MasterDeptTB on VT.DeptID=MasterDeptTB.DeptID where ( select Approvedby from VacDetTB where VacID=VT.VacancyId and Approvedby='" + Convert.ToInt32(Session["UserId"]) + "')='" + Convert.ToInt32(Session["UserId"]) + "'");
                if (dtfecthvacancyDetails.Rows.Count > 0)
                {
                    grdAtt.DataSource = dtfecthvacancyDetails;
                    grdAtt.DataBind();
                    lblcnt.Text = dtfecthvacancyDetails.Rows.Count.ToString();
                }
                else
                {
                    grdAtt.DataSource = null;
                    grdAtt.DataBind();
                    lblcnt.Text = "0";
                }
            }

          
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void binddesignation()
    {
        try
        {
           
                var dt = from p in hr.MasterDesgTBs
                         where p.Status == 0 && p.DeptID == Convert.ToInt32(ddlDept.SelectedValue)
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
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void fillcompany()
    {
        try
        {
            var dt = from p in hr.CompanyInfoTBs
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
        }
        catch (Exception ex)
        {
            g.ShowMessage(this.Page, ex.Message);
           
        }
    }

    private void fillcity()
    {
        try
        {
            var dtcity = from d in hr.CityTBs
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

    private void bindemp()
    {
        try
        {
           
                var dt = from p in hr.EmployeeTBs
                         where p.RelivingStatus == null && p.CompanyId == Convert.ToInt32(ddlComp.SelectedValue)
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
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void BindDept()
    {
        try
        {
            if (ddlComp.SelectedIndex !=-1)
            {
            
            var dt = from p in hr.MasterDeptTBs
                     where p.Status == 0 && p.CompanyId == Convert.ToInt32(ddlComp.SelectedValue)
                     select new { p.DeptID, p.DeptName };
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
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbType.SelectedIndex == 2)
        {
            tdOther.Visible = true;
        }
        else
        {
            tdOther.Visible = false;
        }
    }

    protected void rd_approve_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_approve.SelectedIndex==0)
        {
            btnsubmit.Text = "Approve";
        }
        else
        {
            btnsubmit.Text = "Not Approve";
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

    protected void fetchvacancydt()
    {
        try
        {
        VancancyTB v = hr.VancancyTBs.Where(d => d.VacancyID == Convert.ToInt32(lblid.Text)).First();
        fillcompany();
        ddlComp.SelectedValue = Convert.ToString(v.CompanyID);
        BindDept();
        ddlDept.SelectedValue = Convert.ToString(v.DeptID);
        binddesignation();
        ddlDesign.SelectedValue = Convert.ToString(v.DesignID);
        txtCode.Text = v.Code;
        txtTitle.Text = v.Title;
        txtQuota.Text = v.Quota;
        rbType.SelectedIndex =Convert.ToInt32(v.EmployeeType);

        txtSkills.Text = v.Skills;
        txtExpr.Text = v.Expr;
        fillcity();
        ddlcity.SelectedValue = Convert.ToString(v.City_Place);
        txtSalary.Text = v.Salary;

        txtDesc.Text = v.Descript;

        string s = v.Qualification;
        string[] str = s.Split(',');

        for (int i = 0; i < str.Count(); i++)
        {
            for (int j = 0; j < lstQualifi.Items.Count; j++)
            {
                if (str[i].ToString() == lstQualifi.Items[j].Text)
                {
                    lstQualifi.Items[j].Selected = true;
                }
            }
            
        }
        bindemp();
        DataTable fetchapprove = g.ReturnData("select  Approvedby from VacDetTB where VacID='" + Convert.ToInt32(lblid.Text) + "'");
        if (fetchapprove.Rows.Count > 0)
        {
            for (int i = 0; i < fetchapprove.Rows.Count; i++)
            {
                for (int j = 0; j < lstEmp.Items.Count; j++)
                {
                    if (fetchapprove.Rows[i]["Approvedby"].ToString() == lstEmp.Items[j].Value)
                    {
                        lstEmp.Items[j].Selected = true;
                    } 
                }
                
            }
        }
  // Document Details............
        var Docu = from dd in hr.VacancyDocumentTBs
                   where dd.Vacancy_Id == Convert.ToInt32(lblid.Text)
                   select new
                   {
                       dd.Document_Name,
                       dd.Document_Path,

                   };
        if (Docu.Count() > 0 && Docu != null)
        {
            grdnewdoc.DataSource = Docu;
            grdnewdoc.DataBind();
        }
        else
        {
            grdnewdoc.DataSource = null;
            grdnewdoc.DataBind();

        }


        DataTable daatadd = g.ReturnData(" SELECT Document_Name, Document_Path  FROM VacancyDocumentTB where Vacancy_Id='" + Convert.ToInt32(lblid.Text) + "'");
        if (daatadd.Rows.Count > 0)
        {
            ViewState["dt1"] = daatadd;
        }
        else
        {
            ViewState["dt1"] = null;
        }


        }
        catch (Exception)
        {

            throw;
        }
        
  }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
       

            if (IsValid())
            {

                var dt = from p in hr.VancancyTBs.Where(d => d.CompanyID == Convert.ToInt32(ddlComp.SelectedValue)
                    && d.DesignID == Convert.ToInt32(ddlDesign.SelectedValue) && d.Code == txtCode.Text && d.VacancyID != Convert.ToInt32(lblid.Text)) 
                         select p;
                if (dt.Count() > 0)
                {
                    g.ShowMessage(this.Page, "Vacancy Code Already Exist");
                }
                else
                {
                    #region
                    VancancyTB v = hr.VancancyTBs.Where(s => s.VacancyID == Convert.ToInt32(lblid.Text)).First();
                    v.Code = txtCode.Text;
                    v.CompanyID = Convert.ToInt32(ddlComp.SelectedValue);
                    v.EmployeeType = rbType.SelectedIndex.ToString();
                    v.Descript = txtDesc.Text;
                    v.Expr = txtExpr.Text;
                    if (tdOther.Visible)
                    {
                        v.Other = txtOther.Text;
                    }
                    v.Place = ddlcity.SelectedItem.Text;
                    v.City_Place = Convert.ToInt32(ddlcity.SelectedValue);
                    v.Quota = txtQuota.Text;

                    string q = "";
                    for (int i = 0; i < lstQualifi.Items.Count; i++)
                    {
                        if (lstQualifi.Items[i].Selected)
                        {
                            q = lstQualifi.Items[i].Text + ',' + q;
                        }
                    }
                    v.Qualification = q;

                    v.Salary = txtSalary.Text;
                    v.Skills = txtSkills.Text;
                    v.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
                    // column name changes because some problems of title & Designation for Position
                    v.Title =  txtTitle.Text;
                    v.DesignID = Convert.ToInt32(ddlDesign.SelectedValue);
                    


                    if (btnsubmit.Text == "Approve")
                    {
                        v.VacancyStatus = "Approved";
                    }
                    if (btnsubmit.Text == "Not Approve")
                    {

                        v.VacancyStatus = "Not Approved";

                    }
                    hr.SubmitChanges();
                    //// Apporved by Save........
                    //string itemdelet1 = "delete from VacDetTB where VacID='" + id + "'";
                    //DataSet ds1 = g.ReturnData1(itemdelet1);

                    //for (int i = 0; i < lstEmp.Items.Count; i++)
                    //{
                    //    if (lstEmp.Items[i].Selected)
                    //    {
                    //        VacDetTB va = new VacDetTB();

                    //        va.Approvedby = Convert.ToInt32(lstEmp.Items[i].Value);
                    //        va.VacID = v.VacancyID;
                    //        hr.VacDetTBs.InsertOnSubmit(va);
                    //        hr.SubmitChanges();
                    //    }
                    //}


                    // Document delete & newly Save........
                    string itemdelet3 = "delete from VacancyDocumentTB where Vacancy_Id='" + Convert.ToInt32(lblid.Text) + "'";
                    DataSet ds3 = g.ReturnData1(itemdelet3);
                    if (ViewState["dt1"] != null)
                    {
                        dt1 = ViewState["dt1"] as DataTable;

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            VacancyDocumentTB D = new VacancyDocumentTB();
                            D.Vacancy_Id = v.VacancyID;
                            D.Document_Name = dt1.Rows[i][0].ToString();
                            D.Document_Path = dt1.Rows[i][1].ToString();
                            hr.VacancyDocumentTBs.InsertOnSubmit(D);
                            hr.SubmitChanges();

                        }
                        ViewState["dt1"] = null;
                        grdnewdoc.DataSource = null;
                        grdnewdoc.DataBind();
                    }
                    string app = "";
                    if (btnsubmit.Text=="Approve")
                    {
                        app = "Approve";
                    }
                    else
                    {
                        app = "Not Approve";
                    }
                    DataTable updateVacapprovedby = g.ReturnData("UPDATE VacDetTB SET AfterApprovedBy='" + Convert.ToInt32(Session["UserId"]) + "', Approve_Status='" + app + "'    WHERE VacID='" + Convert.ToInt32(lblid.Text) + "' AND Approvedby='" + Convert.ToInt32(Session["UserId"]) + "'");


                    var checkallapproval = from p in hr.VacDetTBs.Where(d => d.VacID == Convert.ToInt32(lblid.Text)
                    && d.AfterApprovedBy == null && d.Approve_Status == null || d.Approve_Status == "Not Approve")
                             select p;
                    if (checkallapproval.Count() > 0)
                    {                      
                    
                    }
                    else
                    {
                        VancancyTB vv = hr.VancancyTBs.Where(s => s.VacancyID == Convert.ToInt32(lblid.Text)).First();
                        vv.VacancyStatus = "Approved";
                        hr.SubmitChanges();
                    }
                        g.ShowMessage(this.Page, "Vacancy Approval Done Successfully....");
                        bindgrdvacancy();
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
        ddlComp.Items.Clear();
        ddlDept.Items.Clear();
        ddlDesign.Items.Clear();
        for (int i = 0; i < lstQualifi.Items.Count; i++)
        {
            lstQualifi.Items[i].Selected = false;
        }
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveVacancyForm.aspx");
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDept.SelectedIndex > 0)
            {
                var dt = from p in hr.MasterDesgTBs
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
        if (ddlComp.SelectedIndex != -1)
        {
            BindDept();
            bindemp();
        }
    }




    #region if required sort by search then use as floww

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
                                  .Where(r => r.Title.Contains(prefixText))
                                select d.Title).Distinct().ToList();
        return vacancy;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListDepartment(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> department = (from d in hr.MasterDeptTBs
                                  .Where(r => r.DeptName.Contains(prefixText))
                                   select d.DeptName).Distinct().ToList();
        return department;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListDesig(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> desig = (from d in hr.MasterDesgTBs
                                  .Where(r => r.DesigName.Contains(prefixText))
                              select d.DesigName).Distinct().ToList();
        return desig;
    }


    #region fetch Id for all searching TextBox Like CompanyId, deparmentId..
    protected void txtcompany_TextChanged(object sender, EventArgs e)
    {
        if (txtcompany.Text != "")
        {
            var findcompanyID = from d in hr.CompanyInfoTBs
                                where d.CompanyName == txtcompany.Text
                                select new { d.CompanyId };
            if (findcompanyID.Count() > 0)
            {
                foreach (var item in findcompanyID)
                {
                    lblcompaniId.Text = item.CompanyId.ToString();
                }
            }
            else
            {
                lblcompaniId.Text = "";
            }

        }
    }

    protected void txtdepart_TextChanged(object sender, EventArgs e)
    {
        if (txtdepart.Text != "")
        {
            var finddeptID = from d in hr.MasterDeptTBs
                             where d.DeptName == txtdepart.Text
                             select new { d.DeptID };
            if (finddeptID.Count() > 0)
            {
                foreach (var item in finddeptID)
                {
                    lbldeptId.Text = item.DeptID.ToString();
                }
            }
            else
            {
                lbldeptId.Text = "";
            }
        }
    }
    protected void txtdesig_TextChanged(object sender, EventArgs e)
    {
        if (txtdesig.Text != "")
        {
            var finddesigID = from d in hr.MasterDesgTBs
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


    #endregion
    protected void grdAtt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAtt.PageIndex = e.NewPageIndex;
        bindgrdvacancy();
    }
     protected void Lnknedit_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        LinkButton lnkid = (LinkButton)sender;
        lblid.Text = lnkid.CommandArgument;
        //id = Convert.ToInt32(lblid.Text);
         fetchvacancydt();
    }
}