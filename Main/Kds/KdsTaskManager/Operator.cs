using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KdsTaskManager
{
    public delegate void WakeUpHandler();

    public delegate void EndWorkHandler();

    public class Operator
    {
        public event  WakeUpHandler OnWakeUp;
        public event EndWorkHandler OnEndWork;
        private Thread _Thread;
        private List<Action> _Actions;
        private FactoryCommand _Command;

        public void Start()
        {
            _Actions.ForEach(Action => _Command = FactoryCommand.instance(Action));
//            _Command.Run();
//            OnEndWork();
        }

        /// <remarks>_Thread has to sleep and raise the OnWakeUp event at the end of sleeping</remarks>
        public void Sleep()
        {
            throw new System.NotImplementedException();
            OnWakeUp();
        }
    

        public Operator(List<Action> ActionToExecute)
        {
            _Actions = ActionToExecute;
        }
    }
}
