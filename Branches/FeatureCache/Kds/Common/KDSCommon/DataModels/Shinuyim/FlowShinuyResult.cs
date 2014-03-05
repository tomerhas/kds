using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.Shinuyim
{
    public class FlowShinuyResult
    {
        public FlowShinuyResult()
        {
          //  CardStatus = Enums.CardStatus.Valid;
        }

        //This property will be set to false when an error occured in one of the error cards
        public bool IsSuccess { get; set; }
    }
}
