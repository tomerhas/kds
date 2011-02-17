using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace KdsTaskManager
{
    public class ProgramCommand : FactoryCommand
    {
        private Type _Type;
        private MethodInfo _MethodInfo;

        public ProgramCommand(Action action)
        {
            _ActionToExecute = action;
        }

        /// <remarks>Fill the Type and MethodInfo</remarks>
        private void CreateMethodToExecute()
        {
            throw new System.NotImplementedException();
        }


        protected override bool Execute()
        {
            CreateMethodToExecute();

            throw new NotImplementedException();
        }
    }

  
}
