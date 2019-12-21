<%@ Page Language="C#" MasterPageFile="~/navbar.master" AutoEventWireup="true" CodeFile="SqlTranscationDemo.aspx.cs" Inherits="SqlTranscationDemo" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <div class="jumbotron">
                   <h1 class="mb-4">Student Form<small>Sql Transactionv Demo</small></h1>
                       <div class="form-group">
                           <label>Student Name</label>
                           <asp:TextBox ID="sName" runat="server" class="form-control" placeholder="Student Name"></asp:TextBox>
                       </div>
                       <div class="form-group">
                           <label>Address</label>
                           <asp:TextBox ID="sAddress" runat="server" class="form-control" placeholder="Address"></asp:TextBox>
                       </div>
                       <div class="form-group">
                           <label>Course ID</label>
                           <asp:TextBox ID="cID" runat="server" class="form-control" placeholder="Course ID"></asp:TextBox>
                       </div>
                       <div class="form-group">
                           <label>Course Name</label>
                           <asp:TextBox ID="cName" runat="server" class="form-control" placeholder="Course Name"></asp:TextBox>
                       </div>
                       <div class="form-group">
                           <label>Duration</label>
                           <asp:DropDownList ID="cDuration" runat="server" class="form-control">
                               <asp:ListItem>--Select Course Duration-</asp:ListItem>
                               <asp:ListItem>1 year</asp:ListItem>
                               <asp:ListItem>2 year</asp:ListItem>
                               <asp:ListItem>3 year</asp:ListItem>
                               <asp:ListItem>4 year</asp:ListItem>
                           </asp:DropDownList>
                       </div>
                       <div class="form-group d-flex justify-content-end">
                           <asp:Button ID="btnSave" runat="server" class="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                       </div>
                   </div>
            </div>
        </div>
    </div>
</asp:Content>