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
                <li class="list-inline-item pr-2 white-text"><i class="icon-calendar pr-1"></i><%= image.dateImg %></li>
                <li class="list-inline-item pr-2"><a href="#" class="white-text"><i class="icon-comments pr-1"></i>In progress</a></li>
                <li class="list-inline-item pr-2"><a href="#" class="white-text"><i class="icon-thumbs-up pr-1"></i><%= image.likes %></a></li>
                <li class="list-inline-item"><a href="#" class="white-text"><i class="icon-location-arrow pr-1"> </i>Author: <%= image.username %></a></li>
                <li class="list-inline-item"><a href="#" class="white-text"><i class="icon-ellipsis-horizontal pr-1"> </i>See More</a></li>
            </ul>
            <ul class="list-unstyled list-inline font-small">
                <li class="list-inline-item"><a href="#" class="white-text">EXIF</a></li>
                <li class="list-inline-item pr-2 white-text"><%= image.f %></li>
                <li class="list-inline-item pr-2 white-text"><%= image.t %></li>
                <li class="list-inline-item pr-2 white-text"><%= image.ISO %></li>
                <li class="list-inline-item pr-2 white-text"><%= image.wb %></li>
            </ul>
        </div>
        <div>
            <span>Comments</span>
        </div>
    </div>
    <!-- Card -->
</asp:Content>