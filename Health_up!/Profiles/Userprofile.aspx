<%@ Page Title="Profil użytkownika" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Health_up_.UserProfile" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-user"></i> Profil użytkownika</h2>

        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg p-4">
                    <p class="text-center text-muted">Podstawowe informacje o Twoim koncie</p>

                    <div class="form-group">
                        <label><i class="fas fa-user"></i> Imię</label>
                        <asp:Label ID="lblFirstName" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-user"></i> Nazwisko</label>
                        <asp:Label ID="lblLastName" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-envelope"></i> Adres e-mail</label>
                        <asp:Label ID="lblEmail" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-venus-mars"></i> Płeć</label>
                        <asp:Label ID="lblGender" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-map-marker-alt"></i> Województwo</label>
                        <asp:Label ID="lblRegion" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-city"></i> Miasto</label>
                        <asp:Label ID="lblCity" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                    <div class="text-center">
                        <a href="EditProfile.aspx" class="btn btn-primary">Edytuj profil</a>
                    </div>
                </div>

                <!-- Sekcja zapotrzebowania kalorycznego -->
                <div class="card shadow-lg p-4 mt-4">
                    <h4 class="text-center"><i class="fas fa-utensils"></i> Zapotrzebowanie kaloryczne</h4>

                    <asp:Label ID="lblCalories" runat="server" CssClass="form-control text-center font-weight-bold"></asp:Label>

                    <div class="text-center mt-3">
                        <asp:Button ID="btnCalculateCalories" runat="server" Text="Oblicz zapotrzebowanie" CssClass="btn btn-success" OnClick="btnCalculateCalories_Click" Visible="false" />
                        <div class="text-center mt-3">
                    <asp:HyperLink ID="lnkCaloriesChart" runat="server" 
                         CssClass="btn btn-info" 
                         NavigateUrl="~/Features/CaloriesChart.aspx">
                               Wykres zapotrzebowania kalorycznego
                         </asp:HyperLink>
</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

