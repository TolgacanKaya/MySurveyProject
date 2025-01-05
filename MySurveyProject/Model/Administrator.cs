using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MySurveyProject.Model
{
    public class Administrator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string AdminEmail { get; set; }
        [Required]
        public string AdminPassword { get; set; }
        [Required]
        public string AdminEmailPassword { get; set; }
    }
}
