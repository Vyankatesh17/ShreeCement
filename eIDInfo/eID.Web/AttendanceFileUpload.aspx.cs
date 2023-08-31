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

public partial class AttendanceFileUpload : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    HrPortalDtaClassDataContext RC = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null && Session["UserId"]!=null)
        {
            if (!IsPostBack)
            {
                bindProjectList();
                fuEnquiry.Attributes.Add("onchange", "return CheckFile(this);");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string FileName = Path.GetFileName(fuEnquiry.PostedFile.FileName);
        string Extension = Path.GetExtension(fuEnquiry.PostedFile.FileName);
        //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];


        int i = 0;
        string tempfileName = FileName;
        string availableFileName = FileName;
        while (System.IO.File.Exists(Server.MapPath(@"Upload\" + availableFileName)))
        {
            tempfileName = i.ToString() + "_" + availableFileName;
            availableFileName = tempfileName;
            i++;
        }


        string FilePath = Server.MapPath(@"Upload\" + tempfileName);
        fuEnquiry.SaveAs(FilePath);
        Import_To_GridNonEx(FilePath, Extension, "0");
    }
    private void Import_To_GridNonEx(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        int cnt = 0;
        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["Employee_id"].ToString()))
                {
                    var existcount = RC.LogRecordsDetails.Where(d => d.Employee_id == Convert.ToInt32(dt.Rows[i]["Employee_id"].ToString()) && d.Log_Date_Time == Convert.ToDateTime(dt.Rows[i]["Log_Date_Time"].ToString())).ToList();
                    if (existcount.Count() == 0)
                    {

                        LogRecordsDetail C = new LogRecordsDetail();
                        C.Log_Date_Time = Convert.ToDateTime(dt.Rows[i]["Log_Date_Time"].ToString());
                        C.Employee_id = Convert.ToInt32(dt.Rows[i]["Employee_id"].ToString());
                        //C.Mobile1 = "9999999999";
                        C.Emp_Name = dt.Rows[i]["Emp_Name"].ToString();


                        C.Status = "MP";
                        RC.LogRecordsDetails.InsertOnSubmit(C);
                        RC.SubmitChanges();
                        cnt++;
                    }
                    else
                    {
                        gen.ShowMessage(this.Page, " Records already present in database.");
                    }
                }
            }
        }
        if (cnt > 0)
        {
            gen.ShowMessage(this.Page, cnt + " Records Uploaded Successfully");
        }
     
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
    }
    private void bindProjectList()
    {
        //var projectData = (from d in RC.PropertyInfoTBs where d.PropertieType == 0 select new { d.PropertieID, d.Name }).Distinct();
        //if (projectData != null)
        //{
        //    ddlProject.DataSource = projectData;
        //    ddlProject.DataTextField = "Name";
        //    ddlProject.DataValueField = "PropertieID";
        //    ddlProject.DataBind();
        //    ddlProject.Items.Insert(0, "--Select--");
        //}
        //else
        //{
        //    ddlProject.Items.Clear();
        //    ddlProject.DataSource = null;
        //}
    }
}