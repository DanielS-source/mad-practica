<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/UserProfile/UserProfile.Master" AutoEventWireup="true" CodeBehind="Followers.aspx.cs" Inherits="Web.Pages.UserProfile.Followers.Followers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <br />
    <br />
    <div id="Required" class="custom-container d-flex justify-content-center" runat="server">
        <div class="row justify-content-around">
            <div class="col">
                <div class="d-flex justify-content-center">
                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-person-fill" viewBox="0 0 16 16">
                        <path fill-rule="evenodd" d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" />
                    </svg>
                </div>
            </div>
            <div class="col">
                <asp:Label CssClass="h1" ID="lblTitle" runat="server" meta:resourcekey="title"></asp:Label>
            </div>
            <div id="followersContainer" class="custom-container d-flex justify-content-center" runat="server">
                <% foreach (var userProfile in followers) { %>
                    <div class="form-row">
                        <div class="input-group-prepend">
                            <span class="input-group-text bg-info">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                    <path fill-rule="evenodd" d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5z" />
                                </svg>
                                <span><%= userProfile.FirstName%></span>
                            </span>
                        </div>
                    </div>
                <% } %>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="custom-container d-flex justify-content-center" >
        <div class="form-row">
            <div class="col col-6">
                <asp:LinkButton ID="previousBtn" runat="server" CssClass="btn btn-secondary" OnClick="previousBtn_Click" Text="<%$Resources : , previousBtn.Text %>" CausesValidation="false"></asp:LinkButton>
            </div>
            <div class="col col-6">
                <asp:LinkButton ID="nextBtn" runat="server" CssClass="btn btn-secondary" OnClick="nextBtn_Click" Text ="<%$Resources : , nextBtn.Text %>" CausesValidation="false"></asp:LinkButton>
            </div>
        </div>
    </div>


</asp:Content>
