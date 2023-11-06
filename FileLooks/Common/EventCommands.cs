using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonLib.Common
{
    public interface IEventAction
    {
        string EventName { get; }
    }

    public static class EventCommands
    {
        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.RegisterAttached("Events", typeof(IEnumerable<IEventAction>), typeof(EventCommands), new PropertyMetadata(null, OnCommandChanged));

        public static IEnumerable<IEventAction> GetEvents(DependencyObject obj)
        {
            return (IEnumerable<IEventAction>)obj.GetValue(EventsProperty);
        }

        public static void SetEvents(DependencyObject obj, IEnumerable<IEventAction> value)
        {
            obj.SetValue(EventsProperty, value);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IEnumerable eventActions)
            {
                foreach (IEventAction eventAction in eventActions)
                {
                    if (!string.IsNullOrEmpty(eventAction.EventName))
                    {
                        EventInfo eventInfo = d.GetType().GetEvent(eventAction.EventName);
                        if (eventInfo == null)
                        {
                            throw new Exception($"没有找到名称为{eventAction.EventName}的事件");
                        }
                        Delegate @delegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventAction, "Event");
                        //Delegate @delegate2 = eventAction.Begin(eventInfo.EventHandlerType, typeof(object), typeof(MouseButtonEventArgs));
                        //Delegate @delegate = DelegateBuilder.CreateDelegate(eventAction, "Event", eventInfo.EventHandlerType, BindingFlags.NonPublic);
                        eventInfo.AddEventHandler(d, @delegate);
                    }
                    else
                    {
                        throw new Exception($"事件名不能为空");
                    }
                }
            }
        }
    }

    public class EventAction<TSender, TE> : IEventAction
    {
        private readonly Action<TSender, TE> action;

        private readonly string eventName;

        public EventAction(string eventName, Action<TSender, TE> action)
        {
            this.eventName = eventName;
            this.action = action;
        }

        public string EventName => this.eventName;

        private void Event(TSender sender, TE e)
        {
            this.action?.Invoke(sender, e);
        }
    }

    public class EventAction<TE> : IEventAction
    {
        private readonly Action<object> action;

        private readonly Action<TE> action2;

        private readonly string eventName;

        public EventAction(string eventName, Action<object> action)
        {
            this.eventName = eventName;
            this.action = action;
        }

        public EventAction(string eventName, Action<TE> action)
        {
            this.eventName = eventName;
            this.action2 = action;
        }

        public string EventName => this.eventName;

        private void Event(object sender, TE e)
        {
            this.action?.Invoke(sender);
            this.action2?.Invoke(e);
        }
    }
}
