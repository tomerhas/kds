using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.BL;
using KdsLibrary;

namespace KdsBatch
{
   public class clMeafyenyOved
    {
        public string sMeafyen1 = "";
        public int iMeafyen2 = -1;
        public string sMeafyen3 = "";
        public string sMeafyen4 = "";
        public string sMeafyen5 = "";
        public string sMeafyen6 = "";
        public string sMeafyen7 = "";
        public string sMeafyen8 = "";
        public string sMeafyen10 = "";
        public int iMeafyen11 = -1;
       public int iMeafyen12 = -1;
        public int iMeafyen13 = -1;
        public int iMeafyen14 = -1;
        public int iMeafyen16 = -1;
        public int iMeafyen17 = -1;
        public string sMeafyen23 = "";
        public string sMeafyen24 = "";
        public string sMeafyen25 = "";
        public string sMeafyen26 = "";
        public string sMeafyen27 = "";
        public string sMeafyen28 = "";
        public int iMeafyen30 = -1;
        //public int iMeafyen31 = -1;
        public string sMeafyen32 = "";
        public int iMeafyen33 =-1;
        public string sMeafyen41 = "";
        public string sMeafyen42 = "";
        public string sMeafyen43 = "";
        public string sMeafyen44 = "";
        public string sMeafyen45 = "";
        public string sMeafyen47 = "";
        public string sMeafyen50 = "";
        public string sMeafyen51 = "";
        public int iMeafyen53 = -1;
        public int iMeafyen54 = -1;
        public int iMeafyen56 = -1;
        public string sMeafyen57 = "";
        public int iMeafyen60 = -1;
        public string sMeafyen61 = "";
        public string sMeafyen63 = "";   
        public string sMeafyen64 = "";
        public string sMeafyen72 = "";
        public string sMeafyen74 = "";
        public int iMeafyen83 = -1;
        public int iMeafyen84 = -1;
        public int iMeafyen85 = -1;
        public string sMeafyen91 = "";
        public int iMeafyen100 = -1;
        public int iMeafyen101 = -1;
        public int iMeafyen102 = -1;
        public int iMeafyen103 = -1;
        public int iMeafyen104 = -1;
        public int iMeafyen105 = -1; 
       public int iMeafyen106 = -1;
       public int iMeafyen107 = -1;
       public int iMeafyen108 = -1;
       public int iMeafyen110 = -1;

        private DataTable dtMeafyenyOved;
        private bool _bMeafyen1Exists = false;
        private bool _bMeafyen2Exists = false;
        private bool _bMeafyen3Exists = false;
        private bool _bMeafyen4Exists = false;
        private bool _bMeafyen5Exists = false;
        private bool _bMeafyen6Exists = false;
        private bool _bMeafyen7Exists = false;
        private bool _bMeafyen8Exists = false;
        private bool _bMeafyen10Exists = false;
        private bool _bMeafyen11Exists = false;
        private bool _bMeafyen12Exists = false;
        private bool _bMeafyen13Exists = false;
        private bool _bMeafyen14Exists = false;
        private bool _bMeafyen16Exists = false;
        private bool _bMeafyen17Exists = false;
        private bool _bMeafyen23Exists = false;
       private bool _bMeafyen24Exists = false;
       private bool _bMeafyen25Exists = false;
       private bool _bMeafyen26Exists = false;
       private bool _bMeafyen27Exists = false;
       private bool _bMeafyen28Exists = false;
        private bool _bMeafyen30Exists = false;
        //private bool _bMeafyen31Exists = false;
        private bool _bMeafyen32Exists = false;
        private bool _bMeafyen33Exists = false;
        private bool _bMeafyen41Exists = false;
        private bool _bMeafyen42Exists = false;
        private bool _bMeafyen43Exists = false;
        private bool _bMeafyen44Exists = false;
        private bool _bMeafyen45Exists = false;
        private bool _bMeafyen47Exists = false;
        private bool _bMeafyen50Exists = false;
        private bool _bMeafyen51Exists = false;
        private bool _bMeafyen53Exists = false;
        private bool _bMeafyen54Exists = false;
        private bool _bMeafyen56Exists = false;
        private bool _bMeafyen57Exists = false;
        private bool _bMeafyen60Exists = false;
        private bool _bMeafyen61Exists = false;
        private bool _bMeafyen63Exists = false; 
        private bool _bMeafyen64Exists = false;
        private bool _bMeafyen72Exists = false;
        private bool _bMeafyen74Exists = false;
        private bool _bMeafyen83Exists = false;
        private bool _bMeafyen84Exists = false;
        private bool _bMeafyen85Exists = false;
        private bool _bMeafyen91Exists = false;
        private bool _bMeafyen100Exists = false;
        private bool _bMeafyen101Exists = false;
        private bool _bMeafyen102Exists = false;
        private bool _bMeafyen103Exists = false;
        private bool _bMeafyen104Exists = false;
        private bool _bMeafyen105Exists = false;
        private bool _bMeafyen106Exists = false;
        private bool _bMeafyen107Exists = false;
        private bool _bMeafyen108Exists = false;
        private bool _bMeafyen110Exists = false;
        private string _Type;
        public DateTime _Taarich= new DateTime();
        public clMeafyenyOved(int iMisparIshi, DateTime dDate)
        {
            GetMeafyeneyOvdim(iMisparIshi, dDate);
            if (dtMeafyenyOved.Rows.Count > 0)
            {
                SetMeafyneyOved();                
            }
            dtMeafyenyOved.Dispose();
        }

