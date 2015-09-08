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
using KDSCommon.Enums;
using KDSCommon.Proxies;

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
        BatchProxy client = new BatchProxy();
        client.ExecuteInputDataAndErrors(
            (int)clGeneral.BatchRequestSource.ErrorExecutionFromUI,
            (int)BatchExecutionType.All,
            DateTime.Now, lRequestNum);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string InsetRecordsToHistory(long lRequestNum)
    {
        BatchProxy client = new BatchProxy();
        client.InsetRecordsToHistory(lRequestNum);
        client.Close();
        return "OK";
    }
    [WebMethod(EnableSession = true)]
    public string CalcBatchParallel(long lRequestNum, string sAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
    {
        DateTime dAdChodesh;
        dAdChodesh = clGeneral.GetDateTimeFromStringMonthYear(1, sAdChodesh);
        BatchProxy client = new BatchProxy();
        client.CalcBatchParallel(lRequestNum, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string CalcBatchPremiyot(long lRequestNum)
    {
        BatchProxy client = new BatchProxy();
        client.CalcBatchPremiyot(lRequestNum);
        client.Close();
        return "OK";
    }
    //[WebMethod(EnableSession = true)]
    //public string CalcBatch(long lRequestNum, string sAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
    //{
    //    DateTime dAdChodesh;
    //    dAdChodesh = clGeneral.GetDateTimeFromStringMonthYear(1, sAdChodesh);
    //    KdsWebApplication.BatchProxy client = new KdsWebApplication.BatchProxy();
    //    client.CalcBatch(lRequestNum, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet);
    //    client.Close();
    //    return "OK";
    //}

    [WebMethod(EnableSession = true)]
    public string TransferToHilan(long lRequestNum, long lRequestNumToTransfer)
    {
        BatchProxy client = new BatchProxy();
        client.TransferToHilan(lRequestNum, lRequestNumToTransfer);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string CreateReprts(long lRequestNum, string sMonth, int iUserId)
    {
        BatchProxy client = new BatchProxy();
        client.CreateConstantsReports(lRequestNum, sMonth, iUserId);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string CreateHeavyReports(long lRequestNum)
    {
        BatchProxy client = new BatchProxy();
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

    //[WebMethod]
    //public string RunPremiaRoutine(KdsLibrary.clGeneral.enGeneralBatchType btchType,
    //    long lRequestNum, string periodMonth, int userId, long processBtchNumber)
    //{

    //    KdsLocalServiceProxy.BatchProxy client = new KdsLocalServiceProxy.BatchProxy();
    //    string result = null;
    //    clGeneral.InsertBakashaParam(processBtchNumber, 1, lRequestNum.ToString());
    //    clGeneral.InsertBakashaParam(processBtchNumber, 2, periodMonth);
    //    try
    //    {
    //        DateTime period = GetPeriodFirstDate(periodMonth);
    //        switch (btchType)
    //        {
    //            case clGeneral.enGeneralBatchType.CreatePremiaExcelInput:
    //                result = client.CreatePremiaInputFile(lRequestNum, period, userId, processBtchNumber);
    //                break;
    //            case clGeneral.enGeneralBatchType.ExecutePremiaCalculationMacro:
    //                result = client.RunPremiaCalculation(period, userId, processBtchNumber);
    //                break;
    //            case clGeneral.enGeneralBatchType.StorePremiaCalculationOutput:
    //                result = client.StorePremiaCalculationOutput(lRequestNum, period, userId,
    //                     processBtchNumber);
    //                break;
    //            default:
    //                result = "Invalid Request Type";
    //                break;

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result = ex.Message;
    //    }
    //    if (!String.IsNullOrEmpty(result))
    //        result += String.Concat(" מס' בקשה ", processBtchNumber);
    //    client.Close();
    //    return result;
    //}

    private DateTime GetPeriodFirstDate(string periodMonth)
    {
        string[] args = periodMonth.Split('/');
        return new DateTime(int.Parse(args[1]), int.Parse(args[0]), 1);
    }

    [WebMethod(EnableSession = true)]
    public void RefreshMatzavOvdim()
    {
        BatchProxy client = new BatchProxy();
        client.RefreshMatzavOvdim();
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public void RunTahalichHrChanges(int iseq)
    {
        BatchProxy client = new BatchProxy();
        client.TahalichHrChanges(iseq);
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public void RunTahalichSadran(string taarich)
    {
        BatchProxy client = new BatchProxy();
        client.TahalichSadran(taarich);
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public void RunRefreshMeafyenim()
    {
        BatchProxy client = new BatchProxy();
        client.RefreshMeafyenim();
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public void RunRefreshPirteyOvdim()
    {
        BatchProxy client = new BatchProxy();
        client.RefreshPirteyOvdim();
        client.Close();

    }

    [WebMethod(EnableSession = true)]
    public string BdikatChufshaRezifa(long lRequestNum, int iUserId)
    {
        BatchProxy client = new BatchProxy();
        client.BdikatChufshaRezifa(lRequestNum, iUserId);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string BdikatYemeyMachala(long lRequestNum, int iUserId)
    {
        BatchProxy client = new BatchProxy();
        client.BdikatYemeyMachala(lRequestNum, iUserId);
        client.Close();
        return "OK";
    }


    [WebMethod(EnableSession = true)]
    public string YeziratRikuzim(long lRequestNum, long iRequestIdForRikuzim)
    {
        BatchProxy client = new BatchProxy();
        client.YeziratRikuzim(lRequestNum, iRequestIdForRikuzim);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string TransferTekenNehagim(long lRequestNum, long iRequestIdForTransfer)
    {
        BatchProxy client = new BatchProxy();
        client.TransferTekenNehagim(lRequestNum, iRequestIdForTransfer);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string ShlichatRikuzimMail(long lRequestNum, long iRequestIdForRikuzim)
    {
        BatchProxy client = new BatchProxy();
        client.ShlichatRikuzimMail(lRequestNum, iRequestIdForRikuzim);
        client.Close();
        return "OK";
    }

    [WebMethod(EnableSession = true)]
    public string RunShinuimVeShguim(long lRequestNum, DateTime dTaarich, int TypeShguim, int ExecutionType)
    {
        BatchProxy client = new BatchProxy();
        //client.TahalichHarazatShguimBatch(lRequestNum, dTaarich, TypeShguim, ExecutionType);
        client.Close();
        return "OK";
    }


    [WebMethod(EnableSession = true)]
    public string RunTkinutMakatim(DateTime dTaarich)
    {
        BatchProxy client = new BatchProxy();
        client.TkinutMakatimBatch(dTaarich);
        client.Close();
        return "OK";
    }
}

