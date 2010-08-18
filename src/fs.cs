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
            new fs_lib.readFile(filename, encoding, callback).run();
        }
        public static void readFile(string filename, readFileCallback callback) {
            readFile(filename, "ascii", callback);
        }
    }
}
