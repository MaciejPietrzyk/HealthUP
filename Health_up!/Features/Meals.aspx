<%@ Page Async="true" Title="Posiłki" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Meals.aspx.cs" Inherits="Health_up_.Features.Meals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Twoje posiłki</h2>

        <!-- wybór daty i rodzaju posiłku -->
        <div class="form-group">
            <label for="txtMealDate">Data:</label>
            <asp:TextBox ID="txtMealDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ddlMealType">Rodzaj posiłku:</label>
            <asp:DropDownList ID="ddlMealType" runat="server" CssClass="form-control">
                <asp:ListItem Text="Śniadanie" Value="Śniadanie"></asp:ListItem>
                <asp:ListItem Text="Obiad" Value="Obiad"></asp:ListItem>
                <asp:ListItem Text="Kolacja" Value="Kolacja"></asp:ListItem>
                <asp:ListItem Text="Przekąska" Value="Przekąska"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnSelectMeal" runat="server" Text="Wybierz" CssClass="btn btn-primary" OnClick="btnSelectMeal_Click" />
        <asp:Label ID="lblMealMessage" runat="server" Visible="false"></asp:Label>

        <hr />

        <!-- wyszukiwanie produktów -->
        <div class="form-group mt-3">
            <label for="txtSearchProduct">Wyszukaj produkt:</label>
            <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnSearchProduct" runat="server" Text="Szukaj" CssClass="btn btn-success mt-2" OnClick="btnSearchProduct_Click" />
        </div>

        <!-- wyniki wyszukiwania -->
        <asp:Repeater ID="rptSearchResults" runat="server">
    <ItemTemplate>
        <div class="card mb-3" style="max-width: 500px;">
            <div class="row no-gutters">
                <div class="col-md-4">
                    <img src='<%# Eval("ImageUrl") %>' class="card-img" alt="Brak zdjęcia" />
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("ProductName") %></h5>
                        <p class="card-text">
                            <strong>Kalorie:</strong> <%# Eval("Calories") %> kcal / 100g<br />
                            <strong>Białko:</strong> <%# Eval("Protein") %> g<br />
                            <strong>Węglowodany:</strong> <%# Eval("Carbs") %> g<br />
                            <strong>Tłuszcz:</strong> <%# Eval("Fat") %> g
                        </p>

                        <asp:TextBox ID="txtGrams" runat="server" CssClass="form-control mb-2" placeholder="Ilość gramów"></asp:TextBox>
                        <asp:Button ID="btnAddMeal" runat="server" Text="Dodaj"
                            CommandArgument='<%# Eval("ProductName") + ";" + Eval("Calories") + ";" + Eval("Protein") + ";" + Eval("Carbs") + ";" + Eval("Fat") %>'
                            CssClass="btn btn-success btn-sm"
                            OnClick="btnAddMeal_Click" />
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

        <hr />

        <!-- zapisane produkty w posiłku -->
        <h4>Dodane produkty</h4>
        <asp:GridView ID="gvMeals" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-3"
              DataKeyNames="MealProductID"
              OnRowCommand="gvMeals_RowCommand">
    <Columns>
        <asp:BoundField DataField="ProductName" HeaderText="Produkt" />
        <asp:BoundField DataField="Grams" HeaderText="Ilość (g)" />
        <asp:BoundField DataField="Calories" HeaderText="Kalorie" />
        <asp:BoundField DataField="Protein" HeaderText="Białko" />
        <asp:BoundField DataField="Carbs" HeaderText="Węglowodany" />
        <asp:BoundField DataField="Fat" HeaderText="Tłuszcze" />
        <asp:TemplateField HeaderText="Akcje">
    <ItemTemplate>
        <asp:LinkButton ID="btnDelete" runat="server"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="DeleteMealProduct"
                        CommandArgument='<%# Container.DataItemIndex %>'
                        ToolTip="Usuń">
            <i class="fas fa-trash"></i>
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>
</asp:Content>