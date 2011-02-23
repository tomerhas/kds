using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager
{
    public class OracleSp
    {
        public const string UpdateLogTask = "PKG_TASK_MANAGER.UpdateLogTask";
        public const string GetTaskOfGroup = "PKG_TASK_MANAGER.GetTaskOfGroup";
        public const string GetGroupsDefinition = "PKG_TASK_MANAGER.GetGroupsDefinition";
    }
    public class Functions
    {
        public void TestCommand()
        {
            Console.Write("TestCommand is running ");
        }

    }
    public class Message 
    {
        public int GroupId { get; set; }
        public int IdOrder { get; set; }
        public TypeStatus Status { get; set; }
        public int Sequence { get; set; }
        public string Remark { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public enum TypeCommand
    {
        Program = 1,
        StoredProcedure = 2
    }
    public enum TypeStatus
    {
        /// <remarks>set on start of action</remarks>
        Running = 1,
        /// <remarks>set on failure</remarks>
        Stopped = 3,
        /// <remarks>set on success</remarks>
        Success = 4
    }

}
