<%@ Page Language="C#" MasterPageFile="~/Pages/UserProfile/UserProfile.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Account.Account" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <ul class="list-group mt-4 mr-2">
        <li class="list-group-item mr-5">
            <div class="row">
                <div class="col-sm-5 ml-3">
                    <asp:Label ID="FirstNameLastNameLabel" Text="<%$ Resources: , FirstNameLastNameLabel.Text %>" CssClass="row font-weight-bold" runat="server"></asp:Label>
                    <asp:Label ID="FirstNameLastNameFieldLabel" CssClass="row" runat="server"></asp:Label>
                </div>
                <div class="col-sm-3 ml-auto align-self-center">
                    <asp:Button ID="FirstNameLastNameModifyButton" Text="<%$ Resources: , ModifyButton.Text %>" CssClass="btn btn-secondary btn-block" CausesValidation="False" OnClick="FirstNameLastNameModifyButton_Click" runat="server" />
                </div>
            </div>
        </li>
        <li class="list-group-item mr-5">
            <div class="row">
                <div class="col-sm-5 ml-3">
                    <asp:Label ID="EmailLabel" Text="<%$ Resources: , EmailLabel.Text %>" CssClass="row font-weight-bold" runat="server"></asp:Label>
                    <asp:Label ID="EmailFieldLabel" CssClass="row" runat="server"></asp:Label>
                </div>
                <div class="col-sm-3 ml-auto align-self-center">
                    <asp:Button ID="EmailModifyButton" Text="<%$ Resources: , ModifyButton.Text %>" CssClass="btn btn-secondary btn-block" CausesValidation="False" OnClick="EmailModifyButton_Click" runat="server" />
                </div>
            </div>
        </li>   
        <li class="list-group-item mr-5">
            <div class="row">
                <div class="col-sm-5 ml-3">
                    <asp:Label ID="PasswordLabel" Text="<%$ Resources: , PasswordLabel.Text %>" CssClass="row font-weight-bold" runat="server"></asp:Label>
                    <asp:Label ID="PasswordFieldLabel" Text="********" CssClass="row" runat="server"></asp:Label>
                </div>
                <div class="col-sm-3 align-self-center ml-auto">
                    <asp:Button ID="PasswordModifyButton" Text="<%$ Resources: , ModifyButton.Text %>" CssClass="btn btn-secondary btn-block" CausesValidation="False" OnClick="PasswordModifyButton_Click" runat="server" />
                </div>
            </div>
        </li>
        <li class="list-group-item mr-5">
            <div class="row">
                <div class="col-sm-5 ml-3">
                    <asp:Label ID="CultureLabel" Text="<%$ Resources: , CultureLabel.Text %>" CssClass="row font-weight-bold" runat="server"></asp:Label>
                    <asp:Label ID="CultureFieldLabel" CssClass="row" runat="server"></asp:Label>
                </div>
                <div class="col-sm-3 align-self-center ml-auto">
                    <asp:Button ID="CultureModifyButton" Text="<%$ Resources: , ModifyButton.Text %>" CssClass="btn btn-secondary btn-block" CausesValidation="False" OnClick="CultureModifyButton_Click" runat="server" />
                </div>
            </div>
        </li>
    </ul>
</asp:Content>