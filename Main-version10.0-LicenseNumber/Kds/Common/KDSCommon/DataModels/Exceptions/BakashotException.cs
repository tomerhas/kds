using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Logs;

namespace KDSCommon.DataModels.Exceptions
{
    public class BakashotException : Exception
    {
        public BakashotException()
        {

        }

        public BakashotException(string message, BakashaLog log,Exception ex)
            : base(message, ex)
        {
            Log = new BakashaLog();
        }

        public BakashotException(Exception ex) : base ("",ex) 
        {
            Log = new BakashaLog(); 
        }
        public BakashaLog Log { get; set; }
    }
}
