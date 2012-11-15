using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.UDT;
using KdsLibrary;


namespace KdsBatch.History
{
    public class TaskDay : BaseTask
    {
        private COLL_YAMEY_AVODA_OVDIM _Collection;
        public TaskDay(string filename, char del)
            : base(filename,del)
        {
            ProcedureName = clGeneral.cProInsYameyAvodaHistory;
            TypeName = "COLL_YAMEY_AVODA_OVDIM";
            ParameterName = "p_coll_yamey_avoda_ovdim";
            CollType = TypeTask.Day;
            _Collection = new COLL_YAMEY_AVODA_OVDIM();
        }

        protected override void FillItemsToCollection(List<string[]> Items)
        {
            OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd;  
            try
            {
                foreach (string[]  Item in Items)
                {
                    oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();

                    oObjYameyAvodaUpd.MISPAR_ISHI = int.Parse(Item[0]);
                    oObjYameyAvodaUpd.TAARICH = GetDateTime(Item[1].Trim());
                  //  oObjYameyAvodaUpd. = DateTime.Parse(Item[1]);
                   // oObjYameyAvodaUpd.TAARICH = DateTime.Parse(Item[1]);
                    if (Item[4].Trim() != "")
                        oObjYameyAvodaUpd.TACHOGRAF = Item[4];
                    if (Item[5].Trim() != "")
                        oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = int.Parse(Item[5]);
                    if (Item[6].Trim() != "")
                        oObjYameyAvodaUpd.HALBASHA = decimal.Parse(Item[6]);
                    if (Item[7].Trim() != "")
                        oObjYameyAvodaUpd.LINA = decimal.Parse(Item[7]);
                    if (Item[8].Trim() != "")
                        oObjYameyAvodaUpd.HASHLAMA_LEYOM = decimal.Parse(Item[8]);
                    if (Item[9].Trim() != "")
                        oObjYameyAvodaUpd.HAMARAT_SHABAT = int.Parse(Item[9]);

                    _Collection.Add(oObjYameyAvodaUpd);
                }
                CollObject = _Collection;
            }
            catch (Exception ex)
            {
            }
        }
        
    }
}
