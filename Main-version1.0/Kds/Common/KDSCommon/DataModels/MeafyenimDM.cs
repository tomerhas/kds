using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels
{
    public class MeafyenimDM
    {
        private Dictionary<int, Meafyen> Meafyenim = null;

        public DateTime Taarich { get; set; }

        public MeafyenimDM(Dictionary<int, Meafyen> meafyenim)
        {
            Meafyenim = meafyenim;
        }

        public Meafyen GetMeafyen(int id)
        {
            if (Meafyenim.ContainsKey(id))
                return Meafyenim[id];
            return null;
        }

        public bool IsMeafyenExist(int id)
        {
            if (Meafyenim.ContainsKey(id))
                return Meafyenim[id].IsExist;
            return false;
        }
    }

}
