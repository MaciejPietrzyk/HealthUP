<%@ Page Title="Rejestracja" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Health_up_.Register" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-user-plus"></i> Rejestracja</h2>
        <p class="text-center text-muted">Zarejestruj się, aby stworzyć konto</p>

        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg p-4">
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger font-weight-bold" Visible="false"></asp:Label>

                    <div class="form-group">
                        <label for="txtUsername"><i class="fas fa-user"></i> Nazwa Użytkownika</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Podaj swoją nazwę użytkownika" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="Podaj nazwę użytkownika" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtFirstName"><i class="fas fa-user"></i> Imię</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Wpisz swoje imię" />
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Podaj imię" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtLastName"><i class="fas fa-user"></i> Nazwisko</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Wpisz swoje nazwisko" />
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Podaj nazwisko" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtEmail"><i class="fas fa-envelope"></i> Adres e-mail</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Wpisz swój adres e-mail" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Podaj adres e-mail" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Nieprawidłowy adres e-mail" CssClass="text-danger" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"></asp:RegularExpressionValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtPassword"><i class="fas fa-lock"></i> Hasło</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Wpisz swoje hasło" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Podaj hasło" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtConfirmPassword"><i class="fas fa-lock"></i> Potwierdź hasło</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Potwierdź swoje hasło" />
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Potwierdź hasło" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Hasła nie pasują" CssClass="text-danger" Display="Dynamic"></asp:CompareValidator>
                    </div>

                    <div class="form-group">
                        <label for="ddlGender"><i class="fas fa-venus-mars"></i> Płeć</label>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Mezczyzna" Text="Mężczyzna"></asp:ListItem>
                            <asp:ListItem Value="Kobieta" Text="Kobieta"></asp:ListItem>
                            <asp:ListItem Value="Inne" Text="Inna"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

           

                    <asp:Button ID="btnRegister" runat="server" Text="Zarejestruj się" CssClass="btn btn-primary btn-block" OnClick="btnRegister_Click" />

                    <p class="text-center mt-3">Masz już konto? <a href="Login.aspx">Zaloguj się</a></p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
