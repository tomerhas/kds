using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.Enums;

namespace KDSCommon.DataModels.Errors
{
    public class FlowErrorResult
    {
        public FlowErrorResult()
        {
            CardStatus = Enums.CardStatus.Valid;
        }

        public DataTable Errors  { get; set; }
        //This property will be set to false when an error occured in one of the error cards
        public bool IsSuccess { get; set; }
        public CardStatus CardStatus { get; set; }

        /********************/
        public OrderedDictionary htEmployeeDetails { get; set; }
        public OrderedDictionary htFullEmployeeDetails { get; set; } 
    }
}
