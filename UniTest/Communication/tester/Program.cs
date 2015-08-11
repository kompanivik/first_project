using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;

namespace tester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                APIComm api = new APIComm();
                
            }
            catch (Exception e)
            {
                Logger.log(e.Message);
            }
            
        }
    }
}
