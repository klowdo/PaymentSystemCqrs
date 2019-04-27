using System;
using System.Threading.Tasks;
using PaymentSystem.ReadModel.Projections;

namespace PaymentSystem.ReadModel.Services
{
    public abstract class ViewModelUpdateBase<TProjection> where TProjection : IProjection, new()
    {
        protected readonly IProjectionRepository<TProjection> Repo;

        protected ViewModelUpdateBase(IProjectionRepository<TProjection> repo)
        {
            Repo = repo;
        }

        protected async Task Update(Guid id, Action<TProjection> action)
        {
            var model = await Repo.GetAsync(id);
            if (model == null)
                model = new TProjection();
            action(model);
            await Repo.SaveAsync(model);
        }
    }
}