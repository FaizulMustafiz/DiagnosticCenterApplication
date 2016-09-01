<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DiagnosticCenterApplication.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />

    <title>Diagonostic Center Bill Management System</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
</head>
<body>
    <div class="page-header" style="text-align: center">
        <h2>Diagonostic Center Bill Management System</h2>
        <p>This is a web Application for a small Diagonostic center</p>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="ccol-md-6 col-md-offset-4">
                <form id="form1" runat="server" class="form-horizontal" style="align-content: center">
                    <div align="center" class="form-group">
                        <div class="col-sm-6" align="center">
                            <asp:Button ID="testTypeSetupPageButton" runat="server" Text="Test Type Setup" CssClass="btn btn-primary btn-lg btn-block" OnClick="testTypeSetupPageButton_Click" />
                        </div>
                    </div>
                    <div align="center" class="form-group">
                        <div align="center" class="col-sm-6">
                            <asp:Button ID="testSetupPageButton" runat="server" Text="Test Setup" CssClass="btn btn-primary btn-lg btn-block" OnClick="testSetupPageButton_Click" />
                        </div>
                    </div>
                    <div align="center" class="form-group">
                        <div align="center" class="col-sm-6">
                            <asp:Button ID="testRequestEntryPageButton" runat="server" Text="Test Request" CssClass="btn btn-primary btn-lg btn-block" OnClick="testRequestEntryPageButton_Click" />
                        </div>
                    </div>
                    <div align="center" class="form-group">
                        <div align="center" class="col-sm-6">
                            <asp:Button ID="paymentPageButton" runat="server" Text="Pay Here!" CssClass="btn btn-primary btn-lg btn-block" OnClick="paymentPageButton_Click" />
                        </div>
                    </div>
                    <div align="center" class="form-group">
                        <div align="center" class="col-sm-6">
                            <asp:Button ID="testWiseReportPageButton" runat="server" Text="Test Wise Report" CssClass="btn btn-primary btn-lg btn-block" OnClick="testWiseReportPageButton_Click" />
                        </div>
                    </div>
                    <div align="center" class="form-group">
                        <div align="center" class="col-sm-6">
                            <asp:Button ID="typeWiseReportPageButton" runat="server" Text="Type Wise Report" CssClass="btn btn-primary btn-lg btn-block" OnClick="typeWiseReportPageButton_Click" />
                        </div>
                    </div>
                    <div align="center" class="form-group">
                        <div align="center" class="col-sm-6">
                            <asp:Button ID="unpaidBillPageButton" runat="server" Text="Unpaid Bill Report" CssClass="btn btn-primary btn-lg btn-block" OnClick="unpaidBillPageButton_Click" />
                        </div>
                    </div>

                </form>
            </div>
        </div>
    </div>
    <br/>
    <br/>

    <footer class="panel-footer">
        <div style="text-align: center">
            <h4 class="text-info" style="text-align: center;">copyright@Leicester City</h4>
        </div>
    </footer>

</body>
</html>
