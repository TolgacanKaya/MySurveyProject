using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySurveyProject.Model
{
    public class SurveyAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SurveyAnsweId { get; set; }
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
