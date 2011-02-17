using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager 
{
    public abstract class FactoryCommand 
    {
        protected Action _ActionToExecute;
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
            return Execute();
        }
        protected abstract bool Execute();

        protected void UpdateTaskLog(Message msg)
        {
            throw new System.NotImplementedException();
        }

    }
}
