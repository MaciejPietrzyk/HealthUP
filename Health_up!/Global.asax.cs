using System;
using System.Web.Routing;
using System.Web;

namespace Health_up_
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "HomeRoute",
                "home",
                "~/Home.aspx"
            );
            routes.MapPageRoute(
                "AboutRoute",
                "about",
                "~/Features/About.aspx"
            );
            routes.MapPageRoute(
                "ContactRoute",
                "contact",
                "~/Features/Contact.aspx"
            );
            routes.MapPageRoute(
                "CalculatorRoute",
                "calculator",
                "~/Features/Calculator.aspx"
            );
            routes.MapPageRoute(
                "LoginRoute",
                "account/login",
                "~/Account/Login.aspx"
            );
            routes.MapPageRoute(
                "RegisterRoute",
                "account/register",
                "~/Account/Register.aspx"
            );
            routes.MapPageRoute(
                "LogoutRoute",
                "account/logout",
                "~/Account/Logout.aspx"
            );
            routes.MapPageRoute(
                "ProfileRoute",
                "profiles/userprofile",
                "~/Profiles/Userprofile.aspx"
            );
            routes.MapPageRoute(
                "AdminPanelRoute",
                "admin/adminpanel",
                "~/Admin/AdminPanel.aspx"
            );
        }
    }
}