using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json.Linq;

namespace Health_up_.Features
{
    public partial class Meals : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
             
            }
        }

        protected void btnSelectMeal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMealDate.Text))
            {
                lblMealMessage.Text = "⚠️ Wybierz datę!";
                lblMealMessage.CssClass = "text-danger font-weight-bold mt-2 d-block";
                lblMealMessage.Visible = true;
                return;
            }

            DateTime mealDate = DateTime.Parse(txtMealDate.Text);
            string mealType = ddlMealType.SelectedValue;

            int mealId = EnsureMealExists(mealDate, mealType);
            Session["SelectedMealID"] = mealId;

            LoadMealEntries();

            
            lblMealMessage.Text = $"✅ Wybrano posiłek: {mealType} dnia {mealDate:yyyy-MM-dd}";
            lblMealMessage.CssClass = "text-success font-weight-bold mt-2 d-block";
            lblMealMessage.Visible = true;
        }

        private int EnsureMealExists(DateTime date, string type)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string checkSql = "SELECT MealID FROM Meals WHERE UserID=@UserID AND MealDate=@Date AND MealType=@Type";
                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                checkCmd.Parameters.AddWithValue("@Date", date);
                checkCmd.Parameters.AddWithValue("@Type", type);

                con.Open();
                object result = checkCmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    string insertSql = "INSERT INTO Meals (UserID, MealDate, MealType) OUTPUT INSERTED.MealID VALUES (@UserID, @Date, @Type)";
                    SqlCommand insertCmd = new SqlCommand(insertSql, con);
                    insertCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    insertCmd.Parameters.AddWithValue("@Date", date);
                    insertCmd.Parameters.AddWithValue("@Type", type);
                    return (int)insertCmd.ExecuteScalar();
                }
            }
        }

        protected async void btnSearchProduct_Click(object sender, EventArgs e)
        {
            if (Session["SelectedMealID"] == null)
            {
                Response.Write("<script>alert('Najpierw wybierz dzień i posiłek!');</script>");
                return;
            }

            string query = txtSearchProduct.Text.Trim();
            if (!string.IsNullOrEmpty(query))
            {
                await LoadProductsFromApi(query);
            }
        }

        private async Task LoadProductsFromApi(string query)
        {
            try
            {
                string apiUrl = $"https://world.openfoodfacts.org/cgi/search.pl?search_terms={query}&search_simple=1&action=process&json=1&page_size=10";

                using (HttpClient client = new HttpClient())
                {
                    string json = await client.GetStringAsync(apiUrl);
                    JObject data = JObject.Parse(json);

                    var results = new List<dynamic>();

                    foreach (var item in data["products"])
                    {
                        string name = item["product_name"]?.ToString();
                        string image = item["image_small_url"]?.ToString();
                        string kcal = item["nutriments"]?["energy-kcal_100g"]?.ToString();

                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(kcal))
                        {
                            results.Add(new
                            {
                                ProductName = name,
                                ImageUrl = !string.IsNullOrEmpty(image) ? image : "/Assets/images/no-image.png",
                                Calories = kcal
                            });
                        }
                    }

                    if (results.Count > 0)
                    {
                        rptSearchResults.DataSource = results;
                        rptSearchResults.DataBind();
                    }
                    else
                    {
                        LoadProductsFromDb(query);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Błąd API: " + ex.Message + "');</script>");
            }
        }

        private void LoadProductsFromDb(string query)
        {
            var results = new List<dynamic>();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string sql = "SELECT ProductName, CaloriesPer100g FROM Products WHERE ProductName LIKE @query";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@query", "%" + query + "%");
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        ProductName = reader["ProductName"].ToString(),
                        ImageUrl = "/Assets/images/no-image.png",
                        Calories = reader["CaloriesPer100g"].ToString()
                    });
                }
            }

            rptSearchResults.DataSource = results;
            rptSearchResults.DataBind();
        }

        protected void btnAddMeal_Click(object sender, EventArgs e)
        {
            if (Session["SelectedMealID"] == null)
            {
                Response.Write("<script>alert('Najpierw wybierz dzień i posiłek!');</script>");
                return;
            }

            var btn = (System.Web.UI.WebControls.Button)sender;
            var container = (System.Web.UI.WebControls.RepeaterItem)btn.NamingContainer;

            // Szukamy TextBoxa w aktualnym wierszu
            var txtGrams = (System.Web.UI.WebControls.TextBox)container.FindControl("txtGrams");

            double grams = 100; // domyślnie 100g
            if (!string.IsNullOrEmpty(txtGrams.Text))
            {
                double.TryParse(txtGrams.Text, out grams);
            }

            string[] args = btn.CommandArgument.Split(';');
            string productName = args[0];
            double caloriesPer100g = Convert.ToDouble(args[1]);

            // Obliczamy kalorie proporcjonalnie
            double calories = (grams / 100.0) * caloriesPer100g;

            SaveMealEntry(productName, calories, grams);
            LoadMealEntries();
        }

        private void SaveMealEntry(string productName, double calories, double grams)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "INSERT INTO MealEntries (MealID, ProductName, Calories, Grams) VALUES (@MealID, @ProductName, @Calories, @Grams)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MealID", Session["SelectedMealID"]);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Calories", calories);
                    cmd.Parameters.AddWithValue("@Grams", grams);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadMealEntries()
        {
            if (Session["SelectedMealID"] == null) return;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT ProductName, Grams, Calories FROM MealEntries WHERE MealID=@MealID ORDER BY EntryID DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MealID", Session["SelectedMealID"]);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvMeals.DataSource = dt;
                        gvMeals.DataBind();
                    }
                }
            }
        }
    }
}