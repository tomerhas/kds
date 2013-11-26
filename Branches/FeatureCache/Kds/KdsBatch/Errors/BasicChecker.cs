using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsBatch.Entities;

namespace KdsBatch.Errors
{

    public abstract class BasicChecker
    {
        private bool bIsCorrect = false;
        protected Day DayInstance;
        protected Sidur SidurInstance;
        protected Peilut PeilutInstance;
        protected string Comment;
        public int Sadot_Nosafim;
        protected OriginError _originError;
        public TypeCheck Error;

        protected void SetInstance(object CurrentInstance, OriginError origin)
        {
            bool InstanceCreated = false;
            _originError = origin;
            try
            {
                switch (origin)
                {
                    case OriginError.Day:
                        DayInstance = CurrentInstance as Day;
                        InstanceCreated = (DayInstance != null);
                        break;
                    case OriginError.Sidur:
                        SidurInstance = CurrentInstance as Sidur;
                        InstanceCreated = (SidurInstance != null);
                        break;
                    case OriginError.Peilut:
                        PeilutInstance = CurrentInstance as Peilut;
                        InstanceCreated = (PeilutInstance != null);
                        break;
                    default:
                        break;
                }
                if (!(InstanceCreated))
                    throw new Exception("The object set in the constructor doesn't match to the " + origin.ToString() + " check");
            }
            catch
            {
                throw;
            }
        }

        protected void InsertErrortoCardErrors(Day oDay)
        {
            CardError ErrorItem = new CardError();

            ErrorItem.check_num = int.Parse(Error.GetHashCode().ToString());
            ErrorItem.sadot_nosafim = Sadot_Nosafim;
            switch (_originError)
            {
                case OriginError.Day:
                    ErrorItem.mispar_ishi = DayInstance.oOved.iMisparIshi;
                    ErrorItem.taarich = DayInstance.dCardDate;
                    break;
                case OriginError.Sidur:
                    ErrorItem.mispar_ishi = SidurInstance.objDay.oOved.iMisparIshi;
                    ErrorItem.taarich = SidurInstance.objDay.dCardDate;
                    ErrorItem.mispar_sidur = SidurInstance.iMisparSidur;
                    ErrorItem.shat_hatchala = (SidurInstance.sShatHatchala == null ? DateTime.MinValue : SidurInstance.dFullShatHatchala);
                    
                    break;
                case OriginError.Peilut:
                    ErrorItem.mispar_ishi = PeilutInstance.objSidur.objDay.oOved.iMisparIshi;
                    ErrorItem.taarich = PeilutInstance.objSidur.objDay.oOved.dCardDate;
                    ErrorItem.mispar_sidur = PeilutInstance.objSidur.iMisparSidur;
                    ErrorItem.shat_hatchala = (PeilutInstance.objSidur.sShatHatchala == null ? DateTime.MinValue : PeilutInstance.objSidur.dFullShatHatchala);
                    ErrorItem.shat_yetzia = string.IsNullOrEmpty(PeilutInstance.sShatYetzia) ? DateTime.MinValue : PeilutInstance.dFullShatYetzia;
                    ErrorItem.mispar_knisa = PeilutInstance.iMisparKnisa;
                    ErrorItem.makat_nesia = PeilutInstance.lMakatNesia;
                    break;
                default:
                    break;
            }
            oDay.CardErrors.Add(ErrorItem);
        }
        protected abstract bool IsCorrect();
        public void Check(Day oDay)
        {
            try
            {
                bIsCorrect = IsCorrect();
                if (bIsCorrect)
                    InsertErrortoCardErrors(oDay);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }

    public class ErroData
    {
        public string Member1 { get; set; }
    }

    public interface IBaseError
    {
        bool IsCorrect(ErroData errData);
    }

    public class BaseError : IBaseError
    {
        public BaseError()
        {
        }
        Func<ErroData, bool> _funcValidator;
        public BaseError(Func<ErroData,bool> func)
        {
            _funcValidator = func;
        }

        public bool IsCorrect(ErroData errData)
        {
            return _funcValidator(errData);
        }
    }

    public class ErrorNumber1 : IBaseError
    {
        public bool IsCorrect(ErroData errData)
        {
            if (errData.Member1 == "oded")
                return true;
            return false;
        } 
    }

    public class StumErrorUser
    {
        public StumErrorUser()
        {
            

            ErroData errData = new ErroData(){ Member1="oded"};
            int id = 2;
            //err.IsCorrect(errData);
            //err.IsCorrect(new 

            Dictionary<int, IBaseError> _dic = new Dictionary<int, IBaseError>();
            _dic.Add(1, new ErrorNumber1());
            BaseError err = new BaseError(obj =>
            {
                if (obj.Member1 == "oded")
                    return true;
                return false;
            });
            _dic.Add(2, err);

            BaseError err2 = new BaseError(obj =>
            {
                if (obj.Member1 == "merav")
                    return true;
                return false;
            });
            _dic.Add(3, err2);
            
            _dic[2].IsCorrect(errData);
        }

        
    }
}
