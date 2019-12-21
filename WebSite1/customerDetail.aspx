<%@  Page Language="C#" MasterPageFile="~/navbar.master" AutoEventWireup="true" CodeFile="customerDetail.aspx.cs" Inherits="customerDetail" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        .tbWidth{
            width: 630px !important;
        }
        .scrollTable{
            height: 500px;
            overflow: scroll;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <div class="jumbotron">
                    <asp:GridView ID="GridView1" runat="server" class="scrollTable table table-dark table-responsive table-hover"></asp:GridView>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>        
                </div>
            </div>
        </div>
    </div>
</asp:Content>
