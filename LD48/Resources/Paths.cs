using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LD48
{
    class Paths
    {
        public static string level;

        internal static void Initialize(string contentPath)
        {
            level = Path.Combine(contentPath, "Levels/level_{0}.bin");
        }
    }
}
