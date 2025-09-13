<%@ Page Title="Panel Administratora" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Health_up_.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center"><i class="fas fa-user-shield"></i> Panel Administratora</h2>
        <p class="text-center text-muted">Zarządzaj użytkownikami aplikacji</p>

        <div class="table-responsive mt-4">
            <table id="usersTable" class="table table-striped table-bordered">
                <thead class="thead-dark">
                    <tr>
                        <th>ID</th>
                        <th>Nazwa użytkownika</th>
                        <th>Imię</th>
                        <th>Nazwisko</th>
                        <th>Email</th>
                        <th>Status</th>
                        <th>Akcje</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptUsers" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("UserID") %></td>
                                <td><%# Eval("Username") %></td>
                                <td><%# Eval("FirstName") %></td>
                                <td><%# Eval("LastName") %></td>
                                <td><%# Eval("Email") %></td>
                                <td>
                                    <%# (Convert.ToInt32(Eval("isBanned")) == 1) 
                                        ? "<span class='badge badge-danger'>Zablokowany</span>" 
                                        : "<span class='badge badge-success'>Aktywny</span>" %>
                                </td>
                                <td>
                                    <!-- Zablokuj / Odblokuj -->
                                    <asp:Button ID="btnToggleBlock" runat="server" 
                                        CssClass='<%# (Convert.ToInt32(Eval("isBanned")) == 1) ? "btn btn-success btn-sm mr-1" : "btn btn-warning btn-sm mr-1" %>'
                                        CommandArgument='<%# Eval("UserID") + ";" + Eval("isBanned") %>'
                                        Text='<%# (Convert.ToInt32(Eval("isBanned")) == 1) ? "Odblokuj" : "Zablokuj" %>'
                                        OnClick="btnToggleBlock_Click" />

                                    <!-- Usuń -->
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm"
                                        CommandArgument='<%# Eval("UserID") %>' Text="Usuń"
                                        OnClientClick="return confirm('Czy na pewno chcesz usunąć tego użytkownika?');"
                                        OnClick="btnDelete_Click" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/dataTables.bootstrap4.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#usersTable').DataTable();
        });
    </script>
</asp:Content>