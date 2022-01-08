<%@ Page Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Tags.Tags" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <div class="d-flex justify-content-center mt-4">

            <asp:DataList ID="TagsDataList" RepeatDirection="Horizontal" ItemType="Es.Udc.DotNet.PracticaMaD.Model.ImageService.TagDTO" OnItemDataBound="TagsDataList_ItemDataBound" runat="server">
                <ItemTemplate>
                    <div>
                        <asp:Button ID="TagButton" CommandArgument="<%# Item.TagId %>" CssClass="btn btn-primary" OnClick="TagButton_Click" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>

        <asp:Panel CssClass="row mt-3 font-weight-bold border border-light" ID="PageableLabelPanel" runat="server">
            <div class="col">
                <asp:LinkButton ID="PreviousImageLinkButton" Text="<%$ Resources: , PreviousLinkButton.Text %>" CssClass="float-right" CausesValidation="False" OnClick="PreviousImageLinkButton_Click" runat="server"></asp:LinkButton>
            </div>
            <div class="col-1 d-flex justify-content-center">
                <asp:Label ID="ImagePageTag" runat="server"></asp:Label>
            </div>
            <div class="col">
                <asp:LinkButton ID="NextImageLinkButton" Text="<%$ Resources: , NextLinkButton.Text %>" CssClass="float-left" CausesValidation="False" OnClick="NextImageLinkButton_Click" runat="server"></asp:LinkButton>
            </div>
        </asp:Panel>

        <div class="d-flex justify-content-center mt-4" >
            <asp:Label ID="InfoLabel" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources: , InfoLabel.Text %>" runat="server"></asp:Label>
            <asp:Label ID="EmptyData" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources: , EmptyData.Text %>" runat="server"></asp:Label>
            <asp:Panel ID="ImagesPanel" Height="700px" runat="server">
                <asp:DataList ID="ImagesDataList" ItemType="Es.Udc.DotNet.PracticaMaD.Model.ImageService.ImageWithTagsDto" OnItemDataBound="ImagesDataList_ItemDataBound" runat="server">
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
                              <li class="list-inline-item pr-2"><asp:HyperLink CssClass="white-text" ID="LikesLink" runat="server"></asp:HyperLink></li>
                              <li class="list-inline-item"><asp:HyperLink CssClass="white-text" ID="AutorLink" runat="server"></asp:HyperLink></li>
                              <li class="list-inline-item"><asp:HyperLink CssClass="white-text" ID="DetailsLink" runat="server"></asp:HyperLink></li>
                            </ItemTemplate>
                          </div>
                          <hr />
                          <asp:Panel ID="ImageTagsPanel" ScrollBars="Horizontal" runat="server">
                            <asp:DataList ID="ImageTagsDataList" RepeatDirection="Horizontal" OnItemDataBound="ImageTagsDataList_ItemDataBound" runat="server">
                                <ItemTemplate>
                                    <div class="ml-2 mr-2" style="margin-bottom:10px;padding:10px;font-weight:bold !important;">
                                        <asp:Label ID="Tag" runat="server"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                          </asp:Panel>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>
        </div>

</asp:Content>