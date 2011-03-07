using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                GetGroupsDefinition();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(Utilities.EventLogSource, Utilities.PrepareExceptionMessage(ex.Message), EventLogEntryType.Error);
            }
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
            try
            {
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
                        groupItem.StartTime = IsDateTime(item["STARTTIME"].ToString());
                        groupItem.EndTime = IsDateTime(item["ENDTIME"].ToString());
                        _Groups.Add(groupItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        private DateTime IsDateTime(string strdate)
        {
            DateTime TheDate;
            return (DateTime.TryParse(strdate, out TheDate))? TheDate: DateTime.Now;
        }

        /// <summary>
        /// Set the list of the Command from Db for GroupId
        /// </summary>
        private void SetTaskOfGroup(int GroupId)
        {
            DataTable dt = new DataTable();
            Bl oBl = Bl.GetInstance();
            try
            {
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
            catch (Exception ex)
            {
                EventLog.WriteEntry(Utilities.EventLogSource, Utilities.PrepareExceptionMessage(ex.Message), EventLogEntryType.Error);
            }

        }


        private List<Parameter> FillParameterOfAction(Action CurrentAction)
        {
            int _NbOfParameter = 0;
            DataTable dt = new DataTable();
            List<Parameter> ParameterOfAction = new List<Parameter>();
            Bl oBl = Bl.GetInstance();
            try
            {
                dt = oBl.GetActionParameters(CurrentAction.IdGroup, CurrentAction.IdOrder);
                _NbOfParameter = dt.Rows.Count;
                if (_NbOfParameter > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Parameter ParameterItem = new Parameter();
                        ParameterItem.Name = item["ParamName"].ToString();
                        ParameterItem.Type = clGeneral.GetIntegerValue(item["ParamType"].ToString());
                        ParameterItem.Value = item["ParamValue"].ToString();
                        ParameterOfAction.Add(ParameterItem);
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(Utilities.EventLogSource, Utilities.PrepareExceptionMessage(ex.Message), EventLogEntryType.Error);
            }

            return ParameterOfAction;
        }

        private void operatorItem_OnEndWork(Operator sender)
        {
            _CntRunningOperators--;
            if (Utilities.Debug)
            EventLog.WriteEntry(Utilities.EventLogSource, "Operator " + sender.GroupId+  " was finished his job ", EventLogEntryType.Information);
        }

        private void operatorItem_OnWakeUp(Operator sender)
        {
            if (Utilities.Debug)
                EventLog.WriteEntry(Utilities.EventLogSource, "Operator " + sender.GroupId + "  wake up", EventLogEntryType.Information);
            if ((sender.IsTimeToRun()) && (_CntRunningOperators != 0))
                RunOperator(sender);
            else
            {
                if (Utilities.Debug)
                    EventLog.WriteEntry(Utilities.EventLogSource, "Operator " + sender.GroupId + "  goes to sleep", EventLogEntryType.Information);
                sender.Sleep();
            }

        }

        public void Run()
        {
            try
            {
                CreateOperators();
                _Operators.ForEach(Item => RunOperator(Item));
                while (_CntRunningOperators > 0)
                {
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(Utilities.EventLogSource, Utilities.PrepareExceptionMessage(ex.Message), EventLogEntryType.Error);
            }
            if (Utilities.Debug)
                EventLog.WriteEntry(Utilities.EventLogSource, "Manager was finished his job" , EventLogEntryType.Information);
        }
        /// <summary>
        /// Fill DsCommandOfGroup into _Operator group by GroupId of _DsGroup
        /// </summary>
        private void CreateOperators()
        {
            try
            {
                if (Utilities.Debug)
                    EventLog.WriteEntry(Utilities.EventLogSource, "Create " + _NbOfGroup+" Operator(s)", EventLogEntryType.Information);
                _Operators = new List<Operator>();
                _Groups.ForEach(groupItem => _Operators.Add(new Operator(groupItem)));
                _Operators.ForEach(operatorItem => operatorItem.OnEndWork += new EndWorkHandler(operatorItem_OnEndWork));
                _Operators.ForEach(operatorItem => operatorItem.OnWakeUp += new WakeUpHandler(operatorItem_OnWakeUp));
                if (_NbOfGroup > 0)
                    _Operators.ForEach((OperatorItem => SetTaskOfGroup(OperatorItem.GroupId)));
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(Utilities.EventLogSource, Utilities.PrepareExceptionMessage(ex.Message), EventLogEntryType.Error);
            }


        }

        private void RunOperator(Operator Item)
        {
            try
            {
                if (Item.IsTimeToRun())
                {
                    _CntRunningOperators++;
                    if (Utilities.Debug)
                        EventLog.WriteEntry(Utilities.EventLogSource, "Operator "+Item.GroupId +" will start..."+_CntRunningOperators+" operator(s) are running ", EventLogEntryType.Information);
                    Item.Start();
                }
                else Item.Sleep();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(Utilities.EventLogSource, Utilities.PrepareExceptionMessage(ex.Message), EventLogEntryType.Error);
            }

        }
    }
}
