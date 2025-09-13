<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Health_up_.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Sekcja Hero (Baner powitalny) -->
    <div class="hero-section">
        <div>
            <h1>Witaj w Health Up!</h1>
            <p>Twój partner w drodze do zdrowszego i bardziej aktywnego życia</p>
            <a href="<%= ResolveUrl("~/Features/about.aspx") %>" class="btn btn-primary">Poznaj nas</a>
        </div>
    </div>

    <!-- Sekcja Aktywności Fizycznej -->
    <section class="fitness-section">
        <div class="container">
            <h2>Znajdź swoją aktywność fizyczną</h2>
            <div class="row">
                <!-- Karta 1 -->
                <div class="col-md-4">
                    <div class="fitness-card">
                        <img src="/Assets/images/yoga.jpg" alt="Yoga" class="img-fluid">
                        <h4>Joga</h4>
                        <p>Spokojna i relaksująca aktywność, która poprawia elastyczność i redukuje stres.</p>
                    </div>
                </div>
                <!-- Karta 2 -->
                <div class="col-md-4">
                    <div class="fitness-card">
                        <img src="Assets/images/running.jpg" alt="Running" class="img-fluid">
                        <h4>Bieganie</h4>
                        <p>Idealne dla poprawy kondycji, utraty wagi i zwiększenia wytrzymałości.</p>
                    </div>
                </div>
                <!-- Karta 3 -->
                <div class="col-md-4">
                    <div class="fitness-card">
                        <img src="Assets/images/cycling.jpg" alt="Cycling" class="img-fluid">
                        <h4>Jazda na rowerze</h4>
                        <p>Doskonale angażuje mięśnie nóg i poprawia kondycję serca.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Sekcja Zdrowego Odżywiania -->
    <section class="nutrition-section">
        <div class="container">
            <h2>Zdrowe odżywianie</h2>
            <div class="row">
                <!-- Karta 1 -->
                <div class="col-md-4">
                    <div class="nutrition-card">
                        <img src="Assets/images/salad.jpg" alt="Salad" class="img-fluid">
                        <h4>Sałatki</h4>
                        <p>Świeże, kolorowe składniki bogate w witaminy i minerały.</p>
                    </div>
                </div>
                <!-- Karta 2 -->
                <div class="col-md-4">
                    <div class="nutrition-card">
                        <img src="Assets/images/smoothie.jpg" alt="Smoothie" class="img-fluid">
                        <h4>Smoothie</h4>
                        <p>Zdrowe i pełne energii napoje z owoców i warzyw.</p>
                    </div>
                </div>
                <!-- Karta 3 -->
                <div class="col-md-4">
                    <div class="nutrition-card">
                        <img src="Assets/images/mealprep.jpg" alt="Meal Prep" class="img-fluid">
                        <h4>Meal Prep</h4>
                        <p>Przygotuj zdrowe posiłki na cały tydzień i oszczędzaj czas.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

 



</asp:Content>
