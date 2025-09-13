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
                // Parsujemy dane jeszcze raz z formularza
                double weight = Convert.ToDouble(txtWeight.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                double height = Convert.ToDouble(txtHeight.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                int age = int.Parse(txtAge.Text);
                double activityLevel = Convert.ToDouble(ddlActivityLevel.SelectedValue, System.Globalization.CultureInfo.InvariantCulture);
                int caloricNeeds = Convert.ToInt32(lblCalories.Text);

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    // sprawdź, czy user ma już rekord w UserHealthData
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM UserHealthData WHERE UserID=@UserID", con);
                    checkCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // update istniejącego rekordu
                        SqlCommand updateCmd = new SqlCommand(@"
                    UPDATE UserHealthData 
                    SET Weight=@Weight, Height=@Height, Age=@Age, ActivityLevel=@ActivityLevel, CaloricNeeds=@CaloricNeeds 
                    WHERE UserID=@UserID", con);

                        updateCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        updateCmd.Parameters.AddWithValue("@Weight", weight);
                        updateCmd.Parameters.AddWithValue("@Height", height);
                        updateCmd.Parameters.AddWithValue("@Age", age);
                        updateCmd.Parameters.AddWithValue("@ActivityLevel", activityLevel);
                        updateCmd.Parameters.AddWithValue("@CaloricNeeds", caloricNeeds);

                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // insert nowego rekordu
                        SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO UserHealthData (UserID, Weight, Height, Age, ActivityLevel, CaloricNeeds) 
                    VALUES (@UserID, @Weight, @Height, @Age, @ActivityLevel, @CaloricNeeds)", con);

                        insertCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        insertCmd.Parameters.AddWithValue("@Weight", weight);
                        insertCmd.Parameters.AddWithValue("@Height", height);
                        insertCmd.Parameters.AddWithValue("@Age", age);
                        insertCmd.Parameters.AddWithValue("@ActivityLevel", activityLevel);
                        insertCmd.Parameters.AddWithValue("@CaloricNeeds", caloricNeeds);

                        insertCmd.ExecuteNonQuery();
                    }
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