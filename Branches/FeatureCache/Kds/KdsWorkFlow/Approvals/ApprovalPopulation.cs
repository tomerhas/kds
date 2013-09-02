using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;

namespace KdsWorkFlow.Approvals
{
    /// <summary>
    /// Abstract class the represents workcard population for
    /// Approval batch process
    /// </summary>
    public abstract class ApprovalPopulation
    {
        #region Fields
        protected DateTime _workDate;
        protected clDal _dal;
        protected long _btchRequest;
        #endregion

        #region Constractor
        public ApprovalPopulation(DateTime workDate, long btchRequest)
        {
            _workDate = workDate;
            _btchRequest = btchRequest;
            _dal = new clDal();

        } 
        #endregion

        #region Methods
        public DataTable GetPopulation()
        {
            try
            {
                return GetData();
            }
            catch (Exception ex)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, ex.ToString());
                return null;
            }
        }

        protected abstract DataTable GetData();

        public virtual ApprovalFactoryArgs GetApprovalFactoryArgs()
        {
            ApprovalFactoryArgs args = new ApprovalFactoryArgs();
            args.CheckGarageManagerConfirmation = IsOnlyForGarageApprovals;
            args.GenerateAutoApproval = GenerateAutoApproval;
            return args;
        }

        public static ApprovalPopulation GetPopulationGroup(Type populationType,
            DateTime workDate, long btchRequest)
        {
            var appPopulation =
                   Activator.CreateInstance(populationType, workDate, btchRequest)
                        as ApprovalPopulation;
            return appPopulation;
        }

        
        #endregion

        #region Poperties
        public DateTime WorkDate
        {
            get { return _workDate; }
        }

        public virtual bool IsOnlyForGarageApprovals
        {
            get { return false; }
        }

        public virtual bool GenerateAutoApproval
        {
            get { return false; }
        }
        #endregion
    }

    /// <summary>
    /// Ovdei minhal ve meshek:
    /// no meadken_aharon=-12 for workdate
    /// </summary>
    public class GeneralPopulation : ApprovalPopulation
    {
        public GeneralPopulation(DateTime workDate, long btchRequest)
            : base(workDate, btchRequest)
        {
        }

        protected override DataTable GetData()
        {
           
            DataTable dt = new DataTable();
            _dal.AddParameter("p_taarich", ParameterType.ntOracleDate, _workDate,
                ParameterDir.pdInput);
            _dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
               ParameterDir.pdOutput);
            _dal.ExecuteSP("PKG_APPROVALS.get_general_population", ref dt);
            return dt;
       
        }
    }

    /// <summary>
    /// Garage employees without driving sidurim:
    /// no meadken_aharon=-12 for workdate
    /// </summary>
    public class GarageWithoutDrivingPopulation : ApprovalPopulation
    {

        public GarageWithoutDrivingPopulation(DateTime workDate, long btchRequest)
            : base(workDate, btchRequest)
        {
        }

        protected override DataTable GetData()
        {
           
            DataTable dt = new DataTable();
            _dal.AddParameter("p_taarich", ParameterType.ntOracleDate, _workDate,
                ParameterDir.pdInput);
            _dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
               ParameterDir.pdOutput);
            _dal.ExecuteSP("PKG_APPROVALS.get_mu_lelo_nahagut_population", ref dt);
            return dt;
       
        }

        public override bool GenerateAutoApproval
        {
            get
            {
                return true;
            }
        }
    }
    /// <summary>
    /// Retroactiv Ovdei minhal ve meshek:
    /// measher_o_mistayeg is not null where taarich_idkun_acharon after
    /// previous batch execution
    /// </summary>
    public class RetroPopulation : ApprovalPopulation
    {
        
        
        public RetroPopulation(DateTime workDate, long btchRequest)
            : base(workDate, btchRequest)
        {
           
        }

        protected override DataTable GetData()
        {
            DataTable dt = new DataTable();
            _dal.AddParameter("p_bakasha", ParameterType.ntOracleInt64 , _btchRequest,
                ParameterDir.pdInput);
            _dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
               ParameterDir.pdOutput);
            _dal.ExecuteSP("PKG_APPROVALS.get_retro_population", ref dt);
            return dt;
        }
    }

    /// <summary>
    /// Garage employees that has driving sidurim:
    /// sug_yechida='m_me' or sug_yechida='m_ms'
    /// and meadken_aharon=-12 for workdate
    /// </summary>
    public class GaragePopulation : ApprovalPopulation
    {
        public GaragePopulation(DateTime workDate, long btchRequest)
            : base(workDate, btchRequest)
        {
        }

        protected override DataTable GetData()
        {
            DataTable dt = new DataTable();
            _dal.AddParameter("p_taarich", ParameterType.ntOracleDate, _workDate,
                ParameterDir.pdInput);
            _dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
               ParameterDir.pdOutput);
            _dal.ExecuteSP("PKG_APPROVALS.get_musach_population", ref dt);
            return dt;
        }

        public override bool IsOnlyForGarageApprovals
        {
            get
            {
                return true;
            }
        }

        public override bool GenerateAutoApproval
        {
            get
            {
                return true;
            }
        }
    }
}
