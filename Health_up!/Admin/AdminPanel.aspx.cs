using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Health_up_
{
    public partial class AdminPanel : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
            {
                Response.Redirect("~/Admin/AdminLogin.aspx");
            }

            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT UserID, Username, FirstName, LastName, Email, isBanned FROM Users ORDER BY UserID", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptUsers.DataSource = dt;
                    rptUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Błąd podczas ładowania użytkowników: " + ex.Message + "');</script>");
            }
        }

        protected void btnToggleBlock_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(';');
            string userId = args[0];
            int isBanned = int.Parse(args[1]); // 0 lub 1

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET isBanned = @NewStatus WHERE UserID=@UserID", con);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@NewStatus", (isBanned == 1 ? 0 : 1));
                    cmd.ExecuteNonQuery();
                }

                LoadUsers();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Błąd podczas zmiany statusu: " + ex.Message + "');</script>");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string userId = btn.CommandArgument;

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID=@UserID", con);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }

                LoadUsers();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Błąd podczas usuwania: " + ex.Message + "');</script>");
            }
        }
    }
}