<%@ Page Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Tags.Tags" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form runat="server">
        <div class="d-flex justify-content-center mt-4">

            <asp:DataList ID="TagsDataList" RepeatDirection="Horizontal" ItemType="Es.Udc.DotNet.PracticaMaD.Model.ImageService.TagDTO" OnItemDataBound="TagsDataList_ItemDataBound" runat="server">
                <ItemTemplate>
                    <div>
                        <asp:Button ID="TagButton" CommandArgument="<%# Item.TagId %>" CssClass="btn btn-primary" OnClick="TagButton_Click" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="d-flex justify-content-center mt-4">
            <asp:Label ID="InfoLabel" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources: , InfoLabel.Text %>" runat="server"></asp:Label>
            <asp:Label ID="EmptyData" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources: , EmptyData.Text %>" runat="server"></asp:Label>
            <asp:Panel ID="ImagesPanel" Height="440px" runat="server">
                <asp:DataList ID="ImagesDataList" ItemType="Es.Udc.DotNet.PracticaMaD.Model.ImageService.ImageWithTagsDto" OnItemDataBound="ImagesDataList_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div style="width: 500px">
                            <div class="form-row">
                                <div class="input-group col">
                                    <asp:Label ID="LoginLabel" Text="Usuario" runat="server"></asp:Label>
                                </div>
                                <div class="float-right">
                                    <div class="input-group col ">
                                        <asp:Label ID="DateLabel" Text="Fecha" CssClass=" font-weight-light text-muted" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <asp:TextBox ID="TextTextBox" Width="500px" Height="105px" MaxLength="280" TextMode="MultiLine" Enabled="false" Style="resize: none;" runat="server"></asp:TextBox>
                            <asp:Panel ID="ImageTagsPanel" ScrollBars="Horizontal" runat="server">
                                <asp:DataList ID="ImageTagsDataList" RepeatDirection="Horizontal" OnItemDataBound="ImageTagsDataList_ItemDataBound" runat="server">
                                    <ItemTemplate>
                                        <div class="ml-2 mr-2">
                                            <asp:Label ID="Label" runat="server"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </asp:Panel>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>
        </div>
        <div class="row mt-3">
            <div class="col">
                <asp:LinkButton ID="PreviousImageLinkButton" Text="<%$ Resources: , PreviousLinkButton.Text %>" CssClass="float-right" CausesValidation="False" OnClick="PreviousImageLinkButton_Click" runat="server"></asp:LinkButton>
            </div>
            <div class="col-1 d-flex justify-content-center">
                <asp:Label ID="ImagePageTag" runat="server"></asp:Label>
            </div>
            <div class="col">
                <asp:LinkButton ID="NextImageLinkButton" Text="<%$ Resources: , NextLinkButton.Text %>" CssClass="float-left" CausesValidation="False" OnClick="NextImageLinkButton_Click" runat="server"></asp:LinkButton>
            </div>
        </div>
    </form>
</asp:Content>