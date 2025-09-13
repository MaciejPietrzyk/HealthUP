<%@ Page Title="Kontakt" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Health_up_.Contact" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container mt-5">
            <h2 class="text-center"><i class="fas fa-envelope"></i> Skontaktuj się z nami</h2>
            <p class="text-center text-muted">Masz pytania? Wypełnij formularz, a my się z Tobą skontaktujemy!</p>

            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card shadow-lg p-4">
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-success font-weight-bold" Visible="false"></asp:Label>

                        <div class="form-group">
                            <label for="txtName"><i class="fas fa-user"></i> Imię i nazwisko</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Wpisz swoje imię i nazwisko"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Podaj swoje imię i nazwisko" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label for="txtEmail"><i class="fas fa-envelope"></i> E-mail</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Wpisz swój e-mail"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Podaj adres e-mail" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Nieprawidłowy adres e-mail" CssClass="text-danger" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"></asp:RegularExpressionValidator>
                        </div>

                        <div class="form-group">
                            <label for="txtMessage"><i class="fas fa-comment-dots"></i> Wiadomość</label>
                            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Wpisz swoją wiadomość"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage" ErrorMessage="Podaj treść wiadomości" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <asp:Button ID="btnSend" runat="server" Text="Wyślij" CssClass="btn btn-primary btn-block" OnClick="btnSend_Click" />

                        <!-- Popup -->
                        <div id="popupSuccess" class="alert alert-success mt-3 text-center" style="display: none;">
                            <i class="fas fa-check-circle"></i> Wiadomość została wysłana!
                        </div>
                    </div>
                </div>
            </div>
        </div>

    <script>
        function showSuccessPopup() {
            document.getElementById("popupSuccess").style.display = "block";
        }
    </script>
</asp:Content>
