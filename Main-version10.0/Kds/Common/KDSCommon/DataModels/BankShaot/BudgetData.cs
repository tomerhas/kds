using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.BankShaot
{
    public class BudgetData
    {
        public BudgetData()
        {
            objBudget = new OBJ_BUDGET();
        }
   
        public DateTime Taarich { get; set; }
        public DateTime Month { get; set; }
        public int UserId { get; set; }
        public bool IsSuccsess { get; set; }

        public int kodYechida { get; set; }
        public long RequestId { get; set; }
        public int cntYemeyChol { get; set; }
        public float SumMatzevetLechodesh { get; set; }

        public DataTable DtParamsBank { get; set; }
        public DataTable tbNetuneyYechidot { get; set; }
        public DataTable tbNetuneyChishuv { get; set; }
        public DataTable DtYemeyChol { get; set; }
        public ParametrimDM oParams { get; set; }

        public OBJ_BUDGET objBudget;
       // public COLL_BUDGET oCollBudgets { get; set; }
    }
}
