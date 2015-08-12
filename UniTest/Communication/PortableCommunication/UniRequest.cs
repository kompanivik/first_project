using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Communication
{
    public delegate void stringList(List<string> sl);
    public delegate void taskList(List<Task> tl);
    public class UniRequest
    {
        public event taskList tasksEvent;
        public event stringList projectsEvent;
        public async void PostTask(HttpClient client, Task task){
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "task");
            putOrPostTask(client, req, task);
        }

        public async void PutTask(HttpClient client, Task task)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Put, "task");
            putOrPostTask(client, req, task);
        }

        private async void putOrPostTask(HttpClient client, HttpRequestMessage req, Task task)
        {
            var jsonObject = JsonConvert.SerializeObject(task);
            Logger.log(jsonObject);
            req.Content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            Status status;
            try
            {
                HttpResponseMessage response = await client.SendAsync(req);
                string responseString = await response.Content.ReadAsStringAsync();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Status));
                status = (Status)jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseString)));
                Logger.log(responseString);
                if (status.status)
                {
                    GetTasks(client);
                }
            }
            catch (Exception e)
            {
                Logger.log("Problem with posting: " + e.Message);
            }
        }

        public async void GetTask(HttpClient client, int taskId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "task/" + taskId);
            Task task;
            try
            {
                HttpResponseMessage response = await client.SendAsync(req);
                string responseString = await response.Content.ReadAsStringAsync();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Task));
                task = (Task)jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseString)));
                Logger.log(responseString);
            }
            catch (Exception e)
            {
                Logger.log("Problem with getting spesific task: " + e.Message);
            }
        }

        public async void GetTasks(HttpClient client)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "tasks");
            List<Task> tasks;
            try
            {
                HttpResponseMessage response = await client.SendAsync(req);
                string responseString = await response.Content.ReadAsStringAsync();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Task>));
                tasks = (List<Task>)jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseString)));
                Logger.log(responseString);
                tasksEvent(tasks);
            }
            catch (Exception e)
            {
                Logger.log("Problem with getting tasks: " + e.Message);
            }
        }

        public async void GetTasks(HttpClient client, string project)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "tasks/project/" + project);
            List<Task> tasks;
            try
            {
                HttpResponseMessage response = await client.SendAsync(req);
                string responseString = await response.Content.ReadAsStringAsync();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Task>));
                tasks = (List<Task>)jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseString)));
                Logger.log(responseString);
            }
            catch (Exception e)
            {
                Logger.log("Problem with getting tasks from project: " + e.Message);
            }
        }

        public async void DeleteTask(HttpClient client, int taskId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Delete, "task/" + taskId);
            Status status;
            try
            {
                HttpResponseMessage response = await client.SendAsync(req);
                string responseString = await response.Content.ReadAsStringAsync();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Status));
                status = (Status)jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseString)));
                Logger.log(responseString);
                if(status.status){
                    GetTasks(client);
                }
            }
            catch (Exception e)
            {
                Logger.log("Problem with getting tasks: " + e.Message);
            }
        }

        public async void getProjects(HttpClient client)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "projects");
            List<string> projects;
            try
            {
                HttpResponseMessage response = await client.SendAsync(req);
                string responseString = await response.Content.ReadAsStringAsync();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<string>));
                projects = (List<string>)jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseString)));
                Logger.log(responseString);
                projectsEvent(projects);
            }
            catch (Exception e)
            {
                Logger.log("Problem with getting projects: " + e.Message);
            }
            
        }

    }
}
