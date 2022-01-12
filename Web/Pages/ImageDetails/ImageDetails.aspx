<%@ Page Title="Image Details" Language="C#" EnableEventValidation="false" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="ImageDetails.aspx.cs" Inherits="Web.Pages.ImageDetail" %>

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
        <asp:Button ID="btnDelete" Text="<%$ Resources: , DeleteImage %>" runat="server" OnClick="DeleteImage" CssClass="btn btn-primary"/>
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
                    <asp:LinkButton ID="CommentsButton" runat="server" OnClick="CommentsLink_Click">
                        <i class="icon-comments pr-1"></i><%= image.nComments %>
                    </asp:LinkButton>
                </li>
                <li class="list-inline-item pr-2">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LikeImage">
                        <i class="icon-thumbs-up pr-1"></i><%= image.likes %>
                    </asp:LinkButton>
                </li>
                <li class="list-inline-item">
                    <asp:LinkButton ID="AuthorButton" runat="server" OnClick="AuthorLink_Click">
                        <i class="icon-location-arrow pr-1"></i><asp:Literal runat="server" Text="<%$ Resources: , Author%>" />: <%= image.username %>
                    </asp:LinkButton>
                </li>
            </ul>
            <br />
            <hr />
            <ul class="list-unstyled list-inline font-small">
                <li class="list-inline-item"><p class="font-weight-bold">EXIF:</p></li>
                <li class="list-inline-item pr-2 white-text"><asp:Literal runat="server" Text="<%$ Resources: , diaphragmAperture %>" /> <%= image.f %></li>
                <li class="list-inline-item pr-2 white-text"><asp:Literal runat="server" Text="<%$ Resources: , shutterSpeed %>" /> <%= image.t %></li>
                <li class="list-inline-item pr-2 white-text"><asp:Literal runat="server" Text="<%$ Resources: , iso %>" /> <%= image.ISO %></li>
                <li class="list-inline-item pr-2 white-text"><asp:Literal runat="server" Text="<%$ Resources: , whiteBalance %>" /> <%= image.wb %></li>
            </ul>
        </div>
        <!-- Tags -->
        <div style="margin-left: auto; margin-right: auto; text-align: center;margin-bottom:10px;padding:10px;font-weight:bold !important;">
            <asp:Label ID="TagText" runat="server">--TAGS--</asp:Label>
            <div style="border-color:lightseagreen; border-style:solid">
                <asp:Literal ID="TagsPanel" runat="server"></asp:Literal>
            </div>
        </div>
        <hr />
        <asp:Panel ID="TagsContainer" runat="server" CssClass="d-flex justify-content-center">
            <div class="align-content-center">
                <div class="col">
                    <br />
                    <div style="width: 450px">
                        <div class="row mb-2">
                            <div class="float-right align-self-center">
                                <div class="col">
                                    <asp:LinkButton ID="PreviousTagLinkButton" CausesValidation="False" OnClick="PreviousTagLinkButton_Click" runat="server">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-caret-left-fill" viewBox="0 0 16 16">
                                                <path d="M3.86 8.753l5.482 4.796c.646.566 1.658.106 1.658-.753V3.204a1 1 0 0 0-1.659-.753l-5.48 4.796a1 1 0 0 0 0 1.506z" />
                                            </svg>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col">
                                <asp:DataList ID="TagsDataList" RepeatDirection="Horizontal" ItemType="Es.Udc.DotNet.PracticaMaD.Model.ImageService.TagDTO" OnItemDataBound="TagsDataList_ItemDataBound" runat="server">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Button ID="TagButton" CommandArgument="<%# Item.TagId %>" OnClick="TagButton_Click" runat="server" />
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div class="float-left align-self-center">
                                <div class="col">
                                    <asp:LinkButton ID="NextTagLinkButton" CausesValidation="False" OnClick="NextTagLinkButton_Click" runat="server">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor" class="bi bi-caret-right-fill" viewBox="0 0 16 16">
                                                <path d="M12.14 8.753l-5.482 4.796c-.646.566-1.658.106-1.658-.753V3.204a1 1 0 0 1 1.659-.753l5.48 4.796a1 1 0 0 1 0 1.506z" />
                                            </svg>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="form-row mt-3">
                            <div class="col">
                                <asp:Button ID="UpdateImageButton" Text="<%$ Resources: , UpdateImageButton.Text %>" CssClass="btn btn-secondary btn-block" OnClick="UpdateImageButton_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col" >
                    <br />
                    <asp:CustomValidator ID="TagValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , TagValidator.ErrorMessage %>" CssClass="text-danger" ValidationGroup="Tag" OnServerValidate="TagValidator_ServerValidate" runat="server"></asp:CustomValidator>
                    <div class="form-row">
                        <div class="input-group">
                            <asp:TextBox ID="TagTextBox" MaxLength="12" CssClass="form-control" placeholder="<%$ Resources:, TagTextBox.Placeholder %>" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <asp:Button ID="AddTagButton" Text="<%$ Resources:, AddTagButton.Text %>" CssClass="btn btn-secondary btn-block mt-3" OnClick="AddTagButton_Click" runat="server" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <hr />
        <!-- End Tags -->

        <!-- Comment Form -->
        <div class="d-flex flex-column" style="padding:20px;">
            <div>
                <span style="font-style:oblique">Comments<br /></span>
            &nbsp;<asp:TextBox ID="txtComment" runat="server" CssClass="form-control "></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" Height="43px" OnClick="Button1_Click" Text="<%$ Resources: , Button1.Text %>" style="margin-top: 0px" Width="190px" class="btn btn-primary"/>
            </div>
            <!-- Comment Form -->
            <!-- Comment List -->
            <br />
            <asp:TextBox ID="editCommentText" runat="server"  Text="<%$ Resources:, Edit %>" CssClass="form-control"></asp:TextBox>
            <br />
            <br />
            <asp:Repeater ID="CommRepeater" runat="server">
                <ItemTemplate>
                    <ul class="list-unstyled list-inline font-small">
                        <li class="list-inline-item"><a href="/Pages/UserProfile/Follows/Follows.aspx?userId=<%# Eval("usrId") %>" class="white-text"> <%# Eval("loginName") %></a></li>
                        <li class="list-inline-item"><%# Eval("postDate") %></li>
                        <li class="list-inline-item"><%# Eval("message") %></li>
                        <li class="list-inline-item">
                            <asp:Button ID="btnEditComment" Visible='<%# DataBinder.Eval(Container.DataItem, "usrId").ToString() == userId.ToString() %>' Text="<%$ Resources: , EditComment %>" runat="server" CommandArgument='<%#Eval("comId")%>' OnClick="EditComment" CssClass="btn btn-primary"/>
                        </li>
                        <li class="list-inline-item">
                            <asp:Button ID="btnDeleteComment" Visible='<%# DataBinder.Eval(Container.DataItem, "usrId").ToString() == userId.ToString() %>' runat="server" Text="<%$ Resources: , DeleteComment %>" CommandArgument='<%# Eval("comId") %>' OnClick="DeleteComment"  CssClass="btn btn-primary"/>
                        </li>
                    </ul>
                    <hr />
                </ItemTemplate> 
            </asp:Repeater>
            <!-- Comment List -->
        </div>
            <br />
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
        <br />
    </div>
    <!-- Card -->
</asp:Content>