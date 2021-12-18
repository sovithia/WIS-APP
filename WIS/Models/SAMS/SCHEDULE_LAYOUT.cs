using System;
using System.Collections.Generic;

namespace WIS.Models.SAMS
{
    public class SCHEDULE_LAYOUT
    {
        public SCHEDULE_LAYOUT() { }

        public string layout_type {get;set;}        
        public List<SCHEDULE_LAYOUT_DATA> dataList { get; set; }
    }
}
