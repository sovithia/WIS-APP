using System;
namespace WIS.Models.SAMS
{
    public class SCHEDULE_DATA
    {
        public SCHEDULE_DATA(){}
        public ACADEMIC_SESSION academic_session { get; set; }
        public ACADEMIC_YEAR academic_year { get; set; }
        public CAMPUS campus { get; set; }
        public CAMPUS_ROOM campus_room { get; set; }
        public GRADE grade { get; set; }
        public STUDY study { get; set; }

    }
}
