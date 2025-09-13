using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Health_up_
{
    public partial class AddMeal : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        // lokalny koszyk produktów zanim zapiszemy do DB
        private DataTable ProductsTable
        {
            get
            {
                if (ViewState["ProductsTable"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ProductID", typeof(int));
                    dt.Columns.Add("ProductName", typeof(string));
                    dt.Columns.Add("QuantityGrams", typeof(int));
                    ViewState["ProductsTable"] = dt;
                }
                return (DataTable)ViewState["ProductsTable"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadProducts();
                txtMealDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // domyślnie dzisiaj
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductID, ProductName FROM Products ORDER BY ProductName", con);
                ddlProducts.DataSource = cmd.ExecuteReader();
                ddlProducts.DataTextField = "ProductName";
                ddlProducts.DataValueField = "ProductID";
                ddlProducts.DataBind();
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtQuantity.Text, out int grams) && grams > 0)
            {
                DataRow row = ProductsTable.NewRow();
                row["ProductID"] = int.Parse(ddlProducts.SelectedValue);
                row["ProductName"] = ddlProducts.SelectedItem.Text;
                row["QuantityGrams"] = grams;
                ProductsTable.Rows.Add(row);

                gvProducts.DataSource = ProductsTable;
                gvProducts.DataBind();

                txtQuantity.Text = "";
            }
            else
            {
                lblMessage.Text = "Podaj poprawną ilość w gramach.";
                lblMessage.Visible = true;
            }
        }

        protected void btnSaveMeal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMealName.Text) || ProductsTable.Rows.Count == 0)
            {
                lblMessage.Text = "Podaj nazwę posiłku i dodaj przynajmniej jeden produkt.";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                int mealId;
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    // dodaj posiłek
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Meals (UserID, MealDate, MealName) OUTPUT INSERTED.MealID VALUES (@UserID, @MealDate, @MealName)", con);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@MealDate", txtMealDate.Text);
                    cmd.Parameters.AddWithValue("@MealName", txtMealName.Text.Trim());
                    mealId = (int)cmd.ExecuteScalar();

                    // dodaj produkty do MealProducts
                    foreach (DataRow row in ProductsTable.Rows)
                    {
                        SqlCommand cmdProd = new SqlCommand(
                            "INSERT INTO MealProducts (MealID, ProductID, QuantityGrams) VALUES (@MealID, @ProductID, @QuantityGrams)", con);
                        cmdProd.Parameters.AddWithValue("@MealID", mealId);
                        cmdProd.Parameters.AddWithValue("@ProductID", row["ProductID"]);
                        cmdProd.Parameters.AddWithValue("@QuantityGrams", row["QuantityGrams"]);
                        cmdProd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("~/Features/Meals.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Błąd podczas zapisywania: " + ex.Message;
                lblMessage.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Features/Meals.aspx");
        }
    }
}