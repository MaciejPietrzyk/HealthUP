using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Health_up_
{
    public partial class EditProfile : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                LoadRegions();
                LoadUserData();
            }
        }

        private void LoadRegions()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT RegionID, RegionName FROM Regions ORDER BY RegionName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    ddlRegion.DataSource = cmd.ExecuteReader();
                    ddlRegion.DataTextField = "RegionName";
                    ddlRegion.DataValueField = "RegionID";
                    ddlRegion.DataBind();
                }
            }
            ddlRegion.Items.Insert(0, new ListItem("-- Wybierz województwo --", "0"));
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCities();
        }

        private void LoadCities()
        {
            ddlCity.Items.Clear();
            if (ddlRegion.SelectedValue != "0")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT CityID, CityName FROM Cities WHERE RegionID = @RegionID ORDER BY CityName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                        conn.Open();
                        ddlCity.DataSource = cmd.ExecuteReader();
                        ddlCity.DataTextField = "CityName";
                        ddlCity.DataValueField = "CityID";
                        ddlCity.DataBind();
                    }
                }
            }
            ddlCity.Items.Insert(0, new ListItem("-- Wybierz miasto --", "0"));
        }

        private void LoadUserData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT FirstName, LastName, Gender, RegionID, CityID FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"]));
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        ddlGender.SelectedValue = reader["Gender"].ToString();

                        if (reader["RegionID"] != DBNull.Value)
                        {
                            ddlRegion.SelectedValue = reader["RegionID"].ToString();
                            LoadCities();

                            if (reader["CityID"] != DBNull.Value)
                            {
                                ddlCity.SelectedValue = reader["CityID"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE Users 
                                 SET FirstName = @FirstName, 
                                     LastName = @LastName, 
                                     Gender = @Gender, 
                                     RegionID = @RegionID, 
                                     CityID = @CityID
                                 WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue != "0" ? (object)ddlRegion.SelectedValue : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue != "0" ? (object)ddlCity.SelectedValue : DBNull.Value);

                    // 🔑 klucz – konwersja na int
                    cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"]));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "Dane zostały zaktualizowane!";
            lblMessage.CssClass = "text-success";
            lblMessage.Visible = true;

            // automatyczny powrót na profil
            Response.Redirect("~/Profiles/UserProfile.aspx");
        }
    }
}