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

        <div class="button">
            <asp:Button ID="btnCreate" class="btn purple-gradient btn-rounded" Text="Upload" runat="server" meta:resourcekey="btnCreate" OnClick="PostImage" />
        </div>
    </div>
</asp:Content>
