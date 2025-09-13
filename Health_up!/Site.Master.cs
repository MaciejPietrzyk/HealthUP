using System;
using System.Web;

namespace Health_up_
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HighlightActiveLink();
            }
        }

        private void HighlightActiveLink()
        {
            string currentPage = Request.Url.AbsolutePath.ToLower();
            ResetNavClasses();

            if (currentPage.Contains("home")) lnkHome.CssClass += " active";
            else if (currentPage.Contains("about")) lnkAbout.CssClass += " active";
            else if (currentPage.Contains("contact")) lnkContact.CssClass += " active";
            else if (currentPage.Contains("calculator")) lnkCalc.CssClass += " active";
            else if (currentPage.Contains("userprofile")) lnkProfile.CssClass += " active";
            else if (currentPage.Contains("meals")) lnkMeals.CssClass += " active";
            else if (currentPage.Contains("login")) lnkLogin.CssClass += " active";
            else if (currentPage.Contains("register")) lnkRegister.CssClass += " active";
            else if (currentPage.Contains("adminpanel")) lnkAdminPanel.CssClass += " active";
            else if (currentPage.Contains("products")) lnkProducts.CssClass += " active";
        }

        private void ResetNavClasses()
        {
            if (lnkHome != null) lnkHome.CssClass = "nav-link";
            if (lnkAbout != null) lnkAbout.CssClass = "nav-link";
            if (lnkContact != null) lnkContact.CssClass = "nav-link";
            if (lnkCalc != null) lnkCalc.CssClass = "nav-link";
            if (lnkProfile != null) lnkProfile.CssClass = "nav-link";
            if (lnkMeals != null) lnkMeals.CssClass = "nav-link";
            if (lnkLogin != null) lnkLogin.CssClass = "nav-link";
            if (lnkRegister != null) lnkRegister.CssClass = "nav-link";
            if (lnkAdminPanel != null) lnkAdminPanel.CssClass = "nav-link";
            if (lnkProducts != null) lnkProducts.CssClass = "nav-link";
            if (lnkLogoutAdmin != null) lnkLogoutAdmin.CssClass = "nav-link";
        }
    }
}
