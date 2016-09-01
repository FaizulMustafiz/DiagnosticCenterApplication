<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTypeSetupPage.aspx.cs" Inherits="DiagnosticCenterApplication.UI.TestTypeSetupPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />

    <title>Test Type</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
</head>
<body>
    <div class="page-header" style="text-align: center">
        <h2>Test Type Setup Page</h2>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-10" style="margin-left: 150px">
                <form id="testTypeForm" runat="server" class="form-horizontal">

                    <div class="form-group">
                        <label for="testType" class="col-sm-2 control-label">Test Type</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="testTypeTextbox" runat="server" CssClass="form-control" name="testtype" Placeholder="Enter Test Type"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:Button ID="saveTestTypeButton" runat="server" Text="Save" CssClass="btn btn-info" OnClick="saveTestTypeButton_Click" />
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="form-group">
                            <asp:Label ID="messageLable" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div>
                        <div class="form-group">
                            <asp:GridView ID="testTypeGridView" AutoGenerateColumns="False" runat="server" CssClass="table" HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="Serial No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("TestTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
    <script src="../Scripts/jquery.validate.js"></script>

    <script>
        $(document).ready(function() {
            jQuery.validator.addMethod(
                "content",
                function(value, element) {
                    var isValid = /^[A-Za-z][a-zA-Z0-9- .]+$/.test(value);
                    return this.optional(element) || isValid;
                },
                "Insert"
            );

            $("[id*=saveTestTypeButton]").click(function() {
                $("#testTypeForm").validate({
                    rules: {
                        <%= testTypeTextbox.UniqueID %>: {
                            required: true,
                            content: true,
                            minlength: 3
                        }
                    },
                    messages: {
                        <%= testTypeTextbox.UniqueID %>: {
                            required: "Please provide a Test Type name",
                            content: "Test Type Name Can Have Alphabets Digits Space & - Only!",
                            minlength: "Test Type Name Must be At Least 3 Characters long!"
                        }
                    }
                });
            });

            $("[id*=indexButton]").click(function() {
                Window.location.href = "../Index.aspx"
            });



        });
    </script>

</body>
</html>
