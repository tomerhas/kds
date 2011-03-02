using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager
{
    public abstract class Command
    {
        protected Action _ActionToExecute;
        protected bool _ActionResult = false;
        protected Message _MessageStart, _MessageEnd;
        protected abstract bool Execute();

        protected void UpdateTaskLog(Message msg)
        {
            Console.WriteLine("group {0}, Order {1} with Command {4} was send message {2} \n {3}", msg.GroupId, msg.IdOrder, msg.Status, msg.Remark, _ActionToExecute.TypeCommand);
        }

        public bool Run()
        {
            try
            {
                if (_ActionToExecute.IdOrder == 1)
                {
                    Message msg = new Message(_ActionToExecute, TypeStatus.Idle, string.Empty, DateTime.Now, DateTime.Now);
                    UpdateTaskLog(msg);
                }
                return Execute();
            }
            catch (Exception ex)
            {
                Message msg = new Message(_ActionToExecute, TypeStatus.Stopped, ex.Message, DateTime.Now, DateTime.Now);
                UpdateTaskLog(msg);
                return false;
            }
        }
    
    }

    public static class FactoryCommand
    {

        public static Command GetInstance(Action action)
        {
            switch (action.TypeCommand)
            {
                case TypeCommand.Program:
                    return new ProgramCommand(action);
                case TypeCommand.StoredProcedure:
                    return new StoredProcedureCommand(action);
                default:
                    return null;
            }
        }


    }
}
