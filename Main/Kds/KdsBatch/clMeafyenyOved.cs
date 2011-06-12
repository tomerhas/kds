using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.BL;
using KdsLibrary;
using KdsBatch.CalcParallel; 

namespace KdsBatch
{
   public class clMeafyenyOved
    {
        private List<int> kodMeafyenim = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 , 10 ,11,12,13,14,15,16,17,23,24,25,26,27,28,30,32,33,41,42,43,44,45,47,50,51,53,54,56,57,60,61,63,64,72,74,83,84,85,91,100,101,102,103,104,105,106,107,108,110};
        public Dictionary<int, Meafyen> Meafyenim { get; set; }
        private DataTable dtMeafyenyOved;
        private string _Type;
        public DateTime _Taarich = new DateTime();

        public clMeafyenyOved(int iMisparIshi, DateTime dDate)
        {
            GetMeafyeneyOvdim(iMisparIshi, dDate);
            if (dtMeafyenyOved.Rows.Count > 0)
            {
                PrepareMeafyenim(); //SetMeafyneyOved();
            }
            dtMeafyenyOved.Dispose();
            dtMeafyenyOved = null;
        }

        public clMeafyenyOved(int iMisparIshi, DateTime dDate, string Type)
        {
            _Type = Type;
            _Taarich = dDate;
            dtMeafyenyOved = clCalcData.DtMeafyenyOvedMonth;// GetMeafyeneyOvdim(iMisparIshi, dDate);
            if (dtMeafyenyOved.Rows.Count > 0)
            {
                SetMeafyneyOved();
            }
            dtMeafyenyOved.Dispose();
            dtMeafyenyOved = null;
        }

        public clMeafyenyOved(int iMisparIshi, DateTime dDate, string Type, DataTable dtMeafyenim)
        {
            _Type = Type;
            _Taarich = dDate;
            dtMeafyenyOved = dtMeafyenim;// GetMeafyeneyOvdim(iMisparIshi, dDate);
            if (dtMeafyenyOved.Rows.Count > 0)
            {
                PrepareMeafyenim();
                //SetMeafyneyOved();
            }
            dtMeafyenyOved.Dispose();
            dtMeafyenyOved = null;
        }

