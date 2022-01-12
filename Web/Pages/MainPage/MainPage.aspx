<%@ Page Title=""  Async="true" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Web.Pages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <br />
        <div class="container border border-succes rounded" style="padding:10px;">
            <div class="row justify-content-around">
                <div class="col-md-5 justify-content-around">
                    <div class="offset-md-3">
                        <h4>
                            <asp:Localize ID="keywordsSpan" runat="server" Text="<%$ Resources:Common, keywords %>" />
                        </h4>
                        <%--  --%>
                        <div class="w-100"></div>
                        <br />

                        <asp:TextBox ID="keywordsInput" runat="server" Width="75%" Height="90%"  CssClass="form-control"></asp:TextBox>
                    </div>
                    

                </div>

                <div class="col-md-5 justify-content-around">
                    <div class="offset-md-3">
                        <h4>
                            <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Common, category %>" />                         
                        </h4>

                        <div class="w-100"></div>
                        <br />

                        <asp:DropDownList ID="categoryDropDown" runat="server" Width="75%" AppendDataBoundItems="true" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-12 text-center">
                    <asp:Button ID="btnCreate" Text="Search" runat="server" meta:resourcekey="btnCreate" OnClick="SearchImages" CssClass="btn btn-secondary btn-lg"/>
                </div>
            </div>
        </div>
    <br/>

    <div class="d-flex justify-content-center mt-4" >
        <asp:DataList ID="ImagesDataList" ItemType="Es.Udc.DotNet.PracticaMaD.Model.ImageService.SearchImageDTO" OnItemDataBound="ImagesDataList_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <!-- Card -->
                        <div class="card">
                          <!-- Card image -->
                          <div class="view overlay">
                              <asp:Image ID="ImageImagen" CssClass="card-img" runat="server" AlternateText="ImagenTag" />
                          </div>
                          <!-- Card content -->
                          <div class="card-body">
                            <!-- Title -->
                            <h4 CssClass="card-title" id="ImageTitle" runat="server"></h4>
                            <hr>
                              <!-- Text -->
                            <asp:Label CssClass="card-text" ID="ImageDescription" runat="server"></asp:Label>
                          </div>
                          <!-- Card footer -->
                          <div class="rounded-bottom mdb-color lighten-3 text-center pt-3">
                            <ItemTemplate class="list-unstyled list-inline font-small">
                              <i class="icon-calendar pr-1"></i>
                              <li class="list-inline-item pr-2 white-text"  ID="DateLabel" runat="server"></li>
                              <li class="list-inline-item pr-2"><asp:HyperLink CssClass="white-text" ID="CommentsLink" runat="server"></asp:HyperLink></li>
                              <li class="list-inline-item pr-2"><asp:HyperLink CssClass="white-text" ID="LikesLink" runat="server"></asp:HyperLink></li>
                              <li class="list-inline-item"><asp:HyperLink CssClass="white-text" ID="AutorLink" runat="server"></asp:HyperLink></li>
                              <li class="list-inline-item"><asp:HyperLink CssClass="white-text" ID="DetailsLink" runat="server"></asp:HyperLink></li>
                            </ItemTemplate>
                          </div>
                          <hr />
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:DataList>
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