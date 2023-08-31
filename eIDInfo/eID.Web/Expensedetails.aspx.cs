using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class AddExpense : System.Web.UI.Page
{
    HrPortalDtaClassDataContext BH = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();

    string path = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                txtdate.Text = g.GetCurrentDateTime().ToShortDateString(); 
                MultiView1.ActiveViewIndex = 0;
                bindExpencetype();
            
                Bindalldata();

               
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

   

    private void bindExpencetype()
    {
        var dataexpen = from dt in BH.MasterExpenceTBs
                        orderby dt.ExpenseType ascending
                        select new { dt.ExpenseId,dt.ExpenseType};
        ddexptype.DataTextField = "ExpenseType";
        ddexptype.DataValueField = "ExpenseId";
        ddexptype.DataSource = dataexpen;
        ddexptype.DataBind();
        ddexptype.Items.Insert(0, "--Select--");
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
        if (btnsubmit.Text == "Save")
        {

            string FolderPath1 = Server.MapPath("~/ExpenseDoc");
            MakeDirectoryIfExist(FolderPath1);
            if (FileUploadDocu.HasFile)
            {
                string filename1 = Path.GetFileName(FileUploadDocu.PostedFile.FileName);
                //string ext1 = filename1.Split('.')[0];

                if (lblpath.Text == "NoPhotoAvailable")
                {
                    // FileUpload2.SaveAs(Server.MapPath("~/CompanyLogo/" + ext + ".jpg"));
                    lblpath.Text = "";
                }
                else
                {
                    lblpath.Text = filename1;
                    FileUploadDocu.SaveAs(Server.MapPath("~/ExpenseDoc/" + lblpath.Text + ""));
                }

            }

            EmployeeExpenseTB EDB = new EmployeeExpenseTB();
         
            SaveUPdate(EDB);

            EmployeeExpenseApproveTB EAD = new EmployeeExpenseApproveTB();
            EAD.ExpenseID = EDB.ExpensedetailID;
            EAD.EmployeeId = Convert.ToInt32(Session["UserId"]);
            EAD.Amount = Convert.ToDecimal(txtamount.Text);
            EAD.Date = Convert.ToDateTime(txtdate.Text);
            EAD.DueDate = Convert.ToDateTime(txtdate.Text);
            EAD.Remarks = txtremark.Text;
            EAD.UserType = "Employee";
            EAD.Status = "Approved";
            BH.EmployeeExpenseApproveTBs.InsertOnSubmit(EAD);
            BH.SubmitChanges();


            Bindalldata();

            g.ShowMessage(this.Page, "Add Expense Details Saved successfully");
        }
       
        Clear();

    }

    
         
    private void SaveUPdate(EmployeeExpenseTB EDB)
    {


        EDB.ExpenseDate = Convert.ToDateTime(txtdate.Text);
        EDB.ExpenseTypeID=Convert.ToInt32(ddexptype.SelectedValue.ToString());
        EDB.Amount=Convert.ToDecimal(txtamount.Text);
        EDB.EmployeeID = Convert.ToInt32(Session["UserId"]);
        EDB.Remarks=txtremark.Text;
        EDB.UserID=Convert.ToInt32(Session["UserId"]);
        EDB.EmployeeStatus = "Approved";
        EDB.DepartmentHeadStatus = "Pending";
        EDB.ManagerStatus = "Pending";
        EDB.HRStatus = "Pending";
        EDB.AttachmentPath = lblpath.Text;

        if (btnsubmit.Text == "Save")
        {
            BH.EmployeeExpenseTBs.InsertOnSubmit(EDB);
        }

     
        BH.SubmitChanges();
       // Clear();
       // g.ShowMessage(this.Page, "Expense Details Saved Successfully");
    
    }

   
    private void Bindalldata()
    {
        DataSet dsalldata = g.ReturnData1("select FName + ' '+Lname 'Name',MA.ExpenseType,EA.ExpenseDate,EA.Amount,EA .Remarks,EA.ManagerStatus,EA.DepartmentHeadStatus,EA.HRStatus from EmployeeExpenseTB EA left outer join MasterExpenceTB MA on MA.ExpenseId=EA.ExpenseTypeID left outer join EmployeeTB E on EA.EmployeeId=E.EmployeeId order by EA.ExpenseDate desc ");
     
        GridView1.DataSource = dsalldata.Tables[0];
        GridView1.DataBind();
        lblcnt.Text = dsalldata.Tables[0].Rows.Count.ToString();

      
      
    }
   
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        txtamount.Text = null;
        txtdate.Text = null;
        txtremark.Text = null;
        ddexptype.SelectedIndex = 0;
     

        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }
    protected void addnew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Bindalldata();
      
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        string filePath = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();

        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + filePath);
        Response.TransmitFile(Server.MapPath("~/ExpenseImages/" + filePath));
        Response.End();
    }
 

    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        return ms.ToArray();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{
        //    Image img = (Image)GridView1.Rows[i].FindControl("pic166");
        //    img.ImageUrl = "~\\ExpenseImages\\" + GridView1.Rows[i].Cells[9].Text;// +grd_scandoc.Rows[i].Cells[2].Text + ".jpg";

        //}
    }
}