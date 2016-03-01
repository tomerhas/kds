using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    public class NetuneyYomAvoda
    {
        public SinglePropertyData<int> ZmanNesiot { get; set; }
        public SinglePropertyData<int> Tachograf { get; set; }
        public SinglePropertyData<int> Halbasha { get; set; }
        public SinglePropertyData<int> Lina { get; set; }
        public SinglePropertyData<int> HashlamaForDay { get; set; }
        public SinglePropertyData<int> SibatHashlamaLeyom { get; set; }
      //  public SinglePropertyData<int> HamaratShabat { get; set; }


        public NetuneyYomAvoda()
        {
            ZmanNesiot = new SinglePropertyData<int>();
            Tachograf = new SinglePropertyData<int>();
            Halbasha = new SinglePropertyData<int>();
            Lina = new SinglePropertyData<int>();
            HashlamaForDay = new SinglePropertyData<int>();
            SibatHashlamaLeyom = new SinglePropertyData<int>();
        //    HamaratShabat = new SinglePropertyData<int>();
          
        }
      
    }
}
