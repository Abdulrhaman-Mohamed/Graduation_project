using Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IBlogService
    {
        ICollection<Posts> GetPosts(int page, int pagesize);
        Task<Posts> Add(Posts blogs);
    }
}
