using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeCS
{
    /// <summary>
    /// A standard callback.
    /// </summary>
    public delegate void Callback();

    /// <summary>
    /// Call a function asynchronously.
    /// </summary>
    public static class Async
    {
        /// <summary>
        /// Run the specified callback in a separate thread.
        /// </summary>
        /// <param name="callback"></param>
        public static void run(Callback callback)
        {
            new System.Threading.Thread(() => callback()).Start();
        }
    }
}
