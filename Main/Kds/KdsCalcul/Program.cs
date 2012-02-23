using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
namespace KdsCalcul
{
    class Program
    {
        static void Main(string[] args)
        {
            clCalculMain oCalculMain;
            switch ((clGeneral.enCalcType)int.Parse(args[0]))
            {
                case clGeneral.enCalcType.MonthlyCalc:
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
                    clLogBakashot.InsertErrorToLog(111, "I", 0, "ShinuyimVeShguyim: " + (clGeneral.BatchExecutionType)int.Parse(args[2]));
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (clGeneral.BatchExecutionType)int.Parse(args[2]));
                    // oCalculMain = new clCalculMain(5789,1,clGeneral.BatchExecutionType.All);//(long)int.Parse(args[1]), int.Parse(args[3]),(BatchExecutionType)int.Parse(args[2]));

                    oCalculMain.RunShinuyimVeShguim();
                    break;

                case clGeneral.enCalcType.ShinuyimVeSghuimHR:
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (clGeneral.BatchExecutionType)int.Parse(args[2]));

                    oCalculMain.RunShinuyimVeShguimHR();
                    break;
                case clGeneral.enCalcType.ShinuyimVeSghuimPremiot:
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (clGeneral.BatchExecutionType)int.Parse(args[2]));

                    oCalculMain.RunShinuyimVeShguimHR();
                    break;
            }

        }
    }
}
