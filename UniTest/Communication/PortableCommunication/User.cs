using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Communication
{
    [DataContract]
    class User
    {
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string password { get; set; }
    }
}
