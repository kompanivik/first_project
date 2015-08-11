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
    public class APIComm
    {
        const int POST = 0;
        const int GET = 1;
        const int PUT = 2;
        const int DELETE = 3;

        readonly HttpClient hClient;

        const string BASEADDRESS = "http://uniskill.cloudapp.net/taskservice/";
        const string ACCESSTOKEN = "8c244db574105683730a2673e77aed67";

        public APIComm()
        {
            hClient = new HttpClient();
            hClient.BaseAddress = new Uri(BASEADDRESS);
            hClient.DefaultRequestHeaders.Add("X-Uni-Secret", ACCESSTOKEN);
            Logger.log("APIComm has been constructed");
            
        }
        public async void test()
        {
            UniRequest req = new UniRequest();
            Task task = new Task();
            task.completed = false;
            task.description = "A nice Task";
            task.duedate = "2012-03-26T12:21:24";
            task.id = 4;
            task.project = "testProject";
            req.testPost(hClient, task);
        }
        
    }
}
