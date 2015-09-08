using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DalOraInfra.DAL;


namespace KdsLibrary.BL
{
    public class clDynamicForms
    {

        private clDal _Dal;
        private static clDynamicForms _Instance;
        //  private clSidurim (){}

        public static clDynamicForms GetInstance()
        {
            if (_Instance == null)
                _Instance = new clDynamicForms();
            return _Instance;
        }
        #region Sidur,Type of sidur, Elements Kod - Description
        public string GetDescriptionOfKod(int iKod, clGeneral.enDynamicFormType DynamicFormType)
        {
            _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_Kod", ParameterType.ntOracleInteger, iKod, ParameterDir.pdInput);
                _Dal.AddParameter("p_Desc", ParameterType.ntOracleVarchar, null, ParameterDir.pdOutput, 200);
                switch (DynamicFormType)
                {
                    case clGeneral.enDynamicFormType.Elements:
                        _Dal.ExecuteSP(clGeneral.cProGetElementDescByKod);
                        break;
                    case clGeneral.enDynamicFormType.SpecialSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetSidurDescbyKod);
                        break;
                    case clGeneral.enDynamicFormType.SugSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetTypeSidurDescByKod);
                        break;
                    case clGeneral.enDynamicFormType.Parameters:
                        _Dal.ExecuteSP(clGeneral.cProGetParameterDescByKod);
                        break;
                    case clGeneral.enDynamicFormType.ComponentSidurim:
                        _Dal.ExecuteSP(clGeneral.cProGetComponentSidurDescByKod);
                        break;
                    case clGeneral.enDynamicFormType.ComponentTypeSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetComponentTypeSidurDescByKod);
                        break;
                }
                return _Dal.GetValParam("p_Desc");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RefreshTable(clGeneral.enDynamicFormType DynamicFormType)
        {
            _Dal = new clDal();
            try
            {
                switch (DynamicFormType)
                {
                    case clGeneral.enDynamicFormType.Elements:
                        _Dal.ExecuteSP(clGeneral.cProRefreshMeafyeneyElement);
                        break;
                    case clGeneral.enDynamicFormType.SpecialSidur:
                        _Dal.ExecuteSP(clGeneral.cProRefreshSidurim);
                        break;
                    case clGeneral.enDynamicFormType.SugSidur:
                        _Dal.ExecuteSP(clGeneral.cProRefreshSugSidur);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetKodOfDescription(string Description, clGeneral.enDynamicFormType DynamicFormType)
        {
            _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_Desc", ParameterType.ntOracleVarchar, Description, ParameterDir.pdInput);
                _Dal.AddParameter("p_Kod", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                switch (DynamicFormType)
                {
                    case clGeneral.enDynamicFormType.Elements:
                        _Dal.ExecuteSP(clGeneral.cProGetKodByElementDesc);
                        break;
                    case clGeneral.enDynamicFormType.SpecialSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetKodBySidurDesc);
                        break;
                    case clGeneral.enDynamicFormType.SugSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetKodByTypeSidurDesc);
                        break;
                    case clGeneral.enDynamicFormType.Parameters:
                        _Dal.ExecuteSP(clGeneral.cProGetKodByParametersDesc);
                        break;
                    case clGeneral.enDynamicFormType.ComponentSidurim:
                        _Dal.ExecuteSP(clGeneral.cProGetKodByComponentSidurDesc);
                        break;
                    case clGeneral.enDynamicFormType.ComponentTypeSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetKodByComponentTypeSidurDesc);
                        break;
                }

                return _Dal.GetValParam("p_Kod").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public string[] GetMatchingKod(string Kod, clGeneral.enDynamicFormType DynamicFormType)
        {
            string[] sResult = null;
            _Dal = new clDal();
            DataTable Dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Kod, ParameterDir.pdInput, 200);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                switch (DynamicFormType)
                {
                    case clGeneral.enDynamicFormType.Elements:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingElementKod, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "KOD_ELEMENT");
                        break;
                    case clGeneral.enDynamicFormType.SpecialSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingSidurKod, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "KOD_SIDUR_MEYUCHAD");
                        break;
                    case clGeneral.enDynamicFormType.SugSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingTypeSidurKod, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "KOD_SIDUR_AVODA");
                        break;
                    case clGeneral.enDynamicFormType.Parameters:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingParametersKod, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "KOD_PARAM");
                        break;
                    case clGeneral.enDynamicFormType.ComponentSidurim:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingComponentSidurKod, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "KOD_SIDUR_MEYUCHAD");
                        break;
                    case clGeneral.enDynamicFormType.ComponentTypeSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingComponentTypeSidurKod, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "KOD_SIDUR_AVODA");
                        break;
                }
                return sResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string[] GetMatchingDescription(string Description, clGeneral.enDynamicFormType DynamicFormType)
        {
            string[] sResult = null;
            _Dal = new clDal();
            DataTable Dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Description, ParameterDir.pdInput, 200);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                switch (DynamicFormType)
                {
                    case clGeneral.enDynamicFormType.Elements:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingElementDesc, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "TEUR_ELEMENT");
                        break;
                    case clGeneral.enDynamicFormType.SpecialSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingSidurDesc, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "TEUR_SIDUR_MEYCHAD");
                        break;
                    case clGeneral.enDynamicFormType.SugSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingTypeSidurDesc, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "TEUR_SIDUR_AVODA");
                        break;
                    case clGeneral.enDynamicFormType.Parameters:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingParametersDesc, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "TEUR_PARAM");
                        break;
                    case clGeneral.enDynamicFormType.ComponentSidurim:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingComponentSidurDesc, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "TEUR_SIDUR_MEYCHAD");
                        break;
                    case clGeneral.enDynamicFormType.ComponentTypeSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetMatchingComponentTypeSidurDesc, ref Dt);
                        sResult = clGeneral.ConvertDatatableColumnToStringArray(Dt, "TEUR_SIDUR_AVODA");
                        break;
                }
                return sResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetHistory(string FilterKod ,string Kod,string ToDate, clGeneral.enDynamicFormType DynamicFormType)
        {
            _Dal = new clDal();
            DataTable Dt = new DataTable();
            try
            {
                if (DynamicFormType != clGeneral.enDynamicFormType.Parameters)
                    _Dal.AddParameter("p_FilterKod", ParameterType.ntOracleVarchar, FilterKod, ParameterDir.pdInput, 200);
                if (!((DynamicFormType == clGeneral.enDynamicFormType.ComponentSidurim) |(DynamicFormType == clGeneral.enDynamicFormType.ComponentTypeSidur)))
                    _Dal.AddParameter("p_Kod", ParameterType.ntOracleVarchar, Kod, ParameterDir.pdInput, 200);
                _Dal.AddParameter("p_ToDate", ParameterType.ntOracleVarchar, ToDate, ParameterDir.pdInput, 200);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);


                switch (DynamicFormType)
                {
                    case clGeneral.enDynamicFormType.Elements:
                        _Dal.ExecuteSP(clGeneral.cProGetHistoryOfElement, ref Dt);
                        break;
                    case clGeneral.enDynamicFormType.SpecialSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetHistoryOfSidur, ref Dt);
                        break;
                    case clGeneral.enDynamicFormType.SugSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetHistoryOfTypeSidur, ref Dt);
                        break;
                    case clGeneral.enDynamicFormType.Parameters:
                        _Dal.ExecuteSP(clGeneral.cProGetHistoryOfParameters, ref Dt);
                        break;
                    case clGeneral.enDynamicFormType.ComponentSidurim:
                        _Dal.ExecuteSP(clGeneral.cProGetHistoryOfComponentSidur, ref Dt);
                        break;
                    case clGeneral.enDynamicFormType.ComponentTypeSidur:
                        _Dal.ExecuteSP(clGeneral.cProGetHistoryOfComponentTypeSidur, ref Dt);
                        break;
                }
                return Dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
