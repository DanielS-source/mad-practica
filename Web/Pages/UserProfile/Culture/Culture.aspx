<%@ Page Language="C#" MasterPageFile="~/Pages/UserProfile/UserProfile.Master" AutoEventWireup="true" CodeBehind="Culture.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Culture.Culture" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="d-flex mt-4">
        <div>
            <div class="mb-4">
                <asp:Label ID="InfoLabel" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources:, InfoLabel.Text %>" runat="server"></asp:Label>
            </div>
            <div class="form-row" style="width: 420px;">
                <div class="input-group col">
                    <asp:DropDownList ID="LanguageDropDownList" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="LanguageDropDownList_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </div>
                <div class="input-group col">
                    <asp:DropDownList ID="CountryDropDownList" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <asp:Button ID="SaveButton" Text="<%$ Resources: , SaveButton.Text %>" CssClass="btn btn-secondary btn-block mt-4" CausesValidation="False" OnClick="SaveButton_Click" runat="server" />
        </div>
    </div>
</asp:Content>