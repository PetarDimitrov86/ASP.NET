namespace Whisper.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> expression);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression);

        bool Any();
        bool Any(Expression<Func<T, bool>> expression);

        T Find(int id);

        int Count();

        int Count(Expression<Func<T, bool>> expression);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
