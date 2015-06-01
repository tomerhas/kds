using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsService
{
    public interface ICalcExecuter
    {
        void Add(CalcParam calcParam);
        void UpdateOnCompleted(int count);
        event EventHandler<CompletedExecutionEventArgs> Completed;
    }
}
