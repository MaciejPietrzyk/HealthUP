<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Health_up_.About" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-info-circle"></i> O nas</h2>
        <p class="text-center text-muted">Dowiedz się więcej o naszej firmie i misji</p>

        <!-- Dodajemy większy odstęp za pomocą klasy mt-5 (margines od góry) -->
        <div class="text-center mt-5">
            <!-- Obrazek, który chcesz dodać -->
            <img src="/Assets/images/banner.jpg" alt="Nasza firma" class="img-fluid" />
        </div>

        <div class="row mt-5">
            <div class="col-md-6">
                <h4><i class="fas fa-building"></i> Nasza firma</h4>
                <p>
                    Jesteśmy dynamicznie rozwijającą się firmą zajmującą się zdrowiem i dobrostanem naszych klientów. 
                    Naszą misją jest dostarczanie najwyższej jakości produktów i usług, które pomagają w osiąganiu lepszego zdrowia i jakości życia.
                </p>
                <p>
                    Nasz zespół składa się z ekspertów w dziedzinach zdrowia, fitnessu i żywienia, którzy łączą swoją wiedzę i doświadczenie, aby wspierać Cię w drodze do lepszego samopoczucia.
                </p>
            </div>

            <div class="col-md-6">
                <h4><i class="fas fa-heart"></i> Nasza misja</h4>
                <p>
                    Nasza misja to wspieranie każdego klienta na każdym etapie ich zdrowotnej podróży. 
                    Chcemy, aby każdy miał dostęp do odpowiednich narzędzi, które pozwolą im dbać o zdrowie i dobrą kondycję.
                </p>
                <p>
                    Dzięki innowacyjnym technologiom i nowoczesnym rozwiązaniom, oferujemy naszym użytkownikom łatwe w użyciu aplikacje, które pomagają w monitorowaniu diety, aktywności fizycznej i ogólnego samopoczucia.
                </p>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-4">
                <h5><i class="fas fa-phone-alt"></i> Kontakt</h5>
                <p>Telefon: +48 123 456 789</p>
                <p>Email: kontakt@healthup.com</p>
            </div>

            <div class="col-md-4">
                <h5><i class="fas fa-map-marker-alt"></i> Nasza siedziba</h5>
                <p>ul. Przykładowa 12, 00-123 Warszawa</p>
            </div>

            <div class="col-md-4">
                <h5><i class="fas fa-clock"></i> Godziny pracy</h5>
                <p>Poniedziałek - Piątek: 9:00 - 18:00</p>
                <p>Sobota: 10:00 - 14:00</p>
            </div>
        </div>

        <div class="row mt-5 text-center">
            <div class="col">
                <h4><i class="fas fa-users"></i> Dołącz do nas!</h4>
                <p>
                    Chcesz być częścią naszego zespołu? Jesteśmy otwarci na nowe talenty. Skontaktuj się z nami!
                </p>
                <a href="contact.aspx" class="btn btn-primary">Skontaktuj się z nami</a>
            </div>
        </div>
    </div>
</asp:Content>
