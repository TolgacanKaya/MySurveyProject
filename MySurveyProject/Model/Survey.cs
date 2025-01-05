using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MySurveyProject.Model
{
    public class Survey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SurveyId { get; set; }
        [Required]
        [StringLength(100)]
        public string SurveyName { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool Activity { get; set; }
        [Required]
        public bool Completable { get; set; }
        public int SurveyCompletionCount { get; set; }
    }
}
