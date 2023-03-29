﻿using Repo_Core.Identity_Models;

namespace Repo_Core.Services
{
    public interface IBlogService
    {
        ICollection<Posts> GetPosts(int page, int pagesize);
        Task<Posts> Add(Posts blogs);
    }
}
