using System;
using System.Web;
using System.Web.UI;

namespace Health_up_
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Usunięcie danych sesji
            Session.Clear();
            Session.Abandon();

            // Usunięcie ciasteczek autoryzacyjnych (jeśli są)
            if (Request.Cookies["UserAuth"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UserAuth");
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);
            }
        }
    }
}