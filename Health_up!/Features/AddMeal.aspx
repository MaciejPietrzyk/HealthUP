<%@ Page Title="Dodaj posiłek" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AddMeal.aspx.cs" Inherits="Health_up_.AddMeal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-plus-circle"></i> Dodaj posiłek</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger font-weight-bold" Visible="false"></asp:Label>

        <!-- Nazwa posiłku -->
        <div class="form-group">
            <label for="txtMealName">Nazwa posiłku</label>
            <asp:TextBox ID="txtMealName" runat="server" CssClass="form-control" placeholder="np. Śniadanie" />
        </div>

        <!-- Data posiłku -->
        <div class="form-group">
            <label for="txtMealDate">Data</label>
            <asp:TextBox ID="txtMealDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <hr />

        <!-- Dodawanie produktów -->
        <h4>Dodaj produkty</h4>
        <div class="form-inline mb-3">
            <asp:DropDownList ID="ddlProducts" runat="server" CssClass="form-control mr-2"></asp:DropDownList>
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control mr-2" placeholder="Ilość (g)" Width="120px" />
            <asp:Button ID="btnAddProduct" runat="server" Text="Dodaj" CssClass="btn btn-primary" OnClick="btnAddProduct_Click" />
        </div>

        <!-- Lista dodanych produktów -->
        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="Produkt" />
                <asp:BoundField DataField="QuantityGrams" HeaderText="Ilość (g)" />
            </Columns>
        </asp:GridView>

        <div class="text-center mt-3">
            <asp:Button ID="btnSaveMeal" runat="server" Text="Zapisz posiłek" CssClass="btn btn-success" OnClick="btnSaveMeal_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Anuluj" CssClass="btn btn-secondary ml-2" OnClick="btnCancel_Click" CausesValidation="false" />
        </div>
    </div>
</asp:Content>