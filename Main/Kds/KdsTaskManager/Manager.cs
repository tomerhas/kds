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
        private List<Action> _Actions;
        private List<Group> _Group;
        private List<Operator> _Operators;
        private int _CntOperatorRun;
        private DataTable _DsCommandOfGroup;
        private DataTable _DsGroup;
        private int _NbOfGroup;
        public Manager()
        {
            GetGroupsDefinition();
            if (_NbOfGroup > 0)
                _Group.ForEach(GroupItem => SetTaskOfGroup(GroupItem.IdGroup));
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
                _Group = new List<Group>();
                foreach (DataRow item in dt.Rows)
                {
                    Group groupItem = new Group();
                    groupItem.IdGroup = clGeneral.GetIntegerValue(item["IDGROUP"].ToString());
                    groupItem.Cycle = clGeneral.GetIntegerValue(item["CYCLE"].ToString());
                    groupItem.StartTime = (DateTime)item["STARTTIME"];
                    groupItem.EndTime = (DateTime)item["ENDTIME"];
                    _Group.Add(groupItem);
                }
            }
        }

        /// <summary>
        /// Get the list of the Command from Db for GroupId
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
                    Group groupItem = _Group.Find(group => group.IdGroup == GroupId);
                    Action ActionItem = new Action();
                    ActionItem.IdGroup     = GroupId; 
                    ActionItem.IdOrder     = clGeneral.GetIntegerValue(item["IDORDER"].ToString());
                    ActionItem.OnFailure   = clGeneral.GetIntegerValue(item["ONFAILURE"].ToString());
                    ActionItem.Sequence    = clGeneral.GetIntegerValue(item["SEQUENCE"].ToString());
                    ActionItem.TypeCommand = (TypeCommand)Enum.ToObject(typeof(TypeCommand), clGeneral.GetIntegerValue( item["TYPECOMMAND"].ToString()));
                    ActionItem.LibraryName = item["LIBRARYNAME"].ToString() ;
                    ActionItem.CommandName = item["COMMANDNAME"].ToString() ;
                    ActionOfGroup.Add(ActionItem);
                    groupItem.AddActions(ActionOfGroup);
                }
            }

        }

        private void FillDataItems()
        {
            string ParamLog = null;
            try
            {
                _ReportItems = new List<ReportItem>();
                _DtParamsReports = _BlReport.GetParamsReports;
                foreach (DataRow drReport in _DtReportDefinitions.Rows)
                {
                    ParamLog = string.Empty;
                    ReportItem Item = default(ReportItem);
                    Item = new ReportItem(drReport["REPORT_NAME"].ToString(), EStr.Utilities.GetIntegerValue(drReport["QUERY_RUN_NUMBER"].ToString()), EStr.Utilities.GetIntegerValue(drReport["QUERY_NUMBER"].ToString()), OutputFormat.EXCEL, drReport["USER_NUMBER"].ToString());
                    foreach (DataRow drReportItem in _DtParamsReports.Select("QUERY_NUMBER=" + EStr.Utilities.GetIntegerValue(drReport["QUERY_NUMBER"].ToString()) + "and QUERY_RUN_NUMBER=" + EStr.Utilities.GetIntegerValue(drReport["QUERY_RUN_NUMBER"].ToString())))
                    {
                        ReportParam ItemParams = default(ReportParam);
                        ItemParams = new ReportParam(drReportItem["PARAM_NAME"].ToString(), drReportItem["PARAM_VALUE"].ToString());
                        ParamLog += "Name=" + ItemParams.Name + ",Value=" + ItemParams.Value + Constants.vbCr;
                        Item.ReportParams.Add(ItemParams);
                    }
                    EAl.Utilities.LogMessage(Item.ReportName + "(" + Item.QueryRunNumber + ") was created with parameters:" + Constants.vbCr + ParamLog, EventLogEntryType.Information);
                    _ReportItems.Add(Item);
                }
            }
            catch (Exception ex)
            {
                EggedCommon.Alerts.Utilities.LogMessage(ex.Message, EventLogEntryType.Error, true);
            }
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
