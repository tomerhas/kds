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
            try
            {
                _Type = Type.GetType(_ActionToExecute.LibraryName);
                _MethodInfo = _Type.GetMethod(_ActionToExecute.CommandName);
            }
            catch
            {
                throw; 
            }
        }


        protected override bool Execute()
        {
            try
            {
                CreateMethodToExecute();
                _ActionResult = (bool)_MethodInfo.Invoke(this, null);
                Console.WriteLine("program Action {0} was executed", _ActionToExecute.CommandName);
                return _ActionResult;
            }
            catch
            {
                return false;
            }

        }
    }

  
}
