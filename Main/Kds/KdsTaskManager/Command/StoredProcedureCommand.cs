using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.DAL;

namespace KdsTaskManager
{
    public class StoredProcedureCommand : Command
    {
        private string Command = string.Empty;
        private clDal _dal;
        public StoredProcedureCommand(Action ActionToExecute)
        {
            try
            {
                _ActionToExecute = ActionToExecute;
                _dal = new clDal();
                if (_ActionToExecute.Parameters.Count > 0)
                {
                    _ActionToExecute.Parameters.ForEach(ParamItem =>
                    _dal.AddParameter(ParamItem.Name, (ParameterType)ParamItem.Type, ParamItem.Value, ParameterDir.pdInput));
                }
                Command = _ActionToExecute.LibraryName + "." + _ActionToExecute.CommandName;
            }
            catch (Exception ex)
            {
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Stopped,ex.Message, DateTime.Now, DateTime.Now);
                _MessageEnd.UpdateTaskLog();
                throw ex;
            }

        }

        protected override void Execute()
        {
            try
            {
                _MessageStart = new Message(_ActionToExecute, TypeStatus.Running, string.Empty, DateTime.Now,DateTime.MinValue);
                _MessageStart.UpdateTaskLog();
                _dal.ExecuteSP(Command);
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Success, string.Empty, DateTime.MinValue, DateTime.Now);
                _MessageEnd.UpdateTaskLog();
                _ActionResult = true;
            }
            catch (Exception ex)
            {
                _ActionResult = false ;
                throw ex;
            }
        }
    }
}
