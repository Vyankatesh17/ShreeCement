using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeDocument : System.Web.UI.Page
{
    /// <summary>
    /// Employee document form
    /// Created By Abdul Rahman
    /// Created date :02/12/2014
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string path = "";
    DataTable DTDocument = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                fillcompany();
                fillDocument();
             }
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string FolderPath = Server.MapPath("EmployeeDocument");
                MakeDirectoryIfExist(FolderPath);
                if (FileUploadDocu.HasFile)
                {
                    int cnt = 0;
                    string dir = Server.MapPath("EmployeeDocument/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUploadDocu.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("EmployeeDocument/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUploadDocu.SaveAs(Request.PhysicalApplicationPath + "EmployeeDocument/" + filename1[0] + cnt + "." + filename1[1]);
                    path = filename1[0] + cnt + "." + filename1[1];
                    lblpath.Text = path;
                }
                else
                {
                    // g.ShowMessage(this.Page, "Please Upload File");
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Please Upload File');", true);
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        { 
            if (ViewState["DTDocument"] != null)
            {
               if (btnsubmit.Text=="Save")
               {
          
                DTDocument = (DataTable)ViewState["DTDocument"];
                for (int i = 0; i < DTDocument.Rows.Count; i++)
                {
                    EmployeeDocumentMasterTB t = new EmployeeDocumentMasterTB();
                    t.CompanyId = Convert.ToInt32(DTDocument.Rows[i]["CompanyID"].ToString());
                    t.DeptId = Convert.ToInt32(DTDocument.Rows[i]["DepartmentID"].ToString());
                    t.EmployeeId = Convert.ToInt32(DTDocument.Rows[i]["EmployeeID"].ToString());
                    t.DocumentId = Convert.ToInt32(DTDocument.Rows[i]["DocumentID"].ToString());
                    t.DocumentName = DTDocument.Rows[i]["DocumentName"].ToString();
                    t.DocumentPath = DTDocument.Rows[i]["Documentpath"].ToString();
                    t.Description = DTDocument.Rows[i]["Description"].ToString();
                    t.Status = "0";
                    HR.EmployeeDocumentMasterTBs.InsertOnSubmit(t);
                    HR.SubmitChanges();
                 }
                Clear();
                g.ShowMessage(this.Page, "Document Details Saved Successfully");

            }
            else
            {
                string itemdelet3 = "delete from EmployeeDocumentMasterTB where EmployeeId='" + Convert.ToInt32(ddlEmployee.SelectedValue) + "'";
                DataSet ds3 = g.ReturnData1(itemdelet3);
                if (ViewState["DTDocument"] != null)
                {
                    DTDocument = (DataTable)ViewState["DTDocument"];
                    for (int i = 0; i < DTDocument.Rows.Count; i++)
                    {
                        EmployeeDocumentMasterTB t = new EmployeeDocumentMasterTB();
                        t.CompanyId = Convert.ToInt32(DTDocument.Rows[i]["CompanyID"].ToString());
                        t.DeptId = Convert.ToInt32(DTDocument.Rows[i]["DepartmentID"].ToString());
                        t.EmployeeId = Convert.ToInt32(DTDocument.Rows[i]["EmployeeID"].ToString());
                        t.DocumentId = Convert.ToInt32(DTDocument.Rows[i]["DocumentID"].ToString());
                        t.DocumentName = DTDocument.Rows[i]["DocumentName"].ToString();
                        t.DocumentPath = DTDocument.Rows[i]["Documentpath"].ToString();
                        t.Description = DTDocument.Rows[i]["Description"].ToString();
                        t.Status = "0";
                        HR.EmployeeDocumentMasterTBs.InsertOnSubmit(t);
                        HR.SubmitChanges();
                      }
                    Clear();
                    g.ShowMessage(this.Page, "Document Details Updated Successfully");
                }
            }
            }
            else
            {
                g.ShowMessage(this.Page, "Add Document Details");
            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        if (ddlCompany.Items.Count > 0)
        {
              ddlCompany.SelectedIndex = 0;
        }
        ddldept.Items.Clear();
        ddlEmployee.Items.Clear();
        if (ddldocument.Items.Count > 0)
        {
            ddldocument.SelectedIndex = 0;
        }
        txtdescrip.Text = "";
        lblpath.Text = "";
        ViewState["DTDocument"] = null;
        DTDocument = null;
        GridViewUpload.DataSource = null;
        GridViewUpload.DataBind();
        ddlCompany.Enabled = true;
        ddldept.Enabled = true;
        ddlEmployee.Enabled = true;
        btnsubmit.Text = "Save";
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
    private void fillcompany()
    {
        var data = from dt in HR.CompanyInfoTBs
                   where dt.Status == 0
                   select dt;
       

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        
    }
    private void fillEmployee(int Dept)
    {
            var data = from dt in HR.EmployeeTBs
                   where dt.DeptId == Dept && dt.RelivingStatus == null
                   select new
                   {
                       dt.EmployeeId,
                       Name = dt.FName + ' ' + dt.MName + ' ' + dt.Lname
                   };
            ddlEmployee.DataSource = data;
            ddlEmployee.DataTextField = "Name";
            ddlEmployee.DataValueField = "EmployeeId";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "--Select--");

        
    }

    protected void BtnAddDocument_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = 0;
            //if (ddlEmployee.SelectedIndex==0 || ddlEmployee.SelectedIndex==-1)
            //{
            //    g.ShowMessage(this.Page, "Select Employee Name");
            //}
            //else
            //{
                if (lblpath.Text=="")
                {
                     g.ShowMessage(this.Page, "Attach Document");
                }
               else
                {
              if (ViewState["DTDocument"] != null)
                {
                    DTDocument = (DataTable)ViewState["DTDocument"];
                }
                else
                {
                    DataColumn CompanyID = DTDocument.Columns.Add("CompanyID");
                    DataColumn CompanyName = DTDocument.Columns.Add("CompanyName");

                    DataColumn DepartmentID = DTDocument.Columns.Add("DepartmentID");
                    DataColumn DepartmentName = DTDocument.Columns.Add("DepartmentName");

                    DataColumn EmployeeID = DTDocument.Columns.Add("EmployeeID");
                    DataColumn EmployeeName = DTDocument.Columns.Add("EmployeeName");

                    DataColumn DocumentID = DTDocument.Columns.Add("DocumentID");
                    DataColumn DocumentName = DTDocument.Columns.Add("DocumentName");

                    DataColumn Description = DTDocument.Columns.Add("Description");

                    DataColumn Documentpath = DTDocument.Columns.Add("Documentpath");
                }

                DataRow dr = DTDocument.NewRow();
                dr[0] = ddlCompany.SelectedValue;
                dr[1] = ddlCompany.SelectedItem.Text;
                dr[2] = ddldept.SelectedValue;
                dr[3] = ddldept.SelectedItem.Text;
                dr[4] = ddlEmployee.SelectedValue;
                dr[5] = ddlEmployee.SelectedItem.Text;

                dr[6] = ddldocument.SelectedValue;
                dr[7] = ddldocument.SelectedItem.Text;
                dr[8] = txtdescrip.Text;
                dr[9] = lblpath.Text;   


                if (DTDocument.Rows.Count > 0)
                {
                    for (int f = 0; f < DTDocument.Rows.Count; f++)
                    {

                        string u1 = DTDocument.Rows[f][2].ToString();
                        string u2 = DTDocument.Rows[f][4].ToString();
                        string u3 = DTDocument.Rows[f][6].ToString();

                        if (u1 == Convert.ToString(ddldept.SelectedValue) && u2 == Convert.ToString(ddlEmployee.SelectedValue) && u3 == Convert.ToString(ddldocument.SelectedValue))
                        {
                            cnt++;

                        }
                        else
                        {

                        }
                    }
                    if (cnt > 0)
                    {
                        lblpath.Text = "";
                        g.ShowMessage(this.Page, "This Details Already Exist");
                        // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Already Exist');", true);
                    }
                    else
                    {
                        DTDocument.Rows.Add(dr);
                        ClearTextBoxOFDocDetails();
                        BtnAddDocument.Text = "Add";
                    }
                }
                else
                {
                    DTDocument.Rows.Add(dr);
                    ClearTextBoxOFDocDetails();
                    BtnAddDocument.Text = "Add";
                }

                ViewState["DTDocument"] = DTDocument;

                GridViewUpload.DataSource = DTDocument;
                GridViewUpload.DataBind();
                GridViewUpload.Visible = true;
            }
         // }    

               

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void ClearTextBoxOFDocDetails()
    {
        txtdescrip.Text = "";
        lblpath.Text = "";
        fillDocument();
    }

    private void fillDocument()
    {
        try
        {
            var fillDocdata = from d in HR.DocumentTBs
                              where d.Status == 0
                              select new {d.Document_Id,d.Document_Name };
            ddldocument.DataSource = fillDocdata;
            ddldocument.DataTextField = "Document_Name";
            ddldocument.DataValueField = "Document_Id";
            ddldocument.DataBind();
            ddldocument.Items.Insert(0, "--Select--");

                           
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void imgExpedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DTDocument = (DataTable)ViewState["DTDocument"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;
        //foreach (DataRow d in DTDocument.Rows)
        //{
        //    if (d[0].ToString() == imgedit.CommandArgument)
        //    {
                ddlCompany.SelectedValue =DTDocument.Rows[row]["CompanyID"].ToString(); 
                ddldept.SelectedValue = DTDocument.Rows[row]["DepartmentID"].ToString();
                ddlEmployee.SelectedValue = DTDocument.Rows[row]["EmployeeID"].ToString();
                ddldocument.SelectedValue = DTDocument.Rows[row]["DocumentID"].ToString();
                txtdescrip.Text = DTDocument.Rows[row]["Description"].ToString();
                lblpath.Text = DTDocument.Rows[row]["Documentpath"].ToString();

                DTDocument.Rows[row].Delete();
                DTDocument.AcceptChanges();
                BtnAddDocument.Text = "Update";
               // break;
        //    }
        //}

        GridViewUpload.DataSource = DTDocument;
        GridViewUpload.DataBind();
        ViewState["DTDocument"] = DTDocument;
    }
    protected void imgExpdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        DTDocument = (DataTable)ViewState["DTDocument"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;
        //foreach (DataRow d in DTDocument.Rows)
        //{
        //    if (d[0].ToString() == imgdelete.CommandArgument)
        //    {
                DTDocument.Rows[row].Delete();
                DTDocument.AcceptChanges();
                //break;
        //    }
        //}

        GridViewUpload.DataSource = DTDocument;
        GridViewUpload.DataBind();
        ViewState["DTDocument"] = DTDocument;

    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    private void fillgrid()
    {
        try
        {
            if (ddlEmployee.SelectedIndex > 0)
            {

                DataTable fetchempdoc = g.ReturnData("Select ed.CompanyId AS CompanyID, c.CompanyName, ed.DeptId As DepartmentID, d.DeptName AS DepartmentName, ed.EmployeeId AS EmployeeID, emp.FName +' '+ emp.MName +' '+ emp.LName As EmployeeName, ed.DocumentId AS DocumentID, dc.Document_Name AS DocumentName, ed.Description, ed.DocumentPath AS Documentpath  from EmployeeDocumentMasterTB ed left outer join MasterDeptTB d on ed.DeptId=d.DeptID Left outer join EmployeeTB emp on ed.EmployeeId=emp.EmployeeId Left outer join DocumentTB dc on ed.DocumentId=dc.Document_Id Left outer join CompanyInfoTB c on ed.CompanyId=c.CompanyId Where ed.EmployeeId='" + Convert.ToInt32(ddlEmployee.SelectedValue) + "'  ");
            if (fetchempdoc.Rows.Count >0)
            {
                 GridViewUpload.DataSource = fetchempdoc;
                 GridViewUpload.DataBind();
                 ViewState["DTDocument"] = fetchempdoc;
                 ddlCompany.Enabled = false;
                 ddldept.Enabled = false;
                 ddlEmployee.Enabled = false;
                 btnsubmit.Text = "Update";
            }
            else
            {
                GridViewUpload.DataSource = null;
                GridViewUpload.DataBind();
                ViewState["DTDocument"] = null;
                ddlCompany.Enabled = true;
                ddldept.Enabled = true;
                ddlEmployee.Enabled = true;
                btnsubmit.Text = "Save";
            }

            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
}