using System.Collections.Generic;
using KdsLibrary;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using KdsLibrary.BL;
using KdsLibrary.UI;
using System.Drawing;
using System.Text;
using KdsDataImport;
using KdsBatch.HrWorkersChanges;
using KdsLibrary.DAL;
using KdsWorkFlow.Approvals;
using System.Threading;
using System.Web.UI;
using System.Globalization;




public partial class Modules_Requests_HarazatTaalichKlita : KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadTavlaot(); 
        }
    }

    private void LoadTavlaot()
    {
        DataTable dt;
        clBatch oUtils = new clBatch();
        try
        {
            dt = oUtils.GetTavlaotToRefresh();
            ddlTavlaot.DataValueField = "kod";
            ddlTavlaot.DataTextField = "name";
            ddlTavlaot.DataSource = dt;
            ddlTavlaot.DataBind();
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
        }
    }
    protected void btnKlitatShaonim_Click(object sender, EventArgs e)
    {
        try
        {
            ClKds oKDs = new ClKds();
            oKDs.TryKdsFile();
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
        }

    }
    protected void btnRianunMatzavOvdim_Click(object sender, EventArgs e)
    {
        wsBatch oWsBatch = new wsBatch();
         try
         {
             oWsBatch.RefreshMatzavOvdim();
             ScriptManager.RegisterStartupScript(ddlTavlaot, this.GetType(), "errName", "alert('תהליך הרענון עשוי להימשך זמן ארוך,כדי להתעדכן יש להסתכל במסך לוג תהליך');", true);
         }
         catch (Exception ex)
         {
             lblErrorPage.Text = ex.Message;
             throw ex;   
         }
         //oKDs.KdsWriteProcessLog(3, 1, 1, "start matzav","");
         //oDal.ClearCommand();
         //oDal.AddParameter("shem_mvew", KdsLibrary.DAL.ParameterType.ntOracleVarchar, "New_Matzav_Ovdim", KdsLibrary.DAL.ParameterDir.pdInput);
         //oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv");
         //oKDs.KdsWriteProcessLog(3, 1, 2, "end ok matzav", "");
    }
    protected void btnYeziratYomAvoda_Click(object sender, EventArgs e)
    {
        //clDal oDal = new clDal();
        ClKds oKDs = new ClKds();
        string taarich;
      //  DateTime taarichh = new DateTime();
        try
        {
            if (clnYomAvoda.Text != "")
            {
                lblYomAvoda.Visible = false;
                taarich = DateTime.Parse(clnYomAvoda.Text).ToString("yyyyMMdd");
                oKDs.RunRefreshRetroYamim(taarich);
                //oKDs.KdsWriteProcessLog(7, 1, 1, "start ins_yamey", "");
                //oDal.ClearCommand();
                //oDal.AddParameter("pDt", KdsLibrary.DAL.ParameterType.ntOracleVarchar, taarich, KdsLibrary.DAL.ParameterDir.pdInput);
                //oDal.ExecuteSP("PKG_BATCH.pro_ins_yamey_avoda_ovdim");
                //oKDs.KdsWriteProcessLog(7, 1, 2, "end ok ins_yamey", "");
            }
            else lblYomAvoda.Visible = true;
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
        }
    }
    protected void btnRianunTavlaotTnua_Click(object sender, EventArgs e)
    {
        ClKds oKDs = new ClKds();
        try
        {
            oKDs.refresh_from_shmulik();
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
        }
    }
    protected void btnRianunTavlaotHR_Click(object sender, EventArgs e)
    {
       // ClKds oKDs = new ClKds();
        clBatch obatch = new clBatch();
        clDal oDal = new clDal();
        int sub_tahalich=0;
        int seqNum=0;
        string nameTable="";
        string sProPivotName = "";
        try
        {
            sub_tahalich = int.Parse(ddlTavlaot.SelectedItem.Value);
            nameTable = ddlTavlaot.SelectedItem.Text;
            if (sub_tahalich == 8 || sub_tahalich == 5)
                TipulMeyuchadRefreshPirteyOvdim();
            else if (sub_tahalich != 28 && sub_tahalich != 29 && sub_tahalich != 30) //not pivot
            {
                seqNum = obatch.InsertProcessLog(3, sub_tahalich, RecordStatus.Wait, "start refresh " + nameTable, 0);
                //**oKDs.KdsWriteProcessLog(3, sub_tahalich, 1, "start refresh " + nameTable, "");
                oDal.ClearCommand();
                oDal.AddParameter("shem_mvew", KdsLibrary.DAL.ParameterType.ntOracleVarchar, nameTable, KdsLibrary.DAL.ParameterDir.pdInput);
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv");
                obatch.UpdateProcessLog(seqNum, RecordStatus.Finish, "end ok refresh " + nameTable, 0);
                //**oKDs.KdsWriteProcessLog(3, sub_tahalich, 2, "end ok refresh " + nameTable, "");
            }
            else
            {
                switch (sub_tahalich)
                {
                    //case 8: sProPivotName = "Create_Cursor_Pirtey_Ovdim";
                    //    break;
                    case 28: sProPivotName = "PKG_ELEMENTS.calling_Pivot_Meafyeney_e";
                        break;
                    case 29: sProPivotName = "PKG_SUG_SIDUR.calling_Pivot_Meafyeney_S";
                        break;
                    case 30: sProPivotName = "PKG_SIDURIM.calling_Pivot_Sidurim_M";
                        break;
                }
                seqNum = obatch.InsertProcessLog(3, sub_tahalich, RecordStatus.Wait, "start refresh " + nameTable, 0);
               //** oKDs.KdsWriteProcessLog(3, sub_tahalich, 1, "start refresh " + nameTable, "");
                oDal.ClearCommand();
                oDal.ExecuteSP(sProPivotName);
                obatch.UpdateProcessLog(seqNum, RecordStatus.Finish, "end ok refresh " + nameTable, 0);
               //**oKDs.KdsWriteProcessLog(3, sub_tahalich, 2, "end ok refresh " + nameTable, "");
            }
        }
        catch (Exception ex)
        {
            obatch.UpdateProcessLog(seqNum,RecordStatus.Faild, "Rfresh " + nameTable + " aborted " + ex.Message, 0);   
            //**oKDs.KdsWriteProcessLog(3, sub_tahalich, 3, "Rfresh " + nameTable + " aborted " + ex.Message, "");
            lblErrorPage.Text = ex.Message;
            throw ex;
            
        }
    }
    private void TipulMeyuchadRefreshPirteyOvdim()
    {
        wsBatch oWsBatch = new wsBatch();
        try
        {
            oWsBatch.RunRefreshPirteyOvdim();
            ScriptManager.RegisterStartupScript(ddlTavlaot, this.GetType(), "errName", "alert('תהליך הרענון עשוי להימשך זמן ארוך,כדי להתעדכן יש להסתכל במסך לוג תהליך');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnRianunMeafyeneiBizua_Click(object sender, EventArgs e)
    {
       // ClKds oKDs = new ClKds();
        wsBatch oWsBatch = new wsBatch();
        try
        {
            oWsBatch.RunRefreshMeafyenim();
            ScriptManager.RegisterStartupScript(ddlTavlaot, this.GetType(), "errName", "alert('תהליך הרענון עשוי להימשך זמן ארוך,כדי להתעדכן יש להסתכל במסך לוג תהליך');", true);
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
        }
    }
    protected void btnHasvaatHR_Click(object sender, EventArgs e)
    {
        try
        {
            clMain obClManager = new KdsBatch.HrWorkersChanges.clMain();
            obClManager.HafalatBatchShinuyimHR();
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;   
        }
    }
    protected void btnHashlamatNetunimVeShguimHR_Click(object sender, EventArgs e)
    {
      //  ClKds oKDs = new ClKds();
        wsBatch oWsBatch = new wsBatch();
        long lRequestNum;
        clBatch obatch = new clBatch();
        int seqNum=0;
        try
        {
            seqNum = obatch.InsertProcessLog(3, 37, KdsLibrary.BL.RecordStatus.Wait, "Chk_ThreadHrChainges", 0);
            lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12);
            oWsBatch.RunTahalichHrChanges(seqNum);
            ScriptManager.RegisterStartupScript(ddlTavlaot, this.GetType(), "errName", "alert('תהליך הסדרן עשוי להימשך זמן ארוך,כדי להתעדכן יש להסתכל במסך לוג תהליך');", true);
           // obatch.UpdateProcessLog(seqNum, KdsLibrary.BL.RecordStatus.Finish, "Chk_ThreadHrChainges", 0);
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
            
        }
    }
    protected void btnKlitatNetuneySadran_Click(object sender, EventArgs e)
    { 
         string taarich;
         wsBatch oWsBatch = new wsBatch();
         try
         {
             if (clnSadran.Text != "")
             {
                 lblSadran.Visible = false;
                 taarich = DateTime.Parse(clnSadran.Text).ToString("yyyyMMdd");// DateTime.Parse(clnSadran.Text).ToString("dd/MM/yyyy");
                 oWsBatch.RunTahalichSadran(taarich);
                 ScriptManager.RegisterStartupScript(ddlTavlaot, this.GetType(), "errName", "alert('תהליך הסדרן עשוי להימשך זמן ארוך,כדי להתעדכן יש להסתכל במסך לוג תהליך');", true);
                // oKDs.RunSdrn(taarich);
             }
             else lblSadran.Visible = true;
         }
         catch (Exception ex)
         {
             lblErrorPage.Text = ex.Message;
             throw ex;
          
         }
    }
    protected void btnHahlamatNetunimVeshguim_Click(object sender, EventArgs e)
    {
      //  ClKds oKDs = new ClKds();
        DateTime taarich = new DateTime();
        long lRequestNum;
        try
        {
            if (clnHaslama.Text != "")
            {
                lblHashlama.Visible = false;
                taarich = DateTime.Parse(clnHaslama.Text);
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12);
                KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcess, clGeneral.BatchExecutionType.All, taarich, lRequestNum);
            }
            else lblHashlama.Visible = true;// "חובה להכניס תאריך!";
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
        }
    }
    protected void btnIshurim_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime taarich = new DateTime();
            if (clnIshurim.Text != "")
            {
                lblIshurim.Visible = false;
                //lblIshurim.Text = "";
                taarich = DateTime.Parse(clnIshurim.Text);
                ApprovalFactory.ApprovalsEndOfDayProcess(taarich, false);
            }
            else lblIshurim.Visible = true; //lblIshurim.Text = "חובה להכניס תאריך!";
        }
        catch (Exception ex)
        {
            lblErrorPage.Text = ex.Message;
            throw ex;
        }

    }
}
