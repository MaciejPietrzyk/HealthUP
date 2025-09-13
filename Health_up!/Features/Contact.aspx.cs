
using System;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;

namespace Health_up_
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Pobieranie danych z formularza
                string userName = txtName.Text.Trim();
                string userEmail = txtEmail.Text.Trim();
                string userMessage = txtMessage.Text.Trim();

                // Pobranie konfiguracji z Web.config
                string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
                string smtpPass = ConfigurationManager.AppSettings["SmtpPassword"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);

                // Tworzenie wiadomości
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUser);
                mail.To.Add(smtpUser); // wysyłasz na swój adres
                mail.Subject = "Nowa wiadomość kontaktowa od: " + userName;
                mail.Body = $"Imię i nazwisko: {userName}\nEmail: {userEmail}\n\nWiadomość:\n{userMessage}";
                mail.IsBodyHtml = false;

                // Dodanie Reply-To, żeby odpowiedź trafiała do nadawcy
                if (!string.IsNullOrEmpty(userEmail))
                {
                    mail.ReplyToList.Add(new MailAddress(userEmail));
                }

                // Konfiguracja SMTP
                using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                }

                // Komunikat sukcesu
                lblMessage.Text = "✅ Wiadomość została wysłana pomyślnie!";
                lblMessage.CssClass = "text-success font-weight-bold";
                lblMessage.Visible = true;

                // Czyszczenie formularza
                txtName.Text = "";
                txtEmail.Text = "";
                txtMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Błąd podczas wysyłania wiadomości: " + ex.Message;
                lblMessage.CssClass = "text-danger font-weight-bold";
                lblMessage.Visible = true;
            }
        }
    }
}