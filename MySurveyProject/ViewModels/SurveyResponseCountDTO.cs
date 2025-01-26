namespace MySurveyProject.ViewModels
{
    public class SurveyResponseCountDTO
    {
        public string status { get; set; }
        public Dictionary<string, int> survey_response_count { get; set; }
    }
}

