﻿// Copyright (c) 2013 Jae-jun Kang
// See the file COPYING for license details.

using System;
using System.Collections.Generic;
using System.Threading;

using x2.Events;

namespace x2.Flows {
  public class ThreadlessFlow : Flow {
    protected readonly IBlockingQueue<Event> queue;
    protected readonly object syncRoot;
    protected bool running;

    public ThreadlessFlow(IBlockingQueue<Event> queue)
        : base(new Binder()) {
      this.queue = queue;
      syncRoot = new Object();
      running = false;
    }

    protected internal override void Feed(Event e) {
      queue.Enqueue(e);
    }

    public override void StartUp() {
      lock (syncRoot) {
        if (running) {
          return;
        }
        SetUp();
        caseStack.SetUp();
        currentFlow = this;
        handlerChain = new List<Handler>();

        running = true;

        queue.Enqueue(new FlowStartupEvent());
      }
    }

    public override void ShutDown() {
      lock (syncRoot) {
        if (!running) {
          return;
        }
        queue.Close(new FlowShutdownEvent());
        running = false;

        handlerChain = null;
        currentFlow = null;
        caseStack.TearDown();
        TearDown();
      }
    }

    public void TryDispatch() {
      Event e;
      if (queue.TryDequeue(out e)) {
        base.Dispatch(e);
      }
    }
  }
}