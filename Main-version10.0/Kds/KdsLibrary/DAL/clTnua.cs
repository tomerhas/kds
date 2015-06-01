using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace KdsLibrary.DAL
{
    
    public class clTnua 
    {
        //static string strConnectionString = (string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"];
        //private OracleConnection conn = new OracleConnection(strConnectionString);
        //private OracleCommand cmd = new OracleCommand();
        //static string strConnectionString;
        private OracleConnection conn;
        private OracleCommand cmd;

        public  clTnua(string sConnString)
        {           
           conn = new OracleConnection(sConnString);
           cmd = new OracleCommand();
        }

        private void Open()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Close()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateCommand(string cmdText, CommandType cmdType)
        {
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
        }

        public void ClearCommand()
        {
            cmd.Parameters.Clear();
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
                cmd.Parameters.Add(param);
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
                cmd.Parameters.Add(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public IDataParameter AddParameterOutPut(string paramName, OracleDbType paramType)
        //{
        //    try
        //    {
        //        OracleParameter param = default(OracleParameter);
        //        param = new OracleParameter(paramName, paramType, ParameterDirection.Output);
        //        cmd.Parameters.Add(param);

        //        return param;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public void AddParameterReturn(string paramName, ParameterType paramType, int paramSize)
        //{
        //    try
        //    {
        //        OracleParameter param = default(OracleParameter);
        //        param = new OracleParameter(paramName, paramType, paramSize, ParameterDirection.ReturnValue);
        //        cmd.Parameters.Add(param);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public OracleDataReader GetDataReader()
        {
            Open();
            cmd.Connection = conn;
            return cmd.ExecuteReader();
        }

        public void ExecuteSQL(string sSQL, ref DataTable dt)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
          
            try
            {
                Open();
                CreateCommand(sSQL, CommandType.Text);
                cmd.Connection = conn;
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public void ExecuteSQL(string sSQL)
        {
            //return;
            try
            {
                Open();
                CreateCommand(sSQL, CommandType.Text);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public void ExecuteSP(string sSPName, ref DataSet ds)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
           
            try
            {
                Open();
                CreateCommand(sSPName, CommandType.StoredProcedure);
                cmd.Connection = conn;
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);

                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public void ExecuteSP(string sSPName, ref DataTable dt)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
           
            try
            {
                Open();
                CreateCommand(sSPName, CommandType.StoredProcedure);
                cmd.Connection = conn;
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        public void ExecuteSP(string sSPName)
        {
            //return;
            try
            {
                Open();
                CreateCommand(sSPName, CommandType.StoredProcedure);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }


        public string ExecuteScalar(string ReturnParamName)
        {
            try
            {
                Open();
                cmd.Connection = conn;
                cmd.ExecuteScalar();
                return cmd.Parameters[ReturnParamName].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

        }

        public string GetValParam(string ParamName)
        {
            return cmd.Parameters[ParamName].Value.ToString();
        }

        private void Dispose()
        {
            cmd.Dispose();
        }
    }    
}

