<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TropicalServer.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="TropicalServer.UI.Orders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="CriteriaBar">
        <label class="Criteria label">
            Order Date:
        </label>
        <asp:DropDownList CssClass="Criteria Input" ID="ddlOrderDate" runat="server" AutoPostBack="True"></asp:DropDownList>
        <label class="Criteria label">
            Customer ID:
        </label>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
            <Services>
                <asp:ServiceReference Path="~/OrderService.asmx" />
            </Services>
        </asp:ScriptManager>
        <asp:TextBox ID="tbxCustomerID" runat="server" AutoPostBack="True"></asp:TextBox>
        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
            TargetControlID="tbxCustomerID"
            EnableCaching="true"
            CompletionSetCount="1"
            MinimumPrefixLength="3"
            CompletionInterval="1000"
            ServiceMethod="GetCustomerID"
            ServicePath="~/OrderService.asmx">
        </ajaxToolkit:AutoCompleteExtender>
        <label class="Criteria label">
            Customer Name:
        </label>

        <label class="Criteria label">
            Sales Manager:
        </label>
        <asp:DropDownList CssClass="Criteria Input" ID="ddlSalesManager" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="CustOrderEntryContactName" DataValueField="CustOrderEntryContactName" OnSelectedIndexChanged="ddlSalesManager_SelectedIndexChanged">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>

    </div>
    <div id="grid">

        <asp:GridView class="gvOrders" ID="gvOrderResults" runat="server"
            DataKeyNames="OrderID"
            OnRowCancelingEdit="gvOrders_RowCancelingEdit"
            OnRowDeleting="gvOrders_RowDeleting"
            OnRowEditing="gvOrders_RowEditing"
            OnRowUpdating="gvOrders_RowUpdating"
            OnRowDataBound="OnRowDataBound"
            EmptyDataText="No records has been added." AutoGenerateColumns="False">
            <%--DataSourceID="SqlDataSource1"--%>
            <Columns>
                <asp:TemplateField HeaderText="OrderID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_OrderID" runat="server" Text='<%# Eval("OrderID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tracking #">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Tracking" runat="server" Text='<%# Eval("Tracking #") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Tracking" runat="server" Text='<%# Eval("Tracking #") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_OrderDate" runat="server" Text='<%# Eval("Order Date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_OrderDate" runat="server" Text='<%# Eval("Order Date") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer ID">
                    <ItemTemplate>
                        <asp:Label ID="lblCustID" runat="server" Text='<%# Eval("Customer ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CustomerName" runat="server" Text='<%# Eval("Customer Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_CustomerName" runat="server" Text='<%# Eval("Customer Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Address" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Address" runat="server" Text='<%# Eval("Address") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Route #">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RouteNumber" runat="server" Text='<%# Eval("Route #") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_RouteNumber" runat="server" Text='<%# Eval("Route #") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>Available Action</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LKB3" runat="server" CommandName="View" OnClick="gvOrderResults_SelectedIndexChanged">View</asp:LinkButton>
                        <asp:LinkButton ID="LkB1" runat="server" CommandName="Edit">Edit</asp:LinkButton>
                        <asp:LinkButton ID="LkB2" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LB2" runat="server" CommandName="Update">Update</asp:LinkButton>
                        <asp:LinkButton ID="LB3" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LkB2" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <asp:LinkButton Text="" ID = "lnkFake" runat="server" />
        <asp:Panel ID="Panel1" CssClass="modalPopup" style="background-color:white;" runat="server">
            <div class="header">
                <b>Details</b>
            </div>
            <div class="body">
                <table>
                    <tr>
                        <td style = "width:60px"><b>Tracking #: </b></td>
                        <td><asp:Label ID="lblTrackingNumber" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Order Date: </b></td>
                        <td><asp:Label ID="lblOrderDate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Customer ID: </b></td>
                        <td><asp:Label ID="lblCustID" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Customer Name: </b></td>
                        <td><asp:Label ID="lblCustName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Address</b></td>
                        <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Route #</b></td>
                        <td><asp:Label ID="lblRouteNumber" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <div>
                    <asp:Button ID="OK" runat="server" Text="OK" />
                </div>
            </div>            
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mpeView" runat="server" OkControlID="OK" 
            BackgroundCssClass="modalBackground" 
            PopupControlID="Panel1" 
            CancelControlID="OK" 
            TargetControlID="lnkFake">
        </ajaxToolkit:ModalPopupExtender>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TropicalServer.Properties.Settings.TropicalServerConn %>" SelectCommand="SELECT DISTINCT	c.CustOrderEntryContactName
	FROM tblOrder o 
	JOIN tblCustomer c ON o.OrderCustomerNumber = c.CustNumber
order by c.CustOrderEntryContactName"></asp:SqlDataSource>


    </div>
</asp:Content>
