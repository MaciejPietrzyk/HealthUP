<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="Calculator.aspx.cs" Inherits="Health_up_.Calculator" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-calculator"></i> Kalkulator Zapotrzebowania Kalorycznego</h2>
        <p class="text-center text-muted">Oblicz swoje dzienne zapotrzebowanie kaloryczne.</p>

        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg p-4">
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger font-weight-bold" Visible="false"></asp:Label>

                    <div class="form-group">
                        <label>Waga (kg):</label>
                        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <label>Wzrost (cm):</label>
                        <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <label>Wiek:</label>
                        <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <label>Poziom aktywności:</label>
                        <asp:DropDownList ID="ddlActivityLevel" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Brak aktywności" Value="1.2"></asp:ListItem>
                            <asp:ListItem Text="Lekka aktywność" Value="1.375"></asp:ListItem>
                            <asp:ListItem Text="Średnia aktywność" Value="1.55"></asp:ListItem>
                            <asp:ListItem Text="Duża aktywność" Value="1.725"></asp:ListItem>
                            <asp:ListItem Text="Bardzo duża aktywność" Value="1.9"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:Button ID="btnCalculate" runat="server" Text="Oblicz" CssClass="btn btn-primary btn-block" OnClick="btnCalculate_Click" />

                    <div id="resultDiv" runat="server" visible="false" class="mt-3 text-center">
                        <h4>Twoje dzienne zapotrzebowanie kaloryczne wynosi:</h4>
                        <h3><asp:Label ID="lblCalories" runat="server" CssClass="font-weight-bold text-success"></asp:Label> kcal</h3>

                        <asp:Button ID="btnSaveToProfile" runat="server" Text="Zapisz do profilu" CssClass="btn btn-success mt-2" OnClick="btnSaveToProfile_Click" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
