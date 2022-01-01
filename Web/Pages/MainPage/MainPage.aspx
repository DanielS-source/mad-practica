<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Web.Pages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

        <div class="container border border-primary rounded">
            <div class="row justify-content-around">
                <div class="col-md-5 justify-content-around">
                    <div class="offset-md-3">
                        <h4>
                            <asp:Localize ID="keywordsSpan" runat="server" Text="<%$ Resources:Common, keywords %>" />
                        </h4>

                        <div class="w-100"></div>
                        <br />

                        <asp:TextBox ID="keywordsInput" runat="server" Width="75%" Height="90%" CssClass=""></asp:TextBox>
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

                <div class="col-md-2 justify-content-around flex">
                    <div class="d-flex justify-content-center align-items-center h-100">
                        <button type="button" id="searchImagesSubmit" class="icon-search h-50 w-25" style="vertical-align: middle" runat="server" OnClick="SearchImages"/>
                    </div>
                </div>
            </div>
        </div>
    <br/>
    <br/>

    <div class="container border border-primary rounded">
        <br />
        <% foreach (var image in images) { %>
            <div class="container border border-primary rounded">
                <span><%= image.username %></span>

                <span><%= image.title %></span>
                <span><%= image.description %></span>

                <img id="img" runat="server" src="<%= image.imageSrc %>"/>

                <span> image.category</span> 
                <span><%= image.dateImg %></span>

                <span><%= image.f %></span>
                <span><%= image.t %></span>
                <span><%= image.ISO %></span>
                <span><%= image.wb %></span>

                <span><%= image.likes %></span>
                <span>Comments</span>
            </div>
        <% } %>

    </div>


</asp:Content>
