<%@ Page Title="Podsumowanie posiłków" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MealsSummary.aspx.cs" Inherits="Health_up_.Features.MealsSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">🍽 Podsumowanie posiłków dnia <asp:Label ID="lblDate" runat="server"></asp:Label></h2>

        <asp:GridView ID="gvMeals" runat="server" CssClass="table table-bordered mt-4" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="MealType" HeaderText="Posiłek" />
                <asp:BoundField DataField="ProductName" HeaderText="Produkt" />
                <asp:BoundField DataField="Grams" HeaderText="Ilość (g)" />
                <asp:BoundField DataField="Calories" HeaderText="Kalorie" />
                <asp:BoundField DataField="Protein" HeaderText="Białko" />
                <asp:BoundField DataField="Carbs" HeaderText="Węglowodany" />
                <asp:BoundField DataField="Fat" HeaderText="Tłuszcze" />
            </Columns>
        </asp:GridView>

        <div class="alert mt-4" id="summaryBox" runat="server"></div>
    </div>
</asp:Content>