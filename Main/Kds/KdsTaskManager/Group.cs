using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager
{
    public class Group
    {
        private List<Action> _Actions;

        public Group()
        {
        }
        public void AddActions(List<Action> ActionsOfGroup)
        {
            _Actions = ActionsOfGroup;
        }
    
        public int IdGroup { get; set; }

        public DateTime StartTime { get; set; }

        public long Cycle { get; set; }

        public DateTime EndTime { get; set; }
        public List<Action> Actions
        {
            get
            {
                return _Actions;
            }
        }


    }
}
