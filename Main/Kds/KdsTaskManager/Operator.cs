using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KdsLibrary;

namespace KdsTaskManager
{
    public delegate void EndWorkHandler(Operator sender);

    public class Operator
    {
        public event EndWorkHandler OnEndWork;
        private Thread _Thread;
        private Group _Group;
        private Command _Command;
        public OperatorState State { get; set; }

        public Operator(Group group)
        {
            try
            {
                _Group = group;
                _Thread = new Thread(new ThreadStart(StartWork));
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
                    ResultCommand = _Command.Run();
                    if ((!ResultCommand) && (ActionItem.OnFailure == OnFailureBehavior.Exit))
                    {
                        OnEndWork(this);
                        break;
                    }
                }
                OnEndWork(this);
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
            try
            {
                _Thread.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetGroupToIdle()
        {
            try
            {
                _Group.Actions.ForEach(Action => SetToIdle(Action));
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        private void SetToIdle(Action CurrentAction)
        {
            Message msg = new Message(CurrentAction, TypeStatus.Idle, string.Empty, DateTime.Now, DateTime.MinValue);
            msg.InsertTaskLog();
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