        #region Initialize meafyenim
        public string sMeafyen1
        {
            get
            {
                return Meafyenim[1].Value;
            }
        }
        public int iMeafyen2
        {
            get
            {
                return Meafyenim[2].IntValue;
            }
        }
        public string sMeafyen3
        {
            get
            {
                return Meafyenim[3].Value;
            }
        }
        public string sMeafyen4
        {
            get
            {
                return Meafyenim[4].Value;
            }
        }
        public string sMeafyen5
        {
            get
            {
                return Meafyenim[5].Value;
            }
        }
        public string sMeafyen6
        {
            get
            {
                return Meafyenim[6].Value;
            }
        }
        public string sMeafyen7
        {
            get
            {
                return Meafyenim[7].Value;
            }
        }
        public string sMeafyen8
        {
            get
            {
                return Meafyenim[8].Value;
            }
        }
        public string sMeafyen10
        {
            get
            {
                return Meafyenim[10].Value;
            }
        }
        public int iMeafyen11
        {
            get
            {
                return Meafyenim[11].IntValue;
            }
        }
        public int iMeafyen12
        {
            get
            {
                return Meafyenim[12].IntValue;
            }
        }
        public int iMeafyen13
        {
            get
            {
                return Meafyenim[13].IntValue;
            }
        }
        public int iMeafyen14
        {
            get
            {
                return Meafyenim[14].IntValue;
            }
        }
        public int iMeafyen16
        {
            get
            {
                return Meafyenim[16].IntValue;
            }
        }
        public int iMeafyen17
        {
            get
            {
                return Meafyenim[17].IntValue;
            }
        }
        public string sMeafyen23
        {
            get
            {
                return Meafyenim[23].Value;
            }
        }
        public string sMeafyen24
        {
            get
            {
                return Meafyenim[24].Value;
            }
        }
        public string sMeafyen25
        {
            get
            {
                return Meafyenim[25].Value;
            }
        }
        public string sMeafyen26
        {
            get
            {
                return Meafyenim[26].Value;
            }
        }
        public string sMeafyen27
        {
            get
            {
                return Meafyenim[27].Value;
            }
        }
        public string sMeafyen28
        {
            get
            {
                return Meafyenim[28].Value;
            }
        }
        public int iMeafyen30
        {
            get
            {
                return Meafyenim[30].IntValue;
            }
        }
        //public int iMeafyen31 = -1;
        public string sMeafyen32
        {
            get
            {
                return Meafyenim[32].Value;
            }
        }
        public int iMeafyen33
        {
            get
            {
                return Meafyenim[33].IntValue;
            }
        }
        public string sMeafyen41
        {
            get
            {
                return Meafyenim[41].Value;
            }
        }
        public string sMeafyen42
        {
            get
            {
                return Meafyenim[42].Value;
            }
        }
        public string sMeafyen43
        {
            get
            {
                return Meafyenim[43].Value;
            }
        }
        public string sMeafyen44
        {
            get
            {
                return Meafyenim[44].Value;
            }
        }
        public string sMeafyen45
        {
            get
            {
                return Meafyenim[45].Value;
            }
        }
        public string sMeafyen47
        {
            get
            {
                return Meafyenim[47].Value;
            }
        }
        public string sMeafyen50
        {
            get
            {
                return Meafyenim[50].Value;
            }
        }
        public string sMeafyen51
        {
            get
            {
                return Meafyenim[51].Value;
            }
        }
        public int iMeafyen53
        {
            get
            {
                return Meafyenim[53].IntValue;
            }
        }
        public int iMeafyen54
        {
            get
            {
                return Meafyenim[54].IntValue;
            }
        }
        public int iMeafyen56
        {
            get
            {
                return Meafyenim[56].IntValue;
            }
        }
        public string sMeafyen57
        {
            get
            {
                return Meafyenim[57].Value;
            }
        }
        public int iMeafyen60
        {
            get
            {
                return Meafyenim[60].IntValue;
            }
        }
        public string sMeafyen61
        {
            get
            {
                return Meafyenim[61].Value;
            }
        }
        public string sMeafyen63
        {
            get
            {
                return Meafyenim[63].Value;
            }
        }
        public string sMeafyen64
        {
            get
            {
                return Meafyenim[64].Value;
            }
        }
        public string sMeafyen72
        {
            get
            {
                return Meafyenim[72].Value;
            }
        }
        public string sMeafyen74
        {
            get
            {
                return Meafyenim[74].Value;
            }
        }
        public int iMeafyen83
        {
            get
            {
                return Meafyenim[83].IntValue;
            }
        }
        public int iMeafyen84
        {
            get
            {
                return Meafyenim[84].IntValue;
            }
        }
        public int iMeafyen85
        {
            get
            {
                return Meafyenim[85].IntValue;
            }
        }
        public string sMeafyen91
        {
            get
            {
                return Meafyenim[91].Value;
            }
        }
        public int iMeafyen100
        {
            get
            {
                return Meafyenim[100].IntValue;
            }
        }
        public int iMeafyen101
        {
            get
            {
                return Meafyenim[101].IntValue;
            }
        }
        public int iMeafyen102
        {
            get
            {
                return Meafyenim[102].IntValue;
            }
        }
        public int iMeafyen103
        {
            get
            {
                return Meafyenim[103].IntValue;
            }
        }
        public int iMeafyen104
        {
            get
            {
                return Meafyenim[104].IntValue;
            }
        }
        public int iMeafyen105
        {
            get
            {
                return Meafyenim[105].IntValue;
            }
        }
        public int iMeafyen106
        {
            get
            {
                return Meafyenim[106].IntValue;
            }
        }
        public int iMeafyen107
        {
            get
            {
                return Meafyenim[107].IntValue;
            }
        }
        public int iMeafyen108
        {
            get
            {
                return Meafyenim[108].IntValue;
            }
        }
        public int iMeafyen110
        {
            get
            {
                return Meafyenim[110].IntValue;
            }
        }
        #endregion

        #region Initialize meafyen Exists
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


