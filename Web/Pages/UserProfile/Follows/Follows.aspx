<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/UserProfile/UserProfile.Master" AutoEventWireup="true" CodeBehind="Follows.aspx.cs" Inherits="Web.Pages.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     <div id="Required" class="custom-container d-flex justify-content-center" runat="server">
         <div>
             <br />
             <br />
             <div class="row justify-content-around">
                <div class="d-flex justify-content-center">
                    <svg xmlns="http://www.w3.org/2000/svg" width="100" height="100" fill="currentColor" class="bi bi-person-bounding-box" viewBox="0 0 16 16">
                        <path fill-rule="evenodd" d="M1.5 1a.5.5 0 0 0-.5.5v3a.5.5 0 0 1-1 0v-3A1.5 1.5 0 0 1 1.5 0h3a.5.5 0 0 1 0 1h-3zM11 .5a.5.5 0 0 1 .5-.5h3A1.5 1.5 0 0 1 16 1.5v3a.5.5 0 0 1-1 0v-3a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 1-.5-.5zM.5 11a.5.5 0 0 1 .5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 1 0 1h-3A1.5 1.5 0 0 1 0 14.5v-3a.5.5 0 0 1 .5-.5zm15 0a.5.5 0 0 1 .5.5v3a1.5 1.5 0 0 1-1.5 1.5h-3a.5.5 0 0 1 0-1h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 1 .5-.5z" />
                        <path fill-rule="evenodd" d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" />
                    </svg>
                </div>
             </div>
             <br />
             <div class="form-row">
                 <div id="userContainer" class="col align-self-center" runat="server">
                <asp:TableCell ID="cellAccountID" runat="server"></asp:TableCell>
                 </div>   
             </div>
             <br />
             <div class="form-row justify-content-around">
                 <div class="col col-6">
                    <div class="input-group-prepend">
                        <span class="input-group-text bg-info">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5z" />
                            </svg>
                            <asp:LinkButton ID="btnFollowers" CssClass="btn btn-info btn-sm" runat="server" CausesValidation="false" OnClick="btnFollowers_Click" Text="<%$Resources : , btnFollowers.Text %>"></asp:LinkButton>
                        </span>
                    </div>
                 </div>
                 <div class="col col-6">
                    <div class="input-group-prepend">
                        <span class="input-group-text bg-info">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5z" />
                            </svg>
                            <asp:LinkButton ID="btnFollowed" CssClass="btn btn-info btn-sm" runat="server" CausesValidation="false" OnClick="btnFollowed_Click" Text="<%$Resources : , btnFollowed.Text %>"></asp:LinkButton>
                        </span>
                    </div>
                 </div>
             </div>
             <br />
             <div class="form-row justify-content-around">
                 <div id="followDiv" class="col col-6" runat="server">
                    <div class="input-group-prepend">
                        <span id="backgroundSpan" class="input-group-text bg-info" runat="server">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5z" />
                            </svg>
                            <asp:LinkButton ID="followBtn" CssClass="btn btn-success" runat="server" CausesValidation="false" OnClick="followingBtn_Click" Text="<%$Resources: , follow.Text %>"></asp:LinkButton>
                            <asp:LinkButton ID="followingBtn" CssClass="btn btn-secondary" runat="server" CausesValidation="false" Text="<%$Resources: , followingBtn.Text %>"></asp:LinkButton>
                        </span>
                    </div>
                </div> 
             </div>
         </div>
     </div>
    <br />
    <hr />
    <div class="row d-flex justify-content-center">
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

</asp:Content>
