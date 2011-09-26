using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Errors
{
    public enum TypeCheck
    {
        CheckPrepareMachineForSidurValidity = 86,
        HasHafifa = 167,
        HasBothSidurEilatAndSidurVisa = 171
    }
    public enum OriginError
    {
        Day,
        Sidur,
        Peilut
    }
}
