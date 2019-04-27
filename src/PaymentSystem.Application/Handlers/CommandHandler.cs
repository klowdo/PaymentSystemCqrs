using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PaymentSystem.Domain;

namespace PaymentSystem.Application.Handlers
{
    public abstract class CommandHandler<T, TId> where T : class, IAggregate
    {
        private readonly IAggregateRepository<T> _repo;

        protected CommandHandler(IAggregateRepository<T> repo)
        {
            _repo = repo;
        }

        protected async Task PerformAsync(Command<TId> cmd, Action<T> action)
        {
            var aggregate = await _repo.GetByIdAsync(cmd.AggregateId);
            var version = cmd.Version == 0 ? null : cmd.Version;
            if (cmd.Version != null && cmd.Version.Value > 0 && cmd.Version != aggregate.Version)
                Trace.TraceWarning(
                    $"The supplied aggregate version {cmd.Version} is different from the loaded aggregate version {aggregate.Version}.");
            action(aggregate);
            await SaveAsync(aggregate, version ?? aggregate.Version);
        }

        protected async Task PerformAsync(Command<TId> cmd, Func<T, Task> action)
        {
            var aggregate = await _repo.GetByIdAsync(cmd.AggregateId);
            var version = cmd.Version == 0 ? null : cmd.Version;
            if (cmd.Version != null && cmd.Version.Value > 0 && cmd.Version != aggregate.Version)
                Trace.TraceWarning(
                    $"The supplied aggregate version {cmd.Version} is different from the loaded aggregate version {aggregate.Version}.");
            await action(aggregate);
            await SaveAsync(aggregate, version ?? aggregate.Version);
        }

        protected async Task SaveAsync(T aggregate, long version)
        {
            await _repo.SaveAsync(aggregate, version);
        }
    }
}