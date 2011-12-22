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

                          //oCalculMain = new clCalculMain(0, DateTime.Parse("01/08/2010"), DateTime.Parse("01/09/2010"), "",
                          //                                         true, false,2);
  
                         oCalculMain.RunCalcBatchProcess();
                    break;
                case clGeneral.enCalcType.PremiotCalc:
                         oCalculMain = new clCalculMain((long)int.Parse(args[1]),int.Parse(args[2]));

                         oCalculMain.RunCalcBatchProcessPremiyot();
                    break;
                case clGeneral.enCalcType.ShinuyimVeShguyim:
                    oCalculMain = new clCalculMain(5775,1,BatchExecutionType.All);//(long)int.Parse(args[1]), int.Parse(args[3]),(BatchExecutionType)int.Parse(args[2]));

                    oCalculMain.RunShinuyimVeShguim();
                    break;

                case clGeneral.enCalcType.ShinuyimVeSghuimHR:
                    oCalculMain = new clCalculMain((long)int.Parse(args[1]), int.Parse(args[3]), (BatchExecutionType)int.Parse(args[2]));

                    oCalculMain.RunShinuyimVeShguimHR();
                    break;

            }

        }
    }
}
