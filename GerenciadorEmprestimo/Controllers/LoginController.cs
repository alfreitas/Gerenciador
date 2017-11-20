using GerenciadorEmprestimo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace GerenciadorEmprestimo.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            try
            {
                if (login.Usuario.ToUpper().Equals("ADMIN") && login.Senha == "123456")
                {
                    var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(login);

                    var ticket = new FormsAuthenticationTicket(1, login.Usuario, DateTime.Now, DateTime.Now.AddMinutes(20), false, serializedUser);
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var isSsl = Request.IsSecureConnection; // if we are running in SSL mode then make the cookie secure only

                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                    {
                        HttpOnly = true, // always set this to true!
                        Secure = isSsl,
                    };


                    Response.Cookies.Set(cookie);

                    //FormsAuthentication.SetAuthCookie("teste", false);
                    return RedirectToAction("Index", "Home");
                }else
                {
                    ViewBag.UsuarioInvalido = true;
                    return View("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public void LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
        }

    }
}
