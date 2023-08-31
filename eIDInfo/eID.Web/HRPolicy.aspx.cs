using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
/// <summary>
/// Amit shinde 10 dec 2014
/// </summary>
public partial class HRPolicy : System.Web.UI.Page
{

    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable DTInfo = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserId"] != null)
        {
          
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                // MultiView1.ActiveViewIndex = 1;
                fillcompany();
                txtpolicyDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtpolicyDate.Text = g.GetCurrentDateTime().ToShortDateString();
             
                BindAllPolicy();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    #region Bind Methods
    private void fillcompany()
    {
        try
        {
            var data = (from dt in HR.CompanyInfoTBs
                        where dt.Status == 0
                        select dt).OrderBy(dt => dt.CompanyName);
            if (data != null && data.Count() > 0)
            {

                ddlCompany.DataSource = data;
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "CompanyId";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





    #endregion
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ddlCompany.SelectedIndex = 0;
        txtpolicyDate.Text = g.GetCurrentDateTime().ToShortDateString();
        btnsubmit.Text = "Save";
        btnsubmit.Enabled = true;
        ViewState["dt1"] = null;
        grdnewdoc.DataSource = null;
        grdnewdoc.DataBind();
        ClearTextBox();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {

            checkcompany();
        }
    }

    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }


    public void BindAllPolicy()
    {
        
        //var ExpenseData = from s in HR.ExpenseTBs 
        //                  join d in HR.MasterExpenceTBs
        //                  select new { s.ExpenseId,d.ExpenseType s.ExpenseDate,s.ExpenseAmt,s.Narration, Status = s.Status == 0 ? "Active" : "In Active" };


        if (Session["UserType"].ToString() == "Admin")
        {
            DataSet dsemp = g.ReturnData1("select hr.DocID,MC.Companyname,hr.PolicyName,hr.PolicyNumber,CONVERT(varchar, HR.Date, 101) AS Date,hr.DocName,HR.DocPath from HRPolicyDocumentTB hr left outer join CompanyInfoTB MC ON MC.CompanyId= hr.companyid ");
            if (dsemp.Tables[0].Rows.Count > 0)
            {
                grd_Expense.DataSource = dsemp.Tables[0];
                grd_Expense.DataBind();
                lblcnt.Text = dsemp.Tables[0].Rows.Count.ToString();
            }
            else
            {
                grd_Expense.DataSource = null;
                grd_Expense.DataBind();
                lblcnt.Text = "0";
            }
        }

        else
        {
            btnadd.Visible = false;
            DataSet dsemp = g.ReturnData1("select hr.DocID,MC.Companyname,hr.PolicyName,hr.PolicyNumber,CONVERT(varchar, HR.Date, 101) AS Date,hr.DocName,HR.DocPath from HRPolicyDocumentTB hr left outer join CompanyInfoTB MC ON MC.CompanyId= hr.companyid left outer join EmployeeTB EM on EM.Companyid= MC.CompanyId where EM.EmployeeId='"+ Session["UserID"].ToString()+"'");
            if (dsemp.Tables[0].Rows.Count > 0)
            {
                grd_Expense.DataSource = dsemp.Tables[0];
                grd_Expense.DataBind();
                lblcnt.Text = dsemp.Tables[0].Rows.Count.ToString();
            }
            else
            {
                grd_Expense.DataSource = null;
                grd_Expense.DataBind();
                lblcnt.Text = "0";
            }
        }


    }


    private void checkcompany()
    {
        try
        {
            DataTable dt = g.ReturnData("select HR.DocID,HR.CompanyID,MC.CompanyName,HR.PolicyName,HR.PolicyNumber,HR.DocName,HR.DocPath,convert(varchar,HR.Date,101) as Date from HRPolicyDocumentTB HR left outer join CompanyInfoTB MC  on MC.CompanyId=HR.CompanyID  where MC.CompanyID='" + Convert.ToInt32(ddlCompany.SelectedValue) + "' order by Date desc");
            if (dt.Rows.Count > 0)
            {
                grdnewdoc.DataSource = dt;
                grdnewdoc.DataBind();
                ViewState["dt1"] = dt;
                btnsubmit.Text = "Update";
                ddlCompany.Enabled = false;
            }
            else
            {
                grdnewdoc.DataSource = null;
                grdnewdoc.DataBind();
                ViewState["dt1"] = null;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ImgCitAdd_Click(object sender, ImageClickEventArgs e)
    {

        if (!string.IsNullOrEmpty(ddlCompany.Text.Trim()))
        {
            string path = "";
            int cnt1 = 0;
            string dir = Server.MapPath("~/HRPolicyDocument/");
            string[] files;
            int numFiles;
            files = Directory.GetFiles(dir);
            numFiles = files.Length;
            if (FileUploadDocu.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUploadDocu.PostedFile.FileName);
                    string[] filename1 = filename.Split('.');
                    for (int i = 0; i < numFiles; i++)
                    {
                        if (File.Exists(Server.MapPath("~/HRPolicyDocument/" + filename1[0] + cnt1 + "." + filename1[1])))
                        {
                            cnt1++;
                        }
                    }
                    string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "txt" };

                    FileUploadDocu.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\HRPolicyDocument\\" + filename1[0] + cnt1 + "." + filename1[1]);
                    path = filename1[0] + cnt1 + "." + filename1[1];

                    lblpath.Text = path;
                }
                catch (Exception ex)
                {

                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','" + ex.Message + "');", true);
                }

            }
            else
            {
                if (lblpath.Text == "")
                {
                    // g.ShowMessage(this.Page, "Please Upload File");
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Please Upload Documents');", true);
                }
            }
            if (lblpath.Text == "")
            {
                // g.ShowMessage(this.Page, "Please Upload Documents");
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Please Upload Documents');", true);


            }
            else
            {
                int cnt = 0;
                if (ViewState["dt1"] != null)
                {
                    DTInfo = (DataTable)ViewState["dt1"];
                }
                else
                {
                    DataColumn Companyid = DTInfo.Columns.Add("CompanyID");
                    DataColumn CompanyName = DTInfo.Columns.Add("CompanyName");
                    DataColumn Date = DTInfo.Columns.Add("Date");
                    DataColumn PolicyName = DTInfo.Columns.Add("PolicyName");
                    DataColumn PolicyNumber = DTInfo.Columns.Add("PolicyNumber");
                    DataColumn DocumentName = DTInfo.Columns.Add("DocName");
                    DataColumn Path1 = DTInfo.Columns.Add("DocPath");


                }
                DataRow dr = DTInfo.NewRow();
                dr[0] = ddlCompany.SelectedValue;
                dr[1] = ddlCompany.Text;
                dr[2] = txtpolicyDate.Text;
                dr[3] = txtpolicyname.Text;
                dr[4] = txtpolicyNumber.Text;
                dr[5] = txtdocname.Text;
                dr[6] = lblpath.Text;

                if (DTInfo.Rows.Count > 0)
                {
                    for (int f = 0; f < DTInfo.Rows.Count; f++)
                    {

                        string Comp = DTInfo.Rows[f][0].ToString();

                        string policy = DTInfo.Rows[f][3].ToString();
                        string num = DTInfo.Rows[f][4].ToString();

                        if (Comp == Convert.ToString(ddlCompany.SelectedValue) && policy == txtpolicyname.Text && num == txtpolicyNumber.Text)
                        {
                            cnt++;

                        }
                        else
                        {

                        }
                    }
                    if (cnt > 0)
                    {
                        g.ShowMessage(this.Page, "Document Already Exist For Policy Name and Policy Number");
                        // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Already Exist');", true);


                    }
                    else
                    {
                        DTInfo.Rows.Add(dr);
                        ClearTextBox();
                    }
                }
                else
                {
                    DTInfo.Rows.Add(dr);
                    ClearTextBox();
                }

                ViewState["dt1"] = DTInfo;

                grdnewdoc.DataSource = DTInfo;
                grdnewdoc.DataBind();

            }
        }
        else
        {
            g.ShowMessage(this.Page, "Please Enter Policy Details");
            ddlCompany.Focus();
        }
    }

    private void ClearTextBox()
    {
        // ddlCompany.SelectedIndex = 0;
        txtpolicyDate.Text = g.GetCurrentDateTime().ToShortDateString();
        txtpolicyname.Text = "";
        txtpolicyNumber.Text = "";
        txtdocname.Text = "";
        lblpath.Text = "";

        ViewState["dt1"] = null;


    }

    private void Clear()
    {
        ddlCompany.Enabled = true;
        ddlCompany.SelectedIndex = 0;
        txtpolicyDate.Text = g.GetCurrentDateTime().ToShortDateString();
        txtpolicyname.Text = "";
        txtpolicyNumber.Text = "";
        txtdocname.Text = "";
        lblpath.Text = "";

        ViewState["dt1"] = null;
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton EditDocu = (ImageButton)sender;

        string Id = EditDocu.CommandArgument;

        DTInfo = (DataTable)ViewState["dt1"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;

        ddlCompany.SelectedValue = DTInfo.Rows[row]["CompanyId"].ToString();
        //ddlCompany.Text = DTInfo.Rows[row]["CompanyName"].ToString();
        txtpolicyDate.Text = DTInfo.Rows[row]["Date"].ToString();
        txtpolicyname.Text = DTInfo.Rows[row]["PolicyName"].ToString();
        txtpolicyNumber.Text = DTInfo.Rows[row]["PolicyNumber"].ToString();
        txtdocname.Text = DTInfo.Rows[row]["DocName"].ToString();
        lblpath.Text = DTInfo.Rows[row]["DocPath"].ToString();
        DTInfo.Rows[row].Delete();
        DTInfo.AcceptChanges();


        grdnewdoc.DataSource = DTInfo;
        grdnewdoc.DataBind();

        if (DTInfo.Rows.Count > 0)
        {
            ViewState["dt1"] = DTInfo;
        }
        else
        {
            ViewState["dt1"] = null;
        }
    }
    protected void btndelet_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton DeleteDocu = (ImageButton)sender;
        string Id1 = DeleteDocu.CommandArgument;
        DTInfo = (DataTable)ViewState["dt1"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;

        DTInfo.Rows[row].Delete();
        DTInfo.AcceptChanges();


        grdnewdoc.DataSource = DTInfo;
        grdnewdoc.DataBind();

        if (DTInfo.Rows.Count > 0)
        {
            ViewState["dt1"] = DTInfo;
        }
        else
        {
            ViewState["dt1"] = null;
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["dt1"] != null)
        {

            if (btnsubmit.Text == "Save")
            {
                DTInfo = ViewState["dt1"] as DataTable;

                if (DTInfo.Rows[0][1].ToString() == "All")
                {
                    DataSet dscomp = g.ReturnData1("select CompanyId from CompanyInfoTB where status=0 ");
                    for (int i = 0; i < dscomp.Tables[0].Rows.Count; i++)
                    {
                        DataSet dscheck = g.ReturnData1("select * from HRPolicyDocumentTB where Companyid='" + dscomp.Tables[0].Rows[i]["CompanyId"].ToString() + "' and PolicyName='" + DTInfo.Rows[0][3].ToString() + "' and PolicyNumber='" + DTInfo.Rows[0][4].ToString() + "'");

                        if (dscheck.Tables[0].Rows.Count <= 0)
                        {
                            for (int j = 0; j < DTInfo.Rows.Count; j++)
                            {
                                HRPolicyDocumentTB BT = new HRPolicyDocumentTB();
                                BT.CompanyID = Convert.ToInt32(dscomp.Tables[0].Rows[i]["CompanyId"].ToString());
                                BT.Date = Convert.ToDateTime(DTInfo.Rows[j][2].ToString());
                                BT.PolicyName = DTInfo.Rows[j][3].ToString();
                                BT.PolicyNumber = DTInfo.Rows[j][4].ToString();
                                BT.DocName = DTInfo.Rows[j][5].ToString();
                                BT.DocPath = DTInfo.Rows[j][6].ToString();
                                HR.HRPolicyDocumentTBs.InsertOnSubmit(BT);
                                HR.SubmitChanges();
                            }

                        }
                        else
                        {
                            g.ShowMessage(this.Page, "Policy Details Already Exist for this Month !!!!");
                            break;
                        }

                        g.ShowMessage(this.Page, "Policy Details Saved Successfully!!!!");
                        Clear();
                        BindAllPolicy();
                    }

                }
                if (ddlCompany.SelectedIndex > 0)
                {
                    //DataSet dscomp = g.ReturnData1("select CompanyId from CompanyInfoTB where status=0 ");
                    //for (int i = 0; i < dscomp.Tables[0].Rows.Count; i++)
                    //{
                    DataSet dscheck = g.ReturnData1("select * from HRPolicyDocumentTB where Companyid='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'  and PolicyName='" + DTInfo.Rows[0][3].ToString() + "' and PolicyNumber='" + DTInfo.Rows[0][4].ToString() + "'");

                    if (dscheck.Tables[0].Rows.Count <= 0)
                    {
                        for (int j = 0; j < DTInfo.Rows.Count; j++)
                        {
                            HRPolicyDocumentTB BT = new HRPolicyDocumentTB();
                            BT.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                            BT.Date = Convert.ToDateTime(DTInfo.Rows[j][2].ToString());
                            BT.PolicyName = DTInfo.Rows[j][3].ToString();
                            BT.PolicyNumber = DTInfo.Rows[j][4].ToString();
                            BT.DocName = DTInfo.Rows[j][5].ToString();
                            BT.DocPath = DTInfo.Rows[j][6].ToString();
                            HR.HRPolicyDocumentTBs.InsertOnSubmit(BT);
                            HR.SubmitChanges();
                        }

                    }
                    else
                    {
                        g.ShowMessage(this.Page, "Policy Details Already Exist for this Month!!!!");

                    }

                    g.ShowMessage(this.Page, "Policy Details Saved Successfully!!!!");
                    Clear();
                    BindAllPolicy();
                }
            }
            else
            {

                if (ViewState["dt1"] != null)
                {
                    string itemdelet = "delete from HRPolicyDocumentTB where CompanyID='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'";
                    DataSet ds = g.ReturnData1(itemdelet);
                    DTInfo = ViewState["dt1"] as DataTable;
                    for (int i = 0; i < DTInfo.Rows.Count; i++)
                    {
                       
                            HRPolicyDocumentTB BT = new HRPolicyDocumentTB();
                            BT.CompanyID = Convert.ToInt32(DTInfo.Rows[i]["CompanyId"].ToString());
                            BT.Date = Convert.ToDateTime(DTInfo.Rows[i]["Date"].ToString());
                            BT.PolicyName = DTInfo.Rows[i]["PolicyName"].ToString();
                            BT.PolicyNumber = DTInfo.Rows[i]["PolicyNumber"].ToString();
                            BT.DocName = DTInfo.Rows[i]["DocName"].ToString();
                            BT.DocPath = DTInfo.Rows[i]["DocPath"].ToString();
                            HR.HRPolicyDocumentTBs.InsertOnSubmit(BT);
                            HR.SubmitChanges();
                        }
                    ViewState["dt1"] = null;
                    grd_Expense.DataSource = null;
                    grd_Expense.DataBind();
                    g.ShowMessage(this.Page, "Policy Details Updated Successfully!!!!");
                    Clear();
                    BindAllPolicy();
                }
                else
                {
                    g.ShowMessage(this.Page, "Please Add Policy Details");
                }

            }
        }
        else
        {
            g.ShowMessage(this.Page, "Please Add Policy Details");
        }

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["dt1"] = null;
        grdnewdoc.DataSource = null;
        grdnewdoc.DataBind();
    }

    protected void grd_Expense_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Expense.PageIndex = e.NewPageIndex;
        BindAllPolicy();
    }
   
    protected void Imagedownloadfile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            string filePath = grd_Expense.DataKeys[gvrow.RowIndex].Value.ToString();

            var dowlloaddata = from dt in HR.HRPolicyDocumentTBs
                               where dt.DocID == Convert.ToInt32(filePath)
                               select new { dt.DocPath };
            foreach (var item in dowlloaddata)
            {
                lbup.Text = item.DocPath;
            }
            if (lbup.Text != "")
            {

          //      Response.ContentType = "application/octet-stream";
          //    //   Response.ContentType = "application/ms-excel";
          //      string path = Server.MapPath("Attachments/" + lbup.Text).ToString();
          //      Response.AddHeader("Content-Disposition", "attachment;filename=" + path);
          //      Response.TransmitFile(Server.MapPath("~/Attachments/" + lbup.Text));
               
               
          //      //Response.TransmitFile(path);
               
          ////      Response.ClearContent();
          //////      Response.AddHeader("content-disposition", path);
               

          //      Response.End();
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + lbup.Text);
       
                Response.TransmitFile(Server.MapPath("HRPolicyDocument/" + lbup.Text));
               Response.End();

            }
            else
            {
                g.ShowMessage(this.Page, "There is no file to download");
            }

        }
        catch (Exception ex)
        {
            throw;
           // g.ShowMessage(this.Page, ex.Message);
        }
    }

}