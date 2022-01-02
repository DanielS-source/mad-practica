<%@ Page Language="C#" MasterPageFile="~/Pages/UserProfile/UserProfile.Master" AutoEventWireup="true" CodeBehind="FirstNameLastName.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.FirstNameLastName.FirstNameLastName" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="d-flex mt-4">
        <div>
            <div class="mb-1">
                <asp:Label ID="InfoLabel" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources:, InfoLabel.Text %>" runat="server"></asp:Label>
            </div>
            <asp:CustomValidator ID="FirstNameLastNameValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , FirstNameLastNameValidator.ErrorMessage %>" CssClass="text-danger" OnServerValidate="FirstNameLastNameValidator_ServerValidate" runat="server"></asp:CustomValidator>
            <div class="form-row">
                <div class="input-group col">
                    <asp:TextBox ID="FirstNameTextBox" MaxLength="16" CssClass="form-control" placeholder="<%$ Resources: , FirstNameTextBox.Placeholder %>" runat="server"></asp:TextBox>
                </div>
                <div class="input-group col">
                    <asp:TextBox ID="LastNameTextBox" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , LastNameTextBox.Placeholder %>" runat="server"></asp:TextBox>
                </div>
            </div>
            <asp:Button ID="SaveButton" Text="<%$ Resources: , SaveButton.Text %>" CssClass="btn btn-secondary btn-block mt-4" OnClick="SaveButton_Click" runat="server" />
        </div>
    </div>
</asp:Content>