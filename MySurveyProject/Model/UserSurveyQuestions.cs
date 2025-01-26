using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySurveyProject.Model
{
    public class UserSurveyQuestions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserSurveyQuestionsId { get; set; }

        [Required]
        public string QuestionText { get; set; }
        [Required]
        [StringLength(30)]
        public string QuestionType { get; set; }
        [Required]
        public bool Activity { get; set; }
        [ForeignKey("UserSurveys")]
        public int UserSurveysId { get; set; }
        public UserSurveys UserSurveys { get; set; }

    }
}
