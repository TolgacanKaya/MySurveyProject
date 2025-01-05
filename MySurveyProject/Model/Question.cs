using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MySurveyProject.Model
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        [Required]
        [StringLength(200)]
        public string QuestionText { get; set; }
        [Required]
        [StringLength(30)]
        public string QuestionType { get; set; }
        [Required]
        public bool Activity { get; set; }
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
