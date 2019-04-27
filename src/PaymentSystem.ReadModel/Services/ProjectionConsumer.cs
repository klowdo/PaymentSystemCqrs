using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using MediatR;
using PaymentSystem.Domain;

namespace PaymentSystem.ReadModel
{
    public class ProjectionConsumer<T> where T : Event<T>
    {
        private readonly object _projection;
        private readonly IEnumerable<MethodInfo> _allMethods;

        public ProjectionConsumer(object projection) {
            _projection = projection;

            _allMethods = _projection.GetType().GetMethods().Where(m => m.Name == nameof(INotificationHandler<Event>.Handle) && m.GetParameters().Length == 1);

#if DEBUG
            // This check is only done in debug, it is also done in unit testing. Make sure all projections used with this class are tested only to consume one type of events!
            // Check for consumes of wrong type!
            var parameters = _allMethods.Select(m => m.GetParameters().First());

            var nonMatchingParameter = parameters.FirstOrDefault(p => !p.ParameterType.IsSubclassOf(typeof(T)));
            if (nonMatchingParameter != null) {
                throw new Exception($"This ProjectionConsumer can only handle Consume methods consuming {typeof(T).Name} events. Type {nonMatchingParameter.ParameterType.FullName} is not valid!");
            }
#endif
        }

        public void Consume(Event evt) {
            var type = evt.GetType();
            var method = _allMethods.FirstOrDefault(m => m.GetParameters().First().ParameterType == type);

            if (method == null) return;

            method.Invoke(_projection, new object[] { evt, CancellationToken.None });
        }
    }
}