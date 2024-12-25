using System.Linq.Expressions;

namespace FirstAPI.Repositories.Interfaces
{
    public interface IRepository
    {
        IQueryable<Category> GetAll(
            Expression<Func<Category,
            bool>>? expression=null,
            int skip = 0,
            int take = 0,
            Expression<Func<Category,object>>? orderExpression = null,
            bool isDescending = false,
            bool isTracking=false,
            params string[]? includes);
        Task<Category> GetByIdAsync(int id);

        Task AddAsync(Category category);

        void Delete(Category category);
        void Update(Category category); 
        Task<int> SaveChangesAsync();

    }
}
