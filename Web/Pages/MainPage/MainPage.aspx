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

                        <asp:DropDownList ID="categoryDropDown" runat="server" Width="75%"></asp:DropDownList>
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
</asp:Content>