<%@ Page Title="Wykres zapotrzebowania kalorycznego" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CaloriesChart.aspx.cs" Inherits="Health_up_.Features.CaloriesChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">📊 Wykres zapotrzebowania kalorycznego</h2>
        <p class="text-center text-muted">Twoje rzeczywiste spożycie kalorii vs. limit dzienny</p>
        <asp:Literal ID="ltChart" runat="server"></asp:Literal>
    </div>

    
</asp:Content>