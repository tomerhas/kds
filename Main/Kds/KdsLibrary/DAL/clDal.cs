using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
using System.Configuration; 
using Oracle.DataAccess; 
using Oracle.DataAccess.Client; 
using Oracle.DataAccess.Types;


namespace KdsLibrary.DAL
{
    public enum ParameterType
    {      
        ntOracleRefCursor = OracleDbType.RefCursor,
        ntOracleVarchar = OracleDbType.Varchar2,
        ntOracleChar = OracleDbType.Char,
        ntOracleLong = OracleDbType.Long,
        ntOracleInteger = OracleDbType.Int32,
        ntOracleDate = OracleDbType.Date,
        ntOracleInt64 = OracleDbType.Int64,
        ntOracleDecimal = OracleDbType.Decimal,    
        ntOracleObject = OracleDbType.Object,
        ntOracleArray = OracleDbType.Array
    }

    public enum ParameterDir
    {
        pdInput = ParameterDirection.Input,
        pdOutput = ParameterDirection.Output,
        pdInputOutput = ParameterDirection.InputOutput,
        pdReturnValue = ParameterDirection.ReturnValue
    }

    public class clDal
    {
        
        static  string strConnectionString =  (string) ConfigurationSettings.AppSettings["KDS_CONNECTION"];

    private OracleConnection conn;// = new OracleConnection(strConnectionString); 
    private OracleCommand cmd = new OracleCommand();
    private int _ArrayBindCount=0;
    
    private void Open() 
    {    
        try {
            if (conn == null || conn.State == ConnectionState.Closed)
            { conn = new OracleConnection(strConnectionString); }

            conn.Open(); 
        } 
        catch (Exception ex) {
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

    public void AddParameter(string paramName, ParameterType paramType, object paramVal,
                                ParameterDir paramDirection,  string paramUDTTypeName)
    {
        try
        { 
            OracleParameter param = default(OracleParameter);
            

            param = new OracleParameter(paramName, (OracleDbType)paramType, paramVal,  (ParameterDirection)paramDirection);
           // param.ArrayBindSize = new int[2];
            param.UdtTypeName = paramUDTTypeName;            
            cmd.Parameters.Add(param);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void TestRunSP(UDT.COLL_SIDURIM_OVDIM oCollSidurimOvdim)
    {

        try
        {
            //OracleParameter param = new OracleParameter("p_coll_sidurim_ovdim", OracleDbType.Object, oObjTest, ParameterDirection.Input);
            //param.UdtTypeName = "OBJ_TEST";
            OracleParameter param = new OracleParameter("p_coll_sidurim_ovdim", OracleDbType.Array, oCollSidurimOvdim, ParameterDirection.Input);
            param.UdtTypeName = "COLL_SIDURIM_OVDIM";

            cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = conn;
            cmd.CommandText = "pkg_errors.pro_upd_sidurim_ovdim";
            Open();


            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            string test = ex.Message;
        }
        finally 
        {
            Close();
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
        try
        {
         Open();
         CreateCommand(sSQL,CommandType.Text);
         cmd.Connection = conn;
         cmd.ArrayBindCount = _ArrayBindCount;
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

    public int ArrayBindCount
    {
        get { return _ArrayBindCount; }
        set { _ArrayBindCount = value; }
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
        try
        {
         Open();
         CreateCommand(sSPName,CommandType.StoredProcedure);
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

     //public void ExecuteSPBatch(long lBakashaId, int iMisparIshi,string sSPName)
     //{
     //    try
     //    {
     //       Open();
                                          
     //        CreateCommand(sSPName, CommandType.StoredProcedure);
     //        clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "I", 0, null, "after CreateCommand");
                                          
     //        cmd.Connection = conn;
     //        cmd.ExecuteNonQuery();
     //        clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "I", 0, null, "after ExecuteNonQuery");
                                          
     //    }
     //    catch (Exception ex)
     //    {
     //        throw ex;
     //    }
     //    finally
     //    {
     //        Close();
     //    }
     //}


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

    public object GetObjectParam(string ParamName)
    {
        return cmd.Parameters[ParamName].Value;
    }
    public void InsertXML(string sXML, string sTableName, string[] ucols)
    {
        try
        {
            Open();
            cmd.Connection = conn;
            cmd.XmlCommandType = OracleXmlCommandType.Insert;
            cmd.CommandText = sXML;
            cmd.XmlSaveProperties.Table = sTableName;
            cmd.XmlSaveProperties.UpdateColumnsList = ucols;
            // Insert rows
            int rows = cmd.ExecuteNonQuery();
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
    private void Dispose()
    {
        cmd.Dispose();
    }


    public void ExecuteSP(string sSPName, ref DataSet ds,string TablesNames)
    {
        OracleDataAdapter adapter = new OracleDataAdapter();
        string OldName="";
        string[] TablesNamesSplit;
        try
        {
            Open();
            CreateCommand(sSPName, CommandType.StoredProcedure);
            cmd.Connection = conn;
            adapter.SelectCommand = cmd;
            TablesNamesSplit = TablesNames.Split(',');
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                if (cmd.Parameters[i].OracleDbType  == Oracle.DataAccess.Client.OracleDbType.RefCursor)
                {
                    OldName="Table";
                    if (i>0) OldName += i;

                    adapter.TableMappings.Add(OldName, TablesNamesSplit[i]);
                }
            }
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
    
   }

}
