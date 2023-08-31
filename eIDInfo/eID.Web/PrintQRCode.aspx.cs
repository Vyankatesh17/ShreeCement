using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintQRCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["QRCodeId"]))
                {                   
                    BindQRCodeData();
                }
            }
        }
    }

    private void BindQRCodeData()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                int qrId = Convert.ToInt32(Request.QueryString["QRCodeId"]);

                var qrdata = db.QRCodeTBs.Where(a => a.QR_Code_Id == qrId).FirstOrDefault();

                Session["QRCodeName"] = qrdata.Name;
                Session["value"] = qrdata.Photo;
            }
        }
        catch(Exception ex)
        {

        }
     }


}