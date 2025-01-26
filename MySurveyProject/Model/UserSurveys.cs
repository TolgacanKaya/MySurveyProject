using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySurveyProject.Model
{
    public class UserSurveys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserSurveyId { get; set; }
        [Required]

        public string SurveyName { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool Activity { get; set; }
        [Required]
        public bool Completable { get; set; }
        public int SurveyCompletionCount { get; set; }
        public string? UniuqeLink { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
