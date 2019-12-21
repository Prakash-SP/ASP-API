<%@ Page Language="C#" MasterPageFile="~/navbar.master" AutoEventWireup="true" CodeFile="createOrder.aspx.cs" Inherits="createOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .CompletionList {
            border: solid 2px #17a2b8;
            margin: 0px;
            padding: 3px;
            height: 120px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .CompletionListItem {
            color: black;
        }

        .CompletionListHighlightedItem {
            background-color: black;
            color: white;
        }
    </style>
    <script>
        function seletedItem(sender, e) {
            $get("<%=tb_code.ClientID %>").value = e.get_value();
        }
        $('body').on('keydown', 'input, select, textarea', function (e) {
            var self = $(this)
              , form = self.parents('form:eq(0)')
              , focusable
              , next
            ;
        });
    </script>
</asp:Content>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container mt-3">
        <div class="row">
            <div class="col-lg-12">
                <div class="jumbotron">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label class="mr-2">Item</label>
                                <!--<asp:TextBox ID="tb_item" runat="server" placeholder="Item" class="form-control" AutoPostBack="true" TabIndex="1" OnTextChanged="tb_item_TextChanged" ></asp:TextBox>
                                <asp:TextBox ID="tb_code" runat="server" hidden />
                                <ajax:AutoCompleteExtender runat="server"
                                    ServiceMethod="getProductData"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="1000"
                                    EnableCaching="false"
                                    CompletionSetCount="1"
                                    TargetControlID="tb_item"
                                    ID="AutoCompleteExtender1"
                                    FirstRowSelected="true" CompletionListCssClass="CompletionList"
                                    CompletionListItemCssClass="CompletionListItem"
                                    CompletionListHighlightedItemCssClass="CompletionListHighlightedItem"
                                    ShowOnlyCurrentWordInCompletionListItem="true" 
                                    OnClientItemSelected="seletedItem">
                                </ajax:AutoCompleteExtender>-->
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label class="mr-2">Quantity</label>
                                <asp:TextBox ID="tb_qty" runat="server" placeholder="Quantity" TextMode="Number" TabIndex="2" class="form-control"></asp:TextBox>
                                <asp:TextBox ID="tb_orderNo" runat="server" class="form-control" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label class="mr-2">Item Status</label>
                                <asp:TextBox ID="tb_itemstatus" runat="server" placeholder="Item Status" ReadOnly class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label class="mr-2">Pack</label>
                                <asp:TextBox ID="tb_pack" runat="server" placeholder="Pack" ReadOnly class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="mr-2">Scheme</label>
                                <asp:TextBox ID="tb_scheme" runat="server" placeholder="Scheme" ReadOnly class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="mr-2">MRP</label>
                                <asp:TextBox ID="tb_mrp" runat="server" placeholder="MRP" ReadOnly class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="mr-2">Rate</label>
                                <asp:TextBox ID="tb_rate" runat="server" placeholder="Rate" ReadOnly class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        
                    </div>
                    <div class="row mt-5">
                        <div class="col-lg-6">
                            <label>
                                Total Amount: <span class="fa fa-rupee"></span>
                                <asp:Literal ID="Literal1" runat="server" Text="0"></asp:Literal>
                            </label>
                        </div>
                        <div class="col-lg-6 d-flex justify-content-end">
                            <asp:Button ID="btn_insert" runat="server" Text="Add" CssClass="btn btn-primary mr-3" TabIndex="3" OnClick="btn_insert_Click"></asp:Button>
                            <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btn_reset_Click"></asp:Button>
                            <asp:Button ID="btn_newCustomerOrder" runat="server" Text="New Order" CssClass="btn btn-success" OnClick="btn_newCustomerOrder_Click" />    
                        </div>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-lg-12">
                        <div class="jumbotron">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-primary table-hover table-responsive" AutoGenerateColumns="false" OnRowEditing="editRow" OnRowDeleting="deleteRow" OnRowUpdating="updateRow">
                                <Columns>
                                    <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_orderNo" runat="server" Text='<%#Eval("orderNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Customer ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_custID" runat="server" Text='<%#Eval("custID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_itemCode" runat="server" Text='<%#Eval("itemCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_itemName" runat="server" Text='<%#Eval("itemName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_qty" runat="server" Text='<%#Eval("quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tb_qty" runat="server" Text='<%#Eval("quantity") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_rate" runat="server" Text='<%#Eval("rate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_price" runat="server" Text='<%#Eval("mrp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scheme">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_scheme" runat="server" Text='<%#Eval("scheme") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pack">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_pack" runat="server" Text='<%#Eval("pack") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_tAmt" runat="server" Text='<%#Eval("totalPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" ShowHeader="True" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                                </Columns>
                            </asp:GridView>
                            <div class="row">
                                <div class="col-lg-12 d-flex justify-content-end">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Create Order" OnClick="btnCreate_Click" />
                                </div>
                            </div>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
