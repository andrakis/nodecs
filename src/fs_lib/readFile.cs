using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeCS.fs_lib
{
    public class readFile : Async
    {
        public readFile(string filename, string encoding, fs.readFileCallback callback) :
            base(delegate() {
                try
                {
                    byte[] contents = System.IO.File.ReadAllBytes(filename);
                    callback(null, contents);
                }
                catch (Exception ex)
                {
                    callback(ex, null);
                }
            })
        {}
    }
}
