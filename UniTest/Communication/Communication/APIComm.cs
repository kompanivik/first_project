using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Communication
{
    class APIComm
    {
        const int POST = 0;
        const int GET = 1;
        const int PUT = 2;
        const int DELETE = 3;

        readonly HttpClient hClient = new HttpClient();

        const string BASEADDRESS = "http://uniskill.cloudapp.net/taskservice/";
        const string ACCESSTOKEN = "8c244db574105683730a2673e77aed67";

        APIComm()
        {
            hClient.BaseAddress = new Uri(BASEADDRESS);
            hClient.DefaultRequestHeaders.Add("X-Uni-Secret", ACCESSTOKEN);
            Request req = new Request();
            Task task = new Task();
            task.completed = false;
            task.description = "A nice Task";
            task.duedate = "2012-03-26T12:21:24";

            req.testPost(hClient, );
        }
        
    }
}
