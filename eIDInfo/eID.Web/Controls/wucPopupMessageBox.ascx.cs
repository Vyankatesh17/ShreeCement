using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wucPopupMessageBox : System.Web.UI.UserControl
{
    public string Message { get; set; }



    protected void Page_Load(object sender, EventArgs e)
    {


    }
    public void ShowPopUp()
    {
        lblMsg.Text = Message;
        PopupMsg.Show();
    }
    public void btnOk_Click(object sender, EventArgs e)
    {
        PopupMsg.Hide();
    }
}