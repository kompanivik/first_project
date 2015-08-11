using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Communication
{
    [DataContract]
    class Status
    {
        [DataMember]
        public bool status { get; set; }
        [DataMember]
        public string message { get; set; }

    }
}
