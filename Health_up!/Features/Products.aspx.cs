using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Health_up_.Features
{
    public partial class Products : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // dostęp tylko dla admina
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
            {
                Response.Redirect("~/Admin/AdminLogin.aspx");
            }

            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT ProductID, ProductName, CaloriesPer100g, ProteinPer100g, CarbsPer100g, FatPer100g FROM Products", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand(@"
                INSERT INTO Products (ProductName, CaloriesPer100g, ProteinPer100g, CarbsPer100g, FatPer100g) 
                VALUES (@Name, @Calories, @Protein, @Carbs, @Fat)", con);

                    cmd.Parameters.AddWithValue("@Name", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Calories", Convert.ToDouble(txtCalories.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Protein", string.IsNullOrEmpty(txtProtein.Text) ? 0 : Convert.ToDouble(txtProtein.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Carbs", string.IsNullOrEmpty(txtCarbs.Text) ? 0 : Convert.ToDouble(txtCarbs.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Fat", string.IsNullOrEmpty(txtFat.Text) ? 0 : Convert.ToDouble(txtFat.Text.Trim()));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "✅ Produkt dodany pomyślnie!";
                lblMessage.CssClass = "text-success";
                lblMessage.Visible = true;

                txtProductName.Text = "";
                txtCalories.Text = "";
                txtProtein.Text = "";
                txtCarbs.Text = "";
                txtFat.Text = "";

                LoadProducts();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Błąd: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void gvProducts_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int productId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadProducts();
        }
    }
}