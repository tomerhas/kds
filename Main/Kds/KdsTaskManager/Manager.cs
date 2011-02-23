using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using KdsLibrary;


namespace KdsTaskManager
{

    public class Manager
    {
        private List<Group> _Groups;
        private List<Operator> _Operators;
        private int _CntRunningOperators = 0 ;
        private int _NbOfGroup;
        public Manager()
        {
            GetGroupsDefinition();
        }
        public bool HasSomethingToDo
        {
            get
            {
                return (_NbOfGroup > 0);
            }
        }

        /// <summary>
        /// Get and Fill the definition of groups to initiate the list of operators
        /// </summary>
        private void GetGroupsDefinition()
        {
            DataTable dt = new DataTable();
            Bl oBl = Bl.GetInstance();
            dt = oBl.GetGroupsDefinition();
            _NbOfGroup = dt.Rows.Count;
            if (_NbOfGroup > 0)
            {
                _Groups = new List<Group>();
                foreach (DataRow item in dt.Rows)
                {
                    Group groupItem = new Group();
                    groupItem.IdGroup = clGeneral.GetIntegerValue(item["IDGROUP"].ToString());
                    groupItem.Cycle = clGeneral.GetIntegerValue(item["CYCLE"].ToString());
                    groupItem.StartTime = (DateTime)item["STARTTIME"];
                    groupItem.EndTime = (DateTime)item["ENDTIME"];
                    _Groups.Add(groupItem);
                }
            }
        }

        /// <summary>
        /// Set the list of the Command from Db for GroupId
        /// </summary>
        private void SetTaskOfGroup(int GroupId)
        {
            DataTable dt = new DataTable();
            Bl oBl = Bl.GetInstance();
            dt = oBl.GetTaskOfGroup(GroupId);
            int NbOfAction = dt.Rows.Count;
            if (NbOfAction > 0)
            {
                List<Action> ActionOfGroup = new List<Action>();
                foreach (DataRow item in dt.Rows)
                {
                    Group groupItem = _Groups.Find(group => group.IdGroup == GroupId);
                    Action ActionItem = new Action();
                    ActionItem.IdGroup = GroupId;
                    ActionItem.IdOrder = clGeneral.GetIntegerValue(item["IDORDER"].ToString());
                    ActionItem.OnFailure = (OnFailureBehavior)Enum.ToObject(typeof(OnFailureBehavior), clGeneral.GetIntegerValue(item["ONFAILURE"].ToString()));
                    ActionItem.Sequence = clGeneral.GetIntegerValue(item["SEQUENCE"].ToString());
                    ActionItem.TypeCommand = (TypeCommand)Enum.ToObject(typeof(TypeCommand), clGeneral.GetIntegerValue(item["TYPECOMMAND"].ToString()));
                    ActionItem.LibraryName = item["LIBRARYNAME"].ToString();
                    ActionItem.CommandName = item["COMMANDNAME"].ToString();
                    ActionOfGroup.Add(ActionItem);
                    groupItem.AddActions(ActionOfGroup);
                }
            }

        }


        private void operatorItem_OnEndWork(Operator sender)
        {
            _CntRunningOperators--;
            Console.WriteLine("Operator {0} was finished his job ", sender.GroupId);
        }

        private void operatorItem_OnWakeUp(Operator sender)
        {
            Console.WriteLine("Operator {0} wake up", sender.GroupId);
            if ((sender.IsTimeToRun()) && (_CntRunningOperators != 0))
                RunOperator(sender);
            else sender.Sleep();
        }

        public void Run()
        {
            CreateOperators();
            _Operators.ForEach(Item => RunOperator(Item));
        }
        /// <summary>
        /// Fill DsCommandOfGroup into _Operator group by GroupId of _DsGroup
        /// </summary>
        private void CreateOperators()
        {
            Console.WriteLine("Create Operators");
            _Operators = new List<Operator>();
            _Groups.ForEach(groupItem => _Operators.Add(new Operator(groupItem)));
            _Operators.ForEach(operatorItem => operatorItem.OnEndWork += new EndWorkHandler(operatorItem_OnEndWork));
            _Operators.ForEach(operatorItem => operatorItem.OnWakeUp += new WakeUpHandler(operatorItem_OnWakeUp));
            if (_NbOfGroup > 0)
                _Operators.ForEach((OperatorItem => SetTaskOfGroup(OperatorItem.GroupId)));

        }

        private void RunOperator(Operator Item)
        {
            if (Item.IsTimeToRun())
            {
                _CntRunningOperators++;
                Item.Start();
            }
            else Item.Sleep();
        }



    }
}
