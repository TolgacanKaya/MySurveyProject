using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MySurveyProject.Model
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSurveys> UsersSurveys { get; set; }
        public DbSet<UserSurveyQuestions> UserSurveysQuestions { get; set; }
        public DbSet<UserSurveyOption> UserSurveyOptions { get; set; }
        public DbSet<UserSurveyAnswer> UserSurveyAnswers { get; set; }
        public DbSet<UserQuestionAnswer> UserQuestionAnswers { get; set; }




    }
}
