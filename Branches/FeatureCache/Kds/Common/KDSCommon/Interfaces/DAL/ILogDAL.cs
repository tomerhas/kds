using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Logs;

namespace KDSCommon.Interfaces.DAL
{
    public interface  ILogDAL
    {
        void InsertLog(BakashaLog Log ); 
    }
}
