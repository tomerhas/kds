using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;


namespace KdsBatch.Premia
{
    /// <summary>
    /// Represents a single row for premia calculation
    /// </summary>
    public class PremiaItem
    {
        public int EmployeeNumber { get; set; }
        public DateTime WorkDate { get; set; }
        public string Station { get; set; }
        public int Region { get; set; }
        public string PremiaCode { get; set; }
        public int Position { get; set; }
        public string Mutaam { get; set; }
        public int AgeCode { get; set; }
        public int Occupation { get; set; }
        public string Char60 { get; set; }
        public string Char74 { get; set; }
        public int DaysQuantity { get; set; }
        public double MinutesQuantity { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string RechivCode { get; set; }
        public object[] GetExcelRowValues()
        {
            return new object[]{EmployeeNumber, Station, WorkDate.ToString("yyyyMM"),
                PremiaCode, Region, Position, Mutaam, AgeCode, Occupation, Char60, Char74, DaysQuantity,
                MinutesQuantity, Lastname, Firstname};
        }

        public static PremiaItem GetItemFromDataRow(System.Data.DataRow dr)
        {
            var item = new PremiaItem();
            item.EmployeeNumber = Convert.ToInt32(dr["MISPAR_ISHI"]);
            item.WorkDate = Convert.ToDateTime(dr["TAARICH"]);
            item.Station = dr["mispar_tachana"].ToString();
            item.Region = Convert.ToInt32(dr["EZOR"]);
            item.PremiaCode = dr["KOD_PREMIA"].ToString();
            item.Position = Convert.ToInt32(dr["MAAMAD"]);
            item.Mutaam = dr["MUTAAM"].ToString();
            item.Occupation = Convert.ToInt32(dr["ISUK"]);
            item.AgeCode = Convert.ToInt32(dr["GIL"]);
            item.Char60 = dr["meafeyn_60"].ToString();
            item.Char74 = dr["meafeyn_74"].ToString();
            item.Lastname = dr["SHEM_MISH"].ToString();
            item.Firstname = dr["SHEM_PRAT"].ToString();

            return item;
        }

        public static PremiaItem GetItemFromExcelDataRow(System.Data.DataRow dr)
        {
            try
            {
                var item = new PremiaItem();
                if (clGeneral.IsNumeric(dr[PremiaFileImporter.GetExcelColumnIndex("A")].ToString()))
                    item.EmployeeNumber = Convert.ToInt32(dr[PremiaFileImporter.GetExcelColumnIndex("A")]);
                else return null;

                string strDate = dr[PremiaFileImporter.GetExcelColumnIndex("BW")].ToString();
                if (clGeneral.IsNumeric(strDate) && strDate.Length > 4)
                {
                    item.WorkDate = new DateTime(int.Parse(strDate.Substring(0, 4)),
                    int.Parse(strDate.Substring(4, 2)), 1);
                }
                else return null;
                // item.Station = dr[PremiaFileImporter.GetExcelColumnIndex("C")].ToString();
                if (clGeneral.IsNumeric(dr[PremiaFileImporter.GetExcelColumnIndex("B")].ToString()))
                    item.PremiaCode = dr[PremiaFileImporter.GetExcelColumnIndex("B")].ToString();
                else return null;

                return item;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
