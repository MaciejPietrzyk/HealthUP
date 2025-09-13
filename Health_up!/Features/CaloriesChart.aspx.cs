using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Collections.Generic;

namespace Health_up_.Features
{
    public partial class CaloriesChart : Page
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
                LoadChart();
            }
        }

        private void LoadChart()
        {
            var dates = new List<string>();
            var calories = new List<double>();
            double dailyLimit = 0;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();

                // limit z profilu
                SqlCommand limitCmd = new SqlCommand("SELECT CaloricNeeds FROM UserHealthData WHERE UserID=@UserID", con);
                limitCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                object result = limitCmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    dailyLimit = Convert.ToDouble(result);
                }

                // spożyte kalorie z posiłków (sumujemy per dzień)
                string query = @"
                    SELECT m.MealDate, SUM(mp.Calories) as TotalCalories
                    FROM Meals m
                    JOIN MealProducts mp ON m.MealID = mp.MealID
                    WHERE m.UserID = @UserID
                    GROUP BY m.MealDate
                    ORDER BY m.MealDate";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dates.Add(Convert.ToDateTime(reader["MealDate"]).ToString("yyyy-MM-dd"));
                    calories.Add(Convert.ToDouble(reader["TotalCalories"]));
                }
            }

            // budujemy JS Chart.js
            string chartScript = $@"
            <canvas id='caloriesChart'></canvas>
            <script>
             window.onload = function() {{
             var ctx = document.getElementById('caloriesChart').getContext('2d');
             var myChart = new Chart(ctx, {{
             type: 'bar',
             data: {{
                labels: {Newtonsoft.Json.JsonConvert.SerializeObject(dates)},
                datasets: [{{
                    label: 'Spożyte kalorie',
                    data: {Newtonsoft.Json.JsonConvert.SerializeObject(calories)},
                    backgroundColor: 'rgba(54, 162, 235, 0.6)'
                }},
                {{
                    label: 'Limit dzienny',
                    data: new Array({dates.Count}).fill({dailyLimit}),
                    type: 'line',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 2,
                    fill: false
                }}]
            }},
            options: {{
             responsive: true,
             onClick: function(evt, elements) {{
            if (elements.length > 0) {{
            var index = elements[0].index;
            var date = this.data.labels[index];
            window.location.href = '/Features/MealsSummary.aspx?date=' + date;
        }}
    }},
    scales: {{
        y: {{
            beginAtZero: true
        }}
    }}
}}
        }});
    }};
    </script>";

            ltChart.Text = chartScript;
        }
    }
}