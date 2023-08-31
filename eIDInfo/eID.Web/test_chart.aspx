<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="test_chart.aspx.cs" Inherits="test_chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="//cdn.jsdelivr.net/excanvas/r3/excanvas.js" type="text/javascript"></script>
<script src="//cdn.jsdelivr.net/chart.js/0.2/Chart.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        function jqFunctions() {
            LoadChart();
        }
        $(document).ready(function () {
            LoadChart();
        });
        function LoadChart() {


            $.ajax({
                type: "POST",
                url: "test_chart.aspx/getTrafficSourceData",
                //data: "{companyId: '" + $("[id*=ddlCountries]").val() + "'}",
                data: "{companyId: '1'}",
                // data: "{companyId: '" + $("[id*=ContentPlaceHolder1_ddlCompany]").val() + "'}",
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
    <canvas id="myChart" width="200" height="200"></canvas>

    <canvas id="myBarChart" width="500" height="300"></canvas>
</asp:Content>

