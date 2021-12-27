<%@ Page Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Login.Login" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form runat="server">
        <div class="custom-container" style="padding: 5%;">
            <div class="d-flex justify-content-center">
                <svg xmlns="http://www.w3.org/2000/svg" width="100" height="100" fill="currentColor" class="bi bi-person-bounding-box" viewBox="0 0 16 16">
                    <path fill-rule="evenodd" d="M1.5 1a.5.5 0 0 0-.5.5v3a.5.5 0 0 1-1 0v-3A1.5 1.5 0 0 1 1.5 0h3a.5.5 0 0 1 0 1h-3zM11 .5a.5.5 0 0 1 .5-.5h3A1.5 1.5 0 0 1 16 1.5v3a.5.5 0 0 1-1 0v-3a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 1-.5-.5zM.5 11a.5.5 0 0 1 .5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 1 0 1h-3A1.5 1.5 0 0 1 0 14.5v-3a.5.5 0 0 1 .5-.5zm15 0a.5.5 0 0 1 .5.5v3a1.5 1.5 0 0 1-1.5 1.5h-3a.5.5 0 0 1 0-1h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 1 .5-.5z" />
                    <path fill-rule="evenodd" d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" />
                </svg>
            </div>
            <div class="d-flex justify-content-center mt-1">
                <div>
                    <asp:CustomValidator ID="UsernameValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , UsernameValidator.ErrorMessage %>" CssClass="text-danger" OnServerValidate="UsernameValidator_ServerValidate" runat="server"></asp:CustomValidator>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">
                                <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-person-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd" d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" />
                                </svg>
                            </span>
                        </div>
                        <asp:TextBox ID="UsernameTextBox" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , UsernameTextBox.Placeholder %>" runat="server"></asp:TextBox>
                    </div>
                    <asp:CustomValidator ID="PasswordValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , PasswordValidator.ErrorMessage %>" CssClass="text-danger" OnServerValidate="PasswordValidator_ServerValidate" runat="server"></asp:CustomValidator>
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <span class="input-group-text">
                                <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-lock-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M2.5 9a2 2 0 0 1 2-2h7a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2h-7a2 2 0 0 1-2-2V9z" />
                                    <path fill-rule="evenodd" d="M4.5 4a3.5 3.5 0 1 1 7 0v3h-1V4a2.5 2.5 0 0 0-5 0v3h-1V4z" />
                                </svg>
                            </span>
                        </div>
                        <asp:TextBox ID="PasswordTextBox" TextMode="Password" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , PasswordTextBox.Placeholder %>" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <asp:CheckBox ID="RememberCheckBox" runat="server" />
                        <asp:Label ID="RememberLabel" Text="<%$ Resources: , RememberLabel.Text %>" CssClass="form-check-label" for="RememberCheckBox" runat="server"></asp:Label>
                    </div>
                    <asp:Button ID="LoginButton" Text="<%$ Resources: , LoginButton.Text %>" CssClass="btn btn-secondary btn-block mt-3" OnClick="LoginButton_Click" runat="server" />
                    <div class="d-flex justify-content-center mt-4">
                        <asp:Label ID="RegisterLabel" Text="<%$ Resources: , RegisterLabel.Text %>" runat="server"></asp:Label>
                        <asp:LinkButton ID="RegisterLinkButton" Text="<%$ Resources: , RegisterLinkButton.Text %>" CssClass="ml-2" CausesValidation="False" OnClick="RegisterLinkButton_Click" runat="server"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
