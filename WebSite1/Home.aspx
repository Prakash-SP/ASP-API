<%@ Page Language="C#" MasterPageFile="~/navbar.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>


<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <div class="jumbotron">
                    <h1>Welcome to dashboard</h1>
                    <h2>Hello
                        <asp:Label ID="name" runat="server" Text="Label"></asp:Label></h2>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="jumbotron">
                    <h1>Table</h1>
                    <asp:GridView ID="GridView1" runat="server" OnRowEditing="editRow" OnRowDeleting="deleteRow" OnRowUpdating="updateRow" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lb_code" runat="server" Text='<%# Eval("barcode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lb_name" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label ID="lb_company" runat="server" Text='<%# Eval("compname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pack">
                                <ItemTemplate>
                                    <asp:Label ID="lb_pack" runat="server" Text='<%# Eval("pack") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lb_qty" runat="server" Text='<%# Eval("Clqty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tb_qty" runat="server" Text='<%# Eval("Clqty") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" ShowHeader="True" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />

                        </Columns>
                    </asp:GridView>

                    <asp:Literal ID="showErrMsg" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="jumbotron">
                    <h3>Data Reader Example</h3>
                    <asp:GridView ID="GridView2" runat="server">
                    </asp:GridView>

                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="jumbotron">
                    <h3>Data Table Example</h3>
                    <asp:GridView ID="GridView3" runat="server">
                    </asp:GridView>

                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

