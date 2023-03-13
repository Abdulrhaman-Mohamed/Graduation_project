namespace Repo_Core.Interface
{
    public interface IRegsiter<T> where T : class
    {
        T Regsiter(T info);
        T GetUser(int id);
    }
}
