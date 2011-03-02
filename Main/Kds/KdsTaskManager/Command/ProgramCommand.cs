using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace KdsTaskManager
{
    public class ProgramCommand : Command
    {
        private Type _Type;
        private MethodInfo _MethodInfo;

        public ProgramCommand(Action ActionToExecute)
        {
            _ActionToExecute = ActionToExecute;
        }

        /// <remarks>Fill the Type and MethodInfo</remarks>
        private void CreateMethodToExecute()
        {
            try
            {
                _Type = Type.GetType(_ActionToExecute.LibraryName);
                _MethodInfo = _Type.GetMethod(_ActionToExecute.CommandName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Execute()
        {
            try
            {
                object[] nullObj = null;
                CreateMethodToExecute();
                _MessageStart = new Message(_ActionToExecute, TypeStatus.Running, string.Empty, DateTime.Now, DateTime.Now);
                _MessageStart.UpdateTaskLog();
                _ActionResult = (bool)_MethodInfo.Invoke(_Type, nullObj);
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Success, string.Empty, DateTime.Now, DateTime.Now);
                _MessageEnd.UpdateTaskLog();
            }
            catch (Exception ex)
            {
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Stopped, ex.Message, DateTime.Now, DateTime.Now);
                _MessageEnd.UpdateTaskLog();
                _ActionResult = false;
                throw ex;
            }

        }
    }


}
