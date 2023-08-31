using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QRCodeMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

    Genreal g = new Genreal();
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;                
                fillcompany();
                BindAllQRCode();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void fillcompany()
    {
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());

        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }      
    }

    public void Clear()
    {
        txtqrcodename.Text = null;
        lblqrcodeid.Text = "";
        txtlatitude.Text = null;
        txtlongitude.Text = null;
        txtExpDate.Text = null;
        txtdistance.Text = null;
        BindAllQRCode();
        fillcompany();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void grd_QRCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_QRCode.PageIndex = e.NewPageIndex;
        BindAllQRCode();
        grd_QRCode.DataBind();
    }

    protected void grd_QRCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_QRCode.Rows.Count > 0)
        {
            grd_QRCode.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_QRCode.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var qrcodedata = from p in HR.QRCodeTBs.Where(d => d.Name == txtqrcodename.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)) select p;
            if (qrcodedata.Count() > 0)
            {
                g.ShowMessage(this.Page, "QR Code Already Exist");
            }
            else
            {                
                QRCodeTB qrcode = new QRCodeTB();
                qrcode.Name = txtqrcodename.Text;
                qrcode.Latitude = txtlatitude.Text;
                qrcode.Longitude = txtlongitude.Text;
                qrcode.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                qrcode.Exp_Date = Convert.ToDateTime(txtExpDate.Text);
                qrcode.DistanceInMeters = Convert.ToInt32(txtdistance.Text);
                qrcode.isActive = true;
                qrcode.Created_Date = DateTime.Now;             

                HR.QRCodeTBs.InsertOnSubmit(qrcode);
                HR.SubmitChanges();

                g.ShowMessage(this.Page, "QR Code Saved Successfully");

                //var qrdata = HR.QRCodeTBs.Where(a => a.Name == txtqrcodename.Text && a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).FirstOrDefault();
                //if (qrdata != null)
                //{                   
                    string Qrcodegenerate = qrcode.QR_Code_Id +"_"+ txtqrcodename.Text + "_" + txtlatitude.Text + "_" + txtlongitude.Text;
                    qrcode.Photo = Qrcodegenerate;
                        HR.SubmitChanges();
                GenerateQRCode(Qrcodegenerate);
                //}

                Clear();




            }
        }
        else
        {
            string Qrcodegenerate = lblqrcodeid.Text +"_"+ txtqrcodename.Text + "_" + txtlatitude.Text + "_" + txtlongitude.Text;

            var dte = from p in HR.QRCodeTBs.Where(d => d.Name == txtqrcodename.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.QR_Code_Id == Convert.ToInt32(lblqrcodeid.Text)) select p;
            if (dte.Count() > 0)
            {
                updatecode();
                GenerateQRCode(Qrcodegenerate);
            }
            else
            {
                var dte1 = from p in HR.QRCodeTBs.Where(d => d.Name == txtqrcodename.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.QR_Code_Id != Convert.ToInt32(lblqrcodeid.Text)) select p;
                if (dte1.Count() > 0)
                {

                    g.ShowMessage(this.Page, "QR Code Already Exist");
                    MultiView1.ActiveViewIndex = 1;

                }
                else
                {
                    updatecode();
                    GenerateQRCode(Qrcodegenerate);
                }
            }
        }
    }


    private void GenerateQRCode(string Qrcodegenerate)
    {
        try
        {
            //DateTime QRdt = DateTime.Now;
            //string QRtodaydate = QRdt.Year + "-" + QRdt.Month + "-" + QRdt.Day;
            //string QRdate = "2023-10-31";
            //if (Convert.ToDateTime(QRtodaydate) <= Convert.ToDateTime(QRdate))
            //{
                #region QR code 
                QRCodeGenerator ObjQr = new QRCodeGenerator();
                QRCodeData qrCodeData = ObjQr.CreateQrCode(Qrcodegenerate, QRCodeGenerator.ECCLevel.Q);
                System.Drawing.Bitmap bitMap = new QRCode(qrCodeData).GetGraphic(20);
                string path = Path.Combine(Server.MapPath("/QRCODE/"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string QRfilePath = Path.Combine(Server.MapPath("/QRCODE/"), Qrcodegenerate + ".png");
                bitMap.Save(QRfilePath);
                string fileName = Path.GetFileName(QRfilePath);
                #endregion
            //}
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }


    private void updatecode()
    {
        try
        {
            string Qrcodegenerate = lblqrcodeid.Text +"_"+ txtqrcodename.Text + "_" + txtlatitude.Text + "_" + txtlongitude.Text;

            QRCodeTB MT = HR.QRCodeTBs.Where(d => d.QR_Code_Id == Convert.ToInt32(lblqrcodeid.Text)).First();
            MT.Name = txtqrcodename.Text;
            MT.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            MT.isActive = true;
            MT.Latitude = txtlatitude.Text;
            MT.Longitude = txtlongitude.Text;
            MT.Exp_Date = Convert.ToDateTime(txtExpDate.Text);
            MT.DistanceInMeters = Convert.ToInt32(txtdistance.Text);
            MT.Photo = Qrcodegenerate;
            HR.SubmitChanges();
            g.ShowMessage(this.Page, "QR Code Updated Successfully");
            
            btnsubmit.Text = "Save";
            Clear();
                       
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string QRCodeId = Lnk.CommandArgument;
        lblqrcodeid.Text = QRCodeId;

        QRCodeTB MT = HR.QRCodeTBs.Where(d => d.QR_Code_Id == Convert.ToInt32(QRCodeId)).First();

        MT.isActive = false;
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "QR Code Delete Successfully.....");
        Response.Redirect("QRCodeMaster.aspx");
    }

    protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string QRCodeId = Lnk.CommandArgument;
        lblqrcodeid.Text = QRCodeId;
        MultiView1.ActiveViewIndex = 1;
        QRCodeTB MT = HR.QRCodeTBs.Where(d => d.QR_Code_Id == Convert.ToInt32(QRCodeId)).First();
        txtqrcodename.Text = MT.Name;
        txtlatitude.Text = MT.Latitude;
        txtlongitude.Text = MT.Longitude;
        txtExpDate.Text = MT.Exp_Date.ToString();
        txtdistance.Text = MT.DistanceInMeters.ToString();
        ddlCompany.SelectedValue = MT.CompanyId.ToString();
        btnsubmit.Text = "Update";
    }

    public void BindAllQRCode()
    {

        int companyid = Convert.ToInt32(Session["CompanyID"]);
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
            var deptDataLOcationAdmin = (from d in HR.QRCodeTBs
                                         join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                                         where c.TenantId == Session["TenantId"].ToString() && d.CompanyId == companyid && d.isActive == true
                                         select new
                                         {
                                             d.QR_Code_Id,
                                             d.Name,
                                             d.Latitude,
                                             d.Longitude,
                                             d.Exp_Date,
                                             d.DistanceInMeters,
                                             d.CompanyId,
                                             c.CompanyName,
                                             Status = d.isActive == true ? "Active" : "In Active"
                                         }).OrderBy(d => d.CompanyName).ThenBy(d => d.Name);
            if (deptDataLOcationAdmin.Count() > 0)
            {
                grd_QRCode.DataSource = deptDataLOcationAdmin;
                grd_QRCode.DataBind();
            }
        }
        else
        {
            var deptData = (from d in HR.QRCodeTBs
                            join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                            where c.TenantId == Session["TenantId"].ToString() && d.isActive == true
                            select new
                            {
                                d.QR_Code_Id,
                                d.Name,
                                d.Latitude,
                                d.Longitude,
                                d.Exp_Date,
                                d.DistanceInMeters,
                                d.CompanyId,
                                c.CompanyName,
                                Status = d.isActive == true ? "Active" : "In Active"
                            }).OrderBy(d => d.CompanyName).ThenBy(d => d.Name);
            if (deptData.Count() > 0)
            {
                grd_QRCode.DataSource = deptData;
                grd_QRCode.DataBind();
            }
        }
    }




    protected void View_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string QRCodeId = Lnk.CommandArgument;
        lblqrcodeid.Text = QRCodeId;

        Response.Redirect("PrintQRCode.aspx?QRCodeId="+ QRCodeId);
    }
}