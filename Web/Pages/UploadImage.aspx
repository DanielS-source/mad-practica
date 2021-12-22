<%@ Page Title="Post Photo" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Web.Pages.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="form">
        <form id="CreateAccountForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclIdentifier" runat="server" Text="<%$ Resources:Common, userId %>" />
                </span><span class="entry">
                    <asp:TextBox ID="txtUserId" runat="server" Width="200px" Columns="16"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="typeUserIdValidator" runat="server" ControlToValidate="txtUserId"
                        ValidationExpression="(\d)*" meta:resourcekey="typeUserIdValidator" CssClass="errorMessage"
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                        Display="Dynamic" Text="<%$ Resources: Common, mandatoryField %>"
                        CssClass="errorMessage"></asp:RequiredFieldValidator>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclBalance" runat="server" Text="<%$ Resources:Common, balance %>" /></span>
                <span class="entry">
                    <asp:TextBox ID="txtBalance" runat="server" Width="200px" Columns="16"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="typeBalanceValidator" runat="server" ControlToValidate="txtBalance"
                        ValidationExpression="^\d+(\.\d{1,2})?$" meta:resourcekey="typeBalanceValidator"
                        CssClass="errorMessage" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvBalance" runat="server" ControlToValidate="txtBalance"
                        Display="Dynamic" Text="<%$ Resources: Common, mandatoryField %>" CssClass="errorMessage"></asp:RequiredFieldValidator>
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnCreate" runat="server" meta:resourcekey="btnCreate" OnClick="BtnCreateClick" />
            </div>
        </form>
        <form id="PostImage" method="post" runat="server">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload_Image" />
            <hr />
            <asp:Image ID="Image1" runat="server" Height = "100" Width = "100" />
        </form>
    </div>

</asp:Content>
