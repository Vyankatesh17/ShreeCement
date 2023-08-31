<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="employee_dashboard.aspx.cs" Inherits="employee_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfCompanyId" runat="server" />
    <div class="row">
        <div class="col-md-4">
            <div class="box">
                <div class="box-body">
                    <div class="form-group">
                        <label class="control-label">Device</label>
                        <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDevice_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12"></div>
    </div>
    <div class="row">
        <div class="col-lg-3 col-6">
            <div class="small-box bg-green">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblPresentCount" runat="server"></asp:Label></h3>
                    <p>Present</p>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-6">
            <div class="small-box bg-red">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblAbsentCount" runat="server"></asp:Label></h3>
                    <p>Absent</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-add"></i>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-6">
            <div class="small-box bg-fuchsia-active">
                <div class="inner">
                    <h3>
                        <a href="birthday_list.aspx" class="small-box-footer"> <asp:Label ID="lblBirthdayCount" runat="server"></asp:Label></a></h3>

                    <p>Birthday Count</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
            </div>
        </div>
        <div class="col-lg-3">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">Device Status</h3>
                    <div class="box-tools">
                        <asp:Label ID="lblDeviceStatus" runat="server" Visible="false" CssClass="btn btn-default btn-sm text-uppercase" Style="padding: 10px"></asp:Label>

                        <div id="stat" runat="server"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div id="divStat" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:LinkButton ID="lbtnDoorOpen" OnClick="lbtnDoorOpen_Click" runat="server" CssClass="btn btn-app bg-green-active"><i class="fa fa-2x fa-sign-out"></i> Open Door</asp:LinkButton>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3">
                                <asp:LinkButton ID="lbtnDoorClose" OnClick="lbtnDoorClose_Click" runat="server" CssClass="btn btn-app bg-orange"><i class="fa fa-2x fa-sign-in"></i>Close Door</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

