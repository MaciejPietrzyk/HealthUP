<%@ Page Title="Posiłki" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="Meals.aspx.cs"
    Inherits="Health_up_.Features.Meals" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-utensils"></i> Twoje posiłki</h2>
        <p class="text-center text-muted">Wybierz dzień i rodzaj posiłku, a następnie dodaj produkty.</p>

        <!-- Wybór dnia i posiłku -->
        <div class="card p-4 mb-4">
    <h4>Wybierz dzień i posiłek</h4>
    <div class="form-group">
        <label>Data:</label>
        <asp:TextBox ID="txtMealDate" runat="server" CssClass="form-control" TextMode="Date" />
    </div>
    <div class="form-group">
        <label>Rodzaj posiłku:</label>
        <asp:DropDownList ID="ddlMealType" runat="server" CssClass="form-control">
            <asp:ListItem Text="Śniadanie" Value="Śniadanie"></asp:ListItem>
            <asp:ListItem Text="Obiad" Value="Obiad"></asp:ListItem>
            <asp:ListItem Text="Kolacja" Value="Kolacja"></asp:ListItem>
            <asp:ListItem Text="Przekąska" Value="Przekąska"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnSelectMeal" runat="server" Text="Wybierz posiłek" CssClass="btn btn-primary" OnClick="btnSelectMeal_Click" />

    <!-- Komunikat -->
    <asp:Label ID="lblMealMessage" runat="server" CssClass="mt-3 d-block font-weight-bold" Visible="false"></asp:Label>
</div>

        <!-- Wyszukiwarka -->
        <div class="card p-4 mb-4">
            <h4>Wyszukaj produkt</h4>
            <div class="input-group">
                <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Wpisz nazwę produktu..." />
                <div class="input-group-append">
                    <asp:Button ID="btnSearchProduct" runat="server" Text="Szukaj" CssClass="btn btn-primary" OnClick="btnSearchProduct_Click" />
                </div>
            </div>
        </div>

        <!-- Wyniki wyszukiwania -->
       <asp:Repeater ID="rptSearchResults" runat="server">
    <ItemTemplate>
        <div class="card mb-3 p-3 d-flex flex-row align-items-center">
            <img src='<%# Eval("ImageUrl") %>' alt="produkt" 
                 style="width:80px; height:80px; object-fit:cover;" class="mr-3" />
            <div class="flex-fill">
                <h5><%# Eval("ProductName") %></h5>
                <p>Kalorie: <%# Eval("Calories") %> kcal / 100g</p>
                <asp:TextBox ID="txtGrams" runat="server" CssClass="form-control" 
                             placeholder="Ilość (g)" Width="100" />
            </div>
            <asp:Button ID="btnAddMeal" runat="server" Text="Dodaj"
                CssClass="btn btn-success ml-2"
                CommandArgument='<%# Eval("ProductName") + ";" + Eval("Calories") %>'
                OnClick="btnAddMeal_Click" />
        </div>
    </ItemTemplate>
</asp:Repeater>

        <!-- Lista produktów w posiłku -->
        <div class="card p-4 mt-4">
            <h4>Produkty w wybranym posiłku</h4>
            <asp:GridView ID="gvMeals" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Produkt" />
                    <asp:BoundField DataField="Grams" HeaderText="Ilość (g)" />
                    <asp:BoundField DataField="Calories" HeaderText="Kalorie (kcal)" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>