using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using System.Data;
using System.IO;
using System.Xml;

namespace KDSCommon.Helpers
{
    public static class StaticBL
    {
        public static int GetMakatType(long lMakat)
        {
            int iMakatType = 0;
            long lTmpMakat = 0;

            lTmpMakat = long.Parse(lMakat.ToString());//.PadRight(8, char.Parse("0")));
            if (lTmpMakat.ToString().Substring(0, 1) == "5" && (lTmpMakat >= 50000000))
                iMakatType = enMakatType.mVisa.GetHashCode(); //6-Visa
            else if ((lTmpMakat >= 100000) && (lTmpMakat < 50000000))
                iMakatType = enMakatType.mKavShirut.GetHashCode(); //1-kav sherut            
            else if ((lTmpMakat >= 60000000) && (lTmpMakat <= 69999999))
                iMakatType = enMakatType.mEmpty.GetHashCode(); //2-Empty
            else if ((lTmpMakat >= 80000000) && (lTmpMakat <= 99999999))
                iMakatType = enMakatType.mNamak.GetHashCode(); //3-Namak
            else if ((lTmpMakat >= 70000000) && (lTmpMakat <= 70099999))
                iMakatType = enMakatType.mVisut.GetHashCode(); //4-ויסות  
            // iMakatType = enMakatType.mElement.GetHashCode();
            else if ((lTmpMakat >= 70100000) && (lTmpMakat <= 79999999))
                iMakatType = enMakatType.mElement.GetHashCode(); //5-Element  
            //else if ((lTmpMakat >= 50000000) && (lTmpMakat <= 59999999))
            //    iMakatType = enMakatType.mVisa.GetHashCode(); //6-Visa
            return iMakatType;
        }

        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
    }
}
