using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentSystem.Infrastructure;
using PaymentSystem.Infrastructure.Services;

namespace PaymentSystem.Portal.Services
{
    public class EventProcessor : BackgroundService
    {
        private readonly IEventQueue _eventQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IEventQueue eventQueue)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _eventQueue = eventQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!_eventQueue.IsEmpty)
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var publisher = scope.ServiceProvider.GetService<IEventPublisher>();
                        while (!_eventQueue.IsEmpty)
                            if (_eventQueue.TryDequeue(out var evt))
                                await publisher.PublishAsync(evt);
                    }

                await Task.Delay(TimeSpan.FromMilliseconds(200), stoppingToken);
            }
        }
    }
}