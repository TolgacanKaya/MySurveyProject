namespace MySurveyProject.ViewModels
{
    public class SurveyAnswerModel
    {
        public int SurveyId { get; set; }
        public Dictionary<int, string> TestAnswers { get; set; } = new Dictionary<int, string>();

        

        public Dictionary<int, string> OpenAnswers { get; set; } = new Dictionary<int, string>();

    }
}
