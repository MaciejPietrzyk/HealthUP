<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Health_up_.Logout" %>

<!DOCTYPE html>
<html lang="pl">
<head runat="server">
    <meta charset="utf-8" />
    <title>Wylogowanie</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" />

    <!-- automatyczne przekierowanie -->
    <meta http-equiv="refresh" content="4;url=/Home.aspx">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container text-center mt-5">
            <div class="alert alert-success">
                <h2>✅ Wylogowano pomyślnie!</h2>
                <p>Za 4 sekundy zostaniesz przekierowany na stronę główną.</p>
                <p>Jeśli przekierowanie nie nastąpi, 
                   <a href="/Home.aspx">kliknij tutaj</a>.
                </p>
            </div>
        </div>
    </form>
</body>
</html>
