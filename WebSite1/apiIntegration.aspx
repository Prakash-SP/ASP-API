<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/navbar.master" CodeFile="apiIntegration.aspx.cs" Inherits="apiIntegration" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        .scrollTable{
            height: 500px;
            overflow: scroll;
        }
    </style>
    <div class="container mt-5">
        <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>

        <asp:GridView ID="GridView1" runat="server" class="scrollTable table table-dark table-responsive table-hover"></asp:GridView>
            
    </div>
</asp:Content>