        public bool Meafyen1Exists
        {
            get { return Meafyenim[1].IsExist; }
        }
        public bool Meafyen2Exists
        {
            get { return Meafyenim[2].IsExist; }
        }
        public bool Meafyen3Exists
        {
            get { return Meafyenim[3].IsExist; }
        }
        public bool Meafyen4Exists
        {
            get { return Meafyenim[4].IsExist; }
        }
        public bool Meafyen5Exists
        {
            get { return Meafyenim[5].IsExist; }
        }
        public bool Meafyen6Exists
        {
            get { return Meafyenim[6].IsExist; }
        }
        public bool Meafyen7Exists
        {
            get { return Meafyenim[7].IsExist; }
        }
        public bool Meafyen8Exists
        {
            get { return Meafyenim[8].IsExist; }
        }
        public bool Meafyen10Exists
        {
            get { return Meafyenim[10].IsExist; }
        }
        public bool Meafyen11Exists
        {
            get { return Meafyenim[11].IsExist; }
        }
        public bool Meafyen12Exists
        {
            get { return Meafyenim[12].IsExist; }
        }
        public bool Meafyen13Exists
        {
            get { return Meafyenim[13].IsExist; }
        }
        public bool Meafyen14Exists
        {
            get { return Meafyenim[14].IsExist; }
        }
        public bool Meafyen16Exists
        {
            get { return Meafyenim[16].IsExist; }
        }
        public bool Meafyen17Exists
        {
            get { return Meafyenim[17].IsExist; }
        }
        public bool Meafyen23Exists
        {
            get { return Meafyenim[23].IsExist; }
        }
        public bool Meafyen24Exists
        {
            get { return Meafyenim[24].IsExist; }
        }
        public bool Meafyen25Exists
        {
            get { return Meafyenim[25].IsExist; }
        }
        public bool Meafyen26Exists
        {
            get { return Meafyenim[26].IsExist; }
        }
        public bool Meafyen27Exists
        {
            get { return Meafyenim[27].IsExist; }
        }
        public bool Meafyen28Exists
        {
            get { return Meafyenim[28].IsExist; }
        }
        public bool Meafyen30Exists
        {
            get { return Meafyenim[30].IsExist; }
        }

        //public bool Meafyen31Exists
        //{
        //    get { return _bMeafyen31Exists; }
        //}
        public bool Meafyen32Exists
        {
            get { return Meafyenim[32].IsExist; }
        }
        public bool Meafyen33Exists
        {
            get { return Meafyenim[33].IsExist; }
        }
        public bool Meafyen41Exists
        {
            get { return Meafyenim[41].IsExist; }
        }
        public bool Meafyen42Exists
        {
            get { return Meafyenim[42].IsExist; }
        }
        public bool Meafyen43Exists
        {
            get { return Meafyenim[43].IsExist; }
        }
        public bool Meafyen44Exists
        {
            get { return Meafyenim[44].IsExist; }
        }
        public bool Meafyen45Exists
        {
            get { return Meafyenim[45].IsExist; }
        }

        public bool Meafyen47Exists
        {
            get { return Meafyenim[47].IsExist; }
        }

        public bool Meafyen50Exists
        {
            get { return Meafyenim[50].IsExist; }
        }

        public bool Meafyen51Exists
        {
            get { return Meafyenim[51].IsExist; }
        }
        public bool Meafyen53Exists
        {
            get { return Meafyenim[53].IsExist; }
        }
        public bool Meafyen54Exists
        {
            get { return Meafyenim[54].IsExist; }
        }
        public bool Meafyen56Exists
        {
            get { return Meafyenim[56].IsExist; }
        }
        public bool Meafyen57Exists
        {
            get { return Meafyenim[57].IsExist; }
        }
        public bool Meafyen60Exists
        {
            get { return Meafyenim[60].IsExist; }
        }

        public bool Meafyen61Exists
        {
            get { return Meafyenim[61].IsExist; }
        }

        public bool Meafyen63Exists
        {
            get { return Meafyenim[63].IsExist; }
        }

        public bool Meafyen64Exists
        {
            get { return Meafyenim[64].IsExist; }
        }

        public bool Meafyen72Exists
        {
            get { return Meafyenim[72].IsExist; }
        }
        public bool Meafyen74Exists
        {
            get { return Meafyenim[74].IsExist; }
        }
        public bool Meafyen83Exists
        {
            get { return Meafyenim[83].IsExist; }
        }
        public bool Meafyen84Exists
        {
            get { return Meafyenim[84].IsExist; }
        }
        public bool Meafyen85Exists
        {
            get { return Meafyenim[85].IsExist; }
        }
        public bool Meafyen91Exists
        {
            get { return Meafyenim[91].IsExist; }
        }
        public bool Meafyen100Exists
        {
            get { return Meafyenim[100].IsExist; }
        }
        public bool Meafyen101Exists
        {
            get { return Meafyenim[101].IsExist; }
        }
        public bool Meafyen102Exists
        {
            get { return Meafyenim[102].IsExist; }
        }

