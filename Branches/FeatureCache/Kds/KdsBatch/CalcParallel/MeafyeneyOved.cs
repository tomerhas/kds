using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KdsBatch.CalcParallel
{
    public class MeafyeneyOved
    {
        private DataTable dtMeafyenyOved;
        private List<int> kodMeafyenim = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17, 23, 24, 25, 26, 27, 28, 30, 32, 33, 41, 42, 43, 44, 45, 47, 50, 51, 53, 54, 56, 57, 60, 61, 63, 64, 72, 74, 83, 84, 85, 91, 100, 101, 102, 103, 104, 105, 106, 107, 108, 110 };

        public Dictionary<int, Meafyen> Meafyenim { get; set; }

        public MeafyeneyOved(DataTable dtMeafyenim)
        {
            dtMeafyenyOved = dtMeafyenim;
            if (dtMeafyenyOved.Rows.Count > 0)
                PrepareMeafyenim();
            dtMeafyenyOved.Dispose();
            dtMeafyenyOved = null;
        }

        private void PrepareMeafyenim()
        {
            try
            {
                var List = from c in dtMeafyenyOved.AsEnumerable()
                           select new
                           {
                               kod = Int32.Parse(c.Field<string>("kod_meafyen").ToString()),
                               exist = c.Field<string>("source_meafyen"),
                               value = c.Field<string>("value_erech_ishi")
                           };
                Meafyenim = List.ToDictionary(item => item.kod, item =>
                                                     {
                                                         return new Meafyen((item.exist == "1"), item.value);
                                                     }
                                  );

            }
            catch (Exception ex)
            {
                throw new Exception("PrepareMeafyenim :" + ex.Message);
            }
        }

    }
}
