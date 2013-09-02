using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsBatch.Entities;
using KdsLibrary.BL;

namespace KdsBatch.Errors
{
    public abstract class BasicErrors
    {
        private List<TypeCheck> _SpecificErrors;

        protected OriginError _originError;
        protected List<BasicChecker> ListOfChecks;
        public BasicErrors(OriginError originError)
        {
            _originError = originError;
            _SpecificErrors = GlobalData.ActiveErrors.FindAll(item => item.Origin == _originError).Select(Error => Error.Id).ToList();
        }
        public void InitializeErrors()
        {
            ListOfChecks = new List<BasicChecker>();
            try
            {

                _SpecificErrors.ForEach(item =>
                {
                    FactoryChecker error = new FactoryChecker();
                    ListOfChecks.Add(error.GetInstance(item, this, _originError));

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Run(Day oDay)
        {
            try
            {
                ListOfChecks.ForEach(itemErr => itemErr.Check(oDay));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertAllError()
        {
            // implement here InsertErrorsToTbShgiot
        }

        public void RemoveShgiotMeusharotFromDt(ref string sArrKodShgia, Day oDay)
        {
            clWorkCard.ErrorLevel iErrorLevel;
            bool bMeushar;
            int iCount;
            iCount = oDay.CardErrors.Count;
            CardError ErrorItem = new CardError();
            int I = 0;
            try
            {
                sArrKodShgia = "";
                if (iCount > 0)
                {
                    do
                    {
                        bMeushar = false;
                        ErrorItem = oDay.CardErrors[I];
                        if (ErrorItem.shat_yetzia != DateTime.MinValue)
                        {
                            iErrorLevel = clWorkCard.ErrorLevel.LevelPeilut;
                            bMeushar = clWorkCard.IsErrorApprovalExists(iErrorLevel, (int)ErrorItem.check_num, (int)ErrorItem.mispar_ishi, DateTime.Parse(ErrorItem.taarich.ToString()), (int)ErrorItem.mispar_sidur, DateTime.Parse(ErrorItem.shat_hatchala.ToString()), DateTime.Parse(ErrorItem.shat_yetzia.ToString()), (int)ErrorItem.mispar_knisa);

                        }
                        else if (string.IsNullOrEmpty(ErrorItem.mispar_sidur.ToString()))
                        {
                            iErrorLevel = clWorkCard.ErrorLevel.LevelYomAvoda;
                            bMeushar = clWorkCard.IsErrorApprovalExists(iErrorLevel, (int)ErrorItem.check_num, (int)ErrorItem.mispar_ishi, DateTime.Parse(ErrorItem.taarich.ToString()), 0, DateTime.MinValue, DateTime.MinValue, 0);

                        }
                        else
                        {
                            iErrorLevel = clWorkCard.ErrorLevel.LevelSidur;
                            bMeushar = clWorkCard.IsErrorApprovalExists(iErrorLevel, (int)ErrorItem.check_num, (int)ErrorItem.mispar_ishi, DateTime.Parse(ErrorItem.taarich.ToString()), (int)ErrorItem.mispar_sidur, DateTime.Parse(ErrorItem.shat_hatchala.ToString()), DateTime.MinValue, 0);
                        }


                        if (bMeushar)
                        {
                            oDay.CardErrors.Remove(ErrorItem);
                        }
                        else
                        {
                            sArrKodShgia += ErrorItem.check_num.ToString() + ",";
                            I += 1;
                        }

                        iCount = oDay.CardErrors.Count;
                    }
                    while (I < iCount);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
