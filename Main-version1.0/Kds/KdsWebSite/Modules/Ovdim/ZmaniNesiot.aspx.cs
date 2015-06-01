using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KdsLibrary.BL;
using KdsBatch;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Security;
using KDSCommon.UDT;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Managers;


public partial class Modules_Ovdim_ZmaniNesiot : KdsLibrary.UI.KdsPage
{ 
    private int iMisparIshi;
    private DateTime dTarich;

     private int iMeafienIn;
     private int iKodMeafienIn;
     private int iZmanMeafienIn;
     private int iZmanMeafien;


     private int  izmaNesiaHaloch ;
     private int  iZmanNesiaHazor ;

     private int iMerkazErua;
     private int iMikumYaad;
     private int iDakot;

    protected void Page_Load(object sender, EventArgs e)

    {
        string sDate;
   
        //ID – מספר אישי
        //Date – תאריך 
        //Type – יכיל 51 או 61
        //Value – ערך של מאפיין 51/61
       
       iMisparIshi = Int32.Parse(Request.QueryString["id".ToLower()]);
       iMeafienIn = Int32.Parse(Request.QueryString["type".ToLower()]);
       sDate = Request.QueryString["date".ToLower()].ToString();
       dTarich = new DateTime(int.Parse(sDate.Substring(6,4)), int.Parse(sDate.Substring(3,2)), int.Parse(sDate.Substring(0,2)));
       iKodMeafienIn = (Int32.Parse(Request.QueryString["Value".ToLower()]))/1000;
       iZmanMeafienIn = (Int32.Parse(Request.QueryString["Value".ToLower()])) - (iKodMeafienIn * 1000);


        //        iMeafienIn = 51;
        //iKodMeafienIn =2;
        //iZmanMeafienIn = 45;

        //iMisparIshi = 16525;
        //dTarich = new DateTime(2009, 06, 04);



        int iDakot = 0;

        txtMisparIshi.Value = iMisparIshi.ToString();
        txtTaarich.Value = dTarich.ToString();

            try
        {
         
            if (!Page.IsPostBack)
            {
             
                if (iMeafienIn.ToString() == "51")
                {
                    switch (iKodMeafienIn)
                    {
                        case 1:
                            iZmanMeafien = iZmanMeafienIn;
                            
                            txtZNLavodaMax.Text = iZmanMeafienIn.ToString() ;
                            txtZNMavodaMax.Text = "לא זכאי ";
                            break; 
                        case 2:
                            iZmanMeafien = iZmanMeafienIn;
                            txtZNLavodaMax.Text = "לא זכאי"; 
                            txtZNMavodaMax.Text  = iZmanMeafienIn.ToString() ;
                            break; 
                        case 3:
                            iZmanMeafien = (iZmanMeafienIn+1)/ 2;
                            txtZNLavodaMax.Text = iZmanMeafien.ToString() ;
                            txtZNMavodaMax.Text = iZmanMeafien.ToString() ;
                            break;      
                    }
                }
                else if (iMeafienIn.ToString() == "61")
                {
                    try  // get Erech from "Merkaz Erua of Oved" (from table -"Pirtay-Ovdim") for kod 10 
                    {
                        clOvdim oOvdim =  new clOvdim ();
                        int iKodErech = 10;
                        iMerkazErua = oOvdim.GetMerkazEruaByKod(iMisparIshi, iKodErech, dTarich);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    switch (iKodMeafienIn)
                    {
                        case 1:
                            iDakot = ZmanMistane61Kod1(iMisparIshi, dTarich, iMerkazErua, iMikumYaad);
                           
                            txtZNLavodaMax.Text = iDakot.ToString() ;
                            txtZNMavodaMax.Text = "לא זכאי ";
                            break;
                        case 2:
                            iDakot = ZmanMistane61Kod2(iMisparIshi, dTarich, iMerkazErua, iMikumYaad);
                            txtZNMavodaMax.Text = iDakot.ToString() ;
                            txtZNLavodaMax.Text = "לא זכאי";
                            break;
                        case 3:
                            iDakot = ZmanMistane61Kod1(iMisparIshi, dTarich, iMerkazErua, iMikumYaad);
                            txtZNLavodaMax.Text = iDakot.ToString() ;
                            iDakot = ZmanMistane61Kod2(iMisparIshi, dTarich, iMerkazErua, iMikumYaad);
                            txtZNMavodaMax.Text = iDakot.ToString() ;
                            break;
                    }
                }
                // Update זמני נסיעות בפועל:   זמן נסיעה לעבודה & זמן נסיעה מהעבודה
                ZmanNesiaBefoal(iMisparIshi, dTarich);

                Session["ZmanHalochOld"] = int.Parse(txtZNLavoda.Text);
                Session["ZmanHazorOld"] = int.Parse(txtZNMavoda.Text);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
   private void    ZmanNesiaBefoal(int iMisparIshi,DateTime dTarich)
       // Update זמני נסיעות בפועל:   זמן נסיעה לעבודה & זמן נסיעה מהעבודה
    {
        int iZmanNesiaHaloch = 0;
        int iZmanNesiaHazor = 0;
        
              clOvdim oOvdim = new clOvdim();
              oOvdim.GetZmanNesiaaOvdim(iMisparIshi, dTarich, ref  iZmanNesiaHaloch, ref  iZmanNesiaHazor);
             
              txtZNLavoda.Text = iZmanNesiaHaloch.ToString();
              txtZNMavoda.Text = iZmanNesiaHazor.ToString();
    }
    
    private int ZmanMistane61Kod1(int iMisparIshi, DateTime dTarich, int iMerkazErua, int iMikumYaad )
    {
        int iMikumShaonKnisa = 0;
        int iMikumShaonYetzia = 0;
      
        
        try  //  get Mikum Shaon Knisa of first "Sidur" on that date
        {
            clOvdim oOvdim = new clOvdim();
            oOvdim.GetMikumShaonInOut(iMisparIshi, dTarich, ref  iMikumShaonKnisa, ref  iMikumShaonYetzia);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        if (iMikumShaonKnisa > 0)
           {
            iMikumYaad = iMikumShaonKnisa;
       
              try
              {
                  clOvdim oOvdim = new clOvdim();
                  oOvdim.GetDakot(iMerkazErua, iMikumYaad, dTarich, ref  iDakot);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
        } 
         else
            {
                try
                {
                    clUtils oUtils = new clUtils();
                    int iKodErech = 139;
                 //   iDakot = oOvdim.GetErechPirteyOvdim(iMisparIshi, iKodErech);

                    iDakot = oUtils.GetTbParametrim(iKodErech, dTarich);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        return iDakot;
        }


    private int ZmanMistane61Kod2(int iMisparIshi, DateTime dTarich, int iMerkazErua, int iMikumYaad  )
        {
            int iMikumShaonKnisa = 0;
            int iMikumShaonYetzia = 0;
          

            try  //  get Mikum Shaon Yetziaa of last "Sidur" on that date
            {
                clOvdim oOvdim = new clOvdim();
                oOvdim.GetMikumShaonInOut(iMisparIshi, dTarich, ref  iMikumShaonKnisa, ref  iMikumShaonYetzia);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (iMikumShaonYetzia > 0)
            {
                iMikumYaad = iMikumShaonYetzia;
                try
                {
                    clOvdim oOvdim = new clOvdim();
                    oOvdim.GetDakot(iMerkazErua, iMikumYaad, dTarich, ref  iDakot);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
           else
            {
                try
                {
                    clUtils oUtils = new clUtils();
                    int iKodErech = 139;
                   // iDakot = oOvdim.GetErechPirteyOvdim(iMisparIshi, iKodErech);

                    iDakot = oUtils.GetTbParametrim(iKodErech, dTarich);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iDakot;
        }

    protected void  btnClose_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtChanged.Value == "1")
            {
                ScriptManager.RegisterStartupScript(btnUpdate, this.GetType(), "Close", "confirmUpdate();", true);
                //UpdateDB();
            }


            else
            {
                ScriptManager.RegisterStartupScript(btnUpdate, this.GetType(), "Close", "window.close();", true);
                //close();
            }
         
       }
 
    }

    private void close()
    {
        throw new NotImplementedException();
    }

    protected void  btnUpdate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            UpdateDB();
        //    if (txtSwitchClose.Value=="1")
                ScriptManager.RegisterStartupScript(btnUpdate, this.GetType(), "Close", "window.close();", true);
        }  
    }

    private void UpdateDB()
    {
        int meadken_acharon=0;
        if (Page.IsValid)
        {
            txtChanged.Value = "0";  // undo txtchaned .

            izmaNesiaHaloch = int.Parse(txtZNLavoda.Text.ToString());
            iZmanNesiaHazor = int.Parse(txtZNMavoda.Text.ToString());

            if (LoginUser.UserInfo.EmployeeNumber != null && LoginUser.UserInfo.EmployeeNumber !="")
                meadken_acharon = int.Parse(LoginUser.UserInfo.EmployeeNumber);
            try
            {
                clOvdim oOvdim = new clOvdim();
                oOvdim.UpdZmanNesiaa(iMisparIshi, dTarich, izmaNesiaHaloch, iZmanNesiaHazor, meadken_acharon);
                InsertToTBIdkuneyRashemet();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    private void InsertToTBIdkuneyRashemet()
    {
        COLL_IDKUN_RASHEMET oCollIdkunRashemet = new COLL_IDKUN_RASHEMET();
        OBJ_IDKUN_RASHEMET _ObjIdkunRashemet; 
         try
            {
                if (int.Parse(Session["ZmanHalochOld"].ToString()) != izmaNesiaHaloch)
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = 40;//זמן נסיעה הלוך
                    FillObjIdkuneyRashemet(ref _ObjIdkunRashemet);
                    oCollIdkunRashemet.Add(_ObjIdkunRashemet);
                }

                if (int.Parse(Session["ZmanHazorOld"].ToString()) != iZmanNesiaHazor)
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = 41; //זמן נסיעה חזור
                    FillObjIdkuneyRashemet(ref _ObjIdkunRashemet);
                    oCollIdkunRashemet.Add(_ObjIdkunRashemet);
                }
                ServiceLocator.Current.GetInstance<IShinuyimManager>().SaveIdkunRashemet(oCollIdkunRashemet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillObjIdkuneyRashemet(ref OBJ_IDKUN_RASHEMET _ObjIdkunRashemet)
        {
            _ObjIdkunRashemet.TAARICH = dTarich;
            _ObjIdkunRashemet.MISPAR_ISHI = iMisparIshi;
            _ObjIdkunRashemet.MISPAR_SIDUR = 0;
            _ObjIdkunRashemet.SHAT_HATCHALA = DateTime.MinValue;
            _ObjIdkunRashemet.NEW_SHAT_HATCHALA = DateTime.MinValue; 
            _ObjIdkunRashemet.SHAT_YETZIA = DateTime.MinValue;
            _ObjIdkunRashemet.NEW_SHAT_YETZIA = DateTime.MinValue; 
            _ObjIdkunRashemet.MISPAR_KNISA = 0;
            _ObjIdkunRashemet.GOREM_MEADKEN = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        }
}



      







