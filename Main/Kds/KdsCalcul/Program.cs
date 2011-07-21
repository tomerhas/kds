using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsCalcul
{
    class Program
    {
        static void Main(string[] args)
        {

            clCalculMain oCalculMain = new clCalculMain((long)int.Parse(args[0]), DateTime.Parse(args[1]), DateTime.Parse(args[2]), (string)args[3],
                                                        (int.Parse(args[4]) == 1), (int.Parse(args[5]) == 1), int.Parse(args[6]));
  
            oCalculMain.RunCalcBatchProcess();
        }
    }
}
