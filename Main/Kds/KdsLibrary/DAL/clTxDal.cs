using System;
using System.Configuration; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace KdsLibrary.DAL
{
  
    public class clTxDal
    {
        static string strConnectionString = (string)ConfigurationSettings.AppSettings["KDS_CONNECTION"];
        private OracleConnection TxConn = new OracleConnection(strConnectionString);
        private OracleCommand TxCmd = new OracleCommand();

        private const int _CommandTimeOut = 90;
        private bool TxExists = false;        
        private OracleTransaction TxTransaction;

        public clTxDal()
        {
         
        }
        public clTxDal(string sConnString)
        {
           TxConn = new OracleConnection(sConnString);           
        }

        private void Close()
        {            
            if (TxConn.State == ConnectionState.Open)
            {
                TxConn.Close();
            }
            TxTransaction.Dispose();
            TxCmd.Dispose();
            TxConn.Dispose();
        }

        public void TxBegin()
        {
            if (TxExists)
            {
                throw new Exception("Transaction Already Started");
            }
            try
            {                
                TxConn.Open();

                TxCmd = TxConn.CreateCommand();

                TxTransaction = TxConn.BeginTransaction();
                TxCmd.Connection = TxConn;               

                TxExists = true;
            }

            catch (Exception ex)
            {
                TxExists = false;
                Close();
                throw ex;
            }
        }


        public void TxCommit()
        {
            if (!TxExists)
            {
                throw new Exception("Transaction Not Started");
            }

            try
            {
                TxTransaction.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TxExists = false;
                Close();
            }
        }

        public void TxRollBack()
        {
            if (!TxExists)
            {
                throw new Exception("Transaction Not Started");
            }
            try
            {
                TxTransaction.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TxExists = false;
                Close();
            }
        }

        public void ClearCommand()
        {
            TxCmd.Parameters.Clear();
        } 

        public void ExecuteSP(string sSPName)
        {
            try
            {               
               CreateCommand(sSPName, CommandType.StoredProcedure);
               TxCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        public void ExecuteSP(string sSPName, ref DataTable dt)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
           
            try
            {
                CreateCommand(sSPName, CommandType.StoredProcedure);
                adapter.SelectCommand = TxCmd;
                adapter.Fill(dt);

                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }



        public void ExecuteSP(string sSPName, ref DataSet ds)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
          
            try
            {
                CreateCommand(sSPName, CommandType.StoredProcedure);
                adapter.SelectCommand = TxCmd;
                adapter.Fill(ds);

                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CreateCommand(string cmdText, CommandType cmdType)
        {
            TxCmd.CommandType = cmdType;
            TxCmd.CommandText = cmdText;
            TxCmd.CommandTimeout = _CommandTimeOut;
        }

        public void AddParameter(string paramName, ParameterType paramType, object paramVal,
                            ParameterDir paramDirection)
        {
            OracleParameter param = default(OracleParameter);
            try
            {
                if ((paramVal == null))
                {
                    paramVal = DBNull.Value;
                }
                else
                {

                    if ((paramVal.ToString().Equals("")))
                    {
                        paramVal = DBNull.Value;
                    }
                }

                param = new OracleParameter(paramName, (OracleDbType)paramType, paramVal, (ParameterDirection)paramDirection);
                TxCmd.Parameters.Add(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddParameter(string paramName, ParameterType paramType, object paramVal,
                               ParameterDir paramDirection, string paramUDTTypeName)
        {
            try
            {
                OracleParameter param = default(OracleParameter);


                param = new OracleParameter(paramName, (OracleDbType)paramType, paramVal, (ParameterDirection)paramDirection);
                param.UdtTypeName = paramUDTTypeName;
                TxCmd.Parameters.Add(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddParameter(string paramName, ParameterType paramType, object paramVal,
                                 ParameterDir paramDirection, int paramSize)
        {
            try
            {
                OracleParameter param = default(OracleParameter);
                if ((paramVal == null))
                {
                    paramVal = DBNull.Value;
                }
                else
                {

                    if ((paramVal.ToString().Equals("")))
                    {
                        paramVal = DBNull.Value;
                    }
                }

                param = new OracleParameter(paramName, (OracleDbType)paramType, paramSize, paramVal, (ParameterDirection)paramDirection);
                TxCmd.Parameters.Add(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetValParam(string ParamName)
        {
            return TxCmd.Parameters[ParamName].Value.ToString();
        }

        //public IDataParameter AddParameterOutPut(string paramName, ParameterType paramType)
        //{
        //    try
        //    {
        //        OracleParameter param = default(OracleParameter);
        //        param = new OracleParameter(paramName, paramType, ParameterDirection.Output);
        //        TxCmd.Parameters.Add(param);

        //        return param;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public void AddParameterReturn(string paramName, OracleDbType paramType, int paramSize)
        //{
        //    try
        //    {
        //        OracleParameter param = default(OracleParameter);
        //        param = new OracleParameter(paramName, paramType, paramSize, ParameterDirection.ReturnValue);
        //        TxCmd.Parameters.Add(param);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void Dispose()
        {
            Close();
        }
    }


    
}