        public clMeafyenyOved(int iMisparIshi, DateTime dDate,string Type)
        {
            _Type = Type;
            _Taarich = dDate;
            dtMeafyenyOved = clCalcData.DtMeafyenyOvedMonth;// GetMeafyeneyOvdim(iMisparIshi, dDate);
            if (dtMeafyenyOved.Rows.Count > 0)
            {
                SetMeafyneyOved();
            }
            dtMeafyenyOved.Dispose();
        }

        public bool Meafyen1Exists
        {
            get { return _bMeafyen1Exists; }
        }
        public bool Meafyen2Exists
        {
            get { return _bMeafyen2Exists; }
        }
        public bool Meafyen3Exists
        {
            get { return _bMeafyen3Exists; }
        }
        public bool Meafyen4Exists
        {
            get { return _bMeafyen4Exists; }
        }
        public bool Meafyen5Exists
        {
            get { return _bMeafyen5Exists; }
        }
        public bool Meafyen6Exists
        {
            get { return _bMeafyen6Exists; }
        }
        public bool Meafyen7Exists
        {
            get { return _bMeafyen7Exists; }
        }
        public bool Meafyen8Exists
        {
            get { return _bMeafyen8Exists; }
        }
        public bool Meafyen10Exists
        {
            get { return _bMeafyen10Exists; }
        }
        public bool Meafyen11Exists
        {
            get { return _bMeafyen11Exists; }
        }
        public bool Meafyen12Exists
        {
            get { return _bMeafyen12Exists; }
        }
        public bool Meafyen13Exists
        {
            get { return _bMeafyen13Exists; }
        }
        public bool Meafyen14Exists
        {
            get { return _bMeafyen14Exists; }
        }
        public bool Meafyen16Exists
        {
            get { return _bMeafyen16Exists; }
        }
        public bool Meafyen17Exists
        {
            get { return _bMeafyen17Exists; }
        }
        public bool Meafyen23Exists
        {
            get { return _bMeafyen23Exists; }
        }
        public bool Meafyen24Exists
        {
            get { return _bMeafyen24Exists; }
        }
        public bool Meafyen25Exists
        {
            get { return _bMeafyen25Exists; }
        }
        public bool Meafyen26Exists
        {
            get { return _bMeafyen26Exists; }
        }
        public bool Meafyen27Exists
        {
            get { return _bMeafyen27Exists; }
        }
        public bool Meafyen28Exists
        {
            get { return _bMeafyen28Exists; }
        }
        public bool Meafyen30Exists
        {
            get { return _bMeafyen30Exists; }
        }

