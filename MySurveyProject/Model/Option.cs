using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MySurveyProject.Model
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OptionId { get; set; }

        [Required]
        [StringLength(100)]
        public string OptionText { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
