using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Errors
{
    public class FactoryChecker
    {

        public BasicChecker GetInstance(TypeCheck Error, object CurrentInstance)
        {
            BasicChecker creator = null;
            if (creator == null)
            {
                switch (Error)
                {
                    case TypeCheck.HasHafifa:
                        creator = new DayError167(CurrentInstance);
                        break;
                    case TypeCheck.HasBothSidurEilatAndSidurVisa:
                        creator = new DayError171(CurrentInstance);
                        break;
                    case TypeCheck.CheckPrepareMachineForSidurValidity:
                        break;
                }
            }
            if (creator == null)
                throw new Exception("Error " + Error + " doesn't not defined");
            return creator;
        }
    }
}
