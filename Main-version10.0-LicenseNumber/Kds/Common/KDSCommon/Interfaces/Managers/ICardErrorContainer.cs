﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Errors;

namespace KDSCommon.Interfaces.Managers
{
    public interface ICardErrorContainer
    {
        void Add(ICardError cardError);
        void Init();
        ICardError this[ErrorDualKey index] { get; }

        
        
    }
}