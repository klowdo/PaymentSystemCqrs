using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystem.ReadModel;

namespace PaymentSystem.Infrastructure
{
    public class InMemoryProjectionRepo<TProjection>:IProjectionRepository<TProjection> where TProjection : IProjection
    {
        private IDictionary<Guid,TProjection > _projections = new Dictionary<Guid,TProjection>();

        public Task<TProjection> GetAsync(Guid id)
        {
            return _projections.TryGetValue(id, out var model) 
                ? Task.FromResult(model) : Task.FromResult(default(TProjection));
        }

        public Task SaveAsync(TProjection model)
        {
            _projections[model.ProjectionId] = model;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TProjection>> GetAll()
        {
            return Task.FromResult(_projections.Values.AsEnumerable());
        }
    }
}