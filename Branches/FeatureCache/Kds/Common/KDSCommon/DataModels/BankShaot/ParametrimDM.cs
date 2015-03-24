using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDSCommon.DataModels.BankShaot
{
    public class ParametrimDM
    {
        private Dictionary<int, Param> Parametrim = null;

        public DateTime Taarich { get; set; }

        public ParametrimDM(Dictionary<int, Param> parametrim)
        {
            Parametrim = parametrim;
        }

        public Param GetParam(int id)
        {
            if (Parametrim.ContainsKey(id))
                return Parametrim[id];
            return null;
        }

        public bool IsParamExist(int id)
        {
            if (Parametrim.ContainsKey(id))
                return Parametrim[id].IsExist;
            return false;
        }
    }
  
}
