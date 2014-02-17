
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Helpers
{
    public class DateHelper
    {
        public const int cYearNull = 1900;
        public static DateTime GetDateTimeFromStringHour(string sShaa, DateTime dDate)
        {
            DateTime dTemp;
            string[] arrTime;
            try
            {
                dDate = dDate.Date;
                arrTime = sShaa.Split(char.Parse(":"));
                if (arrTime.Length > 1)
                {
                    dTemp = dDate.AddHours(double.Parse(arrTime[0])).AddMinutes(double.Parse(arrTime[1]));
                    if (arrTime.Length > 2)
                    {
                        dTemp = dTemp.AddSeconds(double.Parse(arrTime[2]));
                    }
                }
                else
                {
                    sShaa = sShaa.PadLeft(4, (char)48);
                    dTemp = dDate.AddHours(double.Parse(sShaa.Substring(0, 2))).AddMinutes(double.Parse(sShaa.Substring(2, 2)));
                }

                return dTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
