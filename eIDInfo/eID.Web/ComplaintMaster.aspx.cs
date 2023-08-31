using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ComplaintMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string path = "";
    DataTable DTDocument = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                txtdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                generateCompalainCode();
                bindgrid();
               
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindgrid()
    {
         bool Status = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));

         if (Status == true)
         {
             var griddata = from dt in HR.ComplaintTBs
                            select new { dt.ComplaintId, dt.ComplaintCode, dt.Date, dt.Title, dt.Description, dt.Status, HRRemark = dt.HRRemark == null ? "Pending" : dt.HRRemark };
             if (griddata.Count() > 0)
             {
                 grd_Complaint.DataSource = griddata;
                 grd_Complaint.DataBind();
                 lblcnt.Text = griddata.Count().ToString();

             }
             else
             {
                 grd_Complaint.DataSource = null;
                 grd_Complaint.DataBind();
                 lblcnt.Text = griddata.Count().ToString();
             }
         }
         else
         {
             var griddata = from dt in HR.ComplaintTBs
                            where dt.EmpId == Convert.ToInt32(Session["UserId"])
                            select new { dt.ComplaintId, dt.ComplaintCode, dt.Date, dt.Title, dt.Description, dt.Status, HRRemark = dt.HRRemark == null ? "Pending" : dt.HRRemark };
             if (griddata.Count() > 0)
             {
                 grd_Complaint.DataSource = griddata;
                 grd_Complaint.DataBind();
                 lblcnt.Text = griddata.Count().ToString();

             }
             else
             {
                 grd_Complaint.DataSource = null;
                 grd_Complaint.DataBind();
                 lblcnt.Text = griddata.Count().ToString();
             }
         }
    }

    private void generateCompalainCode()
    {
        try
        {
            var Id = (from dt in HR.ComplaintTBs
                      select dt.ComplaintId).Max();
            if (Id != null)
            {
                int ID1 = Convert.ToInt32(Id) + 1;
                lblcode.Text = "Comp-/" + ID1;
            }
            else
            {
                lblcode.Text = "Comp-/" + 1;
            }

        }
        catch
        {
            lblcode.Text = "Comp-/" + 1;
        }
    }
   
    protected void grd_Complaint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Complaint.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {

        ComplaintTB CT = new ComplaintTB();
        CT.ComplaintCode = lblcode.Text;
        CT.Date =Convert.ToDateTime(txtdate.Text);
        CT.Title = txttitle.Text;
        CT.Description = txtdescription.Text;
        CT.Status = "Open";
        CT.EmpId = Convert.ToInt32(Session["UserId"].ToString());
        HR.ComplaintTBs.InsertOnSubmit(CT);
        HR.SubmitChanges();
        if (ViewState["DTDocument"] != null)
            {
                DTDocument = (DataTable)ViewState["DTDocument"];
                for (int i = 0; i < DTDocument.Rows.Count; i++)
                {
                    ComplaintDocTB t = new ComplaintDocTB();
                    t.ComplaintId = CT.ComplaintId;
                    t.DocumentName = DTDocument.Rows[i]["DocumentName"].ToString();
                    t.DocumentPath = DTDocument.Rows[i]["Documentpath"].ToString();
                    HR.ComplaintDocTBs.InsertOnSubmit(t);
                    HR.SubmitChanges();
                 }
            }
        g.ShowMessage(this.Page, "Complaint Details Saved Successfully");
        Clear();
        bindgrid();
        }
        catch (Exception)
        {
            
            throw;
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        MultiView1.ActiveViewIndex = 0;
        txtdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        txttitle.Text = "";
        txtdescription.Text = "";
        txtdocname.Text = "";
        lblpath.Text = "";
        ViewState["DTDocument"] = null;

        GridViewUpload.DataSource = null;
        GridViewUpload.DataBind();
      
    }
    protected void txttitle_TextChanged(object sender, EventArgs e)
    {
        if (!txttitle.Text.Any(char.IsLetter))
        {
             g.ShowMessage(this.Page, "Please Enter Valid Title Name.");
             txttitle.Text = "";
        }
    }
    protected void BtnAddDocument_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = 0;
           
           

                string FolderPath = Server.MapPath("ComplaintDoc");
               
                if (FileUploadDocu.HasFile)
                {
                    int cnt1 = 0;
                    string dir = Server.MapPath("ComplaintDoc/");
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };
                    string filenamee = Path.GetFileName(FileUploadDocu.FileName);
                    string[] filename1 = filenamee.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("ComplaintDoc/" + filename1[0] + cnt1 + "." + filename1[1])))
                        {
                            cnt1++;
                        }
                    }
                    FileUploadDocu.SaveAs(Request.PhysicalApplicationPath + "ComplaintDoc/" + filename1[0] + cnt1 + "." + filename1[1]);
                    path = filename1[0] + cnt1 + "." + filename1[1];
                    lblpath.Text = path;
                }
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
                    DataColumn DocumentName = DTDocument.Columns.Add("DocumentName");
                    DataColumn Documentpath = DTDocument.Columns.Add("DocumentPath");
                }

                DataRow dr = DTDocument.NewRow();
                dr[0] =txtdocname.Text;
                dr[1] =lblpath.Text;
               

                if (DTDocument.Rows.Count > 0)
                {
                    for (int f = 0; f < DTDocument.Rows.Count; f++)
                    {

                        string u1 = DTDocument.Rows[f][0].ToString();
                       

                        if (u1 == txtdocname.Text)
                        {
                            cnt++;

                        }
                        else
                        {

                        }
                    }
                    if (cnt > 0)
                    {
                        g.ShowMessage(this.Page, "This Document Already Added");
                        // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Already Exist');", true);
                    }
                    else
                    {
                        DTDocument.Rows.Add(dr);
                        txtdocname.Text = "";
                        lblpath.Text = "";
                    }
                }
                else
                {
                    DTDocument.Rows.Add(dr);
                    txtdocname.Text = "";
                    lblpath.Text = "";
                }
               
                ViewState["DTDocument"] = DTDocument;

                GridViewUpload.DataSource = DTDocument;
                GridViewUpload.DataBind();
              
            }
         
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void imgDocedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DTDocument = (DataTable)ViewState["DTDocument"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;

        txtdocname.Text = DTDocument.Rows[row]["DocumentName"].ToString();
        lblpath.Text = DTDocument.Rows[row]["DocumentPath"].ToString();
        DTDocument.Rows[row].Delete();
        DTDocument.AcceptChanges();
       
        GridViewUpload.DataSource = DTDocument;
        GridViewUpload.DataBind();
        ViewState["DTDocument"] = DTDocument;
    }
    protected void imgDocdelete_Click(object sender, ImageClickEventArgs e)
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
}