using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Health_up_
{
    public partial class Calculator : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Parsowanie danych wejściowych
                double weight = Convert.ToDouble(txtWeight.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                double height = Convert.ToDouble(txtHeight.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                int age = int.Parse(txtAge.Text);
                double activityLevel = Convert.ToDouble(ddlActivityLevel.SelectedValue, System.Globalization.CultureInfo.InvariantCulture);

                // Wzór na BMR (Mifflin-St Jeor)
                double bmr;
                if (Session["Gender"] != null && Session["Gender"].ToString() == "Mężczyzna")
                {
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) + 5;
                }
                else
                {
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) - 161;
                }

                // CPM (BMR * współczynnik aktywności)
                double caloricNeeds = bmr * activityLevel;
                lblCalories.Text = caloricNeeds.ToString("F0"); // zaokrąglone do pełnych kcal

                // Pokazanie wyników
                resultDiv.Visible = true;
                btnSaveToProfile.Visible = Session["UserID"] != null; // tylko zalogowani mogą zapisać
            }
            catch (FormatException)
            {
                lblMessage.Text = "Wprowadź poprawne wartości liczbowe!";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Błąd: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void btnSaveToProfile_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblMessage.Text = "Musisz być zalogowany, aby zapisać dane!";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Users SET DailyCalories = @CaloricNeeds WHERE UserID = @UserID", con);

                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@CaloricNeeds", Convert.ToInt32(lblCalories.Text));

                    cmd.ExecuteNonQuery();
                }

                // komunikat i powrót do profilu
                Response.Redirect("~/Profiles/UserProfile.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Błąd podczas zapisywania: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }
    }
}