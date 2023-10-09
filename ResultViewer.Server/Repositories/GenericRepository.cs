using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.Repositories.Interfaces;
using System.Collections.Generic;

namespace ResultViewer.Server.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IDbContextFactory<AppDbContext> _contextFactory;
        private DbSet<T> entities;

        // Constructor to initialize the repository with the DbContextFactory.
        public GenericRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // Retrieve all entities of type T from the repository asynchronously.
        public async Task<IEnumerable<T>> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    entities = context.Set<T>(); // method of the DbContext class that returns a DbSet<T> representing the entity set for the specified type T.
                    return await entities.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        // Retrieve an entity of type T by its unique identifier asynchronously.
        public async Task<T> GetById(object id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                entities = context.Set<T>();
                return await entities.FindAsync(id);
            }
        }

        // Insert a new entity of type T into the repository asynchronously.
        public async Task<T> Insert(T obj)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                entities = context.Set<T>();
                var result = await entities.AddAsync(obj);
                return result.Entity;
            }
        }

        // Update an existing entity of type T in the repository asynchronously.
        public async Task<T> Update(T obj)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                entities = context.Set<T>();
                var result = entities.Attach(obj);
                context.Entry(obj).State = EntityState.Modified;
                await SaveAsync();
                return result.Entity;
            }
        }

        // Delete an entity of type T from the repository by its unique identifier asynchronously.
        public async Task Delete(object id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                entities = context.Set<T>();
                var entity = await GetById(id);
                context.Remove(entity);
                await SaveAsync();
            }
        }

        // Save any pending changes in the DbContext asynchronously.
        public async Task SaveAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
