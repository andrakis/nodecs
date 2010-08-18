using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeCS
{
    public abstract class Async
    {
        protected delegate void AsyncCallback();
        private readonly System.Threading.Thread thread;

        protected Async(AsyncCallback callback)
        {
            this.thread = new System.Threading.Thread(() => callback());
        }

        public void run()
        {
            this.thread.Start();
        }
    }
}
