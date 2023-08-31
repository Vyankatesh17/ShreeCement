using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DocumentForm : System.Web.UI.Page
{
    /// <summary>
    /// Document Master Form 
    /// Created by Abdul Rahman
    /// Created Date : 02/12/2014
    /// </summary> 
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {

                MultiView1.ActiveViewIndex = 0;
                binddocumentGrd();
            }
            
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var checkdocname = from p in HR.DocumentTBs.Where(d => d.Document_Name == txtdocumentname.Text) select p;
            if (checkdocname.Count() > 0)
            {
                g.ShowMessage(this.Page, "Document Name Already Exist");
                //modpop.ShowPopUp();

            }
            else
            {
            DocumentTB MTB = new DocumentTB();
            MTB.Document_Name = txtdocumentname.Text;
            MTB.Status =Convert.ToInt32(rd_status.SelectedValue);
            HR.DocumentTBs.InsertOnSubmit(MTB);
            HR.SubmitChanges();
            g.ShowMessage(this.Page, "Document Details Saved Successfully");
            binddocumentGrd();
            clear();
            }

            
        }
        else
        {
            var checkdocname = from p in HR.DocumentTBs.Where(d => d.Document_Name == txtdocumentname.Text && d.Document_Id==Convert.ToInt32(lbldocid.Text)) select p;
            if (checkdocname.Count() > 0)
            {
                updatecode();
              
            }
            else
            {
                var checkdocname1 = from p in HR.DocumentTBs.Where(d => d.Document_Name == txtdocumentname.Text && d.Document_Id != Convert.ToInt32(lbldocid.Text)) select p;
                if (checkdocname1.Count() > 0)
                {
                    g.ShowMessage(this.Page, "Document Details Already Exist");

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
            DocumentTB MTB = HR.DocumentTBs.Where(d => d.Document_Id == Convert.ToInt32(lbldocid.Text)).First();
             MTB.Document_Name = txtdocumentname.Text;
             MTB.Status = Convert.ToInt32(rd_status.SelectedValue);
             HR.SubmitChanges();
             g.ShowMessage(this.Page, "Document Details Updated Successfully");
             binddocumentGrd();
             clear();

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
            
        }
    }

    private void clear()
    {
        txtdocumentname.Text = "";
        rd_status.SelectedIndex = 0;
        MultiView1.ActiveViewIndex = 0;
        binddocumentGrd();
        btnsubmit.Text = "Save";

    }

    private void binddocumentGrd()
    {
        try
        {
            var fetchDocdata = (from d in HR.DocumentTBs
                               //where d.Status == 0
                               select new {d.Document_Id,d.Document_Name, Status= d.Status==0 ? "Active" : "In Active" }).OrderByDescending(d=>d.Document_Id);
            if (fetchDocdata.Count() > 0)
            {
                grddoc.DataSource = fetchDocdata;
                grddoc.DataBind();
                lblcnt.Text = fetchDocdata.Count().ToString();
            }
            else
            {
                grddoc.DataSource = null;
                grddoc.DataBind();
                lblcnt.Text = "0";
            }
        }
        catch (Exception ex)
        {
            
            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void lnkedit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        ImageButton Lnk = (ImageButton)sender;
        lbldocid.Text = Lnk.CommandArgument;
        MultiView1.ActiveViewIndex = 1;
        DocumentTB MT = HR.DocumentTBs.Where(d => d.Document_Id == Convert.ToInt32(lbldocid.Text)).First();
        txtdocumentname.Text = MT.Document_Name;
        rd_status.SelectedValue = MT.Status.ToString();
         btnsubmit.Text = "Update";
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
}