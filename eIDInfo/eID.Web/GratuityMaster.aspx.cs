using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GratuityMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable DTDocument = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                DataTable dtdel = g.ReturnData("delete GratuityDummyTB");
                
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
        var gratuitydata = from dt in HR.GratuityTBs
                           select new { dt.GratuityId,dt.FromYears,dt.ToYears,dt.BasicDays};
        if (gratuitydata.Count()>0)
        {
            grd_Gratuity.DataSource = gratuitydata;
            grd_Gratuity.DataBind();
            lblcnt.Text = gratuitydata.Count().ToString();
        }
        else
        {
            grd_Gratuity.DataSource = null;
            grd_Gratuity.DataBind();
            lblcnt.Text = gratuitydata.Count().ToString();
        }
    }
   
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = 0;
            
                if (ViewState["DTDocument"] != null)
                {
                    DTDocument = (DataTable)ViewState["DTDocument"];
                }
                else
                {
                    DataColumn FromYears = DTDocument.Columns.Add("FromYears");
                    DataColumn ToYears = DTDocument.Columns.Add("ToYears");
                    DataColumn BasicDays = DTDocument.Columns.Add("BasicDays");
                }

                DataRow dr = DTDocument.NewRow();
                dr[0] = txtfromyear.Text;
                dr[1] = txtToyear.Text;
                dr[2] = txtbasicday.Text;

                decimal.Compare(Convert.ToDecimal(txtfromyear.Text), Convert.ToDecimal(txtToyear.Text));
                if (DTDocument.Rows.Count > 0)
                {
                    for (int f = 0; f < DTDocument.Rows.Count; f++)
                    {

                        string u1 = DTDocument.Rows[f][0].ToString();


                        DataTable dtcheck = g.ReturnData("select *from GratuityDummyTB where '" + Convert.ToDecimal(txtfromyear.Text) + "' between FromYears and ToYears  or '" + Convert.ToDecimal(txtToyear.Text) + "'  between FromYears and ToYears");
                        if (dtcheck.Rows.Count>0)
                        {
                            cnt++;
                            

                        }
                        else
                        {

                        }
                    }
                    if (cnt > 0)
                    {
                        g.ShowMessage(this.Page, "This Data Already Added");
                        
                    }
                    else
                    {
                        DTDocument.Rows.Add(dr);
                        GratuityDummyTB GT = new GratuityDummyTB();
                        GT.FromYears =Convert.ToDecimal(txtfromyear.Text);
                        GT.ToYears = Convert.ToDecimal(txtToyear.Text);
                        HR.GratuityDummyTBs.InsertOnSubmit(GT);
                        HR.SubmitChanges();
                        txtfromyear.Text = "";
                        txtToyear.Text = "";
                        txtbasicday.Text = "";

                    }
                }
                else
                {
                    DTDocument.Rows.Add(dr);
                    GratuityDummyTB GT = new GratuityDummyTB();
                    GT.FromYears = Convert.ToDecimal(txtfromyear.Text);
                    GT.ToYears = Convert.ToDecimal(txtToyear.Text);
                    HR.GratuityDummyTBs.InsertOnSubmit(GT);
                    HR.SubmitChanges();
                    txtfromyear.Text = "";
                    txtToyear.Text = "";
                    txtbasicday.Text = "";
                }

                ViewState["DTDocument"] = DTDocument;

                grdaddgratuity.DataSource = DTDocument;
                grdaddgratuity.DataBind();

            }

       
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void imgDocedit_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtdel = g.ReturnData("delete GratuityDummyTB");
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DTDocument = (DataTable)ViewState["DTDocument"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;

        txtfromyear.Text = DTDocument.Rows[row]["FromYears"].ToString();
        txtToyear.Text = DTDocument.Rows[row]["ToYears"].ToString();
        txtbasicday.Text = DTDocument.Rows[row]["BasicDays"].ToString();
        DTDocument.Rows[row].Delete();
        DTDocument.AcceptChanges();

        grdaddgratuity.DataSource = DTDocument;
        grdaddgratuity.DataBind();
        ViewState["DTDocument"] = DTDocument;
    }
    protected void imgDocdelete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtdel = g.ReturnData("delete GratuityDummyTB");
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        DTDocument = (DataTable)ViewState["DTDocument"];
        GridViewRow grow = (GridViewRow)(sender as Control).Parent.Parent;
        int row = grow.RowIndex;

        DTDocument.Rows[row].Delete();
        DTDocument.AcceptChanges();

        grdaddgratuity.DataSource = DTDocument;
        grdaddgratuity.DataBind();
        ViewState["DTDocument"] = DTDocument;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = 0;
             if (ViewState["DTDocument"] != null)
            {

                DTDocument = (DataTable)ViewState["DTDocument"];
                if (btnsubmit.Text == "Save")
                {
                for (int j = 0; j < DTDocument.Rows.Count; j++)
                {
                    DataTable dtcheck = g.ReturnData("select *from GratuityTB where '" + Convert.ToDecimal(DTDocument.Rows[j]["FromYears"].ToString()) + "' between FromYears and ToYears  or '" + Convert.ToDecimal(DTDocument.Rows[j]["ToYears"].ToString()) + "'  between FromYears and ToYears");
                    if (dtcheck.Rows.Count > 0)
                    {
                        cnt++;
                    }
                    
                 }
                if (cnt == 0)
                {

                    for (int i = 0; i < DTDocument.Rows.Count; i++)
                    {
                        GratuityTB t = new GratuityTB();
                        t.FromYears = Convert.ToDecimal(DTDocument.Rows[i]["FromYears"].ToString());
                        t.ToYears = Convert.ToDecimal(DTDocument.Rows[i]["ToYears"].ToString());
                        t.BasicDays = Convert.ToInt32(DTDocument.Rows[i]["BasicDays"].ToString());
                        HR.GratuityTBs.InsertOnSubmit(t);
                        HR.SubmitChanges();

                    }
                    g.ShowMessage(this.Page, "Gratuity Details Saved Successfully");
                     clear();
                    bindgrid();
                }
                    else
                {
                    g.ShowMessage(this.Page, "This Gratuity Details already added");
                }
                   
                }
                
               
                    else
                    {
                        for (int j = 0; j < DTDocument.Rows.Count; j++)
                        {
                            DataTable dtcheck = g.ReturnData("select *from GratuityTB where GratuityId<>'" + Convert.ToInt32(lblgratutyid.Text) + "' and ('" + Convert.ToDecimal(DTDocument.Rows[j]["FromYears"].ToString()) + "' between FromYears and ToYears  or '" + Convert.ToDecimal(DTDocument.Rows[j]["ToYears"].ToString()) + "'  between FromYears and ToYears)");
                            if (dtcheck.Rows.Count > 0)
                            {
                                cnt++;
                            }

                        }
                        if (cnt == 0)
                        {
                            DataTable dtgratuity = g.ReturnData("delete from GratuityTB where GratuityId='" + Convert.ToInt32(lblgratutyid.Text) + "'");
                            for (int i = 0; i < DTDocument.Rows.Count; i++)
                            {
                                GratuityTB t = new GratuityTB();
                                t.FromYears = Convert.ToDecimal(DTDocument.Rows[i]["FromYears"].ToString());
                                t.ToYears = Convert.ToDecimal(DTDocument.Rows[i]["ToYears"].ToString());
                                t.BasicDays = Convert.ToInt32(DTDocument.Rows[i]["BasicDays"].ToString());
                                HR.GratuityTBs.InsertOnSubmit(t);
                                HR.SubmitChanges();

                            }
                            g.ShowMessage(this.Page, "Gratuity Details Updated Successfully");
                            clear();
                            bindgrid();
                        }
                        else
                        {
                            g.ShowMessage(this.Page, "This Gratuity Details already added");
                        }
                      
                       
                    }
            }
             else
             {
                 g.ShowMessage(this.Page, "Please Add Gratuity Details");
             }
       
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        
    }

    private void clear()
    {
        DataTable dtdel = g.ReturnData("delete GratuityDummyTB");
        MultiView1.ActiveViewIndex = 0;
        txtfromyear.Text = "";
        txtToyear.Text = "";
        txtbasicday.Text = "";
        ViewState["DTDocument"] = null;

        grdaddgratuity.DataSource = null;
        grdaddgratuity.DataBind();
    }
    protected void grd_Gratuity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Gratuity.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        DataTable dtdel = g.ReturnData("delete GratuityDummyTB");
    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        lblgratutyid.Text = imgedit.CommandArgument;
        DataTable dtgratuity = g.ReturnData("select FromYears,ToYears,BasicDays from GratuityTB where GratuityId='"+Convert.ToInt32(lblgratutyid.Text)+"'");
        grdaddgratuity.DataSource = dtgratuity;
        grdaddgratuity.DataBind();
        GratuityDummyTB GT = new GratuityDummyTB();
        GT.FromYears = Convert.ToDecimal(dtgratuity.Rows[0]["FromYears"].ToString());
        GT.ToYears = Convert.ToDecimal(dtgratuity.Rows[0]["ToYears"].ToString());
        HR.GratuityDummyTBs.InsertOnSubmit(GT);
        HR.SubmitChanges();
        ViewState["DTDocument"] = dtgratuity;
        MultiView1.ActiveViewIndex = 1;
        btnsubmit.Text = "Update";

    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgdel = (ImageButton)sender;
            lblgratutyid.Text = imgdel.CommandArgument;
            DataTable dtgratuity = g.ReturnData("delete from GratuityTB where GratuityId='" + Convert.ToInt32(lblgratutyid.Text) + "'");
            g.ShowMessage(this.Page, "Gratuity Details Deleted Successfully");
            bindgrid();
        }
        catch (Exception)
        {

            g.ShowMessage(this.Page, "This Detail already in Use.");
        }
        
    }
}