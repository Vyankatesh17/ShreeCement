using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterCompany : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string path = "";
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                
                MultiView1.ActiveViewIndex = 0;
                BindAllCompany();
                fillCountry(); 
            }
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string FolderPath = Server.MapPath("Cmplogo");
                MakeDirectoryIfExist(FolderPath);
               
                if (FileUpload1.HasFile)
                {
                    int cnt = 0;
                    string dir = Server.MapPath("Cmplogo/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg" };
                    string filenamee = Path.GetFileName(FileUpload1.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("Cmplogo/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUpload1.SaveAs(Request.PhysicalApplicationPath + "Cmplogo/" + filename1[0] + cnt + "." + filename1[1]);
                    path = filename1[0] + cnt + "." + filename1[1];
                    // lblpath.Text = path;
                }
                else
                {
                    if (lbllogo.Text != "")
                        path = lbllogo.Text;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    private void fillCountry()
    {
        var data = from dt in HR.CountryTBs
                   where  dt.Status==0
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlCountry.DataSource = data;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "--Select--");
        }
    }
    public void BindAllCompany()
    {
        var CompanyData = (from d in HR.CompanyInfoTBs where d.Status == 1 select new {d.TenantId,d.ShortName, d.CompanyId, d.Address, d.CompanyName, d.ContactNo, d.Email, OwnerName=d.ContactPerson, d.Website, Status = d.Status == 1 ? "Active" : "In-Active" }).ToList();

        if (Session["UserType"].ToString() != "SuperAdmin")
        {
            CompanyData = CompanyData.Where(d => d.TenantId == Session["TenantId"].ToString()).ToList();
        }
        int companyid = Convert.ToInt32(Session["CompanyID"]);
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
            CompanyData = CompanyData.Where(d => d.CompanyId == companyid).ToList();
        }
        grd_Company.DataSource = CompanyData;
            grd_Company.DataBind();
           
        
    }
    protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {
        // Changes by Abdul Rahman 1/12/2014 Link button change to Image button
    //protected void OnClick_Edit(object sender, EventArgs e)
    //{
        ImageButton lnk = (ImageButton)sender;
        lblCompanyid.Text = lnk.CommandArgument.ToString();
        CompanyInfoTB MTB = HR.CompanyInfoTBs.Where(d => d.CompanyId == Convert.ToInt32(lnk.CommandArgument.ToString())).First();
       txtAddress.Text= MTB.Address ;
       fillCountry();
       if (!string.IsNullOrEmpty(MTB.Country.ToString()))
       {
           ddlCountry.SelectedValue = MTB.Country.ToString();
       }
       txtCompanyName.Text= MTB.CompanyName ;
       txtContact.Text=   MTB.ContactNo;
       lbllogo.Text = MTB.Imgpath;
       txtEmail.Text=  MTB.Email  ;
        txtShortName.Text = MTB.ShortName;

       txtLandline.Text=  MTB.LandlineNo ;
       txtPincode.Text= MTB.Pincode ;
    
       txtWebsite.Text= MTB.Website  ;
       MultiView1.ActiveViewIndex = 1;
       btnSave.Text = "Update";
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (btnSave.Text == "Save")
        {
            var dte = from p in HR.CompanyInfoTBs.Where(d => d.CompanyName == txtCompanyName.Text) select p;
            if (dte.Count() > 0)
            {
                g.ShowMessage(this.Page, "Company Name Already Exist");
                //modpop.ShowPopUp();

            }
            else
            {              
                CompanyInfoTB MTB = new CompanyInfoTB();
                MTB.Address = txtAddress.Text;
                MTB.CompanyName = txtCompanyName.Text;
                MTB.ShortName = txtShortName.Text;
                MTB.ContactNo = txtContact.Text;
                if(ddlCountry.SelectedIndex>0)
                MTB.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                MTB.Email = txtEmail.Text;
                MTB.LandlineNo = txtLandline.Text;
                MTB.Pincode = txtPincode.Text;
                //  MTB.State = Convert.ToInt32(ddlState.SelectedValue);
                MTB.Website = txtWebsite.Text;
                MTB.Imgpath = path;
                MTB.TenantId = Session["TenantId"].ToString();
                MTB.Status = 1;
                HR.CompanyInfoTBs.InsertOnSubmit(MTB);
                HR.SubmitChanges();

                //modpop.Message = "Submitted Successfully";
                //modpop.ShowPopUp();



                var companydata = HR.CompanyInfoTBs.Where(a => a.CompanyName == txtCompanyName.Text && a.Email == txtEmail.Text && a.TenantId == Session["TenantId"].ToString()).FirstOrDefault();

                if(companydata != null)
                {
                    #region Add Manual Device
                    DevicesTB device = new DevicesTB();
                    device.DeviceName = "ME";
                    device.DeviceSerialNo = "123";
                    device.IPAddress = "192.168.1.1";

                    var data = HR.DevicesTBs.OrderByDescending(u => u.DeviceId).FirstOrDefault();
                    int maxId = data == null ? 1 : data.DeviceId + 1;
                    device.DeviceAccountId = String.Format("{0,7:100000}", maxId).Trim();

                    device.DeviceDirection = "InOut";
                    device.CompanyId = companydata.CompanyId;
                    device.TenantId = Session["TenantId"].ToString();
                    device.PortNo = 9099;
                    device.UserName = "admin";
                    device.Password = "Admin@1234";
                    device.DeviceIp = "192.168.1.1";
                    device.DeviceModel = "Hikvision";
                    HR.DevicesTBs.InsertOnSubmit(device);
                    HR.SubmitChanges();
                    #endregion
                }


                g.ShowMessage(this.Page, "Submitted Successfully");
                Clear();
            }
        }
        else
        {
            // Check duplicate company name on the time of update ..By Abdul rahman
            var check = from p in HR.CompanyInfoTBs.Where(d => d.CompanyName == txtCompanyName.Text && d.CompanyId == Convert.ToInt32(lblCompanyid.Text)) select p;
            if (check.Count() > 0)
            {
                updatecode();
               // g.ShowMessage(this.Page, "Company Name Already Exist");
                //modpop.ShowPopUp();

            }
            else
            {
                var check1 = from p in HR.CompanyInfoTBs.Where(d => d.CompanyName == txtCompanyName.Text && d.CompanyId != Convert.ToInt32(lblCompanyid.Text)) select p;
                if (check1.Count() > 0)
                {
                    
                    g.ShowMessage(this.Page, "Company Name Already Exist");
                    MultiView1.ActiveViewIndex = 1;
                    txtCompanyName.Focus();
                    //modpop.ShowPopUp();

                }
                else
                {
                    updatecode();
                }
            }
        }
    }
    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }
    private void updatecode()
    {
        try
        {
            CompanyInfoTB MTB = HR.CompanyInfoTBs.Where(d => d.CompanyId == Convert.ToInt32(lblCompanyid.Text)).First();
           
            MTB.Address = txtAddress.Text;
            MTB.ShortName = txtShortName.Text;
            MTB.CompanyName = txtCompanyName.Text;
            MTB.ContactNo = txtContact.Text;
            if(ddlCountry.SelectedValue != "")
            {
                MTB.Country = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            
            MTB.Email = txtEmail.Text;
            MTB.LandlineNo = txtLandline.Text;
            MTB.Pincode = txtPincode.Text;
            MTB.Imgpath = path;
        //    MTB.State = Convert.ToInt32(ddlState.SelectedValue);
            MTB.Website = txtWebsite.Text;
            MTB.Status = 1;
            MTB.TenantId = Session["TenantId"].ToString();
            HR.SubmitChanges();
             g.ShowMessage(this.Page, "Updated Successfully");
             btnSave.Text = "Save";
             Clear();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        //Clear();
    }
    public void Clear()
    {
        clearFields();
        BindAllCompany();
        MultiView1.ActiveViewIndex = 0;
        btnSave.Text = "Save";
        lbllogo.Text = "";

    }
    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
        txtWebsite.Text = "http://";
    }

    private void clearFields()
    {
        ClearInputs(Page.Controls);
        MultiView1.ActiveViewIndex = 0; 
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        txtWebsite.Text = "http://";
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
     //   fillCity(Convert.ToInt32(ddlState.SelectedValue));
    }
    protected void grd_Company_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Company.PageIndex = e.NewPageIndex;
        BindAllCompany();
        grd_Company.DataBind();
    }
    
    protected void txtAddress_TextChanged(object sender, EventArgs e)
    {
        if (!txtAddress.Text.Any(char.IsLetter))
        {
            g.ShowMessage(this.Page, "Enter Valid Address");
            txtAddress.Focus();
            txtAddress.Text = "";
        }
    }

    protected void grd_Company_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Company.Rows.Count > 0)
        {
            grd_Company.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Company.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = (ImageButton)sender;
        lblCompanyid.Text = lnk.CommandArgument.ToString();
        CompanyInfoTB MTB = HR.CompanyInfoTBs.Where(d => d.CompanyId == Convert.ToInt32(lnk.CommandArgument.ToString())).First();

        MTB.Status = 0;
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "Company Delete Successfully.....");
    }
}