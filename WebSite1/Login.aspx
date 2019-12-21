<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/StyleSheet.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</head>
<body class="bgImg">
    <form id="form1" runat="server">
    <div class="container">
        <div class="row mt-5">
            
            <div class="col-lg-2"></div>
            <div class="col-lg-8 mt-5">
                <div class="jumbotron mt-5">
                    <h2 style="text-align: center">Login</h2>
                    <div class="form-group">
                        <label>Customer ID</label>
                        <asp:TextBox runat="server" ID="username" placeholder="Enter User Name" class="form-control" MaxLength="5" ></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Password</label>
                        <asp:TextBox runat="server" ID="password" placeholder="*************" TextMode="Password" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="login" runat="server" class="btn btn-success justify-content-end" Text="Login" OnClick="login_Click" />
                    </div>
                    
                    <div class="form-group" style="text-align: center; color: red">
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="col-lg-2"></div>
        </div>
    </div>
    </form>

    <script src="js/script.js"></script>
</body>
</html>
