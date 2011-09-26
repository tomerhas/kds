using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsBatch.Entities;

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
                    ListOfChecks.Add(error.GetInstance(item, this));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Run()
        {
            try
            {
                _SpecificErrors.ForEach(item =>
                {
                    ListOfChecks.ForEach(itemErr => itemErr.Check());
                });
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
    }
}
