using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DalOraInfra.DAL;

namespace KdsWorkFlow.Approvals
{
    /// <summary>
    /// Represents Approval Details
    /// </summary>
    public class Approval
    {
        #region Fields
        private int _code;
        private int _level;
        private string _description;
        private int _jobCode;
        private ApprovalTypes _approvalType;
        private int _suspendsPayment;
        private bool _exists;
        internal const int FIRST_APPROVAL_LEVEL = 1;
        private AccessTypeToHR _hrType;
        private bool _hasGarageManagerConfirmation;
        private bool _useSimpleAccessIfNoChanges;
        private SubCompanyApprovalType _subCompanyType;
        internal const int SUB_COMPANY_CODE_ADDITION = 11;
        private bool _active;
        #endregion

        #region Constractors
        public Approval()
        {
        }

        public Approval(int approvalCode)
            : this(approvalCode, FIRST_APPROVAL_LEVEL)
        {
        }

        public Approval(int approvalCode, int level)
        {
            _code = approvalCode;
            _level = level;
            Init();
        }

        public Approval(int approvalCode, int level, int jobCode)
        {
            _code = approvalCode;
            _level = level;
            _jobCode = jobCode;
            Init();
        }

        #endregion

        #region Properties
        public int Code
        {
            get { return _code; }
            set { _code = value; _exists = true; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int JobCode
        {
            get { return _jobCode; }
            set { _jobCode = value; }
        }

        public ApprovalTypes ApprovalType
        {
            get { return _approvalType; }
            set { _approvalType = value; }
        }
        
        public int SuspendsPayment
        {
            get { return _suspendsPayment; }
            set { _suspendsPayment = value; }
        }
        
        public bool IsExist
        {
            get { return _exists; }
        }

        public AccessTypeToHR AccessTypeToHR
        {
            get { return _hrType; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public bool RequiresPaymentHoursUpdate
        {
            get 
            { 
                return _code == 1 || _code == 2 ||
                    _code == 3 || _code == 4 || _code == 36; 
            }
        }

        public bool HasGarageManagerConfirmation
        {
            get { return _hasGarageManagerConfirmation; }
        }

        public bool RequiresExternalProcessOnFinish
        {
            get { return _code == 34 || _code == 44 || _code == 45; }
        }

        public int[] ComponentCodes
        {
            get 
            {
                return _code == 34 ? new int[] { 147, 253, 143 } :
                        new int[] { 161, 255, 254 };
            }
        }

        public int[] CharacteristicCodesForUpdate
        {
            get 
            {
                return _code == 34 ? new int[] { 14, 13 } :
                                      new int[] { 16, 17 };
            }
        }

        public bool IsForwardToAnotherFactor
        {
            get { return GetRealLevel() != _level; }
        }

        public bool UseSimpleAccessIfNoChanges
        {
            get { return _useSimpleAccessIfNoChanges; }
        }

        public bool NeedsDeviationUpdating
        {
            get { return _code == 2 || _code == 4; }
        }

        public bool IsForSubCompany
        {
            get { return _subCompanyType != SubCompanyApprovalType.NotForSubCompany; }
        }

        public SubCompanyApprovalType SubCompanyApprovalType
        {
            get { return _subCompanyType; }
        }

        public bool IsActiveForInsert
        {
            get { return _active || _level > FIRST_APPROVAL_LEVEL; }
        }

        #endregion

        #region Methods
        internal void Init()
        {
            DataTable dt = GetApprovalDetailsFromDB();
            if (dt != null && dt.Rows.Count > 0)
            {
                _description = dt.Rows[0]["teur_ishur"].ToString();
                if (_jobCode == 0)
                    int.TryParse(dt.Rows[0]["kod_tafkid_measher"].ToString(), out _jobCode);
                int apprType;
                if (int.TryParse(dt.Rows[0]["kod_sug_ishur"].ToString(),
                    out apprType))
                    _approvalType = (ApprovalTypes)apprType;
                int.TryParse(dt.Rows[0]["meakev_tashlum"].ToString(), out _suspendsPayment);
                _hrType = AccessTypeToHR.Simple;
                int iHr = 0;
                if (int.TryParse(dt.Rows[0]["sug_peilut"].ToString(), out iHr))
                    _hrType = (AccessTypeToHR)iHr;
                _hasGarageManagerConfirmation = dt.Rows[0]["ishur_menahel_musach"] != null &&
                    dt.Rows[0]["ishur_menahel_musach"].ToString().Equals("1");
                _useSimpleAccessIfNoChanges = dt.Columns.Contains("sug_peilut_advanced") &&
                    dt.Rows[0]["sug_peilut_advanced"] != null && 
                    dt.Rows[0]["sug_peilut_advanced"].ToString().Equals("1");
                _subCompanyType = SubCompanyApprovalType.NotForSubCompany;

                if (dt.Rows[0]["EGGED_TAAVORA"] != null && dt.Rows[0]["EGGED_TAAVORA"] != DBNull.Value)
                {
                    int subComp = 0;
                    if (int.TryParse(dt.Rows[0]["EGGED_TAAVORA"].ToString(), out subComp))
                        _subCompanyType = (SubCompanyApprovalType)subComp;
                }
                if (dt.Rows[0]["pail"] != null && dt.Rows[0]["pail"] != DBNull.Value)
                    _active = dt.Rows[0]["pail"].ToString().Equals("1");
                
                _exists = true;
            }
        }

        private DataTable GetApprovalDetailsFromDB()
        {
            int realLevel = GetRealLevel();
            
            clDal dal = new clDal();
            DataTable dt = new DataTable();
            dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger, _code,
                ParameterDir.pdInput);
            dal.AddParameter("p_rama", ParameterType.ntOracleInteger, realLevel,
               ParameterDir.pdInput);
            dal.AddParameter("p_kod_tafkid", ParameterType.ntOracleInteger, _jobCode,
               ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, 
                ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_approval_details", ref dt);
            return dt;
        }

        public bool IsNextLevelExists()
        {
            clDal dal = new clDal();
            dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger, _code,
                ParameterDir.pdInput);
            dal.AddParameter("p_max_rama", ParameterType.ntOracleInteger, null,
               ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.check_approval_max_rama");
            int realLevel = GetRealLevel();
            int nextLevel = realLevel;
            return (int.TryParse(dal.GetValParam("p_max_rama"), out nextLevel) &&
                nextLevel > realLevel);
        }

        public bool IsEqual(Approval appr)
        {
            if (appr == null) return false;
            else return _code == appr.Code;
        }

        public int GetRealLevel()
        {
            int realLevel = _level;
            while (realLevel % 10 == 0)
            {
                realLevel = realLevel / 10;
            }
            return realLevel;
        }

        internal int CalculateSubCompanyCode()
        {
            int exp = 1;
            for (int i = 0; i < SUB_COMPANY_CODE_ADDITION.ToString().Length; ++i)
                exp *= 10;
            return _code * exp + SUB_COMPANY_CODE_ADDITION;
        }
        #endregion


        
    }

    /// <summary>
    /// Types of Approval
    /// </summary>
    public enum ApprovalTypes
    {
        OrganizationTreeAccess = 1,
        WithoutOrganizationTreeAccess = 2,
        OrganizationTreeAccessAccordingToUnit = 3,
        ManuallyUserAssigned = 4
    }

    /// <summary>
    /// Types of access to HR to get the number of approval factor
    /// Simple with View
    /// others with Stored Procedure
    /// </summary>
    public enum AccessTypeToHR
    {
        Simple=0,
        ByNumberOrRegion=2,
        ByNumberOrBranch=3,
        ByNumberOrBranchExt = 4,
        WithoutParameters=6,
        OrganizationUnit=7
    }

    public enum SubCompanyApprovalType
    {
        NotForSubCompany=0,
        BelongsOrBorrowedToSubCompany=1,
        ReinforceSubCompany=2
    }
}
