using System;
using System.Web.UI;

namespace Health_up_
{
    public partial class AdminLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false; // Ukrywamy komunikat błędu po załadowaniu strony
        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            string adminUsername = txtAdminUsername.Text.Trim();
            string adminPassword = txtAdminPassword.Text.Trim();

            // Sprawdzenie danych logowania (przykład)
            if (adminUsername == "admin" && adminPassword == "admin123")
            {
                Session["IsAdmin"] = true;
                Response.Redirect("~/Admin/AdminPanel.aspx");
            }
            else
            {
                lblMessage.Text = "Nieprawidłowa nazwa użytkownika lub hasło!";
                lblMessage.Visible = true;
            }
        
        }
    }
}