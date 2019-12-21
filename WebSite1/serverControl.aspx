<%@ Page Language="C#" MasterPageFile="~/navbar.master" AutoEventWireup="true" CodeFile="serverControl.aspx.cs" Inherits="About" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-4">
                <div class="jumbotron">
                   <div class="form-group">
                       <h1>File Upload</h1>
                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                        <asp:Button ID="Button1" runat="server" Text="Upload" class="btn btn-success" OnClick="Button1_Click" />
                        <asp:Literal ID="fileMsg" runat="server"></asp:Literal>
                   </div> 
                </div> 
            </div>
        </div>
    </div>
</asp:Content> 