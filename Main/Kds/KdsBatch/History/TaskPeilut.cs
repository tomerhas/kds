using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.UDT;
using KdsLibrary;


namespace KdsBatch.History
{
    public class TaskPeilut : BaseTask
    {

        private COLL_OBJ_PEILUT_OVDIM _Collection;
        public TaskPeilut()
        {
            ProcedureName = clGeneral.cProInsPeilutOvdimHistory;
            TypeName = "COLL_OBJ_PEILUT_OVDIM";
            ParameterName = "p_coll_obj_peilut_ovdim";
            CollType = TypeTask.Peilut;
            CollObject = new COLL_OBJ_PEILUT_OVDIM();
        }

        protected override void FillItemsToCollection(List<string[]> Items)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdim;
            try
            {
                foreach (string[] Item in Items)
                {
                    oObjPeilutOvdim = new OBJ_PEILUT_OVDIM();

                    oObjPeilutOvdim.MISPAR_ISHI = int.Parse(Item[0]);
                    oObjPeilutOvdim.TAARICH = DateTime.Parse(Item[1]);
                    oObjPeilutOvdim.MISPAR_SIDUR = int.Parse(Item[2]);
                    if (Item[3] != "")
                        oObjPeilutOvdim.SHAT_HATCHALA_SIDUR= DateTime.Parse(Item[3]);
                    if (Item[4] != "")
                        oObjPeilutOvdim.SHAT_YETZIA = DateTime.Parse(Item[4]); 
                    if (Item[5] != "")
                        oObjPeilutOvdim.MISPAR_KNISA = int.Parse(Item[5]);
                    if (Item[6] != "")
                        oObjPeilutOvdim.OTO_NO = int.Parse(Item[6]); 
                    if (Item[7] != "")
                        oObjPeilutOvdim.KISUY_TOR = int.Parse(Item[7]);
                    if (Item[8] != "")
                        oObjPeilutOvdim.SNIF_TNUA = int.Parse(Item[8]);
                    if (Item[9] != "")
                        oObjPeilutOvdim.MISPAR_VISA = int.Parse(Item[9]);
                    if (Item[10] != "")
                        oObjPeilutOvdim.DAKOT_BAFOAL = int.Parse(Item[10]);
                    if (Item[11] != "")
                        oObjPeilutOvdim.KM_VISA = int.Parse(Item[11]);
                    if (Item[12] != "")
                        oObjPeilutOvdim.TEUR_NESIA = Item[12];

                    _Collection.Add(oObjPeilutOvdim);
                }
                CollObject = _Collection;
            }
            catch (Exception ex)
            {
            }
        }
        
    }
}
