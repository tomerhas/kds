using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Xml;
using System.IO;


namespace KdsTaskManager
{
    public class OracleSp
    {
        public const string InsertLogTask = "PKG_TASK_MANAGER.InsLogKvuzot";
        public const string UpdateLogTask = "PKG_TASK_MANAGER.UpdLogKvuzot";
        public const string GetTaskOfGroup = "PKG_TASK_MANAGER.GetPeiluyotBekvuza";
        public const string GetGroupsDefinition = "PKG_TASK_MANAGER.GetKvuzot";
        public const string GetActionParameters = "PKG_TASK_MANAGER.GetActionParameters";
    }
    public static class Utilities
    {
        public static string EventLogSource;
        public static bool Debug; 

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

        public static object DeserializeObject(System.Type type, string xmlSerialized)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            TextReader reader = new StringReader(xmlSerialized);
            return serializer.Deserialize(reader);
        }

        public static string SerializeObject(object objToSerialzie)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);
            XmlSerializer serializer = new XmlSerializer(objToSerialzie.GetType());
            serializer.Serialize(writer, objToSerialzie);
            return sb.ToString();
        }
    }
    public class Functions
    {
        public  void TestCommand()
        {
            Console.WriteLine("TestCommand is running ");
        }
        public  bool TestCommandWithParam(string param)
        {
            Console.WriteLine("TestCommand is running with param {0}", param);
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
            try
            {
                if (Utilities.Debug)
                    EventLog.WriteEntry(Utilities.EventLogSource, "group " + GroupId + ",Order " + IdOrder + " with Command " + _Type + " will update message " + Status + " \n " + Remark, EventLogEntryType.Information);
                Bl _Bl = new Bl();
                _Bl.UpdateProcessLog(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertTaskLog()
        {
            try
            {
                if (Utilities.Debug)
                    EventLog.WriteEntry(Utilities.EventLogSource, "group " + GroupId + ",Order " + IdOrder + " with Command " + _Type + " will insert message " + Status + " \n " + Remark, EventLogEntryType.Information);
                Bl _Bl = new Bl();
                _Bl.InsertProcessLog(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        Success = 2,
        Stopped = 3
    }
    public enum OperatorState
    {
        Sleeping ,
        Working ,
        Endded 
    }
    public enum OnFailureBehavior
    {
        Exit = 1,
        Continue = 2
    }

}
