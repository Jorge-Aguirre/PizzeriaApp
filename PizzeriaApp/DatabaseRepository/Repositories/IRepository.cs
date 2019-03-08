using System.Collections.Generic;

namespace DatabaseRepository.Repositories
{
    public interface IRepository<T>
    {
        ICollection<T> GetAll();

        T GetById(int id);

        void Save(T entity);

        void Update(T entity);
    }
}
