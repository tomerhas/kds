using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager
{
    public class Group
    {
        private List<Action> _Actions;

        public Group(List<Action> ActionsOfGroup)
        {
            _Actions = ActionsOfGroup; 
        }
    
        public int IdGroup { get; set; }

        public DateTime StartTime { get; set; }

        public long Cycle { get; set; }

        public DateTime EndTime { get; set; }

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
