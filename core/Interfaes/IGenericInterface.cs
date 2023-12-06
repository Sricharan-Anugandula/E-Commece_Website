using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Specifications;

namespace core.Interfaes
{
    public interface IGenericInterface<T> where T :BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(Ispecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(Ispecification<T> spec);
    }
}