using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NodeCS
{
    /// <summary>
    /// A timeout that can be cancelled if desired
    /// </summary>
    public class Timeout
    {
        public delegate void CallbackDelegate();

        protected Thread thread;
        protected bool cancelled;
        protected CallbackDelegate callback;
        protected int delay;

        public Timeout(CallbackDelegate callback, int delayMs)
        {
            this.callback = callback;
            this.delay = delayMs;
            this.schedule();
        }

        protected virtual void schedule()
        {
            Async.run(delegate()
            {
                Thread.Sleep(this.delay);
                if (false == this.cancelled)
                {
                    callback();
                }
            });
        }

        public virtual void cancel()
        {
            if (null != this.thread)
            {
                this.thread.Abort();
                this.thread = null;
            }
            this.cancelled = true;
        }
    }
}
