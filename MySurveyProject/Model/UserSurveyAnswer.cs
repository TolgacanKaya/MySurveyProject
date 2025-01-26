using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MySurveyProject.Model
{
    public class UserSurveyAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserSurveyAnswerId { get; set; }
        [ForeignKey("UserSurveys")]
        public int UserSurveysId { get; set; }
        public UserSurveys UserSurveys { get; set; }
    }
}

