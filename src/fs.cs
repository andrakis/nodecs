using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeCS
{
    /// <summary>
    /// Filesystem functions
    /// </summary>
    public static class fs
    {
        public delegate void readFileCallback(Exception err, byte[] data);

        /// <summary>
        /// Read a file asynchronously
        /// </summary>
        /// <param name="filename">File to read</param>
        /// <param name="encoding">Not used</param>
        /// <param name="callback">Callback that takes Exception, byte[]</param>
        public static void readFile(string filename, string encoding, readFileCallback callback)
        {
            Async.run(delegate()
            {
                try
                {
                    byte[] contents = System.IO.File.ReadAllBytes(filename);
                    callback(null, contents);
                }
                catch (Exception ex)
                {
                    callback(ex, null);
                }
            });
        }
        public static void readFile(string filename, readFileCallback callback) {
            readFile(filename, "ascii", callback);
        }
    }
}
