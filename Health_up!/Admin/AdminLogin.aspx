<%@ Page Title="Admin Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="Health_up_.AdminLogin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-user-shield"></i> Logowanie dla administratora</h2>
        <p class="text-center text-muted">Zaloguj się, aby uzyskać dostęp do panelu administratora</p>

        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg p-4">
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger font-weight-bold" Visible="false"></asp:Label>

                    <div class="form-group">
                        <label for="txtAdminUsername"><i class="fas fa-user"></i> Nazwa użytkownika</label>
                        <asp:TextBox ID="txtAdminUsername" runat="server" CssClass="form-control" placeholder="Wpisz nazwę użytkownika" />
                        <asp:RequiredFieldValidator ID="rfvAdminUsername" runat="server" ControlToValidate="txtAdminUsername" ErrorMessage="Podaj nazwę użytkownika" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtAdminPassword"><i class="fas fa-lock"></i> Hasło</label>
                        <asp:TextBox ID="txtAdminPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Wpisz swoje hasło" />
                        <asp:RequiredFieldValidator ID="rfvAdminPassword" runat="server" ControlToValidate="txtAdminPassword" ErrorMessage="Podaj hasło" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <asp:Button ID="btnAdminLogin" runat="server" Text="Zaloguj się" CssClass="btn btn-primary btn-block" OnClick="btnAdminLogin_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
