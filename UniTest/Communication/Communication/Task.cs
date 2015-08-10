using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Communication
{
    [DataContract]
    public class Task
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string duedate { get; set; }
        [DataMember]
        public bool completed { get; set; }
        [DataMember]
        public string project { get; set; }
    }
}