        //public bool Meafyen31Exists
        //{
        //    get { return _bMeafyen31Exists; }
        //}
        public bool Meafyen32Exists
        {
            get { return _bMeafyen32Exists; }
        }
        public bool Meafyen33Exists
        {
            get { return _bMeafyen33Exists; }
        }
        public bool Meafyen41Exists
        {
            get { return _bMeafyen41Exists; }
        }
        public bool Meafyen42Exists
        {
            get { return _bMeafyen42Exists; }
        }
        public bool Meafyen43Exists
        {
            get { return _bMeafyen43Exists; }
        }
        public bool Meafyen44Exists
        {
            get { return _bMeafyen44Exists; }
        }
        public bool Meafyen45Exists
        {
            get { return _bMeafyen45Exists; }
        }

        public bool Meafyen47Exists
        {
            get { return _bMeafyen47Exists; }
        }

        public bool Meafyen50Exists
        {
            get { return _bMeafyen50Exists; }
        }

        public bool Meafyen51Exists
        {
            get { return _bMeafyen51Exists; }
        }
        public bool Meafyen53Exists
        {
            get { return _bMeafyen53Exists; }
        }
        public bool Meafyen54Exists
        {
            get { return _bMeafyen54Exists; }
        }
        public bool Meafyen56Exists
        {
            get { return _bMeafyen56Exists; }
        }
        public bool Meafyen57Exists
        {
            get { return _bMeafyen57Exists; }
        }
        public bool Meafyen60Exists
        {
            get { return _bMeafyen60Exists; }
        }

        public bool Meafyen61Exists
        {
            get { return _bMeafyen61Exists; }
        }

        public bool Meafyen63Exists
        {
            get { return _bMeafyen63Exists; }
        }

        public bool Meafyen64Exists
        {
            get { return _bMeafyen64Exists; }
        }

        public bool Meafyen72Exists
        {
            get { return _bMeafyen72Exists; }
        }
        public bool Meafyen74Exists
        {
            get { return _bMeafyen74Exists; }
        }
        public bool Meafyen83Exists
        {
            get { return _bMeafyen83Exists; }
        }
        public bool Meafyen84Exists
        {
            get { return _bMeafyen84Exists; }
        }
        public bool Meafyen85Exists
        {
            get { return _bMeafyen85Exists; }
        }
        public bool Meafyen91Exists
        {
            get { return _bMeafyen91Exists; }
        }
        public bool Meafyen100Exists
        {
            get { return _bMeafyen100Exists; }
        }
        public bool Meafyen101Exists
        {
            get { return _bMeafyen101Exists; }
        }
        public bool Meafyen102Exists
        {
            get { return _bMeafyen102Exists; }
        }

        public bool Meafyen103Exists
        {
            get { return _bMeafyen103Exists; }
        }

        public bool Meafyen104Exists
        {
            get { return _bMeafyen104Exists; }
        }

        public bool Meafyen105Exists
        {
            get { return _bMeafyen105Exists; }
        }

        public bool Meafyen106Exists
        {
            get { return _bMeafyen106Exists; }
        }

        public bool Meafyen107Exists
        {
            get { return _bMeafyen107Exists; }
        }

        public bool Meafyen108Exists
        {
            get { return _bMeafyen108Exists; }
        }

        public bool Meafyen110Exists
        {
            get { return _bMeafyen110Exists; }
        }

