<%@ Page Title="Zarządzanie produktami" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Health_up_.Features.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-box"></i> Zarządzanie produktami</h2>
        <p class="text-center text-muted">Dodawaj i usuwaj produkty dostępne w aplikacji</p>

        <!-- Formularz dodawania produktu -->
        <div class="card p-4 mb-4">
            <h4>Dodaj nowy produkt</h4>
            <div class="form-group">
                <label for="txtProductName">Nazwa produktu</label>
                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtCalories">Kalorie (kcal / 100g)</label>
                <asp:TextBox ID="txtCalories" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtProtein">Białko (g / 100g):</label>
                <asp:TextBox ID="txtProtein" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtCarbs">Węglowodany (g / 100g):</label>
                <asp:TextBox ID="txtCarbs" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

<div class="form-group">
    <label for="txtFat">Tłuszcz (g / 100g):</label>
    <asp:TextBox ID="txtFat" runat="server" CssClass="form-control"></asp:TextBox>
</div>
            <asp:Button ID="btnAddProduct" runat="server" Text="Dodaj produkt" CssClass="btn btn-success mt-2" OnClick="btnAddProduct_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2" Visible="false"></asp:Label>
        </div>

        <!-- Lista produktów -->
        <div class="card p-4">
            <h4>Lista produktów</h4>
            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="ProductID" OnRowDeleting="gvProducts_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="ProductName" HeaderText="Nazwa produktu" />
                    <asp:BoundField DataField="CaloriesPer100g" HeaderText="Kalorie (kcal/100g)" />
                    <asp:BoundField DataField="ProteinPer100g" HeaderText="Białko (g)" />
                    <asp:BoundField DataField="CarbsPer100g" HeaderText="Węglowodany (g)" />
                    <asp:BoundField DataField="FatPer100g" HeaderText="Tłuszcz (g)" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Usuń" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>