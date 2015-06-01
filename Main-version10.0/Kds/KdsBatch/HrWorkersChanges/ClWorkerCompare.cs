using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.BL;

namespace KdsBatch.HrWorkersChanges
{
    public abstract class ClWorkerCompare
    {
        private List<ClWorkerTable> _WorkerTable;
        protected ClPeriodOfID _PeriodOfID;
        protected TableType _Type;
        protected DataTable _Dt;
        protected abstract DataTable GetData();
        protected List<ClPeriodOfID> ListPeriod;
        private DateTime taarichHatchalatMaarechet = new DateTime(2009, 5, 1, 0, 0, 0);

        public ClWorkerCompare(TableType State,ref bool flag) 
        {
            try
            {
                _WorkerTable = new List<ClWorkerTable>();
                ListPeriod = new List<ClPeriodOfID>();
                _Type = State;
                FillData();
                if (State != TableType.Defaults)
                    PrepareListOfPeriod();
                else
                    PrepareListOfPeriodDefaults();
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
        }

        private void FillData()
        {
            try
            {
                GetData();
                foreach (DataRow dr in _Dt.Rows)
                {
                    _WorkerTable.Add(new ClWorkerTable(int.Parse(dr["mispar_ishi"].ToString()),
                                                       DateTime.Parse(dr["taarich_hatchala"].ToString()),
                                                       DateTime.Parse(dr["date_a"].ToString()),
                                                       dr["date_b"].ToString() != "" ? DateTime.Parse(dr["date_b"].ToString()) : DateTime.MinValue,
                                                       dr["erech_a"].ToString() != "" ? dr["erech_a"].ToString() : null,
                                                       dr["erech_b"].ToString() != "" ? dr["erech_b"].ToString() : null,
                                                        int.Parse(dr["kod"].ToString())));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ClPeriodOfID CompareDate(ClWorkerTable CurrentWorkerTable)
        {
            ClPeriodOfID Period;
            DateTime _FromDate , _ToDate ;
            try
            {
                _FromDate = CurrentWorkerTable.ToDateA < CurrentWorkerTable.ToDateB ?
                                                    CurrentWorkerTable.ToDateA.AddDays(1) :
                                                    CurrentWorkerTable.ToDateB.AddDays(1);
                _ToDate = CurrentWorkerTable.ToDateA > CurrentWorkerTable.ToDateB ?
                    CurrentWorkerTable.ToDateA : CurrentWorkerTable.ToDateB;
             //   _FromDate = _FromDate < taarichHatchalatMaarechet ? taarichHatchalatMaarechet : _FromDate;
              
                Period = new ClPeriodOfID(CurrentWorkerTable.IdNumber, _FromDate, _ToDate, CurrentWorkerTable.Code);
                //if (_ToDate < taarichHatchalatMaarechet)
                //    return null;
                return Period;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ClPeriodOfID CompareDateAndValue(ClWorkerTable CurrentWorkerTable)
        {
            ClPeriodOfID Period;
            DateTime _FromDate, _ToDate;
            try
            {
                _FromDate = CurrentWorkerTable.FromDate;
                _ToDate = CurrentWorkerTable.ToDateA > CurrentWorkerTable.ToDateB ?
                    CurrentWorkerTable.ToDateA : CurrentWorkerTable.ToDateB;
               // _FromDate = _FromDate < taarichHatchalatMaarechet ? taarichHatchalatMaarechet : _FromDate;

                Period = new ClPeriodOfID(CurrentWorkerTable.IdNumber, _FromDate, _ToDate, CurrentWorkerTable.Code);
                //if (_ToDate < taarichHatchalatMaarechet)
                //    return null;
                return Period;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ClPeriodOfID CompareValue(ClWorkerTable CurrentWorkerTable)
        {
            ClPeriodOfID Period;
            DateTime _FromDate, _ToDate;
            try
            {
                _FromDate = CurrentWorkerTable.FromDate;
                _ToDate = CurrentWorkerTable.ToDateA;
                //_FromDate = _FromDate < taarichHatchalatMaarechet ? taarichHatchalatMaarechet : _FromDate;

                Period = new ClPeriodOfID(CurrentWorkerTable.IdNumber, _FromDate, _ToDate, CurrentWorkerTable.Code);
                //if (_ToDate < taarichHatchalatMaarechet)
                //    return null;
                return Period;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ClPeriodOfID PrepareTkufaMelea(ClWorkerTable CurrentWorkerTable)
        {
            ClPeriodOfID Period;
            DateTime _FromDate, _ToDate;
            try
            {
                _FromDate = CurrentWorkerTable.FromDate;
                _ToDate = CurrentWorkerTable.ToDateA;

                //_FromDate = _FromDate < taarichHatchalatMaarechet ? taarichHatchalatMaarechet : _FromDate;
                Period = new ClPeriodOfID(CurrentWorkerTable.IdNumber, _FromDate, _ToDate, CurrentWorkerTable.Code);
                //if (_ToDate < taarichHatchalatMaarechet)
                //    return null;
                return Period;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ClPeriodOfID CompareWorkerTable(ClWorkerTable CurrentWorkerTable)
        {
            ClPeriodOfID Period = null;
            try
            {
                if (CurrentWorkerTable.ToDateB == DateTime.MinValue &&
                    CurrentWorkerTable.ValueB == null)
                    Period = PrepareTkufaMelea(CurrentWorkerTable);
                else if ((CurrentWorkerTable.ToDateA != CurrentWorkerTable.ToDateB)
                    && (CurrentWorkerTable.ValueA != CurrentWorkerTable.ValueB))
                    Period = CompareDateAndValue(CurrentWorkerTable);
                else if ((CurrentWorkerTable.ToDateA != CurrentWorkerTable.ToDateB)
                    && (CurrentWorkerTable.ValueA == CurrentWorkerTable.ValueB))
                    Period = CompareDate(CurrentWorkerTable);
                else if ((CurrentWorkerTable.ToDateA == CurrentWorkerTable.ToDateB)
                    && (CurrentWorkerTable.ValueA != CurrentWorkerTable.ValueB))
                    Period = CompareValue(CurrentWorkerTable);

                if (Period != null)
                {
                    if (Period.ToDate < taarichHatchalatMaarechet)
                        Period = null;
                    else if (Period.FromDate < taarichHatchalatMaarechet)
                    {
                        Period.FromDate = taarichHatchalatMaarechet;
                        Period.CountOfDay = Period.ToDate.Subtract(Period.FromDate).Days + 1;
                    }
                }

                return Period;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrepareListOfPeriod()
        {
            ClPeriodOfID oPeriod;
            ClPeriodOfID oPeriod1;
            ClWorkerTable oWorker;
            DateTime FromDate = new DateTime();
            try
            {

                _WorkerTable.ForEach(delegate(ClWorkerTable WorkerTable)
                {
                    oWorker = _WorkerTable.Find(Worker => (Worker.IdNumber == WorkerTable.IdNumber &&
                                                           Worker.ToDateA == WorkerTable.ToDateA &&
                                                           Worker.ToDateB == WorkerTable.ToDateB &&
                                                           Worker.ValueA == WorkerTable.ValueA &&
                                                           Worker.ValueB == WorkerTable.ValueB &&
                                                           Worker.FromDate != WorkerTable.FromDate));
                    if (oWorker != null)
                    {

                        oPeriod = new ClPeriodOfID(oWorker.IdNumber,
                            oWorker.FromDate < WorkerTable.FromDate ? oWorker.FromDate : WorkerTable.FromDate,
                            oWorker.FromDate > WorkerTable.FromDate ? oWorker.FromDate : WorkerTable.FromDate, 0);

                        if (oPeriod.ToDate < taarichHatchalatMaarechet)
                            oPeriod = null;
                        else if (oPeriod.FromDate < taarichHatchalatMaarechet)
                             {
                                oPeriod.FromDate = taarichHatchalatMaarechet;
                                oPeriod.CountOfDay = oPeriod.ToDate.Subtract(oPeriod.FromDate).Days+1;
                             }
                    }
                    else
                        oPeriod = CompareWorkerTable(WorkerTable);
                    if (oPeriod != null)
                    {
                        oPeriod1 = ListPeriod.Find(Period => (Period.IdNumber == oPeriod.IdNumber && Period.FromDate == oPeriod.FromDate && Period.ToDate == oPeriod.ToDate));
                        if (oPeriod1 == null)
                            ListPeriod.Add(oPeriod);
                    }
                });
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void PrepareListOfPeriodDefaults()
        {
            ClPeriodOfID oPeriod;
            ClPeriodOfID oPeriod1;
            try
            {

                _WorkerTable.ForEach(delegate(ClWorkerTable WorkerTable)
                {
                   // ListPeriod.Add(CompareWorkerTable(WorkerTable));

                    oPeriod = CompareWorkerTable(WorkerTable);
                    if (oPeriod != null)
                    {
                        oPeriod1 = ListPeriod.Find(Period => (Period.Code == oPeriod.Code && Period.FromDate == oPeriod.FromDate && Period.ToDate == oPeriod.ToDate));
                        if (oPeriod1 == null)
                            ListPeriod.Add(oPeriod);
                    }
                });
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
