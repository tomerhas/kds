using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Threading;
using KdsBatch;
using System.Data;
using KdsLibrary;

delegate void CalcBatchDelegate(long lRequestNum, DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet);
delegate void TransferToHilanDelegate(long lRequestNum, long lRequestNumToTransfer);
delegate void InputDataAndErrorsDelegate(long lRequestNum);

/// <summary>
/// Summary description for wsBatch
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class wsBatch : System.Web.Services.WebService
{

    public wsBatch()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public string InputDataAndErrors(long lRequestNum)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.ExecuteInputDataAndErrors(
            (int)BatchRequestSource.ErrorExecutionFromUI,
            (int)BatchExecutionType.All,
            DateTime.Now, lRequestNum);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string CalcBatchParallel(long lRequestNum, string sAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
    {
        DateTime dAdChodesh;
        dAdChodesh = clGeneral.GetDateTimeFromStringMonthYear(1, sAdChodesh);
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.CalcBatchParallel(lRequestNum, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet);
        client.Close();
        return "OK";
    }
    
    [WebMethod(EnableSession = true)]
    public string CalcBatchPremiyot(long lRequestNum)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.CalcBatchPremiyot(lRequestNum);
        client.Close();
        return "OK";
    }
    [WebMethod(EnableSession = true)]
    public string CalcBatch(long lRequestNum, string sAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
    {
        DateTime dAdChodesh;
        dAdChodesh = clGeneral.GetDateTimeFromStringMonthYear(1, sAdChodesh);
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.CalcBatch(lRequestNum, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string TransferToHilan(long lRequestNum, long lRequestNumToTransfer)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
            client.TransferToHilan(lRequestNum, lRequestNumToTransfer);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string CreateReprts(long lRequestNum, string sMonth, int iUserId)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.CreateConstantsReports(lRequestNum, sMonth, iUserId);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string CreateHeavyReports(long lRequestNum)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.CreateHeavyReports(lRequestNum);
        client.Close();
        return "OK";
    }

    [WebMethod]
    public string OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType btchType, 
        int userId, string Description)
    {
        return KdsLibrary.clGeneral.OpenBatchRequest(btchType,
               String.Concat(Description, " ", btchType.ToString()), userId).ToString();
    }

    [WebMethod]
    public string RunPremiaRoutine(KdsLibrary.clGeneral.enGeneralBatchType btchType,
        long lRequestNum, string periodMonth, int userId, long processBtchNumber)
    {
        
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        string result = null;
        clGeneral.InsertBakashaParam(processBtchNumber, 1, lRequestNum.ToString());
        clGeneral.InsertBakashaParam(processBtchNumber, 2, periodMonth);
        try
        {
            DateTime period = GetPeriodFirstDate(periodMonth);
            switch (btchType)
            {
                case clGeneral.enGeneralBatchType.CreatePremiaExcelInput:
                    result = client.CreatePremiaInputFile(lRequestNum, period, userId, processBtchNumber);
                    break;
                case clGeneral.enGeneralBatchType.ExecutePremiaCalculationMacro:
                    result = client.RunPremiaCalculation(period, userId, processBtchNumber);
                    break;
                case clGeneral.enGeneralBatchType.StorePremiaCalculationOutput:
                    result = client.StorePremiaCalculationOutput(lRequestNum, period, userId,
                         processBtchNumber);
                    break;
                default:
                    result = "Invalid Request Type";
                    break;

            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        if (!String.IsNullOrEmpty(result))
            result += String.Concat(" מס' בקשה ", processBtchNumber);
        client.Close();
        return result;
    }

    private DateTime GetPeriodFirstDate(string periodMonth)
    {
        string[] args = periodMonth.Split('/');
        return new DateTime(int.Parse(args[1]), int.Parse(args[0]), 1);
    }

    [WebMethod(EnableSession = true)]
    public void RefreshMatzavOvdim()
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.RefreshMatzavOvdim();
        client.Close();
       
    }

    [WebMethod(EnableSession = true)]
    public void RunTahalichHrChanges(int iseq)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.TahalichHrChanges(iseq);
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public void RunTahalichSadran(string taarich )
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.TahalichSadran(taarich);
        client.Close();
     
    }
    
    [WebMethod(EnableSession = true)]
    public void RunRefreshMeafyenim()
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.RefreshMeafyenim();
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public void RunRefreshPirteyOvdim()
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.RefreshPirteyOvdim();
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public string YeziratRikuzim(long lRequestNum, long iRequestIdForRikuzim)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.YeziratRikuzim(lRequestNum, iRequestIdForRikuzim);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string RunShinuimVeShguim(long lRequestNum, DateTime dTaarich,int TypeShguim ,int ExecutionType)
    {
        KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
        client.TahalichHarazatShguimBatch(lRequestNum, dTaarich, TypeShguim, ExecutionType);
        client.Close();
        return "OK";
    }
}

