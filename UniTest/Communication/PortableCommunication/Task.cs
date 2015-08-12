using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Globalization;

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

        public string status
        {
            get
            {
                if (!completed)
                {
                    return "To do";
                }
                else
                {
                    return "done";
                }
            }
        }

        public DateTime dateTime
        {
            get
            {
                DateTime d;
                DateTime.TryParseExact(
                duedate,
                @"yyyy-MM-ddTHH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal,
                out d);
                return d;
            }
        }

        public string displayDate
        {
            get
            {
                return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
    }
}
