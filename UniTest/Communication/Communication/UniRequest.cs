using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Communication
{
    public class UniRequest
    {
        
        public async void testPost(HttpClient client, Task task){
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "task");
            var jsonObject = JsonConvert.SerializeObject(task);
            Logger.log(jsonObject);
            req.Content = new StringContent(jsonObject, Encoding.UTF8,"application/json");
            //Logger.log(req.Content.ToString());
            try
            {
                Logger.log("before await nr1");
                HttpResponseMessage response = await client.SendAsync(req);
                Logger.log("between await 1 and 2");
                string responseString = await response.Content.ReadAsStringAsync();
                Logger.log("after await 2");
                Logger.log(responseString);
            }
            catch (Exception e)
            {
                Logger.log("Problem with posting: " + e.Message);
            }
        }
    }
}
