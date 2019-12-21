<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title> 
    <link rel="stylesheet" href="css/StyleSheet.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</head>
<body class="bgImg">
    <form id="form1" runat="server">
    <div class="container">
        <div class="row mt-5">
            <div class="col-lg-1"></div>
            <div class="col-lg-10">
                <div class="jumbotron">
                    <h2 class="text-center">Registration Form</h2>
                    <div class="row mb-5">
                        <div class="col-lg-12">
                            <span class="text-danger">*</span> <small>All Fields are required.</small>
                        </div>
                    </div>


                    <div class="form-group">
                        <label>Name</label><span class="text-danger">*</span>
                        <div class="row">
                            <div class="col-lg-4">
                                <asp:TextBox ID="fname" runat="server" class="form-control mr-2" placeholder="First Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="fname" runat="server" ErrorMessage="Name Cannot Be left Empty. This Field is mendatory" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="fname" runat="server" ForeColor="Red" ErrorMessage="Number is not allowed" ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-4">
                                <asp:TextBox ID="mName" runat="server" class="form-control mr-2" placeholder="Middle Name"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="mname" runat="server" ForeColor="Red" ErrorMessage="Number is not allowed" ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-4">
                                <asp:TextBox ID="lname" runat="server" class="form-control mr-2" placeholder="Last Name"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="lname" runat="server" ForeColor="Red" ErrorMessage="Number is not allowed" ValidationExpression="^[a-zA-Z]+$" ></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Date of Birth</label><span class="text-danger">*</span>
                        <asp:TextBox ID="dob" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="dob" runat="server" ErrorMessage="Date of Birth Be left Empty. This Field is mendatory" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Email ID</label><span class="text-danger">*</span>
                        <asp:TextBox ID="email" runat="server" class="form-control" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="email" runat="server" ErrorMessage="Email Field Cannot Be left Empty. This Field is mendatory" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="email" runat="server" ForeColor="Red" ErrorMessage="Invalid Email Address" ValidationExpression="^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$" ></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label>Contact Number</label><span class="text-danger">*</span>
                        <asp:TextBox ID="contactNo" runat="server" class="form-control" TextMode="Phone"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="contactNo" runat="server" ErrorMessage="Contact Number Field Cannot Be left Empty. This Field is mendatory" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="contactNo" runat="server" ForeColor="Red" ErrorMessage="Invalid Contact Number" ValidationExpression="^\d{10}$" ></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label>Gender</label><span class="text-danger">*</span>
                        <asp:DropDownList ID="gender" runat="server" class="form-control">
                           <asp:ListItem>--Select Gender--</asp:ListItem>
                           <asp:ListItem >Male</asp:ListItem>
                           <asp:ListItem>Female</asp:ListItem>
                           <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="gender" runat="server" ErrorMessage="Please Select Gender Field Cannot Be left Empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Address</label><span class="text-danger">*</span>
                        <asp:TextBox ID="address" runat="server" class="form-control" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="address" runat="server" ErrorMessage="Address Field Cannot Be left Empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>          
                    <div class="form-group">
                        <label>Upload Image</label><span class="text-danger">*</span>
                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="FileUpload1" runat="server" ErrorMessage="Upload Image Field is required." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Password</label><span class="text-danger">*</span>
                        <asp:TextBox ID="password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="password" runat="server" ErrorMessage="Password Field is required." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Re-Enter Password</label><span class="text-danger">*</span>
                        <asp:TextBox ID="rePassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="password" runat="server" ErrorMessage="Password Field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password is not Matched" ForeColor="Red" ControlToCompare="rePassword" ControlToValidate="password" Display="Dynamic"></asp:CompareValidator>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="bt_register" runat="server" Text="Register" class="btn btn-success" OnClick="bt_register_Click" />
                    </div>
                    <div class="form-group">
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="col-lg-1"></div>
        </div>
        <div class="row">
            <%--<div class="col-lg-12 jumbotron">
                <div class="table-responsive table-hover table-bordered">
                    <table class="table bg-primary text-white">
                    <thead>
                        <tr>
                            <th>Data Enter By User</th>
                            <th>Values </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>First Name</td>
                            <td>
                                <asp:Label ID="lb_fName" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="lb_mName" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="lb_lName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Image</td>
                            <td>
                                <asp:Image ID="Image1" runat="server" Height="100" Width="100" />
                            </td>
                        </tr>    
                        <tr>
                            <td>Date of Birth</td>
                            <td>
                                <asp:Label ID="lb_dob" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Email ID</td>
                            <td>
                                <asp:Label ID="lb_email" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Contact Number</td>
                            <td>
                                <asp:Label ID="lb_contactNo" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Gender</td>
                            <td>
                                <asp:Label ID="lb_gender" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Address</td>
                            <td>
                                <asp:Label ID="lb_address" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Password</td>
                            <td>
                                <asp:Label ID="lb_password" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Re Password</td>
                            <td>
                                <asp:Label ID="lb_repassword" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </div>
            </div>--%>

            <div class="col-lg-12 jumbotron">
                <h2 class="text-center">List of Users</h2>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-hover table-responsive table-primary" OnRowDeleting="deleteRow" OnRowEditing="editRow" OnRowUpdating="updateRow" OnRowCancelingEdit="canceledit" >
                    <Columns>
                        <asp:TemplateField HeaderText="User ID">
                            <ItemTemplate>
                                <asp:Label ID="lb_userID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:Label ID="lb_fName" runat="server" Text='<%# Eval("fName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tb_FirstName" runat="server" Text='<%# Eval("fName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Middle Name">
                            <ItemTemplate>
                                <asp:Label ID="lb_mName" runat="server" Text='<%# Eval("mName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tb_MiddleName" runat="server" Text='<%# Eval("mName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <ItemTemplate>
                                <asp:Label ID="lb_lName" runat="server" Text='<%# Eval("lName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tb_LastName" runat="server" Text='<%# Eval("lName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Number">
                            <ItemTemplate>
                                <asp:Label ID="lb_CNo" runat="server" Text='<%# Eval("contactNo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tb_ContactNumber" runat="server" Text='<%# Eval("contactNo") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Address">
                            <ItemTemplate>
                                <asp:Label ID="lb_mail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tb_Email" runat="server" Text='<%# Eval("email") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gender">
                            <ItemTemplate>
                                <asp:Label ID="lb_gender" runat="server" Text='<%# Eval("gender") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tb_Gender" runat="server" Text='<%# Eval("gender") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <asp:Label ID="lb_address" runat="server" Text='<%# Eval("address") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tb_Address" runat="server" Text='<%# Eval("address") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" ShowHeader="True" CausesValidation="False" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                    </Columns>
                </asp:GridView>

                <asp:Literal ID="regTableMsg" runat="server"></asp:Literal>
            </div>

        </div>
    </div>
    </form>
</body>
</html>