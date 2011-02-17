using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KdsTaskManager
{
    public class Manager
    {
        private List<Action> _Actions;
        private List<Operator> _Operators;
        private int _CntOperatorRun;
        private DataSet _DsCommandOfGroup;
        private int _NbOfGroup;
        private int field;

        public Manager()
        {
        }

        /// <summary>
        /// Return the number of groups to initiate the list of operators
        /// </summary>
        private int GetNumberOfGroups()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get the list of the Command from Db for each group defined in the system
        /// </summary>
        private DataSet GetCommandOfGroup()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Fill DsCommandOfGroup into _Operator group by GroupId
        /// </summary>
        private void FillCommandInOperators()
        {
            throw new System.NotImplementedException();
        }




    }
}
