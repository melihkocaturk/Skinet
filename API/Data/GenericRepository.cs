using API.Data.Interfaces;
using API.Data.Specifications;
using API.Entities;

namespace API.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            this._context = context;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IReadOnlyList<T> ListAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetEntityWithSpec(ISpecification<T> spec)
        {
            return ApplySpecification(spec).FirstOrDefault();
        }

        public IReadOnlyList<T> List(ISpecification<T> spec)
        {
            return ApplySpecification(spec).ToList();
        }

        public int Count(ISpecification<T> spec)
        {
            return ApplySpecification(spec).Count();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