        public bool Meafyen103Exists
        {
            get { return Meafyenim[103].IsExist; }
        }

        public bool Meafyen104Exists
        {
            get { return Meafyenim[104].IsExist; }
        }

        public bool Meafyen105Exists
        {
            get { return Meafyenim[105].IsExist; }
        }

        public bool Meafyen106Exists
        {
            get { return Meafyenim[106].IsExist; }
        }

        public bool Meafyen107Exists
        {
            get { return Meafyenim[107].IsExist; }
        }

        public bool Meafyen108Exists
        {
            get { return Meafyenim[108].IsExist; }
        }

        public bool Meafyen110Exists
        {
            get { return Meafyenim[110].IsExist; }
        }
        #endregion


        //public clMeafyenyOved(int iMisparIshi, DateTime dDate,  DataTable dtMeafyenim)
        //{
        //    _Taarich = dDate;
        //    dtMeafyenyOved = dtMeafyenim;// GetMeafyeneyOvdim(iMisparIshi, dDate);
        //    if (dtMeafyenyOved.Rows.Count > 0)
        //    {
        //        SetMeafyenim();
        //    }
        //    dtMeafyenyOved.Dispose();
        //    dtMeafyenyOved = null;
        //}

       

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
                drMeafyn = null;
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
        //private void SetMeafyenim()
        //{
        //    Meafyenim= new List<Meafyen>();
        //    kodMeafyenim.ForEach(item => 
        //        {
        //            Meafyen _Meafyen = SetMeafyen(item);
        //            Meafyenim.Add(_Meafyen);
        //        });
        //}
       //private Meafyen SetMeafyen(int kod)
       //{
       //     DataRow[] drMeafyn;
       //     string sQury = "";
       //     Meafyen oMeafyen = new  Meafyen(kod);
       //     try
       //     {
       //         sQury = "kod_meafyen=" + oMeafyen.Kod;
       //         //sQury += " and Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')>= ME_TAARICH";
       //         //sQury += " and Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')<= AD_TAARICH";
       //         drMeafyn = dtMeafyenyOved.Select(sQury);
                 
       //         if (drMeafyn.Length > 0)
       //         {
       //             oMeafyen.IsExist = int.Parse(drMeafyn[0]["source_meafyen"].ToString()) == 1;
       //             oMeafyen.Value = drMeafyn[0]["value_erech_ishi"].ToString();
       //         }
       //         return oMeafyen;
       //     }
       //     catch (Exception ex)
       //     {
       //         throw ex;
       //     }
       //}

