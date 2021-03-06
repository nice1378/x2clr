﻿// Copyright (c) 2013 Jae-jun Kang
// See the file COPYING for license details.

using System;
using System.Collections;
using System.Collections.Generic;

namespace x2
{
    /// <summary>
    /// Cleanup helper base class for any event-consuming classes.
    /// </summary>
    /// <remarks>
    /// x2 event handlers are built on C# delegates. In case of an instance
    /// method delegate, it keeps a strong reference to the method target object.
    /// This means that when you bind an event with an instance method, the
    /// target object will never be garbage-collected as long as the handler
    /// delegate lives, resulting in undesirable memory leak.
    ///
    /// EventSink is here to handle the case. First, let your event-consuming
    /// class be derived from EventSink. When the object is no longer needed,
    /// call its CleanUp() method to ensure that all the event bindings to the
    /// object are removed so that the object is properly garbage-collected.
    ///
    /// An EventSink object should be initialized with a single specific flow.
    /// And an object instance of any EventSink-derived class should never be
    /// shared by two or more different flows. These are constraints by design.
    /// </remarks>
    public class EventSink
    {
        private readonly IList<Binder.Token> bindings;
        private WeakReference flow;

        public Flow Flow
        { 
            get
            {
                return flow.Target as Flow;
            }
            set
            {
                if (bindings.Count != 0)
                {
                    throw new InvalidOperationException();
                }
                this.flow = new WeakReference(value);
            }
        }

        public EventSink() : this(Flow.CurrentFlow) { }

        public EventSink(Flow flow)
        {
            bindings = new List<Binder.Token>();
            this.flow = new WeakReference(flow);
        }

        public void Bind<T>(T e, Action<T> handler)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Subscribe(e, handler);
            }
        }

        public void Bind<T>(T e, Action<T> handler, Predicate<T> predicate)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Subscribe(e, handler, predicate);
            }
        }

        public void Bind<T>(T e, Func<Coroutine, T, IEnumerator> handler)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Subscribe(e, handler);
            }
        }

        public void Bind<T>(T e, Func<Coroutine, T, IEnumerator> handler,
            Predicate<T> predicate)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Subscribe(e, handler, predicate);
            }
        }

        public void Unbind<T>(T e, Action<T> handler)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Unsubscribe(e, handler);
            }
        }

        public void Unbind<T>(T e, Action<T> handler, Predicate<T> predicate)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Unsubscribe(e, handler, predicate);
            }
        }

        public void Unbind<T>(T e, Func<Coroutine, T, IEnumerator> handler)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Unsubscribe(e, handler);
            }
        }

        public void Unbind<T>(T e, Func<Coroutine, T, IEnumerator> handler,
            Predicate<T> predicate)
            where T : Event
        {
            var flow = Flow;
            if (flow != null)
            {
                flow.Unsubscribe(e, handler, predicate);
            }
        }

        internal void AddBinding(Binder.Token binderToken)
        {
            bindings.Add(binderToken);
        }

        internal void RemoveBinding(Binder.Token binderToken)
        {
            bindings.Remove(binderToken);
        }

        public void CleanUp()
        {
            var flow = Flow;
            if (flow == null)
            {
                return;
            }
            foreach (var binding in bindings)
            {
                flow.Unsubscribe(binding);
            }
            bindings.Clear();
        }
    }
}
