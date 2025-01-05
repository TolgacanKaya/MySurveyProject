using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MySurveyProject.Model
{
    public class QuestionAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionAnswerId { get; set; }

        public int SurveyAnswerId { get; set; }

        public int SurveyId { get; set; }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string? AnswerText { get; set; }
        public string? OptionText { get; set; }






    }
}
