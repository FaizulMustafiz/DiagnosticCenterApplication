<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="DiagnosticCenterApplication.UI.PaymentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />

    <title>Payment</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-autoComplete.js"></script>


    <script>
        $(document).ready(function () {

            $('#billNoOrMobilenoSearchTextBox').autocomplete({
                Source: 'BillNoHandler.ashx'
            });

        });
    </script>

</head>
<body>
    <div class="page-header" style="text-align: center">
        <h2>Payment Page</h2>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-10" style="margin-left: 150px">
                <form id="payment" runat="server" class="form-horizontal">

                    <div class="form-group">
                        <label for="billNoOrMobileNo" class="col-sm-2 control-label">Bill/Mobile Number</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="billNoOrMobilenoSearchTextBox" runat="server" CssClass="form-control" name="billNoOrMobileNo" Placeholder="Enter Bill/Mobile Number"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="messageLable" runat="server" Text="" CssClass="text-info"></asp:Label>
                        <br />
                        <br />
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="searchButton" runat="server" Text="Search" CssClass="btn btn-success" OnClick="searchButton_Click" />
                        </div>
                    </div>


                    <div>
                        <div class="form-group">
                            <asp:GridView ID="billInfoGridView" runat="server" AutoGenerateColumns="False" CssClass="table" HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="Serial No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Fee">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("TestFee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>



                    <div class="form-group" align="center">
                        <table style="table-layout: inherit; text-align: center; margin-top: 25px; margin-bottom: 25px; margin-right: 400px;">
                            <tr>
                                <td>
                                    <asp:Label ID="billDateFixedLable" runat="server" Text="Bill Date: "></asp:Label></td>
                                <td>
                                    <asp:Label ID="billDateLable" runat="server" Text="" Font-Bold="True" CssClass="text-success"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="totalFeeFixedLable" runat="server" Text="Total Fee: "></asp:Label></td>
                                <td>
                                    <asp:Label ID="totalFeeLable" runat="server" Text="" Font-Bold="True" CssClass="text-success"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="paidAmountFixedlable" runat="server" Text="Paid Amount: "></asp:Label></td>
                                <td>
                                    <asp:Label ID="paidAmountLable" runat="server" Text="" Font-Bold="True" CssClass="text-success"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="dueAmountFixedLable" runat="server" Text="Due Amount: "></asp:Label></td>
                                <td>
                                    <asp:Label ID="dueAmountLable" runat="server" Text="" Font-Bold="True" CssClass="text-success"></asp:Label></td>
                            </tr>
                        </table>
                    </div>


                    <div class="form-group">
                        <label for="amount" class="col-sm-2 control-label">Amount</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="amountTextBox" runat="server" CssClass="form-control" name="amount" placeholder="Enter Amount to pay"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="payButton" runat="server" Text="Pay" CssClass="btn btn-info" OnClick="payButton_Click" />
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






    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/jquery-3.1.0.js"></script>
    <script src="../Scripts/jquery.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/jquery.validate.js"></script>
    <script src="../Scripts/bootstrap-datepicker.js"></script>

</body>
</html>
