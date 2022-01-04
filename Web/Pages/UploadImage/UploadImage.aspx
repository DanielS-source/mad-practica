<%@ Page Title="Post Photo" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Web.Pages.WebForm3" Trace="true" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="form" class="card">

        <h5 class="card-header info-color white-text text-center py-4">
            <strong>Post Image</strong>
        </h5>

        <div class="field mt-2" style="align-content:center">
            <span class="entry">
                <asp:TextBox ID="txtTitle" runat="server" Width="75%" Columns="16" 
                    meta:resourcekey="txtTitle" class="form-control mb-4" 
                    placeholder="<%$ Resources:Common, title %>">
                </asp:TextBox>
            </span>
        </div>

        <div class="field">
            <span class="entry">
                <asp:TextBox ID="txtDescription" runat="server" Width="75%"
                    Columns="16" meta:resourcekey="txtTitle" class="form-control mb-4" 
                    placeholder="<%$ Resources:Common, description %>">
                </asp:TextBox>
            </span>
        </div>

        <div class="field mb-4">
            <span class="label">
                <asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Common, category %>"/>
            </span>
            <asp:DropDownList ID="categoryDropDown" runat="server" Width="70%"></asp:DropDownList>
        </div>
            

        <div class="field">
            <span class="entry">
                <asp:TextBox ID="txtDiaphragmAperture" runat="server" Width="75%"
                    Columns="16" meta:resourcekey="txtTitle" class="form-control mb-4" 
                    placeholder="<%$ Resources:Common, diaphragmAperture %>">
                </asp:TextBox>
            </span>
        </div>

        <div class="field">
            <span class="entry">
                <asp:TextBox ID="txtShutterSpeed" runat="server" Width="75%"
                    Columns="16" meta:resourcekey="txtTitle" class="form-control mb-4" 
                    placeholder="<%$ Resources:Common, shutterSpeed %>">
                </asp:TextBox>
            </span>
        </div>

        <div class="field">
            <span class="entry">
                <asp:TextBox ID="txtISO" runat="server" Width="75%"
                    Columns="16" meta:resourcekey="txtTitle" class="form-control mb-4" 
                    placeholder="<%$ Resources:Common, iso %>">
                </asp:TextBox>
            </span>
        </div>

        <div class="field">
            <span class="entry">
                <asp:TextBox ID="txtWhiteBalance" runat="server" Width="75%"
                    Columns="16" meta:resourcekey="txtTitle" class="form-control mb-4" 
                    placeholder="<%$ Resources:Common, whiteBalance %>">
                </asp:TextBox>
            </span>
        </div>

        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload_Image" />

        <hr />
        <br />
        <br />
        <br />
        <!-- TAGS -->
        <div class="d-flex justify-content-center mt-4">
            <asp:Label ID="InfoLabel" Font-Size="X-Large" Font-Bold="True" Text="<%$ Resources: , InfoLabel.Text %>" runat="server"></asp:Label>
        </div>

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
                            <asp:Button ID="TagButton" CommandArgument="<%# Item.TagId %>"  OnClick="TagButton_Click" runat="server" />
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

        <!-- XXX -->
        <div class="d-flex justify-content-center">
            <div style="width: 200px">
                <asp:CustomValidator ID="TagValidator" Font-Size="Smaller" ErrorMessage="<%$ Resources: , TagValidator.ErrorMessage %>" CssClass="text-danger" ValidationGroup="Tag" OnServerValidate="TagValidator_ServerValidate" runat="server"></asp:CustomValidator>
                <div class="form-row">
                    <div class="input-group">
                        <asp:TextBox ID="TagTextBox" MaxLength="12" CssClass="form-control" placeholder="<%$ Resources:, TagTextBox.Placeholder %>" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <asp:Button ID="AddTagButton" Width="200px" Text="<%$ Resources:, AddTagButton.Text %>" CssClass="btn btn-secondary btn-block mt-3" OnClick="AddTagButton_Click" runat="server" />
                </div>
            </div>
        </div>
        <!-- END TAGS -->

        <hr />
        <asp:Image ID="Image1" runat="server" Height = "100" Width = "100" />


        <div class="button">
            <asp:Button ID="btnCreate" class="btn purple-gradient btn-rounded" Text="Upload" runat="server" meta:resourcekey="btnCreate" OnClick="PostImage" />
        </div>
    </div>
</asp:Content>
