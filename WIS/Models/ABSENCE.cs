using System;
using Newtonsoft.Json;
using SQLite;

namespace WIS.Models
{
    public class ABSENCE
    {
        public string id { get; set; }
        public string note { get; set; }


        public int starttimestamp { get; set; }
        public int endtimestamp { get; set; }

        public string studentdisplayname { get; set; }
        // full
        //public string teacher { get; set; }// Unavailable
        //public string coursename { get; set; }// Unavailable


        [JsonIgnore,Ignore]
        public DateTime Start
        {
            get
            {
                // Unix timestamp is seconds past epoch
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(starttimestamp).ToLocalTime();
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
                dateTime = dateTime.AddSeconds(endtimestamp).ToLocalTime();
                return dateTime;
            }
        }
    }
}