        public void SetOneMeafyen(string sMeafyenNum, ref bool bMeafyenExists, ref string sMeafyenValue)
        {
            DataRow[] drMeafyn;
            string sQury = "";
            try
            {
                bMeafyenExists = false;
                if (_Type == "Calc")
                {
                    sQury = "kod_meafyen=" + sMeafyenNum;
                    sQury += " and Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')>= ME_TAARICH";
                    sQury += " and Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')<= AD_TAARICH";
                    drMeafyn = dtMeafyenyOved.Select(sQury);
                }
                else drMeafyn = dtMeafyenyOved.Select(string.Concat("kod_meafyen=", sMeafyenNum));
                if (drMeafyn.Length > 0)
                {
                    bMeafyenExists = int.Parse(drMeafyn[0]["source_meafyen"].ToString()) == 1;
                    sMeafyenValue = drMeafyn[0]["value_erech_ishi"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetOneMeafyen(string sMeafyenNum, ref bool bMeafyenExists, ref int iMeafyenValue)
        {
            DataRow[] drMeafyn;
            string sQury;
            try
            {
                bMeafyenExists = false;
                if (_Type == "Calc")
                {
                    sQury = "kod_meafyen=" + sMeafyenNum;
                    sQury += " and Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')>= ME_TAARICH";
                    sQury += " and Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')<= AD_TAARICH";
                    drMeafyn = dtMeafyenyOved.Select(sQury);
                }
                else  drMeafyn = dtMeafyenyOved.Select(string.Concat("kod_meafyen=", sMeafyenNum));
                if (drMeafyn.Length > 0)
                {
                    bMeafyenExists = (drMeafyn[0]["source_meafyen"].ToString() == "1");
                    if (drMeafyn[0]["value_erech_ishi"].ToString().Length > 0)
                    {
                        if (clGeneral.IsNumeric(drMeafyn[0]["value_erech_ishi"]))
                        {
                            iMeafyenValue =int.Parse(drMeafyn[0]["value_erech_ishi"].ToString());
                        }
                     }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetMeafyneyOved()
        {
            
            try
            {
                SetOneMeafyen("1", ref  _bMeafyen1Exists, ref sMeafyen1);
                SetOneMeafyen("2", ref  _bMeafyen2Exists, ref iMeafyen2);
                SetOneMeafyen("3", ref  _bMeafyen3Exists,  ref sMeafyen3);
                SetOneMeafyen("4", ref  _bMeafyen4Exists, ref sMeafyen4);
                SetOneMeafyen("5", ref  _bMeafyen5Exists, ref sMeafyen5);
                SetOneMeafyen("6", ref  _bMeafyen6Exists, ref sMeafyen6);
                SetOneMeafyen("7", ref  _bMeafyen7Exists, ref  sMeafyen7);
                SetOneMeafyen("8", ref  _bMeafyen8Exists, ref sMeafyen8);
                SetOneMeafyen("10", ref _bMeafyen10Exists, ref  sMeafyen10);
                SetOneMeafyen("11", ref _bMeafyen11Exists, ref  iMeafyen11);
                SetOneMeafyen("12", ref _bMeafyen12Exists, ref  iMeafyen12);
                SetOneMeafyen("13", ref _bMeafyen13Exists, ref  iMeafyen13);
                SetOneMeafyen("14", ref _bMeafyen12Exists, ref  iMeafyen14);
                SetOneMeafyen("16", ref _bMeafyen16Exists, ref  iMeafyen16);
                SetOneMeafyen("17", ref _bMeafyen17Exists, ref  iMeafyen17);
                SetOneMeafyen("23", ref _bMeafyen23Exists, ref  sMeafyen23);
                SetOneMeafyen("24", ref _bMeafyen24Exists, ref  sMeafyen24);
                SetOneMeafyen("25", ref _bMeafyen25Exists, ref  sMeafyen25);
                SetOneMeafyen("26", ref _bMeafyen26Exists, ref  sMeafyen26);
                SetOneMeafyen("27", ref _bMeafyen27Exists, ref  sMeafyen27);
                SetOneMeafyen("28", ref _bMeafyen28Exists, ref  sMeafyen28);
                SetOneMeafyen("30", ref _bMeafyen30Exists, ref  iMeafyen30);
                //SetOneMeafyen("31", ref _bMeafyen31Exists, ref iMeafyen31);
                SetOneMeafyen("32", ref _bMeafyen32Exists, ref  sMeafyen32);
                SetOneMeafyen("33", ref _bMeafyen33Exists, ref  iMeafyen33);
                SetOneMeafyen("41", ref _bMeafyen41Exists, ref  sMeafyen41);
                SetOneMeafyen("42", ref _bMeafyen42Exists, ref  sMeafyen42);
                SetOneMeafyen("43", ref _bMeafyen43Exists, ref  sMeafyen43);
                SetOneMeafyen("44", ref _bMeafyen44Exists, ref  sMeafyen44);
                SetOneMeafyen("45", ref _bMeafyen45Exists, ref  sMeafyen45);
                SetOneMeafyen("47", ref _bMeafyen47Exists, ref  sMeafyen47);
                SetOneMeafyen("50", ref _bMeafyen50Exists, ref  sMeafyen50);
                SetOneMeafyen("51", ref _bMeafyen51Exists, ref  sMeafyen51);
                SetOneMeafyen("53", ref _bMeafyen53Exists, ref  iMeafyen53);
                SetOneMeafyen("54", ref _bMeafyen54Exists, ref  iMeafyen54);
                SetOneMeafyen("56", ref _bMeafyen56Exists, ref  iMeafyen56);
                SetOneMeafyen("57", ref _bMeafyen57Exists, ref  sMeafyen57);
                SetOneMeafyen("60", ref _bMeafyen60Exists, ref  iMeafyen60);
                SetOneMeafyen("61", ref _bMeafyen61Exists, ref  sMeafyen61);
                SetOneMeafyen("63", ref _bMeafyen63Exists, ref  sMeafyen63);
                
                //עובד במרכז נ.צ.ר
                SetOneMeafyen("64", ref _bMeafyen64Exists, ref  sMeafyen64);
                SetOneMeafyen("72", ref _bMeafyen72Exists, ref  sMeafyen72);
                SetOneMeafyen("74", ref _bMeafyen74Exists, ref  sMeafyen74);
                SetOneMeafyen("83", ref _bMeafyen83Exists, ref  iMeafyen83);
                SetOneMeafyen("84", ref _bMeafyen84Exists, ref  iMeafyen84);
                SetOneMeafyen("85", ref _bMeafyen85Exists, ref  iMeafyen85);
                SetOneMeafyen("91", ref _bMeafyen91Exists, ref  sMeafyen91);
                SetOneMeafyen("100", ref _bMeafyen100Exists, ref  iMeafyen100);
                SetOneMeafyen("101", ref _bMeafyen101Exists, ref  iMeafyen101);
                SetOneMeafyen("102", ref _bMeafyen102Exists, ref  iMeafyen102);
                SetOneMeafyen("103", ref _bMeafyen103Exists, ref  iMeafyen103);
                SetOneMeafyen("104", ref _bMeafyen104Exists, ref  iMeafyen104);
                SetOneMeafyen("105", ref _bMeafyen105Exists, ref  iMeafyen105);
                SetOneMeafyen("106", ref _bMeafyen106Exists, ref  iMeafyen106);
                SetOneMeafyen("107", ref _bMeafyen107Exists, ref  iMeafyen107);
                SetOneMeafyen("108", ref _bMeafyen108Exists, ref  iMeafyen108);
                SetOneMeafyen("110", ref _bMeafyen110Exists, ref  iMeafyen110);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void GetMeafyeneyOvdim(int iMisparIshi, DateTime dCardDate)
        {
            clOvdim oOvdim = new clOvdim();

            try
            {
                dtMeafyenyOved = oOvdim.GetMeafyeneyBitzuaLeOved(iMisparIshi, dCardDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime ConvertMefyenShaotValid(DateTime dTaarich,string sShaaMeafyen)
        {
            DateTime dMeafyenDate;
            string sMeafyen;
            sMeafyen = clGeneral.ConvertToValidHour(sShaaMeafyen);
            if (clGeneral.IsEggedTime(sMeafyen))
            {
                dMeafyenDate = clGeneral.GetDateTimeFromStringHour(clGeneral.ConvertFromEggedTime(sMeafyen),dTaarich.Date).AddDays(1);
            }
            else
            {
                dMeafyenDate = clGeneral.GetDateTimeFromStringHour(sMeafyen, dTaarich.Date);
            }
            return dMeafyenDate;
        }

        
    }
}
