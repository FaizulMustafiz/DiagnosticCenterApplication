<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TypeWiseReportPage.aspx.cs" Inherits="DiagnosticCenterApplication.UI.TypeWiseReportPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />

    <title>Type Wise Report</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
</head>
<body>
    <div class="page-header" style="text-align: center">
        <h2>Type Wise Report Page</h2>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-10" style="margin-left: 150px">
                <form id="typeWiseReportForm" runat="server" class="form-horizontal">
                    <div class="form-group">
                        <label for="fromDate" class="col-sm-2 control-label">From Date</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="fromDateTextBox" runat="server" name="fromDate" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="toDate" class="col-sm-2 control-label">To Date</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="toDateTextBox" runat="server" name="toDate" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Label ID="messageLable" runat="server" Text="" class="text-info"></asp:Label>
                        </div>
                        <br />
                        <br />
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="typeWiseSearchButton" runat="server" Text="Show" CssClass="btn btn-success" OnClick="typeWiseSearchButton_Click" />
                        </div>
                    </div>
                    <br />
                    <br />

                    <div class="form-group">
                        <asp:GridView ID="typeWiseGridView" runat="server" CssClass="table" HorizontalAlign="Center" AutoGenerateColumns="False" EmptyDataText="No Records For Input Date" OnRowDataBound="typeWiseGridView_RowDataBound" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Serial">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Test Type Name" DataField="TestTypeName" />
                                <asp:BoundField HeaderText="Total Test" DataField="TotalTest" />
                                <asp:BoundField HeaderText="Total Amount" DataField="TotalFee" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="typeWisePdfButton" runat="server" Text="Pdf" CssClass="btn btn-primary" OnClick="typeWisePdfButton_Click" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="indexButton" runat="server" Text="Go Back To Home Page" CssClass="btn btn-default" OnClick="indexButton_Click" />
                        </div>
                    </div>
                </form>

            </div>
        </div>
    </div>


    <footer class="panel-footer">
        <div style="text-align: center">
            <h4 class="text-info" style="text-align: center;">copyright@Leicester City</h4>
        </div>
    </footer>







    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/jquery-3.1.0.js"></script>
    <script src="../Scripts/jquery.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/jquery.validate.js"></script>
    <script src="../Scripts/bootstrap-datepicker.js"></script>


    <script>
        $(document).ready(function () {

            $('#fromDateTextBox').datepicker({
                autoclose: true,
                todayHighlight: true,
            });

            $('#toDateTextBox').datepicker({
                autoclose: true,
                todayHighlight: true,
            });

            $("[id*=fromDateTextBox]").datepicker({
                defaultDate: "-1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("[id*=fromDateTextBox]").datepicker("option", "minDate", selectedDate);
                }


            });

            $("[id*=toDateTextBox]").datepicker({
                //defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                setDate: new Date(),
                onClose: function (selectedDate) {
                    $("[id*=toDateTextBox]").datepicker("option", "maxDate", selectedDate);
                }
            });



            //$("[id*=showButton]").click(function () {

            //    var startDate = $("[id*=searchStartDateTextBox]").val();
            //    var endDate = $("[id*=searchEndDateTextBox]").val();

            //    var currentDate = moment().format("MM/DD/YYYY");

            //    if (startDate === "") {
            //        alert("Select start Date!");
            //        return false;
            //    }

            //    if (endDate === "") {
            //        alert("Select end Date!");
            //        return false;
            //    }


            //    if (startDate > currentDate) {
            //        alert("Search Date Cannot Go Beyond Current Date!");
            //        return false;
            //    }

            //    if (endDate > currentDate) {
            //        alert("Search Date Cannot Go Beyond Current Date!");
            //        return false;
            //    }

            //});


            $("[id*=fromDateTextBox]").datepicker({
                changeMonth: true,
                changeYear: true
            });

            $("[id*=toDateTextBox]").datepicker({
                changeMonth: true,
                changeYear: true
            });

            var rowCount = $('#typeWiseGridView tr').length;

            if (rowCount > 1) {
                rows = $("#typeWiseGridView").children("tbody").children("tr");
                values = rows.children("td:nth-child(4)");
                var sum = 0;
                values.each(function () {
                    sum += parseInt($(this).html());
                })
                $("[id*=totalTextBox]").text(sum);
            }


        });
    </script>






</body>
</html>
