using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager
{
   
    public class Action
    {
        private int field;
    
        public int IdGroup { get; set; }
        public TypeCommand TypeCommand { get; set; }
        public string LibraryName { get; set; }
        public string CommandName { get; set; }
        public DateTime EndTime { get; set; }
        public long Cycle { get; set; }
        public int IdOrder { get; set; }
        public DateTime StartTime { get; set; }
        public int OnFailure { get; set; }
        public int Sequence { get; set; }

        /// <summary>
        /// Now() ( in hour ) between startTime and Endtime
        /// or Cycle = true
        /// </summary>
        public void IsTimeToRun()
        {
            throw new System.NotImplementedException();
        }
    }
}
