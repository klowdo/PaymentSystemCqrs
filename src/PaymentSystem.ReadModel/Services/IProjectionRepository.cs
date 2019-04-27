using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentSystem.ReadModel.Projections;

namespace PaymentSystem.ReadModel.Services
{
    public interface IProjectionRepository<TProjection> where TProjection : IProjection
    {
        Task<TProjection> GetAsync(Guid id);
        Task SaveAsync(TProjection model);
        Task<IEnumerable<TProjection>> GetAll();
    }
}