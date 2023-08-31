using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
//using ServiceReference1;
using System.Data;
using System.IO;


public partial class _Default : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    DataTable Dt = new DataTable();
    DataTable DtExperience = new DataTable();
    string AttachPath;
    Genreal g = new Genreal();
    String Employeeid;
    int ii;
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                FillGrade();
                DataColumn EducationId = Dt.Columns.Add("EducationId");
                DataColumn EducationName = Dt.Columns.Add("EducationName");
                DataColumn College = Dt.Columns.Add("College");
                DataColumn University = Dt.Columns.Add("University");
                DataColumn YearOfPassing = Dt.Columns.Add("YearOfPassing");
                DataColumn ObtainPercent = Dt.Columns.Add("ObtainPercent");




                MultiView1.ActiveViewIndex = 0;
                //ddlCountry.SelectedIndex = 1;
                BindAllEmp();
                fillCountry();
                fillcompany();
                fillCountry1();
                fillEducation();
                FillCompanyList();

                fillReportingHead();
                string empid11 = Request.QueryString["Empid"];
                if (Request.QueryString["Empid"] != null)
                {
                    MultiView1.ActiveViewIndex = 1;
                    editemp(int.Parse(empid11));
                }
                else
                {
                    DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                    DataColumn Location = DtExperience.Columns.Add("Location");
                    DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                    DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                    DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                    DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                    DataColumn Department = DtExperience.Columns.Add("Department");
                    DataColumn Designation = DtExperience.Columns.Add("Designation");

                }


                Employeeid = Session["UserId"].ToString();
            }
            else
            {
                string FolderPath = Server.MapPath("Attachments");
                MakeDirectoryIfExist(FolderPath);
                if (FileUpload1.HasFile)
                {
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("Attachments/" + filename));
                    AttachPath = filename;
                    image1.ImageUrl = "~/Attachments/" + AttachPath;
                    lblAttachPath.Text = AttachPath;
                    image1.Visible = true;
                }
            }

            if (grd_Emp.Rows.Count > 0)
                grd_Emp.HeaderRow.TableSection = TableRowSection.TableHeader;
            BindJqFunctions();
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

    private void FillEmployeeList()
    {
        try
        {
            if (ddlCompanyList.SelectedIndex == 0)
            {
                //All Com Data..
                var data = (from dtReportHead in HR.EmployeeTBs
                            join dep in HR.MasterDeptTBs on dtReportHead.CompanyId equals dep.CompanyId
                            where dtReportHead.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)
                            select new
                            {
                                dtReportHead.EmployeeId,
                                Name = dtReportHead.FName + ' ' + dtReportHead.MName + ' ' + dtReportHead.Lname
                            }).OrderBy(d => d.Name).Distinct();
                if (data != null && data.Count() > 0)
                {

                    ddlEmployeeList.DataSource = data;
                    ddlEmployeeList.DataTextField = "Name";
                    ddlEmployeeList.DataValueField = "EmployeeId";
                    ddlEmployeeList.DataBind();
                    ddlEmployeeList.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddlEmployeeList.Items.Clear();
                }
            }
            else
            {
                if (ddlDepartment.SelectedIndex == 0)
                {
                    //Singla Company All Depat data
                    var data = (from dtReportHead in HR.EmployeeTBs
                                join dep in HR.MasterDeptTBs on dtReportHead.CompanyId equals dep.CompanyId
                                where dtReportHead.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue)
                                select new
                                {
                                    dtReportHead.EmployeeId,
                                    Name = dtReportHead.FName + ' ' + dtReportHead.MName + ' ' + dtReportHead.Lname
                                }).OrderBy(d => d.Name).Distinct();
                    if (data != null && data.Count() > 0)
                    {

                        ddlEmployeeList.DataSource = data;
                        ddlEmployeeList.DataTextField = "Name";
                        ddlEmployeeList.DataValueField = "EmployeeId";
                        ddlEmployeeList.DataBind();
                        ddlEmployeeList.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddlEmployeeList.Items.Clear();
                    }
                }
                else
                {
                    //Singla dep wise data.
                    var data = (from dtReportHead in HR.EmployeeTBs
                                join dep in HR.MasterDeptTBs on dtReportHead.CompanyId equals dep.CompanyId
                                where dtReportHead.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue) && dtReportHead.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)
                                select new
                                {
                                    dtReportHead.EmployeeId,
                                    Name = dtReportHead.FName + ' ' + dtReportHead.MName + ' ' + dtReportHead.Lname
                                }).OrderBy(d => d.Name).Distinct();
                    if (data != null && data.Count() > 0)
                    {

                        ddlEmployeeList.DataSource = data;
                        ddlEmployeeList.DataTextField = "Name";
                        ddlEmployeeList.DataValueField = "EmployeeId";
                        ddlEmployeeList.DataBind();
                        ddlEmployeeList.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddlEmployeeList.Items.Clear();
                    }
                }
            }
        }
        catch(Exception ex) { }
    }



    private void FillCompanyList()
    {
        var data = (from dt in HR.CompanyInfoTBs
                    where dt.Status == 0
                    select dt).OrderBy(d => d.CompanyName);
        if (data != null && data.Count() > 0)
        {

            ddlCompanyList.DataSource = data;
            ddlCompanyList.DataTextField = "CompanyName";
            ddlCompanyList.DataValueField = "CompanyId";
            ddlCompanyList.DataBind();

        }
        else
        {
            ddlCompanyList.DataSource = null;
            ddlCompanyList.DataBind();
        }
        ddlCompanyList.Items.Insert(0, "--Select--");
    }

    private void BindDepartment(string p)
    {
        if (ddlCompanyList.SelectedIndex > 0)
        {
            var data = (from dt in HR.CompanyInfoTBs
                        join dep in HR.MasterDeptTBs on dt.CompanyId equals dep.CompanyId
                        where dt.CompanyName == p
                        select dep).OrderBy(d => d.DeptName);

            if (data != null && data.Count() > 0)
            {

                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
            else
            {
                ddlDepartment.DataSource = null;
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, "--All--");
        }
    }
    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }
    public void BindAllEmp()
    {
        //bool Status = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        bool Status = g.CheckSuperAdmin(Convert.ToString(Session["UserId"]));
        var EmpData = (from d in HR.EmployeeTBs
                       join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID into empDept
                       from ed in empDept.DefaultIfEmpty()
                       join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId into empComp
                       from ec in empComp.DefaultIfEmpty()
                       select new
                       {
                           Name = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                           Name1 = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                           d.FName,
                           d.Lname,
                           d.EmployeeId,
                           d.BirthDate,
                           d.Email,
                           DOJ1 = d.DOJ,
                           d.PanNo,
                           d.ContactNo,
                           d.PassportNo,
                           ed.DeptName,
                           ec.CompanyName,
                           emnae = d.FName + " " + d.Lname,
                           d.RelivingDate,
                           d.EmployeeNo,
                           d.personalEmail,
                           d.CompanyId,
                           d.DeptId,
                           d.DesgId
                       }).ToList();

        if (Status == true)
        {
            //btnadd.Visible = false;
            btnadd.Visible = true;
        

            //if (txtFirstNameSearch.Text != "")
            //{
            //    EmpData = EmpData.Where(d => d.emnae.Contains(txtFirstNameSearch.Text));
            //}

            if (ddlCompany.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
            }
            if (ddldept.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.DeptId == Convert.ToInt32(ddldept.SelectedValue)).ToList();
            }
            if (ddlEmployeeList.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployeeList.SelectedValue)).ToList();
            }

            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                EmpData = EmpData.Where(d => d.EmployeeNo.Contains(txtEmployeeCode.Text.Trim())).ToList();
            }


         
         
            grd_Emp.DataSource = EmpData;
            //lblcnt.Text = EmpData.Count().ToString();

            grd_Emp.DataBind();

           

        }
        else
        {
            //t1.Visible = false;
            //btnadd.Visible = false;
            btnadd.Visible = true;
         
            //if (txtFirstNameSearch.Text != "")
            //{
            //    EmpData = EmpData.Where(d => d.emnae.Contains(txtFirstNameSearch.Text));
            //}
            if (ddlCompany.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
            }
            if (ddldept.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.DeptId == Convert.ToInt32(ddldept.SelectedValue)).ToList();
            }
            if (ddlEmployeeList.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployeeList.SelectedValue)).ToList();
            }

            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                EmpData = EmpData.Where(d => d.EmployeeNo.Contains(txtEmployeeCode.Text.Trim())).ToList();
            }
           
            grd_Emp.DataSource = EmpData;
            //lblcnt.Text = EmpData.Count().ToString();
            grd_Emp.DataBind();

            //for (int l = 0; l < grd_Emp.Rows.Count; l++)
            //{
                
            //    LinkButton lnkRelivedate = (LinkButton)grd_Emp.Rows[l].FindControl("LnkReliveDate");
            //    LinkButton LnkRejoin = (LinkButton)grd_Emp.Rows[l].FindControl("LnkRejoin");

            //    if (!string.IsNullOrEmpty(grd_Emp.Rows[l].Cells[8].Text) && grd_Emp.Rows[l].Cells[8].Text != "&nbsp;")
            //    {
            //        //  LnkRejoin.Visible = true;
            //        //   LnkRejoin.Enabled = false;
            //        //  LnkRejoin.Text = "Relieved";
            //        lnkRelivedate.Visible = false;
            //        lnkRelivedate.Enabled = false;
            //    }
            //    else
            //    {
            //        lnkRelivedate.Visible = true;
            //        lnkRelivedate.Enabled = true;
            //        lnkRelivedate.Text = "Relieving";
            //        // LnkRejoin.Visible = false;
            //    }
            //}

        }


       
    }


    private void fillCountry1()
    {
        var data = (from dt in HR.CountryTBs
                    select dt).OrderBy(d => d.CountryName);
        if (data != null && data.Count() > 0)
        {

            ddlCountryP.DataSource = data;
            ddlCountryP.DataTextField = "CountryName";
            ddlCountryP.DataValueField = "CountryId";
            ddlCountryP.DataBind();
            ddlCountryP.Items.Insert(0, "--Select--");

        }
        else
        {
            ddlCountryP.DataSource = data;
            ddlCountryP.DataTextField = "CountryName";
            ddlCountryP.DataValueField = "CountryId";
            ddlCountryP.DataBind();
            ddlCountryP.Items.Insert(0, "--Select--");

        }
    }

    private void fillReportingHead()
    {

        //int Empid123 = Convert.ToInt32(Session["UserId"]);

        //var empData1 = (from dd in HR.EmployeeTBs.Where(d => d.EmployeeId == Empid123)
        //                select dd.CompanyId).First();

        //int CMY = Convert.ToInt32(empData1.Value.ToString());



        if (ddlCompany.SelectedIndex != 0)
        {
            var GetEmpid = from NotRegId in HR.EmployeeTBs
                           where NotRegId.EmployeeId == Convert.ToInt32(Session["UserId"].ToString())
                           select new
                           {
                               NotRegId.CompanyId,
                           };

            if (GetEmpid.First().CompanyId != null)
            {
                var data = (from dtReportHead in HR.EmployeeTBs
                            where dtReportHead.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                            select new
                            {
                                dtReportHead.EmployeeId,
                                Name = dtReportHead.FName + " " + dtReportHead.MName + " " + dtReportHead.Lname
                            }).OrderBy(d => d.Name);

                if (data != null && data.Count() > 0)
                {
                    ddlReportingHead.DataSource = data;
                    ddlReportingHead.DataTextField = "Name";
                    ddlReportingHead.DataValueField = "EmployeeId";
                    ddlReportingHead.DataBind();
                    ddlReportingHead.Items.Insert(0, "--Select--");

                }
                else
                {
                    ddlReportingHead.Items.Clear();
                }
            }
            else
            {
                var data = (from dtReportHead in HR.EmployeeTBs
                            select new
                            {
                                dtReportHead.EmployeeId,
                                Name = dtReportHead.FName + " " + dtReportHead.MName + " " + dtReportHead.Lname
                            }).OrderBy(d => d.Name);

                if (data != null && data.Count() > 0)
                {
                    ddlReportingHead.DataSource = data;
                    ddlReportingHead.DataTextField = "Name";
                    ddlReportingHead.DataValueField = "EmployeeId";
                    ddlReportingHead.DataBind();
                    ddlReportingHead.Items.Insert(0, "--Select--");

                }
                else
                {
                    ddlReportingHead.Items.Clear();
                }
            }
        }
    }

    private void fillCountry()
    {
        var data = (from dt in HR.CountryTBs
                    select dt).OrderBy(d => d.CountryName);
        if (data != null && data.Count() > 0)
        {
            ddlCountry.DataSource = data;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "--Select--");

        }
        else
        {
            ddlCountry.DataSource = data;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "--Select--");

        }
    }
    private void fillState(int countryid)
    {
        var data = (from dt in HR.StateTBs
                    where dt.CountryId == countryid
                    select dt).OrderBy(d => d.StateName);
        if (data != null && data.Count() > 0)
        {

            ddlState.DataSource = data;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "--Select--");

        }
        else
        {
            ddlState.DataSource = null;
            ddlState.DataBind();
            ddlState.Items.Clear();

            ddlCity.Items.Clear();
            //DropDownList2.DataSource = null;
            //DropDownList2.DataBind();
        }
    }

    private void fillState1(int countryid)
    {
        var data = from dt in HR.StateTBs
                   where dt.CountryId == countryid
                   select dt;
        if (data != null && data.Count() > 0)
        {

            //DropDownList2.DataSource = data;
            //DropDownList2.DataTextField = "StateName";
            //DropDownList2.DataValueField = "StateId";
            //DropDownList2.DataBind();
            //DropDownList2.Items.Insert(0, "--Select--");

        }
        else
        {
            //DropDownList2.DataSource = null;
            //DropDownList2.DataBind();
            //DropDownList2.Items.Clear();


        }
    }

    private void fillCity(int Stateid)
    {
        var data = (from dt in HR.CityTBs
                    where dt.CountryID == Stateid
                    select dt).OrderBy(d => d.CityName);
        if (data != null && data.Count() > 0)
        {

            ddlCity.DataSource = data;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityId";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.DataSource = null;
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, "--Select--");
        }

    }
    private void fillState2(int countryid)
    {
        var data = (from dt in HR.StateTBs
                    where dt.CountryId == countryid
                    select dt).OrderBy(d => d.StateName);
        if (data != null && data.Count() > 0)
        {
            ddlStateP.DataSource = data;
            ddlStateP.DataTextField = "StateName";
            ddlStateP.DataValueField = "StateId";
            ddlStateP.DataBind();
            ddlStateP.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlStateP.DataSource = null;
            ddlStateP.DataBind();
            ddlStateP.Items.Clear();

            ddlCityP.Items.Clear();
        }
    }
    private void fillCity2(int Stateid)
    {
        var data = (from dt in HR.CityTBs
                    where dt.CountryID == Stateid
                    select dt).OrderBy(d => d.CityName);
        if (data != null && data.Count() > 0)
        {

            ddlCityP.DataSource = data;
            ddlCityP.DataTextField = "CityName";
            ddlCityP.DataValueField = "CityId";
            ddlCityP.DataBind();
            ddlCityP.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlCityP.Items.Clear();
            ddlCityP.DataSource = null;
            ddlCityP.DataBind();

            ddlCityP.Items.Insert(0, "--Select--");
        }
    }
    private void fillcompany()
    {
        var data = (from dt in HR.CompanyInfoTBs
                    where dt.Status == 0
                    select dt).OrderBy(d => d.CompanyName);
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }

    }
    private void fillEducation()
    {
        var data = (from dt in HR.MasterEducationTBs
                    where dt.Status == 0
                    select dt).OrderBy(d => d.EducationName);
        if (data != null && data.Count() > 0)
        {

            ddeducation.DataSource = data;
            ddeducation.DataTextField = "EducationName";
            ddeducation.DataValueField = "EducationId";
            ddeducation.DataBind();
            ddeducation.Items.Insert(0, "--Select--");
        }

    }
    private void fillDept(int companyid)
    {
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 0 && dt.CompanyId == companyid
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {

            ddldept.DataSource = data;
            ddldept.DataTextField = "DeptName";
            ddldept.DataValueField = "DeptID";
            ddldept.DataBind();
            ddldept.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldept.DataSource = null;
            ddldept.DataBind();
            ddldept.Items.Clear();
        }
    }

    private void fillDesignation(int deptid)
    {
        var data = (from dt in HR.MasterDesgTBs
                    where dt.Status == 0 && dt.DeptID == deptid
                    select dt).OrderBy(d => d.DesigName);
        if (data != null && data.Count() > 0)
        {

            dddesg.DataSource = data;
            dddesg.DataTextField = "DesigName";
            dddesg.DataValueField = "DesigID";
            dddesg.DataBind();
            dddesg.Items.Insert(0, "--Select--");
        }
        else
        {

            dddesg.DataSource = null;
            dddesg.DataBind();
            dddesg.Items.Clear();

        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
            fillReportingHead();
        }
        catch
        {

        }
    }
    protected void dddept_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDesignation(Convert.ToInt32(ddldept.SelectedValue));
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
        {
          //  fillState(Convert.ToInt32(ddlCountry.SelectedValue));
            fillCity(Convert.ToInt32(ddlCountry.SelectedValue));
        }
        else
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            fillCity(Convert.ToInt32(ddlState.SelectedValue));
        }
        else
        {
            ddlCity.Items.Clear();
        }
    }
    protected void ddlCountryP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountryP.SelectedIndex != 0)
        {
           //fillState2(Convert.ToInt32(ddlCountryP.SelectedValue));
            fillCity2(Convert.ToInt32(ddlCountryP.SelectedValue));
        }
        else
        {
            ddlStateP.Items.Clear();
            ddlCityP.Items.Clear();
        }
    }
    protected void ddlStateP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStateP.SelectedIndex != 0)
        {
            fillCity2(Convert.ToInt32(ddlStateP.SelectedValue));
        }
        else
        {
            ddlCityP.Items.Clear();
        }
    }
    protected void chksame_CheckedChanged(object sender, EventArgs e)
    {
        if (chksame.Checked == true)
        {
            // txtAddressP.Text = txtAddress.Text;
            ddlCountryP.SelectedValue = ddlCountry.SelectedValue;
            //fillState2(Convert.ToInt32(ddlCountryP.SelectedValue));
            //ddlStateP.SelectedValue = ddlState.SelectedValue;
            //if (ddlStateP.SelectedIndex != 0)
            fillCity2(Convert.ToInt32(ddlCountry.SelectedValue));
            ddlCityP.SelectedValue = ddlCity.SelectedValue;
            txtpinP.Text = txtpin.Text;
            txtlandmarkP.Text = txtlandmark.Text;

            txtContact1.Text = txtContact0.Text;
            txtContactP.Text = txtContact.Text;
        }
        else
        {
            ddlCountryP.SelectedIndex = 0;
            ddlStateP.Items.Clear();
            //  ddlprState.SelectedIndex = 0;
            ddlCityP.Items.Clear();
            //   ddlprCity.SelectedIndex = 0;
            txtpinP.Text = " ";
            //   txtAddressP.Text = " ";
            txtContact1.Text = "";
            txtContactP.Text = "";

        }
    }
    protected void btnconP1_Click(object sender, EventArgs e)
    {
        #region Save Employee Profile..
        var chkExists = (from d in HR.EmployeeTBs where d.EmployeeNo == txtEmpCode.Text select d).Distinct();

        if (btnconP1.Text == "Save")
        {
            if (chkExists.Count() > 0) { g.ShowMessage(this.Page, "Employee coode already present.. Try another code.."); }
            else
            {
                //personal Information
                var maxid = (from m in HR.EmployeeTBs
                             select m.EmployeeId).Max();

                EmployeeTB emp = new EmployeeTB();
                emp.EmployeeNo = txtEmpCode.Text;
                emp.MachineID = txtBioId.Text;
                emp.Photo = lblAttachPath.Text;
                // emp.EmployeeId =int.Parse(txtempid.Text);
                //  emp.EmployeeNo = txtmachinid.Text;
                emp.Solitude = ddlsalitude.SelectedIndex;
                emp.FName = txtfname.Text;
                emp.MName = txtmname.Text;
                emp.Lname = txtlname.Text;
                emp.Gender = RbGender.SelectedValue;
                emp.MaritalStaus = rbMaritalStatus.SelectedValue;
                emp.BloodGroup = ddlBloodGroup.SelectedItem.Text;
                emp.BirthDate = DateTime.Parse(txtbirtdate.Text);
                emp.CompanyId = int.Parse(ddlCompany.SelectedValue);
                emp.DeptId = int.Parse(ddldept.SelectedValue);
                emp.DesgId = int.Parse(dddesg.SelectedValue);
                emp.ContactNo = txtcontactno.Text;
                emp.AltContactNo = txtaltcontact.Text;
                emp.Email = txtEmail.Text;

                if (ddlGrade.SelectedIndex > 0)
                {
                    emp.Grade = ddlGrade.SelectedItem.Text;
                }
                emp.EmpCardID = txtCardNo.Text;
                emp.personalEmail = txtpersonalEmail.Text;
                emp.PanNo = txtpannumber.Text;
                emp.PassportNo = txtpassportnumber.Text;
                emp.MachineID = txtBioId.Text;
                emp.DOJ = DateTime.Parse(txtcurrentjoiningdate.Text);
                emp.ProbationPeriod = txtprobation.Text;
                if (txtcnfrmdate.Text == null)
                {
                    emp.ConfirmDate = null;
                }
                else
                {
                    emp.ConfirmDate = DateTime.Parse(txtcnfrmdate.Text);
                }
                emp.NetSalary = txtsalary.Text;
                //account details
                emp.PFAccountNo = txtpfaccount.Text;
                emp.ESICAccountNo = txtesicaccnumber.Text;
                emp.SalaryAccountNo = txtaccount.Text;
                emp.BankName = txtbankname.Text;
                emp.Salarytype = ddlsalarytype.SelectedValue.ToString();

                //current Adderess
                emp.CurCountry = int.Parse(ddlCountry.SelectedValue);
                emp.CurState = int.Parse(ddlState.SelectedValue);
                emp.CurCity = int.Parse(ddlCity.SelectedValue);
                emp.CurPin = txtpin.Text;
                emp.CurLandmark = txtlandmark.Text;
                //renatal
                emp.CurRentName = txtContact0.Text;
                emp.CurRentContact = txtContact.Text;
                //permenent Adderess

                emp.PerCountry = int.Parse(ddlCountryP.SelectedValue);
                emp.PerState = int.Parse(ddlStateP.SelectedValue);
                emp.PerCity = int.Parse(ddlCityP.SelectedValue);
                emp.PerPin = txtpinP.Text;
                emp.PerLandmark = txtlandmarkP.Text;
                //renatal
                emp.PerRentName = txtContact1.Text;
                emp.PerRentContact = txtContactP.Text;

                if (ddlReportingHead.SelectedValue == null)
                {
                }
                else
                {
                    emp.ReportingStatus = Convert.ToInt32(ddlReportingHead.SelectedValue);
                }

                HR.EmployeeTBs.InsertOnSubmit(emp);
                HR.SubmitChanges();
                //Educational information
                DataTable dttt = (DataTable)ViewState["DT"];
                if (dttt != null)
                {
                    for (int i = 0; i < dttt.Rows.Count; i++)
                    {
                        EmpQualificationTB EmpQ = new EmpQualificationTB();
                        EmpQ.EmployeeId = (maxid + 1);
                        EmpQ.EducationId = int.Parse(dttt.Rows[i]["EducationId"].ToString());
                        EmpQ.College_School = dttt.Rows[i]["College"].ToString();
                        EmpQ.University = dttt.Rows[i]["University"].ToString();
                        EmpQ.YearofPassing = int.Parse(dttt.Rows[i]["YearofPassing"].ToString());
                        EmpQ.Perc = float.Parse(dttt.Rows[i]["ObtainPercent"].ToString());

                        HR.EmpQualificationTBs.InsertOnSubmit(EmpQ);
                        HR.SubmitChanges();

                    }
                }
                //Experiance information
                DataTable dttt1 = (DataTable)ViewState["DtExperience"];
                if (dttt1 != null)
                {
                    for (int i = 0; i < dttt1.Rows.Count; i++)
                    {
                        EmpExprienceTB EmpQ = new EmpExprienceTB();
                        EmpQ.EmployeeId = (maxid + 1);
                        EmpQ.CompanyName = dttt1.Rows[i]["CompanyName"].ToString();
                        EmpQ.CompanyAddress = dttt1.Rows[i]["Location"].ToString();
                        EmpQ.JoiningDate = DateTime.Parse(dttt1.Rows[i]["JoiningDate"].ToString());
                        EmpQ.RelvDate = DateTime.Parse(dttt1.Rows[i]["RelivingDate"].ToString());
                        EmpQ.RefPerson = dttt1.Rows[i]["RefPerson"].ToString();

                        EmpQ.refContactNo = dttt1.Rows[i]["ContactNo"].ToString();
                        EmpQ.Department = dttt1.Rows[i]["Department"].ToString();
                        EmpQ.Designation = dttt1.Rows[i]["Designation"].ToString();

                        HR.EmpExprienceTBs.InsertOnSubmit(EmpQ);
                        HR.SubmitChanges();
                        Clear1();

                    }
                }
                g.ShowMessage(this.Page, "Employee added  Successfully");
            }
        }
        #endregion End Employee Add....
        else
        {
            chkExists = chkExists.Where(d => d.EmployeeId != int.Parse(lblempid.Text)).Distinct();
            if (chkExists.Count() > 0) { g.ShowMessage(this.Page, "Employee coode already present.. Try another code.."); }
            else
            {
                #region Update Profile......
                EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(lblempid.Text)).First();
                MT.EmployeeNo = txtEmpCode.Text;
            MT.Photo = lblAttachPath.Text;
            MT.Solitude = Convert.ToInt32(ddlsalitude.SelectedIndex);
            MT.MachineID = txtBioId.Text;
            if (MT.FName == "")
            {
                MT.FName = txtfname.Text;
            }
            else
            {
                if (MT.FName != null)
                {
                    MT.FName = txtfname.Text;
                }
                else
                {
                    txtfname.Text = MT.FName;
                }
            }
            if (MT.MName == "")
            {
                MT.MName = txtmname.Text;
            }
            else
            {
                if (MT.MName != null)
                {
                    MT.MName = txtmname.Text;
                }
                else
                {
                    txtmname.Text = MT.MName;
                }
            }
            if (MT.Lname == "")
            {
                MT.Lname = txtlname.Text;
            }
            else
            {
                if (MT.Lname != null)
                {
                    MT.Lname = txtlname.Text;
                }
                else
                {
                    txtlname.Text = MT.Lname;
                }
            }
            if (MT.BirthDate.ToString() == "")
            {
                MT.BirthDate = Convert.ToDateTime(txtbirtdate.Text);
            }
            else
            {
                if (MT.BirthDate.ToString() != "")
                {
                    MT.BirthDate = Convert.ToDateTime(txtbirtdate.Text);
                }
                else
                {
                    txtbirtdate.Text = MT.BirthDate.Value.ToShortDateString();
                }
            }
            MT.Gender = RbGender.SelectedValue;
            //if (MT.Gender == "")
            //{

            //}
            //else
            //    if (MT.Gender != "")
            //    {
            //        MT.Gender = RbGender.SelectedValue;
            //    }
            //    else
            //    {
            //      RbGender.SelectedValue = MT.Gender ;
            //    }
            //}
            if (MT.MaritalStaus == "")
            {
                MT.MaritalStaus = rbMaritalStatus.Text;
            }
            else
            {
                if (MT.MaritalStaus != "")
                {
                    MT.MaritalStaus = rbMaritalStatus.Text;
                }
                else
                {
                    rbMaritalStatus.Text = MT.MaritalStaus;
                }
            }
            if (MT.BloodGroup == "")
            {
                MT.BloodGroup = ddlBloodGroup.SelectedValue;
            }
            else
            {
                if (MT.BloodGroup != "")
                {
                    MT.BloodGroup = ddlBloodGroup.SelectedValue;
                }
                else
                {
                    ddlBloodGroup.SelectedValue = MT.BloodGroup;
                }
            }
            if (MT.PanNo == "")
            {
                MT.PanNo = txtpannumber.Text;
            }
            else
            {
                if (MT.PanNo != "")
                {
                    MT.PanNo = txtpannumber.Text;
                }
                else
                {
                    txtpannumber.Text = MT.PanNo;
                }
            }
            if (MT.PassportNo == "")
            {
                MT.PassportNo = txtpassportnumber.Text;
            }
            else
            {
                if (MT.PassportNo != "")
                {
                    MT.PassportNo = txtpassportnumber.Text;
                }
                else
                {

                    txtpassportnumber.Text = MT.PassportNo;
                }
            }
            if (MT.Email == "")
            {
                MT.Email = txtEmail.Text;
            }
            else
            {
                if (MT.Email != "")
                {
                    MT.Email = txtEmail.Text;
                }
                else
                {
                    txtEmail.Text = MT.Email;
                }
            }
            // for Reporting Head

            if (MT.ReportingStatus == null || MT.ReportingStatus.ToString() == "")
            {

                MT.ReportingStatus = Convert.ToInt32(ddlReportingHead.SelectedValue);
            }
            else
            {
                if (MT.ReportingStatus != null || MT.ReportingStatus.ToString() != "")
                {
                    MT.ReportingStatus = Convert.ToInt32(ddlReportingHead.SelectedValue);

                }
                else
                {
                    (ddlReportingHead.SelectedValue) = Convert.ToString(MT.ReportingStatus);
                }
            }


            if (MT.personalEmail == "")
            {
                MT.personalEmail = txtpersonalEmail.Text;
            }
            else
            {
                if (MT.personalEmail != "")
                {
                    MT.personalEmail = txtpersonalEmail.Text;
                }
                else
                {
                    txtpersonalEmail.Text = MT.personalEmail;
                }
            }

            //if (MT.MachineID == "")
            //{
            //    MT.MachineID = txtmachinid.Text;
            //}
            //else
            //{

            //    if (MT.MachineID != "")
            //    {
            //        MT.MachineID = txtmachinid.Text;
            //    }
            //    else
            //    {
            //        txtmachinid.Text = MT.MachineID;
            //    }
            //}
            txtempid.Text = MT.EmployeeId.ToString();

            //ddlCompany.SelectedValue = MT.CompanyId.ToString();

            //ddldept.SelectedValue = MT.DeptId.ToString();

            //fillDesignation(int.Parse(MT.DeptId.ToString()));

            if (MT.CompanyId.ToString() == "")
            {
                MT.CompanyId = int.Parse(ddlCompany.SelectedValue);
            }
            else
            {
                if (MT.CompanyId.ToString() != "")
                {
                    MT.CompanyId = int.Parse(ddlCompany.SelectedValue);
                }
                else
                {
                    ddlCompany.SelectedValue = MT.CompanyId.ToString();
                }
            }

            //fillDept(int.Parse(MT.CompanyId.ToString()));

            if (MT.DeptId.ToString() == "")
            {
                MT.DeptId = int.Parse(ddldept.SelectedValue);
            }
            else
            {
                if (MT.DeptId.ToString() != "")
                {
                    MT.DeptId = int.Parse(ddldept.SelectedValue);
                }
                else
                {
                    ddldept.SelectedValue = MT.DeptId.ToString();
                }
            }


            if (Convert.ToString(MT.Grade) == "")
            {
                MT.Grade = ddlGrade.SelectedItem.Text;
            }
            else
            {
                if (Convert.ToString(MT.Grade) != "")
                {
                    MT.Grade = ddlGrade.SelectedItem.Text;
                }
                else
                {
                    ddlGrade.SelectedValue = MT.Grade.ToString();
                }
            }
            if (MT.ProbationPeriod == "")
            {
                MT.ProbationPeriod = txtprobation.Text;
            }
            else
            {
                if (MT.ProbationPeriod != "")
                {
                    MT.ProbationPeriod = txtprobation.Text;
                }
                else
                {
                    txtprobation.Text = MT.ProbationPeriod;
                }
            }
            if (MT.NetSalary == "")
            {
                MT.NetSalary = txtsalary.Text;
            }
            else
            {
                if (MT.NetSalary != "")
                {
                    MT.NetSalary = txtsalary.Text;
                }
                else
                {
                    txtsalary.Text = MT.NetSalary;
                }
            }
            //  fillDesignation(int.Parse(MT.DeptId.ToString()));
            if (MT.DesgId.ToString() == "")
            {
                    if(dddesg.SelectedIndex>0)
                MT.DesgId = Convert.ToInt32(dddesg.SelectedValue);
            }
            else
            {
                if (MT.DesgId.ToString() != "")
                {
                        if (dddesg.SelectedIndex > 0)
                            MT.DesgId = Convert.ToInt32(dddesg.SelectedValue);
                }
                else
                {
                    dddesg.SelectedValue = MT.DesgId.ToString();
                }
            }


            if (MT.ContactNo == "")
            {
                MT.ContactNo = txtcontactno.Text;
            }
            else
            {
                if (MT.ContactNo != "")
                {
                    MT.ContactNo = txtcontactno.Text;
                }
                else
                {
                    txtcontactno.Text = MT.ContactNo;
                }
            }

            if (MT.AltContactNo == "")
            {
                MT.AltContactNo = txtaltcontact.Text;
            }
            else
            {
                if (MT.AltContactNo != "")
                {
                    MT.AltContactNo = txtaltcontact.Text;
                }
                else
                {
                    txtaltcontact.Text = MT.AltContactNo;
                }
            }
            if (MT.DOJ.ToString() == "")
            {
                MT.DOJ = Convert.ToDateTime(txtcurrentjoiningdate.Text);
            }
            else
            {
                if (MT.DOJ.ToString() != "")
                {
                    MT.DOJ = Convert.ToDateTime(txtcurrentjoiningdate.Text);
                }
                else
                {
                    txtcurrentjoiningdate.Text = MT.DOJ.Value.ToShortDateString();
                }
            }
            if (MT.ConfirmDate.ToString() == "")
            {
                MT.ConfirmDate = Convert.ToDateTime(txtcnfrmdate.Text);
            }
            else
            {
                if (MT.ConfirmDate.ToString() != "")
                {
                    MT.ConfirmDate = Convert.ToDateTime(txtcnfrmdate.Text);
                }
                else
                {
                    txtcnfrmdate.Text = MT.ConfirmDate.Value.ToShortDateString();
                }
            }

            if (MT.PassportExpiryDate.ToString() == "")
            {
                if (Convert.ToString(txtExpiryDate.Text) != "")
                {
                    MT.PassportExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                }

            }
            else
            {
                if (Convert.ToString(txtExpiryDate.Text) != "")
                {
                    MT.PassportExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                }
                else
                {
                    txtExpiryDate.Text = MT.PassportExpiryDate.Value.ToShortDateString();
                }

            }

            if (MT.VisaExpiryDate.ToString() == "")
            {
                if (Convert.ToString(txtVisaExpiryDate.Text) != "")
                {
                    MT.VisaExpiryDate = Convert.ToDateTime(txtVisaExpiryDate.Text);
                }
            }
            else
            {
                if (Convert.ToString(txtVisaExpiryDate.Text) != "")
                {
                    MT.VisaExpiryDate = Convert.ToDateTime(txtVisaExpiryDate.Text);
                }
                else
                {
                    txtVisaExpiryDate.Text = MT.VisaExpiryDate.Value.ToShortDateString();
                }
            }


            if (MT.VisaNo == "")
            {
                MT.VisaNo = txtVisaNo.Text;
            }
            else
            {
                if (Convert.ToString(txtVisaNo.Text) != "")
                {
                    MT.VisaNo = txtVisaNo.Text;
                }
                else
                {
                    txtVisaNo.Text = MT.VisaNo;
                }
            }



            //account details
            if (MT.PFAccountNo == "")
            {
                MT.PFAccountNo = txtpfaccount.Text;
            }
            else
            {
                if (MT.PFAccountNo != "")
                {
                    MT.PFAccountNo = txtpfaccount.Text;
                }
                else
                {
                    txtpfaccount.Text = MT.PFAccountNo;
                }
            }
            if (MT.ESICAccountNo == "")
            {
                MT.ESICAccountNo = txtesicaccnumber.Text;
            }
            else
            {
                if (MT.ESICAccountNo != "")
                {
                    MT.ESICAccountNo = txtesicaccnumber.Text;
                }
                else
                {
                    txtesicaccnumber.Text = MT.ESICAccountNo;
                }
            }

            if (MT.SalaryAccountNo == "")
            {
                MT.SalaryAccountNo = txtaccount.Text;
            }
            else
            {
                if (MT.SalaryAccountNo != "")
                {
                    MT.SalaryAccountNo = txtaccount.Text;
                }
                else
                {
                    txtaccount.Text = MT.SalaryAccountNo;
                }
            }
            if (MT.BankName == "")
            {
                MT.BankName = txtbankname.Text;
            }
            else
            {
                if (MT.BankName != "")
                {
                    MT.BankName = txtbankname.Text;
                }
                else
                {
                    txtbankname.Text = MT.BankName;
                }
            }
            if ((MT.Salarytype == null) || (MT.Salarytype == "--Select--"))
            {
                MT.Salarytype = "--";
            }
            else
            {
                if ((MT.Salarytype != null) || (MT.Salarytype == "--Select--"))
                {
                    MT.Salarytype = "--";
                }
                else
                {
                    ddlsalarytype.Text = MT.Salarytype;
                }
            }
            //current Adderess
            if (MT.CurCountry.ToString() == "")
            {
                MT.CurCountry = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            else
            {
                if (MT.CurCountry.ToString() != "")
                {
                    MT.CurCountry = Convert.ToInt32(ddlCountry.SelectedValue);
                }
                else
                {
                    ddlCountry.SelectedValue = MT.CurCountry.ToString();
                }
            }
            //fillState(int.Parse(MT.CurCountry.ToString()));

            MT.EmpCardID = txtCardNo.Text;

            if (!string.IsNullOrEmpty(MT.CurState.ToString()))
            {
                MT.CurState = Convert.ToInt32(ddlState.SelectedValue);
            }
            else
            {
                //if (!string.IsNullOrEmpty(MT.CurState.ToString()))
                //{
                //    MT.CurState = Convert.ToInt32(ddlState.SelectedValue);
                //}
                //else
                //{
                //    ddlState.SelectedValue = MT.CurState.ToString();
                //}
            }
            //fillCity(int.Parse(MT.CurState.ToString()));
            if (MT.CurCity.ToString() == "")
            {
                MT.CurCity = Convert.ToInt32(ddlCity.SelectedValue);
            }
            else
            {
                if (MT.CurCity.ToString() != "")
                {
                    //ddlCity.SelectedValue = MT.CurCity.ToString();
                    MT.CurCity = Convert.ToInt32(ddlCity.SelectedValue);
                }
                else
                {
                    ddlCity.SelectedValue = MT.CurCity.ToString();
                }
            }
            if (MT.CurPin == "")
            {
                MT.CurPin = txtpin.Text;
            }
            else
            {
                if (MT.CurPin != "")
                {
                    MT.CurPin = txtpin.Text;
                }
                else
                {
                    txtpin.Text = MT.CurPin;
                }
            }
            if (MT.PerPin == "")
            {
                MT.PerPin = txtpinP.Text;
            }
            else
            {
                if (MT.PerPin != "")
                {
                    MT.PerPin = txtpinP.Text;
                }
                else
                {
                    txtpinP.Text = MT.PerPin.ToString();
                }
            }

            if (MT.CurLandmark == "")
            {
                MT.CurLandmark = txtlandmark.Text;
            }
            else
            {
                if (MT.CurLandmark != "")
                {
                    MT.CurLandmark = txtlandmark.Text;
                }
                else
                {
                    txtlandmark.Text = MT.CurLandmark;
                }

            }

            if (MT.PerLandmark == "")
            {
                MT.PerLandmark = txtlandmarkP.Text;
            }
            else
            {
                if (MT.PerLandmark != "")
                {
                    MT.PerLandmark = txtlandmarkP.Text;
                }
                else
                {
                    txtlandmarkP.Text = MT.PerLandmark.ToString();
                }
            }

            if (MT.CurRentName == "")
            {
                MT.CurRentName = txtContact0.Text;
            }
            else
            {
                if (MT.CurRentName != "")
                {
                    MT.CurRentName = txtContact0.Text;
                }
                else
                {
                    txtContact0.Text = MT.CurRentName;
                }
            }
            if (MT.CurRentContact == "")
            {
                MT.CurRentContact = txtContact.Text;
            }
            else
            {
                if (MT.CurRentContact != "")
                {
                    MT.CurRentContact = txtContact.Text;
                }
                else
                {
                    txtContact.Text = MT.CurRentContact;
                }
            }
            //permenant Address
            if (MT.PerCountry.ToString() == "")
            {
                MT.PerCountry = Convert.ToInt32(ddlCountryP.SelectedValue);
            }
            else
            {
                if (MT.PerCountry.ToString() != "")
                {
                    MT.PerCountry = Convert.ToInt32(ddlCountryP.SelectedValue);
                }
                else
                {
                    ddlCountryP.SelectedValue = MT.PerCountry.ToString();
                }
            }
            //fillState2(int.Parse(MT.PerCountry.ToString()));

            if (!string.IsNullOrEmpty(MT.PerState.ToString()))
            {
                MT.PerState = Convert.ToInt32(ddlStateP.SelectedValue);
            }
            else
            {
                //if (MT.PerState.ToString() != "")
                //{
                //    MT.PerState = Convert.ToInt32(ddlStateP.SelectedValue);

                //}
                //else
                //{
                //    ddlStateP.SelectedValue = MT.PerState.ToString();
                //}
            }
            //fillCity2(int.Parse(MT.PerState.ToString()));

            if (MT.PerCity.ToString() == "")
            {
                MT.PerCity = Convert.ToInt32(ddlCityP.SelectedValue);
            }
            else
            {
                if (MT.PerCity.ToString() != "")
                {
                    MT.PerCity = Convert.ToInt32(ddlCityP.SelectedValue);
                }
                else
                {
                    ddlCityP.SelectedValue = MT.PerCity.ToString();
                }
            }

            if (MT.PerRentName == "")
            {
                MT.PerRentName = txtContact1.Text;
            }
            else
            {
                if (MT.PerRentName != "")
                {
                    MT.PerRentName = txtContact1.Text;
                }
                else
                {
                    txtContact1.Text = MT.PerRentName;
                }
            }
            if (MT.PerRentContact == "")
            {
                MT.PerRentContact = txtContactP.Text;
            }
            else
            {
                if (MT.PerRentContact != "")
                {
                    MT.PerRentContact = txtContactP.Text;
                }
                else
                {
                    txtContactP.Text = MT.PerRentContact;
                }
            }
            HR.SubmitChanges();

            if (ViewState["DT"] == null)
            {
                Dt = new DataTable();
                DataColumn EducationId = Dt.Columns.Add("EducationId");
                DataColumn EducationName = Dt.Columns.Add("EducationName");
                DataColumn College = Dt.Columns.Add("College");
                DataColumn YearOfPassing = Dt.Columns.Add("YearOfPassing");
                DataColumn University = Dt.Columns.Add("University");
                DataColumn ObtainPercent = Dt.Columns.Add("ObtainPercent");

            }
            else
            {
                Dt = (DataTable)ViewState["DT"];

                DataTable ds = g.ReturnData("delete from EmpQualificationTB where EmployeeId='" + lblempid.Text + "'");
                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    EmpQualificationTB EmpQ = new EmpQualificationTB();
                    EmpQ.EmployeeId = Convert.ToInt32(lblempid.Text);
                    EmpQ.EducationId = int.Parse(Dt.Rows[i]["EducationId"].ToString());
                    EmpQ.College_School = Dt.Rows[i]["College_School"].ToString();
                    EmpQ.University = Dt.Rows[i]["University"].ToString();
                    EmpQ.YearofPassing = int.Parse(Dt.Rows[i]["YearofPassing"].ToString());
                    EmpQ.Perc = float.Parse(Dt.Rows[i]["Perc"].ToString());

                    HR.EmpQualificationTBs.InsertOnSubmit(EmpQ);
                    HR.SubmitChanges();
                }

            }

            if (ViewState["DtExperience"] == null)
            {
                DtExperience = new DataTable();
                DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                DataColumn Location = DtExperience.Columns.Add("Location");
                DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                DataColumn Department = DtExperience.Columns.Add("Department");
                DataColumn Designation = DtExperience.Columns.Add("Designation");
            }
            else
            {
                DtExperience = (DataTable)ViewState["DtExperience"];
                DataTable ds = g.ReturnData("delete from EmpExprienceTB where EmployeeId='" + lblempid.Text + "'");
                for (int i = 0; i < DtExperience.Rows.Count; i++)
                {
                    EmpExprienceTB exp = new EmpExprienceTB();
                    exp.EmployeeId = Convert.ToInt32(lblempid.Text);

                    exp.CompanyName = DtExperience.Rows[i]["CompanyName"].ToString();
                    exp.CompanyAddress = DtExperience.Rows[i]["Location"].ToString();
                    exp.JoiningDate = DateTime.Parse(DtExperience.Rows[i]["JoiningDate"].ToString());
                    exp.RelvDate = DateTime.Parse(DtExperience.Rows[i]["RelivingDate"].ToString());
                    exp.RefPerson = DtExperience.Rows[i]["RefPerson"].ToString();

                    exp.refContactNo = DtExperience.Rows[i]["ContactNo"].ToString();
                    exp.Department = DtExperience.Rows[i]["Department"].ToString();
                    exp.Designation = DtExperience.Rows[i]["Designation"].ToString();

                    HR.EmpExprienceTBs.InsertOnSubmit(exp);
                    HR.SubmitChanges();
                    Clear1();

                }

            }


            grd.DataSource = Dt;
            grd.DataBind();

            grdExperience.DataSource = DtExperience;
            grdExperience.DataBind();

            ViewState["DT"] = Dt;
            ViewState["DtExperience"] = DtExperience;
            //}


            btnconP1.Text = btnconP2.Text="Update";
            BindAllEmp();
            Clear1();
            MultiView1.ActiveViewIndex = 0;

            g.ShowMessage(this.Page, "Employee Updateded  Successfully.");
            #endregion End Update Profile..
        }
        }

    }

    private void Clear1()
    {
        txtEmployeeCode.Text = "";
        lblAttachPath.Text = "";
        // emp.EmployeeId =int.Parse(txtempid.Text);
        // txtmachinid.Text = null;
        txtfname.Text = null;
        txtmname.Text = null;
        txtlname.Text = null;
        txtCardNo.Text = string.Empty;
        RbGender.SelectedValue = null;
        rbMaritalStatus.SelectedValue = null;
        ddlBloodGroup.SelectedValue = null;
        txtbirtdate.Text = null;
        ddlCompany.SelectedValue = null;
        ddldept.SelectedValue = null;
        dddesg.SelectedValue = null;
        txtcontactno.Text = null;
        txtaltcontact.Text = null;
        txtEmail.Text = null;
        //ddlgreade.SelectedValue = null;

        txtpersonalEmail.Text = null;
        txtpannumber.Text = null;
        txtpassportnumber.Text = null;
        //  txtmachinid.Text = null;
        txtcurrentjoiningdate.Text = null;
        txtprobation.Text = null;
        txtcnfrmdate.Text = null;
        txtsalary.Text = null;
        //account details
        txtpfaccount.Text = null;
        txtesicaccnumber.Text = null;
        txtaccount.Text = null;
        txtbankname.Text = null;
        ddlsalarytype.SelectedValue = null;

        //current Adderess
        ddlCountry.SelectedValue = null;
        ddlState.SelectedValue = null;
        ddlCity.SelectedValue = null;
        txtpin.Text = null;
        txtlandmark.Text = null;
        //renatal
        txtContact0.Text = null;
        txtContact.Text = null;
        //permenent Adderess

        ddlCountryP.SelectedValue = null;
        ddlStateP.SelectedValue = null;
        ddlCityP.SelectedValue = null;
        txtpinP.Text = null;
        txtlandmarkP.Text = null;
        //renatal
        txtContact1.Text = null;
        txtContactP.Text = null;
        BindAllEmp();
        MultiView1.ActiveViewIndex = 0;
        if (Session["EditPage"] == "1")
        {
            Session["EMpID"] = Session["EmploeeID"].ToString();
            Response.Redirect("EmployeeDetails1.aspx");

        }
        if (Session["EditPage"] == "2")
        {
            Response.Redirect("EmployeeDetails.aspx");
        }
        if (Session["EditPage"] == "3")
        {
            Response.Redirect("Employee.aspx");
        }

    }




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
            DataColumn EducationId = Dt.Columns.Add("EducationId");
            DataColumn EducationName = Dt.Columns.Add("EducationName");
            DataColumn College = Dt.Columns.Add("College_School");
            DataColumn YearOfPassing = Dt.Columns.Add("YearOfPassing");
            DataColumn University = Dt.Columns.Add("University");
            DataColumn ObtainPercent = Dt.Columns.Add("Perc");

        }

        DataRow dr = Dt.NewRow();


        dr[0] = ddeducation.SelectedValue;
        dr[1] = ddeducation.SelectedItem.Text;
        dr[2] = txtcollegename.Text;
        dr[3] = txtYearOfPassing.Text;
        dr[4] = txtUniversity.Text;

        dr[5] = txtObtainperc.Text;


        if (Dt.Rows.Count > 0)
        {
            for (int f = 0; f < Dt.Rows.Count; f++)
            {

                string u2 = Dt.Rows[f][0].ToString();
                if (u2 == ddeducation.SelectedValue)
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

                ddeducation.SelectedValue = d["EducationId"].ToString();
                ddeducation.SelectedItem.Text = d["EducationName"].ToString();
                txtcollegename.Text = d["College_School"].ToString();

                txtYearOfPassing.Text = d["YearOfPassing"].ToString();
                txtUniversity.Text = d["University"].ToString();
                txtObtainperc.Text = d["Perc"].ToString();
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
        ddeducation.SelectedIndex = 0;
        txtcollegename.Text = null;
        txtUniversity.Text = null;
        txtYearOfPassing.Text = null;
        txtObtainperc.Text = null;

    }
    protected void btnaddexp_Click(object sender, EventArgs e)
    {
        //if (txtcompanyname.Text != "")
        //{

        int cnt = 0;
        if (ViewState["DtExperience"] != null)
        {
            DtExperience = (DataTable)ViewState["DtExperience"];
        }
        else
        {

            DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
            DataColumn Location = DtExperience.Columns.Add("Location");
            DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
            DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
            DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
            DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
            DataColumn Department = DtExperience.Columns.Add("Department");
            DataColumn Designation = DtExperience.Columns.Add("Designation");
        }
        DataRow dr = DtExperience.NewRow();


        dr[0] = txtcompanyname.Text;
        dr[1] = txtlocation.Text; ;
        dr[2] = txtjoiningdate.Text;
        dr[3] = txtreldate.Text;
        dr[4] = txtref.Text;
        dr[5] = txtrefcontactno.Text;
        dr[6] = txtDept.Text;
        dr[7] = txtDesig.Text;


        if (DtExperience.Rows.Count > 0)
        {
            for (int f = 0; f < DtExperience.Rows.Count; f++)
            {

                string u2 = DtExperience.Rows[f][0].ToString();
                if (u2 == ddeducation.SelectedValue)
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
                DtExperience.Rows.Add(dr);
                clearExpirience();
            }
        }
        else
        {
            DtExperience.Rows.Add(dr);
            clearExpirience();
        }
        ViewState["DtExperience"] = DtExperience;

        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();

        //}
        //else
        //{
        //    g.ShowMessage(this.Page, "Please Enter Company Name");
        //}
    }
    private void clearExpirience()
    {
        txtcompanyname.Text = null;
        txtlocation.Text = null;
        txtjoiningdate.Text = null;
        txtreldate.Text = null;
        txtref.Text = null;
        txtrefcontactno.Text = null;
        txtDept.Text = null;
        txtDesig.Text = null;

    }

    protected void imgeditExpr_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DtExperience = (DataTable)ViewState["DtExperience"];
        foreach (DataRow d in DtExperience.Rows)
        {
            if (d[0].ToString() == imgedit.CommandArgument)
            {
                txtcompanyname.Text = d["CompanyName"].ToString();
                txtlocation.Text = d["Location"].ToString();

                //DateTime date = Convert.ToDateTime(d["JoiningDate"].ToString());
                //txtjoiningdate.Text = date.ToShortDateString();
                //txtjoiningdate.Text = txtjoiningdate.Text.Replace(" 12:00:00 AM", " ");


                //DateTime date1 = Convert.ToDateTime(d["RelivingDate"].ToString());
                //txtreldate.Text = date1.ToShortDateString();
                //txtreldate.Text = txtreldate.Text.Replace(" 12:00:00 AM", " ");

                txtreldate.Text = d["RelivingDate"].ToString();
                txtreldate.Text = txtreldate.Text.Replace(" 12:00:00 AM", " ");

                txtjoiningdate.Text = d["JoiningDate"].ToString();
                txtjoiningdate.Text = txtjoiningdate.Text.Replace(" 12:00:00 AM", " ");

                txtref.Text = d["RefPerson"].ToString();
                txtrefcontactno.Text = d["ContactNo"].ToString();
                txtDept.Text = d["Department"].ToString();
                txtDesig.Text = d["Designation"].ToString();


                d.Delete();
                DtExperience.AcceptChanges();
                break;
            }
        }

        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();
    }
    protected void imgdeleteExpr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        DtExperience = (DataTable)ViewState["DtExperience"];

        foreach (DataRow d in DtExperience.Rows)
        {
            if (d[0].ToString() == imgdelete.CommandArgument)
            {
                d.Delete();
                DtExperience.AcceptChanges();
                break;

            }
        }

        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();
        clearExpirience();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    //private void FillGrade()
    //{
    //    var Datagrade = from gd in HR.GradeMasterTBs
    //                 select new { gd.Grade_Id,gd.GradeName };
    //    if (Datagrade.Count() > 0)
    //    {
    //        ddlgreade.DataSource = Datagrade;
    //        ddlgreade.DataTextField = "GradeName";
    //        ddlgreade.DataValueField = "GradeName";
    //        ddlgreade.DataBind();
    //        ddlgreade.Items.Insert(0,"--Select--");
    //    }
    //    else
    //    {
    //        ddlgreade.DataSource = null;
    //        ddlgreade.DataBind();
    //    }
    //}
    protected void OnClick_Edit(object sender, EventArgs e)
    {
       
        //ViewState["DT"] = Dt;
        //LinkButton Lnk = (LinkButton)sender;
        


    }



    public void editemp(int empid)
    {


        string EmployeeId = Convert.ToString(empid);
        lblempid.Text = EmployeeId;
        EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(EmployeeId)).First();
        if (MT.Solitude == 0)
        {
            ddlsalitude.SelectedIndex = 0;
        }
        if (MT.Solitude == 1)
        {
            ddlsalitude.SelectedIndex = 1;
        }
        if (MT.Solitude == 2)
        {
            ddlsalitude.SelectedIndex = 2;
        }
        if (MT.Solitude == 3)
        {
            ddlsalitude.SelectedIndex = 3;
        }
        //ddlsalitude.SelectedValue =Convert.ToString(MT.Solitude);
        txtfname.Text = MT.FName;
        txtmname.Text = MT.MName;
        txtlname.Text = MT.Lname;
        if (MT.BirthDate != null)
        {
            if (!string.IsNullOrEmpty(MT.BirthDate.Value.ToString()))
            {
                txtbirtdate.Text = MT.BirthDate.Value.ToShortDateString();
            }
        }
        RbGender.Text = MT.Gender;
        rbMaritalStatus.Text = MT.MaritalStaus;
        txtCardNo.Text = MT.EmpCardID;
        if (MT.BloodGroup == null || MT.BloodGroup == "")
        {
        }
        else
        {
            ddlBloodGroup.SelectedValue = MT.BloodGroup;
        }
        lblAttachPath.Text = MT.Photo;


        if (MT.CompanyId != null)
        {
            ddlCompany.SelectedValue = MT.CompanyId.ToString();
            fillDept(int.Parse(MT.CompanyId.ToString()));
            ddldept.SelectedValue = MT.DeptId.ToString();
            fillDesignation(int.Parse(MT.DeptId.ToString()));
        }


        fillReportingHead();

        //fillReportingHead();
        if (MT.ReportingStatus == null || MT.ReportingStatus.ToString() == "")
        {

        }
        else
        {
           
            ddlReportingHead.SelectedValue = Convert.ToString(MT.ReportingStatus);
        }

        image1.ImageUrl = "~/Attachments/" + lblAttachPath.Text;
        image1.Visible = true;
        txtpannumber.Text = MT.PanNo;
        txtpassportnumber.Text = MT.PassportNo;
        txtEmail.Text = MT.Email;
        txtprobation.Text = MT.ProbationPeriod;


        txtVisaNo.Text = MT.VisaNo;
        if (MT.VisaExpiryDate!=null)
        {
            txtExpiryDate.Text = MT.VisaExpiryDate.Value.ToShortDateString();
        }
        if (MT.PassportExpiryDate!=null)
        {
            txtExpiryDate.Text = MT.PassportExpiryDate.Value.ToShortDateString();
        }

        if(MT.VisaExpiryDate!=null)
        {
            txtVisaExpiryDate.Text = MT.VisaExpiryDate.Value.ToShortDateString();
        }



        //RegistrationTB RG = HR.RegistrationTBs.Where(d => d.EmployeeId == int.Parse(EmployeeId)).First();

        txtpersonalEmail.Text = MT.EmailId;
        txtpersonalEmail.Attributes.Add("readonly", "readonly");

        // txtpersonalEmail.Text = MT.personalEmail;
        txtBioId.Text = MT.MachineID;
        if (!string.IsNullOrEmpty(MT.MachineID))
            txtBioId.Attributes.Add("readonly", "readonly");
        txtempid.Text = MT.EmployeeId.ToString();
        txtempid.Attributes.Add("readonly", "readonly");
        txtcurrentjoiningdate.Attributes.Add("readonly", "readonly");


        //ddlgreade.SelectedValue = MT.Grade.ToString();
        txtprobation.Text = MT.ProbationPeriod;
        txtsalary.Text = MT.NetSalary;
        dddesg.SelectedValue = MT.DesgId.ToString();
        txtcontactno.Text = MT.ContactNo;
        txtaltcontact.Text = MT.AltContactNo;
        if (MT.DOJ != null)
        {

            txtcurrentjoiningdate.Text = MT.DOJ.Value.ToShortDateString();
        }
        if (MT.ConfirmDate != null)
        {
            txtcnfrmdate.Text = MT.ConfirmDate.Value.ToShortDateString();
        }


        //account details
        txtpfaccount.Text = MT.PFAccountNo;
        txtesicaccnumber.Text = MT.ESICAccountNo;
        txtaccount.Text = MT.SalaryAccountNo;
        txtbankname.Text = MT.BankName;
        if ((MT.Salarytype == "") || (MT.Salarytype == "--Select--") || (MT.Salarytype == "--"))
        {
            MT.Salarytype = "--";
        }
        else
        {
            ddlsalarytype.Text = MT.Salarytype;
        }
        //current Adderess
        if (MT.CurCountry != null)
        {
            ddlCountry.SelectedValue = MT.CurCountry.ToString();
          //  fillState(int.Parse(MT.CurCountry.ToString()));
          //  ddlState.SelectedValue = MT.CurState.ToString();
            fillCity(int.Parse(MT.CurCountry.ToString()));
            ddlCity.SelectedValue = MT.CurCity.ToString();
            txtpin.Text = MT.CurPin;
            txtlandmark.Text = MT.CurLandmark;
            txtContact0.Text = MT.CurRentName;
            txtContact.Text = MT.CurRentContact;
        }


        //permenant Address
        if (MT.PerCountry != null)
        {
            ddlCountryP.SelectedValue = MT.PerCountry.ToString();
         //   fillState2(int.Parse(MT.PerCountry.ToString()));
           // ddlStateP.SelectedValue = MT.PerCountry.ToString();
            fillCity2(int.Parse(MT.PerCountry.ToString()));
            ddlCityP.SelectedValue = MT.PerCity.ToString();
            txtpinP.Text = MT.PerPin;
            txtlandmarkP.Text = MT.PerLandmark;
            txtContact1.Text = MT.PerRentName;
            txtContactP.Text = MT.PerRentContact;
        }


        var EDU = (from m in HR.EmpQualificationTBs
                   join n in HR.MasterEducationTBs on m.EducationId equals n.EducationId
                   where m.EmployeeId == int.Parse(EmployeeId)
                   select new
                   {
                       n.EducationId,
                       n.EducationName,
                       College_School = m.College_School,
                       m.University,
                       m.YearofPassing,
                       Perc = m.Perc
                   }).ToList();
        if (EDU.Count() > 0)
        {

            grd.DataSource = EDU;
            grd.DataBind();


            foreach (var item in EDU)
            {

                //Dt = (DataTable)ViewState["DT"];
                if (ViewState["DT"] == null)
                {
                    Dt = new DataTable();
                    DataColumn EducationId = Dt.Columns.Add("EducationId");
                    DataColumn EducationName = Dt.Columns.Add("EducationName");
                    DataColumn College = Dt.Columns.Add("College");
                    DataColumn University = Dt.Columns.Add("University");
                    DataColumn YearOfPassing = Dt.Columns.Add("YearOfPassing");
                    DataColumn ObtainPercent = Dt.Columns.Add("ObtainPercent");

                }
                else
                {
                    Dt = (DataTable)ViewState["DT"];
                }
                DataTable ds = g.ReturnData("SELECT  MasterEducationTB.EducationId, MasterEducationTB.EducationName, EmpQualificationTB.College_School, EmpQualificationTB.YearofPassing,EmpQualificationTB.University,EmpQualificationTB.Perc FROM         MasterEducationTB INNER JOIN                      EmpQualificationTB ON MasterEducationTB.EducationId = EmpQualificationTB.EducationId RIGHT OUTER JOIN                     EmployeeTB ON EmpQualificationTB.EmployeeId = EmployeeTB.EmployeeId where EmployeeTB.EmployeeId = '" + EmployeeId + "'");
                DataRow dr = ds.NewRow();
                dr[0] = ds.Rows[0]["EducationId"].ToString();
                dr[1] = ds.Rows[0]["EducationName"].ToString();
                dr[2] = ds.Rows[0]["College_School"].ToString();
                dr[3] = ds.Rows[0]["YearofPassing"].ToString();
                dr[4] = ds.Rows[0]["University"].ToString();

                dr[5] = ds.Rows[0]["Perc"].ToString();
                //ds.Rows.Add(dr);
                ViewState["DT"] = ds;
            }
        }
        var Exp = (from m in HR.EmpExprienceTBs
                   where m.EmployeeId == int.Parse(EmployeeId)
                   select new
                   {
                       m.CompanyName,
                       Location = m.CompanyAddress,
                       JoiningDate = m.JoiningDate.Value,
                       RelivingDate = m.RelvDate,
                       RefPerson = m.RefPerson,
                       ContactNo = m.refContactNo,
                       Department = m.Department,
                       Designation = m.Designation

                   }).ToList();
        if (Exp.Count() > 0)
        {

            grdExperience.DataSource = Exp;
            grdExperience.DataBind();
            // ViewState["DtExperience"] = Exp;
            foreach (var item in Exp)
            {

                //Dt = (DataTable)ViewState["DT"];
                if (ViewState["DtExperience"] == null)
                {
                    Dt = new DataTable();
                    DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                    DataColumn Location = DtExperience.Columns.Add("Location");
                    DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                    DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                    DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                    DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                    DataColumn Department = DtExperience.Columns.Add("Department");
                    DataColumn Designation = DtExperience.Columns.Add("Designation");

                }
                else
                {
                    Dt = (DataTable)ViewState["DtExperience"];
                }
                DataTable ds1 = g.ReturnData("select CompanyName,CompanyAddress as Location, JoiningDate,RelvDate as RelivingDate, RefPerson,refContactNo as ContactNo, Department,Designation from  EmpExprienceTB where EmpExprienceTB.EmployeeId = '" + EmployeeId + "'");
                DataRow dr = ds1.NewRow();

                dr[0] = ds1.Rows[0]["CompanyName"].ToString();
                dr[1] = ds1.Rows[0]["Location"].ToString();
                dr[2] = ds1.Rows[0]["JoiningDate"].ToString();
                dr[3] = ds1.Rows[0]["RelivingDate"].ToString();
                dr[4] = ds1.Rows[0]["RefPerson"].ToString();
                dr[5] = ds1.Rows[0]["ContactNo"].ToString();
                dr[6] = ds1.Rows[0]["Department"].ToString();
                dr[7] = ds1.Rows[0]["Designation"].ToString();

                //ds.Rows.Add(dr);
                ViewState["DtExperience"] = ds1;
            }
        }

        btnconP1.Text = btnconP2.Text = "Update";
        //BindAllEmp();
        MultiView1.ActiveViewIndex = 1;


    }




    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Emp.PageIndex = e.NewPageIndex;
        fillCountry();
        BindAllEmp();
        fillcompany();
        fillEducation();
        grd_Emp.DataBind();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindAllEmp();
        //txtFirstNameSearch.Text = null;

        // txtDOJsearch.Text = null;
        //txtpansearch.Text = null;
        //txtdeptsearch.Text = null;
        //  txtempidsearch.Text = null;

    }

    protected void grd_Emp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

        Clear1();
    }
    protected void txtprobation_TextChanged(object sender, EventArgs e)
    {
        string a = txtprobation.Text;

        DateTime JoiningDate = DateTime.Parse(txtcurrentjoiningdate.Text);
        DateTime confirmDate = JoiningDate.AddMonths(int.Parse(a));
        txtcnfrmdate.Text = confirmDate.ToString("MM/dd/yyyy");
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCountries(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        List<string> Name = (from d in HR.EmployeeTBs
                             join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID
                             where
                                 (d.FName + ' ' + d.Lname).StartsWith(prefixText)
                             select d.FName + ' ' + d.Lname).Distinct().ToList();
        return Name;

    }

    protected void txtcnfrmdate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnsavecity_Click(object sender, EventArgs e)
    {
        // Add Grade. Abdul Rahman:
        var ExistData = from d in HR.GradeMasterTBs where d.GradeName == txtaddgrade.Text select d;
        if (ExistData.Count() == 0)
        {
            GradeMasterTB MTB = new GradeMasterTB();


            MTB.GradeName = txtaddgrade.Text;
            MTB.Status = 0;
            HR.GradeMasterTBs.InsertOnSubmit(MTB);
            HR.SubmitChanges();
            //modpop.Message = "Submitted Successfully";
            //modpop.ShowPopUp();
            g.ShowMessage(this.Page, "Submitted Successfully");
            txtaddgrade.Text = "";
        }
        else
        {
            g.ShowMessage(this.Page, "This Grade Already Exist");
            modNewTax.Show();
        }
        //  FillGrade();
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        modNewTax.Hide();
    }
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        modNewTax.Show();
    }
    protected void ImgCitAdd_Click(object sender, ImageClickEventArgs e)
    {
        HiddenField1.Value = "1";
        ModelPopUpCity.Show();
    }
    protected void btnCityAdd_Click(object sender, EventArgs e)
    {
        //var ExistData = from d in HR.CityTBs where d.CityName == txtCityName.Text select d;
        //if (ExistData.Count() == 0)
        //{
        //   CityTB ct = new CityTB();
        //   ct.CityName = txtCityName.Text;
        //   ct.Status = 0;

        //    HR.CityTBs.InsertOnSubmit(ct);
        //    HR.SubmitChanges();
        //    //modpop.Message = "Submitted Successfully";
        //    //modpop.ShowPopUp();
        //    g.ShowMessage(this.Page, "Submitted Successfully");
        //    txtaddgrade.Text = "";
        //}
        //else
        //{
        //    g.ShowMessage(this.Page, "This City Already Exist");
        //    ModelPopUpCity.Show();
        //}
        // FillGrade();
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {

    }

    public void Clear()
    {
        txtcityname.Text = null;
        lblcityid.Text = "";
        //DropDownList1.SelectedIndex = 0;
        //DropDownList1.Items.Clear();
        //DropDownList2.SelectedIndex = 0;
        //DropDownList2.Items.Clear();


        rd_status.SelectedIndex = 0;
        //  BindAllSource();
        //MultiView1.ActiveViewIndex = 0;
        //btnsubmit.Text = "Save";

    }



    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (HiddenField1.Value == "1")
        {
            if (btnsubmit.Text == "Save")
            {
                if (Convert.ToInt32(ddlCountry.SelectedIndex) != 0)
                {
                    if (Convert.ToInt32(ddlState.SelectedIndex) != 0)
                    {

                        var dte = from p in HR.CityTBs.Where(d => d.CityName == txtcityname.Text && d.CountryID == Convert.ToInt32(ddlCountry.SelectedValue) && d.StateId == Convert.ToInt32(ddlState.SelectedValue)) select p;
                        if (dte.Count() > 0)
                        {
                            g.ShowMessage(this.Page, "This City Already Exist");


                        }
                        else
                        {
                            CityTB mtb = new CityTB();
                            mtb.CityName = txtcityname.Text;
                            mtb.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                          //  mtb.StateId = Convert.ToInt32(ddlState.SelectedValue);
                            mtb.Status = rd_status.SelectedIndex;
                            HR.CityTBs.InsertOnSubmit(mtb);
                            HR.SubmitChanges();
                            g.ShowMessage(this.Page, "Data Added Successfully");
                            //fillCity2(Convert.ToInt32(ddlStateP.SelectedValue));
                            fillCity(Convert.ToInt32(ddlState.SelectedValue));
                            Clear();
                        }
                    }
                    else
                    {
                        g.ShowMessage(this.Page, "Please Select State First");
                        txtcityname.Text = "";
                    }
                }
                else
                {
                    g.ShowMessage(this.Page, "Please Select Country First");
                    txtcityname.Text = "";
                }
            }
        }
        if (HiddenField1.Value == "2")
        {

            if (btnsubmit.Text == "Save")
            {
                if (Convert.ToInt32(ddlCountryP.SelectedIndex) != 0)
                {
                    if (Convert.ToInt32(ddlStateP.SelectedIndex) != 0)
                    {


                        var dte = from p in HR.CityTBs.Where(d => d.CityName == txtcityname.Text && d.CountryID == Convert.ToInt32(ddlCountryP.SelectedValue) && d.StateId == Convert.ToInt32(ddlStateP.SelectedValue)) select p;
                        if (dte.Count() > 0)
                        {
                            g.ShowMessage(this.Page, "This City Already Exist");


                        }
                        else
                        {
                            CityTB mtb = new CityTB();
                            mtb.CityName = txtcityname.Text;
                            mtb.CountryID = Convert.ToInt32(ddlCountryP.SelectedValue);
                          //  mtb.StateId = Convert.ToInt32(ddlStateP.SelectedValue);
                            mtb.Status = rd_status.SelectedIndex;
                            HR.CityTBs.InsertOnSubmit(mtb);
                            HR.SubmitChanges();
                            g.ShowMessage(this.Page, "Data Added Successfully");
                            //fillCity(Convert.ToInt32(ddlState.SelectedValue));
                            fillCity2(Convert.ToInt32(ddlStateP.SelectedValue));
                            Clear();
                        }
                    }
                    else
                    {
                        g.ShowMessage(this.Page, "Please Select State First");
                        txtcityname.Text = "";

                    }
                }
                else
                {
                    g.ShowMessage(this.Page, "Please Select Country First");
                    txtcityname.Text = "";
                }


            }
        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState1(Convert.ToInt32(ddlCountry.SelectedValue));
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        txtcityname.Text = "";
        MultiView1.ActiveViewIndex = 1;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        HiddenField1.Value = "2";
        ModelPopUpCity.Show();
    }
    //protected void BtnAADDCity_Click(object sender, EventArgs e)
    //{
    //    if (BtnAADDCity.Text == "Save")
    //    {
    //        if (Convert.ToInt32(ddlCountryP.SelectedValue) != 0)
    //        {
    //            if (Convert.ToInt32(ddlStateP.SelectedValue) != 0)
    //            {

    //                var dte = from p in HR.CityTBs.Where(d => d.CityName == txtCityName11.Text && d.CountryId == Convert.ToInt32(ddlCountryP.SelectedValue) && d.StateId == Convert.ToInt32(ddlStateP.SelectedValue)) select p;
    //                if (dte.Count() > 0)
    //                {
    //                    g.ShowMessage(this.Page, "This City Already Exist");


    //                }
    //                else
    //                {
    //                    CityTB mtb = new CityTB();
    //                    mtb.CityName = txtCityName11.Text;
    //                    mtb.CountryId = Convert.ToInt32(ddlCountryP.SelectedValue);
    //                    mtb.StateId = Convert.ToInt32(ddlStateP.SelectedValue);
    //                    mtb.Status = rd_status.SelectedIndex;
    //                    HR.CityTBs.InsertOnSubmit(mtb);
    //                    HR.SubmitChanges();
    //                    g.ShowMessage(this.Page, "Data Added Successfully");
    //                    fillCity2(Convert.ToInt32(ddlStateP.SelectedValue));

    //                    Clear();
    //                }
    //            }
    //            else
    //            {
    //                g.ShowMessage(this.Page, "Please Select State First");
    //            }
    //        }
    //        else
    //        {
    //            g.ShowMessage(this.Page, "Please Select Country First");
    //        }
    //    }
    //}
    protected void ddlCityP_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnaddexp_Click1(object sender, EventArgs e)
    {

        int cnt = 0;
        if (ViewState["DtExperience"] != null)
        {
            DtExperience = (DataTable)ViewState["DtExperience"];
        }
        else
        {

            DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
            DataColumn Location = DtExperience.Columns.Add("Location");
            //DateTime date = Convert.ToDateTime(d["JoiningDate"].ToString());
            //txtjoiningdate.Text = date.ToShortDateString();
            //txtjoiningdate.Text = txtbirtdate.Text.Replace(" 12:00:00 AM", " ")


            DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
            DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
            DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
            DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
            DataColumn Department = DtExperience.Columns.Add("Department");
            DataColumn Designation = DtExperience.Columns.Add("Designation");
        }
        DataRow dr = DtExperience.NewRow();


        dr[0] = txtcompanyname.Text;
        dr[1] = txtlocation.Text; ;
        dr[2] = txtjoiningdate.Text;
        dr[3] = txtreldate.Text;
        dr[4] = txtref.Text;
        dr[5] = txtrefcontactno.Text;
        dr[6] = txtDept.Text;
        dr[7] = txtDesig.Text;


        if (DtExperience.Rows.Count > 0)
        {
            for (int f = 0; f < DtExperience.Rows.Count; f++)
            {

                string u2 = DtExperience.Rows[f][0].ToString();
                if (u2 == ddeducation.SelectedValue)
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
                DtExperience.Rows.Add(dr);
                clearExpirience();
            }
        }
        else
        {
            DtExperience.Rows.Add(dr);
            clearExpirience();
        }
        ViewState["DtExperience"] = DtExperience;

        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();

        //}
        //else
        //{
        //    g.ShowMessage(this.Page, "Please Enter Company Name");
        //}
    }
    protected void LnkReliveDate_Click(object sender, EventArgs e)
    {
        LinkButton lnkReliveDate = (LinkButton)sender;
        int EmployeeId = Convert.ToInt32(lnkReliveDate.CommandArgument);
        Session["EmployeeId3"] = EmployeeId;

        tblReliveingEmployee.Visible = true;
        modRelive.Show();


    }

    protected void btnpopsave_Click1(object sender, EventArgs e)
    {

        EmployeeTB emp = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmployeeId3"])).First();
        emp.RelivingDate = g.GetCurrentDateTime();
        emp.RelivingStatus = 0;
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "Employee relieved Details Save Sucessfully");
        Response.Redirect("Employee.aspx");

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grd_Emp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Emp.Rows.Count > 0)
        {
            grd_Emp.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Emp.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }
    protected void grd_Emp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Emp.PageIndex = e.NewPageIndex;
        fillCountry();
        BindAllEmp();
        fillcompany();
        fillEducation();
        grd_Emp.DataBind();
    }

    protected void imgbtnViewProfile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgemp = (ImageButton)sender;
            int EmployeeID = Convert.ToInt32(imgemp.CommandArgument);
            Session["EmpBasicId"] = EmployeeID;
            Response.Redirect("EmployeeDetails.aspx?id=" + EmployeeID);
            //ModelPoUpEmployeeView.Show();
        }
        catch(Exception exc)
        {
            throw;
        }

    }
    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompanyList.SelectedIndex > 0)
        {
            BindDepartment(ddlCompanyList.SelectedItem.Text);
            FillEmployeeList();
        }
        else
        {
            ddlDepartment.Items.Clear();
            ddlEmployeeList.Items.Clear();
            FillEmployeeList();
            BindAllEmp();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlCompanyList.SelectedIndex = 0;
        ddlDepartment.Items.Clear();
        ddlEmployeeList.Items.Clear();
        BindAllEmp();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployeeList();
        FillEmployeeList();
    }





    #region Document display & download
    private void bindEmpDoc()
    {
        var fetchempDoc = from d in HR.EmployeeDocumentMasterTBs
                          where d.EmployeeId == Convert.ToInt32(lblempid.Text)
                          select new { d.DocumentName, d.DocumentPath, d.DocumentId, d.Document_ID, d.EmployeeId, d.Description };
        if (fetchempDoc.Count() > 0)
        {
            grddocument.DataSource = fetchempDoc;
            grddocument.DataBind();
        }
        else
        {
            grddocument.DataSource = null;
            grddocument.DataBind();
        }

        for (int i = 0; i < grddocument.Rows.Count; i++)
        {
            Image img = (Image)grddocument.Rows[i].FindControl("ImageDoc");
            string DocumentName = grddocument.Rows[i].Cells[3].Text;


            if (DocumentName.EndsWith(".docx") || DocumentName == "&nbsp;" || DocumentName.EndsWith(".xlsx"))
            {
                img.ImageUrl = "~\\Images\\th.jpg";
                img.Height = Unit.Pixel(50);
                img.Width = Unit.Pixel(70);
            }
            else if (DocumentName.EndsWith(".png") || DocumentName.EndsWith(".jpg") || DocumentName.EndsWith(".jpeg"))
            {
                img.ImageUrl = "~\\EmployeeDocument\\" + grddocument.Rows[i].Cells[3].Text;
            }

        }

    }
    protected void ImageDoc_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void lnkDownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkDoc = (ImageButton)sender;
        string Docname = lnkDoc.CommandArgument;
        string filePath;
        var data = from doc in HR.EmployeeDocumentMasterTBs
                   where doc.DocumentPath == Docname
                   select new
                   {
                       DocumentName = doc.DocumentPath
                   };
        if (data.Count() > 0)
        {
            string DocumentName = data.First().DocumentName;
            filePath = "EmployeeDocument\\" + DocumentName;

            if (DocumentName.EndsWith(".docx"))
            {
                Response.ContentType = "application/docx";
            }
            else if (DocumentName.EndsWith(".xlsx"))
            {
                Response.ContentType = "application/vnd.ms-excel";
            }
            else if (DocumentName.EndsWith(".png"))
            {
                Response.ContentType = "image/png";
            }
            else if (DocumentName.EndsWith(".jpg"))
            {
                Response.ContentType = "image/jpg";
            }
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
        }
    }

    #endregion

    //protected void imgbtnViewProfile_Click1(object sender, EventArgs e)
    //{
    //   // ModalPopupExtender1.Show();
    //}
    protected void LnkViewProfile_Click(object sender, EventArgs e)
    {
        //  ModalPopupExtender1.Show();
    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgemp = (ImageButton)sender;
            int EmployeeID = Convert.ToInt32(imgemp.CommandArgument);
            Session["EmpBasicId"] = EmployeeID;
            Response.Redirect("EmployeeDetails.aspx?id=" + EmployeeID);
            //ModelPoUpEmployeeView.Show();
        }
        catch (Exception exc)
        {
            throw;
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        Session["EditPage"] = "3";
        ImageButton Lnk = (ImageButton)sender;
        string EmployeeId = Lnk.CommandArgument;
        lblempid.Text = EmployeeId;
        EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(EmployeeId)).First();
        txtEmpCode.Text = MT.EmployeeNo;
        if (MT.Solitude == 0)
        {
            ddlsalitude.SelectedIndex = 0;
        }
        if (MT.Solitude == 1)
        {
            ddlsalitude.SelectedIndex = 1;
        }
        if (MT.Solitude == 2)
        {
            ddlsalitude.SelectedIndex = 2;
        }
        if (MT.Solitude == 3)
        {
            ddlsalitude.SelectedIndex = 3;
        }
        //ddlsalitude.SelectedValue =Convert.ToString(MT.Solitude);
        txtfname.Text = MT.FName;
        txtmname.Text = MT.MName;
        txtlname.Text = MT.Lname;
        //txtbirtdate.Text = Convert.ToString(MT.BirthDate);
        txtbirtdate.Text = Convert.ToDateTime(MT.BirthDate).ToShortDateString();
        RbGender.SelectedValue = MT.Gender;
        rbMaritalStatus.Text = MT.MaritalStaus;

        if (MT.BloodGroup == null || MT.BloodGroup == "")
        {
        }
        else
        {
            ddlBloodGroup.SelectedValue = MT.BloodGroup;
        }


        lblAttachPath.Text = MT.Photo;

        image1.ImageUrl = "~/Attachments/" + lblAttachPath.Text;
        image1.Visible = true;
        txtpannumber.Text = MT.PanNo;
        txtpassportnumber.Text = MT.PassportNo;
        txtEmail.Text = MT.Email;
        txtCardNo.Text = MT.EmpCardID;
        txtprobation.Text = MT.ProbationPeriod;

        txtVisaNo.Text = MT.VisaNo;
        if (MT.VisaExpiryDate != null)
        {
            txtVisaExpiryDate.Text = MT.VisaExpiryDate.Value.ToShortDateString();
        }
        if (MT.PassportExpiryDate != null)
        {
            txtExpiryDate.Text = MT.PassportExpiryDate.Value.ToShortDateString();
        }

        if (MT.VisaExpiryDate != null)
        {
            txtVisaExpiryDate.Text = MT.VisaExpiryDate.Value.ToShortDateString();
        }


        //var ExistDataMatch = (from d in HR.EmployeeTBs
        //                      join dt in HR.RegistrationTBs on d.EmployeeId equals dt.EmployeeId
        //                      where dt.EmployeeId == Convert.ToInt32(lblempid.Text)
        //                      select new { dt.EmailID }).First();


        // RegistrationTB RG = HR.RegistrationTBs.Where(d => d.EmployeeId == int.Parse(EmployeeId)).First();

        txtpersonalEmail.Text = MT.EmailId;
        txtpersonalEmail.Attributes.Add("readonly", "readonly");
        // txtpersonalEmail.Text = MT.personalEmail;
        txtBioId.Text = MT.MachineID;

        txtBioId.Attributes.Add("readonly", "readonly");
        txtempid.Text = MT.EmployeeId.ToString();
        txtempid.Attributes.Add("readonly", "readonly");
        try
        {
            ddlCompany.SelectedValue = MT.CompanyId.ToString();
            fillDept(int.Parse(MT.CompanyId.ToString()));
            ddldept.SelectedValue = MT.DeptId.ToString();

            fillDesignation(int.Parse(MT.DeptId.ToString()));
        }
        catch (Exception ex) { }
        if (MT.ReportingStatus == null || MT.ReportingStatus.ToString() == "")
        {
            fillReportingHead();
        }
        else
        {
            try
            {
                fillReportingHead();
                ddlReportingHead.SelectedValue = Convert.ToString(MT.ReportingStatus);
            }
            catch (Exception)
            {

                fillReportingHead();
            }



        }
        if (!string.IsNullOrEmpty(MT.Grade))
            ddlGrade.SelectedValue = string.IsNullOrEmpty(MT.Grade) ? "0" : Convert.ToString(MT.Grade);


        txtprobation.Text = MT.ProbationPeriod;
        txtsalary.Text = MT.NetSalary;
        if (MT.DesgId == null)
        {

        }
        else
        {
            dddesg.SelectedValue = Convert.ToString(MT.DesgId);
        }
        txtcontactno.Text = MT.ContactNo;
        txtaltcontact.Text = MT.AltContactNo;
        //txtcurrentjoiningdate.Text = Convert.ToString(MT.DOJ);
        txtcurrentjoiningdate.Text = Convert.ToDateTime(MT.DOJ).ToShortDateString();
        //txtcnfrmdate.Text = Convert.ToString(MT.ConfirmDate);
        txtcnfrmdate.Text = Convert.ToDateTime(MT.ConfirmDate).ToShortDateString();



        //account details
        txtpfaccount.Text = MT.PFAccountNo;
        txtesicaccnumber.Text = MT.ESICAccountNo;
        txtaccount.Text = MT.SalaryAccountNo;
        txtbankname.Text = MT.BankName;
        if ((MT.Salarytype == "") || (MT.Salarytype == "--Select--") || (MT.Salarytype == "--"))
        {
            MT.Salarytype = "--";
        }
        else
        {
            ddlsalarytype.Text = MT.Salarytype;
        }
        //current Adderess
        if (Convert.ToString(MT.CurCountry) == "")
        {
        }
        else
        {
            ddlCountry.SelectedValue = MT.CurCountry.ToString();
            fillCity(int.Parse(MT.CurCountry.ToString()));
        }
        //if (Convert.ToString(MT.CurState) == "")
        //{
        //    fillCity(0);
        //}
        //else
        //{
        //    ddlState.SelectedValue = MT.CurState.ToString();
        //    fillCity(int.Parse(MT.CurState.ToString()));
        //}
        if (Convert.ToString(MT.CurCity) == "")
        {
        }
        else
        {
            try
            {
                ddlCity.SelectedValue = MT.CurCity.ToString();
            }
            catch (Exception ex)
            { }
        }
        txtpin.Text = MT.CurPin;
        txtlandmark.Text = MT.CurLandmark;
        txtContact0.Text = MT.CurRentName;
        txtContact.Text = MT.CurRentContact;
        //permenant Address
        if (Convert.ToString(MT.PerCountry) == "")
        {
        }
        else
        {
            ddlCountryP.SelectedValue = MT.PerCountry.ToString();
            fillCity2(int.Parse(MT.PerCountry.ToString()));
        }
        //if (Convert.ToString(MT.PerState) == "")
        //{
        //    fillCity2(0);
        //}
        //else
        //{
        //    ddlStateP.SelectedValue = MT.PerState.ToString();
        //    fillCity2(Convert.ToInt32(MT.PerState.ToString()));
        //}


        if (Convert.ToString(MT.PerCity) == "")
        {
        }
        else
        {
            try
            {
                ddlCityP.SelectedValue = Convert.ToString(MT.PerCity);
            }
            catch (Exception ex)
            { }
        }
        txtpinP.Text = Convert.ToString(MT.PerPin);
        txtlandmarkP.Text = Convert.ToString(MT.PerLandmark);
        txtContact1.Text = MT.PerRentName;
        txtContactP.Text = MT.PerRentContact;



        var EDU = (from m in HR.EmpQualificationTBs
                   join n in HR.MasterEducationTBs on m.EducationId equals n.EducationId
                   where m.EmployeeId == int.Parse(EmployeeId)
                   select new
                   {
                       n.EducationId,
                       n.EducationName,
                       College_School = m.College_School,
                       m.University,
                       m.YearofPassing,
                       Perc = m.Perc
                   }).ToList();
        grd.DataSource = EDU;
        grd.DataBind();


        foreach (var item in EDU)
        {

            //Dt = (DataTable)ViewState["DT"];
            if (ViewState["DT"] == null)
            {
                Dt = new DataTable();
                DataColumn EducationId = Dt.Columns.Add("EducationId");
                DataColumn EducationName = Dt.Columns.Add("EducationName");
                DataColumn College = Dt.Columns.Add("College");
                DataColumn University = Dt.Columns.Add("University");
                DataColumn YearOfPassing = Dt.Columns.Add("YearOfPassing");
                DataColumn ObtainPercent = Dt.Columns.Add("ObtainPercent");

            }
            else
            {
                Dt = (DataTable)ViewState["DT"];
            }
            DataTable ds = g.ReturnData("SELECT  MasterEducationTB.EducationId, MasterEducationTB.EducationName, EmpQualificationTB.College_School, EmpQualificationTB.YearofPassing,EmpQualificationTB.University,EmpQualificationTB.Perc FROM         MasterEducationTB INNER JOIN                      EmpQualificationTB ON MasterEducationTB.EducationId = EmpQualificationTB.EducationId RIGHT OUTER JOIN                     EmployeeTB ON EmpQualificationTB.EmployeeId = EmployeeTB.EmployeeId where EmployeeTB.EmployeeId = '" + EmployeeId + "'");
            DataRow dr = ds.NewRow();
            dr[0] = ds.Rows[0]["EducationId"].ToString();
            dr[1] = ds.Rows[0]["EducationName"].ToString();
            dr[2] = ds.Rows[0]["College_School"].ToString();
            dr[3] = ds.Rows[0]["YearofPassing"].ToString();
            dr[4] = ds.Rows[0]["University"].ToString();

            dr[5] = ds.Rows[0]["Perc"].ToString();
            //ds.Rows.Add(dr);
            ViewState["DT"] = ds;
        }
        var Exp = (from m in HR.EmpExprienceTBs
                   where m.EmployeeId == int.Parse(EmployeeId)
                   select new
                   {
                       m.CompanyName,
                       Location = m.CompanyAddress,
                       JoiningDate = m.JoiningDate.Value,
                       RelivingDate = m.RelvDate,
                       RefPerson = m.RefPerson,
                       ContactNo = m.refContactNo,
                       Department = m.Department,
                       Designation = m.Designation

                   }).ToList();
        grdExperience.DataSource = Exp;
        grdExperience.DataBind();

        foreach (var item in Exp)
        {

            //Dt = (DataTable)ViewState["DT"];
            if (ViewState["DtExperience"] == null)
            {
                Dt = new DataTable();
                DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                DataColumn Location = DtExperience.Columns.Add("Location");
                DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                DataColumn Department = DtExperience.Columns.Add("Department");
                DataColumn Designation = DtExperience.Columns.Add("Designation");

            }
            else
            {
                Dt = (DataTable)ViewState["DtExperience"];
            }
            DataTable ds1 = g.ReturnData("select CompanyName,CompanyAddress as Location, JoiningDate,RelvDate as RelivingDate, RefPerson,refContactNo as ContactNo, Department,Designation from  EmpExprienceTB where EmpExprienceTB.EmployeeId = '" + EmployeeId + "'");
            DataRow dr = ds1.NewRow();

            dr[0] = ds1.Rows[0]["CompanyName"].ToString();
            dr[1] = ds1.Rows[0]["Location"].ToString();
            dr[2] = ds1.Rows[0]["JoiningDate"].ToString();
            dr[3] = ds1.Rows[0]["RelivingDate"].ToString();
            dr[4] = ds1.Rows[0]["RefPerson"].ToString();
            dr[5] = ds1.Rows[0]["ContactNo"].ToString();
            dr[6] = ds1.Rows[0]["Department"].ToString();
            dr[7] = ds1.Rows[0]["Designation"].ToString();

            //ds.Rows.Add(dr);
            ViewState["DtExperience"] = ds1;
        }
        bindEmpDoc();

        btnconP1.Text = btnconP2.Text = "Update";
        //BindAllEmp();
        MultiView1.ActiveViewIndex = 1;
    }

    protected void lbtnRelease_Click(object sender, EventArgs e)
    {
        LinkButton lnkReliveDate = (LinkButton)sender;
        int EmployeeId = Convert.ToInt32(lnkReliveDate.CommandArgument);
        Session["EmployeeId3"] = EmployeeId;

        tblReliveingEmployee.Visible = true;
        modRelive.Show();
    }
}
