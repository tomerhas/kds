using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Errors
{
    public class FactoryChecker
    {

        public BasicChecker GetInstance(TypeCheck Error, object CurrentInstance)
        {
            BasicChecker creator = null;
            creator.Error = Error;
            if (creator == null)
            {
                switch (Error)
                {
                    case TypeCheck.errHrStatusNotValid:
                        creator = new DayError1(CurrentInstance);
                        break;
                    case TypeCheck.errSimunNesiaNotValid:
                        creator = new DayError27(CurrentInstance);
                        break;
                    case TypeCheck.errLinaValueNotValid:
                        creator = new DayError30(CurrentInstance);
                        break;
                    case TypeCheck.errHalbashaNotvalid:
                        creator = new DayError36(CurrentInstance);
                        break;
                    case TypeCheck.errShbatHashlamaNotValid:
                        creator = new DayError47(CurrentInstance);
                        break;
                    case TypeCheck.errDuplicateShatYetiza:
                        creator = new DayError103(CurrentInstance);
                        break;
                    case TypeCheck.errOvdaInMachalaNotAllowed:
                        creator = new DayError132(CurrentInstance);
                        break;
                    case TypeCheck.errHafifaBetweenSidurim:
                        creator = new DayError167(CurrentInstance);
                        break;
                    case TypeCheck.errHasBothSidurEilatAndSidurVisa:
                        creator = new DayError171(CurrentInstance);
                        break;
                    case TypeCheck.errOvedPeilutNotValid:
                        creator = new DayError172(CurrentInstance);
                        break;
                    case TypeCheck.errAvodatNahagutNotValid:
                        creator = new DayError165(CurrentInstance);
                        break;
                    case TypeCheck.errNesiaMeshtanaNotDefine:
                        creator = new DayError150(CurrentInstance);
                        break;
                    case TypeCheck.errTimeForPrepareMechineNotValid:
                        creator = new DayError86(CurrentInstance);
                        break;
                }
            }
            if (creator == null)
                throw new Exception("Error " + Error + " doesn't not defined");
            return creator;
        }
    }
}
