﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers.BankShaot
{
    public interface IBankShaotManager
    {
        void ExecBankShaot(long BakashaId);
        void ExecBankShaotLefiParametrim(long BakashaId, int Mitkan, DateTime Chodesh);
    }
}