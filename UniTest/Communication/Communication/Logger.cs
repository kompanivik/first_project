using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Communication
{
    class Logger
    {
        private bool isLogging = true;
        public void log(string text)
        {
            if (isLogging)
            {
                Debug.WriteLine(text);
            }
        }
    }
}
