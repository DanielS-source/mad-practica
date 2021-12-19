<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/WebMaster.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Web.Pages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <form id="filterImagesForm" method="post" runat="server" class="form-group">

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
                        <asp:RegularExpressionValidator ID="notEmptyKeywords" runat="server" ControlToValidate="keywordsInput"
                            Display="Dynamic" Text="<%$ Resources: Common, mandatoryField %>"></asp:RegularExpressionValidator>
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

                <div class="col-md-2 justify-content-around flex">
                    <div class="d-flex justify-content-center align-items-center h-100">
                        <button type="button" id="searchImagesSubmit" class="icon-search h-50 w-25" style="vertical-align: middle"></button>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <br/>
    <br/>

    <div class="container border border-primary rounded">
        <br />
        <div class="mh-100">
            <ul style="overflow-y: scroll; -webkit-overflow-scrolling: touch">
                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>
                                <li>
                    <h1>patata</h1>
                </li>

            </ul>
        </div>

    </div>


</asp:Content>
