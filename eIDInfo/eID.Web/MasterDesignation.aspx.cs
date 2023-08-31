using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterDesignation : System.Web.UI.Page
{
    public bool salary_flag = false;
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

    Genreal g = new Genreal();
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    salary_flag = Convert.ToBoolean(ConfigurationManager.AppSettings["daily_salary_setup"]);
                }
                catch(Exception ex) { }

                MultiView1.ActiveViewIndex = 0;
                fillcompany();
                BindAllDesig();

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void fillcompany()
    {

        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());

        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
    }

    private void fillDept(int companyid)
    {
        var data = (from dt in HR.MasterDeptTBs
                   where dt.Status == 1 && dt.CompanyId==companyid
                   select dt).OrderBy(d=>d.DeptName);
        if (data != null && data.Count() > 0)
        {

            ddldept.DataSource = data;
            ddldept.DataTextField = "DeptName";
            ddldept.DataValueField = "DeptID";
            ddldept.DataBind();
            ddldept.Items.Insert(0, "--Select--");
        }
    }

    public void BindAllDesig()
    {

        var deptData = (from d in HR.MasterDesgTBs
                       join dtt in HR.MasterDeptTBs
                       on d.DeptID equals dtt.DeptID
                       join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                       where d.Status == 1
                       select new { d.DesigID, 
                           d.CompanyId,
                           d.DesigName, 
                           dtt.DeptName,
                           c.CompanyName,
                           d.TenantId,
                                    Status = d.Status == 1 ? "Active" : "In Active"
                       }).OrderBy(d => d.CompanyName).ThenBy(d=>d.DeptName).ThenBy(d=>d.DesigName).ToList();

        if (Session["UserType"].ToString() != "SuperAdmin")
        {
            deptData = deptData.Where(d => d.TenantId == Session["TenantId"].ToString()).ToList();
        }

        if (deptData.Count() > 0)
        {
            grd_Dept.DataSource = deptData;
            grd_Dept.DataBind();
        }


    }

    //protected void OnClick_Edit(object sender, EventArgs e)
    //{
    // Change by Abdul rahman 2/12/2014
     protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string DesgId = Lnk.CommandArgument;
        MultiView1.ActiveViewIndex = 1;
        MasterDesgTB MT = HR.MasterDesgTBs.Where(d => d.DesigID == Convert.ToInt32(DesgId)).First();
        lbldesid.Text = DesgId;
        ddlCompany.SelectedValue = MT.CompanyId.ToString();
        fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
        ddldept.SelectedValue = MT.DeptID.ToString();
        txtdesigname.Text = MT.DesigName;
       
        btnsubmit.Text = "Update";

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
             var dte = from p in HR.MasterDesgTBs.Where(d => d.DesigName == txtdesigname.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.DeptID == Convert.ToInt32(ddldept.SelectedValue)) select p;
             if (dte.Count() > 0)
             {
                 g.ShowMessage(this.Page, "Designation Details Already Exist");
                 //modpop.Message = "Designation Name Already Exist";
                 //modpop.ShowPopUp();
              
             }
             else
             {
                 MasterDesgTB MTB = new MasterDesgTB();
                 MTB.DesigName = txtdesigname.Text;
                 MTB.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                 MTB.DeptID = Convert.ToInt32(ddldept.SelectedValue);
                 MTB.Status = 1;
                MTB.TenantId = Convert.ToString(Session["TenantId"]);
                HR.MasterDesgTBs.InsertOnSubmit(MTB);
                 HR.SubmitChanges();
                 g.ShowMessage(this.Page, "Designation Details Saved Successfully");
                 //modpop.Message = "Submitted Successfully";
                 //modpop.ShowPopUp();
              
                 Clear();
             }
        }
        else
        {
            var dte = from p in HR.MasterDesgTBs.Where(d => d.DesigName == txtdesigname.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) 
                && d.DeptID == Convert.ToInt32(ddldept.SelectedValue) && d.DesigID == Convert.ToInt32(lbldesid.Text)) select p;
            if (dte.Count() > 0)
            {

                updatecode();
                //modpop.Message = "Designation Name Already Exist";
                //modpop.ShowPopUp();
                g.ShowMessage(this.Page, "Designation Details Already Exist");

            }
            else
            {
                 var dte1 = from p in HR.MasterDesgTBs.Where(d => d.DesigName == txtdesigname.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) 
                 && d.DeptID == Convert.ToInt32(ddldept.SelectedValue) && d.DesigID != Convert.ToInt32(lbldesid.Text)) select p;
                 if (dte1.Count() > 0)
                 {
                     g.ShowMessage(this.Page, "Designation Details Already Exist");
                 }
                 else
                 {
                     updatecode();
                 }
            }
            

           
        }
    }

    private void updatecode()
    {
        try
        {
            MasterDesgTB MT = HR.MasterDesgTBs.Where(d => d.DesigID == Convert.ToInt32(lbldesid.Text)).First();
            MT.DesigName = txtdesigname.Text;
            MT.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            MT.DeptID = Convert.ToInt32(ddldept.SelectedValue);
            MT.Status = 1;
            MT.TenantId = Convert.ToString(Session["TenantId"]);
            HR.SubmitChanges();
            g.ShowMessage(this.Page,"Designation Details Updated Successfully");
            btnsubmit.Text = "Save";
            Clear();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    public void Clear()
    {
        txtdesigname.Text = null;
        lbldesid.Text = "";
        ddlCompany.SelectedIndex = 0;
        ddldept.Items.Clear();
     
      
        BindAllDesig();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
         fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
    }

    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Dept.PageIndex = e.NewPageIndex;
        BindAllDesig();
        grd_Dept.DataBind();
    }

    protected void grd_Dept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Dept.Rows.Count > 0)
        {
            grd_Dept.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Dept.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.PostedFile != null)
            {


                string excelPath = string.Concat(Server.MapPath("~/Attachments/" + FileUpload1.PostedFile.FileName));
                FileUpload1.SaveAs(excelPath);

                string conString = string.Empty;
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;

                }
                conString = string.Format(conString, excelPath, "YES");
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE Designation IS NOT NULL", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();

                    #region Add Excel In Database
                   
                    int counter = 0;
                    for (int i = 0; i < dtExcelData.Rows.Count; i++)
                    {
                        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                        {
                            string Company = Convert.ToString(dtExcelData.Rows[i]["Company"]);
                            string Department = Convert.ToString(dtExcelData.Rows[i]["Department"]);
                            string Designation = Convert.ToString(dtExcelData.Rows[i]["Designation"]);
                            
                            try
                            {
                                int companyId = 0; int deptId = 0; int desigId = 0;
                                #region get company data
                                var compData = db.CompanyInfoTBs.FirstOrDefault(d => d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyName == Company);
                                companyId = compData != null ? compData.CompanyId : 0;
                             bool   isPresentComp = compData != null ? true : false;


                                #endregion
                                #region get department data
                                var deptData = db.MasterDeptTBs.FirstOrDefault(d => d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == companyId && d.DeptName == Department);
                                deptId = deptData != null ? deptData.DeptID : 0;
                              bool  isPresentDept = deptData != null ? true : false;
                                #endregion
                                
                                if (isPresentComp && isPresentDept )
                                {
                                    MasterDesgTB desgTB = new MasterDesgTB();
                                    desgTB.CompanyId = companyId;
                                    desgTB.DeptID = deptId;
                                    desgTB.DesigName = Designation;
                                    desgTB.Status = 1;
                                    desgTB.TenantId = Convert.ToString(Session["TenantId"]);
                                    db.MasterDesgTBs.InsertOnSubmit(desgTB);
                                    db.SubmitChanges();
                                    
                                    counter++;
                                }
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                    }
                    if (counter > 0)
                    {
                        g.ShowMessage(this.Page, counter + " Employee imported successfully out of : " + dtExcelData.Rows.Count);
                    }
                   
                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ibtnSalarySetup_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton=(ImageButton)sender;
        int desigId = Convert.ToInt32(imageButton.CommandArgument);

        if (desigId == 0)
        {
            g.ShowMessage(this.Page, "Invalid operation..");
        }
        else
        {
            Response.Redirect($"setup_salary_desigwise.aspx?desig={desigId}");
        }
    }

    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string DesgId = Lnk.CommandArgument;       
        MasterDesgTB MT = HR.MasterDesgTBs.Where(d => d.DesigID == Convert.ToInt32(DesgId)).First();

        MT.Status = 0;
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "Designation Delete Successfully.....");
        Response.Redirect("MasterDesignation.aspx");

    }
}