using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KdsTaskManager
{
    public abstract class Command
    {
        protected Action _ActionToExecute;
        protected bool _ActionResult = false;
        protected Message _MessageStart, _MessageEnd;
        protected abstract void Execute();


        public bool Run()
        {
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(Utilities.EventLogSource, Utilities.PrepareExceptionMessage(ex.Message), EventLogEntryType.Error);
                Message msg = new Message(_ActionToExecute, TypeStatus.Stopped,  "Group:" + _ActionToExecute.IdGroup + 
                                                                                 ",Order:" +  _ActionToExecute.IdOrder + "\n" +
                        Utilities.PrepareExceptionMessage(ex.Message), DateTime.MinValue, DateTime.Now);
                msg.UpdateTaskLog();
            }

            return _ActionResult;

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
