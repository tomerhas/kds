using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KdsTaskManager
{
    public class OracleSp
    {
        public const string InsertLogTask = "PKG_TASK_MANAGER.InsertLogTask";
        public const string UpdateLogTask = "PKG_TASK_MANAGER.UpdateLogTask";
        public const string GetTaskOfGroup = "PKG_TASK_MANAGER.GetTaskOfGroup";
        public const string GetGroupsDefinition = "PKG_TASK_MANAGER.GetKvuzot";
        public const string GetActionParameters = "PKG_TASK_MANAGER.GetActionParameters";
    }
    public static class Utilities
    {
        public static string PrepareExceptionMessage(string message)
        {
            string OriginFunction = string.Empty;
            string StrError = message;
            StackTrace st = new StackTrace();
            StackFrame[] AllStack = st.GetFrames();
            List<StackFrame> RelevantStack = AllStack.Reverse().ToList();
            RelevantStack.RemoveAt(0);
            RelevantStack.RemoveAt(1);
            RelevantStack.RemoveAt(RelevantStack.Count -1 );
            RelevantStack.ForEach(stack => OriginFunction += stack.GetMethod().DeclaringType.Name + ":" + stack.GetMethod().Name + "=>\n");
            StrError = OriginFunction + StrError;
            return StrError;
        }
    }
    public static class Functions
    {
        public static bool TestCommand()
        {
            Console.WriteLine("TestCommand is running ");
            return false;
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
        private TypeCommand _Type;


        public Message(Action action, TypeStatus status, string remark, DateTime startTime, DateTime endTime)
        {
            try
            {
                GroupId = action.IdGroup;
                IdOrder = action.IdOrder;
                _Type = action.TypeCommand;
                Status = status;
                Sequence = action.Sequence;
                Remark = (status == TypeStatus.Stopped) ? Utilities.PrepareExceptionMessage(remark) : remark;
                StartTime = startTime;
                EndTime = endTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateTaskLog()
        {
            Console.WriteLine("group {0}, Order {1} with Command {4} was update message {2} \n {3}", GroupId, IdOrder, Status, Remark, _Type);
            Bl _Bl = new Bl();
            _Bl.UpdateProcessLog(this);
        }

        public void InsertTaskLog()
        {
            Console.WriteLine("group {0}, Order {1} with Command {4} was insert message {2} \n {3}", GroupId, IdOrder, Status, Remark, _Type);
            Bl _Bl = new Bl();
            _Bl.InsertProcessLog(this);
        }


    }

    public enum TypeCommand
    {
        Program = 1,
        StoredProcedure = 2
    }
    public enum TypeStatus
    {
        Idle = 0,
        Running = 1,
        Success = 3,
        Stopped = 4
    }
    public enum OnFailureBehavior
    {
        Exit = 1,
        Continue = 2
    }


}
