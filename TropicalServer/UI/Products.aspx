<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TropicalServer.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TropicalServer.UI.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../AppThemes/TropicalStyles/Products.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="productCategories">
        <div class="productCategoriesLabel">Product Categories</div>
        <asp:Repeater ID="rptrProductCategories" runat="server" >
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="itemType" CssClass="imageStyle" runat="server" Text='<%# Eval("ItemTypeDescription") %>' CommandArgument='<%# Eval("ItemTypeDescription") %>' OnClick="itemType_Click"/>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="chartdisplay">
        <asp:GridView class="dataGrid" ID="gvChart" runat="server" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvChart_PageIndexChanging">
            <AlternatingRowStyle CssClass="chartAlternatingItemStyle" />
            <HeaderStyle CssClass="chartHeaderStyle" />
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="5" PreviousPageText="Prev" />
            <RowStyle CssClass="chartItemStyle" />
        </asp:GridView>     
    </div>   
</asp:Content>
