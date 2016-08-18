using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KDSCommon.Interfaces.Errors
{
    public interface ICardError
    {
        bool IsCorrect(ErrorInputData input);
        ErrorTypes CardErrorType { get; }
        ErrorSubLevel CardErrorSubLevel { get; }
    }
}
