<%@ Page Title="Image Details" Language="C#"  MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="ImageDetails.aspx.cs" Inherits="Web.Pages.ImageDetail" %>

<asp:Content ID="ImgDetails" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                <li class="list-inline-item pr-2 white-text">
                    <i class="icon-calendar pr-1"></i><%= image.dateImg %>
                </li>
                <li class="list-inline-item pr-2">
                    <a href="#" class="white-text">
                        <i class="icon-comments pr-1"></i>In progress
                    </a>
                </li>
                <li class="list-inline-item pr-2">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LikeImage">
                        <i class="icon-thumbs-up pr-1"></i><%= image.likes %>
                    </asp:LinkButton>
                </li>
                <li class="list-inline-item">
                    <a href="/Pages/UserProfile/Account/Account.aspx?userId=<%= image.usrId %>"" class="white-text">
                        <i class="icon-location-arrow pr-1"> </i>Author: <%= image.username %>
                    </a>
                </li>
            </ul>
            <ul class="list-unstyled list-inline font-small">
                <li class="list-inline-item"><a href="#" class="white-text">EXIF</a></li>
                <li class="list-inline-item pr-2 white-text"><%= image.f %></li>
                <li class="list-inline-item pr-2 white-text"><%= image.t %></li>
                <li class="list-inline-item pr-2 white-text"><%= image.ISO %></li>
                <li class="list-inline-item pr-2 white-text"><%= image.wb %></li>
            </ul>
        </div>
        <!-- Comment Form -->
        <div>
            <span>Comments<br />
            </span>
        &nbsp;<asp:TextBox ID="txtComment" runat="server" Height="46px" Width="929px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Height="43px" OnClick="Button1_Click" style="margin-top: 0px" Text="Button" Width="190px" />
        </div>
        <!-- Comment Form -->
        <!-- Comment List -->
        <% foreach (var comment in comments.CommentList) { %>
            <span><a href="/Pages/UserProfile/Account/Account.aspx?userId=<%= comment.usrId %>" class="white-text"> <%= comment.loginName %></a></span>
            <span> <%= comment.message %></span>
            <span> <%= comment.postDate %></span>
        <% } %>
        <!-- Comment Lits -->
    </div>
    <!-- Card -->
</asp:Content>