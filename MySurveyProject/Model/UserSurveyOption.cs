using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MySurveyProject.Model
{
    public class UserSurveyOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OptionId { get; set; }

        [Required]

        public string OptionText { get; set; }

        [ForeignKey("UserQuestionQuestions")]
        public int UserSurveyQuestionsId { get; set; }

        public UserSurveyQuestions UserSurveyQuestions { get; set; }
    }
}