        private void SetMeafyneyOved()
        {
            
            try
            {
                //SetOneMeafyen("1", ref  _bMeafyen1Exists, ref sMeafyen1);
                //SetOneMeafyen("2", ref  _bMeafyen2Exists, ref iMeafyen2);
                //SetOneMeafyen("3", ref  _bMeafyen3Exists,  ref sMeafyen3);
                //SetOneMeafyen("4", ref  _bMeafyen4Exists, ref sMeafyen4);
                //SetOneMeafyen("5", ref  _bMeafyen5Exists, ref sMeafyen5);
                //SetOneMeafyen("6", ref  _bMeafyen6Exists, ref sMeafyen6);
                //SetOneMeafyen("7", ref  _bMeafyen7Exists, ref  sMeafyen7);
                //SetOneMeafyen("8", ref  _bMeafyen8Exists, ref sMeafyen8);
                //SetOneMeafyen("10", ref _bMeafyen10Exists, ref  sMeafyen10);
                //SetOneMeafyen("11", ref _bMeafyen11Exists, ref  iMeafyen11);
                //SetOneMeafyen("12", ref _bMeafyen12Exists, ref  iMeafyen12);
                //SetOneMeafyen("13", ref _bMeafyen13Exists, ref  iMeafyen13);
                //SetOneMeafyen("14", ref _bMeafyen12Exists, ref  iMeafyen14);
                //SetOneMeafyen("16", ref _bMeafyen16Exists, ref  iMeafyen16);
                //SetOneMeafyen("17", ref _bMeafyen17Exists, ref  iMeafyen17);
                //SetOneMeafyen("23", ref _bMeafyen23Exists, ref  sMeafyen23);
                //SetOneMeafyen("24", ref _bMeafyen24Exists, ref  sMeafyen24);
                //SetOneMeafyen("25", ref _bMeafyen25Exists, ref  sMeafyen25);
                //SetOneMeafyen("26", ref _bMeafyen26Exists, ref  sMeafyen26);
                //SetOneMeafyen("27", ref _bMeafyen27Exists, ref  sMeafyen27);
                //SetOneMeafyen("28", ref _bMeafyen28Exists, ref  sMeafyen28);
                //SetOneMeafyen("30", ref _bMeafyen30Exists, ref  iMeafyen30);
                ////SetOneMeafyen("31", ref _bMeafyen31Exists, ref iMeafyen31);
                //SetOneMeafyen("32", ref _bMeafyen32Exists, ref  sMeafyen32);
                //SetOneMeafyen("33", ref _bMeafyen33Exists, ref  iMeafyen33);
                //SetOneMeafyen("41", ref _bMeafyen41Exists, ref  sMeafyen41);
                //SetOneMeafyen("42", ref _bMeafyen42Exists, ref  sMeafyen42);
                //SetOneMeafyen("43", ref _bMeafyen43Exists, ref  sMeafyen43);
                //SetOneMeafyen("44", ref _bMeafyen44Exists, ref  sMeafyen44);
                //SetOneMeafyen("45", ref _bMeafyen45Exists, ref  sMeafyen45);
                //SetOneMeafyen("47", ref _bMeafyen47Exists, ref  sMeafyen47);
                //SetOneMeafyen("50", ref _bMeafyen50Exists, ref  sMeafyen50);
                //SetOneMeafyen("51", ref _bMeafyen51Exists, ref  sMeafyen51);
                //SetOneMeafyen("53", ref _bMeafyen53Exists, ref  iMeafyen53);
                //SetOneMeafyen("54", ref _bMeafyen54Exists, ref  iMeafyen54);
                //SetOneMeafyen("56", ref _bMeafyen56Exists, ref  iMeafyen56);
                //SetOneMeafyen("57", ref _bMeafyen57Exists, ref  sMeafyen57);
                //SetOneMeafyen("60", ref _bMeafyen60Exists, ref  iMeafyen60);
                //SetOneMeafyen("61", ref _bMeafyen61Exists, ref  sMeafyen61);
                //SetOneMeafyen("63", ref _bMeafyen63Exists, ref  sMeafyen63);
                
                //עובד במרכז נ.צ.ר
                //SetOneMeafyen("64", ref _bMeafyen64Exists, ref  sMeafyen64);
                //SetOneMeafyen("72", ref _bMeafyen72Exists, ref  sMeafyen72);
                //SetOneMeafyen("74", ref _bMeafyen74Exists, ref  sMeafyen74);
                //SetOneMeafyen("83", ref _bMeafyen83Exists, ref  iMeafyen83);
                //SetOneMeafyen("84", ref _bMeafyen84Exists, ref  iMeafyen84);
                //SetOneMeafyen("85", ref _bMeafyen85Exists, ref  iMeafyen85);
                //SetOneMeafyen("91", ref _bMeafyen91Exists, ref  sMeafyen91);
                //SetOneMeafyen("100", ref _bMeafyen100Exists, ref  iMeafyen100);
                //SetOneMeafyen("101", ref _bMeafyen101Exists, ref  iMeafyen101);
                //SetOneMeafyen("102", ref _bMeafyen102Exists, ref  iMeafyen102);
                //SetOneMeafyen("103", ref _bMeafyen103Exists, ref  iMeafyen103);
                //SetOneMeafyen("104", ref _bMeafyen104Exists, ref  iMeafyen104);
                //SetOneMeafyen("105", ref _bMeafyen105Exists, ref  iMeafyen105);
                //SetOneMeafyen("106", ref _bMeafyen106Exists, ref  iMeafyen106);
                //SetOneMeafyen("107", ref _bMeafyen107Exists, ref  iMeafyen107);
                //SetOneMeafyen("108", ref _bMeafyen108Exists, ref  iMeafyen108);
                //SetOneMeafyen("110", ref _bMeafyen110Exists, ref  iMeafyen110);
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
