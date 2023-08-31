using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class MasterDocument : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    string path = "";
    DataTable dtUpload = new DataTable();
    DataTable dtUpload1 = new DataTable();
    DataTable dtUpload2 = new DataTable();
    DataTable dtUpload3 = new DataTable();
    DataTable dtUpload4 = new DataTable();
    DataTable dtUpload5 = new DataTable();

   
    string newFileName = null;
    Genreal g = new Genreal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {

                MultiView1.ActiveViewIndex = 0;
                BindAllDOCUMENT();
                fillcompany();
            }
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string FolderPath = Server.MapPath("Attachments");
                MakeDirectoryIfExist(FolderPath);
               
                //if (FileUploadDocu.HasFile)
                //{
                  //  string filename = Path.GetFileName(FileUploadDocu.FileName);

                  //  string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "txt" };

                  //  Session["FileName"] = filename;
                  //  newFileName = Convert.ToString(Session["FileName"]);

                  ////  FileUpload1.SaveAs(Server.MapPath("Attachments/" + filename));
                  //  FileUploadDocu.SaveAs(Server.MapPath("Attachments/" + filename));
                  // // FileUploadDocu.SaveAs(Request.PhysicalApplicationPath + "\\Attachments\\" + filename + "");
                  //  path = filename;
                  //  lblpath.Text = path;

                    if (FileUploadDocu.HasFile)
                    {
                        int cnt = 0;
                        string dir = Server.MapPath("Attachments/");
                        string[] files;
                        int numFiles;
                        files = Directory.GetFiles(dir);
                        numFiles = files.Length;

                        string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                        string filenamee = Path.GetFileName(FileUploadDocu.FileName);
                        string[] filename1 = filenamee.Split('.');
                        for (int i = 0; i < numFiles; i++)
                        {
                            if (File.Exists(Server.MapPath("Attachments/" + filename1[0] + cnt + "." + filename1[1])))
                            {
                                cnt++;
                            }
                        }
                        FileUploadDocu.SaveAs(Request.PhysicalApplicationPath + "Attachments/" + filename1[0] + cnt + "." + filename1[1]);
                        path = filename1[0] + cnt + "." + filename1[1];
                        lblpath.Text = path;
                    }
                    else
                    {
                        // g.ShowMessage(this.Page, "Please Upload File");
                        // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Please Upload File');", true);
                    }
                //}
                if (FileUpload2.HasFile)
                {
                 //   string filename = Path.GetFileName(FileUpload2.FileName);

                 //   string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "txt" };

                 //   Session["FileName"] = filename;
                 //   newFileName = Convert.ToString(Session["FileName"]);
                 ////   FileUpload2.SaveAs(Request.PhysicalApplicationPath + "\\Attachments\\" + filename + "");
                 //   FileUpload2.SaveAs(Server.MapPath("Attachments/" + filename));

                 //   path = filename;
                 //   lblPath1.Text = path;

                    int cnt = 0;
                    string dir = Server.MapPath("Attachments/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUpload2.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("Attachments/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUpload2.SaveAs(Request.PhysicalApplicationPath + "Attachments/" + filename1[0] + cnt + "." + filename1[1]);
                    path = filename1[0] + cnt + "." + filename1[1];
                    lblPath1.Text = path;
                }
                if (FileUpload3.HasFile)
                {
                 //   string filename = Path.GetFileName(FileUpload3.FileName);

                 //   string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "txt" };

                 //   Session["FileName"] = filename;
                 //   newFileName = Convert.ToString(Session["FileName"]);
                 ////   FileUpload3.SaveAs(Request.PhysicalApplicationPath + "\\Attachments\\" + filename + "");

                 //   FileUpload3.SaveAs(Server.MapPath("Attachments/" + filename));
                 //   path = filename;
                 //   lblPath2.Text = path;

                    int cnt = 0;
                    string dir = Server.MapPath("Attachments/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUpload3.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("Attachments/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUpload3.SaveAs(Request.PhysicalApplicationPath + "Attachments/" + filename1[0] + cnt + "." + filename1[1]);
                    path = filename1[0] + cnt + "." + filename1[1];
                    lblPath2.Text = path;
                }
                if (FileUpload4.HasFile)
                {
                  //  string filename = Path.GetFileName(FileUpload4.FileName);

                  //  string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "txt" };

                  //  Session["FileName"] = filename;
                  //  newFileName = Convert.ToString(Session["FileName"]);
                  ////  FileUpload4.SaveAs(Request.PhysicalApplicationPath + "\\Attachments\\" + filename + "");

                  //  FileUpload4.SaveAs(Server.MapPath("Attachments/" + filename));
                  //  path = filename;
                  //  lblPath3.Text = path;

                    int cnt = 0;
                    string dir = Server.MapPath("Attachments/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUpload4.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("Attachments/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUpload4.SaveAs(Request.PhysicalApplicationPath + "Attachments/" + filename1[0] + cnt + "." + filename1[1]);
                    path = filename1[0] + cnt + "." + filename1[1];
                    lblPath3.Text = path;
                }
                if (FileUpload5.HasFile)
                {
                 //   string filename = Path.GetFileName(FileUpload5.FileName);

                 //   string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "txt" };

                 //   Session["FileName"] = filename;
                 //   newFileName = Convert.ToString(Session["FileName"]);
                 ////   FileUpload5.SaveAs(Request.PhysicalApplicationPath + "\\Attachments\\" + filename + "");
                 //   FileUpload5.SaveAs(Server.MapPath("Attachments/" + filename));

                 //   path = filename;
                 //   lblPath4.Text = path;

                    int cnt = 0;
                    string dir = Server.MapPath("Attachments/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUpload5.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("Attachments/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUpload5.SaveAs(Request.PhysicalApplicationPath + "Attachments/" + filename1[0] + cnt + "." + filename1[1]);
                    path = filename1[0] + cnt + "." + filename1[1];
                    lblPath4.Text = path;
                }
                if (FileUpload6.HasFile)
                {
                  //  string filename = Path.GetFileName(FileUpload6.FileName);

                  //  string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "txt" };

                  //  Session["FileName"] = filename;
                  //  newFileName = Convert.ToString(Session["FileName"]);
                  ////  FileUpload6.SaveAs(Request.PhysicalApplicationPath + "\\Attachments\\" + filename + "");

                  //  FileUpload6.SaveAs(Server.MapPath("Attachments/" + filename));
                  //  path = filename;
                  //  lblPath5.Text = path;

                    int cnt = 0;
                    string dir = Server.MapPath("Attachments/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUpload6.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("Attachments/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUpload6.SaveAs(Request.PhysicalApplicationPath + "Attachments/" + filename1[0] + cnt + "." + filename1[1]);
                    path = filename1[0] + cnt + "." + filename1[1];
                    lblPath5.Text = path;
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
    public void BindAllEmployeeDOCUMENT()
    {
        var docData = (from d in HR.EmployeeDocumentMasterTBs
                       join e in HR.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                       where d.EmployeeId == Convert.ToInt32(Session["E1"])
                       select new
                       {
                           d.EmployeeId,
                           Status = d.Status == "0" ? "Passport Documents" : (d.Status == "1" ? "Visa Documents" : (d.Status == "2" ? "CV Documents" : (d.Status == "3" ? "Labour Card Documents" : (d.Status == "4" ? "Other Attachment" : ((d.Status == "5" ? "Other Attachment" : "Other Attachment")))))), 
                     
                           Name = e.FName + ' ' + e.MName + ' ' + e.Lname,
                           d.DocumentName

                       }).Distinct();
        if (docData.Count() > 0)
        {
            Grd_Employee_Documents.DataSource = docData;
            Grd_Employee_Documents.DataBind();
        }
        else
        {
            Grd_Employee_Documents.DataSource = null;
            Grd_Employee_Documents.DataBind();
        }

        for (int i = 0; i < Grd_Employee_Documents.Rows.Count; i++)
        {
            Image img = (Image)Grd_Employee_Documents.Rows[i].FindControl("ImageDoc");
            img.ImageUrl = "~\\Attachments\\" + Grd_Employee_Documents.Rows[i].Cells[2].Text;
        }
    }

    public void BindAllDOCUMENT()
    {
        var docData = (from d in HR.EmployeeDocumentMasterTBs 
                       join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                       join e in HR.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                       select new 
                       { 
                           d.EmployeeId,
                            c.CompanyName, 
                           Name=e.FName+' '+e.MName+' '+e.Lname
                      
                       }).Distinct();
        if (docData.Count() > 0)
        {
            grd_DOCUMENT.DataSource = docData;
            grd_DOCUMENT.DataBind();
            lblcnt.Text = docData.Count().ToString();
        }
    }

    protected void OnClick_Edit(object sender, EventArgs e)
    {

        LinkButton Lnk = (LinkButton)sender;
        string EmployeetId = Lnk.CommandArgument;
        Session["EmpIdd"] = EmployeetId;

        MultiView1.ActiveViewIndex = 1;
        EmployeeDocumentMasterTB MT = HR.EmployeeDocumentMasterTBs.Where(d => d.EmployeeId == Convert.ToInt32(EmployeetId)).First();
     
        ddlCompany.SelectedValue = MT.CompanyId.ToString();
        fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
        ddldept.SelectedValue = MT.DeptId.ToString();
        fillEmployee(Convert.ToInt32(ddldept.SelectedValue));
        
        ddlEmployee.SelectedValue = MT.EmployeeId.ToString();

       
    
      
      

        DataTable dsUpload = g.ReturnData("select DocumentName from EmployeeDocumentMasterTB where EmployeeId ='" + EmployeetId + "' and Status=0 ");
        ViewState["dtUpload"] = dsUpload;
        GridViewUpload.DataSource = dsUpload;
        GridViewUpload.DataBind();


        DataTable dsUpload1 = g.ReturnData("select DocumentName from EmployeeDocumentMasterTB where EmployeeId ='" + EmployeetId + "' and Status=1 ");
        ViewState["dtUpload1"] = dsUpload1;
        Grd_Upload1.DataSource = dsUpload1;
        Grd_Upload1.DataBind();

        DataTable dsUpload2 = g.ReturnData("select DocumentName from EmployeeDocumentMasterTB where EmployeeId ='" + EmployeetId + "' and Status=2 ");
        ViewState["dtUpload2"] = dsUpload2;
        Grd_Upload2.DataSource = dsUpload2;
        Grd_Upload2.DataBind();

        DataTable dsUpload3 = g.ReturnData("select DocumentName from EmployeeDocumentMasterTB where EmployeeId ='" + EmployeetId + "' and Status=3 ");
        ViewState["dtUpload3"] = dsUpload3;
        Grd_Upload3.DataSource = dsUpload3;
        Grd_Upload3.DataBind();

        DataTable dsUpload4 = g.ReturnData("select DocumentName from EmployeeDocumentMasterTB where EmployeeId ='" + EmployeetId + "' and Status=4 ");
        ViewState["dtUpload4"] = dsUpload4;
        Grd_Upload4.DataSource = dsUpload4;
        Grd_Upload4.DataBind();

        DataTable dsUpload5 = g.ReturnData("select DocumentName from EmployeeDocumentMasterTB where EmployeeId ='" + EmployeetId + "' and Status=5 ");
        ViewState["dtUpload5"] = dsUpload5;
        Grd_Upload5.DataSource = dsUpload5;
        Grd_Upload5.DataBind();


        btnsubmit.Text = "Update";

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
                 if (ViewState["dtUpload"] != null)
                 {
                     dtUpload = ViewState["dtUpload"] as DataTable;

                     for (int i = 0; i < dtUpload.Rows.Count; i++)
                     {
                         EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                         D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                         D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                         D.EmployeeId =Convert.ToInt32(ddlEmployee.SelectedValue);
                         D.DocumentName = dtUpload.Rows[i][0].ToString();
                         D.Status = "0";
                         HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                         HR.SubmitChanges();

                     }
                     ViewState["dtUpload"] = null;
                     GridViewUpload.DataSource = null;
                     GridViewUpload.DataBind();
                 }
                 if (ViewState["dtUpload1"] != null)
                 {
                     dtUpload1 = ViewState["dtUpload1"] as DataTable;

                     for (int i = 0; i < dtUpload1.Rows.Count; i++)
                     {
                         EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                         D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                         D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                         D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                         D.DocumentName = dtUpload1.Rows[i][0].ToString();
                         D.Status = "1";
                         HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                         HR.SubmitChanges();

                     }
                     ViewState["dtUpload1"] = null;
                     Grd_Upload1.DataSource = null;
                     Grd_Upload1.DataBind();
                 }
                 if (ViewState["dtUpload2"] != null)
                 {
                     dtUpload2 = ViewState["dtUpload2"] as DataTable;

                     for (int i = 0; i < dtUpload2.Rows.Count; i++)
                     {
                         EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                         D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                         D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                         D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                         D.DocumentName = dtUpload2.Rows[i][0].ToString();
                         D.Status = "2";
                         HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                         HR.SubmitChanges();

                     }
                     ViewState["dtUpload2"] = null;
                     Grd_Upload2.DataSource = null;
                     Grd_Upload2.DataBind();
                 }
                 if (ViewState["dtUpload3"] != null)
                 {
                     dtUpload3 = ViewState["dtUpload3"] as DataTable;

                     for (int i = 0; i < dtUpload3.Rows.Count; i++)
                     {
                         EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                         D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                         D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                         D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                         D.DocumentName = dtUpload3.Rows[i][0].ToString();
                         D.Status = "3";
                         HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                         HR.SubmitChanges();

                     }
                     ViewState["dtUpload3"] = null;
                     Grd_Upload3.DataSource = null;
                     Grd_Upload3.DataBind();
                 }
                 if (ViewState["dtUpload4"] != null)
                 {
                     dtUpload4 = ViewState["dtUpload4"] as DataTable;

                     for (int i = 0; i < dtUpload4.Rows.Count; i++)
                     {
                         EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                         D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                         D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                         D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                         D.DocumentName = dtUpload4.Rows[i][0].ToString();
                         D.Status = "4";
                         HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                         HR.SubmitChanges();

                     }
                     ViewState["dtUpload4"] = null;
                     Grd_Upload4.DataSource = null;
                     Grd_Upload4.DataBind();
                 }
                 if (ViewState["dtUpload5"] != null)
                 {
                     dtUpload5 = ViewState["dtUpload5"] as DataTable;

                     for (int i = 0; i < dtUpload5.Rows.Count; i++)
                     {
                         EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                         D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                         D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                         D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                         D.DocumentName = dtUpload5.Rows[i][0].ToString();
                         D.Status = "5";
                         HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                         HR.SubmitChanges();

                     }
                     ViewState["dtUpload5"] = null;
                     Grd_Upload5.DataSource = null;
                     Grd_Upload5.DataBind();
                 }

                 modpop.Message = "Submitted Successfully";
                 modpop.ShowPopUp();


                 Clear();
          
        }
        else
        {
            DataTable ds1 = g.ReturnData("delete from EmployeeDocumentMasterTB where EmployeeId='" + Convert.ToInt32(Session["EmpIdd"]) + "'");


            if (ViewState["dtUpload"] != null)
            {
                dtUpload = ViewState["dtUpload"] as DataTable;

                for (int i = 0; i < dtUpload.Rows.Count; i++)
                {
                    EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                    D.EmployeeId = Convert.ToInt32(Convert.ToInt32(Session["EmpIdd"]));

                    D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                    D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);


                    D.DocumentName = dtUpload.Rows[i][0].ToString();
                    D.Status = "0";
                    HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                    HR.SubmitChanges();

                }
                ViewState["dtUpload"] = null;
                GridViewUpload.DataSource = null;
                GridViewUpload.DataBind();
            }
            if (ViewState["dtUpload1"] != null)
            {
                dtUpload1 = ViewState["dtUpload1"] as DataTable;

                for (int i = 0; i < dtUpload1.Rows.Count; i++)
                {
                    EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                    D.EmployeeId = Convert.ToInt32(Convert.ToInt32(Session["EmpIdd"]));
                    D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                    D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                    D.DocumentName = dtUpload1.Rows[i][0].ToString();
                    D.Status = "1";
                    HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                    HR.SubmitChanges();

                }
                ViewState["dtUpload1"] = null;
                Grd_Upload1.DataSource = null;
                Grd_Upload1.DataBind();
            }
            if (ViewState["dtUpload2"] != null)
            {
                dtUpload2 = ViewState["dtUpload2"] as DataTable;

                for (int i = 0; i < dtUpload2.Rows.Count; i++)
                {
                    EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                   // D.EmployeeId = Convert.ToInt32(Convert.ToInt32(Session["EmpIdd"]));
                    D.DocumentName = dtUpload2.Rows[i][0].ToString();
                    D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                    D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                    D.Status = "2";
                    HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                    HR.SubmitChanges();

                }
                ViewState["dtUpload2"] = null;
                Grd_Upload2.DataSource = null;
                Grd_Upload2.DataBind();
            }
            if (ViewState["dtUpload3"] != null)
            {
                dtUpload3 = ViewState["dtUpload3"] as DataTable;

                for (int i = 0; i < dtUpload3.Rows.Count; i++)
                {
                    EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                    D.EmployeeId = Convert.ToInt32(Convert.ToInt32(Session["EmpIdd"]));
                    D.DocumentName = dtUpload3.Rows[i][0].ToString();
                    D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                    D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);

                    D.Status = "3";
                    HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                    HR.SubmitChanges();

                }
                ViewState["dtUpload3"] = null;
                Grd_Upload3.DataSource = null;
                Grd_Upload3.DataBind();
            }
            if (ViewState["dtUpload4"] != null)
            {
                dtUpload4 = ViewState["dtUpload4"] as DataTable;

                for (int i = 0; i < dtUpload4.Rows.Count; i++)
                {
                    EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                    D.EmployeeId = Convert.ToInt32(Convert.ToInt32(Session["EmpIdd"]));
                    D.DocumentName = dtUpload4.Rows[i][0].ToString();
                    D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                    D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);

                    D.Status = "4";
                    HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                    HR.SubmitChanges();

                }
                ViewState["dtUpload4"] = null;
                Grd_Upload4.DataSource = null;
                Grd_Upload4.DataBind();
            }
            if (ViewState["dtUpload5"] != null)
            {
                dtUpload5 = ViewState["dtUpload5"] as DataTable;

                for (int i = 0; i < dtUpload5.Rows.Count; i++)
                {
                    EmployeeDocumentMasterTB D = new EmployeeDocumentMasterTB();
                    D.EmployeeId = Convert.ToInt32(Convert.ToInt32(Session["EmpIdd"]));
                    D.DocumentName = dtUpload5.Rows[i][0].ToString();
                    D.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    D.DeptId = Convert.ToInt32(ddldept.SelectedValue);
                    D.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                    D.Status = "5";
                    HR.EmployeeDocumentMasterTBs.InsertOnSubmit(D);
                    HR.SubmitChanges();

                }
                ViewState["dtUpload5"] = null;
                Grd_Upload5.DataSource = null;
                Grd_Upload5.DataBind();
            }
            modpop.Message = "Updated Successfully";
            modpop.ShowPopUp();
            btnsubmit.Text = "Save";
            Clear();
        }
    }

    public void Clear()
    {
        BindAllDOCUMENT();
        fillcompany();
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
    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
   
    protected void BtnUpload1_Click(object sender, EventArgs e)
    {

    }
    protected void BtnUpload2_Click(object sender, EventArgs e)
    {

    }
    protected void BtnUpload3_Click(object sender, EventArgs e)
    {

    }
    protected void BtnUpload4_Click(object sender, EventArgs e)
    {

    }
    protected void BtnUpload5_Click(object sender, EventArgs e)
    {

    }
    private void fillDept(int companyid)
    {

        var data = from dt in HR.MasterDeptTBs
                   where dt.Status == 0 && dt.CompanyId == companyid
                   select dt;
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
            ddlEmployee.Items.Clear();
        }
    }
    private void fillEmployee(int Dept)
    {

        var data = from dt in HR.EmployeeTBs
                   where dt.DeptId == Dept
                   select new 
                   {
                   dt.EmployeeId,
                    Name=dt.FName+' '+dt.MName+' '+dt.Lname
                   };
        if (data != null && data.Count() > 0)
        {

            ddlEmployee.DataSource = data;
            ddlEmployee.DataTextField = "Name";
            ddlEmployee.DataValueField = "EmployeeId";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "--Select--");


        }
        else
        {
            ddlEmployee.DataSource = null;
            ddlEmployee.DataBind();
            ddlEmployee.Items.Clear();
      
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex != 0)
        {
            fillDept(Convert.ToInt32(ddlCompany.SelectedValue));
        }
        else
        {
            ddldept.Items.Clear();
            ddlEmployee.Items.Clear();
        }
        
    }
    protected void dddept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldept.SelectedIndex != 0)
        {
            fillEmployee(Convert.ToInt32(ddldept.SelectedValue));
        }
        else
        {
            ddldept.Items.Clear();
            ddlEmployee.Items.Clear();
        }
    }
    protected void BtnAddDocument1_Click(object sender, EventArgs e)
    {
        if (lblPath1.Text == "")
        {
            g.ShowMessage(this.Page, "Please Upload Documents");
        }
        else
        {
            int cnt = 0;
            if (ViewState["dtUpload1"] != null)
            {
                dtUpload1 = (DataTable)ViewState["dtUpload1"];
            }
            else
            {
                DataColumn DocumentName = dtUpload1.Columns.Add("DocumentName");
            }

            DataRow dr = dtUpload1.NewRow();
            dr[0] = lblPath1.Text;

            if (dtUpload1.Rows.Count > 0)
            {
                for (int f = 0; f < dtUpload1.Rows.Count; f++)
                {
                    string u2 = dtUpload1.Rows[f][0].ToString();
                    if (u2 == lblPath1.Text)
                    {
                        cnt++;
                    }
                    else
                    {

                    }
                }
                if (cnt > 0)
                {
                    g.ShowMessage(this.Page, "Document Already Exist");
                }
                else
                {
                    dtUpload1.Rows.Add(dr);
                    lblPath1.Text = "";
                }
            }
            else
            {
                dtUpload1.Rows.Add(dr);
                lblPath1.Text = "";
            }

            ViewState["dtUpload1"] = dtUpload1;

            Grd_Upload1.DataSource = dtUpload1;
            Grd_Upload1.DataBind();

        }
    }
    protected void BtnAddDocument2_Click(object sender, EventArgs e)
    {
        if (lblPath2.Text == "")
        {
            g.ShowMessage(this.Page, "Please Upload Documents");
        }
        else
        {
            int cnt = 0;
            if (ViewState["dtUpload2"] != null)
            {
                dtUpload2 = (DataTable)ViewState["dtUpload2"];
            }
            else
            {
                DataColumn DocumentName = dtUpload2.Columns.Add("DocumentName");
            }

            DataRow dr = dtUpload2.NewRow();
            dr[0] = lblPath2.Text;

            if (dtUpload2.Rows.Count > 0)
            {
                for (int f = 0; f < dtUpload2.Rows.Count; f++)
                {
                    string u2 = dtUpload2.Rows[f][0].ToString();
                    if (u2 == lblPath2.Text)
                    {
                        cnt++;
                    }
                    else
                    {

                    }
                }
                if (cnt > 0)
                {
                    g.ShowMessage(this.Page, "Document Already Exist");
                }
                else
                {
                    dtUpload2.Rows.Add(dr);
                    lblPath2.Text = "";
                }
            }
            else
            {
                dtUpload2.Rows.Add(dr);
                lblPath2.Text = "";
            }

            ViewState["dtUpload2"] = dtUpload2;

            Grd_Upload2.DataSource = dtUpload2;
            Grd_Upload2.DataBind();

        }
    }
    protected void BtnAddDocument3_Click(object sender, EventArgs e)
    {
        if (lblPath3.Text == "")
        {
            g.ShowMessage(this.Page, "Please Upload Documents");
        }
        else
        {
            int cnt = 0;
            if (ViewState["dtUpload3"] != null)
            {
                dtUpload3 = (DataTable)ViewState["dtUpload3"];
            }
            else
            {
                DataColumn DocumentName = dtUpload3.Columns.Add("DocumentName");
            }

            DataRow dr = dtUpload3.NewRow();
            dr[0] = lblPath3.Text;

            if (dtUpload3.Rows.Count > 0)
            {
                for (int f = 0; f < dtUpload3.Rows.Count; f++)
                {
                    string u2 = dtUpload3.Rows[f][0].ToString();
                    if (u2 == lblPath3.Text)
                    {
                        cnt++;
                    }
                    else
                    {

                    }
                }
                if (cnt > 0)
                {
                    g.ShowMessage(this.Page, "Document Already Exist");
                }
                else
                {
                    dtUpload3.Rows.Add(dr);
                    lblPath3.Text = "";
                }
            }
            else
            {
                dtUpload3.Rows.Add(dr);
                lblPath3.Text = "";
            }

            ViewState["dtUpload3"] = dtUpload3;

            Grd_Upload3.DataSource = dtUpload3;
            Grd_Upload3.DataBind();

        }
    }


    protected void BtnAddDocument4_Click(object sender, EventArgs e)
    {
        if (lblPath4.Text == "")
        {
            g.ShowMessage(this.Page, "Please Upload Documents");
        }
        else
        {
            int cnt = 0;
            if (ViewState["dtUpload4"] != null)
            {
                dtUpload4 = (DataTable)ViewState["dtUpload4"];
            }
            else
            {
                DataColumn DocumentName = dtUpload4.Columns.Add("DocumentName");
            }

            DataRow dr = dtUpload4.NewRow();
            dr[0] = lblPath4.Text;

            if (dtUpload4.Rows.Count > 0)
            {
                for (int f = 0; f < dtUpload4.Rows.Count; f++)
                {
                    string u2 = dtUpload4.Rows[f][0].ToString();
                    if (u2 == lblPath4.Text)
                    {
                        cnt++;
                    }
                    else
                    {

                    }
                }
                if (cnt > 0)
                {
                    g.ShowMessage(this.Page, "Document Already Exist");
                }
                else
                {
                    dtUpload4.Rows.Add(dr);
                    lblPath4.Text = "";
                }
            }
            else
            {
                dtUpload4.Rows.Add(dr);
                lblPath4.Text = "";
            }

            ViewState["dtUpload4"] = dtUpload4;

            Grd_Upload4.DataSource = dtUpload4;
            Grd_Upload4.DataBind();
        }

    }
    protected void BtnAddDocument5_Click(object sender, EventArgs e)
    {
        if (lblPath5.Text == "")
        {
            g.ShowMessage(this.Page, "Please Upload Documents");
        }
        else
        {
            int cnt = 0;
            if (ViewState["dtUpload5"] != null)
            {
                dtUpload5 = (DataTable)ViewState["dtUpload5"];
            }
            else
            {
                DataColumn DocumentName = dtUpload5.Columns.Add("DocumentName");
            }

            DataRow dr = dtUpload5.NewRow();
            dr[0] = lblPath5.Text;

            if (dtUpload5.Rows.Count > 0)
            {
                for (int f = 0; f < dtUpload5.Rows.Count; f++)
                {
                    string u2 = dtUpload5.Rows[f][0].ToString();
                    if (u2 == lblPath5.Text)
                    {
                        cnt++;
                    }
                    else
                    {

                    }
                }
                if (cnt > 0)
                {
                    g.ShowMessage(this.Page, "Document Already Exist");
                }
                else
                {
                    dtUpload5.Rows.Add(dr);
                    lblPath5.Text = "";
                }
            }
            else
            {
                dtUpload5.Rows.Add(dr);
                lblPath5.Text = "";
            }

            ViewState["dtUpload5"] = dtUpload5;

            Grd_Upload5.DataSource = dtUpload5;
            Grd_Upload5.DataBind();
        }


    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton DeleteDocu = (LinkButton)sender;
        dtUpload = (DataTable)ViewState["dtUpload"];
        foreach (DataRow d in dtUpload.Rows)
        {
            if (d[0].ToString() == DeleteDocu.CommandArgument)
            {
                d.Delete();
                dtUpload.AcceptChanges();
                break;
            }
        }
        GridViewUpload.DataSource = dtUpload;
        GridViewUpload.DataBind();
    }
    protected void lnkDelete5_Click(object sender, EventArgs e)
    {
        LinkButton DeleteDocu = (LinkButton)sender;
        dtUpload5 = (DataTable)ViewState["dtUpload5"];
        foreach (DataRow d in dtUpload5.Rows)
        {
            if (d[0].ToString() == DeleteDocu.CommandArgument)
            {
                d.Delete();
                dtUpload5.AcceptChanges();
                break;
            }
        }
        Grd_Upload5.DataSource = dtUpload5;
        Grd_Upload5.DataBind();
    }
    protected void lnkDelete4_Click(object sender, EventArgs e)
    {
        LinkButton DeleteDocu = (LinkButton)sender;
        dtUpload4 = (DataTable)ViewState["dtUpload4"];
        foreach (DataRow d in dtUpload4.Rows)
        {
            if (d[0].ToString() == DeleteDocu.CommandArgument)
            {
                d.Delete();
                dtUpload4.AcceptChanges();
                break;
            }
        }
        Grd_Upload4.DataSource = dtUpload4;
        Grd_Upload4.DataBind();
    }
    protected void lnkDelete3_Click(object sender, EventArgs e)
    {
        LinkButton DeleteDocu = (LinkButton)sender;
        dtUpload3 = (DataTable)ViewState["dtUpload3"];
        foreach (DataRow d in dtUpload3.Rows)
        {
            if (d[0].ToString() == DeleteDocu.CommandArgument)
            {
                d.Delete();
                dtUpload3.AcceptChanges();
                break;
            }
        }
        Grd_Upload3.DataSource = dtUpload3;
        Grd_Upload3.DataBind();
    }
    protected void lnkDelete2_Click(object sender, EventArgs e)
    {
        LinkButton DeleteDocu = (LinkButton)sender;
        dtUpload2 = (DataTable)ViewState["dtUpload2"];
        foreach (DataRow d in dtUpload2.Rows)
        {
            if (d[0].ToString() == DeleteDocu.CommandArgument)
            {
                d.Delete();
                dtUpload2.AcceptChanges();
                break;
            }
        }
        Grd_Upload2.DataSource = dtUpload2;
        Grd_Upload2.DataBind();
    }
    protected void lnkDelete1_Click(object sender, EventArgs e)
    {
        LinkButton DeleteDocu = (LinkButton)sender;
        dtUpload1 = (DataTable)ViewState["dtUpload1"];
        foreach (DataRow d in dtUpload1.Rows)
        {
            if (d[0].ToString() == DeleteDocu.CommandArgument)
            {
                d.Delete();
                dtUpload1.AcceptChanges();
                break;
            }
        }
        Grd_Upload1.DataSource = dtUpload1;
        Grd_Upload1.DataBind();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUploadDocu.HasFile)
        {
            string filename = Path.GetFileName(FileUploadDocu.FileName);

            string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "txt" };

            Session["FileName"] = filename;
            newFileName = Convert.ToString(Session["FileName"]);
            FileUploadDocu.SaveAs(Server.MapPath("Attachments/" + filename));
         //   FileUploadDocu.SaveAs(Request.PhysicalApplicationPath + "\\Attachments\\" + filename + "");
            path = filename;
            lblpath.Text = path;
        }
        else
        {
            g.ShowMessage(this.Page, "Please Upload File");
        }
    }
    protected void BtnAddDocument_Click(object sender, EventArgs e)
    {
        if (lblpath.Text == "")
        {
            g.ShowMessage(this.Page, "Please Upload Documents");
        }
        else
        {
            int cnt = 0;
            if (ViewState["dtUpload"] != null)
            {
                dtUpload = (DataTable)ViewState["dtUpload"];
            }
            else
            {
                DataColumn DocumentName = dtUpload.Columns.Add("DocumentName");
            }

            DataRow dr = dtUpload.NewRow();
            dr[0] = lblpath.Text;

            if (dtUpload.Rows.Count > 0)
            {
                for (int f = 0; f < dtUpload.Rows.Count; f++)
                {
                    string u2 = dtUpload.Rows[f][0].ToString();
                    if (u2 == lblpath.Text)
                    {
                        cnt++;
                    }
                    else
                    {

                    }
                }
                if (cnt > 0)
                {
                    g.ShowMessage(this.Page, "Document Already Exist");
                }
                else
                {
                    dtUpload.Rows.Add(dr);
                    lblpath.Text = "";
                }
            }
            else
            {
                dtUpload.Rows.Add(dr);
                lblpath.Text = "";
            }

            ViewState["dtUpload"] = dtUpload;

            GridViewUpload.DataSource = dtUpload;
            GridViewUpload.DataBind();

        }
    }
  
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void grd_DOCUMENT_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_DOCUMENT.PageIndex = e.NewPageIndex;
        BindAllDOCUMENT();
    }
    protected void imgbtnviewDocuments_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgEmp = (ImageButton)sender;
        int EmpId = Convert.ToInt32(imgEmp.CommandArgument);

        Session["E1"] = EmpId;
        ModelPoUpDocumentDetails.Show();
        BindAllEmployeeDOCUMENT();
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        LinkButton lnkDoc = (LinkButton)sender;
        string Docname = lnkDoc.CommandArgument;
        string filePath;
        var data = from doc in HR.EmployeeDocumentMasterTBs
                   where doc.DocumentName == Docname
                   select new
                   {
                       doc.DocumentName
                   };
        if (data.Count() > 0)
        {
            string DocumentName = data.First().DocumentName;



            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + DocumentName);
            //Response.TransmitFile(Server.MapPath("~/Attachments/" + DocumentName));
            //Response.End();




            filePath = "Attachments\\" + DocumentName;

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
    protected void ImageDoc_Click(object sender, ImageClickEventArgs e)
    {
        ModelPoUpDocumentDetails.Show();
    }
    protected void Grd_Employee_Documents_DataBound(object sender, EventArgs e)
    {
        for (int rowIndex = Grd_Employee_Documents.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvrow = Grd_Employee_Documents.Rows[rowIndex];
            GridViewRow gvprivous = Grd_Employee_Documents.Rows[rowIndex + 1];

            for (int cellCount = 0; cellCount < gvrow.Cells.Count; cellCount++)
            {
                if (cellCount == 1)
                {
                    if (gvrow.Cells[cellCount].Text == gvprivous.Cells[cellCount].Text)
                    {
                        if (gvprivous.Cells[cellCount].RowSpan < 2)
                        {
                            gvrow.Cells[cellCount].RowSpan = 2;
                        }

                        else
                        {
                            gvrow.Cells[cellCount].RowSpan = gvprivous.Cells[cellCount].RowSpan + 1;
                        }
                        gvprivous.Cells[cellCount].Visible = false;
                    }
                }

            }
        }
    }
}