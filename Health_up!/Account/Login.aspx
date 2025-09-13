<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Health_up_.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Strona logowania -->
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-sign-in-alt"></i> Logowanie</h2>
        <p class="text-center text-muted">Zaloguj się, aby uzyskać dostęp do swojego konta</p>

        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg p-4">
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger font-weight-bold" Visible="false"></asp:Label>

                    <div class="form-group">
                        <label for="txtUsername"><i class="fas fa-user"></i> Nazwa użytkownika</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Wpisz nazwę użytkownika" />
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Podaj nazwę użytkownika" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtPassword"><i class="fas fa-lock"></i> Hasło</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Wpisz swoje hasło" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Podaj hasło" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <asp:Button ID="btnLogin" runat="server" Text="Zaloguj się" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                    
                    <p class="text-center mt-3">Nie masz konta? <a href="register.aspx">Zarejestruj się</a></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>