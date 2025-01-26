namespace MySurveyProject.ViewModels
{
    public class ViewSurveyWithUrlViewModel
    {
        public int UserId { get; set; }
        public int UserSurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<SurveyQuestions> Questions { get; set; }
        public List<TestQuestions> TestQuestions { get; set; }
       

    }
}
