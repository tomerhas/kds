using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.CalcParallel
{
    public class Meafyen
    {
        public int Kod { get; set; }
        public bool IsExist { get; set; }
        public string Value { get; set; }

        public Meafyen(int kod)
        {
            Kod = kod;
            IsExist = false;
        }
    }
}
