<%@ Page Title="Post Photo" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Web.Pages.WebForm3" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="form">
        <form id="PostImage" method="post" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTitle" runat="server" Text="<%$ Resources:Common, title %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtTitle" runat="server" Width="100px"
                        Columns="16" meta:resourcekey="txtTitle">
                    </asp:TextBox>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclDescription" runat="server" Text="<%$ Resources:Common, description %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtDescription" runat="server" Width="100px"
                        Columns="16" meta:resourcekey="txtTitle">
                    </asp:TextBox>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Common, category %>" />
                </span>
                <asp:DropDownList ID="categoryDropDown" runat="server" Width="75%"></asp:DropDownList>
            </div>
            

            <div class="field">
                <span class="label">
                    <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Common, diaphragmAperture %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtDiaphragmAperture" runat="server" Width="100px"
                        Columns="16" meta:resourcekey="txtTitle">
                    </asp:TextBox>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Common, shutterSpeed %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtShutterSpeed" runat="server" Width="100px"
                        Columns="16" meta:resourcekey="txtTitle">
                    </asp:TextBox>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Common, iso %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtISO" runat="server" Width="100px"
                        Columns="16" meta:resourcekey="txtTitle">
                    </asp:TextBox>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Common, whiteBalance %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtWhiteBalance" runat="server" Width="100px"
                        Columns="16" meta:resourcekey="txtTitle">
                    </asp:TextBox>
                </span>
            </div>

            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload_Image" />
            <hr />
            <asp:Image ID="Image1" runat="server" Height = "100" Width = "100" />

            <div class="button">
                <asp:Button ID="btnCreate" runat="server" meta:resourcekey="btnCreate" OnClick="BtnCreateClick" />
            </div>

        </form>
    </div>

</asp:Content>
