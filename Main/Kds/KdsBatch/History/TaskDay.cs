using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.UDT;
using KdsLibrary;
using System.Configuration;

namespace KdsBatch.History
{
    public class TaskDay : BaseTask 
    {
        private COLL_YAMEY_AVODA_OVDIM _Collection;
        ////public TaskDay(long lRequestNum, char del)
        ////    : base(lRequestNum,del)
        ////{
        ////    ProcedureName = clGeneral.cProInsYameyAvodaHistory;
        ////    TypeName = "COLL_YAMEY_AVODA_OVDIM";
        ////    ParameterName = "p_coll_yamey_avoda_ovdim";
        ////    CollType = TypeTask.Day;
        ////    //_Collection = new COLL_YAMEY_AVODA_OVDIM();
        ////    Pattern = "BZAY";
        ////    PathDirectory = ConfigurationManager.AppSettings["PathFileMF"];
        ////    PathDirectoryOld = ConfigurationManager.AppSettings["PathFileMFOld"];
        ////}

        public TaskDay(long lRequestNum,string file, char del)
            : base(lRequestNum,file, del)
        {
            ProcedureName = clGeneral.cProInsYameyAvodaHistory;
            TypeName = "COLL_YAMEY_AVODA_OVDIM";
            ParameterName = "p_coll_yamey_avoda_ovdim";
            CollType = TypeTask.Day;
            //_Collection = new COLL_YAMEY_AVODA_OVDIM();
            Pattern = "BZAY";
            //PathDirectory = ConfigurationManager.AppSettings["PathFileMF"];
            //PathDirectoryOld = ConfigurationManager.AppSettings["PathFileMFOld"];
        }
        protected override void AllocateCollection()
        {
            _Collection = new COLL_YAMEY_AVODA_OVDIM(RecordsCount-1);
        }

        protected override void FillItemsToCollection(string[] Item, int index)
        {
            OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd;
            int mispar_ishi = 0;
            try
            {

                oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();

                mispar_ishi = int.Parse(Item[0]);
                if (mispar_ishi != 999999999)
                {
                    oObjYameyAvodaUpd.MISPAR_ISHI = int.Parse(mispar_ishi.ToString().Substring(0, mispar_ishi.ToString().Length - 1));
                    oObjYameyAvodaUpd.TAARICH = GetDateTime(Item[1].Trim());
                    //  oObjYameyAvodaUpd. = DateTime.Parse(Item[1]);
                    // oObjYameyAvodaUpd.TAARICH = DateTime.Parse(Item[1]);
                    if (Item[4].Trim() != "")
                        oObjYameyAvodaUpd.TACHOGRAF = Item[4];
                    if (Item[5].Trim() != "")
                        oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = decimal.Parse(Item[5]);
                    if (Item[6].Trim() != "")
                        oObjYameyAvodaUpd.HALBASHA = decimal.Parse(Item[6]);
                    if (Item[7].Trim() != "")
                        oObjYameyAvodaUpd.LINA = decimal.Parse(Item[7]);
                    if (Item[8].Trim() != "")
                        oObjYameyAvodaUpd.HASHLAMA_LEYOM = decimal.Parse(Item[8]);
                    if (Item[9].Trim() != "")
                        oObjYameyAvodaUpd.HAMARAT_SHABAT = int.Parse(Item[9]);

                    _Collection.AddToFixSizeCollection(oObjYameyAvodaUpd, index);
                }
            }
            catch (Exception ex)
            {
                string ItemList = "index:" + index + ",Count:" + Item.Count() + ",List:";
                foreach (var item in Item)
                {
                    ItemList += item + ",";
                }
                throw new Exception("FillItemsToCollectionY Error: " + ex.Message + "\n" + ex.StackTrace + " mispar_ishi=" + mispar_ishi + ",count:" + _Collection.Count + ItemList);
            }
        }

        protected override void SetCollection()
        {
            CollObject = _Collection;
        }

        protected override void Dispose()
        {
            _Collection.Dispose();
        }
    }
}
