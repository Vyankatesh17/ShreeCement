using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExpenceMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext BH = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {



        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindExpence();
            }
        }

        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindExpence()
    {
        var dataexpence = (from dt in BH.MasterExpenceTBs
                          select new
                          {
                              dt.ExpenseId,
                              dt.ExpenseType,
                              Status = dt.Status == 0 ? "Active" : "In Active" 
                          }).OrderByDescending(d=>d.ExpenseId);
        if (dataexpence.Count() > 0)
        {
            GridView1.DataSource = dataexpence;
            GridView1.DataBind();
            lblcnt.Text = dataexpence.Count().ToString();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblcnt.Text = dataexpence.Count().ToString();

        }
    }
    protected void imgview_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ImgView = (ImageButton)sender;
        lbldesid1.Text = ImgView.CommandArgument;
        MasterExpenceTB MET = BH.MasterExpenceTBs.Where(d => d.ExpenseId == Convert.ToInt32(lbldesid1.Text)).First();
        txtchargename.Text = MET.ExpenseType;
        txtAccountType.Text = MET.AccountNo;
        rd_status.SelectedIndex = Convert.ToInt32(MET.Status);
        btnsubmit.Text = "Update";
        MultiView1.ActiveViewIndex = 1;

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {

            var dataExc = from p in BH.MasterExpenceTBs.Where(d => d.ExpenseType == txtchargename.Text) select p;
            if (dataExc.Count() > 0)
            {
                g.ShowMessage(this.Page, "Expence Details Already Exist.");
                txtchargename.Focus();
            }
            else
            {
                MasterExpenceTB EX = new MasterExpenceTB();
                EX.ExpenseType = txtchargename.Text;
                EX.AccountNo = txtAccountType.Text;
                EX.Status = rd_status.SelectedIndex;
                BH.MasterExpenceTBs.InsertOnSubmit(EX);
                BH.SubmitChanges();
            }



            //SaveUPdate(EX);


            g.ShowMessage(this.Page, "Expence Details Saved successfully");
        }
        else
        {
            MasterExpenceTB EX = BH.MasterExpenceTBs.Where(d => d.ExpenseId == Convert.ToInt32(lbldesid1.Text)).First();

            EX.ExpenseType = txtchargename.Text;
            EX.AccountNo = txtAccountType.Text;
            EX.Status = rd_status.SelectedIndex;
            BH.SubmitChanges();
            //BindExpence();
            g.ShowMessage(this.Page, "Expence Details Updated successfully");
        }
        Clear();
        BindExpence();
    }

    //private void SaveUPdate(MasterExpenceTB EX)
    //{

    //}

    private void Clear()
    {
        txtchargename.Text = null;
        txtAccountType.Text = null;
        rd_status.SelectedIndex = 0;
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void addnew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindExpence();
    }
}