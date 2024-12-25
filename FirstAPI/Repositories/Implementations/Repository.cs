using FirstAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FirstAPI.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
            var a = new List<int>();
        }

        public IQueryable<Category> GetAll(
            Expression<Func<Category,
            bool>>? expression = null,
            int skip = 0,
            int take = 0,
            Expression<Func<Category, object>>? orderExpression = null,
            bool isDescending = false,
            bool isTracking = false,
            params string[]? includes)
        {

            IQueryable<Category> query = _context.Categories.AsNoTracking();


            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if(orderExpression != null)
            {
                if (isDescending)
                {
                    query = query.OrderByDescending(orderExpression);
                }
                else
                {
                    query = query.OrderBy(orderExpression);
                }
            }
            query = query.Skip(skip);
            if(take != 0)
            {
                query = query.Take(take);
            }
           
           return isTracking?query:query.AsNoTracking(); 

        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
           await _context.Categories.AddAsync(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void Update(Category category)
        {
           _context.Categories.Update(category);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
