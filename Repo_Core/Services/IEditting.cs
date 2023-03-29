using Repo_Core.Identity_Models;

namespace Repo_Core.Services
{
    public interface IEditting
    {
        public Task Editting(ApplicationUser info);
        public string Addfeedback(Feedback feed);
    }
}
