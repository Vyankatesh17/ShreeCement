<%@ Page Title="Print QR Code" Language="C#" MasterPageFile="~/UserMaster1.master" AutoEventWireup="true" CodeFile="PrintQRCode.aspx.cs" Inherits="PrintQRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-lg-2">
    </div>
    <div class="col-lg-7">
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Print QR Code</h3>
                
            </div>
            <div class="box-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="control-label col-sm-8">QR Code Name : <%= Session["QRCodeName"]%></label>  
                        </div>
                        <div class="form-group">
                             <div class="col-sm-2"></div>
                        <div class="col-sm-10">
                            <img src="QRCODE/<%= Session["value"]%>.png" style="width:400px; height:400px"/>
                        </div>
                    </div>
                        <button id="btnPrint" class="hidden-print">Print</button>
                    
                </div>
            </div>
        </div>
    </div>


     <script>
         const $btnPrint = document.querySelector("#btnPrint");
         $btnPrint.addEventListener("click", () => {
             window.print();
         });
         </script>

</asp:Content>

