using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using KdsLibrary.BL;
using System.Data;
using System.Collections.Generic;
using KdsLibrary;
using KdsBatch;
using System.Text;
using KdsWorkFlow.Approvals;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using KdsLibrary.Security;
using System.Collections.Specialized;

//using System.Web.UI;
//using System.IO;
/// <summary>
/// Summary description for wsGeneral
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class wsGeneral : System.Web.Services.WebService
{
    private const string COL_TRIP_EMPTY = "ריקה";
    private const string COL_TRIP_NAMAK = "נמ'ק";
    private const string COL_TRIP_ELEMENT = "אלמנט";
    private const string COL_TRIP_VISUT = "ויסות";
    private const string MAKAT_KNISA = "לפי-צורך";
    private const int SIDUR_HITYAZVUT_A = 99200;
    private const int SIDUR_HITYAZVUT_B = 99214;    
   
    public wsGeneral()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string IsCardExists(int iMisparIshi, string sWorkCard)
    {
        clWorkCard _WorkCard = new clWorkCard();
        DateTime dWorkCard;
        int iCardCount;
        try
        {
            dWorkCard = DateTime.Parse(sWorkCard);
            iCardCount = _WorkCard.GetIsCardExistsInYemeyAvodaOvdim(iMisparIshi, dWorkCard);
            return iCardCount.ToString() + "|" + clGeneral.arrDays[dWorkCard.DayOfWeek.GetHashCode()];
        }
        catch (System.FormatException ex)
        {
            return "0";
        }
        catch (System.InvalidCastException ex)
        {
            return "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    [WebMethod]
    public string GetOvedMisparIshi(string sName)
    {
        string sMisparIshi = "";

        clOvdim oOvdim = new clOvdim();
        try
        {
            if (sName != string.Empty)
            {
                sMisparIshi = oOvdim.GetOvedMisparIshi(sName);
            }
            return sMisparIshi;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string GetOvedSnifAndUnit(int iMisparIshi)
    {
        try
        {
            string sSnif = "";
            string sUnit = "";
            clOvdim oOvdim = new clOvdim();
            if (iMisparIshi > 0)
            {
                oOvdim.GetOvedSnifAndUnit(iMisparIshi, ref sSnif, ref sUnit);
            }
            return string.Concat(sSnif, "/", sUnit);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string GetOvedAllDetails(int iMisparIshi, string sCardDate)
    {
        String sXMLResult = "";
        clOvdim oOvdim = new clOvdim();
        DataTable dt;
        DateTime dCardDate = DateTime.Parse(sCardDate);

        try
        {
            if (iMisparIshi > 0)
            {
                //נתוני עובד                
                dt = oOvdim.GetOvedDetails(iMisparIshi, dCardDate);
                sXMLResult = BuildOvedDetailsXml(dt, iMisparIshi, dCardDate);
                //נתוני כרטיס     

            }
            return sXMLResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool IsElementHachanatMechona(long lMakatMesia)
    {
        string sMakatPrefix =  lMakatMesia.ToString().Substring(0,3);
        return ((sMakatPrefix == clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString()) || (sMakatPrefix == clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
                             || (sMakatPrefix == clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString()));

    }
    [WebMethod(EnableSession = true)]
    private string BuildMakatDetails(DataTable dtMakat, string sTravelDate, string sShatYetiza, string sDayToAdd, 
                                     long lNewMakat, long lOldMakat, int iSidurIndex, int iPeilutIndex, ref clPeilut _PeilutElement)
    {
        StringBuilder sXML = new StringBuilder();
        if (sShatYetiza.Equals("__:__")) sShatYetiza = "";
        DateTime dActivityDate = DateTime.Parse(sTravelDate + " " + sShatYetiza).AddDays(int.Parse(sDayToAdd));
        clKavim _Kavim = new clKavim();
        DataTable dtElement=new DataTable();
        clKavim.enMakatType oMakatType;
        DataRow[] dr;
        clSidur _Sidur;
        clPeilut _Peilut;
        int iDefMinutesForElement, iDefMinForElemenTWithoutFactor;
        try
        {
            _Sidur = (clSidur)(((OrderedDictionary)Session["Sidurim"])[iSidurIndex]);
            _Peilut = (clPeilut)_Sidur.htPeilut[iPeilutIndex];

            sXML.Append("<MAKAT>");


            if (dtMakat.Rows.Count > 0)
            {
                oMakatType = (clKavim.enMakatType)GetMakatType(lNewMakat);

                if (oMakatType == clKavim.enMakatType.mElement)
                {
                    sXML.Append(string.Concat("<DAKOT_DEF>", "", "</DAKOT_DEF>"));
                    sXML.Append(string.Concat("<DAKOT_DEF_TITLE>", "", "</DAKOT_DEF_TITLE>"));

                    long lElementValue;
                    //יכיל את מאפייני האלמנט
                    dtElement = _Kavim.GetMeafyeneyElementByKod(lNewMakat, DateTime.Parse(sTravelDate));
                    if (dtElement.Rows.Count > 0)
                    {
                        //ד.	אם שינו לאלמנט עם מאפיין 13 (דיווח בסידור מיוחד) וערך 2 (לא רשאי לדיווח בסידור מיוחד) והסידור הוא סידור מיוחד,  יש להציג הודעה: "אסור לדווח אלמנט זה בסידור מיוחד". 
                        //dr = dtElement.Select("kod_meafyen=" + 13);
                        //if (dr.Length > 0)
                        //    if ((dtElement.Rows[0]["erech"].Equals("2")))
                        //        sXML.Append(string.Concat("<LO_DIVUCH>", "1", "</LO_DIVUCH>"));
                        //ה.	אם שינו לאלמנט עם מאפיין 12 (דיווח בסידור ויזה) עם ערך 2 (לא רשאי לדיווח בסידור  ויזה), והסידור הוא סידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים), 
                        //dr = dtElement.Select("kod_meafyen=" + 12);
                        //if (dr.Length > 0)
                        //    if ((dtElement.Rows[0]["erech"].Equals("2")))
                        //        sXML.Append(string.Concat("<NO_DIVUCH_VISA>", "1", "</NO_DIVUCH_VISA>"));


                        //ב.	שינוי לאלמנט שאסור לדווח בו מספר רכב (אלמנט ללא מאפיין 11 (חובה מספר רכב) ולפני שינוי המק"ט היה ערך בשדה מספר רכב, יש למחוק את מספר הרכב
                        dr = dtElement.Select("kod_meafyen=" + 11);
                        if (dr.Length == 0)
                        {
                            //if (!(dtElement.Rows[0]["erech"].Equals("1")))
                            //{
                            sXML.Append(string.Concat("<OTO_NO>", "", "</OTO_NO>"));
                            sXML.Append(string.Concat("<OTO_NO_ENABLED>", "0", "</OTO_NO_ENABLED>"));
                            sXML.Append(string.Concat("<OTO_NO_TITEL>", "", "</OTO_NO_TITEL>"));                           
                            // }
                        }
                        else{
                            sXML.Append(string.Concat("<OTO_NO_ENABLED>", "1", "</OTO_NO_ENABLED>"));
                            _PeilutElement.bBusNumberMustExists = true;
                        }
                        dr = dtElement.Select("kod_meafyen=" + 40);
                        if (dr.Length > 0)
                        {
                            sXML.Append(string.Concat("<MAKAT_NOT_EXIST>", "1", "</MAKAT_NOT_EXIST>"));
                        }

                        dr = dtElement.Select("kod_meafyen=" + 39);
                        if (dr.Length > 0)
                        {
                            _PeilutElement.bElementIgnoreReka = true;
                        }
                        ////ג.	אם שינוי לאלמנט לידיעה (אלמנט עם מאפיין 3 (פעולה/ידיעה בלבד) וערך 2 (ידיעה בלבד), ולפני שינוי המק"ט היה ערך בשדה שעת יציאה, יש למחוק את שעת היציאה
                        //dr = dtElement.Select("kod_meafyen=" + 3);
                        //if (dr.Length > 0)
                        //    if ((dtElement.Rows[0]["erech"].Equals("2")))
                        //    {
                        //        sXML.Append(string.Concat("<SHAT_YETIZA>", "", "</SHAT_YETIZA>"));
                        //        sXML.Append(string.Concat("<SHAT_YETIZA_ENABLED>", "0", "</SHAT_YETIZA_ENABLED>"));
                        //    }

                        //. אם שינו למק"ט מסוג אלמנט או ששינו לאמנט את הערך, ראה המקרים הבאים:
                        // א.	עבור אלמנט מסוג דקות/כמות/קוד  (ערך 1 (דקות)/2 (כמות)/3 (קוד) במאפיין 4 (ערך האלמנט) בטבלת מאפייני אלמנטים, ניתן להגדיר דקות/כמות/קוד שנע בין הערך המינימלי לאלמנט לפי מאפיין 6 (זמן / כמות קוד מינימלי) למאפיין 7 (זמן / כמות / קוד מכסימלי) בטבלת מאפייני אלמנטים. אם לאלמנט אין מאפיין 6 ו/או 7 אז אין הגבלה בערך (יכול להיות שיש את אחד מהמאפיינים). אם הקלידו ערך לא בהתאם למאפיינים, יש להציג הודעה: "יש להקליד ערך בתחום:"ערך מאפיין  
                        // 6"/"0" (אם לא קיים מאפיין 6) עד 
                        //ערך בתחום +"ערך מאפיין 7"/"999"      
                        //(אם לא קיים מאפיין 7).

                        lElementValue = ((lNewMakat % 100000) / 100);
                        dr = dtElement.Select("kod_meafyen=" + 6);
                        if (dr.Length > 0)
                            if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
                                if (lElementValue < long.Parse(dr[0]["erech"].ToString()))
                                    sXML.Append(string.Concat("<MEAFYEN6ERR>", long.Parse(dr[0]["erech"].ToString()), "</MEAFYEN6ERR>"));

                        dr = dtElement.Select("kod_meafyen=" + 7);
                        if (dr.Length > 0)
                            if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
                                if (lElementValue > long.Parse(dr[0]["erech"].ToString()))
                                    sXML.Append(string.Concat("<MEAFYEN7ERR>", long.Parse(dr[0]["erech"].ToString()), "</MEAFYEN7ERR>"));
                        // אלמנט מסוג ריקה (מאפיין 23 = 1)  - להציג 
                        //בעמודה ערך מפוזיציות 4-6 * פרמטר 43.
                        dr = dtElement.Select("kod_meafyen=" + 23);
                        if (dr.Length > 0)
                            if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
                                if (dr[0]["erech"].ToString().Equals("1"))
                                {
                                    iDefMinForElemenTWithoutFactor = int.Parse(lNewMakat.ToString().Substring(3, 3));
                                    iDefMinutesForElement = (int)(System.Math.Round(((clParameters)Session["Parameters"]).fFactorNesiotRekot * iDefMinForElemenTWithoutFactor));
                                    sXML.Append(string.Concat("<DAKOT_DEF>", iDefMinutesForElement.ToString(), "</DAKOT_DEF>"));
                                    sXML.Append(string.Concat("<DAKOT_DEF_TITLE>", "הגדרה לגמר היא " + iDefMinForElemenTWithoutFactor.ToString() + " דקות ", "</DAKOT_DEF_TITLE>"));
                                }
                        //// אלמנט מסוג נסיעה מלאה (מאפיין 35 = 1) - 
                        ////להציג בעמודה ערך מפוזיציות 4-6 * פרמטר 42.
                        //dr = dtElement.Select("kod_meafyen=" + 35);
                        //if (dr.Length > 0)
                        //    if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
                        //        if (dr[0]["erech"].ToString().Equals("1"))
                        //        {
                        //            iDefMinForElemenTWithoutFactor =int.Parse(lNewMakat.ToString().Substring(3, 3));
                        //            iDefMinutesForElement = (int)((((clParameters)Session["Parameters"]).fFactor) * iDefMinForElemenTWithoutFactor);
                        //            sXML.Append(string.Concat("<DAKOT_DEF>", iDefMinutesForElement.ToString(), "</DAKOT_DEF>"));
                        //            sXML.Append(string.Concat("<DAKOT_DEF_TITLE>", "הגדרה לגמר היא " + iDefMinForElemenTWithoutFactor.ToString() + " דקות ", "</DAKOT_DEF_TITLE>"));
                        //        }
                    }
                    else
                    {   //אין מאפיינים
                        sXML.Append(string.Concat("<MAKAT_NOT_EXIST>", "1", "</MAKAT_NOT_EXIST>"));
                    }
                }
                
                switch (oMakatType)
                {
                    case clKavim.enMakatType.mKavShirut:
                        sXML.Append(string.Concat("<MAZAN_TASHLUM>", dtMakat.Rows[0]["mazantashlum"].ToString(), "</MAZAN_TASHLUM>"));
                        sXML.Append(string.Concat("<HYPER_LINK>", "1", "</HYPER_LINK>"));
                        sXML.Append(string.Concat("<SHILUT>", dtMakat.Rows[0]["shilut"].ToString(), "</SHILUT>"));
                        sXML.Append(string.Concat("<SHILUT_NAME>", dtMakat.Rows[0]["sugshirutname"].ToString(), "</SHILUT_NAME>"));
                        sXML.Append(string.Concat("<DESC>", dtMakat.Rows[0]["description"].ToString(), "</DESC>"));
                        sXML.Append(string.Concat("<DAKOT_DEF>", dtMakat.Rows[0]["mazantichnun"].ToString(), "</DAKOT_DEF>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL>", "", "</DAKOT_BAFOAL>"));                       
                        sXML.Append(string.Concat("<OTO_NO_ENABLED>", "1", "</OTO_NO_ENABLED>"));                        
                        sXML.Append(string.Concat("<DAKOT_DEF_TITLE>", "הגדרה לגמר היא " + dtMakat.Rows[0]["mazantashlum"].ToString() + " דקות ", "</DAKOT_DEF_TITLE>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL_ENABLED>", "1", "</DAKOT_BAFOAL_ENABLED>"));
                       
                        //sXML.Append(string.Concat("<HYPER_LINK>", "a onclick='AddHosafatKnisot(0,lstSidurim_000_ctl03);' style='text-decoration:underline;cursor:pointer;'>" + dtMakat.Rows[0]["description"].ToString(), "</HYPER_LINK>"));
                       
                        if (!sShatYetiza.Equals(""))
                            if (!String.IsNullOrEmpty(dtMakat.Rows[0]["kisuitor"].ToString()))
                               dActivityDate = dActivityDate.AddMinutes(-int.Parse(dtMakat.Rows[0]["kisuitor"].ToString()));

                        if ((dActivityDate.ToShortTimeString().Equals(sShatYetiza)))
                        {
                            sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                            sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                        }
                        else
                        {
                            if (sShatYetiza.Equals(""))                            
                                sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                            else
                                sXML.Append(string.Concat("<KISUY_TOR>", dActivityDate.ToShortTimeString(), "</KISUY_TOR>"));

                            if (String.IsNullOrEmpty(dtMakat.Rows[0]["kisuitor"].ToString()))
                                sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                            else
                            {
                                if (int.Parse(dtMakat.Rows[0]["kisuitor"].ToString()).Equals(0))
                                    sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                                else
                                    sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "1", "</KISUY_TOR_ENABLED>"));
                            }
                        }

                        sXML.Append(string.Concat("<KISUY_TOR_MAP>", dtMakat.Rows[0]["kisuitor"].ToString(), "</KISUY_TOR_MAP>"));
                        if (iPeilutIndex==0)//נאפשר הוספת ריקה למעלה
                            sXML.Append(string.Concat("<REKA_UP>", "1", "</REKA_UP>"));
                        if (iPeilutIndex == 1) //נבדוק את הפעילות הקודמת, אם היא הכנת מכונה, נאפשר הוספת ריקה למעלה
                        {
                            _Peilut = (clPeilut)_Sidur.htPeilut[iPeilutIndex-1];
                            if (IsElementHachanatMechona(_Peilut.lMakatNesia))
                                sXML.Append(string.Concat("<REKA_UP>", "1", "</REKA_UP>"));
                        }
                        break;
                    case clKavim.enMakatType.mNamak:
                        sXML.Append(string.Concat("<HYPER_LINK>", "0", "</HYPER_LINK>"));
                        if ((!sShatYetiza.Equals("")) && (!String.IsNullOrEmpty(dtMakat.Rows[0]["kisuitor"].ToString())))
                            dActivityDate = dActivityDate.AddMinutes(-int.Parse(dtMakat.Rows[0]["kisuitor"].ToString()));
                        if (dtMakat.Rows[0]["kisuitor"].ToString().Equals("0")){
                             sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                             sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                        }
                        else{
                             if (sShatYetiza.Equals("")) 
                                sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                             else
                                 sXML.Append(string.Concat("<KISUY_TOR>", dActivityDate.ToShortTimeString(), "</KISUY_TOR>"));

                             sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "1", "</KISUY_TOR_ENABLED>"));
                        }
                        sXML.Append(string.Concat("<MAZAN_TASHLUM>", dtMakat.Rows[0]["mazantashlum"].ToString(), "</MAZAN_TASHLUM>"));
                        sXML.Append(string.Concat("<KISUY_TOR_MAP>", dtMakat.Rows[0]["kisuitor"].ToString(), "</KISUY_TOR_MAP>"));
                        sXML.Append(string.Concat("<SHILUT>", dtMakat.Rows[0]["shilut"].ToString(), "</SHILUT>"));
                        sXML.Append(string.Concat("<SHILUT_NAME>", COL_TRIP_NAMAK, "</SHILUT_NAME>"));
                        sXML.Append(string.Concat("<DESC>", dtMakat.Rows[0]["description"].ToString(), "</DESC>"));
                        sXML.Append(string.Concat("<DAKOT_DEF>", dtMakat.Rows[0]["mazantichnun"].ToString(), "</DAKOT_DEF>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL>", "", "</DAKOT_BAFOAL>"));                        
                        sXML.Append(string.Concat("<OTO_NO_ENABLED>", "1", "</OTO_NO_ENABLED>"));
                        sXML.Append(string.Concat("<DAKOT_DEF_TITLE>", "הגדרה לגמר היא " + dtMakat.Rows[0]["mazantashlum"].ToString() + " דקות ","</DAKOT_DEF_TITLE>")) ;
                        sXML.Append(string.Concat("<DAKOT_BAFOAL_ENABLED>", "1", "</DAKOT_BAFOAL_ENABLED>"));
                        if (iPeilutIndex==0)//נאפשר הוספת ריקה למעלה
                            sXML.Append(string.Concat("<REKA_UP>", "1", "</REKA_UP>"));
                        if (iPeilutIndex == 1) //נבדוק את הפעילות הקודמת, אם היא הכנת מכונה, נאפשר הוספת ריקה למעלה
                        {
                            _Peilut = (clPeilut)_Sidur.htPeilut[iPeilutIndex-1];
                            if (IsElementHachanatMechona(_Peilut.lMakatNesia))
                                sXML.Append(string.Concat("<REKA_UP>", "1", "</REKA_UP>"));
                        }
                        break;
                    case clKavim.enMakatType.mElement:
                        sXML.Append(string.Concat("<HYPER_LINK>", "0", "</HYPER_LINK>"));
                        sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                        sXML.Append(string.Concat("<SHILUT>", "", "</SHILUT>"));
                        if (dtElement.Rows.Count > 0)
                            sXML.Append(string.Concat("<DESC>", dtMakat.Rows[0]["teur_element"].ToString(), "</DESC>"));
                       

                        sXML.Append(string.Concat("<SHILUT_NAME>", "", "</SHILUT_NAME>"));
                        sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL>", "", "</DAKOT_BAFOAL>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL_ENABLED>", "0", "</DAKOT_BAFOAL_ENABLED>"));
                        
                        break;
                    case clKavim.enMakatType.mEmpty:
                        sXML.Append(string.Concat("<MAZAN_TASHLUM>", dtMakat.Rows[0]["mazantashlum"].ToString(), "</MAZAN_TASHLUM>"));
                        sXML.Append(string.Concat("<HYPER_LINK>", "0", "</HYPER_LINK>"));
                        sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                        sXML.Append(string.Concat("<SHILUT>", "", "</SHILUT>"));
                        sXML.Append(string.Concat("<SHILUT_NAME>", COL_TRIP_EMPTY, "</SHILUT_NAME>"));
                        sXML.Append(string.Concat("<DAKOT_DEF_TITLE>", "הגדרה לגמר היא " + dtMakat.Rows[0]["mazantashlum"].ToString() + " דקות ", "</DAKOT_DEF_TITLE>"));
                        sXML.Append(string.Concat("<DESC>", dtMakat.Rows[0]["description"].ToString(), "</DESC>"));
                        sXML.Append(string.Concat("<DAKOT_DEF>", dtMakat.Rows[0]["mazantichnun"].ToString(), "</DAKOT_DEF>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL>", "", "</DAKOT_BAFOAL>"));                        
                        sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL_ENABLED>", "1", "</DAKOT_BAFOAL_ENABLED>"));
                        sXML.Append(string.Concat("<OTO_NO_ENABLED>", "1", "</OTO_NO_ENABLED>"));
                        if (iPeilutIndex==0)//נאפשר הוספת ריקה למעלה
                            sXML.Append(string.Concat("<REKA_UP>", "1", "</REKA_UP>"));
                        if (iPeilutIndex == 1) //נבדוק את הפעילות הקודמת, אם היא הכנת מכונה, נאפשר הוספת ריקה למעלה
                        {
                            _Peilut = (clPeilut)_Sidur.htPeilut[iPeilutIndex-1];
                            if (IsElementHachanatMechona(_Peilut.lMakatNesia))
                                sXML.Append(string.Concat("<REKA_UP>", "1", "</REKA_UP>"));
                        }
                        break;
                    case clKavim.enMakatType.mVisa:
                        sXML.Append(string.Concat("<MAZAN_TASHLUM>", dtMakat.Rows[0]["mazantashlum"].ToString(), "</MAZAN_TASHLUM>"));
                        sXML.Append(string.Concat("<HYPER_LINK>", "0", "</HYPER_LINK>"));
                        sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                        sXML.Append(string.Concat("<SHILUT>", "", "</SHILUT>"));
                        sXML.Append(string.Concat("<DESC>", dtMakat.Rows[0]["teur_visut"].ToString(), "</DESC>"));
                        sXML.Append(string.Concat("<DAKOT_DEF>", dtMakat.Rows[0]["mazantichnun"].ToString(), "</DAKOT_DEF>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL>", dtMakat.Rows[0]["mazantashlum"].ToString(), "</DAKOT_BAFOAL>"));
                        
                        sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL_ENABLED>", "1", "</DAKOT_BAFOAL_ENABLED>"));
                        //sXML.Append(string.Concat("<SHILUT_NAME>", COL_TRIP_VISUT, "</SHILUT_NAME>"));
                        break;
                    case clKavim.enMakatType.mVisut:
                        sXML.Append(string.Concat("<HYPER_LINK>", "0", "</HYPER_LINK>"));
                        sXML.Append(string.Concat("<KISUY_TOR>", "", "</KISUY_TOR>"));
                        sXML.Append(string.Concat("<SHILUT>", "", "</SHILUT>"));
                        sXML.Append(string.Concat("<SHILUT_NAME>", COL_TRIP_VISUT, "</SHILUT_NAME>"));
                        sXML.Append(string.Concat("<DESC>", dtMakat.Rows[0]["description"].ToString(), "</DESC>"));
                        sXML.Append(string.Concat("<DAKOT_DEF>", "0", "</DAKOT_DEF>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL>", "0", "</DAKOT_BAFOAL>"));
                        sXML.Append(string.Concat("<DAKOT_BAFOAL_ENABLED>", "0", "</DAKOT_BAFOAL_ENABLED>"));
                        sXML.Append(string.Concat("<KISUY_TOR_ENABLED>", "0", "</KISUY_TOR_ENABLED>"));
                        break;
                }
            }
            sXML.Append("</MAKAT>");
            return sXML.ToString();
        }
        catch (Exception ex)
        {


            throw ex;
        }
    }

    private string BuildOvedDetailsXml(DataTable dtOvedDetails, int iMisparIshi, DateTime dCardDate)
    {
        StringBuilder sXML = new StringBuilder();
        clOvedYomAvoda oOvedYomAvodaDetails = new clOvedYomAvoda(iMisparIshi, dCardDate);
        try
        {
            sXML.Append("<OVED>");
            if (dtOvedDetails.Rows.Count > 0)
            {
                sXML.Append(string.Concat("<COMPANY>", dtOvedDetails.Rows[0]["teur_hevra"].ToString(), "</COMPANY>"));
                sXML.Append(string.Concat("<EZOR>", dtOvedDetails.Rows[0]["teur_ezor"].ToString(), "</EZOR>"));
                sXML.Append(string.Concat("<SNIF>", dtOvedDetails.Rows[0]["teur_snif_av"].ToString(), "</SNIF>"));
                sXML.Append(string.Concat("<MAAMAD>", dtOvedDetails.Rows[0]["teur_maamad_hr"].ToString(), "</MAAMAD>"));
                sXML.Append(string.Concat("<ISUK>", dtOvedDetails.Rows[0]["teur_isuk"].ToString(), "</ISUK>"));
                sXML.Append(string.Concat("<NAME>", dtOvedDetails.Rows[0]["full_name"].ToString(), "</NAME>"));

            }
            if (oOvedYomAvodaDetails.OvedDetailsExists)
            {
                sXML.Append(string.Concat("<TACHOGRAPH>", oOvedYomAvodaDetails.sTachograf, "</TACHOGRAPH>"));
                sXML.Append(string.Concat("<HALBASHA>", oOvedYomAvodaDetails.sHalbasha, "</HALBASHA>"));
                sXML.Append(string.Concat("<LINA>", oOvedYomAvodaDetails.sLina, "</LINA>"));
                sXML.Append(string.Concat("<BITUL_ZMAN_NESIOT>", oOvedYomAvodaDetails.sBitulZmanNesiot, "</BITUL_ZMAN_NESIOT>"));
            }

            sXML.Append("</OVED>");
            return sXML.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string GetOvedName(int iMisparIshi)
    {
        try
        {
            clOvdim oOvdim = new clOvdim();
            string sName = "";

            if (iMisparIshi > 0)
            {
                sName = oOvdim.GetOvedFullName(iMisparIshi);
            }
            return sName;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod(EnableSession = true)]
    public string[] GetOvdimByName(string prefixText, int count, string contextKey)
    {   //
        DataTable dt;
        clOvdim oOvdim = new clOvdim();
        string sOvdimNumber = "";
        DataRow[] drOvdimNumber;
        try
        {
            prefixText = string.Concat(prefixText, "%");

            if (contextKey == "1")
            {
                dt = (DataTable)Session["MisparimIshi"];
                drOvdimNumber = dt.Select("full_name like '" + prefixText.Replace("'", "''") + "'", " full_name asc");
                foreach (DataRow dr in drOvdimNumber)
                {
                    sOvdimNumber += dr["mispar_ishi"] + ",";
                }

                if (sOvdimNumber.Length > 0)
                {
                    sOvdimNumber = sOvdimNumber.Substring(0, sOvdimNumber.Length - 1);

                }
            }

            dt = oOvdim.GetOvdimByName(prefixText, sOvdimNumber);

            //string[] items = new string[dt.Rows.Count];
            List<string> items = new List<string>(count);
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i > count) { break; }
                items.Add(dr["Oved_Name"].ToString());
                //items.SetValue(dr["Oved_Name"].ToString(), i);
                i++;
            }

            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetOvdimForPremiot(string prefixText, int count, string contextKey)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(',');
            dt = oUtils.GetOvdimForPremiotType(prefixText, (string)Params[0], (string)Params[1]);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, "MISPAR_ISHI");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string[] GetOvdimForPresence(string prefixText, int count, string contextKey)
    {
        clOvdim oOvdim = clOvdim.GetInstance();
        DataTable dt = new DataTable();
        clGeneral.enProfile SecurityLevel;
        try
        {
            string[] Params = contextKey.Split(',');
            SecurityLevel = (clGeneral.enProfile)Enum.Parse(typeof(clGeneral.enProfile), Params[0]);
            switch (SecurityLevel)
            {
                case clGeneral.enProfile.enRashemet:
                case clGeneral.enProfile.enRashemetAll: 
                case clGeneral.enProfile.enSystemAdmin : 
                case clGeneral.enProfile.enVaadatPikuah :
                    dt = oOvdim.GetActiveWorkers(prefixText, DateTime.Parse(Params[2]), DateTime.Parse(Params[3]));
                    break;
                case clGeneral.enProfile.enMenahelImKfufim : 
                    dt = oOvdim.GetOvdimToUser(prefixText + "%", clGeneral.GetIntegerValue(Params[1]));
                    break;
                default:
                    break;
            }

            return clGeneral.ConvertDatatableColumnToStringArray(dt, "MISPAR_ISHI");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string[] GetIdOfYameyAvoda(string prefixText, int count, string contextKey)
    {
        clReport oReport = clReport.GetInstance();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(';');
            dt = oReport.GetIdOfYameyAvoda(DateTime.Parse(Params[0]), DateTime.Parse(Params[1]), prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, "mispar_ishi");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod(EnableSession = true)]
    public string[] GetOvdimById(string prefixText, int count, string contextKey)
    {   //מביא את כל המספרים האישיים שבתחום
        DataTable dt;
        clOvdim oOvdim = new clOvdim();
        string sOvdimNumber = "";
        string sSelect;//, ezor, snif, maamad;
        string[] pirteySinun;
        DataRow[] drOvdimNumber;
        try
        {
            prefixText = string.Concat(prefixText, "%");
            if (contextKey == "1")
            {
                sSelect="mispar_ishi_char  like '" + prefixText + "'";
                if (Session["PirteySinun"] != null)
                {
                    pirteySinun = Session["PirteySinun"].ToString().Split(';');
                    if (pirteySinun[0].ToString() != "-1" && pirteySinun[0].ToString() != "")
                        sSelect += " and TEUR_EZOR='" + pirteySinun[0].ToString() + "'";
                    if (pirteySinun[1].ToString() != "-1" && pirteySinun[1].ToString() != "")
                        sSelect += " and SNIF_AV='" + pirteySinun[1].ToString() + "'";
                    if (pirteySinun[2].ToString() != "-1" && pirteySinun[2].ToString() != "")
                        sSelect += " and TEUR_MAAMAD_HR='" + pirteySinun[2].ToString() + "'";
                }
                dt = (DataTable)Session["MisparimIshi"];
                drOvdimNumber = dt.Select(sSelect, "mispar_ishi asc");
                //drOvdimNumber = dt.Select("mispar_ishi_char  like '" + prefixText + "'", "mispar_ishi asc");
                foreach (DataRow dr in drOvdimNumber)
                {
                    sOvdimNumber += dr["mispar_ishi"] + ",";
                }

                if (sOvdimNumber.Length > 0)
                {
                    sOvdimNumber = sOvdimNumber.Substring(0, sOvdimNumber.Length - 1);
                    dt = oOvdim.GetOvdimMisparIshi(prefixText, sOvdimNumber);
                }
                else dt = new DataTable();
            }
            else
                dt = oOvdim.GetOvdimMisparIshi(prefixText, sOvdimNumber);
            
                //string[] items = new string[dt.Rows.Count];
                count = dt.Rows.Count;
                List<string> items = new List<string>(count);
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i > count) { break; }
                    //items.SetValue(dr["mispar_ishi"].ToString(), i);
                    items.Add(dr["mispar_ishi"].ToString());
                    i++;
                }
                return items.ToArray();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetSnifim(string prefixText, int count, string contextKey)
    {
        DataTable dt;
        DataRow[] drSelect;
        clUtils oUtils = new clUtils();
        string sSQL = "";

        try
        {
            dt = oUtils.GetSnifAv(int.Parse(contextKey));
            sSQL = string.Concat("Description like '", prefixText, "%'");
            drSelect = dt.Select(sSQL);
            string[] items = new string[drSelect.Length];
            int i = 0;
            foreach (DataRow dr in drSelect)
            {
                items.SetValue(dr["Description"].ToString(), i);
                i++;
            }
            return items;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }

    [WebMethod]
    public string[] GetOvdimToUser(string prefixText, int count, string contextKey)
    {   //מביא את כל המספרים האישיים  של העובדים הכפופים
        DataTable dt;
        clOvdim oOvdim = new clOvdim();

        try
        {
           
                prefixText = string.Concat(prefixText, "%");
                if (contextKey.Length > 0)
                {
                    dt = oOvdim.GetOvdimToUser(prefixText, int.Parse(contextKey));
                }
                else
                {
                    dt = oOvdim.GetOvdimMisparIshi(prefixText, contextKey);
                }

                List<string> items = new List<string>(count);

                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i > count) { break; }
                    items.Add(dr["mispar_ishi"].ToString());
                    i++;
                }

                return items.ToArray();                        
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetOvdimToUserByName(string prefixText, int count, string contextKey)
    {   //מביא את כל המספרים האישיים  של העובדים הכפופים
        DataTable dt;
        clOvdim oOvdim = new clOvdim();

        try
        {
            prefixText = string.Concat(prefixText, "%");
            if (contextKey.Length > 0)
            {
                dt = oOvdim.GetOvdimToUserByName(prefixText, int.Parse(contextKey));
            }
            else
            {
                dt = oOvdim.GetOvdimByName(prefixText, contextKey);
            }
            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i > count) { break; }
                items.Add(dr["Oved_Name"].ToString());
                i++;

            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod(EnableSession = true)]
    public string[] GetAdminEmployeeById(string prefixText, int count, string contextKey)
    {   //מביא את כל המספרים האישיים  של העובדים הכפופים
        DataTable dt;
        clOvdim oOvdim = new clOvdim();

        try
        {
            KdsSecurityLevel _SecurityLevel = (KdsSecurityLevel)(Session["SecurityLevel"]);

            if (_SecurityLevel == KdsSecurityLevel.ViewAll)
            {
                contextKey = "";
            }
            prefixText = string.Concat(prefixText, "%");
            if (contextKey.Length > 0)
            {
                dt = oOvdim.GetOvdimToUser(prefixText, int.Parse(contextKey));
            }
            else
            {
                dt = oOvdim.GetOvdimMisparIshi(prefixText, contextKey);
            }

            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i > count) { break; }
                items.Add(dr["mispar_ishi"].ToString());
                i++;
            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod(EnableSession = true)]
    public string[] GetAdminEmployeeByName(string prefixText, int count, string contextKey)
    {   //מביא את כל המספרים האישיים  של העובדים הכפופים
        DataTable dt;
        clOvdim oOvdim = new clOvdim();

        try
        {
            KdsSecurityLevel _SecurityLevel = (KdsSecurityLevel)(Session["SecurityLevel"]);

            if (_SecurityLevel == KdsSecurityLevel.ViewAll)
            {
                contextKey = "";
            }
            prefixText = string.Concat(prefixText, "%");
            if (contextKey.Length > 0)
            {
                dt = oOvdim.GetOvdimToUserByName(prefixText, int.Parse(contextKey));
            }
            else
            {
                dt = oOvdim.GetOvdimByName(prefixText, contextKey);
            }
            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i > count) { break; }
                items.Add(dr["Oved_Name"].ToString());
                i++;

            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string[] GetMeafyenyeBitzuaCode(string prefixText, int count)
    {   //מביא מאפייני ביצוע
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drMeafyenim;

        try
        {
            prefixText = string.Concat(prefixText, "%");
            dt = oUtils.GetCombo(clGeneral.cProGetMeafyeneyBitsua, "");

            drMeafyenim = dt.Select("KOD_MEAFYEN_BITZUA like '" + prefixText + "'", "KOD_MEAFYEN_BITZUA asc");


            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in drMeafyenim)
            {
                if (i > count) { break; }
                items.Add(dr["KOD_MEAFYEN_BITZUA"].ToString());
                i++;

            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string[] GetMeafyenyeBitzuaTeur(string prefixText, int count)
    {   //מביא מאפייני ביצוע
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drMeafyenim;

        try
        {
            prefixText = string.Concat(prefixText, "%");
            dt = oUtils.GetCombo(clGeneral.cProGetMeafyeneyBitsua, "");

            drMeafyenim = dt.Select("TEUR_MEAFYEN_BITZUA like '" + prefixText + "'", "TEUR_MEAFYEN_BITZUA asc");


            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in drMeafyenim)
            {
                if (i > count) { break; }
                items.Add(dr["TEUR_MEAFYEN_BITZUA"].ToString());
                i++;

            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string[] GetTeurByKod(string prefixText, int count)
    {   //מביא קודי נתון
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drNatun;

        try
        {
            prefixText = string.Concat(prefixText, "%");
            dt = oUtils.GetCombo(clGeneral.cProGetKodNatun, "");

            drNatun = dt.Select("KOD_NATUN like '" + prefixText + "'", "KOD_NATUN asc");


            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in drNatun)
            {
                if (i > count) { break; }
                items.Add(dr["KOD_NATUN"].ToString());
                i++;

            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string[] GetNatunByTeur(string prefixText, int count)
    {   //מביא קודי נתון
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drNetunim;

        try
        {
            prefixText = string.Concat(prefixText, "%");
            dt = oUtils.GetCombo(clGeneral.cProGetKodNatun, "");

            drNetunim = dt.Select("TEUR_NATUN like '" + prefixText + "'", "TEUR_NATUN asc");


            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in drNetunim)
            {
                if (i > count) { break; }
                items.Add(dr["TEUR_NATUN"].ToString());
                i++;

            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// get the matching worker details (name/Id) for the autocomplete process 
    /// </summary>
    /// <param name="prefixText">the matching prefix</param>
    /// <param name="count"></param>
    /// <param name="contextKey">contain 6 sub parameters separeted by a ","</param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetWorkersDetailsOfHoursApproval(string prefixText, int count, string contextKey)
    {
        clOvdim oOvdim = clOvdim.GetInstance();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(',');
            string Filter = " AND " + Params[0] + " LIKE '" + prefixText.ToString() + "%'";
            dt = oOvdim.GetIshurim((clGeneral.enDemandType)(Int32.Parse(Params[1])),
                                                           Int32.Parse(Params[2]),
                                                           Params[3],
                                   (clGeneral.enMonthlyQuotaForm)(Int32.Parse(Params[4])),
                                   Filter);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, Params[0]);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    [WebMethod]
    public string[] GetRequestList(string prefixText, int count, string contextKey)
    {   //מבירא רשימת בקשות
        DataTable dt;
        clRequest oRequest = new clRequest();

        try
        {
            prefixText = string.Concat(prefixText, "%");

            dt = oRequest.GetRequestList(prefixText, contextKey);

            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i > count) { break; }
                items.Add(dr["BAKASHA_ID"].ToString());
                i++;
            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetOvdimFromLogBakashot(string prefixText, int count, string contextKey)
    {   //מביא רשימת עובדים מטבלת log bakashot
        //רק עובדים שיש להם רשומה בטבלה יופיעו ברשימה 
        DataTable dt;
        clRequest oRequest = new clRequest();

        try
        {
            prefixText = string.Concat(prefixText, "%");

            if (contextKey == null) { contextKey = "|0"; }
            dt = oRequest.GetListOvdimFromLogBakashot(prefixText, contextKey.Split('|')[0], int.Parse(contextKey.Split('|')[1]));

            List<string> items = new List<string>(count);

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i > count) { break; }
                items.Add(dr["mispar_ishi"].ToString());
                i++;
            }
            return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string GetOvedCardDetails()
    {

        try
        {
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string CheckOtoNo(long lOtoNo)
    {
        clKavim oKavim = new clKavim();
        long lLicenseNumber = 0;

        try
        {
            if (lOtoNo > 0)
            {
                //בודק אם מספר רכב קיים בתנועה ואם כן מחזיר מספר רישוי
                oKavim.GetBusLicenseNumber(lOtoNo, ref lLicenseNumber);
            }
            return lLicenseNumber.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod(EnableSession = true)]
    public string CheckMakat(int iMisparIshi, string  sCardDate, int iSidurIndex, int iPeilutIndex,
                             long lNewMakat, long lOldMakat, string sTravelDate, 
                             string sShatYetiza, string sDayToAdd)
    {
        clKavim oKavim = new clKavim();
        string sResult = "0";
        DataTable dtDetailsFromTnua;
        clPeilut _PeilutElement = new clPeilut();
        try
        {
            if (lNewMakat > 0)
            {
                //if ((sShatYetiza != string.Empty) || (sTravelDate != "01/01/0001"))
                //{
                    //בדיקה אם קיים מקט בתנועה
                    dtDetailsFromTnua = oKavim.GetMakatDetails(lNewMakat, DateTime.Parse(sCardDate));
                    if (dtDetailsFromTnua.Rows.Count > 0)
                    {
                        sResult = BuildMakatDetails(dtDetailsFromTnua, DateTime.Parse(sCardDate).ToShortDateString(),
                                                    sShatYetiza, sDayToAdd, lNewMakat, lOldMakat,  iSidurIndex,  iPeilutIndex, ref _PeilutElement);
                        //Update cash peilyot details from tnua
                        GetPeilyotTnuaDetails(iMisparIshi, DateTime.Parse(sCardDate), iSidurIndex, iPeilutIndex, lNewMakat, dtDetailsFromTnua, _PeilutElement);
                    }
                //}
            }
            return sResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    private void GetPeilyotTnuaDetails(int iMisparIshi, DateTime dCardDate, int iSidurIndex, 
                                       int iPeilutIndex, long lMakatNesia, DataTable dtPeilyotTnuaDetails,
                                       clPeilut _PeilutElement)
    {
        //string sCacheKey = iMisparIshi + dCardDate.ToShortDateString();
        clKavim _Kavim = new clKavim();
        //DataTable _Peilyout;
        DataRow[] _PeilyotDetails;
        clPeilut _Peilut;
        clSidur _Sidur;
        int iMakatType;
       // long lMakatNesia;
        try
        {
            _Sidur = (clSidur)(((OrderedDictionary)Session["Sidurim"])[iSidurIndex]);
            _Peilut =  (clPeilut)_Sidur.htPeilut[iPeilutIndex];
            iMakatType = _Kavim.GetMakatType(lMakatNesia);
            clKavim.enMakatType _MakatType;
            _MakatType = (clKavim.enMakatType)iMakatType;

            //lMakatNesia = long.Parse(dtPeilyotTnuaDetails.Rows[0]["Makat8"].ToString());
           
            
                //iMakatType = _Kavim.GetMakatType(lMakatNesia);
                //clKavim.enMakatType _MakatType;
                //_MakatType = (clKavim.enMakatType)iMakatType;
               
                switch (_MakatType)
                {
                    case clKavim.enMakatType.mKavShirut:
                        _Peilut.lMakatNesia = lMakatNesia;
                        _PeilyotDetails = dtPeilyotTnuaDetails.Select("Makat8=" + lMakatNesia.ToString());
                        if (_PeilyotDetails.Length > 0)
                        {
                            _Peilut.sMakatDescription = _PeilyotDetails[0]["Description"].ToString();
                            _Peilut.sShilut = _PeilyotDetails[0]["Shilut"].ToString();
                            _Peilut.sSugShirutName = _PeilyotDetails[0]["SugShirutName"].ToString();
                            _Peilut.iMazanTichnun = (!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTichnun"])) ? int.Parse(_PeilyotDetails[0]["MazanTichnun"].ToString()) : 0;
                            _Peilut.iMazanTashlum = (!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTashlum"])) ? int.Parse(_PeilyotDetails[0]["MazanTashlum"].ToString()) : 0;
                            _Peilut.iXyMokedTchila = (System.Convert.IsDBNull(_PeilyotDetails[0]["xymokedtchila"]) ? 0 : int.Parse(_PeilyotDetails[0]["xymokedtchila"].ToString()));
                            _Peilut.iXyMokedSiyum = (System.Convert.IsDBNull(_PeilyotDetails[0]["xymokedsiyum"]) ? 0 : int.Parse(_PeilyotDetails[0]["xymokedsiyum"].ToString()));
                            _Peilut.iKisuyTorMap = (!System.Convert.IsDBNull(_PeilyotDetails[0]["kisuitor"])) ? int.Parse(_PeilyotDetails[0]["kisuitor"].ToString()) : 0;
                            _Peilut.fKm = (!System.Convert.IsDBNull(_PeilyotDetails[0]["KM"])) ? float.Parse(_PeilyotDetails[0]["KM"].ToString()) : 0;
                            AddPeilutToPeiluyotDT(iMisparIshi, dCardDate, ref _Peilut);
                        }
                        break;
                    case clKavim.enMakatType.mEmpty:
                        _Peilut.lMakatNesia = lMakatNesia;
                        _PeilyotDetails = dtPeilyotTnuaDetails.Select("Makat8=" + lMakatNesia.ToString());
                        if (_PeilyotDetails.Length > 0)
                        {
                            _Peilut.iMazanTichnun = (!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTichnun"])) ? int.Parse(_PeilyotDetails[0]["MazanTichnun"].ToString()) : 0;
                            _Peilut.iMazanTashlum = (!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTashlum"])) ? int.Parse(_PeilyotDetails[0]["MazanTashlum"].ToString()) : 0;
                            _Peilut.iXyMokedTchila = (System.Convert.IsDBNull(_PeilyotDetails[0]["xymokedtchila"]) ? 0 : int.Parse(_PeilyotDetails[0]["xymokedtchila"].ToString()));
                            _Peilut.iXyMokedSiyum = (System.Convert.IsDBNull(_PeilyotDetails[0]["xymokedsiyum"]) ? 0 : int.Parse(_PeilyotDetails[0]["xymokedsiyum"].ToString()));
                           // _Peilut.iKisuyTorMap = (!System.Convert.IsDBNull(_PeilyotDetails[0]["kisuitor"])) ? int.Parse(_PeilyotDetails[0]["kisuitor"].ToString()) : 0;
                            _Peilut.fKm = (!System.Convert.IsDBNull(_PeilyotDetails[0]["KM"])) ? float.Parse(_PeilyotDetails[0]["KM"].ToString()) : 0;
                            _Peilut.sMakatDescription = _PeilyotDetails[0]["Description"].ToString(); ;
                            _Peilut.sShilut = "";
                            _Peilut.sSugShirutName = COL_TRIP_EMPTY;
                            
                            AddPeilutToPeiluyotDT(iMisparIshi, dCardDate, ref _Peilut);
                        }
                        break;

                    case clKavim.enMakatType.mNamak:
                         _Peilut.lMakatNesia = lMakatNesia;
                         _PeilyotDetails = dtPeilyotTnuaDetails.Select("Makat8=" + lMakatNesia.ToString());
                         if (_PeilyotDetails.Length > 0)
                         {
                             _Peilut.iXyMokedTchila = (System.Convert.IsDBNull(_PeilyotDetails[0]["xymokedtchila"]) ? 0 : int.Parse(_PeilyotDetails[0]["xymokedtchila"].ToString()));
                             _Peilut.iXyMokedSiyum = (System.Convert.IsDBNull(_PeilyotDetails[0]["xymokedsiyum"]) ? 0 : int.Parse(_PeilyotDetails[0]["xymokedsiyum"].ToString()));
                             _Peilut.iMazanTichnun = (!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTichnun"])) ? int.Parse(_PeilyotDetails[0]["MazanTichnun"].ToString()) : 0;
                             _Peilut.iMazanTashlum = (!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTashlum"])) ? int.Parse(_PeilyotDetails[0]["MazanTashlum"].ToString()) : 0;
                             _Peilut.sMakatDescription = _PeilyotDetails[0]["Description"].ToString();
                             _Peilut.sShilut = _PeilyotDetails[0]["Shilut"].ToString();
                             _Peilut.sSugShirutName = COL_TRIP_NAMAK;
                             _Peilut.iKisuyTorMap = (!System.Convert.IsDBNull(_PeilyotDetails[0]["kisuitor"])) ? int.Parse(_PeilyotDetails[0]["kisuitor"].ToString()) : 0;
                             _Peilut.fKm = (!System.Convert.IsDBNull(_PeilyotDetails[0]["KM"])) ? float.Parse(_PeilyotDetails[0]["KM"].ToString()) : 0;
                             AddPeilutToPeiluyotDT(iMisparIshi, dCardDate, ref _Peilut);
                         }
                         break;
                    case clKavim.enMakatType.mElement:
                        _Peilut.sMakatDescription=dtPeilyotTnuaDetails.Rows[0]["TEUR_ELEMENT"].ToString();
                        _Peilut.sShilut = "";
                        _Peilut.sSugShirutName = "";
                        _Peilut.iKisuyTorMap = 0;
                        _Peilut.iKisuyTor = 0;
                        _Peilut.iMazanTichnun = 0;//(!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTichnun"])) ? int.Parse(_PeilyotDetails[0]["MazanTichnun"].ToString()) : 0;
                        _Peilut.iMazanTashlum =0 ;//(!System.Convert.IsDBNull(_PeilyotDetails[0]["MazanTashlum"])) ? int.Parse(_PeilyotDetails[0]["MazanTashlum"].ToString()) : 0;
                        _Peilut.iXyMokedTchila = 0;//(System.Convert.IsDBNull(_PeilyotDetails[0]["xymokedtchila"]) ? 0 : int.Parse(_PeilyotDetails[0]["xymokedtchila"].ToString()));
                        _Peilut.iXyMokedSiyum=0;
                        _Peilut.fKm = 0;
                        _Peilut.bBusNumberMustExists = _PeilutElement.bBusNumberMustExists;
                        _Peilut.bElementIgnoreReka = _PeilutElement.bElementIgnoreReka;
                        _Peilut.lMakatNesia = lMakatNesia;
                         AddPeilutToPeiluyotDT(iMisparIshi, dCardDate, ref _Peilut);
                        break;
                    case clKavim.enMakatType.mVisut:                       
                        break;
                }                       
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void AddPeilutToPeiluyotDT(int iMisparIshi, DateTime dCardDate, ref clPeilut _Peilut)
    {
     
      DataRow dr;

      try
      {
          DataTable dt = (DataTable)HttpRuntime.Cache.Get(iMisparIshi.ToString() + dCardDate.ToShortDateString());
          if (dt != null)
          {
              if (dt.Columns.Count > 0)
              {
                  dr = dt.NewRow();
                  dr["Activity_Date"] = dCardDate;
                  dr["Makat8"] = _Peilut.lMakatNesia;
                  dr["description"] = _Peilut.sMakatDescription;
                  dr["shilut"] = _Peilut.sShilut;
                  dr["kisui_tor"] = _Peilut.iKisuyTorMap;
                  dr["mazan_tichnun"] = _Peilut.iMazanTichnun;
                  dr["mazan_tashlum"] = _Peilut.iMazanTashlum;
                  dr["km"] = _Peilut.fKm;
                  dr["sug_shirut_name"] = _Peilut.sSugShirutName;
                  dr["xy_moked_tchila"] = _Peilut.iXyMokedTchila;
                  dr["xy_moked_siyum"] = _Peilut.iXyMokedSiyum;
                  dt.Rows.Add(dr);
              }
          }
      }
      catch (Exception ex)
      {
          throw ex;
      }
    }
    [WebMethod]
    public int GetMakatType(long lMakat)
    {
        clKavim oKavim = new clKavim();
        try
        {
            return oKavim.GetMakatType(lMakat);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string[] GetOvdimLeRitza(string prefixText, int count, string contextKey)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(';');
            dt = oUtils.GetOvdimLeRitza(clGeneral.GetIntegerValue(Params[0]), (string)Params[1], (string)Params[2], prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetMakatOfActivity(string prefixText, int count, string contextKey)
    {
        clReport oReport = clReport.GetInstance();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(';');
            dt = oReport.GetMakatOfActivity(DateTime.Parse(Params[0]), DateTime.Parse(Params[1]), prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string[] GetWorkStation(string prefixText, int count, string contextKey)
    {
        clReport oReport = clReport.GetInstance();
        DataTable dt = new DataTable();
        try
        {
            dt = oReport.GetWorkStation(prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetLicenceNumber(string prefixText, int count, string contextKey)
    {
        clReport oReport = clReport.GetInstance();
        DataTable dt = new DataTable();
        try
        {
            dt = oReport.GetLicenceNumber(prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string[] GetSidurimOvdim(string prefixText, int count, string contextKey)
    {
        clReport oReport = clReport.GetInstance();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(';');
            dt = oReport.GetSidurimOvdim(DateTime.Parse(Params[0]), DateTime.Parse(Params[1]), prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public int CheckRashamPail(string prefixText, string contextKey)
    {
        string[] rasham;
        try
        {
            rasham = getMispareiRashamot(prefixText, 1, contextKey);
            return rasham.Length;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    [WebMethod]
    public string[] getMispareiRashamot(string prefixText, int count, string contextKey)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(',');
            dt = oUtils.getMispareiRashamot(clGeneral.GetIntegerValue(Params[0]), (string)Params[1], DateTime.Parse(Params[2]), prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string CheckKodElement(string kod, string Sidur, string taarichCA)
    {
        DataTable dt;
        DataTable dtSidur;
        DataRow[] drSelect;
        clUtils oUtils = new clUtils();
        string sSQL = "";
        string pratim = "";
        string teur;
        try
        {
            dt = oUtils.GetElementsVeMeafyenim(taarichCA);
            sSQL = string.Concat("KOD_ELEMENT='" + kod + "'");
            drSelect = dt.Select(sSQL);
            if (drSelect.Length == 0)
                return "1";
            teur = drSelect[0]["TEUR_ELEMENT"].ToString();
            sSQL = string.Concat("KOD_ELEMENT='" + kod + "' AND KOD_MEAFYEN='12' AND ERECH='2'");
            drSelect = dt.Select(sSQL);
            if (drSelect.Length == 0)
            {
                dtSidur = oUtils.GetMeafyenSidurByKodSidur(int.Parse(Sidur), taarichCA);

                sSQL = string.Concat("KOD_MEAFYEN='45'");
                drSelect = dtSidur.Select(sSQL);

                if (drSelect.Length > 0)
                    return "2";
            }
           

            sSQL = string.Concat("KOD_ELEMENT='" + kod + "' AND KOD_MEAFYEN='13' AND ERECH='1'");
            drSelect = dt.Select(sSQL);
            if (drSelect.Length > 0)
                if (Sidur.Substring(0, 1) == "9")
                    return "3";

            sSQL = string.Concat("KOD_ELEMENT='" + kod + "' AND KOD_MEAFYEN='4'");
            drSelect = dt.Select(sSQL);
            if (drSelect.Length > 0)
                pratim = drSelect[0]["TEUR_ELEMENT"].ToString() + ";" + drSelect[0]["ERECH"].ToString();
            else
                pratim = teur+";";
            //בדיקה אם חובה רכב
            sSQL = "KOD_ELEMENT='" + kod + "'  AND KOD_MEAFYEN='11'";
            drSelect = dt.Select(sSQL);
            if (drSelect.Length > 0)
                pratim = pratim + ";1";
            else pratim = pratim + ";0";

            return pratim;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }


    [WebMethod]
    public string[] GetTeurElements(string prefixText, int count)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        try
        {
            dt = oUtils.GetTeurElements(prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public int GetKodElementByTeur(string teurElemnt)
    {
        clUtils oUtils = new clUtils();
        int kod;

        try
        {
            kod = oUtils.GetKodElementByTeur(teurElemnt);
            return kod;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string getMigbalaLeErechElement(string kod, string taarichCA)
    {
        clUtils oUtils = new clUtils();
        DataTable dt;
        DataRow[] drSelect;
        string sSQL = "";
        string pratim = "";
        try
        {
            dt = oUtils.GetElementsVeMeafyenim(taarichCA);
            sSQL = string.Concat("KOD_ELEMENT='" + kod + "' AND KOD_MEAFYEN='6'");
            drSelect = dt.Select(sSQL);
            if (drSelect.Length > 0)
                pratim = drSelect[0]["ERECH"].ToString() + "-";
            else
                pratim = "0-";

            sSQL = string.Concat("KOD_ELEMENT='" + kod + "' AND KOD_MEAFYEN='7'");
            drSelect = dt.Select(sSQL);
            if (drSelect.Length > 0)
                pratim = pratim + drSelect[0]["ERECH"].ToString();
            else
                pratim = pratim + "999";
            return pratim;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] getTeureySidurimWhithOutList(string prefixText, int count, string contextKey)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(';');
            dt = oUtils.getTeureySidurimWhithOutList(prefixText, Params[0], Params[1]);
            //   dt = oUtils.getTeureySidurimWhithOutList(prefixText, contextKey);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public int getKodSidurByTeur(string teur)
    {
        clUtils oUtils = new clUtils();
        int kod;

        try
        {
            kod = oUtils.getKodSidurByTeur(teur);
            return kod;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] getKodSidurimWhithOutList(string prefixText, int count, string contextKey)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        clGeneral.enMeasherOMistayeg _MeasherOMistayeg;
        try
        {
            //AutoComplete for new sidur
            string[] Params = contextKey.Split(';');
            _MeasherOMistayeg = (clGeneral.enMeasherOMistayeg)int.Parse(Params[2]);
            if (_MeasherOMistayeg==clGeneral.enMeasherOMistayeg.ValueNull)
                dt = oUtils.getKodElement(prefixText, Params[0], Params[1]);
            else                
                dt = oUtils.getkodSidurimWhithOutList(prefixText, Params[0], Params[1]);

            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);        
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string getTeurSidurByKod(int kod)
    {
        clUtils oUtils = new clUtils();
        string teur;

        try
        {
            teur = oUtils.getTeurSidurByKod(kod);
            return teur;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string GetSidurDetailsFromTnua(int sidur, string date)
    {
        clKavim oKavim = new clKavim(); //.GetInstance();
        DataTable PirteySidur;
        int result;

        try
        {
            PirteySidur = oKavim.GetSidurDetailsFromTnua(sidur, DateTime.Parse(date), out result);
            if (result == 1)
                return "-1";
            return "0";
            //return teur;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod(EnableSession = true)]
    public string CancelError(int iMisparIshi, string sCardDate, string sErrorKey, string sGoremMeasher)
    {
        clWorkCard oWorkCard = new clWorkCard();
        int iResult = 0;

        try
        {
            iResult = oWorkCard.InsertToShgiotMeusharot(iMisparIshi, DateTime.Parse(sCardDate), sErrorKey, sGoremMeasher);
            if (iResult.Equals(1))
                RemoveErrorFromSession(iMisparIshi,DateTime.Parse(sCardDate),sErrorKey);
            return iResult.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void RemoveErrorFromSession(int iMisparIshi, DateTime dCardDate, string sErrorKey){
        //נוריד מאובייקט שגיאות את השגיאה שבוטלה כדי שלא תעלה שוב במקרה של הוספת פעילות
        string[] arrResult = sErrorKey.Split((char.Parse("|")));
        int iMisparSidur,iErrNum, iErrorLevel, iMisparKnisa;
        DateTime dSidurStartHour, dActivityHour;
        DataTable dtErrorList = (DataTable)Session["Errors"];
        DataRow[] drResults;
        iErrorLevel = arrResult.Length;

        switch (iErrorLevel)
        {
            case 3: //רמת יום עבודה
                iErrNum = int.Parse(arrResult[2]);
                //הרוטינה מחזיר אמת אם קיימת שגיאה למפתח המתקבל
                drResults = dtErrorList.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrNum );
                if (drResults.Length > 0)
                {
                    foreach (DataRow dr in drResults)
                    {
                        dr.Delete();
                    }
                }
                break;
            case 5://רמת סידור
                iMisparSidur = int.Parse(arrResult[3]);
                dSidurStartHour = DateTime.Parse(arrResult[4]);
                iErrNum = int.Parse(arrResult[2]);
                //הרוטינה מחזיר אמת אם קיימת שגיאה למפתח המתקבל
                drResults = dtErrorList.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrNum + " and mispar_sidur=" + iMisparSidur + " and shat_hatchala = '" + dSidurStartHour + "'");
                if (drResults.Length > 0)
                {
                    foreach (DataRow dr in drResults)
                    {
                        dr.Delete();
                    }  
                }
                    break;
            case 7: //רמת פעילות                 
                iMisparSidur = int.Parse(arrResult[3]);
                dSidurStartHour = DateTime.Parse(arrResult[4]);
                iErrNum = int.Parse(arrResult[2]);
                dActivityHour = DateTime.Parse(arrResult[5]);
                iMisparKnisa = int.Parse(arrResult[6]);
                drResults = dtErrorList.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrNum + " and mispar_sidur=" + iMisparSidur + " and shat_hatchala = '" + dSidurStartHour + "' and shat_yetzia='" + dActivityHour + "' and mispar_knisa=" + iMisparKnisa.ToString());
                if (drResults.Length > 0)
                {
                    foreach (DataRow dr in drResults)
                    {
                        dr.Delete();
                    }
                }
                            
                break;
        }
       
            
    }
    [WebMethod]
    public string ApproveError(int iMisparIshi, string sCardDate, string sErrorKey, string sGoremMeasher)
    {
        WorkCard _WorkCard = new WorkCard();
        int iResult;

        try
        {
            iResult = _WorkCard.ApproveError(iMisparIshi, DateTime.Parse(sCardDate), sErrorKey, sGoremMeasher);
            return iResult.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //[WebMethod]
    //public string GetRSSReader()
    //{
    //    // Create a new Page and add the control to it.
    //    Page page = new Page();
    //    UserControl ctl =
    //      (UserControl)page.LoadControl("~/Modules/UserControl/ucPeiluyot.ascx");
    //    page.Controls.Add(ctl);

    //    // Render the page and capture the resulting HTML.
    //    StringWriter writer = new StringWriter();
    //    HttpContext.Current.Server.Execute(page, writer, false);

    //    // Return that HTML, as a string.
    //    return writer.ToString();
    //}
    [WebMethod(EnableSession = true)]
    public string GetFieldErrors(int iLevel, int iMisparIshi, string sCardDate, int iSidurNumber,
                                 string sSidurStartHour, string sPeilutShatYetiza, int iMisparKnisa,
                                 string sFieldName)
    {
        DataSet ds = new DataSet();
        string sWriter = "";
        DateTime dSidurStartHour, dActiveStartHour;        
        bool bProfileRashemet = (bool)Session["ProfileRashemet"];
      //  DataTable dt = (DataTable)Session["Errors"];
        DataTable dtErr = new DataTable();
        switch (iLevel)
        {
            case 1:
                ds = clDefinitions.GetErrorsForFields(bProfileRashemet, iMisparIshi, DateTime.Parse(sCardDate), sFieldName);
                break;
            case 2:
                dSidurStartHour = DateTime.Parse(sSidurStartHour);
                ds = clDefinitions.GetErrorsForFields(bProfileRashemet, iMisparIshi, DateTime.Parse(sCardDate), 
                     iSidurNumber, dSidurStartHour, sFieldName, ref dtErr);
                Session["Errors"] = dtErr;
                break;
            case 3:
                dSidurStartHour = DateTime.Parse(sSidurStartHour);
                dActiveStartHour = DateTime.Parse(sPeilutShatYetiza);
                ds = clDefinitions.GetErrorsForFields(bProfileRashemet,  iMisparIshi, DateTime.Parse(sCardDate), 
                     iSidurNumber, dSidurStartHour, dActiveStartHour, iMisparKnisa, sFieldName);
                break;
        }

        if (ds.Tables.Count > 0)
        {
            sWriter = TransformXsl(ds);
        }
        return sWriter;
    }
    private string TransformXsl(DataSet ds)
    {
        XmlDataDocument xmlDoc = new XmlDataDocument(ds);
        XPathNavigator nav = xmlDoc.CreateNavigator();
        XsltArgumentList xslArg = new XsltArgumentList();
        //xslArg.AddParam("SugHaskala", "", cboSugHaskala.SelectedValue)

        XslTransform xslTran = new XslTransform();
        xslTran.Load(Server.MapPath("..\\Xslt\\XSLErrors.xslt"));

        StringWriter writer = new StringWriter();
        xslTran.Transform(nav, xslArg, writer, null);
        return writer.ToString();
    }

    [WebMethod]
    public string[] GetOvdim(string prefixText, int count, string contextKey)
    {
        clReport oReport = clReport.GetInstance();
        DataTable dt = new DataTable();
        try
        {
            string[] Params = contextKey.Split(';');
            dt = oReport.GetOvdim(DateTime.Parse(Params[0]), DateTime.Parse(Params[1]), prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, "mispar_ishi");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string GetDakotPremiya(int misIshi, string taarich, int sugPremia)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        try
        {
            dt = oUtils.GetPremiaYadanitForOved(misIshi, DateTime.Parse(taarich), sugPremia);

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["DAKOT_PREMYA"].ToString();
            else
                return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string[] GetElements(string prefixText, int count)
    {
        clUtils oUtils = new clUtils();
        DataTable dt = new DataTable();
        try
        {

            dt = oUtils.getAllElements(prefixText);
            return clGeneral.ConvertDatatableColumnToStringArray(dt, dt.Columns[0].ColumnName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string SetMeasherOMistayeg(int iMisaprIshi, string sCardDate, int iStatus)
    {
        clOvdim _Ovdim = clOvdim.GetInstance();
        string sResult = "0";
        try
        {
            _Ovdim.SetMeasherOMistayeg(iMisaprIshi, DateTime.Parse(sCardDate), iStatus);
            sResult = "1";
            return sResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string GetLoLetashlumRemark(int iMisaprIshi, string sCardDate, int iMisparSidur, string sSidurStartHour)
    {
        clOvdim _Ovdim = clOvdim.GetInstance();
        string sResult = "";
        try
        {

            sResult = _Ovdim.GetSibaLoLetashlum(iMisaprIshi, DateTime.Parse(sCardDate), iMisparSidur, DateTime.Parse(sSidurStartHour));

            return sResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //[WebMethod(EnableSession = true)]
    //public string AddNesiaReka(int iMisparIshi, string sCardDate, int iMisparSidur, string sSidurShatHatchala, long lMakatStart, long lMakatEnd, string sShatYetiza,
    //                           string sPeilutDate, int iMazanTichnun, long lCarNum, string sPeilutShatYetiza)
    //{       
    //    DataTable _NesiaDetails;
    //    DataTable _Peiluyot;
    //    DataRow[] dr;
    //    long xPosStart=0;
    //    long xPosEnd = 0;
    //    clKavim _Kavim = new clKavim();
    //    int iResult = 0;
    //    long lMakat=0;
    //    long lRepresentMakat8=0;
    //    DateTime dPeilutShatYetiza = DateTime.Parse(sPeilutDate + " " + sShatYetiza);
        
    //    try
    //    {
    //        dPeilutShatYetiza = dPeilutShatYetiza.AddMinutes(iMazanTichnun);
            
    //        _Peiluyot = (DataTable)HttpRuntime.Cache.Get(iMisparIshi.ToString() + sCardDate);
    //        dr = _Peiluyot.Select("makat8=" + lMakatStart);
    //        if (dr.Length > 0)
    //            xPosStart = String.IsNullOrEmpty(dr[0]["xy_moked_siyum"].ToString()) ? 0 : long.Parse(dr[0]["xy_moked_siyum"].ToString());
    //        dr = _Peiluyot.Select("makat8=" + lMakatEnd);
    //        if (dr.Length > 0)
    //            xPosEnd = String.IsNullOrEmpty(dr[0]["xy_moked_tchila"].ToString()) ? 0 : long.Parse(dr[0]["xy_moked_tchila"].ToString());


    //        _NesiaDetails = _Kavim.GetRekaDetailsByXY(DateTime.Parse(sCardDate), xPosStart, xPosEnd, out iResult);

    //        //נמצאה ריקה מתאימה
    //        if (_NesiaDetails.Rows.Count > 0)
    //        {
    //            if (iResult == 0)
    //            {
    //                lMakat = String.IsNullOrEmpty(_NesiaDetails.Rows[0]["makat8"].ToString()) ? 0 : long.Parse((_NesiaDetails.Rows[0]["makat8"].ToString()));
    //                lRepresentMakat8 = String.IsNullOrEmpty(_NesiaDetails.Rows[0]["representmakat8"].ToString()) ? 0 : long.Parse((_NesiaDetails.Rows[0]["representmakat8"].ToString()));
    //                if ((lMakat == 0) && (lRepresentMakat8 > 0))
    //                    lMakat = lRepresentMakat8;

    //                InsertNesiaRekaToDB(int.Parse(Session["LoginUserEmp"].ToString()), iMisparIshi, sCardDate, iMisparSidur, sSidurShatHatchala, lMakat, lCarNum, dPeilutShatYetiza, sPeilutShatYetiza);
    //                HttpRuntime.Cache.Remove(iMisparIshi.ToString() + DateTime.Parse(sCardDate).ToShortDateString());
    //            }
    //        }
    //        else
    //        {
    //            iResult = 1;
    //        }
    //        return iResult.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public void InsertNesiaRekaToDB(int iLoginUser, int iMisparIshi, string sCardDate, int iMisparSidur, string sSidurShatHatchala, long lMakat, long lCarNum, DateTime dPeilutShatYetiza, string sPeilutShatYetiza)
    {
        clWorkCard _workCard = new clWorkCard();

        _workCard.InsertNesiaReka(iLoginUser, iMisparIshi, sCardDate, iMisparSidur, sSidurShatHatchala, lMakat, lCarNum, dPeilutShatYetiza, sPeilutShatYetiza);        
    }

    [WebMethod(EnableSession = true)]
    public string SidurStartHourChanged(string sCardDate, int iSidurKey, string sNewStartHour, string sOrgStartHour, int iSidurIndex)
    {
        OrderedDictionary odSidurim;
        clSidur _Sidur = new clSidur();
        DateTime dSidurStartHour;
        DateTime dStartHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0,0,0);
        string sParam244 = ((KdsBatch.clParameters)(Session["Parameters"])).dShatHatchalaNahagutNihulTnua.ToShortTimeString();
        if (!sNewStartHour.Equals(string.Empty))
            dSidurStartHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(sNewStartHour.Substring(0, 2)), int.Parse(sNewStartHour.Substring(3, 2)), 0);
        else
            dSidurStartHour = dStartHour;
        
        DateTime dEndHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(sParam244.Substring(0, 2)), int.Parse(sParam244.Substring(3,2)), 0);
        
        DataTable dtUpdateSidurim = (DataTable)Session["SidurimUpdated"];
        DataRow[] dr;
        string sResult = "0,0";
        //אם סידור נהגות או ניהול ושעת ההתחלה היא בין 0 ל- פרמרטר 244, נעלה הודעה של היום הבא
        if ((dSidurStartHour >= dStartHour) && (dSidurStartHour <= dEndHour) && ((!sNewStartHour.Equals(string.Empty)))) 
        {
            sResult = "0,1";   
            dr = dtUpdateSidurim.Select("sidur_number=" + iSidurKey + " and sidur_org_start_hour='" + DateTime.Parse(sOrgStartHour).ToShortTimeString() + "'");
            if (dr.Length > 0)
            {
                if (_Sidur.bSidurMyuhad)
                {
                    if ((dr[0]["sidur_nihul_tnua"].ToString().Equals("1")) || (dr[0]["sidur_nahagut"].ToString().Equals("1")))
                        sResult = "1,1";
                }
                else
                {
                    sResult = "1,1";
                }
            }
            else
            {
                odSidurim = (OrderedDictionary)Session["Sidurim"];
                _Sidur = (clSidur)(odSidurim[iSidurIndex]);
                _Sidur.dSidurDate = DateTime.Parse(sOrgStartHour);
                if (IsNewSidurNahagutOrNihul(_Sidur))
                    sResult = "1,1";
            }           
        }

        if (sResult.Substring(0,1).Equals("0"))
            UpdateSidurDate(sCardDate, iSidurKey, sOrgStartHour, sNewStartHour, 0, iSidurIndex);
        
        //שינוי שעת התחלה - נבדוק אם צריך לפתוח/לסגור את שדה השלמה
        string sRes = (IsHashlamaAllowed(iSidurIndex, sCardDate));
        string sExecp = IsExecptionAllowed(iSidurIndex);
        sResult = sResult + "," + sRes + "," + sExecp;
        return sResult;       
    }

    [WebMethod(EnableSession = true)]
    public string UpdateShatGmar(int iSidurIndex, string sCardDate, string sShatGmar, int iAddDay)
    {
        OrderedDictionary odSidurim;
        clSidur _Sidur = new clSidur();

        odSidurim = (OrderedDictionary)Session["Sidurim"];
        _Sidur = (clSidur)(odSidurim[iSidurIndex]);
        _Sidur.dFullShatGmar = DateTime.Parse(DateTime.Parse(sCardDate).AddDays(iAddDay).ToShortDateString() + " " + sShatGmar);
        _Sidur.sShatGmar = sShatGmar;
        return IsHashlamaAllowed(iSidurIndex, sCardDate) + "," + IsExecptionAllowed(iSidurIndex);
    }
    [WebMethod(EnableSession = true)]
    public string IsHashlamaAllowed(int iSidurIndex, string sCardDate)
    {       
        DataTable dtSugSidur;
        DataRow[] drSugSidur;
        clOvedYomAvoda OvedYomAvoda = (clOvedYomAvoda)Session["OvedYomAvodaDetails"];
        OrderedDictionary odSidurim;
        clSidur _Sidur = new clSidur();

        odSidurim = (OrderedDictionary)Session["Sidurim"];
        if (odSidurim.Count > 0)
        {
            _Sidur = (clSidur)(odSidurim[iSidurIndex]);
            dtSugSidur = clDefinitions.GetSugeySidur();
            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(_Sidur.iSugSidurRagil, DateTime.Parse(sCardDate), dtSugSidur);
            return clDefinitions.IsHashlamaAllowed(ref _Sidur, drSugSidur, OvedYomAvoda) ? "1" : "0";
        }
        else
            return "0";
    }
    [WebMethod(EnableSession = true)]
    public string IsExecptionAllowed(int iSidurIndex)
    {
        clParameters _parameters = (clParameters)Session["Parameters"];
        OrderedDictionary odSidurim;
        clSidur _Sidur = new clSidur();
        string sCharigaType="";
        odSidurim = (OrderedDictionary)Session["Sidurim"];
        if (odSidurim.Count > 0)
        {
            _Sidur = (clSidur)(odSidurim[iSidurIndex]);
            return clDefinitions.IsExceptionAllowed(ref _Sidur,ref sCharigaType, _parameters) ? "1" : "0";
        }
        else
            return "0";
    }
    private bool IsNewSidurNahagutOrNihul(clSidur _Sidur)
    {
       // DataRow dr;
       // DataTable dtUpdateSidurim = (DataTable)Session["SidurimUpdated"];
        
       // bool bFound=false;
        bool bSidurNihulONahagut = false;

        //dr = dtUpdateSidurim.NewRow();
        //dr["sidur_number"] = iSidurNumber;
        //dr["sidur_org_start_hour"] = sShatHatchala;
        //dr["sidur_start_hour"] = sShatHatchala;
        //dr["sidur_date"] = DateTime.Parse(dSidurDate.ToShortDateString() + " " + sShatHatchala);
        //odSidurim = (OrderedDictionary)Session["Sidurim"];
        ////for (int i = 0; i <= odSidurim.Count - 1; i++)
        ////{
        //_Sidur = (clSidur)(odSidurim[iSidutIndex]);
            //if ((_Sidur.iMisparSidur == iSidurNumber) && (_Sidur.oSidurStatus == clSidur.enSidurStatus.enNew))
            //{
            //    bFound = true;
            //    break;
            //}
        //}

        //if (bFound)
        //{
        if ((_Sidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) ||
            (_Sidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString()))
            bSidurNihulONahagut = true;
            //if (_Sidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
            //{
            //    dr["sidur_nahagut"] = 1;
            //    bSidurNihulONahagut = true;
            //}
            //if (_Sidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString())
            //{
            //    dr["sidur_nihul_tnua"] = 1;
            //    bSidurNihulONahagut = true;
            //}
        //}
       // dtUpdateSidurim.Rows.Add(dr);
        return bSidurNihulONahagut;
    }
    [WebMethod(EnableSession = true)]
    public void UpdateSidurDate(string sCardDate, int iSidurKey, string sOldStartHour, string sNewStartHour, int iAddDay, int iSidurIndex)
    {        
        DataTable dtUpdateSidurim = (DataTable)Session["SidurimUpdated"];
        DataRow[] dr;
        DateTime dtOrgDate, dtCardDate;
        OrderedDictionary odSidurim;
        clSidur _Sidur;
        dr = dtUpdateSidurim.Select("sidur_number=" + iSidurKey + " and sidur_org_start_hour='" + DateTime.Parse(sOldStartHour).ToShortTimeString() + "'");
        if (dr.Length > 0)
        {
            dr[0]["sidur_number"] = iSidurKey;
            dr[0]["sidur_start_hour"] = sNewStartHour;
            if (DateTime.Parse(dr[0]["sidur_date"].ToString()).Year < clGeneral.cYearNull)
                //אם שעת ההתחלה ריקה, שנה 0001, נכניס את תאריך הכרטיס
                dtOrgDate = DateTime.Parse(sCardDate);
            else
                dtOrgDate = DateTime.Parse(DateTime.Parse(dr[0]["sidur_date"].ToString()).ToShortDateString());

            if (sNewStartHour.Equals(string.Empty))
                dr[0]["sidur_date"] = DateTime.Parse("01/01/001 00:00:00");
            else
            {
                dtCardDate = DateTime.Parse(sCardDate);
                if (dtOrgDate == dtCardDate)
                    dr[0]["sidur_date"] = DateTime.Parse(dtOrgDate.ToShortDateString() + " " + sNewStartHour).AddDays(iAddDay);
                else
                {
                    dr[0]["sidur_date"] = DateTime.Parse(DateTime.Parse(dr[0]["sidur_date"].ToString()).ToShortDateString() + " " + sNewStartHour);
                    if (dtOrgDate > dtCardDate)
                        if (iAddDay == 0)
                            dr[0]["sidur_date"] = ((DateTime)dr[0]["sidur_date"]).AddDays(-1);
                }
            }
        }
        else // new sidur
        {
            odSidurim = (OrderedDictionary)Session["Sidurim"];
            _Sidur = (clSidur)(odSidurim[iSidurIndex]);
            _Sidur.dSidurDate = DateTime.Parse(sCardDate).AddDays(iAddDay); //DateTime.Parse(sNewStartHour);
            _Sidur.dFullShatHatchala = DateTime.Parse(DateTime.Parse(sCardDate).AddDays(iAddDay).ToShortDateString() + " " + sNewStartHour);
            //_Sidur.dOldFullShatHatchala = _Sidur.dFullShatHatchala;
            _Sidur.sShatHatchala = _Sidur.dFullShatHatchala.ToShortTimeString();
        }
    }
    [WebMethod]
    public int MeafyenSidurMapaExists(int imisSidur, string sTaarich, int iMeafyen, int iErech)
    {
        clKavim oKavim = new clKavim();
        clUtils oUtils = new clUtils();
        DataTable PirteySidur, dtMeafyeneySidur;
        int result,sugSidur;
        string sSql;
        try
        {
            PirteySidur = oKavim.GetSidurDetailsFromTnua(imisSidur,DateTime.Parse(sTaarich), out result);
            if (PirteySidur.Rows.Count > 0)
            {
                if (PirteySidur.Rows[0]["sugsidur"].ToString() != "")
                {
                    sugSidur = int.Parse(PirteySidur.Rows[0]["sugsidur"].ToString());
                    dtMeafyeneySidur = oUtils.InitDtMeafyeneySugSidur(DateTime.Parse(sTaarich), DateTime.Parse(sTaarich));
                    sSql = "sug_sidur=" + sugSidur + " and kod_meafyen=99 and erech=1 ";
                    if (dtMeafyeneySidur.Select(sSql).Length > 0)
                        return 1;
                    else return -1;
                }
                return -2;
            }
            return -2;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public int MeafyenSidurRagilExists(int imisSidur, string sTaarich, int iMeafyen, int iErech)
    {
      clUtils oUtils = new clUtils();
      DataTable dtSidur;
      string sSql;
      try
      {
          dtSidur = oUtils.GetMeafyenSidurByKodSidur(imisSidur, sTaarich);
          if (dtSidur.Rows.Count > 0)
          {
              sSql = "kod_meafyen=99 and erech=1";
              if (dtSidur.Select(sSql).Length > 0)
                  return 1;
              else return -1;
          }
          else return 0;
      }
      catch (Exception ex)
      {
         throw ex;
      }
    }
    private bool IsSidurMyuhad(string sMisparSidur)
    {
        if (sMisparSidur.Length > 1)
            return (sMisparSidur.Substring(0, 2) == "99");
        else
        {
            return false;
        }
    }
    [WebMethod]
    public string GetNextErrorCardDate(string sMisparSidur, string dCardDate)
    {
        string sNextErrorCardDate = "";
        clWorkCard _WorkCard = new clWorkCard();

        sNextErrorCardDate = clWorkCard.GetNextErrorCard(int.Parse(sMisparSidur), DateTime.Parse(dCardDate)).ToShortDateString();

        return sNextErrorCardDate;
    }
    [WebMethod(EnableSession=true)]
    public string IsNewSidurNumberValid(int iSidurNumber, int iSidurIndex, string sSidurDate, int iMeasherMistayeg)
    {
        string sReturnCode = "0";
        DataTable dtMeafyenim= new DataTable();
        DataRow[] dr;
                
        if ((iSidurNumber.Equals(SIDUR_HITYAZVUT_A)) || (iSidurNumber.Equals(SIDUR_HITYAZVUT_B)))
            sReturnCode = "1|לא ניתן לדווח סידור התייצבות";
        else            
            {
                if (!IsSidurMyuhad(iSidurNumber.ToString()))
                {
                    sReturnCode = "1| מספר סידור שגוי";
                }
                else{
                    dtMeafyenim = (DataTable)Session["MeafyeneySidur"];
                    dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=53 or kod_meafyen=27)");
                    if (dr.Length > 0)
                        sReturnCode = "1|יש לדווח במסך הוסף דיווח היעדרות";
                    else
                    {
                        dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber);
                        if (dr.Length == 0)
                            sReturnCode = "1| מספר סידור שגוי";

                        if ((clGeneral.enMeasherOMistayeg)iMeasherMistayeg == clGeneral.enMeasherOMistayeg.ValueNull)
                        {   //אם כרטיס ללא התייחסות נבדוק שלא הקלידו סידור ללא מאפיין 99 עם ערך 1
                            dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and kod_meafyen=99 and erech='1'");
                            if (dr.Length == 0)
                                sReturnCode = "1| כרטיס ללא התייחסות, לא ניתן להוסיף סידור זה";
                        }
                    }
                }
            }
        if (sReturnCode.Equals("0"))
        {
            // XML אם מספר סידור תקין, נקרא את המאפיינים שלו וניצור מבנה
            //שמגדיר אילו מהשדות יינתנו לשינוי ואילו לא
            UpdateSidurimCash(iSidurNumber, iSidurIndex, DateTime.Parse(sSidurDate));
            sReturnCode = sReturnCode + "|" + BuildSidurFieldsDefaults(iSidurNumber,iSidurIndex, dtMeafyenim);
        }
        return sReturnCode;
    }
    public string BuildSidurFieldsDefaults(int iSidurNumber, int iSidurIndex, DataTable dtMeafyenim)
    {
        StringBuilder sXML = new StringBuilder();
        DataRow[] dr;
        clOvedYomAvoda OvedYomAvoda = (clOvedYomAvoda)Session["OvedYomAvodaDetails"];
        clMeafyenyOved _MeafyenyOved = (clMeafyenyOved)Session["MeafyenyOved"];
        clSidur _Sidur= (clSidur)(((OrderedDictionary)Session["Sidurim"])[iSidurIndex]);
      
        try
        {
            sXML.Append("<SIDUR>");
            sXML.Append(string.Concat("<SHAT_HATCHALA>", "1", "</SHAT_HATCHALA>"));
            sXML.Append(string.Concat("<SHAT_GMAR>", "1", "</SHAT_GMAR>"));

            //עבור משתמש שאינו רשמת/רשמת על/מנהל מערכת מותר לעדכן רק בסידורים מיוחדים מסוג החתמת שעון (סידורים מיוחדים עם מאפיין 54 (שעון נוכחות
            dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=54)");
            if (dr.Length > 0)
            {                
                sXML.Append(string.Concat("<DIVUCH_KNISA>", "1", "</DIVUCH_KNISA>"));
                sXML.Append(string.Concat("<DIVUCH_YETIZA>", "1", "</DIVUCH_YETIZA>"));
                sXML.Append(string.Concat("<SHAT_HATCHALA_LETASHLUM>", "1", "</SHAT_HATCHALA_LETASHLUM>"));
                sXML.Append(string.Concat("<SHAT_GMAR_LETASHLUM>", "1", "</SHAT_GMAR_LETASHLUM>"));              
            }
            else
            {
                //sXML.Append(string.Concat("<SHAT_HATCHALA>", "0", "</SHAT_HATCHALA>"));
                //sXML.Append(string.Concat("<SHAT_GMAR>", "0", "</SHAT_GMAR>"));
                sXML.Append(string.Concat("<DIVUCH_KNISA>", "0", "</DIVUCH_KNISA>"));
                sXML.Append(string.Concat("<DIVUCH_YETIZA>", "0", "</DIVUCH_YETIZA>"));
                sXML.Append(string.Concat("<SHAT_HATCHALA_LETASHLUM>", "0", "</SHAT_HATCHALA_LETASHLUM>"));
                sXML.Append(string.Concat("<SHAT_GMAR_LETASHLUM>", "0", "</SHAT_GMAR_LETASHLUM>"));
            
            }
            //פיצול הפסקה לא יינתן לעדכון עד לאחר שיעבוד שינוי קלט
            sXML.Append(string.Concat("<PITZUL_HAFSAKA>", "0", "</PITZUL_HAFSAKA>"));

            //רק לסידורים מיוחדים שיש להם ערך 1 (זכאי) במאפיין 35 (זכאות לחריגה משעות כניסה ויציאה
            dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=" + clGeneral.enMeafyenSidur35.enCharigaZakai.GetHashCode().ToString() + " and erech='1')");
            if (dr.Length > 0)            
                sXML.Append(string.Concat("<CHARIGA>", "1", "</CHARIGA>"));
            else
                //סידור מיוחד עם מאפיין 54 וערך 1 (מנהל) 
                //אם קיים מאפיין 54 עם ערך 1 יש לבדוק אם מתקיים אחד מהתנאים הבאים
                //1.לעובד אין מאפיינים 3 ו- 4 ברמה האישית
                //ג.  יום חול וערב חג
                //או
                // לעובד אין מאפיינים 5 ו- 6 ברמה האישית
                //ב.  יום שישי (שישי, לא ערב חג)
                //או 
                //לעובד אין מאפיינים 7 ו- 8 ברמה האישית
                //ב.  יום שבת/שבתון
                
               
                dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=" + clGeneral.enMeafyenim.Meafyen54.GetHashCode().ToString() + " and erech='"+ clGeneral.enMeafyenSidur54.enAdministrator.GetHashCode().ToString()+ "')");
                if (dr.Length > 0)
                {
                    if (((_Sidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) && (!_MeafyenyOved.Meafyen5Exists) && (!_MeafyenyOved.Meafyen6Exists))
                        || (((_Sidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (OvedYomAvoda.sShabaton.Equals("1"))) && ((!_MeafyenyOved.Meafyen7Exists) && (!_MeafyenyOved.Meafyen8Exists)))
                        || (((OvedYomAvoda.sErevShishiChag.Equals("1") || ((_Sidur.sSidurDay != clGeneral.enDay.Shishi.GetHashCode().ToString()) && (_Sidur.sSidurDay != clGeneral.enDay.Shabat.GetHashCode().ToString())))) && ((!_MeafyenyOved.Meafyen4Exists) && (!_MeafyenyOved.Meafyen5Exists))))

                        sXML.Append(string.Concat("<CHARIGA>", "1", "</CHARIGA>"));
                    else
                        sXML.Append(string.Concat("<CHARIGA>", "0", "</CHARIGA>"));
                }
                else
                    sXML.Append(string.Concat("<CHARIGA>", "0", "</CHARIGA>"));


             

            //ניתן לעדכון- סידור מיוחד עם ערך 1 במאפיין  - 25.
            dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=" + clGeneral.enMeafyenim.Meafyen25.GetHashCode().ToString() + " and erech='1')");
            if (dr.Length > 0)
                if ((OvedYomAvoda.iKodHevra) != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
                    sXML.Append(string.Concat("<OUT_MICHSA>", "1", "</OUT_MICHSA>"));
                else
                    sXML.Append(string.Concat("<OUT_MICHSA>", "0", "</OUT_MICHSA>"));
            else
                sXML.Append(string.Concat("<OUT_MICHSA>", "0", "</OUT_MICHSA>"));
           

            //מיוחד עם מאפיין 45-סידור ויזה            
            dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=" + clGeneral.enMeafyenim.Meafyen45.GetHashCode().ToString() + ")");
            if (dr.Length>0)
                sXML.Append(string.Concat("<SIDUR_VISA>", "1", "</SIDUR_VISA>"));
            else
                sXML.Append(string.Concat("<SIDUR_VISA>", "0", "</SIDUR_VISA>"));
            
            //אם קיים מאפיין 46 לא נאפשר הוספת פעילות
            dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=" + clGeneral.enMeafyenim.Meafyen46.GetHashCode().ToString() + ")");
            if (dr.Length==0)
                sXML.Append(string.Concat("<ADD_PEILUT>", "1", "</ADD_PEILUT>"));
            else
                sXML.Append(string.Concat("<ADD_PEILUT>", "0", "</ADD_PEILUT>"));


            //השלמה 
            //לסידור מאפיין 40 (לפי מספר סידור או סוג סידור) - ניתן לחסום את השדה אם אין מאפיין
            dr = dtMeafyenim.Select("Sidur_Key=" + iSidurNumber + " and (kod_meafyen=" + clGeneral.enMeafyenim.Meafyen40.GetHashCode().ToString() + ")");
            if ((dr.Length > 0) && ((OvedYomAvoda.iKodHevra != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())))
                sXML.Append(string.Concat("<HASHLAMA>", "1", "</HASHLAMA>"));
            else
                sXML.Append(string.Concat("<HASHLAMA>", "0", "</HASHLAMA>"));
            sXML.Append("</SIDUR>");
            return sXML.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void UpdateSidurimCash(int iSidurNumber, int iSidurIndex, DateTime dCardDate)
    {        
        clSidur _Sidur;
        DataTable dt;
        
        //Get Sidur from session
        _Sidur = (clSidur)(((OrderedDictionary)Session["Sidurim"])[iSidurIndex]);
        //Update sidurim cash with new sidur
        dt = clWorkCard.GetMeafyeneySidurById(dCardDate,iSidurNumber);
        if (dt.Rows.Count > 0)
        {
            _Sidur.oSidurStatus = clSidur.enSidurStatus.enNew;
            _Sidur.bSidurMyuhad = true;
            _Sidur.iMisparSidur = iSidurNumber;
            _Sidur.iMisparSidurMyuhad = iSidurNumber;
            _Sidur.dSidurDate = dCardDate;
            _Sidur.sHalbashKod = dt.Rows[0]["zakay_lezman_halbasha"].ToString();
            _Sidur.bHalbashKodExists = !(String.IsNullOrEmpty(dt.Rows[0]["zakay_lezman_halbasha"].ToString()));            
            _Sidur.sShatHatchalaMuteret =dt.Rows[0]["shat_hatchala_muteret"].ToString();             
            _Sidur.bShatHatchalaMuteretExists = !(String.IsNullOrEmpty( dt.Rows[0]["shat_hatchala_muteret"].ToString()));
            _Sidur.sShatGmarMuteret =dt.Rows[0]["shat_gmar_muteret"].ToString();             
            _Sidur.bShatGmarMuteretExists= !(String.IsNullOrEmpty( dt.Rows[0]["shat_gmar_muteret"].ToString()));
            _Sidur.sZakayMichutzLamichsa =  dt.Rows[0]["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
            _Sidur.sNoPeilotKod = dt.Rows[0]["sidur_asur_ledaveach_peilut"].ToString();
            _Sidur.bNoPeilotKodExists = !(String.IsNullOrEmpty(dt.Rows[0]["sidur_asur_ledaveach_peilut"].ToString()));
            _Sidur.sNoOtoNo = dt.Rows[0]["asur_ledaveach_mispar_rechev"].ToString();
            _Sidur.bNoOtoNoExists = !(String.IsNullOrEmpty(dt.Rows[0]["asur_ledaveach_mispar_rechev"].ToString()));
            _Sidur.sSidurVisaKod = dt.Rows[0]["sidur_namlak_visa"].ToString();
            _Sidur.bSidurVisaKodExists = !(String.IsNullOrEmpty(dt.Rows[0]["sidur_namlak_visa"].ToString()));
            _Sidur.sSectorAvoda =  dt.Rows[0]["sector_avoda"].ToString();
            _Sidur.bSectorAvodaExists = !String.IsNullOrEmpty( dt.Rows[0]["sector_avoda"].ToString());
            _Sidur.iSidurLoNibdakSofShavua = (System.Convert.IsDBNull( dt.Rows[0]["sidur_lo_nivdak_sofash"]) ? 0 : int.Parse( dt.Rows[0]["sidur_lo_nivdak_sofash"].ToString()));
            _Sidur.sHashlamaKod = dt.Rows[0]["zakay_lehashlama_avur_sidur"].ToString();
            _Sidur.bHashlamaKodExists = !String.IsNullOrEmpty(dt.Rows[0]["zakay_lehashlama_avur_sidur"].ToString());
            _Sidur.sZakaiLeChariga =  dt.Rows[0]["zakay_lechariga"].ToString();
            _Sidur.bZakaiLeCharigaExists = !(String.IsNullOrEmpty( dt.Rows[0]["zakay_lechariga"].ToString()));
            _Sidur.sZakaiLehamara =  dt.Rows[0]["zakay_lehamara"].ToString();
            _Sidur.bZakaiLehamaraExists = !(String.IsNullOrEmpty( dt.Rows[0]["zakay_lehamara"].ToString()));
            _Sidur.sZakayLezamanNesia =  dt.Rows[0]["Zakay_Leaman_Nesia"].ToString();
            _Sidur.bZakayLezamanNesiaExists = !(String.IsNullOrEmpty( dt.Rows[0]["zakay_leaman_nesia"].ToString()));
            _Sidur.sPeilutRequiredKod = dt.Rows[0]["hova_ledaveach_peilut"].ToString();
            _Sidur.bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dt.Rows[0]["hova_ledaveach_peilut"].ToString()));
            _Sidur.sSidurNotValidKod = dt.Rows[0]["asur_ledivuach_lechevrot_bat"].ToString();
            _Sidur.bSidurNotValidKodExists = !(String.IsNullOrEmpty(dt.Rows[0]["asur_ledivuach_lechevrot_bat"].ToString()));
            _Sidur.sSidurNotInShabtonKod = dt.Rows[0]["hukiyut_beshabaton"].ToString();
            _Sidur.bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dt.Rows[0]["hukiyut_beshabaton"].ToString()));
            _Sidur.sHeadrutTypeKod = dt.Rows[0]["sidur_misug_headrut"].ToString();
            _Sidur.bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dt.Rows[0]["sidur_misug_headrut"].ToString()));
            _Sidur.sSugAvoda =  dt.Rows[0]["sug_avoda"].ToString();
            _Sidur.bSugAvodaExists = !(String.IsNullOrEmpty( dt.Rows[0]["sug_avoda"].ToString()));
            _Sidur.bHovaMisparMachsan = !(String.IsNullOrEmpty( dt.Rows[0]["hova_ledaveach_mispar_machsan"].ToString())); 
            _Sidur.sShaonNochachut =  dt.Rows[0]["shaon_nochachut"].ToString();
            _Sidur.bShaonNochachutExists = !(String.IsNullOrEmpty(dt.Rows[0]["shaon_nochachut"].ToString()));
            _Sidur.iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull( dt.Rows[0]["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse( dt.Rows[0]["nitan_ledaveach_bmachala_aruc"].ToString());
            _Sidur.sSidurInSummer = dt.Rows[0]["sidur_kaytanot_veruey_kayiz"].ToString();
            _Sidur.bSidurInSummerExists = !(String.IsNullOrEmpty(dt.Rows[0]["sidur_kaytanot_veruey_kayiz"].ToString())); 
            _Sidur.sLoLetashlumAutomati =  dt.Rows[0]["lo_letashlum_automati"].ToString();
            _Sidur.bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty( dt.Rows[0]["lo_letashlum_automati"].ToString()));
            _Sidur.iZakayLepizul = (System.Convert.IsDBNull( dt.Rows[0]["zakay_lepizul"]) ? 0 : int.Parse( dt.Rows[0]["zakay_lepizul"].ToString()));
            _Sidur.sLidroshKodMivtza =  dt.Rows[0]["lidrosh_kod_mivtza"].ToString();
            _Sidur.sRashaiLedaveach =  dt.Rows[0]["rashai_ledaveach"].ToString();
            _Sidur.bRashaiLedaveachExists = !(String.IsNullOrEmpty( dt.Rows[0]["rashai_ledaveach"].ToString()));
		    _Sidur.sKizuzAlPiHatchalaGmar =  dt.Rows[0]["kizuz_al_pi_hatchala_gmar"].ToString();
            _Sidur.bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty( dt.Rows[0]["kizuz_al_pi_hatchala_gmar"].ToString()));
            _Sidur.sNitanLedaveachAdTaarich =  dt.Rows[0]["nitan_ledaveach_ad_taarich"].ToString();
            _Sidur.bNitanLedaveachAdTaarichExists = !(String.IsNullOrEmpty( dt.Rows[0]["nitan_ledaveach_ad_taarich"].ToString()));
            _Sidur.iElement1Hova = (System.Convert.IsDBNull( dt.Rows[0]["element1_hova"]) ? 0 : int.Parse( dt.Rows[0]["element1_hova"].ToString()));
            _Sidur.iElement2Hova = (System.Convert.IsDBNull( dt.Rows[0]["element2_hova"]) ? 0 : int.Parse( dt.Rows[0]["element2_hova"].ToString()));
            _Sidur.iElement3Hova = (System.Convert.IsDBNull( dt.Rows[0]["element3_hova"]) ? 0 : int.Parse( dt.Rows[0]["element3_hova"].ToString()));
            _Sidur.iSugSidurRagil = System.Convert.IsDBNull(dt.Rows[0]["sug_sidur"]) ? 0 : int.Parse(dt.Rows[0]["sug_sidur"].ToString());         
        }
    }
    //[WebMethod(EnableSession = true)]
    //public string ChkIfSidurNahagut(int iSidurIndex, string sCardDate)
    //{
    //    string sReturn = "0";
    //    OrderedDictionary htSidurim = (OrderedDictionary)(Session["Sidurim"]);
    //    clSidur _Sidur = (clSidur)htSidurim[iSidurIndex];
    //    DataTable dtSugeySidur = clDefinitions.GetSugeySidur();
    //    DataRow[] drSugSidur;
    //    bool bSidurDriver=false;
    //    try
    //    {
    //        //נשלוף את מאפייני סוג הסידור ( סידורים רגילים(
    //        drSugSidur = clDefinitions.GetOneSugSidurMeafyen(_Sidur.iSugSidurRagil, DateTime.Parse(sCardDate), dtSugeySidur);
    //        if (drSugSidur.Length > 0)
    //            bSidurDriver = String.IsNullOrEmpty(drSugSidur[0]["sector_avoda"].ToString()) ? false : (int.Parse(drSugSidur[0]["sector_avoda"].ToString()) == clGeneral.enSectorAvoda.Nahagut.GetHashCode());
    //        //אם הסידור הראשון הוא סידור נהגות, נבדוק את הסידור הבא
    //        if (bSidurDriver)
    //        {
    //            _Sidur = (clSidur)htSidurim[iSidurIndex+1];
    //            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(_Sidur.iSugSidurRagil, DateTime.Parse(sCardDate), dtSugeySidur);
    //            if (drSugSidur.Length > 0)
    //            {
    //                bSidurDriver = String.IsNullOrEmpty(drSugSidur[0]["sector_avoda"].ToString()) ? false : (int.Parse(drSugSidur[0]["sector_avoda"].ToString()) == clGeneral.enSectorAvoda.Nahagut.GetHashCode());
    //                if (bSidurDriver)                    
    //                    sReturn = "1"; //שני הסידורים הם מסוג נהגות                    
    //            }
    //        }
    //        return sReturn;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public string GetWorkCardErrors(string sXML)
    //{
    //    string sResult = "";
    //    //XmlDataDocument xmlDoc = new XmlDataDocument();
    //    //XmlNode _DayDetails;
    //    //xmlDoc.LoadXml(sXML);
    //    //_DayDetails = xmlDoc.FirstChild.FirstChild;
    //    //for (int i = 0; i < _DayDetails.ChildNodes.Count; i++)
    //    //{
    //    //    switch (_DayDetails.ChildNodes.Name)
    //    //    {
    //    //        case "hashlama":
    //    //            if ((_DayDetails.ChildNodes[i].Attributes[0] == -1) && (_DayDetails.ChildNodes[i].Attributes[1] == 1))
    //    //            {
    //    //                sResult = "סומנה השלמה ליום, יש לדווח סיבה";
    //    //            }
    //    //            break;
    //    //        case "part1": //התייצבות ראשונה
    //    //            if ((_DayDetails.ChildNodes[i].Attributes[0] == -1) && (_DayDetails.ChildNodes[i].Attributes[1].indexOf(":") > -1))
    //    //            {
    //    //                sResult = sResult.concat("\n דווחה שעת התייצבות ראשונה, יש לדווח סיבה");
    //    //            }
    //    //            break;
    //    //        case "part2"://התייצבות שנייה
    //    //            if ((_DayDetails.ChildNodes[i].Attributes[0] == -1) && (_DayDetails.ChildNodes[i].Attributes[1].indexOf(":") > -1))
    //    //            {
    //    //                sResult = "1";
    //    //            }
    //    //            break;
    //    //    }
    //    //}

    //    return sResult;
    //}
}

