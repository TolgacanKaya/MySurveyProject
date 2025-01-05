using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySurveyProject.Model;

namespace MySurveyProject.Controllers
{
    public class SurveyController : Controller
    {
        private readonly AppDbContext _context;

        public SurveyController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CreateSurvey()
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
            {
                return RedirectToAction("SignIn", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateSurvey(string name, string description)
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
            {
                return RedirectToAction("SignIn", "Admin");
            }

            _context.Surveys.Add(new Survey
            {
                CreatedDate = DateTime.Now,
                Description = description,
                SurveyName = name,
                Activity = false,
                Completable = false
            });
            _context.SaveChanges();
           
            int surveyId = _context.Surveys.OrderByDescending(p => p.SurveyId).FirstOrDefault()!.SurveyId;
            TempData["surveyId"] = surveyId;
            return RedirectToAction("AddQuestionToSurvey", "Survey", new { route = "Ekle" });
        }
        [HttpGet]
        public ActionResult AddQuestionToSurvey(string? route)
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
            {
                return RedirectToAction("SignIn", "Admin");
            }

            if (route != "Ekle")
            {
                return RedirectToAction("SurveyHistory", "Survey");
            }
            int? srId = (int)TempData["surveyId"];


            var survey = _context.Surveys.Find(Convert.ToInt32(srId));
            bool? completable = survey.Completable;
            if (completable.HasValue)
            {
                if (completable == true)
                {
                    ViewBag.Completable = true;
                }
                else
                {
                    ViewBag.Completable = false;
                }
            }
            ViewBag.SurveyID = srId;



            return View();
        }

        [HttpPost]
        public ActionResult AddQuestionToSurvey(string question, string questionType, int surveyId)
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
            {
                return RedirectToAction("SignIn", "Admin");
            }





            _context.Questions.Add(new Question
            {
                QuestionText = question,
                QuestionType = questionType,
                SurveyId = surveyId
            });
            var survey = _context.Surveys.Find(surveyId);
            survey!.Completable = true;
            _context.SaveChanges();
            int questionId = _context.Questions.OrderByDescending(p => p.QuestionId).First().QuestionId;

            TempData["questionId"] = questionId;
            if (questionType == "Test")
            {

                return RedirectToAction("AddOptionForQuestion", "Survey");
            }
            else
            {
                TempData["surveyId"] = surveyId;
                return RedirectToAction("AddQuestionToSurvey", "Survey", new { route = "Ekle" });
            }
        }
        [HttpGet]
        public ActionResult AddOptionForQuestion()
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
            {
                return RedirectToAction("SignIn", "Admin");
            }
            int questionId = (int)TempData["questionId"];
            ViewBag.QuestionID = questionId;


            return View();
        }
        [HttpPost]
        public ActionResult AddOptionForQuestion(string opt1, string opt2, string opt3, string opt4, int QuestionId)
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
            {
                return RedirectToAction("SignIn", "Admin");
            }

            if (!String.IsNullOrEmpty(opt1))
            {
                _context.Options.Add(new Option
                {
                    QuestionId = QuestionId,
                    OptionText = opt1
                });
            }
            if (!String.IsNullOrEmpty(opt2))
            {
                _context.Options.Add(new Option
                {
                    QuestionId = QuestionId,
                    OptionText = opt2,
                });
            }
            if (!String.IsNullOrEmpty(opt3))
            {
                _context.Options.Add(new Option
                {
                    QuestionId = QuestionId,
                    OptionText = opt3,
                });
            }
            if (!String.IsNullOrEmpty(opt4))
            {
                _context.Options.Add(new Option
                {
                    QuestionId = QuestionId,
                    OptionText = opt4,
                });
            }
            _context.SaveChanges();

            int surveyId = _context.Surveys.OrderByDescending(p => p.SurveyId).FirstOrDefault().SurveyId;
            TempData["surveyId"] = surveyId;
            return RedirectToAction("AddQuestionToSurvey", "Survey", new { route = "Ekle" });
        }
        public ActionResult CompleteSurvey(int surveyId)
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (!id.HasValue)
            {
                return RedirectToAction("SignIn", "Admin");
            }

            _context.Surveys.Find(surveyId)!.Activity = true;
            var survey = _context.Surveys.OrderByDescending(p => p.SurveyId).FirstOrDefault();
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Adınız?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Soyadınız?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Yaşınız?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Yaş?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Cinsiyet?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Mesleğiniz?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Eğitim Durumunuz?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });
            _context.Questions.Add(new Question
            {
                Activity = true,
                QuestionText = "Şehir?",
                QuestionType = "AcikUclu",
                SurveyId = survey!.SurveyId
            });



            _context.SaveChanges();




            return RedirectToAction("AdminIndex", "Admin");
        }


    }
}
