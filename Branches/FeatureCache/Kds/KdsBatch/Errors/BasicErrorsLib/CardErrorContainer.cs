using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Errors;
using KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors;

namespace KdsBatch.Errors.BasicErrorsLib
{
    public class CardErrorContainer : ICardErrorContainer
    {
        private Dictionary<ErrorTypes, ICardError> _errorDic;
        public CardErrorContainer()
        {
            _errorDic = new Dictionary<ErrorTypes, ICardError>();
        }
        
        public void Add(ICardError cardError)
        {
            _errorDic.Add(cardError.CardErrorType, cardError);
        }

        public ICardError this[ErrorTypes index]
        {
            get { return _errorDic[index]; }
        }

        public void Init()
        {
            Add(new DayErrorHrStatusValid01());
            Add(new DayErrorIsSimunNesiaValid27());
            Add(new DayErrorHrStatusValid01());
        }
    }
}
