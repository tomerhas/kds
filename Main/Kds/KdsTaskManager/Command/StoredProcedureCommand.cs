using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager 
{
    public class StoredProcedureCommand : FactoryCommand
    {
        public StoredProcedureCommand(Action action)
        {
            _ActionToExecute = action;
        }

        protected override bool Execute()
        {
            Console.WriteLine("Sp Action {0} was executed", _ActionToExecute.CommandName);
            return true;
        }
    }
}
