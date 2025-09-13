using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Health_up_
{
    public partial class Login : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("~/Profiles/UserProfile.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@Username", con))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string storedHash = dr["PasswordHash"].ToString();
                                bool isBanned = Convert.ToBoolean(dr["isBanned"]);

                                if (isBanned)
                                {
                                    lblMessage.Text = "Twoje konto zostało zablokowane.";
                                    lblMessage.CssClass = "text-danger";
                                    lblMessage.Visible = true;
                                    return;
                                }

                                // 🔐 Hashowanie wprowadzonego hasła i porównanie z bazą
                                string enteredPasswordHash = ComputeSha256Hash(txtPassword.Text.Trim());
                                if (storedHash == enteredPasswordHash)
                                {
                                    // 🟢 Logowanie pomyślne – zapisanie danych w sesji
                                    Session["UserID"] = dr["UserID"] != DBNull.Value ? dr["UserID"].ToString() : "0";
                                    Session["Username"] = dr["Username"].ToString();
                                    Session["FirstName"] = dr["FirstName"].ToString();
                                    Session["LastName"] = dr["LastName"].ToString();
                                    Session["Email"] = dr["Email"].ToString();
                                    Session["Gender"] = dr["Gender"].ToString();

                                    Response.Redirect("~/Profiles/UserProfile.aspx");
                                }
                                else
                                {
                                    lblMessage.Text = "Błędne hasło.";
                                    lblMessage.CssClass = "text-danger";
                                    lblMessage.Visible = true;
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Nie znaleziono użytkownika o podanym loginie/haśle.";
                                lblMessage.CssClass = "text-danger";
                                lblMessage.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Błąd logowania: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        // 🔒 Funkcja hashująca SHA-256
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Zamiana bajtów na zapis heksadecymalny
                }
                return builder.ToString();
            }
        }
    }
}
