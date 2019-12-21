<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultPage.aspx.cs" Inherits="DefaultPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    
    <link rel="stylesheet" href="css/StyleSheet.css" />
    <link rel="stylesheet" href="css/defaultPage.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</head>
<body class="bgHomeImg">
    <form id="form1" runat="server">
    <div class="container mt-5">
        <h1 class="text-dark text-center mt-5">Welcome to ASP .Net Traning</h1>
        <div class="row mt-3">
            <div class="col-lg-6">
                <div class="regBg">
                    <div class="text-center p-5">
                        <asp:LinkButton ID="regBtn" runat="server" OnClick="regBtn_Click">
                            <span class="fa fa-5x fa-address-card text-light"></span>
                            <h3 class="mt-3 text-light">Registration</h3>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="loginBg">
                    <div class="text-center p-5">
                        <asp:LinkButton ID="loginbtn" runat="server" OnClick="loginbtn_Click">
                            <span class="fa fa-5x fa-user-secret text-light"></span>
                            <h3 class="mt-3 text-light">Login Now</h3>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    
    </div>
    </form>
</body>
</html>
