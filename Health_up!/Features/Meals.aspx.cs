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
                        string name = item["product_name_pl"]?.ToString()
                                   ?? item["product_name"]?.ToString()
                                   ?? item["generic_name"]?.ToString()
                                   ?? "Nieznany produkt";

                        string image = item["image_small_url"]?.ToString();

                        double kcal = item["nutriments"]?["energy-kcal_100g"] != null ? (double)item["nutriments"]["energy-kcal_100g"] : 0;
                        double protein = item["nutriments"]?["proteins_100g"] != null ? (double)item["nutriments"]["proteins_100g"] : 0;
                        double carbs = item["nutriments"]?["carbohydrates_100g"] != null ? (double)item["nutriments"]["carbohydrates_100g"] : 0;
                        double fat = item["nutriments"]?["fat_100g"] != null ? (double)item["nutriments"]["fat_100g"] : 0;

                        if (!string.IsNullOrEmpty(name))
                        {
                            results.Add(new
                            {
                                ProductName = name,
                                ImageUrl = !string.IsNullOrEmpty(image) ? image : "/Assets/images/no-image.png",
                                Calories = kcal,
                                Protein = protein,
                                Carbs = carbs,
                                Fat = fat
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
                string sql = "SELECT ProductID, ProductName, CaloriesPer100g, ProteinPer100g, CarbsPer100g, FatPer100g FROM Products WHERE ProductName LIKE @query";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@query", "%" + query + "%");
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        ProductID = reader["ProductID"].ToString(),
                        ProductName = reader["ProductName"].ToString(),
                        ImageUrl = "/Assets/images/no-image.png",
                        Calories = Convert.ToDouble(reader["CaloriesPer100g"]),
                        Protein = Convert.ToDouble(reader["ProteinPer100g"]),
                        Carbs = Convert.ToDouble(reader["CarbsPer100g"]),
                        Fat = Convert.ToDouble(reader["FatPer100g"])
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

            var txtGrams = (System.Web.UI.WebControls.TextBox)container.FindControl("txtGrams");

            double grams = 100;
            if (!string.IsNullOrEmpty(txtGrams.Text))
            {
                double.TryParse(txtGrams.Text, out grams);
            }

            string[] args = btn.CommandArgument.Split(';');
            string productName = args[0];
            double caloriesPer100g = Convert.ToDouble(args[1]);
            double proteinPer100g = Convert.ToDouble(args[2]);
            double carbsPer100g = Convert.ToDouble(args[3]);
            double fatPer100g = Convert.ToDouble(args[4]);

            double calories = Math.Round((grams / 100.0) * caloriesPer100g, 2);
            double protein = Math.Round((grams / 100.0) * proteinPer100g, 2);
            double carbs = Math.Round((grams / 100.0) * carbsPer100g, 2);
            double fat = Math.Round((grams / 100.0) * fatPer100g, 2);

            SaveMealEntry(productName, grams, calories, protein, fat, carbs);
            LoadMealEntries();
        }

        private void SaveMealEntry(string productName, double grams, double calories, double protein, double carbs, double fat)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = @"INSERT INTO MealProducts (MealID, ProductName, Grams, Calories, Protein, Carbs, Fat)
                                 VALUES (@MealID, @ProductName, @Grams, @Calories, @Protein, @Carbs, @Fat)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MealID", Session["SelectedMealID"]);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Grams", grams);
                    cmd.Parameters.AddWithValue("@Calories", calories);
                    cmd.Parameters.AddWithValue("@Protein", protein);
                    cmd.Parameters.AddWithValue("@Carbs", carbs);
                    cmd.Parameters.AddWithValue("@Fat", fat);
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
                string query = @"SELECT MealProductID, ProductName, Grams, Calories, Protein, Carbs, Fat 
                         FROM MealProducts 
                         WHERE MealID=@MealID 
                         ORDER BY MealProductID DESC";
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
        protected void gvMeals_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMealProduct")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int mealProductId = Convert.ToInt32(gvMeals.DataKeys[rowIndex].Value);

                DeleteMealProduct(mealProductId);
                LoadMealEntries();
            }
        }

        private void DeleteMealProduct(int mealProductId)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "DELETE FROM MealProducts WHERE MealProductID=@MealProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MealProductID", mealProductId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}