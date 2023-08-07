namespace LibraryApp.Web.Repository.IRepository;

public interface IRepositoryHttp<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string controller);
    Task<T> GetAsync(string controller, int id);
    Task UpdatePostAsync(string controller, T entity, int entityId);
    Task DeleteAsync(string controller, int id);
    Task CreatePostAsync(string controller, T entity);

}
