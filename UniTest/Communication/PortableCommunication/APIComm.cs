﻿using System;
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

        public event taskList updateTaskList;
        public event stringList updateProjectList;
        readonly HttpClient hClient;
        UniRequest req;

        const string BASEADDRESS = "http://uniskill.cloudapp.net/taskservice/";
        const string ACCESSTOKEN = "8c244db574105683730a2673e77aed67";

        public APIComm()
        {
            
            req = new UniRequest();
            hClient = new HttpClient();

            hClient.BaseAddress = new Uri(BASEADDRESS);
            hClient.DefaultRequestHeaders.Add("X-Uni-Secret", ACCESSTOKEN);

            req.tasksEvent += req_tasksEvent;
            req.projectsEvent += req_projectsEvent;
            Logger.log("APIComm has been constructed");
            
        }

        void req_projectsEvent(List<string> sl)
        {
            updateProjectList(sl);
        }

        void req_tasksEvent(List<Task> tl)
        {
            updateTaskList(tl);
        }

        public void Post(Task task)
        {
            req.PostTask(hClient, task);
        }
        public void Get(Task task)
        {
            req.GetTask(hClient, task.id);
        }
        public void GetAllTasks()
        {
            req.GetTasks(hClient);
        }

        public void GetFrom(string project)
        {
            req.GetTasks(hClient, project);
        }

        public void GetAllProjects()
        {
            req.getProjects(hClient);
        }

        public void Put(Task task)
        {
            req.PutTask(hClient, task);
        }

        public void Delete(int taskId)
        {
            req.DeleteTask(hClient, taskId);
        }
        
    }
}
