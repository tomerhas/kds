using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Errors
{
    public class DayError167 : BasicChecker
    {
        public DayError167(object CurrentInstance)
        {
            Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            //sample of condition 
            return (DayInstance.sHalbasha == "75290");
            //Implement the logic condition of error 11 
        }
    }


    public class DayError171 : BasicChecker
    {
        public DayError171(object CurrentInstance)
        {
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            return false;
            //Implement the logic condition of error 11 
        }
    }
}
