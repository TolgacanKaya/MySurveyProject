using Microsoft.AspNetCore.Mvc;
using MySurveyProject.Model;
using MySurveyProject.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MySurveyProject.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public StatisticsController(IHttpClientFactory httpClientFactory, AppDbContext context)
        {

            _httpClient = httpClientFactory.CreateClient();
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewTotalRecord()
        {
            var response = await _httpClient.GetAsync("http://127.0.0.1:8000/total-records");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TotalRecordsDTO>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return View(result);
        }
        public async Task<IActionResult> QuestionResponseCount()
        {
            var response = await _httpClient.GetAsync("http://127.0.0.1:8000/question-response-count");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<QuestionResponsesCountDTO>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            List<KeyValuePair<string, int>> updatedResult = new List<KeyValuePair<string, int>>();

            foreach (var item in result.question_response_count)
            {
                int questionId = Convert.ToInt32(item.Key);
                var question = _context.Questions.Find(questionId);

                if (question != null)
                {
                    updatedResult.Add(new KeyValuePair<string, int>(question.QuestionText, item.Value));
                }
            }

            return View(updatedResult);
        }
        public async Task<IActionResult> AnswerTextFrequency()
        {
            var response = await _httpClient.GetAsync("http://127.0.0.1:8000/answer-text-frequency");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AnswerTextFrequencyResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(result.answer_text_frequency);
        }
        public async Task<IActionResult> SummaryAnswersAndQuestions()
        {

            var response = await _httpClient.GetAsync("http://127.0.0.1:8000/question-summary");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SummaryAnswersAndQuestionsDTO>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true

            });


            return View(result);
        }
        public async Task<IActionResult> SurveyResponseCount()
        {
            var response = await _httpClient.GetAsync("http://127.0.0.1:8000/survey-response-count/");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SurveyResponseCountDTO>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var QuestionTextIds = result.survey_response_count.Keys.ToList();
            List<KeyValuePair<string, string>> Question = new List<KeyValuePair<string, string>>();
            foreach (var item in QuestionTextIds)
            {
                var resultValue = result.survey_response_count[item];
                var question = await _context.UserSurveysQuestions.FindAsync(Convert.ToInt32(item));
                Question.Add(new KeyValuePair<string, string>(question.QuestionText, resultValue.ToString()));
            }


            return View(Question);
        }
    }
}
