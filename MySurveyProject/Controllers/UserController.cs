using Microsoft.AspNetCore.Mvc;
using MySurveyProject.Model;
using MySurveyProject.ViewModels;

namespace MySurveyProject.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult UserLogin(string? message)
        {

            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;

            }



            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(UserLoginDTO model)
        {
            var user = _context.Users.FirstOrDefault(p => p.UserName == model.UserName);
            if (user is null)
            {
                return RedirectToAction("UserLogin", "User", new { message = "Kullanıcı Adı veya Şifre Yanlış" });
            }
            if (user.Password != model.Password)
            {
                return RedirectToAction("UserLogin", "User", new { message = "Kullanıcı Adı veya Şifre Yanlış" });
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            return RedirectToAction("UserIndex", "User");
        }

        [HttpGet]
        public IActionResult UserSignUp(string? message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;

            }


            return View();
        }
        [HttpPost]
        public IActionResult UserSignUp(UserLoginDTO model)
        {
            if ((_context.Users.Any(p => p.UserName == model.UserName)) == false)
            {
                _context.Users.Add(new()
                {
                    UserName = model.UserName,
                    Password = model.Password
                });
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("UserLogin", "User", new { message = "Bu kullanıcı Adı Alınmış" });
            }
            return RedirectToAction("UserLogin", "User");
        }
        public IActionResult UserIndex()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("UserLogin", "User", new { message = "Lütfen Önce Giriş Yapınız" });
            }



            var surveys = _context.Surveys.Where(p => p.Activity == true).ToList();


            return View(surveys);
        }
    }
}
