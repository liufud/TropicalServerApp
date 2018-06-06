<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TropicalServer.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TropicalServer.UI.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../AppThemes/TropicalStyles/Login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container">
        <div id="BodyDetail">
            <div>
                <label id="Loginlbl">MOBILE CUSTOMER ORDER TRACKING</label>
            </div>
            <div id="loginBox">

                <div id="usernamelbl">Login ID:  &nbsp&nbsp&nbsp <asp:TextBox ID="usernametextbox" runat="server"></asp:TextBox></div>
                <div id="passwordlbl">Password: <asp:TextBox ID="passwordtextbox" runat="server" TextMode="Password"></asp:TextBox></div>

                <div id="login">
                    <div>Remember my ID <asp:CheckBox ID="cbRememberID" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button id="loginButton" runat="server" Text="Log-In" BackColor="Black" ForeColor="White" OnClick="loginButton_Click"/></div>
                </div> 
                <div id="errorBox">                
                    <div id="errorlbl"><asp:Label ID="lblerror" runat="server" Text="Incorrect user credentials" ForeColor="Red"></asp:Label></div>
                </div>
            </div>    
            
            <div id="forgot">
                <a id="forgotUsername" href="#">Forgot Username</a>
                <a id="forgotPassword" href="#">Forgot Password</a>
            </div>     
        </div>
    </div>
</asp:Content>
