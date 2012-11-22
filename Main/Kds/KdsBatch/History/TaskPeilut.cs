﻿using System;
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
        public TaskPeilut(long lRequestNum,string filename, char del)
            : base(lRequestNum,filename, del)
        {
            ProcedureName = clGeneral.cProInsPeilutOvdimHistory;
            TypeName = "COLL_OBJ_PEILUT_OVDIM";
            ParameterName = "p_coll_obj_peilut_ovdim";
            CollType = TypeTask.Peilut;
            _Collection = new COLL_OBJ_PEILUT_OVDIM();
        }

        protected override void FillItemsToCollection(List<string[]> Items)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdim;
            int mispar_ishi = 0;
            try
            {
                foreach (string[] Item in Items)
                {
                    oObjPeilutOvdim = new OBJ_PEILUT_OVDIM();

                    mispar_ishi = int.Parse(Item[0]);
                    if (mispar_ishi != 999999999)
                    {
                        oObjPeilutOvdim.MISPAR_ISHI = int.Parse(mispar_ishi.ToString().Substring(0, mispar_ishi.ToString().Length - 1));
                        oObjPeilutOvdim.TAARICH = GetDateTime(Item[1].Trim());
                        oObjPeilutOvdim.MISPAR_SIDUR = int.Parse(Item[2]);
                        if (Item[3].Trim() != "")
                            oObjPeilutOvdim.SHAT_HATCHALA_SIDUR = GetDateTime(Item[3].Trim());
                        if (Item[4].Trim() != "")
                            oObjPeilutOvdim.SHAT_YETZIA = GetDateTime(Item[4].Trim());
                        if (Item[5].Trim() != "")
                            oObjPeilutOvdim.MISPAR_KNISA = int.Parse(Item[5]);
                        if (Item[6].Trim() != "")
                            oObjPeilutOvdim.MAKAT_NESIA = int.Parse(Item[6]);
                        if (Item[7].Trim() != "")
                            oObjPeilutOvdim.OTO_NO = int.Parse(Item[7]);
                        if (Item[8].Trim() != "")
                            oObjPeilutOvdim.KISUY_TOR = int.Parse(Item[8]);
                       if (Item[9].Trim() != "")
                            oObjPeilutOvdim.SNIF_TNUA = int.Parse(Item[9]);
                        if (Item[10].Trim() != "")
                            oObjPeilutOvdim.MISPAR_VISA = int.Parse(Item[10]);
                        if (Item[11].Trim() != "")
                            oObjPeilutOvdim.DAKOT_BAFOAL = int.Parse(Item[11]);
                        if (Item[12].Trim() != "")
                            oObjPeilutOvdim.KM_VISA = int.Parse(Item[12]);
                       if (Item[13].Trim() != "")
                           oObjPeilutOvdim.TEUR_NESIA = Item[13];

                        _Collection.Add(oObjPeilutOvdim);
                    }
                }
                CollObject = _Collection;
            }
            catch (Exception ex)
            {
                throw new Exception("FillItemsToCollectionP Error: " + ex.Message + " mispar_ishi=" + mispar_ishi);
            }
        }
        
    }
}
