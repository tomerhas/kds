using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Threading;
using KDSCommon.Enums;
namespace KdsCalcul
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper b = new Bootstrapper();
            b.Init();
            clCalculMain oCalculMain;
            int DelayBetweenConnections = Int32.Parse(ConfigurationSettings.AppSettings["DelayBetweenConnections"].ToString());
            switch ((enCalcType)int.Parse(args[0]))
            {
                case enCalcType.MonthlyCalc:
                    //Thread.Sleep(int.Parse(args[7]) * DelayBetweenConnections * 1000);
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), DateTime.Parse(args[2]), DateTime.Parse(args[3]), (string)args[4],
                                                       (int.Parse(args[5]) == 1), (int.Parse(args[6]) == 1), int.Parse(args[7]));

                 //   oCalculMain = new clCalculMain(1111, DateTime.Parse("01/01/2016"), DateTime.Parse("31/01/2016"), "",
                  //                                           true, false, 1);
                         oCalculMain.RunCalcBatchProcess();
                    break;
                case enCalcType.PremiotCalc:
                         oCalculMain = new clCalculMain((long)int.Parse(args[1]),int.Parse(args[2]));
                         oCalculMain.RunCalcBatchProcessPremiyot();
                    break;
                case enCalcType.ShinuyimVeShguyim:
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (BatchExecutionType)int.Parse(args[2]));
                   //  oCalculMain = new clCalculMain(155203,1, BatchExecutionType.All);//(long)int.Parse(args[1]), int.Parse(args[3]),(BatchExecutionType)int.Parse(args[2]));
                    oCalculMain.RunShinuyimVeShguim();
                    break;

                case enCalcType.ShinuyimVeSghuimHR:
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (BatchExecutionType)int.Parse(args[2]));
                    oCalculMain.RunShinuyimVeShguimHR();
                    break;
                case enCalcType.ShinuyimVeSghuimPremiot:
                  //  oCalculMain = new clCalculMain(7970,1, clGeneral.BatchExecutionType.All);
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (BatchExecutionType)int.Parse(args[2]));
                    oCalculMain.RunShinuyimVeShguimPremiot();
                    break;
                case enCalcType.Rikuzim:
                    clTask _Task = new clTask();
                    _Task.RunRikuzim((long)int.Parse(args[1]), (long)int.Parse(args[2]), int.Parse(args[3]));
                    break; 
            }

        }
    }
}
