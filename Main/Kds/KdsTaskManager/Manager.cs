using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.DAL;

namespace KdsTaskManager
{

    public class Manager
    {
        private List<Group> _Groups;
        private List<Operator> _Operators;
        private int _CntRunningOperators = 0;
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
                ActionItem.Parameters = FillParameterOfAction(ActionItem);
                ActionOfGroup.Add(ActionItem);
                groupItem.AddActions(ActionOfGroup);
            }
        }


        private List<Parameter> FillParameterOfAction(Action CurrentAction)
        {
            int _NbOfParameter = 0;
            DataTable dt = new DataTable();
            List<Parameter> ParameterOfAction = new List<Parameter>();
            Bl oBl = Bl.GetInstance();
            dt = oBl.GetActionParameters(CurrentAction.IdGroup, CurrentAction.IdOrder);
            _NbOfParameter = dt.Rows.Count;
            if (_NbOfParameter > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Parameter ParameterItem = new Parameter();
                    ParameterItem.Name = item["ParamName"].ToString();
                    ParameterItem.Type = (ParameterType)clGeneral.GetIntegerValue(item["ParamType"].ToString());
                    ParameterItem.Value = item["ParamValue"].ToString();
                    ParameterOfAction.Add(ParameterItem);
                }
            }
            return ParameterOfAction;
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
            while (_CntRunningOperators > 0)
            {
                Console.WriteLine("Manager goes to sleep until {0} operators will finish them job...", _CntRunningOperators.ToString());
                Thread.Sleep(5000);
            }
            Console.WriteLine("Manager was finished his job", _CntRunningOperators.ToString());
            Console.ReadKey();

        }
        /// <summary>
        /// Fill DsCommandOfGroup into _Operator group by GroupId of _DsGroup
        /// </summary>
        private void CreateOperators()
        {
            Console.WriteLine("Create {0} Operator(s)", _NbOfGroup);
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
                Console.WriteLine("Operator {0} will start...{1} operator(s) are running ", Item.GroupId, _CntRunningOperators);
                Item.Start();
            }
            else Item.Sleep();
        }
    }
}
