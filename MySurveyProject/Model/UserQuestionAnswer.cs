using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MySurveyProject.Model
{
    public class UserQuestionAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserQuestionAnswerId { get; set; }

        public int SurveyAnswerId { get; set; }

        public int SurveyId { get; set; }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string? AnswerText { get; set; }
        public string? OptionText { get; set; }
    }
}
