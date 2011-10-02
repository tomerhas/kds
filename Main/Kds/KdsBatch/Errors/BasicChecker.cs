﻿using System;
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

        protected void InsertErrortoCardErrors()
        {
            CardError ErrorItem = new CardError();

            ErrorItem.check_num = int.Parse(Error.GetHashCode().ToString());
            switch (_originError)
            {
                case OriginError.Day:
                    ErrorItem.mispar_ishi = DayInstance.oOved.iMisparIshi;
                    ErrorItem.taarich = DayInstance.dCardDate;
                    break;
                case OriginError.Sidur:
                   
                    break;
                case OriginError.Peilut:
                    
                    break;
                default:
                    break;
            }
            GlobalData.CardErrors.Add(ErrorItem);
        }
        protected abstract bool IsCorrect();
        public void Check()
        {
            bIsCorrect = IsCorrect();
            if (bIsCorrect)
                InsertErrortoCardErrors();
        }

    }
}
