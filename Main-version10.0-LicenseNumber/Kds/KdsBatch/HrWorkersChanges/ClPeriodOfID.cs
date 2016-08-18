using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.HrWorkersChanges
{
    public class ClPeriodOfID
    {
        private DateTime _FromDate, _ToDate;
        private int _IdNumber, _Code;
        private int _CountOfDay;
        public ClPeriodOfID(int IdNumber, DateTime FromDate, DateTime ToDate, int kod)
        {
            _IdNumber = IdNumber;
            _FromDate = FromDate;
            _ToDate = ToDate;
            _CountOfDay = CalcDifferenceDate();
            _Code = kod;
        }

        private int CalcDifferenceDate()
        {
            TimeSpan diffDate = _ToDate.Subtract(_FromDate);
            return diffDate.Days+1;
        }

        #region Properties

        public int IdNumber
        {
            set
            {
                _IdNumber =value;
            }
            get
            {
               return _IdNumber;
            }
        }

        public int Code
        {
            set
            {
                _Code = value;
            }
            get
            {
                return _Code;
            }
        }

        public DateTime FromDate
        {
            set
            {
                _FromDate = value;
            }
            get
            {
                return _FromDate;
            }
        }


        public DateTime ToDate
        {
            set
            {
                _ToDate = value;
            }
            get
            {
                return _ToDate;
            }
        }

        public int CountOfDay
        {
            set
            {
                _CountOfDay = value;
            }
            get
            {
                return _CountOfDay;
            }
        }
       
        #endregion

    }
}
