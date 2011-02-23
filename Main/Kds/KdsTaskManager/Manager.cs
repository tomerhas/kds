using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;


namespace KdsTaskManager
{
    
    public class Manager
    {
        private List<Action> _Actions;
        private List<Operator> _Operators;
        private int _CntOperatorRun;
        private DataTable _DsCommandOfGroup;
        private DataTable _DsGroup;
        private int _NbOfGroup;
        public Manager()
        {
            _DsGroup = GetGroupsDefinition();
        }

        /// <summary>
        /// Return the definition of groups to initiate the list of operators
        /// </summary>
        private DataTable GetGroupsDefinition()
        {
            DataTable dt = new DataTable();
            Bl oBl = Bl.GetInstance();
            dt = oBl.GetGroupsDefinition();
            return dt; ;
        }

        /// <summary>
        /// Get the list of the Command from Db for GroupId
        /// </summary>
        private DataTable GetTaskOfGroup(int GroupId)
        {
            DataTable dt = new DataTable();
            Bl oBl = Bl.GetInstance();
            dt = oBl.GetTaskOfGroup(GroupId);

            return dt;
        }

        /// <summary>
        /// Fill DsCommandOfGroup into _Operator group by GroupId of _DsGroup
        /// </summary>
        private void FillCommandInOperators()
        {
            throw new System.NotImplementedException();
        }

        public void Run()
        {
            FillCommandInOperators();
        }




    }
}
