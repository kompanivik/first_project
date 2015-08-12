using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Communication
{ 
    /// <summary>
    /// this is the logger i have been using all along.
    /// Here i can easely switch all my logging on or off.
    /// </summary>
    public static class Logger
    {
        private static bool isLogging = true;
        public static void log(string text)
        {
            if (isLogging)
            {
                Debug.WriteLine(text);
            }
        }
    }
}
