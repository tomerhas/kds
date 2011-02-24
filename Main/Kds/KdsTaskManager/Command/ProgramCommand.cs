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


        protected override bool Execute()
        {
            try
            {
                object[] nullObj = null;
                CreateMethodToExecute();
                _MessageStart = new Message(_ActionToExecute, TypeStatus.Running, string.Empty, DateTime.Now, DateTime.Now);
                UpdateTaskLog(_MessageStart);
                _ActionResult = (bool)_MethodInfo.Invoke(_Type, nullObj);
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Success, string.Empty, DateTime.Now, DateTime.Now);
                UpdateTaskLog(_MessageEnd);
                return _ActionResult;
            }
            catch (Exception ex)
            {
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Stopped, string.Empty, DateTime.Now, DateTime.Now);
                UpdateTaskLog(_MessageEnd);
                throw ex;
            }

        }
    }


}
