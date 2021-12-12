using System;
using Newtonsoft.Json;
using SQLite;

namespace WIS.Models
{
    public class ABSENCE
    {
        public string ID { get; set; }        
        public string start { get; set; }
        public string end { get; set; }
        public string student { get; set; }

        // full
        public string teacher { get; set; }
        public string coursename { get; set; }

        [JsonIgnore,Ignore]
        public DateTime Start
        {
            get
            {
                // Unix timestamp is seconds past epoch
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(int.Parse(start)).ToLocalTime();
                return dateTime;
            }
        }

        [JsonIgnore, Ignore]
        public DateTime End
        {
            get
            {
                // Unix timestamp is seconds past epoch
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(int.Parse(end)).ToLocalTime();
                return dateTime;
            }
        }
    }
}
