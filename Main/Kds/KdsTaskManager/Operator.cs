﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using KdsLibrary;

namespace KdsTaskManager
{
    public delegate void WakeUpHandler(Operator sender);

    public delegate void EndWorkHandler(Operator sender);

    public class Operator
    {
        public event WakeUpHandler OnWakeUp;
        public event EndWorkHandler OnEndWork;
        private Thread _Thread;
        private Group _Group;
        private Command _Command;
        private int OperatorSleepTime = 0;

        public Operator(Group group)
        {
            try
            {
                _Group = group;
                _Thread = new Thread(new ThreadStart(StartWork));
                OperatorSleepTime = clGeneral.GetIntegerValue(ConfigurationSettings.AppSettings["OperatorSleepTime"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StartWork()
        {
            bool ResultCommand = false;
            try
            {
                foreach (Action ActionItem in _Group.Actions)
                {
                    _Command = FactoryCommand.GetInstance(ActionItem);
                    if (_Group.Actions[0] == ActionItem)
                    {
                        Message msg = new Message(ActionItem, TypeStatus.Idle, string.Empty, DateTime.Now, DateTime.Now);
                        msg.InsertTaskLog();
                    }
                    ResultCommand = _Command.Run();
                    if ((ResultCommand) ||
                       ((!ResultCommand) && (ActionItem.OnFailure == OnFailureBehavior.Exit)))
                    {
                        OnEndWork(this);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region properties

        public int GroupId
        {
            get
            {
                return _Group.IdGroup;
            }
        }

        #endregion

        public void Start()
        {
            _Thread.Start();
        }

        /// <remarks>_Thread has to sleep and raise the OnWakeUp event at the end of sleeping</remarks>
        public void Sleep()
        {
            Thread.Sleep(OperatorSleepTime * 1000);
            OnWakeUp(this);
        }



        /// <summary>
        /// Now() ( in hour ) between startTime and Endtime
        /// or Cycle = true
        /// </summary>
        public bool IsTimeToRun()
        {
            DateTime StartHour = PrepareDateFromDayAndHour(DateTime.Now, _Group.StartTime);
            DateTime EndHour = PrepareDateFromDayAndHour(DateTime.Now, _Group.EndTime);
            return ((_Group.Cycle == 1) || ((StartHour <= DateTime.Now) && (EndHour >= DateTime.Now)));
        }

        private DateTime PrepareDateFromDayAndHour(DateTime Date, DateTime Hour)
        {
            string OnlyHour = string.Format("{0:HH:mm:ss}", Hour);
            string OnlyDate = string.Format("{0:d/M/yyyy}", Date);
            return DateTime.Parse(OnlyDate + " " + OnlyHour);
        }

    }
}
