using Microsoft.AspNetCore.Mvc;
using MySurveyProject.Model;
using MySurveyProject.ViewModels;

namespace MySurveyProject.Controllers
{
    public class GuestController : Controller
    {
        private readonly AppDbContext _context;

        public GuestController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult GuestIndex()
        {
            var surveys = _context.Surveys.Where(p => p.Activity == true).ToList();


            return View(surveys);
        }


        [HttpGet]
        public IActionResult CompleteSurvey(int surveyId)
        {
            var survey = _context.Surveys.Find(surveyId);

            var questions = _context.Questions.Where(p => (p.SurveyId == surveyId && p.QuestionType != "Test")).Select(p => new SurveyQuestions
            {
                QuestionName = p.QuestionText,
                QuestionId = p.QuestionId

            }).ToList();
            var test = _context.Questions.Where(p => (p.SurveyId == surveyId && p.QuestionType == "Test")).Select(p => new TestQuestions
            {
                TestQuestionText = p.QuestionText,
                TestId = p.QuestionId,
            }).ToList();

            foreach (var item in test)
            {
                var opts = _context.Options.Where(p => p.QuestionId == item.TestId).Select(x => x.OptionText).ToList();
                item.opt1 = opts[0];
                item.opt2 = opts[1];
                item.opt3 = opts[2];
                item.opt4 = opts[3];
            }

            SurveyModel surveyModel = new SurveyModel()
            {
                SurveyTitle = survey.SurveyName,
                SurveyId = surveyId,
                Questions = questions,
                TestQuestions = test
            };
            return View(surveyModel);
        }
        [HttpPost]
        public IActionResult CompleteSurvey(SurveyAnswerModel model, int surveyId)
        {
            _context.SurveyAnswers.Add(new SurveyAnswer()
            {
                SurveyId = surveyId

            });
            var survey = _context.Surveys.Find(surveyId);
            survey!.SurveyCompletionCount++;
            _context.SaveChanges();

            var lastSurveyAnswerId = _context.SurveyAnswers.OrderByDescending(p => p.SurveyAnsweId).FirstOrDefault()!.SurveyAnsweId;


            foreach (var testAnswer in model.TestAnswers)
            {
                var question = _context.Questions.Find(testAnswer.Key);
                _context.QuestionAnswers.Add(new QuestionAnswer
                {
                    SurveyId = surveyId,
                    QuestionId = testAnswer.Key,
                    SurveyAnswerId = lastSurveyAnswerId,
                    OptionText = testAnswer.Value,
                    QuestionText = question!.QuestionText
                });
            }
            foreach (var openAnswer in model.OpenAnswers)
            {
                var question = _context.Questions.Find(openAnswer.Key);

                _context.QuestionAnswers.Add(new QuestionAnswer
                {
                    AnswerText = openAnswer.Value,
                    QuestionId = openAnswer.Key,
                    SurveyAnswerId = lastSurveyAnswerId,
                    SurveyId = surveyId,
                    QuestionText = question!.QuestionText
                });
            }
            _context.SaveChanges();


            return RedirectToAction("GuestIndex", "Guest");
        }
    }
}

