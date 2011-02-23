using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager 
{
    public abstract class FactoryCommand 
    {
        protected Action _ActionToExecute;
        protected bool _ActionResult = false;

        private static FactoryCommand _FactoryCommand; 

        public static FactoryCommand instance(Action action)
        {
            if (_FactoryCommand == null)
            {
                switch (action.TypeCommand)
                {
                    case TypeCommand.Program:
                        _FactoryCommand = new ProgramCommand(action);
                        break;
                    case TypeCommand.StoredProcedure:
                        _FactoryCommand = new StoredProcedureCommand(action);
                        break;
                }
            }
            return _FactoryCommand; 
        }

        public bool Run()
        {
            _ActionResult =  Execute();
            return _ActionResult; 
        }
        protected abstract bool Execute();

        protected void UpdateTaskLog(Message msg)
        {
            Console.WriteLine("Message {0},{1},{2} was send to Db", msg.GroupId,msg.IdOrder,msg.Status);
        }

    }
}
