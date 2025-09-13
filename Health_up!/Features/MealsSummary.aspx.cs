using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Health_up_.Features
{
    public partial class MealsSummary : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/Account/Login.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["date"] != null)
                {
                    string date = Request.QueryString["date"];
                    lblDate.Text = date;
                    LoadMeals(date);
                }
            }
        }

        private void LoadMeals(string date)
        {
            DataTable dt = new DataTable();
            double totalCalories = 0, totalProtein = 0, totalCarbs = 0, totalFat = 0;
            double dailyLimit = 0;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();

                string query = @"
                    SELECT m.MealType, mp.ProductName, mp.Grams, mp.Calories, mp.Protein, mp.Carbs, mp.Fat
                    FROM Meals m
                    INNER JOIN MealProducts mp ON m.MealID = mp.MealID
                    WHERE m.UserID = @UserID AND CONVERT(date, m.MealDate) = @Date";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.Parameters.AddWithValue("@Date", date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    totalCalories += Convert.ToDouble(row["Calories"]);
                    totalProtein += Convert.ToDouble(row["Protein"]);
                    totalCarbs += Convert.ToDouble(row["Carbs"]);
                    totalFat += Convert.ToDouble(row["Fat"]);
                }

                SqlCommand limitCmd = new SqlCommand(@"
                    SELECT TOP 1 CaloricNeeds 
                    FROM UserHealthData 
                    WHERE UserID=@UserID 
                    ORDER BY HealthDataID DESC", con);
                limitCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                object result = limitCmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    dailyLimit = Convert.ToDouble(result);
                }
            }

            gvMeals.DataSource = dt;
            gvMeals.DataBind();

            double difference = totalCalories - dailyLimit;
            string status;

            if (difference > 0)
            {
                status = $"<span class='text-danger'>⚠️ Jesteś NAD limitem o {difference:F0} kcal " +
                         $"({totalCalories:F0} kcal / {dailyLimit:F0} kcal)</span>";
            }
            else
            {
                status = $"<span class='text-success'>✅ Jesteś POD limitem o {Math.Abs(difference):F0} kcal " +
                         $"({totalCalories:F0} kcal / {dailyLimit:F0} kcal)</span>";
            }

            summaryBox.InnerHtml = $@"
    <h4>Podsumowanie dnia:</h4>
    <p><strong>Kalorie:</strong> {totalCalories:F0} kcal</p>
    <p><strong>Białko:</strong> {totalProtein:F1} g</p>
    <p><strong>Węglowodany:</strong> {totalCarbs:F1} g</p>
    <p><strong>Tłuszcze:</strong> {totalFat:F1} g</p>
    <p>{status}</p>";
        }
    }
}