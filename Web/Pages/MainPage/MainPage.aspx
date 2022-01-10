<%@ Page Title=""  Async="true" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Web.Pages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

        <div class="container border border-primary rounded">
            <div class="row justify-content-around">
                <div class="col-md-5 justify-content-around">
                    <div class="offset-md-3">
                        <h4>
                            <asp:Localize ID="keywordsSpan" runat="server" Text="<%$ Resources:Common, keywords %>" />
                        </h4>
                        <%--  --%>
                        <div class="w-100"></div>
                        <br />

                        <asp:TextBox ID="keywordsInput" runat="server" Width="75%" Height="90%" CssClass=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="keywordsInput"></asp:RequiredFieldValidator>
                    </div>
                    

                </div>

                <div class="col-md-5 justify-content-around">
                    <div class="offset-md-3">
                        <h4>
                            <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Common, category %>" />                         
                        </h4>

                        <div class="w-100"></div>
                        <br />

                        <asp:DropDownList ID="categoryDropDown" runat="server" Width="75%" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="button">
                    <asp:Button ID="btnCreate" Text="Search" runat="server" meta:resourcekey="btnCreate" OnClick="SearchImages" />
                </div>
            </div>
        </div>
    <br/>

    <div class="row row-cols-1 row-cols-md-2">
        <% foreach (var image in images) { %>
            <!-- Card -->
            <div class="card">
              <!-- Card image -->
              <div class="view overlay">
                <img class="card-img-top" src="<%= image.imageSrc %>" alt="Image">
              </div>
              <!-- Card content -->
              <div class="card-body">
                <!-- Title -->
                <h4 class="card-title"><%= image.title %></h4>
                <hr>
                <!-- Text -->
                <p class="card-text"><%= image.description %></p>
              </div>
              <!-- Card footer -->
              <div class="rounded-bottom mdb-color lighten-3 text-center pt-3">
                <ul class="list-unstyled list-inline font-small">
                  <li class="list-inline-item pr-2 white-text"><i class="icon-calendar pr-1"></i><%= image.dateImg %></li>
                  <li class="list-inline-item pr-2"><a href="#" class="white-text"><i class="icon-comments pr-1"></i>In progress</a></li>
                  <li class="list-inline-item pr-2"><a href="#" class="white-text"><i class="icon-thumbs-up pr-1"></i><%= image.likes %></a></li>
                  <li class="list-inline-item"><a href="/Pages/UserProfile/Follows/Follows.aspx?userId=<%= image.usrId %>" class="white-text"><i class="icon-location-arrow pr-1"> </i>Author: <%= image.username %></a></li>
                  <li class="list-inline-item"><a href="/Pages/ImageDetails/ImageDetails.aspx?Image=<%= image.imgId %>" class="white-text"><i class="icon-ellipsis-horizontal pr-1"> </i>See More</a></li>
                </ul>
              </div>
            </div>
            <!-- Card -->
        <% } %>
    </div>
    <div class="custom-container d-flex justify-content-center" >
        <div class="form-row">
            <div class="col col-6">
                <asp:LinkButton ID="previousBtn" runat="server" CssClass="btn btn-secondary" OnClick="previousBtn_Click" CausesValidation="false">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-caret-left-fill" viewBox="0 0 16 16">
                        <path d="M3.86 8.753l5.482 4.796c.646.566 1.658.106 1.658-.753V3.204a1 1 0 0 0-1.659-.753l-5.48 4.796a1 1 0 0 0 0 1.506z" />
                    </svg>
                </asp:LinkButton>
            </div>
            <div class="col col-6">
                <asp:LinkButton ID="nextBtn" runat="server" CssClass="btn btn-secondary" OnClick="nextBtn_Click" CausesValidation="false">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-caret-right-fill" viewBox="0 0 16 16">
                        <path d="M12.14 8.753l-5.482 4.796c-.646.566-1.658.106-1.658-.753V3.204a1 1 0 0 1 1.659-.753l5.48 4.796a1 1 0 0 1 0 1.506z" />
                    </svg>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>