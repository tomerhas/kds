using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Threading;
namespace KdsCalcul
{
    class Program
    {
        static void Main(string[] args)
        {
            clCalculMain oCalculMain;
            int DelayBetweenConnections = Int32.Parse(ConfigurationSettings.AppSettings["DelayBetweenConnections"].ToString());
            switch ((clGeneral.enCalcType)int.Parse(args[0]))
            {
                case clGeneral.enCalcType.MonthlyCalc:
                    Thread.Sleep(int.Parse(args[7]) * DelayBetweenConnections * 1000);
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), DateTime.Parse(args[2]), DateTime.Parse(args[3]), (string)args[4],
                                                       (int.Parse(args[5]) == 1), (int.Parse(args[6]) == 1), int.Parse(args[7]));

                  //  oCalculMain = new clCalculMain(6435, DateTime.Parse("01/08/2010"), DateTime.Parse("30/09/2010"), "",
                   //                                          true, false, 1);
                         oCalculMain.RunCalcBatchProcess();
                    break;
                case clGeneral.enCalcType.PremiotCalc:
                         oCalculMain = new clCalculMain((long)int.Parse(args[1]),int.Parse(args[2]));
                         oCalculMain.RunCalcBatchProcessPremiyot();
                    break;
                case clGeneral.enCalcType.ShinuyimVeShguyim:
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (clGeneral.BatchExecutionType)int.Parse(args[2]));
                    // oCalculMain = new clCalculMain(5789,1,clGeneral.BatchExecutionType.All);//(long)int.Parse(args[1]), int.Parse(args[3]),(BatchExecutionType)int.Parse(args[2]));
                    oCalculMain.RunShinuyimVeShguim();
                    break;

                case clGeneral.enCalcType.ShinuyimVeSghuimHR:
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (clGeneral.BatchExecutionType)int.Parse(args[2]));
                    oCalculMain.RunShinuyimVeShguimHR();
                    break;
                case clGeneral.enCalcType.ShinuyimVeSghuimPremiot:
                  //  oCalculMain = new clCalculMain(7970,1, clGeneral.BatchExecutionType.All);
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (clGeneral.BatchExecutionType)int.Parse(args[2]));
                    oCalculMain.RunShinuyimVeShguimPremiot();
                    break;
                case clGeneral.enCalcType.Rikuzim:
                    clTask _Task = new clTask();
                    _Task.RunRikuzim((long)int.Parse(args[1]), (long)int.Parse(args[2]), int.Parse(args[3]));
                    break; 
            }

        }
    }
}
