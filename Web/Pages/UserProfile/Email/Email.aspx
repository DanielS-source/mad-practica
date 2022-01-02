<%@ Page Language="C#" MasterPageFile="~/Pages/UserProfile/UserProfile.Master" AutoEventWireup="true" CodeBehind="Email.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Email.Email" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="d-flex mt-4">
        <div>
            <div class="mb-1">
                <asp:Label ID="InfoLabel" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources:, InfoLabel.Text %>" runat="server"></asp:Label>
            </div>
            <asp:CustomValidator ID="EmailValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , EmailValidator.ErrorMessage %>" CssClass="text-danger" OnServerValidate="EmailValidator_ServerValidate" runat="server"></asp:CustomValidator>
            <div class="input-group">
                <asp:TextBox ID="EmailTextBox" MaxLength="40" CssClass="form-control" Style="width: 410px;" placeholder="<%$ Resources: , EmailTextBox.Placeholder %>" runat="server"></asp:TextBox>
            </div>
            <asp:Button ID="SaveButton" Text="<%$ Resources: , SaveButton.Text %>" CssClass="btn btn-secondary btn-block mt-4" OnClick="SaveButton_Click" runat="server" />
        </div>
    </div>
</asp:Content>