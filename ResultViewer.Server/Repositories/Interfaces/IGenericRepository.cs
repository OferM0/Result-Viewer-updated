namespace ResultViewer.Server.Repositories.Interfaces
{
    //The constraint where T : class ensures that the type parameter T must be a class
    public interface IGenericRepository<T> where T : class
    {
        // Get all entities of type T from the repository.
        Task<IEnumerable<T>> GetAll();

        // Get an entity of type T by its unique identifier.
        Task<T> GetById(object id);

        // Insert a new entity of type T into the repository.
        Task<T> Insert(T obj);

        // Update an existing entity of type T in the repository.
        Task<T> Update(T obj);

        // Delete an entity of type T from the repository by its unique identifier.
        Task Delete(object id);

        // Save any pending changes asynchronously.
        Task SaveAsync();
    }
}
