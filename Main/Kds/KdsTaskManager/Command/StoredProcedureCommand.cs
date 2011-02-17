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
            // execute the stored procedure
            throw new NotImplementedException();
        }
    }
}
