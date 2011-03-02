using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.DAL;

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
    public class Action
    {
        public int IdGroup { get; set; }
        public TypeCommand TypeCommand { get; set; }
        public string LibraryName { get; set; }
        public string CommandName { get; set; }
        public int IdOrder { get; set; }
        public OnFailureBehavior OnFailure { get; set; }
        public int Sequence { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
    public class Parameter
    {
        public ParameterType Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
