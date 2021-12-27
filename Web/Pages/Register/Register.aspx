<%@ Page Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Register.Register" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form runat="server">
        <div id="Required" class="custom-container d-flex justify-content-center" runat="server">
            <div>
                <asp:CustomValidator ID="UsernameValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , UsernameValidator.ErrorMessage %>" CssClass="text-danger" ValidationGroup="Required" OnServerValidate="UsernameValidator_ServerValidate" runat="server"></asp:CustomValidator>
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
                <asp:CustomValidator ID="FirstNameLastNameValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , FirstNameLastNameValidator.ErrorMessage %>" CssClass="text-danger" ValidationGroup="Required" OnServerValidate="FirstNameLastNameValidator_ServerValidate" runat="server"></asp:CustomValidator>
                <div class="form-row">
                    <div class="input-group col">
                        <div class="input-group-prepend">
                            <span class="input-group-text">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                    <path fill-rule="evenodd" d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5z" />
                                </svg>
                            </span>
                        </div>
                        <asp:TextBox ID="FirstNameTextBox" MaxLength="16" CssClass="form-control" placeholder="<%$ Resources: , FirstNameTextBox.Placeholder %>" runat="server"></asp:TextBox>
                    </div>
                    <div class="input-group col">
                        <div class="input-group-prepend">
                            <span class="input-group-text">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                    <path fill-rule="evenodd" d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5z" />
                                </svg>
                            </span>
                        </div>
                        <asp:TextBox ID="LastNameTextBox" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , LastNameTextBox.Placeholder %>" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <div class="form-row">
                        <div class="col">
                            <asp:Label ID="LanguageLabel" Font-Size="Smaller" Text="<%$ Resources: , LanguageLabel.Text %>" CssClass="mt-1" runat="server"></asp:Label>
                            <asp:DropDownList ID="LanguageDropDownList" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="LanguageDropDownList_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label ID="CountryLabel" Font-Size="Smaller" Text="<%$ Resources: , CountryLabel.Text %>" CssClass="mt-1" runat="server"></asp:Label>
                            <asp:DropDownList ID="CountryDropDownList" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <asp:CustomValidator ID="EmailValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , EmailValidator.ErrorMessage %>" CssClass="text-danger" ValidationGroup="Required" OnServerValidate="EmailValidator_ServerValidate" runat="server"></asp:CustomValidator>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-envelope-fill" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M.05 3.555A2 2 0 0 1 2 2h12a2 2 0 0 1 1.95 1.555L8 8.414.05 3.555zM0 4.697v7.104l5.803-3.558L0 4.697zM6.761 8.83l-6.57 4.027A2 2 0 0 0 2 14h12a2 2 0 0 0 1.808-1.144l-6.57-4.027L8 9.586l-1.239-.757zm3.436-.586L16 11.801V4.697l-5.803 3.546z" />
                            </svg>
                        </span>
                    </div>
                    <asp:TextBox ID="EmailTextBox" TextMode="Email" MaxLength="40" CssClass="form-control" placeholder="<%$ Resources: , EmailTextBox.Placeholder %>" runat="server"></asp:TextBox>
                </div>
                <asp:CustomValidator ID="PasswordValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , PasswordValidator.ErrorMessage %>" CssClass="text-danger" ValidationGroup="Required" OnServerValidate="PasswordValidator_ServerValidate" runat="server"></asp:CustomValidator>
                <div class="form-row">
                    <div class="input-group col">
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
                    <div class="input-group col">
                        <div class="input-group-prepend">
                            <span class="input-group-text">
                                <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-lock-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M2.5 9a2 2 0 0 1 2-2h7a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2h-7a2 2 0 0 1-2-2V9z" />
                                    <path fill-rule="evenodd" d="M4.5 4a3.5 3.5 0 1 1 7 0v3h-1V4a2.5 2.5 0 0 0-5 0v3h-1V4z" />
                                </svg>
                            </span>
                        </div>
                        <asp:TextBox ID="PasswordConfirmTextBox" TextMode="Password" MaxLength="24" CssClass="form-control" placeholder="<%$ Resources: , PasswordConfirmTextBox.Placeholder %>" runat="server"></asp:TextBox>
                    </div>
                </div>
               <asp:Button ID="RegisterButton" Text="<%$ Resources: , RegisterButton.Text %>" CssClass="btn btn-secondary btn-block mt-4" OnClick="RegisterButton_Click" runat="server" />
               <div class="d-flex justify-content-center mt-4">
                    <div>
                        <asp:Label ID="LoginLabel" Text="<%$ Resources: , LoginLabel.Text %>" runat="server"></asp:Label>
                        <asp:LinkButton ID="LoginLinkButton" Text="<%$ Resources: , LoginLinkButton.Text %>" CssClass="ml-2" CausesValidation="False" OnClick="LoginLinkButton_Click" runat="server"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>