<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="admin_dashboard.aspx.cs" Inherits="admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>

   <link href="admin_theme/dist/css/bootstrap_multiselect.css" rel="stylesheet" />
    <script src="admin_theme/dist/js/bootstrap_multiselect.js"></script>

    
    <script>
        function LoadChart() {

            //var cDate = $("[id*=ContentPlaceHolder1_txtDate]").value;
            //console.log(cDate);
            $.ajax({
                type: "POST",
                url: "admin_dashboard.aspx/getTrafficSourceData",
                //data: "{companyId: '" + $("[id*=ddlCountries]").val() + "'}",
                //data: "{companyId: '1'}",
                data: "{companyId: '" + $("[id*=ContentPlaceHolder1_ddlCompany]").val() + "', date:'" + $("[id*=ContentPlaceHolder1_txtDate]").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var aData = response.d;
                    var arr = [];
                    var labels = [];
                    var values = [];
                    $.each(aData, function (inx, val) {
                        var obj = {};
                        obj.color = val.color;
                        obj.value = val.value;
                        obj.label = val.label;
                        console.log(val.hightlight);
                        arr.push(obj);
                        labels.push(val.label);
                        values.push(val.value);
                    });
                    var ctx = $("#myChart").get(0).getContext("2d");

                    var myPieChart = new Chart(ctx).Pie(arr);

                    var data = {
                        labels: labels,
                        datasets: [{
                            label: "My First dataset",
                            fillColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850",
                                "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850",
                                "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850",
                                "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850",
                                "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850",
                                "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                            data: values
                        }]
                    };

                    var ctxbar = $("#myBarChart").get(0).getContext("2d");
                    var myBarChart = new Chart(ctxbar).Bar(data);

                },
                error: function (response) {
                    console.log(response);
                    alert('There was an error.');
                }
            });
        }
        const randomColor = () => {
            let color = '#';
            for (let i = 0; i < 6; i++) {
                const random = Math.random();
                const bit = (random * 16) | 0;
                color += (bit).toString(16);
            };
            return color;
        };
        function jqFunctions() {
            LoadChart();



            $('#ContentPlaceHolder1_ddlDevice').multiselect({

                includeSelectAllOption: true,
                enableFiltering: true,
                innerWidth: 2000,
                outerWidth: 2000,
                maxHeight: 2000,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
                dropRight: true,
                onDropdownShow: function (event) {
                    $(this).closest('select').css('width', '2000px')
                }
            });



            //$('#ContentPlaceHolder1_ddlCompany').multiselect({

            //    includeSelectAllOption: true,
            //    enableFiltering: true,
            //    innerWidth: 2000,
            //    outerWidth: 2000,
            //    maxHeight: 2000,
            //    filterPlaceholder: 'Search',
            //    enableCaseInsensitiveFiltering: true,
            //    //dropRight: true,
            //    onDropdownShow: function (event) {
            //        $(this).closest('select').css('width', '2000px')
            //    }
            //});


                  }

        $(document).ready(function () {
            jqFunctions();

           
        });


        function showPresents() {
            if ($("[id*=ContentPlaceHolder1_txtDate]").val() !== "") {
                var url = "dash_presents.aspx?cId=" + $("[id*=ContentPlaceHolder1_ddlCompany]").val() + "&sDate=" + $("[id*=ContentPlaceHolder1_txtDate]").val();
                console.log(url);
                window.location.href = url;
            }
            else {
                alert('Please select date first');
            }
        }
        function showAbsents() {
            if ($("[id*=ContentPlaceHolder1_txtDate]").val() !== "") {
                var url = "dash_absents.aspx?cId=" + $("[id*=ContentPlaceHolder1_ddlCompany]").val() + "&sDate=" + $("[id*=ContentPlaceHolder1_txtDate]").val();
                console.log(url);
                window.location.href = url;
            }
            else {
                alert('Please select date first');
            }
        }
        function showLeaves() {
            if ($("[id*=ContentPlaceHolder1_txtDate]").val() !== "") {
                var url = "dash_leave_report.aspx?cId=" + $("[id*=ContentPlaceHolder1_ddlCompany]").val() + "&sDate=" + $("[id*=ContentPlaceHolder1_txtDate]").val();
                console.log(url);
                window.location.href = url;
            }
            else {
                alert('Please select date first');
            }
        }
        function showMobilePunches() {
            if ($("[id*=ContentPlaceHolder1_txtDate]").val() !== "") {
                var url = "dash_mobile_attendance.aspx?cId=" + $("[id*=ContentPlaceHolder1_ddlCompany]").val() + "&sDate=" + $("[id*=ContentPlaceHolder1_txtDate]").val();
                console.log(url);
                window.location.href = url;
            }
            else {
                alert('Please select date first');
            }
        }
        function showDeviceStatus(stat) {
            alert(stat);
            if ($("[id*=ContentPlaceHolder1_ddlCompany]").val() !== "") {
                // here 1 is online device and 0 for offline device 
                var url = "dash_device_stats.aspx?cId=" + $("[id*=ContentPlaceHolder1_ddlCompany]").val() + "&stat=" + stat;
                console.log(url);
                window.location.href = url;
            }
            else {
                alert('Please select company first');
            }
        }
    </script>


   
    <style>
        .widget-user-header-custom{
            padding:10px !important;
            height:70px !important;
        }


      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-4">
            <div class="box">
                <div class="box-body">
                    <div class="form-group">
                        <label class="control-label">Company</label>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                        <%--<asp:ListBox ID="ddlCompany" runat="server" CssClass="form-control"  SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:ListBox>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4 hidden">
        </div>
        <div class="col-md-4">
            <div class="box">
                <div class="box-body">
                    <div class="form-group">
                        <label class="control-label">Device</label>
                        <%--<asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDevice_SelectedIndexChanged"></asp:DropDownList>--%>
                         <div class="">
                                        <asp:ListBox ID="ddlDevice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDevice_SelectedIndexChanged" ></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="ddlDevice" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="box">
                <div class="box-body">
                    <div class="form-group">
                        <label class="control-label">Date</label>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="txtDate" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="box hidden">
                <div class="box-header">
                    <h3 class="box-title">Branchwise</h3>
                    <div class="box-tools" style="width: 70%;">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12"></div>
    </div>
    <div class="row">
        <div class="col-lg-4 col-6">
            <!-- small box -->
            <div class="small-box bg-primary">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblTotalEmployees" runat="server"></asp:Label></h3>

                    <p>Total Employees</p>
                </div>
                <div class="icon">
                    <i class="ion ion-bag"></i>
                </div>
                <a href="employee_report.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-4 col-6">
            <!-- small box -->
            <div class="small-box bg-green">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblPresentCount" runat="server"></asp:Label></h3>

                    <p>Present Employees</p>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" onclick="showPresents();" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-4 col-6">
            <!-- small box -->
            <div class="small-box bg-red">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblAbsentCount" runat="server"></asp:Label></h3>

                    <p>Absent Employees</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-add"></i>
                </div>
                <a href="#" onclick="showAbsents();" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->

        <%-- <div class="col-lg-4 col-6">
            <!-- small box -->
            <div class="small-box bg-fuchsia-active">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblCurrentCount" runat="server"></asp:Label></h3>

                    <p>Current Count</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>--%>



        <div class="col-lg-4 col-6">
            <!-- small box -->
            <div class="small-box bg-fuchsia-active">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblBirthdayCount" runat="server"></asp:Label></h3>

                    <p>Birthday Count</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="birthday_list.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-4 col-6">
            <!-- small box -->
            <div class="small-box bg-orange">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblOnLeaveCount" runat="server"></asp:Label></h3>

                    <p>On Leave Employees</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-add"></i>
                </div>
                <a href="#" onclick="showLeaves();" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-4 col-6">
            <!-- small box -->
           
            <div class="small-box bg-purple" id="Mobilepunch" runat="server">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblMobileCount" runat="server"></asp:Label></h3>

                    <p>Mobile Punches</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="#" onclick="showMobilePunches();" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        
        <div class="col-lg-3 col-6 hidden">
            <!-- small box -->
            <div class="small-box bg-green-gradient">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblOnlineDevices" runat="server" Text="0"></asp:Label></h3>

                    <p>Online Devices</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="#" onclick="showDeviceStatus(1);" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-3 col-6 hidden">
            <!-- small box -->
            <div class="small-box bg-red-gradient">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblOfflineDevices" runat="server" Text="0"></asp:Label></h3>

                    <p>Offline Devices</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="#" onclick="showDeviceStatus(0);" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
    </div>
    <div class="row">
        <div class="col-lg-12"></div>
    </div>
    <div class="row">
        <div class="col-lg-4 hidden">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">Chart</h3>
                </div>
                <div class="box-body">
                    <canvas id="myChart"></canvas>
                </div>
            </div>
        </div>
        <div class="col-lg-3 hidden">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">Chart</h3>
                </div>
                <div class="box-body">
                    <canvas id="myBarChart"></canvas>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="box box-widget widget-user">
                <div class="widget-user-header widget-user-header-custom bg-teal-active">
                    <h3 class="widget-user-username">Employee</h3>
                    <h5 class="widget-user-desc">Face, Finger & Cards</h5>
                </div>
                <div class="box-footer bg-teal-gradient" style="padding:10px !important;">
                    <div class="row">
                        <div class="col-sm-4 border-right">
                            <div class="description-block">
                                <h5 class="description-header"><asp:Label ID="lblFaces" runat="server"></asp:Label></h5>
                                <span class="description-text">FACES</span>
                            </div>
                        </div>
                        <div class="col-sm-4 border-right">
                            <div class="description-block">
                                <h5 class="description-header"><asp:Label ID="lblFingers" runat="server"></asp:Label></h5>
                                <span class="description-text">FINGERS</span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="description-block">
                                <h5 class="description-header"><asp:Label ID="lblCards" runat="server"></asp:Label></h5>
                                <span class="description-text">CARDS</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="info-box bg-aqua">
                <span class="info-box-icon"><i class="fa fa-bookmark-o"></i></span>

                <div class="info-box-content">
                    <span class="info-box-text">Devices</span>
                    <span class="info-box-number">
                        <asp:Label ID="lblTotalDeviceCount" runat="server"></asp:Label></span>
                    <asp:Literal ID="litStat" runat="server"></asp:Literal>
                    <%--<div class="progress">
                        <div class="progress-bar" style="width: 70%"></div>
                    </div>
                    <span class="progress-description">70% online out of 
                    </span>--%>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
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
                            <div class="col-md-4">
                                <asp:LinkButton ID="lbtnDoorOpen" OnClick="lbtnDoorOpen_Click" runat="server" CssClass="btn btn-app bg-green-active"><i class="fa fa-2x fa-sign-out"></i> Open Door</asp:LinkButton>
                            </div>
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:LinkButton ID="lbtnDoorClose" OnClick="lbtnDoorClose_Click" runat="server" CssClass="btn btn-app bg-orange"><i class="fa fa-2x fa-sign-in"></i>Close Door</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

