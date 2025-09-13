using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Health_up_
{
    public partial class Register : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                Response.Write("<script>alert('Użytkownik z tym loginem już istnieje');</script>");
            }
            else if (CheckEmail())
            {
                Response.Write("<script>alert('Użytkownik z tym adresem email już istnieje');</script>");
            }
            else
            {
                SignUp();
            }
        }

        // Sprawdzanie czy email już istnieje
        private bool CheckEmail()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Email=@Email", con))
                    {
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt.Rows.Count >= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        // Sprawdzanie czy login już istnieje
        private bool CheckLogin()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@Username", con))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt.Rows.Count >= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        // 🔒 Rejestracja użytkownika z hashowaniem hasła
        private void SignUp()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username,FirstName,LastName,Email,PasswordHash,Gender,RegionID,CityID,IsBanned) VALUES (@Username,@FirstName,@LastName,@Email,@PasswordHash,@Gender,@RegionID,@CityID,@IsBanned)", con))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                        // 🔒 Hashowanie hasła przed zapisaniem do bazy
                        string hashedPassword = ComputeSha256Hash(txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@RegionID", 17);
                        cmd.Parameters.AddWithValue("@CityID", 81);
                        cmd.Parameters.AddWithValue("@IsBanned", 0);

                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Write("<script>alert('Rejestracja przebiegła pomyślnie!');</script>");
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Błąd podczas rejestracji: " + ex.Message;
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
