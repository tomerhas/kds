using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KdsBatch.Entities
{
    public class Day
    {
        List<Sidur> Sidurim;

        Day()
        {
            Init();
        }
        void Init()
        {
            Sidurim = new List<Sidur>();
            DataTable dt = GetSidurim();
            Sidur item = new Sidur();
        }

        DataTable GetSidurim()
        {
            DataTable dt = null;
            return dt; 
        }


    }
}
