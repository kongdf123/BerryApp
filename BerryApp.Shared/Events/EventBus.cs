using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Shared.Events
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _handlers = new Dictionary<Type, List<Action<object>>>();

        public void Substribe<T>(Action<T> handler)
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
            {
                _handlers[type] = new List<Action<object>>();
            }

            _handlers[type].Add(e => handler((T)e));
        }

        public void Publish<T>(T @event)
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
            {
                return;
            }

            foreach (var handler in _handlers[type])
            {
                handler(@event);
            }
        }
    }
}
