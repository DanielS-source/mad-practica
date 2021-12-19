<%@ Page Title="Post Photo" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Web.Pages.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="form">
        <form id="PostImage" method="post" runat="server">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload_Image" />
            <hr />
            <asp:Image ID="Image1" runat="server" Height = "100" Width = "100" />
        </form>
    </div>

</asp:Content>
