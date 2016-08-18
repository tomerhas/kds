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
using KDSCommon.Helpers;

namespace KdsBankShaot.FlowManager
{
    public class BankShaotManager : IBankShaotManager
    {
        private IUnityContainer _container;

        public BankShaotManager(IUnityContainer container)
        {
            _container = container;
        }

        public void ExecBankShaot(long BakashaId)
        {
            BankShaotDal dal = new BankShaotDal();
            DataTable TbYechidotBank = new DataTable();
            DataTable Months = new DataTable();
            BudgetData inputData = null;
            COLL_BUDGET oCollBudgets;
            DateTime Taarich, Month;
            try
            {
                //Month = DateTime.Parse("01/06/2016");
                //Taarich = DateTime.Parse("01/06/2016");
                //for (int j = 0; j <30; j++)
                //{
                //    oCollBudgets = new COLL_BUDGET();
                Months = dal.GetMonthsToCalc();
                foreach (DataRow dr in Months.Rows)
                {
                    oCollBudgets = new COLL_BUDGET();
                    Month = DateTime.Parse(dr["taarich"].ToString());
                    if (Month.Month != DateTime.Now.Month)
                        Taarich = Month.AddMonths(1).AddDays(-1);
                    else Taarich = DateTime.Now.Date;// DateTime.Parse(dr["taarich"].ToString());


                    TbYechidotBank = dal.GetYechidotLeChishuv(Taarich);

                    for (int i = 0; i < TbYechidotBank.Rows.Count; i++)
                    {
                        try
                        {
                            inputData = FillBudgetData(int.Parse(TbYechidotBank.Rows[i][0].ToString()), Month, Taarich, BakashaId);

                            CalcBudgetToYechida(inputData);

                            oCollBudgets.Add(inputData.objBudget);

                            dal.SaveEmployeesBudget(inputData.kodYechida, Taarich, inputData.RequestId, inputData.UserId);
                        }
                        catch (Exception ex)
                        {
                            _container.Resolve<ILogBakashot>().InsertLog(inputData.RequestId, "E", 0, "ExecBankShaot: yechida= " + inputData.kodYechida + ",err: " + ex.Message, null);
                        }

                    }

                    dal.SaveNetuneyBudgets(oCollBudgets);

              //         Taarich = Taarich.AddDays(1);
             }
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogBakashot>().InsertLog(inputData.RequestId, "E", 0, "ExecBankShaot: " + ex.Message, null);
                throw ex;
            }
        }
        public void ExecBankShaotLefiParametrim(long BakashaId,int Mitkan,DateTime Chodesh)
        {
            BankShaotDal dal = new BankShaotDal();
            DataTable TbYechidotBank = new DataTable();
            DataTable Months = new DataTable();
            BudgetData inputData = null;
            COLL_BUDGET oCollBudgets;
            DateTime Taarich, Month,tempdate;
            int num;
            try
            {
                Month = Chodesh;
                Taarich = Chodesh;
                if (DateTime.Now.Month == Chodesh.Month && DateTime.Now.Year == Chodesh.Year)
                {
                    num = DateTime.Now.Day;   
                }
                else
                {
                    tempdate = Chodesh;
                    tempdate = tempdate.AddMonths(1).AddDays(-1);
                    num = tempdate.Day;
                }
                for (int j = 0; j < num-1; j++)
                {
                    oCollBudgets = new COLL_BUDGET();
                    
                    try
                    {
                      
                        inputData = FillBudgetData(Mitkan, Month, Taarich, BakashaId);

                        CalcBudgetToYechida(inputData);

                        oCollBudgets.Add(inputData.objBudget);

                        dal.SaveEmployeesBudget(inputData.kodYechida, Taarich, inputData.RequestId, inputData.UserId);
                    }
                    catch (Exception ex)
                    {
                        _container.Resolve<ILogBakashot>().InsertLog(inputData.RequestId, "E", 0, "ExecBankShaot: yechida= " + inputData.kodYechida + ",err: " + ex.Message, null);
                    }

                    // }

                    dal.SaveNetuneyBudgets(oCollBudgets);

                    Taarich = Taarich.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogBakashot>().InsertLog(inputData.RequestId, "E", 0, "ExecBankShaot: " + ex.Message, null);
                throw ex;
            }
        }
        private BudgetData FillBudgetData(int kodYechida, DateTime Month, DateTime Taarich, long BakashaId)
        {
            BudgetData inputData = new BudgetData();
            BankShaotDal dal = new BankShaotDal();
            DataSet dsNetunim;
            try
            {
                inputData.kodYechida = kodYechida;
                inputData.Taarich = Taarich;
                inputData.Month = Month;// DateTime.Parse("01/" + Taarich.Month.ToString() + "/" + Taarich.Year.ToString());
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

                //var cacheManager = _container.Resolve<IKDSCacheManager>();
                //inputData.SugeyYamimMeyuchadim = cacheManager.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
                //inputData.YamimMeyuchadim = cacheManager.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);

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
            DataTable distinctValues = new DataTable();
            DataView view;
            DataRow[] rows;
            float matzevet = 0;
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



                var sumMatzevet = (from c in inputData.tbNetuneyYechidot.AsEnumerable()
                                   where c.Field<Int16>("izun_matzevet").Equals(1)
                                   && c.Field<decimal>("Teken").Equals(1)
                                   select c.Field<decimal>("matzevet")).Sum();

                var numNewOvdim = inputData.tbNetuneyChishuv.Select("meafyen46 ='1'").Count();

                sumMatzevet -= numNewOvdim;
                matzevet = float.Parse(Math.Round(((teken - float.Parse(sumMatzevet.ToString())) * (inputData.oParams.GetParam(5).FloatValue / inputData.cntYemeyChol)), 2, MidpointRounding.AwayFromZero).ToString());
                inputData.objBudget.IZUN_MATZEVET_LETEKEN = matzevet;
                if (IsYomChol(inputData))
                {
                    inputData.objBudget.IZUN_MATZEVET_LETEKEN_MIZTABER = inputData.SumMatzevetLechodesh + matzevet;
                    inputData.objBudget.ADD_TO_MATZEVET_MIZTABER = 1;
                }
                else inputData.objBudget.IZUN_MATZEVET_LETEKEN_MIZTABER = inputData.SumMatzevetLechodesh;

                inputData.objBudget.BUDGET = inputData.objBudget.MICHSA_BASIC + inputData.objBudget.AGE_ADDITION + inputData.objBudget.HALBASHA_ADDITION + inputData.objBudget.IZUN_MATZEVET_LETEKEN_MIZTABER;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool IsYomChol(BudgetData inputData)
        {
            var dr = inputData.DtYemeyChol.Select("taarich=Convert('" + inputData.Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (dr.Length > 0)
                return true;
            else return false;
        }
    }
}


