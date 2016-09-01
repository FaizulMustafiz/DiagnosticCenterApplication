<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRequestEntryPage.aspx.cs" Inherits="DiagnosticCenterApplication.UI.TestRequestEntryPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />

    <title>Test Request</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
</head>

<body>
    <div class="page-header" style="text-align: center">
        <h2>Test Request Page</h2>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-10" style="margin-left: 150px">
                <form id="testRequestForm" runat="server" class="form-horizontal">
                    <div class="form-group">
                        <table>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="patientNameLable" runat="server" Text="" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="billNoLable" runat="server" Text="" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="dateLable" runat="server" Text="" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div class="form-group">
                        <label for="patientName" class="col-sm-2 control-label">Patirnt Name</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="patientNameTextBox" runat="server" CssClass="form-control" name="patientName" placeholder="Patient Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="dob" class="col-sm-2 control-label">Date Of Birth</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="dobTextBox" runat="server" CssClass="form-control" name="dob" placholder="Date of Birth"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="mobileNo" class="col-sm-2 control-label">Mobile Number</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="mobileNoTextBox" runat="server" CssClass="form-control" name="mobileNo" placeholder="Mobile number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="selectTestDropdown" class="col-sm-2 control-label">Select Test</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="selectTestDropDownList" runat="server" CssClass="form-control dropdown" name="selectTestDropdown" OnSelectedIndexChanged="selectTestDropDownList_SelectedIndexChanged" AutoPostBack="True" EnableViewState="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="fee" class="col-sm-2 control-label" style="">Fee</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="feeTextBox" runat="server" CssClass="form-control input-sm" name="fee" placeHolder="Fee"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="addButton" runat="server" Text="Add" CssClass="btn btn-default" OnClick="addButton_Click" />
                        </div>
                    </div>
                    <div class="container-fluid">
                        <div class="form-group">
                            <div class="col-sm-10">
                                <asp:GridView ID="testRequestGridview" HorizontalAlign="Center" runat="server" CssClass="table" AutoGenerateColumns="False" ViewStateMode="Enabled" DataKeyNames="TestName" OnRowDataBound="testRequestGridview_RowDataBound" ShowFooter="True">
                                    <Columns>
                                        <%--<asp:BoundField DataField="SerialNo" HeaderText="Serial No" ReadOnly="True" SortExpression="SerialNo" ></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Serial No">
                                            <ItemTemplate>
                                                <span><%#Container.DataItemIndex+1 %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="TestName" HeaderText="Test Name" ReadOnly="True" SortExpression="TestName"></asp:BoundField>

                                        <asp:BoundField DataField="TestFee" HeaderText="Test Fee" ReadOnly="True" SortExpression="TestFee"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="messageLable" runat="server" Text="" CssClass="text-info"></asp:Label>
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click" CssClass="btn btn-info" />
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

        $(document).ready(function() {
            $('#dobTextBox').datepicker({
                autoclose: true,
                todayHighlight: true,
            });

            var checkDate = new Date();
            $("#dobTextBox").datepicker({
                maxDate: new Date(),
            });

            jQuery.validator.addMethod(
                "content",
                function(value, element) {
                    var isValid = /^[a-zA-Z][a-zA-Z ]+$/.test(value);
                    return this.optional(element) || isValid;
                },
                "Insert "
            );


            jQuery.validator.addMethod(
                "selected",
                function(value, element) {
                    if ($("select option:selected").index() > 0)
                        return true;

                },
                "Insert "
            );


            jQuery.validator.addMethod(
                "mobileNo",
                function(value, element) {
                    var isValidMoney = /^[0-9]*$/.test(value);
                    return this.optional(element) || isValidMoney;
                },
                "Insert "
            );

            $("[id*=saveButton]").click(function() {
                $("#testRequestForm").validate({
                    rules: {
                        <%= patientNameTextBox.UniqueID %>: {
                            required: true,
                            content: true,
                            minlength: 3
                        },
                        <%= dobTextBox.UniqueID %>: "required",
                        <%= mobileNoTextBox.UniqueID %>: {
                            required: true,
                            mobileNo: true,
                            minlength: 11,
                            maxlength: 11
                        }

                    },

                    messages: {
                        <%= patientNameTextBox.UniqueID %>: {
                            required: "Please provide Patient Name",
                            content: "Patient Name Can Have Alphabets Numbers & Space Only!",
                            minlength: "Patient Name Must be At Least 3 Characters long!"
                        },
                        <%= dobTextBox.UniqueID %>: "Please select Date of Birth",
                        <%= mobileNoTextBox.UniqueID %>: {
                            required: "Please provide Mobile No",
                            mobileNo: "Please provide corrent Mobile No",
                            minlength: "Mobile number should be 11 digits"
                        }
                    }

                });


                $("[id*=addButton]").click(function() {

                    $("#selectTestDropDownList").validate({
                        rules: {
                            <%= selectTestDropDownList.UniqueID %>: {
                                required: true,
                                selected: true
                            }
                        },
                        messages: {
                            <%= selectTestDropDownList.UniqueID %>: {
                                required: "Please select test type",
                                selected: "Please select test type"
                            }
                        }

                    });


                });

                var billNo = 1000;

                $("[id*=addButton]").click(function() {
                    var rowCount;
                    rowCount = $("[id*=testRequestGridview tr]").length; // GET ROW COUNT.

                    // ADD TEXTBOX VALUES TO THE GRIDVIEW.


                    billNo = billNo + 1;


                    if ($("[id*=selectTestDropDownList]").val() != '' && $("[id*=testRequestGridview tr]").length > 1) {
                        $("[id*=testRequestGridvie tr:last]").after('<tr>' +
                            '<td>' + rowCount + '</td>' +
                            '<td>' + $("[id*=selectTestDropDownList]").val() + '</td>' +
                            '<td>' + $("[id*=feeTextBox]").val() + '</td>' +
                            '<td>' + billNo + '</td>' +
                            '</tr>');

                    } else alert('Invalid!');
                });
            });
        });
    </script>
</body>
</html>
