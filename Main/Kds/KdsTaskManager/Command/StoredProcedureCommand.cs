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
        public StoredProcedureCommand(Action ActionToExecute)
        {
            _ActionToExecute = ActionToExecute;
            Command = _ActionToExecute.LibraryName + "." + _ActionToExecute.CommandName;
        }

        protected override bool Execute()
        {
            try
            {
                _MessageStart = new Message(_ActionToExecute, TypeStatus.Running, string.Empty, DateTime.Now, DateTime.Now);
                UpdateTaskLog(_MessageStart);
                clDal dal = new clDal();
                dal.ExecuteSP(Command);
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Success, string.Empty, DateTime.Now, DateTime.Now);
                UpdateTaskLog(_MessageEnd);
                return true;

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
