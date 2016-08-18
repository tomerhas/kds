using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using KdsLibrary.Utils;
using System.Configuration;
using System.Threading;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.ServiceLocation;
using System.Net.Mail;
using KDSCommon.DataModels.Mails;


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
        public enum LogType
        {
            Database,
            EventLog,
            Mail
        }
        public enum SeverityLevel
        {
            Fatal,
            Critical,
            Information
        }
        public static string EventLogSource;
        public static bool Debug;
        public static string[] RecipientsList;

        public static string PrepareExceptionMessage(Exception exception)
        {
            string StrError = string.Empty ;
            StackTrace st = new StackTrace();
            StackFrame[] AllStack = st.GetFrames();
            List<StackFrame> RelevantStack = AllStack.Reverse().ToList();
            RelevantStack.RemoveAt(0);
            RelevantStack.RemoveAt(1);
            RelevantStack.RemoveAt(RelevantStack.Count - 1);
            RelevantStack.ForEach(stack => StrError += stack.GetMethod().DeclaringType.Name + ":" + stack.GetMethod().Name + "=>\r\n");
            if (exception.InnerException != null)
                StrError += "\n" + exception.InnerException.Message;
            if (exception.TargetSite != null)
                StrError += "\n" + exception.TargetSite;
            return StrError + "\nError message:" + exception.Message ;
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
        public static void WriteLog(string Message, SeverityLevel severityLevel)
        {
            var mail = ServiceLocator.Current.GetInstance<IMailManager>();
            var mailManager = ServiceLocator.Current.GetInstance<IMailManager>();
           // clMail omail;
            switch (severityLevel)
            {
                case SeverityLevel.Fatal:
                    EventLog.WriteEntry(Utilities.EventLogSource, Message, EventLogEntryType.Error);
                    mailManager.SendMessage(new MailMessageWrapper(string.Join(";", RecipientsList)) { Subject = "Fatal event", Body = Message });

                    
                    break;
                case SeverityLevel.Critical:
                    EventLog.WriteEntry(Utilities.EventLogSource, Message, EventLogEntryType.Error);
                    mailManager.SendMessage(new MailMessageWrapper(string.Join(";", RecipientsList)) { Subject = "Critical event", Body = Message });
                   
                    break;
                case SeverityLevel.Information:
                    if (Utilities.Debug)
                        EventLog.WriteEntry(Utilities.EventLogSource, Message, EventLogEntryType.Information);
                    break;
                default:
                    break;
            }

        }
    }
    public class Functions
    {
        public void TestCommand()
        {
            Console.WriteLine("TestCommand is running ");
            Thread.Sleep(100000); 
        }
        public bool TestCommandWithParam(string param)
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
                Remark = remark;
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
                Utilities.WriteLog("group " + GroupId + ",Order " + IdOrder + " with Command " + _Type + " will update message " + Status + " \n " + Remark, Utilities.SeverityLevel.Information);
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
                Utilities.WriteLog("group " + GroupId + ",Order " + IdOrder + " with Command " + _Type + " will insert message " + Status + " \n " + Remark, Utilities.SeverityLevel.Information);
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
        Sleeping,
        Working,
        Endded
    }
    public enum OnFailureBehavior
    {
        Exit = 1,
        Continue = 2
    }

}
