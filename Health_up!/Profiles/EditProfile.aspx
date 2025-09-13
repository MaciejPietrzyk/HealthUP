<%@ Page Title="Edytuj profil" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Health_up_.EditProfile" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-user-edit"></i> Edytuj profil</h2>
        <p class="text-center text-muted">Zmień swoje dane osobowe</p>

        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg p-4">
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-success font-weight-bold" Visible="false"></asp:Label>

                    <div class="form-group">
                        <label for="txtFirstName"><i class="fas fa-user"></i> Imię</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Podaj imię" />
                    </div>

                    <div class="form-group">
                        <label for="txtLastName"><i class="fas fa-user"></i> Nazwisko</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Podaj nazwisko" />
                    </div>

                    <div class="form-group">
                        <label for="ddlGender"><i class="fas fa-venus-mars"></i> Płeć</label>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                           <asp:ListItem Value="Mężczyzna" Text="Mężczyzna"></asp:ListItem>
                           <asp:ListItem Value="Kobieta" Text="Kobieta"></asp:ListItem>
                           <asp:ListItem Value="Inne" Text="Inne"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="ddlRegion"><i class="fas fa-map-marker-alt"></i> Województwo</label>
                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="ddlCity"><i class="fas fa-city"></i> Miasto</label>
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <asp:Button ID="btnSave" runat="server" Text="Zapisz zmiany" CssClass="btn btn-success btn-block" OnClick="btnSave_Click" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
