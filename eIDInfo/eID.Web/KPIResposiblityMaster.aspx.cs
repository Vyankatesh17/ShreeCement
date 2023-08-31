using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class KPIResposiblityMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable dtt = new DataTable();
    DataTable DtExperience = new DataTable();
    string AttachPath;
    int empFlag;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserId"] != null)
            {

            }
        }
        else
        {

        }
       


        int Flag = Convert.ToInt32(Request.QueryString["Flag"]);
        if (Flag == 0)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        else
        {
            List<string> empid5 = (from dd in HR.tbl_KRA_KPITB1s select dd.EmpID.ToString()).ToList();

            String empidd = Convert.ToString(Session["UserId"]);
            if (empid5.Contains(empidd))
            {
                MultiView1.ActiveViewIndex = 0;
                BindAllEmp();
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
            }
        }
        dtt = new DataTable();
        loadKRANo();

        //dtt.Columns.Add(new DataColumn("KRPId", typeof(string)));
        dtt.Columns.Add(new DataColumn("Role", typeof(string)));
        dtt.Columns.Add(new DataColumn("Resposiblity", typeof(string)));
        dtt.Columns.Add(new DataColumn("KeyResultAreas", typeof(string)));
        dtt.Columns.Add(new DataColumn("KeyPerformance", typeof(string)));
        dtt.Columns.Add(new DataColumn("Target", typeof(string)));
          dtt.Columns.Add(new DataColumn("Remark", typeof(string)));

    }
    public void BindAllEmp()
    {
        //bool Status = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));

        //if (Status == true)
        //{
            //btnadd.Visible = false;
            var EmpData = (from d in HR.tbl_KRA_KPITB1s
                           where d.EmpID == Convert.ToInt32(Session["UserId"])
                           select new
                           {
                            d.Role,
                            d.Resposiblity,
                            d.KeyResultAreas,
                            d.KeyPerformance,
                            d.Target,
                            d.Remark
                           });
            if (EmpData.Count() > 0)
            {
                GridView1.DataSource = EmpData;
                GridView1.DataBind();

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();


            }
        //}
        //else
        //{
            #region 

            //t1.Visible = false;
            //btnadd.Visible = false;
            //var EmpData = (from d in HR.EmployeeTBs
            //               join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID
            //               join us in HR.RegistrationTBs on d.EmployeeId equals us.EmployeeId
            //               where us.EmployeeId == Convert.ToInt32(Session["UserId"])
            //               select new
            //               {
            //                   Name = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
            //                   Name1 = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
            //                   d.FName,
            //                   d.Lname,
            //                   d.EmployeeId,
            //                   d.BirthDate,
            //                   d.Email,
            //                   DOJ1 = d.DOJ,
            //                   d.PanNo,
            //                   d.ContactNo,
            //                   d.PassportNo,
            //                   n.DeptName,
            //                   emnae = d.FName + " " + d.Lname
            //               });

            //if (txtFirstNameSearch.Text != "")
            //{
            //    EmpData = EmpData.Where(d => d.emnae.Contains(txtFirstNameSearch.Text));
            //}
            //if (txtempidsearch.Text != "")
            //{
            //    EmpData = EmpData.Where(d => d.EmployeeId.Equals(txtempidsearch.Text));
            //}

            //if (txtDOJsearch.Text != "")
            //{
            //    EmpData = EmpData.Where(d => d.DOJ1.Equals(txtDOJsearch.Text));
            //}
            //if (txtpansearch.Text != "")
            //{
            //    EmpData = EmpData.Where(d => d.PanNo.Equals(txtpansearch.Text));
            //}
            //if (txtdeptsearch.Text != "")
            //{
            //    EmpData = EmpData.Where(d => d.DeptName.Contains(txtdeptsearch.Text));
            //}

            ////DataTable dt = g.ReturnData(EmpData);
            //grd_Emp.DataSource = EmpData;
            //grd_Emp.DataBind();

            #endregion

        //}

    }



    private void loadKRANo()
    {
        DataSet dss1 = g.ReturnData1("select  ISNULL(MAX(KRPId)+1,1) as [KRPId] from tbl_KRA_KPITB1 ");
        lblexpenseid.Text = dss1.Tables[0].Rows[0][0].ToString();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["dtt"] == null)
        {
            g.ShowMessage(this.Page, "Please Enter Your all Details");
        }
        else
        {
        if (btnsubmit.Text == "Save")
        {
            dtt = ViewState["dtt"] as DataTable;
            if (ViewState["dtt"] != null)
            {

                for (int i = 0; i < dtt.Rows.Count; i++)
                {

                    tbl_KRA_KPITB1 tb = new tbl_KRA_KPITB1();
                    tb.Role = (dtt.Rows[i][0].ToString());
                    tb.Resposiblity = (dtt.Rows[i][1].ToString());
                    tb.KeyResultAreas = (dtt.Rows[i][2].ToString());
                    tb.KeyPerformance = (dtt.Rows[i][3].ToString());
                    tb.Target = (dtt.Rows[i][4].ToString());
                    tb.Remark = (dtt.Rows[i][5].ToString());

                    tb.EmpID = Convert.ToInt32(Session["UserId"]);
                    HR.tbl_KRA_KPITB1s.InsertOnSubmit(tb);
                    HR.SubmitChanges();


                }

            }
            else
            {

            }
        }
        else
        {
            string roll =   Session["Role"].ToString();
            tbl_KRA_KPITB1 tb = HR.tbl_KRA_KPITB1s.Where(d => d.Role == roll).First();
            tb.Role = txtRole.Text;
            tb.Resposiblity = txtResposibility.Text;
            tb.KeyResultAreas = txtkeyResultAreas.Text;
            tb.KeyPerformance = txtKeyPerformanceIndicator.Text;
            tb.Target = txtWorkTarget.Text;
            tb.Remark = txtRemark.Text;

            tb.EmpID = Convert.ToInt32(Session["UserId"]);
         
            HR.SubmitChanges();
            BtnADDrole.Visible = true;
          

        }

        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        //To Export all pages
        //        grd_role.AllowPaging = false;
        //       BindAllEmp();
        //        //grd_Emp.DataBind();
        //       grd_role.Columns[5].Visible = false;

        //       grd_role.Style.Add("text-decoration", "none");
        //       grd_role.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        //       grd_role.Style.Add("font-size", "8px");

        //       grd_role.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 0f, 10f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.Write(pdfDoc);
        //        Response.End();
        //    }
        //}
        if (Session["KRIPage"].ToString() == "1")
        {
            Response.Redirect("EmployeeProfile.aspx");
        }
        else if (Session["KRIPage"].ToString() == "2")
        {
           Response.Redirect("EmployeeDetails.aspx");
        }
       

        }
                
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (Session["KRIPage"].ToString() == "1")
        {
            Response.Redirect("EmployeeProfile.aspx");
        }
        else if (Session["KRIPage"].ToString() == "2")
        {
            Response.Redirect("EmployeeDetails.aspx");
        }
       
    }
    protected void grd_role_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void BtnADDrole_Click(object sender, EventArgs e)
    {
        if (ViewState["dtt"] != null)
        {
            dtt = (DataTable)ViewState["dtt"];

            DataRow dr = dtt.NewRow();
            //dr[0] = lblexpenseid.Text;
            dr[0] = txtRole.Text;
            dr[1] = txtResposibility.Text;
            dr[2] = txtkeyResultAreas.Text;
            dr[3] = txtKeyPerformanceIndicator.Text;
            dr[4] = txtWorkTarget.Text ;
            dr[5] = txtRemark.Text;
           

            dtt.Rows.Add(dr);
            ViewState["dtt"] = dtt;


        }
        else
        {
            dtt = new DataTable();

            //DataColumn KRPId = dtt.Columns.Add("KRPId");
            DataColumn Role = dtt.Columns.Add("Role");
            DataColumn Resposiblity = dtt.Columns.Add("Resposiblity");
            DataColumn KeyResultAreas = dtt.Columns.Add("KeyResultAreas");
            DataColumn KeyPerformance = dtt.Columns.Add("KeyPerformance");
            DataColumn Target = dtt.Columns.Add("Target");
            DataColumn Remark = dtt.Columns.Add("Remark");
        

            DataRow dr = dtt.NewRow();
            dr[0] = txtRole.Text;
            dr[1] = txtResposibility.Text;
            dr[2] = txtkeyResultAreas.Text;
            dr[3] = txtKeyPerformanceIndicator.Text;
            dr[4] = txtWorkTarget.Text;
            dr[5] = txtRemark.Text;
           
           

            dtt.Rows.Add(dr);
            ViewState["dtt"] = dtt;

        }
        loadKRANo();
        grd_role.DataSource = dtt;
        grd_role.DataBind();


        //btnsubmit.Visible = true;
        //btncancel.Visible = true;
        MultiView1.ActiveViewIndex = 1;

        CLEAR();
     
    }
    private void CLEAR()
    {
    
        txtRole.Text = "";
        txtRemark.Text = "";

        txtKeyPerformanceIndicator.Text = "";
        txtkeyResultAreas.Text = "";

        txtResposibility.Text = "";
        txtWorkTarget.Text = "";

    }

    protected void Unnamed1_Click(object sender, EventArgs e)
    {

    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        //btnsubmit.Visible = true;
        //btncancel.Visible = true;
        MultiView1.ActiveViewIndex = 1;

        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        dtt = (DataTable)ViewState["dtt"];


        foreach (DataRow d in dtt.Rows)
        {
            if (d[0].ToString() == imgedit.CommandArgument)
            {

                txtRole.Text = d["Role"].ToString();
                txtResposibility.Text = d["Resposiblity"].ToString();
                txtkeyResultAreas.Text = d["KeyResultAreas"].ToString();
                txtKeyPerformanceIndicator.Text = d["KeyPerformance"].ToString();
                txtWorkTarget.Text = d["Target"].ToString();
                txtRemark.Text = d["Remark"].ToString();
                d.Delete();
                dtt.AcceptChanges();

                break;
            }
        }

        grd_role.DataSource = dtt;
        grd_role.DataBind();
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        //btnsubmit.Visible = true;
        //btncancel.Visible = true;
        MultiView1.ActiveViewIndex = 1;
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        dtt = (DataTable)ViewState["dtt"];

        foreach (DataRow d in dtt.Rows)
        {
            if (d[0].ToString() == imgdelete.CommandArgument)
            {

                d.Delete();
                dtt.AcceptChanges();
                break;
            }
        }

        grd_role.DataSource = dtt;
        grd_role.DataBind();
        //cleareducation();
    }
    protected void imgedit1_Click(object sender, ImageClickEventArgs e)
    {
        //btnsubmit.Visible = true;
        //btncancel.Visible = true;
        MultiView1.ActiveViewIndex = 1;
        ImageButton imgedit = (ImageButton)sender;
        string  id = imgedit.CommandArgument;
        Session["Role"] = id;
      //  dtt = (DataTable)ViewState["dtt"];

        tbl_KRA_KPITB1 mt = HR.tbl_KRA_KPITB1s.Where(s => s.Role == id).First();
        txtRole.Text = mt.Role;


      txtResposibility.Text =  mt.Resposiblity;
        txtkeyResultAreas.Text = mt.KeyResultAreas;
        txtKeyPerformanceIndicator.Text = mt.KeyPerformance;
        txtWorkTarget.Text = mt.Target;
        txtRemark.Text  = mt.Remark;




        btnsubmit.Text = "Update";
        BtnADDrole.Visible = false;
        GridView1.DataSource = dtt;
        GridView1.DataBind();
    }
    protected void imgdelete1_Click(object sender, ImageClickEventArgs e)
    {
        //btnsubmit.Visible = true;
        //btncancel.Visible = true;
        MultiView1.ActiveViewIndex = 1;
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        dtt = (DataTable)ViewState["dtt"];

        foreach (DataRow d in dtt.Rows)
        {
            if (d[0].ToString() == imgdelete.CommandArgument)
            {

                d.Delete();
                dtt.AcceptChanges();
                break;
            }
        }

        GridView1.DataSource = dtt;
        GridView1.DataBind();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
}