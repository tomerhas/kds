using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Data;
using KdsBankShaot.DAL;
using KDSCommon.UDT;
using KDSCommon.Interfaces;
using KDSCommon.DataModels.BankShaot;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers.BankShaot;
using KDSCommon.Interfaces.Logs;

namespace KdsBankShaot.FlowManager
{
    public class BankShaotManager : IBankShaotManager
    {
        private IUnityContainer _container;
        
        public BankShaotManager(IUnityContainer container)
        {
            _container = container;
        }

        public void ExecBankShaot(long BakashaId,DateTime Taarich)
        {
            BankShaotDal dal = new BankShaotDal();
            DataTable TbYechidotBank = new DataTable();
            BudgetData inputData = null;
            COLL_BUDGET oCollBudgets = new COLL_BUDGET();
            try
            {
               
                TbYechidotBank = dal.GetYechidotLeChishuv(Taarich);

                for (int i = 0; i < TbYechidotBank.Rows.Count; i++)
                {
                    try
                    {
                        inputData = FillBudgetData(int.Parse(TbYechidotBank.Rows[i][0].ToString()), Taarich, BakashaId);

                        CalcBudgetToYechida(inputData);

                        oCollBudgets.Add(inputData.objBudget);

                        dal.SaveEmployeesBudget(inputData.kodYechida, Taarich, inputData.RequestId, inputData.UserId);
                    }
                    catch (Exception ex)
                    {
                        _container.Resolve<ILogBakashot>().InsertLog(inputData.RequestId, "E", 0, "ExecBankShaot: yechida= " +inputData.kodYechida +",err: "+ ex.Message,  null);
                    }

                }

                dal.SaveNetuneyBudgets(oCollBudgets);
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogBakashot>().InsertLog(inputData.RequestId, "E", 0, "ExecBankShaot: " + ex.Message, null);
                throw ex;
            }
        }

        private BudgetData FillBudgetData(int kodYechida, DateTime Taarich, long BakashaId)
        {
            BudgetData inputData = new BudgetData();
            BankShaotDal dal = new BankShaotDal();
            DataSet dsNetunim;
            try
            {
                inputData.kodYechida = kodYechida;
                inputData.Taarich = Taarich;
                inputData.Month = DateTime.Parse("01/" + Taarich.Month.ToString() + "/" + Taarich.Year.ToString());
                inputData.oParams = new ParametrimDM(PrepareParametrim(dal.GetParametrim(Taarich)));
                dsNetunim = dal.GetNetuneyOvdimToYechida(kodYechida, Taarich);
                inputData.tbNetuneyYechidot = dsNetunim.Tables[0];// dal.GetNetuneyOvdimToYechida(kodYechida, Taarich);
                inputData.tbNetuneyChishuv = dsNetunim.Tables[1];
                inputData.DtYemeyChol = dal.GetYemeyChol(inputData.Month);
                inputData.cntYemeyChol = inputData.DtYemeyChol.Rows.Count;
                inputData.RequestId = BakashaId;
                inputData.SumMatzevetLechodesh = dal.GetMatzevetMiztaber(kodYechida, Taarich);
               // inputData.UserId = UserId;
                // fill current budget

                inputData.objBudget.KOD_YECHIDA = kodYechida;
                inputData.objBudget.CHODESH = inputData.Month;
                inputData.objBudget.TAARICH = Taarich;
                inputData.objBudget.BAKASHA_ID = BakashaId;
               
               
                return inputData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       private Dictionary<int, Param> PrepareParametrim(DataTable dtParametrim)
       {
           try
           {
               var List = from c in dtParametrim.AsEnumerable()
                          select new
                          {
                              kod = int.Parse(c.Field<string>("kod_param").ToString()),
                              //exist = Int32.Parse(c.Field<string>("source_meafyen").ToString()),
                             value = c.Field<string>("erech"),
                             // erech_ishi = c.Field<string>("erech_ishi_partany")
                          };
               Dictionary<int, Param> Parametrim = List.ToDictionary(item => item.kod, item =>
               {
                   return new Param(item.value);
               }
                   );
               return Parametrim;

           }
           catch (Exception ex)
           {
               throw new Exception("PrepareParametrim :" + ex.Message);
           }
       }
    
       private void CalcBudgetToYechida(BudgetData inputData)
       {
           BankShaotDal dal = new BankShaotDal();
           DataTable TbNetnim = new DataTable();
           DataTable distinctValues=new DataTable();
           DataView view;
           DataRow[] rows;
           float matzevet=0;
           float teken = 0;
           try
           {
              var sumTeken = (from c in inputData.tbNetuneyYechidot.AsEnumerable()
                              where c.Field<decimal>("Teken").Equals(1)
                                && c.Field<Int16>("budget_calc").Equals(1)
                              select c.Field<decimal>("TEKEN_LEISUK")).Sum();
              teken = (sumTeken == null) ? 0 : float.Parse(sumTeken.ToString());
              inputData.objBudget.MICHSA_BASIC = teken * inputData.oParams.GetParam(4).FloatValue;

              inputData.objBudget.AGE_ADDITION = inputData.tbNetuneyChishuv.Select("budget_calc =1 and gil=" + enKodGil.enKashish.GetHashCode()).Length * inputData.oParams.GetParam(2).FloatValue;
              inputData.objBudget.AGE_ADDITION += inputData.tbNetuneyChishuv.Select("budget_calc=1 and gil=" + enKodGil.enKshishon.GetHashCode()).Length * inputData.oParams.GetParam(1).FloatValue;
              inputData.objBudget.HALBASHA_ADDITION = (inputData.tbNetuneyChishuv.Select("budget_calc=1 and meafyen44=1").Length * inputData.oParams.GetParam(3).FloatValue * inputData.cntYemeyChol) / 60;

               rows = inputData.tbNetuneyChishuv.Select("izun_matzevet=1 and meafyen46='1'");
               if (rows.Length > 0)
               {
                   view = new DataView(rows.CopyToDataTable());
                   distinctValues = view.ToTable(true, "ISUK", "YECHIDA_IRGUNIT");
               }

               matzevet = float.Parse(Math.Round(((teken - rows.Length) * (inputData.oParams.GetParam(5).FloatValue / inputData.cntYemeyChol)), 2, MidpointRounding.AwayFromZero).ToString());
               inputData.objBudget.IZUN_MATZEVET_LETEKEN = matzevet;
               inputData.objBudget.IZUN_MATZEVET_LETEKEN_MIZTABER = inputData.SumMatzevetLechodesh + matzevet;
               inputData.objBudget.BUDGET = inputData.objBudget.MICHSA_BASIC + inputData.objBudget.AGE_ADDITION + inputData.objBudget.HALBASHA_ADDITION + inputData.objBudget.IZUN_MATZEVET_LETEKEN_MIZTABER;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
    }
}


