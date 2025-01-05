using Microsoft.AspNetCore.Mvc;
using MySurveyProject.Model;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MySurveyProject.ViewModels;

namespace MySurveyProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public int SendCodeToMail(string email)
        {

            Random rnd = new Random();
            int random = rnd.Next(100000, 999999 + 1);
            var admin = _context.Administrators.Find(2);
            string adminMail = admin!.AdminEmail;
            string adminMailSifre = admin.AdminEmailPassword;
            var cred = new NetworkCredential(adminMail, adminMailSifre);
            var client = new SmtpClient("smtp.gmail.com", 587);
            var msg = new System.Net.Mail.MailMessage();
            msg.To.Add(email);
            msg.Subject = "Giriş kodu doğrulama";
            msg.Body = $"Giriş kodunuz <strong>{random}</strong>";
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(adminMail, "Doğrulama kodu", Encoding.UTF8);
            client.Credentials = cred;
            client.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; }!;
            client.Send(msg);
            return random;

        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var admin = _context.Administrators.Find(2);
            if (admin!.AdminEmail == model.Email)
            {

                if (admin.AdminPassword == model.Password)
                {
                    int code = SendCodeToMail(model.Email);
                    HttpContext.Session.SetInt32("code", code);
                    return RedirectToAction("CodeVerification", "Admin");
                }
                else
                {

                    return RedirectToAction("Login", "Admin");
                }
            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }


        }
        [HttpGet]
        public IActionResult CodeVerification()
        {



            return View();
        }
        [HttpPost]
        public IActionResult CodeVerification(string _code)
        {
            if (HttpContext.Session.GetInt32("code").ToString() == _code)
            {

                int adminId = _context.Administrators.Find(2)!.Id;
                HttpContext.Session.SetInt32("id", adminId);
                return RedirectToAction("AdminIndex", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
        public IActionResult AdminIndex()
        {
            string id = HttpContext.Session.GetInt32("id").ToString()!;
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("SignIn", "Admin");
            }







            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("_code");

            return RedirectToAction("Login", "Admin");
        }
    }
}
