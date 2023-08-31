using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRManualAttchmentForm : System.Web.UI.Page
{
    /// <summary>
    /// HR Manual Attachment form
    /// Created By Abdul Rahman
    /// Created date :08/12/2014
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
                bindgrid();
                txtdate.Attributes.Add("readonly", "readonly");
                txtdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string FolderPath = Server.MapPath("HRManualAttachment");
                MakeDirectoryIfExist(FolderPath);
                if (FileUploadDocu.HasFile)
                {
                    int cnt = 0;
                    string dir = Server.MapPath("HRManualAttachment/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUploadDocu.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("HRManualAttachment/" + filename1[0] + cnt + "." + filename1[1])))
                        {
                            cnt++;
                        }
                    }
                    FileUploadDocu.SaveAs(Request.PhysicalApplicationPath + "HRManualAttachment/" + filename1[0] + cnt + "." + filename1[1]);
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

    private void bindgrid()
    {
        try
        {
            DataTable dt = g.ReturnData("Select Description AS Description, File_Path AS filepath, Revise_Date AS Revisedate from HRManulAttachmentTB");
            if (dt.Rows.Count > 0)
            {
                GridViewUpload.DataSource = dt;
                GridViewUpload.DataBind();
                ViewState["DTDocument"] = dt;
               

            }
            else
            {
                GridViewUpload.DataSource = null;
                GridViewUpload.DataBind();
                ViewState["DTDocument"] = null;
            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }


    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
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
            if (lblpath.Text == "")
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
                    DataColumn Documentpath = DTDocument.Columns.Add("filepath");
                   
                    DataColumn Description = DTDocument.Columns.Add("Description");

                    DataColumn Revisedate = DTDocument.Columns.Add("Revisedate");
                }

                DataRow dr = DTDocument.NewRow();
                dr[0] = lblpath.Text;
                dr[1] = txtdescrip.Text;
                dr[2] = txtdate.Text;


                if (DTDocument.Rows.Count > 0)
                {
                    for (int f = 0; f < DTDocument.Rows.Count; f++)
                    {

                        string u1 = DTDocument.Rows[f][1].ToString();
                        

                        if (u1 == txtdescrip.Text)
                        {
                            cnt++;

                        }
                        else
                        {

                        }
                    }
                    if (cnt > 0)
                    {
                        g.ShowMessage(this.Page, "This Description Already Exist");
                        // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Already Exist');", true);
                    }
                    else
                    {
                        DTDocument.Rows.Add(dr);
                        ClearTextBoxOFDocDetails();
                    }
                }
                else
                {
                    DTDocument.Rows.Add(dr);
                    ClearTextBoxOFDocDetails();
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
        txtdate.Text = "";
       
    }
    protected void imgExpedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DTDocument = (DataTable)ViewState["DTDocument"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;
       lblpath.Text = DTDocument.Rows[row]["filepath"].ToString();
        txtdescrip.Text = DTDocument.Rows[row]["Description"].ToString();
        txtdate.Text = DTDocument.Rows[row]["Revisedate"].ToString();
        DTDocument.Rows[row].Delete();
        DTDocument.AcceptChanges();
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
       
        DTDocument.Rows[row].Delete();
        DTDocument.AcceptChanges();
        GridViewUpload.DataSource = DTDocument;
        GridViewUpload.DataBind();
        ViewState["DTDocument"] = DTDocument;

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["DTDocument"] != null)
            {
                if (btnsubmit.Text == "Save")
                {
                    string itemdelet3 = "delete from HRManulAttachmentTB";
                    DataSet ds3 = g.ReturnData1(itemdelet3);
                    DTDocument = (DataTable)ViewState["DTDocument"];
                    for (int i = 0; i < DTDocument.Rows.Count; i++)
                    {
                        HRManulAttachmentTB t = new HRManulAttachmentTB();
                        t.File_Path = DTDocument.Rows[i]["filepath"].ToString();
                        t.Description = DTDocument.Rows[i]["Description"].ToString();
                        t.Revise_Date = Convert.ToDateTime(DTDocument.Rows[i]["Revisedate"].ToString());
                        t.status = 0;
                        t.User_Id = Convert.ToInt32(Session["UserId"]);
                        HR.HRManulAttachmentTBs.InsertOnSubmit(t);
                        HR.SubmitChanges();
                    }
                    Clear();
                    g.ShowMessage(this.Page, "Document Details Saved Successfully");

                }
                else
                {
                //    string itemdelet3 = "delete from HRManulAttachmentTB";
                //    DataSet ds3 = g.ReturnData1(itemdelet3);
                //    if (ViewState["DTDocument"] != null)
                //    {
                //        DTDocument = (DataTable)ViewState["DTDocument"];
                //        for (int i = 0; i < DTDocument.Rows.Count; i++)
                //        {
                //            HRManulAttachmentTB t = new HRManulAttachmentTB();
                //            t.File_Path = DTDocument.Rows[i]["filepath"].ToString();
                //            t.Description = DTDocument.Rows[i]["Description"].ToString();
                //            t.Revise_Date = Convert.ToDateTime(DTDocument.Rows[i]["Revisedate"].ToString());
                //            t.status = 0;
                //            t.User_Id = Convert.ToInt32(Session["UserId"]);
                //            HR.HRManulAttachmentTBs.InsertOnSubmit(t);
                //            HR.SubmitChanges();
                //        }
                //        Clear();
                //        g.ShowMessage(this.Page, "Document Details Updated Successfully");
                //    }
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
        
        txtdescrip.Text = "";
        lblpath.Text = "";
        txtdate.Text = "";
        ViewState["DTDocument"] = null;
        DTDocument = null;
        GridViewUpload.DataSource = null;
        GridViewUpload.DataBind();
        btnsubmit.Text = "Save";
        bindgrid();
    }

}