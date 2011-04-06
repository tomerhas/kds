using System;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
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
        private System.Timers.Timer TmrOperatorSleep;
        private int OperatorSleepTime = 0;
        public Manager()
        {
            try
            {
                OperatorSleepTime = clGeneral.GetIntegerValue(ConfigurationSettings.AppSettings["OperatorSleepTime"].ToString());
                TmrOperatorSleep = new System.Timers.Timer();
                TmrOperatorSleep.Elapsed += new ElapsedEventHandler(TmrOperatorSleep_Elapsed);
                TmrOperatorSleep.Start();
                TmrOperatorSleep.Interval = OperatorSleepTime * 60000;
                GetGroupsDefinition();
            }
            catch (Exception ex)
            {
                Utilities.WriteLog(Utilities.PrepareExceptionMessage(ex), Utilities.SeverityLevel.Fatal);
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
            return (DateTime.TryParse(strdate, out TheDate)) ? TheDate : DateTime.Now;
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
                Utilities.WriteLog(Utilities.PrepareExceptionMessage(ex), Utilities.SeverityLevel.Fatal);
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
                Utilities.WriteLog(Utilities.PrepareExceptionMessage(ex), Utilities.SeverityLevel.Fatal);
            }

            return ParameterOfAction;
        }

        private void operatorItem_OnEndWork(Operator sender)
        {
            _CntRunningOperators--;
            Utilities.WriteLog("Operator " + sender.GroupId + " was finished his job ", Utilities.SeverityLevel.Information);
        }

        private void TmrOperatorSleep_Elapsed(object sender, ElapsedEventArgs e)
        {
            Utilities.WriteLog("There are still " + _CntRunningOperators + " are running..", Utilities.SeverityLevel.Information);
            _Operators.FindAll(item =>
                                ((item.State == OperatorState.Sleeping) &&
                                (item.IsTimeToRun()) &&
                                (_CntRunningOperators != 0))
                ).ForEach(OperatorFound => RunOperator(OperatorFound));
        }
        private void RunOperator(Operator Item)
        {
            try
            {
                if (Item.IsTimeToRun())
                {
                    _CntRunningOperators++;
                    Utilities.WriteLog("Operator " + Item.GroupId + " will start..." + _CntRunningOperators + " operator(s) are running ", Utilities.SeverityLevel.Information);
                    Item.Start();
                    Item.State = OperatorState.Working;
                }
                else
                {
                    Item.State = OperatorState.Sleeping;
                }
            }
            catch (Exception ex)
            {
                Utilities.WriteLog(Utilities.PrepareExceptionMessage(ex), Utilities.SeverityLevel.Fatal);
            }

        }


        public void Run()
        {
            try
            {
                CreateOperators();
                _Operators.ForEach(Item => RunOperator(Item));
                while (_CntRunningOperators > 0)
                    Thread.Sleep(60000);
            }
            catch (Exception ex)
            {
                Utilities.WriteLog(Utilities.PrepareExceptionMessage(ex), Utilities.SeverityLevel.Fatal);
            }
            Utilities.WriteLog("Manager was finished his job", Utilities.SeverityLevel.Information);
        }
        /// <summary>
        /// Fill DsCommandOfGroup into _Operator group by GroupId of _DsGroup
        /// </summary>
        private void CreateOperators()
        {
            try
            {
                Utilities.WriteLog("Create " + _NbOfGroup + " Operator(s)", Utilities.SeverityLevel.Information);

                _Operators = new List<Operator>();
                _Groups.ForEach(groupItem => _Operators.Add(new Operator(groupItem)));
                _Operators.ForEach(operatorItem => operatorItem.OnEndWork += new EndWorkHandler(operatorItem_OnEndWork));
                if (_NbOfGroup > 0)
                    _Operators.ForEach((OperatorItem => SetTaskOfGroup(OperatorItem.GroupId)));
                _Operators.ForEach(OperatorItem => OperatorItem.SetGroupToIdle());
            }
            catch (Exception ex)
            {
                Utilities.WriteLog(Utilities.PrepareExceptionMessage(ex), Utilities.SeverityLevel.Fatal);
            }


        }




    }
}
