using MySurveyProject.Model;

namespace MySurveyProject.ViewModels
{
    public class SurveyModel
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public List<SurveyQuestions> Questions { get; set; }
        public List<TestQuestions> TestQuestions { get; set; }




    }
}
