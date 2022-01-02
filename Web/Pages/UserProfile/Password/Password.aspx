<%@ Page Language="C#" MasterPageFile="~/Pages/UserProfile/UserProfile.Master" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Password.Password" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="d-flex mt-4">
        <div>
            <div class="mb-1">
                <asp:Label ID="InfoLabel" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources:, InfoLabel.Text %>" runat="server"></asp:Label>
            </div>
            <asp:CustomValidator ID="OldPasswordValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , OldPasswordValidator.ErrorMessage %>" CssClass="text-danger" OnServerValidate="OldPasswordValidator_ServerValidate" runat="server"></asp:CustomValidator>
            <asp:TextBox ID="OldPasswordTextBox" TextMode="Password" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , OldPasswordTextBox.Placeholder %>" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="NewPasswordValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , NewPasswordValidator.ErrorMessage %>" CssClass="text-danger" OnServerValidate="NewPasswordValidator_ServerValidate" runat="server"></asp:CustomValidator>
            <div class="form-row">
                <div class="input-group col">
                    <asp:TextBox ID="NewPasswordTextBox" TextMode="Password" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , NewPasswordTextBox.Placeholder %>" runat="server"></asp:TextBox>
                </div>
                <div class="input-group col">
                    <asp:TextBox ID="NewPasswordConfirmTextBox" TextMode="Password" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , NewPasswordConfirmTextBox.Placeholder %>" runat="server"></asp:TextBox>
                </div>
            </div>
            <asp:Button ID="SaveButton" Text="<%$ Resources: , SaveButton.Text %>" CssClass="btn btn-secondary btn-block mt-4" OnClick="SaveButton_Click" runat="server" />
        </div>
    </div>
</asp:Content>