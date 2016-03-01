using KDSCommon.DataModels;
using KDSCommon.DataModels.Nezer;
using KDSCommon.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSBLLogic.Managers
{
    public class NezerMergeManager : INezerMergeManager
    {

        public void Merge(DataTable dtNezer,OrderedDictionary odSidurim)
        {
            int MisparSidur,i;
            long MakatNesia;
            DateTime ShatYetzia;
            bool flag = false;
            foreach (DataRow drNezer in dtNezer.Rows)
            {
                flag = false;
                MisparSidur = int.Parse(drNezer["mispar_sidur"].ToString());
                ShatYetzia = DateTime.Parse(drNezer["SHAT_YETZIA"].ToString());
                MakatNesia=long.Parse(drNezer["makat_nesia"].ToString());

                odSidurim.Values.Cast<SidurDM>().ToList().ForEach(sidur => 
                {
                    i=0;
                    PeilutDM oPeilut = sidur.htPeilut.Values.Cast<PeilutDM>().ToList().SingleOrDefault(peilut => peilut.dFullShatYetzia == ShatYetzia && peilut.lMakatNesia == MakatNesia);
                    if (oPeilut != null)
                    {
                        InsertPeilutNezer(oPeilut, drNezer);
                    }
                    else
                    {
                        if (sidur.iMisparSidur == MisparSidur && ShatYetzia >= sidur.dFullShatHatchala && ShatYetzia <= sidur.dOldFullShatGmar)
                        {
                           sidur.htPeilut.Values.Cast<PeilutDM>().ToList().ForEach(peilut =>
                            {
                                if (ShatYetzia < peilut.dFullShatYetzia)
                                {
                                    if (!flag)
                                    {
                                        flag = true;
                                        CreateNewPeilut(sidur, drNezer, i);
                                    }
                                    
                                }
                                i++;
                            });
                            if (!flag)
                                CreateNewPeilut(sidur, drNezer, i);
                        }
                    }
                    
                });

                
            }
        }

        private void InsertPeilutNezer(PeilutDM oPeilut, DataRow drNezer)
        {
            oPeilut.NezerContainer.Nezer = new NezerDM();
            oPeilut.NezerContainer.Nezer.MisparIshi = int.Parse(drNezer["mispar_ishi"].ToString());
            oPeilut.NezerContainer.Nezer.Taarich = DateTime.Parse(drNezer["taarich"].ToString());
            oPeilut.NezerContainer.Nezer.MisparSidur = int.Parse(drNezer["mispar_sidur"].ToString());
            oPeilut.NezerContainer.Nezer.ShatYetzia = DateTime.Parse(drNezer["SHAT_YETZIA"].ToString());
            oPeilut.NezerContainer.Nezer.MakatNesia = long.Parse(drNezer["makat_nesia"].ToString());
            oPeilut.NezerContainer.Nezer.OtoNo = int.Parse(drNezer["oto_no"].ToString());
            oPeilut.NezerContainer.Nezer.SnifTnua = int.Parse(drNezer["snif_tnua"].ToString());
        }
        private void CreateNewPeilut(SidurDM oSidur, DataRow drNezer, int i)
        {
            PeilutDM oNewPeilut = new PeilutDM();
            InsertPeilutNezer(oNewPeilut,  drNezer);
            oSidur.htPeilut.Insert(i, oSidur.htPeilut.Count + 1, oNewPeilut);
        }
        //public void Merge(DataTable dtNezer,OrderedDictionary odSidurim)
        //{
        //    //Get the peilus
        //    List<PeilutDM> peiluts =  GetAllPeiluts(odSidurim);
        //    //Iterate over all nezer and find list of contained
        //   List<int> ContainedIds
        //}

        ///// <summary>
        ///// Get all the peiluts from the list of sidurim
        ///// </summary>
        ///// <param name="odSidurim"></param>
        ///// <returns></returns>
        //private List<PeilutDM> GetAllPeiluts(OrderedDictionary odSidurim)
        //{
        //    List<PeilutDM> peiluts = new List<PeilutDM>();
        //    foreach (var sidurObj in odSidurim.Values)
        //    {
        //        var sidur = sidurObj as SidurDM;
        //        if (sidur != null)
        //        {
        //            if (sidur.htPeilut != null)
        //            {
        //                foreach (var peilutObj in sidur.htPeilut.Values)
        //                {
        //                    PeilutDM peilutDm = peilutObj as PeilutDM;
        //                    if (peilutDm != null)
        //                    {
        //                        peiluts.Add(peilutDm);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return peiluts;
        //}
    }
}
