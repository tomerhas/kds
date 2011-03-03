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
        private object[] _parameters;

        public ProgramCommand(Action ActionToExecute)
        {
            _ActionToExecute = ActionToExecute;
            _parameters = null;

        }

        /// <remarks>Fill the Type and MethodInfo</remarks>
        private void CreateMethodToExecute()
        {
            try
            {
                _Type = Type.GetType(_ActionToExecute.LibraryName);
                _MethodInfo = _Type.GetMethod(_ActionToExecute.CommandName);
                if (_ActionToExecute.Parameters.Count > 0)
                    _parameters = GetParametersOfFunction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal Type GetParameterType(int dataType)
        {
            string type = string.Empty;
            switch (dataType)
            {
                case 1:
                    type = "System.String";
                    break;
                case 2:
                    type = "System.Int16";
                    break;
                case 3:
                    type = "System.Int32";
                    break;
                case 4:
                    type = "System.Int64";
                    break;
                case 5:
                    type = "System.DateTime";
                    break;
                case 6:
                    type = "System.Decimal";
                    break;
            }
            return Type.GetType(type);
        }

        private object[] GetParametersOfFunction()
        {
            object[] Obj = new object[_ActionToExecute.Parameters.Count];
            int Counter = 0;
            _ActionToExecute.Parameters.ForEach((ParameterItem) =>
                                    {
                                        Obj[Counter] = Convert.ChangeType(ParameterItem.Value,  GetParameterType(ParameterItem.Type));
                                        Counter++;
                                    });
            return Obj;
        }

        protected override void Execute()
        {
            try
            {
                CreateMethodToExecute();
                _MessageStart = new Message(_ActionToExecute, TypeStatus.Running, string.Empty, DateTime.Now, DateTime.Now);
                _MessageStart.UpdateTaskLog();
                _ActionResult = (bool)_MethodInfo.Invoke(_Type, _parameters);
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
