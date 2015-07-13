﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Configuration;
using KDSCommon.UDT;

namespace KdsBatch.History
{
    public class TaskSidur : BaseTask
    {
        private COLL_SIDURIM_OVDIM _Collection;
        public TaskSidur(long lRequestNum,string filename, char del)
            : base(lRequestNum,filename, del)
        {
            ProcedureName = clGeneral.cProInsSidurimOvdimHistory;
            TypeName = "COLL_SIDURIM_OVDIM";
            ParameterName = "p_coll_sidurim_ovdim";
            CollType = TypeTask.Sidur;
           // _Collection = new COLL_SIDURIM_OVDIM();
            Pattern = "BZAS";
            PathDirectory = ConfigurationManager.AppSettings["PathFileMF"];
            PathDirectoryOld = ConfigurationManager.AppSettings["PathFileMFOld"];
        }

        protected override void AllocateCollection()
        {
            _Collection = new COLL_SIDURIM_OVDIM(RecordsCount-1);
        }
        protected override void FillItemsToCollection(string[] Item, int index)
        {
            OBJ_SIDURIM_OVDIM oObjSidurimOvdim;
            int mispar_ishi = 0;
            try
            {
                oObjSidurimOvdim = new OBJ_SIDURIM_OVDIM();


                mispar_ishi = int.Parse(Item[0]);

                if (mispar_ishi != 999999999)
                {
                    oObjSidurimOvdim.MISPAR_ISHI = int.Parse(mispar_ishi.ToString().Substring(0, mispar_ishi.ToString().Length - 1));
                    oObjSidurimOvdim.TAARICH = GetDateTime(Item[1].Trim());
                    oObjSidurimOvdim.MISPAR_SIDUR = int.Parse(Item[2]);
                    if (Item[3].Trim() != "")
                        oObjSidurimOvdim.SHAT_HATCHALA = GetDateTime(Item[3].Trim());
                    if (Item[4].Trim() != "")
                        oObjSidurimOvdim.SHAT_GMAR = GetDateTime(Item[4].Trim());
                    if (Item[5].Trim() != "")
                        oObjSidurimOvdim.PITZUL_HAFSAKA = int.Parse(Item[5]);
                    if (Item[6].Trim() != "")
                        oObjSidurimOvdim.CHARIGA = int.Parse(Item[6]);
                    if (Item[7].Trim() != "")
                        oObjSidurimOvdim.HASHLAMA = int.Parse(Item[7]);
                    if (Item[8].Trim() != "")
                        oObjSidurimOvdim.YOM_VISA = int.Parse(Item[8]);
                    if (Item[9].Trim() != "")
                        oObjSidurimOvdim.LO_LETASHLUM = int.Parse(Item[9]);
                    if (Item[10].Trim() != "")
                        oObjSidurimOvdim.OUT_MICHSA = int.Parse(Item[10]);
                    if (Item[11].Trim() != "")
                        oObjSidurimOvdim.MIKUM_SHAON_KNISA = int.Parse(Item[11]);
                    if (Item[12].Trim() != "")
                        oObjSidurimOvdim.MIKUM_SHAON_YETZIA = int.Parse(Item[12]);
                    if (Item[13].Trim() != "")
                        oObjSidurimOvdim.MISPAR_MUSACH_O_MACHSAN = int.Parse(Item[13]);
                    if (Item[14].Trim() != "")
                        oObjSidurimOvdim.SHAYAH_LEYOM_KODEM = int.Parse(Item[14]);
                    if (Item[15].Trim() != "")
                        oObjSidurimOvdim.MISPAR_SHIUREY_NEHIGA = int.Parse(Item[15]);
                    if (Item[16].Trim() != "")
                        oObjSidurimOvdim.SUG_HAZMANAT_VISA = int.Parse(Item[16]);
                    if (Item[17].Trim() != "")
                        oObjSidurimOvdim.TAFKID_VISA = int.Parse(Item[17]);
                    if (Item[18].Trim() != "")
                        oObjSidurimOvdim.SUG_SIDUR = int.Parse(Item[18]);

                    // clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, "_Collection.Count= " + _Collection.Count);
                    _Collection.AddToFixSizeCollection(oObjSidurimOvdim, index);
                }
            }
            catch (Exception ex)
            {
                string ItemList = "index:"+ index +",Count:" + Item.Count() + ",List:";
                foreach (var item in Item)
                {
                    ItemList += item + ",";
                }
                throw new Exception("FillItemsToCollectionS Error: " + ex.Message +"\n" + ex.StackTrace+ " mispar_ishi=" + mispar_ishi + ",count:" + _Collection.Count + ItemList);

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