namespace MySurveyProject.ViewModels
{
    public class QuestionResponsesCountDTO
    {
        public string Status { get; set; }
        public Dictionary<string, int> question_response_count { get; set; }
    }
}
