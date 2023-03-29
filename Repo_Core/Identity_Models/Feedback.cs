namespace Repo_Core.Identity_Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public string comment { get; set; }

        public DateTime feedbacktime { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public int PostId { get; set; }
        public virtual Posts? Post { get; set; }
    }
}
