using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Health_up_
{
    public partial class UserProfile : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    LoadUserData(Session["UserID"].ToString());
                }
                else
                {
                    // jeśli ktoś nie jest zalogowany → przekierowanie do logowania
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }

        private void LoadUserData(string userId)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                string query = @"
                    SELECT 
                        u.FirstName, 
                        u.LastName, 
                        u.Email, 
                        u.Gender, 
                        CASE 
                            WHEN u.RegionID IS NULL OR u.RegionID = 17 THEN 'Nie podano' 
                            ELSE r.RegionName 
                        END AS RegionName,
                        CASE 
                            WHEN u.CityID IS NULL OR u.CityID = 81 THEN 'Nie podano' 
                            ELSE c.CityName 
                        END AS CityName,
                        uh.CaloricNeeds
                    FROM Users u
                    LEFT JOIN UserHealthData uh ON u.UserID = uh.UserID
                    LEFT JOIN Regions r ON u.RegionID = r.RegionID
                    LEFT JOIN Cities c ON u.CityID = c.CityID
                    WHERE u.UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        lblFirstName.Text = dr["FirstName"].ToString();
                        lblLastName.Text = dr["LastName"].ToString();
                        lblEmail.Text = dr["Email"].ToString();
                        lblGender.Text = dr["Gender"].ToString();
                        lblRegion.Text = dr["RegionName"].ToString();
                        lblCity.Text = dr["CityName"].ToString();

                        if (dr["CaloricNeeds"] != DBNull.Value)
                        {
                            lblCalories.Text = "Twoje dzienne zapotrzebowanie: "
                                             + dr["CaloricNeeds"].ToString() + " kcal";
                        }
                        else
                        {
                            lblCalories.Text = "Nie obliczono zapotrzebowania kalorycznego.";
                            btnCalculateCalories.Visible = true;
                        }
                    }
                }
            }
        }

        protected void btnCalculateCalories_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Features/Calculator.aspx");
        }
    }
}