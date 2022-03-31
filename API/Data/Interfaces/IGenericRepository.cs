using API.Data.Specifications;
using API.Entities;

namespace API.Data.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IReadOnlyList<T> ListAll();
        T GetEntityWithSpec(ISpecification<T> spec);
        IReadOnlyList<T> List(ISpecification<T> spec);
        int Count(ISpecification<T> spec);
    }
}
