<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSetupPage.aspx.cs" Inherits="DiagnosticCenterApplication.UI.TestSetupPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />

    <title>Test Setup</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
</head>
<body>
    <div class="page-header" style="text-align: center">
        <h2>Test Setup Page</h2>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-10">
                <form id="testSetUpForm" runat="server" class="form-horizontal">

                    <div class="form-group">
                        <label for="test" class="col-sm-2 control-label">Test Name</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="testNameTextBox" runat="server" CssClass="form-control" name="test" placeholder="Enter Test Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="testFee" class="col-sm-2 control-label">Test Fee</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="feeTextBox" runat="server" CssClass="form-control" name="testFee" placeholder="Enter Test Fee"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="selectTestType" class="col-sm-2 control-label">Test Type</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="testTypeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control dropdown" name="selectTestType">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="messageLable" runat="server" Text="" CssClass="text-info"></asp:Label>
                        <br />
                        <br />
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="saveButton" runat="server" Text="Save" CssClass="btn btn-info" OnClick="saveButton_Click" />
                        </div>
                    </div>

                    <br />
                    <br />

                    <div>
                        <div class="form-group">
                            <asp:GridView ID="testSetupGridView" runat="server" AutoGenerateColumns="False" CssClass="table" HorizontalAlign="Center">
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
                                    <asp:TemplateField HeaderText="Fee">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("TestFee") %>'></asp:Label>
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
        $(document).ready(function () {

            jQuery.validator.addMethod(
           "content",
           function(value, element) {
               var isValid = /^[A-Za-z][a-zA-Z0-9- .]+$/.test(value);
               return this.optional(element) || isValid;
           },
           "Insert "
           );
            
            

            jQuery.validator.addMethod(
       "money",
           function(value, element) {
               var isValidMoney = /^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$/.test(value);
               return this.optional(element) || isValidMoney;
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

            $("[id*=saveButton]").click(function() {
                $("#testSetUpForm").validate({
                    rules: {
                        <%= testNameTextBox.UniqueID %>: {
                        required:true,
                        content:true,
                        minlength:3
                    },
                    <%= feeTextBox.UniqueID %>: {
                        required: true,
                        money:true
                    },
                    <%= testTypeDropDownList.UniqueID %>: {
                        required: true,
                        selected: true
                    }
                    
                    
                },
                    messages: {
                        <%= testNameTextBox.UniqueID %>: {
                        required:"Please provide Test Name",
                        content: "Test Type Name Can Have Alphabets Digits Space & - Only!",
                        minlength: "Test Type Name Must be At Least 5 Characters long!"
                    },
                    <%= feeTextBox.UniqueID %>: {
                        required:"Please provide Fee",
                        money: "Fee must be positive and numeric value"
                    },
                    <%= testTypeDropDownList.UniqueID %>: {
                        required: "Please select Test Type",
                        selected:  "Please select Test Type"
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
