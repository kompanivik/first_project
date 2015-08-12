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
    public class Project
    {
        public string name { get; set; }
        public ObservableCollection<Task> tasks { get; set; }
    }
}
