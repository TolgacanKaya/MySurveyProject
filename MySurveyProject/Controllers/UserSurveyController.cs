using Microsoft.AspNetCore.Mvc;
using MySurveyProject.Model;
using MySurveyProject.ViewModels;

namespace MySurveyProject.Controllers
{
    public class UserSurveyController : Controller
    {
        private readonly AppDbContext _context;

        public UserSurveyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CraeteUserSurvey()
        {



            return View();
        }
        [HttpPost]
        public IActionResult CraeteUserSurvey(string name, string description)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("UserLogin", "User", new { message = "Lütfen Giriş Yapınız" });
            }
            _context.UsersSurveys.Add(new()
            {
                CreatedDate = DateTime.Now,
                Description = description,
                SurveyName = name,
                Activity = false,
                Completable = false,
                SurveyCompletionCount = 0,
                UserId = userId.Value
            });
            _context.SaveChanges();

            int userSurveyId = _context.UsersSurveys.OrderByDescending(p => p.UserSurveyId).FirstOrDefault()!.UserSurveyId;
            TempData["surveyId"] = userSurveyId;
            return RedirectToAction("AddQuestionToUserSurvey", "UserSurvey", new { route = "Ekle" });


        }
        [HttpGet]
        public ActionResult AddQuestionToUserSurvey(string? route)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "User", new { message = "Lütfen Giriş Yapınız" });

            }

            if (route != "Ekle")
            {
                return RedirectToAction("UserIndex", "User");
            }
            int? srId = (int)TempData["surveyId"];


            var survey = _context.UsersSurveys.Find(Convert.ToInt32(srId));
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
        public ActionResult AddQuestionToUserSurvey(string question, string questionType, int surveyId)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "User");
            }





            _context.UserSurveysQuestions.Add(new()
            {
                QuestionText = question,
                QuestionType = questionType,
                UserSurveysId = surveyId
            });
            var survey = _context.UsersSurveys.Find(surveyId);
            survey!.Completable = true;
            _context.SaveChanges();
            int questionId = _context.UserSurveysQuestions.OrderByDescending(p => p.UserSurveyQuestionsId).First().UserSurveyQuestionsId;

            TempData["questionId"] = questionId;
            if (questionType == "Test")
            {

                return RedirectToAction("AddOptionForUserQuestion", "UserSurvey");
            }
            else
            {
                TempData["surveyId"] = surveyId;
                return RedirectToAction("AddQuestionToUserSurvey", "UserSurvey", new { route = "Ekle" });
            }
        }
        [HttpGet]
        public ActionResult AddOptionForUserQuestion()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "User");
            }
            int questionId = (int)TempData["questionId"];
            ViewBag.QuestionID = questionId;



            return View();
        }
        [HttpPost]
        public ActionResult AddOptionForUserQuestion(string opt1, string opt2, string opt3, string opt4, int QuestionId)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "User");
            }

            if (!String.IsNullOrEmpty(opt1))
            {
                _context.UserSurveyOptions.Add(new()
                {
                    UserSurveyQuestionsId = QuestionId,
                    OptionText = opt1
                });
            }
            if (!String.IsNullOrEmpty(opt2))
            {
                _context.UserSurveyOptions.Add(new()
                {
                    UserSurveyQuestionsId = QuestionId,
                    OptionText = opt2,
                });
            }
            if (!String.IsNullOrEmpty(opt3))
            {
                _context.UserSurveyOptions.Add(new()
                {
                    UserSurveyQuestionsId = QuestionId,
                    OptionText = opt3,
                });
            }
            if (!String.IsNullOrEmpty(opt4))
            {
                _context.UserSurveyOptions.Add(new()
                {
                    UserSurveyQuestionsId = QuestionId,
                    OptionText = opt4,
                });
            }
            _context.SaveChanges();

            int surveyId = _context.UsersSurveys.OrderByDescending(p => p.UserSurveyId).FirstOrDefault().UserSurveyId;
            TempData["surveyId"] = surveyId;
            return RedirectToAction("AddQuestionToUserSurvey", "UserSurvey", new { route = "Ekle" });
        }
        public ActionResult CompleteUserSurvey(int surveyId)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var survey = _context.UsersSurveys.Find(surveyId);
            survey.Activity = true;
            string url = Guid.NewGuid().ToString() + surveyId.ToString();
            string lastUrl = "https://localhost:44380/UserSurvey/ViewSurveyWithUrl/" + url;
            survey.UniuqeLink = lastUrl;

            _context.SaveChanges();
            ViewBag.Url = lastUrl;

            return RedirectToAction("GetUrlForSurvey", "UserSurvey", new { url = lastUrl });
        }
        public IActionResult GetUrlForSurvey(string? url, int? surveyId)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "User");
            }
            if (!string.IsNullOrEmpty(url))
            {
                ViewBag.Url = url;
                return View();

            }
            var survey = _context.UsersSurveys.Find(surveyId);
            ViewBag.Url = survey.UniuqeLink;
            return View();
        }
        public IActionResult ViewUserSurveys()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var surveys = _context.UsersSurveys.ToList();



            return View(surveys);
        }
        //public IActionResult ViewAnswerUserSurvey(int userSurveyId)
        //{
        //    int? id = HttpContext.Session.GetInt32("UserId");
        //    if (!id.HasValue)
        //    {
        //        return RedirectToAction("UserLogin", "User");
        //    }

        //    //var survey


        //    return View();
        //}
        [HttpGet("UserSurvey/ViewSurveyWithUrl/{url?}")]
        public IActionResult ViewSurveyWithUrl([FromRoute] string url)
        {

            var survey = _context.UsersSurveys.Where(p => p.UniuqeLink == "https://localhost:44380/UserSurvey/ViewSurveyWithUrl/" + url).FirstOrDefault();
            if (survey == null)
            {
                return RedirectToAction("GuestIndex", "Guest");
            }

            return View(survey);
        }

        [HttpGet]
        public IActionResult AnswerUserSurvey(string url)
        {
            var survey = _context.UsersSurveys.Where(p => p.UniuqeLink == url).FirstOrDefault();

            var questions = _context.UserSurveysQuestions.Where(p => (p.UserSurveys.UniuqeLink == url && p.QuestionType != "Test")).Select(p => new SurveyQuestions
            {
                QuestionName = p.QuestionText,
                QuestionId = p.UserSurveyQuestionsId

            }).ToList();
            var test = _context.UserSurveysQuestions.Where(p => (p.UserSurveys.UniuqeLink == url && p.QuestionType == "Test")).Select(p => new TestQuestions
            {
                TestQuestionText = p.QuestionText,
                TestId = p.UserSurveyQuestionsId
            }).ToList();

            foreach (var item in test)
            {
                var opts = _context.UserSurveyOptions.Where(p => p.UserSurveyQuestionsId == item.TestId).Select(x => x.OptionText).ToList();
                item.opt1 = opts[0];
                item.opt2 = opts[1];
                item.opt3 = opts[2];
                item.opt4 = opts[3];
            }

            ViewSurveyWithUrlViewModel surveyModel = new ViewSurveyWithUrlViewModel()
            {
                CreatedDate = survey.CreatedDate,
                Questions = questions,
                TestQuestions = test,
                SurveyDescription = survey.Description,
                SurveyName = survey.SurveyName,
                UserSurveyId = survey.UserSurveyId
            };
            return View(surveyModel);
        }
        [HttpPost]
        public IActionResult AnswerUserSurvey(UserSurveyCompleteDTO model)
        {
            _context.UserSurveyAnswers.Add(new UserSurveyAnswer()
            {
                UserSurveysId = model.UserSurveyId

            });
            var survey = _context.UsersSurveys.Find(model.UserSurveyId);
            survey!.SurveyCompletionCount++;
            _context.SaveChanges();

            var lastSurveyAnswerId = _context.UserSurveyAnswers.OrderByDescending(p => p.UserSurveysId).FirstOrDefault()!.UserSurveyAnswerId;


            foreach (var testAnswer in model.TestAnswers)
            {
                var question = _context.UserSurveysQuestions.Find(testAnswer.Key);
                _context.UserQuestionAnswers.Add(new UserQuestionAnswer
                {
                    SurveyId = survey.UserSurveyId,
                    QuestionId = testAnswer.Key,
                    SurveyAnswerId = lastSurveyAnswerId,
                    OptionText = testAnswer.Value,
                    QuestionText = question!.QuestionText

                });
            }
            foreach (var openAnswer in model.OpenAnswers)
            {
                var question = _context.UserSurveysQuestions.Find(openAnswer.Key);

                _context.UserQuestionAnswers.Add(new UserQuestionAnswer
                {
                    AnswerText = openAnswer.Value,
                    QuestionId = openAnswer.Key,
                    SurveyAnswerId = lastSurveyAnswerId,
                    SurveyId = survey.UserSurveyId,
                    QuestionText = question!.QuestionText
                });
            }
            _context.SaveChanges();


            return RedirectToAction("GuestIndex", "Guest");
        }
    }
}
