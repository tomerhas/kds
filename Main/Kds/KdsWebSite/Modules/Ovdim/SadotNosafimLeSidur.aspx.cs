using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using KdsLibrary.UI;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.UDT;
using KdsBatch;
using KdsLibrary.Security;


public partial class Modules_Ovdim_SadotNosafimLeSidur : KdsPage
{
    clBatchManager oBatchManager;
    private DataTable _ErrorsList;
    public enum enKodSade
    {
        Mispar_Musach_O_Machsan=1,
        Yom_Visa,
        Achuz_Knas_LePremyat_Visa,
        Achuz_Viza_Besikun,
        Sug_Hazmanat_Visa,
        Mispar_shiurey_nehiga,
        Tafkid_Visa,
        Mivtza_Visa,
        Lina,
        Km_visa,
        Mispar_Visa,
        Kod_shinuy_premia,
        Mispar_Siduri_Oto
    }
    public DataTable ErrorsList
    {
        set
        {
            _ErrorsList = value;
        }
        get
        {
            return oBatchManager.dtErrors;// _ErrorsList;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtSadotNosafimLesidur = new DataTable();
        DataTable dtSadotNosafimLePeilut = new DataTable(); 
        DataTable dtParametrim = new DataTable();
        DataTable dtPeiluyotLesidur = new DataTable();
        DataTable dtMeafyenim = new DataTable();
        string listMeafyenim = "";
        clUtils oUtils = new clUtils();
        clOvdim oOvdim = new clOvdim();
        clKavim oKavim = new clKavim();
        DataRow[] drs;
        int erech;
        try
        {
            ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
            if (!Page.IsPostBack)
            {
               
                SlifatQueryString();

                dtParametrim = oUtils.getErechParamByKod("141,149", clnDate.Value);
                for (int i = 0; i < dtParametrim.Rows.Count; i++)
                    Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());

                if (MisSidur.Value.Substring(0, 1) == "9")
                {
                    listMeafyenim = getMeafyenimSadotNosafimLesidurMeyuchad(clnDate.Value, int.Parse(MisSidur.Value));
                    if (listMeafyenim.IndexOf("45") > -1)
                    {
                        if ((DateTime.Parse(ShatHatchala.Value) < DateTime.Parse(clnDate.Value + " 23:59:00")) &&
                            (DateTime.Parse(ShatGmar.Value) > DateTime.Parse(clnDate.Value + " 06:00:00").AddDays(1)))
                            lblPizulSidur.Visible = true;
                    }
                }
                else
                    listMeafyenim = getMeafyenimSadotNosafimLesidurRagil(DateTime.Parse(clnDate.Value), int.Parse(MisSidur.Value));
                if (listMeafyenim == "")
                    listMeafyenim = "-1";
                dtSadotNosafimLesidur = oUtils.GetSadotNosafimLesidur(int.Parse(MisSidur.Value), listMeafyenim);
                dtMeafyenim = (DataTable)Session["dtMeafyenim"];
                for (int i = dtSadotNosafimLesidur.Rows.Count-1; i >= 0; i--)
                {
                    erech = int.Parse(dtSadotNosafimLesidur.Rows[i]["ERECH"].ToString());
                    if (erech > 0)
                    {
                        drs = dtMeafyenim.Select("KOD_MEAFYEN=" + dtSadotNosafimLesidur.Rows[i]["KOD_MEAFYEN"] + " and ERECH=" + erech.ToString());
                        if (drs.Length == 0)
                            dtSadotNosafimLesidur.Rows.RemoveAt(i);
                    }
                }
               
                dtSadotNosafimLePeilut = oUtils.GetSadotNosafimLePeilut();
                dtPeiluyotLesidur = oOvdim.GetPeiluyotLeOved(int.Parse(txtId.Value), int.Parse(MisSidur.Value),DateTime.Parse(clnDate.Value), DateTime.Parse(ShatHatchala.Value));
                Session["dtMakatimCatalog"] = oKavim.GetKatalogKavim(int.Parse(txtId.Value), DateTime.Parse(clnDate.Value), DateTime.Parse(clnDate.Value));
           //     cheackEilatTrip(ref dtSadotNosafimLesidur, dtPeiluyotLesidur);
                Session["dtPeiluyot"] = dtPeiluyotLesidur;
                Session["dtSadotNosafimLesidur"] = dtSadotNosafimLesidur;
                Session["dtSadotNosafimLePeilut"] = dtSadotNosafimLePeilut;

               
            }
            oBatchManager = new clBatchManager(int.Parse(txtId.Value), DateTime.Parse(clnDate.Value));
            oBatchManager.MainOvedErrors(int.Parse(txtId.Value), DateTime.Parse(clnDate.Value));
            ErrorsList = oBatchManager.dtErrors;
            BuildPirteySidur();
            BuildPeiluyot();
        }

        catch (Exception ex)
        {
            throw ex;
            //clGeneral.BuildError(Page, ex.Message);
        }
    }
    private void SlifatQueryString()
    {
        try
        {// 26/05/2009,19813,99100,12:00,16:00
            if (Request.QueryString["CardDate"] != null)
            {
                clnDate.Value = Request.QueryString["CardDate"].ToString(); //"08/03/2010";//"26/05/2009"; //               
            }
            if (Request.QueryString["EmpID"] != null)
            {
                txtId.Value = Request.QueryString["EmpID"].ToString(); //"65099";//
            }
            if (Request.QueryString["SidurID"] != null)
            {
                MisSidur.Value = Request.QueryString["SidurID"].ToString();//"99100";//"49004"; //
            }
            if (Request.QueryString["ShatHatchala"] != null)
            {
                ShatHatchala.Value = Request.QueryString["ShatHatchala"].ToString();//"08/03/2010 06:24";//                 
            }
            if (Request.QueryString["ShatGmar"] != null && Request.QueryString["ShatGmarDate"] != null)
            {
                ShatGmar.Value = Request.QueryString["ShatGmarDate"].ToString() + " " + Request.QueryString["ShatGmar"].ToString(); //"08/03/2010 15:35";//                
            }
            if (Request.QueryString["SidurDate"] != null)
            {
                txtSidurDate.Value = Request.QueryString["SidurDate"].ToString(); //"08/03/2010";//"26/05/2009"; //                
            }

            Session["dtMakatimCatalog"] = null;
            Session["dtPeiluyot"] = null;
            Session["dtSadotNosafimLesidur"] = null;
            Session["dtSadotNosafimLePeilut"] = null;
        }
        catch (Exception ex)
        {
            throw ex;           
        }
    }
    private string getMeafyenimSadotNosafimLesidurMeyuchad(string TaarichCA, int Sidur) 
    {
        DataTable dtMeafyenim = new DataTable();
        clUtils oUtils = new clUtils();
        string listMeafyenim="";
        try
        {
            dtMeafyenim = oUtils.GetMeafyenSidurByKodSidur(Sidur, TaarichCA);
            Session["dtMeafyenim"] = dtMeafyenim;
            foreach (DataRow dr in dtMeafyenim.Rows)
                listMeafyenim += dr["KOD_MEAFYEN"] + ",";
            if (listMeafyenim.Length > 0)
                listMeafyenim = listMeafyenim.Substring(0, listMeafyenim.Length - 1);
            return listMeafyenim;
        }
        catch (Exception ex)
        {
            throw ex;
        }    
    }
    private string getMeafyenimSadotNosafimLesidurRagil(DateTime TaarichCA, int Sidur) 
    {
        DataTable dtMeafyenim = new DataTable();
        DataSet dsSidur = new DataSet();
        clUtils oUtils = new clUtils();
        clKavim oKavim = new clKavim();
        string listMeafyenim = "";
        int result;
      //  int sugSidur;
        DataRow[] drSelect;
        string sSQL = "";
        try
        {
            dsSidur = oKavim.GetSidurAndPeiluyotFromTnua(Sidur, TaarichCA,null, out result);
            if (result == 0)
            {
             //   sugSidur = int.Parse(dsSidur.Tables[0].Rows[0]["SUGSIDUR"].ToString());
                dtMeafyenim = oUtils.InitDtMeafyeneySugSidur(TaarichCA, TaarichCA);
                sSQL = "SUG_SIDUR ='" + dsSidur.Tables[0].Rows[0]["SUGSIDUR"].ToString() +"'";
                drSelect = dtMeafyenim.Select(sSQL); 
                foreach (DataRow dr in drSelect)
                    listMeafyenim += dr["KOD_MEAFYEN"] + ",";
                if (listMeafyenim.Length > 0)
                    listMeafyenim = listMeafyenim.Substring(0, listMeafyenim.Length - 1);

                for (int i = dtMeafyenim.Rows.Count-1; i >= 0; i--)
                {
                    if (dtMeafyenim.Rows[i]["SUG_SIDUR"].ToString() != dsSidur.Tables[0].Rows[0]["SUGSIDUR"].ToString())
                        dtMeafyenim.Rows.RemoveAt(i);
                }
                Session["dtMeafyenim"] = dtMeafyenim;
            }
            return listMeafyenim;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void cheackEilatTrip(ref DataTable dtSadotNosafimLesidur, DataTable dtPeiluyotLesidur)
    {
        DataRow[] drSelect;
        string sSQL = "";
        int makat;
        int iOnatiyut;
        bool bEilatTrip;
        try
        {
            sSQL = "KOD_SADE_IN_MASACH='" + (int)enKodSade.Lina + "'";
            drSelect = dtSadotNosafimLesidur.Select(sSQL);
            if (drSelect.Length == 0)
            {
                foreach (DataRow dr in dtPeiluyotLesidur.Rows)
                {
                    makat =int.Parse(dr["MAKAT_NESIA"].ToString());

                    if (CheckKavSherut(makat, out bEilatTrip, out iOnatiyut))
                    {
                        if (bEilatTrip)
                        {
                            DataRow drLina = dtSadotNosafimLesidur.NewRow();
                            drLina["KOD_SADE_IN_MASACH"] = "9";
                            drLina["BRAT_MECHDAT"] = "0";
                            drLina["SADE_CHOVA"] = "0";
                            drLina["SUS_SADE_IN_MASACH"] = "cmb";
                            drLina["STORE_PROCEDUR"] = "PKG_SIDURIM.pro_get_lina";
                            drLina["SHEM_SADE_IN_MASACH"] = "לינה";
                            drLina["MIKUM_SADE_IN_DB"] = "TB_Yamey_Avoda_Ovdim.Lina";
                            dtSadotNosafimLesidur.Rows.Add(drLina);
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private bool CheckKavSherut(int makat,out bool bEilatTrip,out int iOnatiyut)
    {
        clKavim oKavim = new clKavim();
        DataTable dtKavim = new DataTable();
        clKavim.enMakatType MakatType;
        decimal km=0;
         iOnatiyut = 0;
      //  int iMakatValid;
        bool flag = false;
        DataRow[] drSelect;
        string sSQL = "";
        bEilatTrip = false;
        try{
              //  makat = int.Parse(dr["MAKAT_NESIA"].ToString());
                MakatType = (clKavim.enMakatType)oKavim.GetMakatType(makat);

                switch (MakatType)
                {
                    case clKavim.enMakatType.mKavShirut:
                        dtKavim =(DataTable)Session["dtMakatimCatalog"]; // oKavim.GetKavimDetailsFromTnuaDT(makat, DateTime.Parse(clnDate.Value), out iMakatValid);
                        sSQL = "MAKAT8=" + makat +" AND ACTIVITY_DATE='" + clnDate.Value +"'";
                        drSelect = dtKavim.Select(sSQL);
                        if (drSelect.Length > 0)
                        {
                            if (drSelect[0]["KM"].ToString() != "")
                                km = decimal.Parse(drSelect[0]["KM"].ToString());
                            if (drSelect[0]["EILAT_TRIP"].ToString() == "1") // && km > int.Parse(Params.Attributes["Param149"].ToString()))
                                bEilatTrip = true;
                            if (drSelect[0]["ONATIUT"].ToString() != "")
                                iOnatiyut = int.Parse(drSelect[0]["ONATIUT"].ToString());
                            //if (dtKavim.Rows.Count > 0){
                            //if (dtKavim.Rows[0]["KM"].ToString() != "")
                            //    km = decimal.Parse(dtKavim.Rows[0]["KM"].ToString());
                            //if (dtKavim.Rows[0]["EILATTRIP"].ToString() == "1" && km > int.Parse(Params.Attributes["Param149"].ToString()))
                            //    flag = true;
                        }
                        flag = true;
                        break;
                }

               
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flag;
    }
    private void BuildPirteySidur()
    {
        HtmlTable oTbl = new HtmlTable();
        HtmlTableRow oTr = new HtmlTableRow();
        DataTable dtSadotNosafimLesidur = new DataTable();
        DataRow dr;
        DataTable dtSadotKayamim = new DataTable();
        clUtils oUtils = new clUtils();
        try
        {
            dtSadotNosafimLesidur = (DataTable)Session["dtSadotNosafimLesidur"];
            dtSadotKayamim = oUtils.GetSadotNosafimKayamim(int.Parse(txtId.Value),
                         int.Parse(MisSidur.Value), DateTime.Parse(clnDate.Value), DateTime.Parse(ShatHatchala.Value));
            if (dtSadotNosafimLesidur.Rows.Count == 0 || dtSadotKayamim.Rows.Count == 0)
            {
                lblEinSadotLesidur.Visible = true;
                Session["dtSadotKayamim"] = null;
            }
            else
            {
                Session["dtSadotKayamim"] = dtSadotKayamim;
                for (int i = 0; i < dtSadotNosafimLesidur.Rows.Count; i++)
                {
                    dr = dtSadotNosafimLesidur.Rows[i];
                    if (i > 0 && i % 2 == 0)
                        oTr = new HtmlTableRow();

                    BuildRowSidur(ref oTr, dr, dtSadotKayamim.Rows[0]);

                    if (i > 0 && i % 2 != 0)
                        oTbl.Rows.Add(oTr);
                }
                //הוספת שורה אחרונה במספר אי זוגי
                if (dtSadotNosafimLesidur.Rows.Count % 2 != 0)
                    oTbl.Rows.Add(oTr);

                fieldsetSidur.Controls.Add(oTbl);
                Session["Table"] = oTbl;
            }
            
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void BuildRowSidur(ref HtmlTableRow oTr, DataRow dr, DataRow drSadotKayamim)
    {
        HtmlTableCell oCell = new HtmlTableCell();
        HtmlTableCell oCellImg = new HtmlTableCell();
        HtmlImage imgErr = new HtmlImage();
        try
        {
         
            //שם השדה
            oCell.InnerHtml = dr["SHEM_SADE_IN_MASACH"].ToString() + ":";
            //   oCell.Attributes.Add("class", "TxtBold");
            oCell.Align = "right";       
            oTr.Cells.Add(oCell);

            //האובייקט
            oCell = new HtmlTableCell();        
            oCell.Width = "180px";
            BuildObjectSidur(ref oCell, dr, drSadotKayamim, ref imgErr); //, dtSadotKayamim);
            oCellImg.Width = "5px";
            if (imgErr.Style["Display"] == "block")
            {
                 oCellImg.Controls.Add(imgErr);
            }
            oTr.Cells.Add(oCellImg);
            oTr.Cells.Add(oCell);
         
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void BuildObjectSidur(ref HtmlTableCell oCell, DataRow dr, DataRow drSadotKayamim,ref HtmlImage imgErr)
    {
        string sugObject;
        string kodObject;
      //  bool bErrorExists;
        clSidur oSidur = new clSidur();//int.Parse(MisIshi.Value), DateTime.Parse(TaarichCA.Value), int.Parse(MisSidur.Value),null);       
        DropDownList cmb = new DropDownList();
        TextBox txt = new TextBox();
        CustomValidator vldCustomValidator = new CustomValidator();
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut = new AjaxControlToolkit.ValidatorCalloutExtender();
        string defult;
        bool bErrorExists = false;
        
        imgErr.Src = "../../Images/ErrorSign.jpg";
        try
        {
           kodObject= dr["KOD_SADE_IN_MASACH"].ToString();
            switch ((enKodSade)Enum.Parse(typeof(enKodSade), kodObject))
            {
  

                case enKodSade.Mispar_Musach_O_Machsan:
                    imgErr.ID = "imgMispar_Musach_O_Machsan";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(cmb_Mispar_Musach_O_Machsan,2,'');");
                   
                    cmb.ID = "cmb_Mispar_Musach_O_Machsan";
                    Fill_Mispar_Musach_O_Machsan(ref cmb);
                    BulidObjectCmb(ref cmb, dr);
                    AddValidSadeChova(dr, cmb.ID, ref vldCustomValidator, ref vldExtenderCallOut,AjaxControlToolkit.ValidatorCalloutPosition.Left);
                    if (drSadotKayamim["Mispar_Musach_O_Machsan"].ToString() != "")
                        cmb.SelectedValue= drSadotKayamim["Mispar_Musach_O_Machsan"].ToString();

                    bErrorExists =SetOneError(cmb, oCell, "Mispar_Musach_O_Machsan", ref oSidur, "0", "");//iIndex.ToString(), "");
                    ErrorImage(imgErr, bErrorExists);
                    break;
                case enKodSade.Yom_Visa:
                    imgErr.ID = "imgYom_Visa";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(cmb_Yom_Visa,2,'');");
                   
                    cmb.ID = "cmb_Yom_Visa";
                    BulidObjectCmb(ref cmb, dr);
                    AddValidSadeChova(dr, cmb.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Left);
                    if (drSadotKayamim["Yom_Visa"].ToString() != "")
                        cmb.SelectedValue = drSadotKayamim["Yom_Visa"].ToString();

                    bErrorExists = SetOneError(cmb, oCell, "Yom_Visa", ref oSidur, "0", "");//iIndex.ToString(), "");
                    ErrorImage(imgErr, bErrorExists);
                    break;
                case enKodSade.Achuz_Knas_LePremyat_Visa:
                    txt.ID = "txt_Achuz_Knas_LePremyat_Visa";
                    BulidObjectTextBox(ref txt, dr);
                    AddValidSadeChova(dr, txt.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    if (drSadotKayamim["Achuz_Knas_LePremyat_Visa"].ToString() != "")
                        txt.Text = drSadotKayamim["Achuz_Knas_LePremyat_Visa"].ToString();
                  //  txt.Attributes.Add("onFocus", "document.getElementById('" + txt.ID + "').select()");
                    bErrorExists = SetOneError(txt, oCell, "Achuz_Knas_LePremyat_Visa", ref oSidur, "0", "");//iIndex.ToString(), "");
                    break;
                case enKodSade.Achuz_Viza_Besikun:
                    txt.ID = "txt_Achuz_Viza_Besikun";
                    BulidObjectTextBox(ref txt, dr);
                    AddValidSadeChova(dr, txt.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Left);
                    if (drSadotKayamim["Achuz_Viza_Besikun"].ToString() != "")
                        txt.Text = drSadotKayamim["Achuz_Viza_Besikun"].ToString();
                  //  txt.Attributes.Add("onFocus", "document.getElementById('" + txt.ID + "').select()");
                    bErrorExists = SetOneError(txt, oCell, "Achuz_Viza_Besikun", ref oSidur, "0", "");//iIndex.ToString(), "");
                    break;
                case enKodSade.Sug_Hazmanat_Visa:
                    imgErr.ID = "imgSug_Hazmanat_Visa";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(cmb_Sug_Hazmanat_Visa,2,'');");
                   
                    cmb.ID = "cmb_Sug_Hazmanat_Visa";
                    BulidObjectCmb(ref cmb, dr);
                    AddValidSadeChova(dr, cmb.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    if (drSadotKayamim["Sug_Hazmanat_Visa"].ToString() != "")
                        cmb.SelectedValue = drSadotKayamim["Sug_Hazmanat_Visa"].ToString();

                    bErrorExists = SetOneError(cmb, oCell, "Sug_Hazmanat_Visa", ref oSidur, "0", "");//iIndex.ToString(), "");
                    ErrorImage(imgErr, bErrorExists);
                    break;
                case enKodSade.Mispar_shiurey_nehiga:
                    txt.ID = "txt_Mispar_shiurey_nehiga";
                    BulidObjectTextBox(ref txt, dr);
                    AddValidSadeChova(dr, txt.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    txt.Attributes.Add("MAX", Params.Attributes["Param141"]);
                    if (drSadotKayamim["Mispar_shiurey_nehiga"].ToString() != "")
                        txt.Text = drSadotKayamim["Mispar_shiurey_nehiga"].ToString();
                  //  txt.Attributes.Add("onFocus", "document.getElementById('" + txt.ID + "').select()");
                    bErrorExists = SetOneError(txt, oCell, "Mispar_shiurey_nehiga", ref oSidur, "0", "");//iIndex.ToString(), "");
                    break;
                case enKodSade.Tafkid_Visa:
                    imgErr.ID = "imgTafkid_Visa";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(cmb_Tafkid_Visa,2,'');");
                   
                    cmb.ID = "cmb_Tafkid_Visa";
                    fill_tafkid_viza(ref cmb);
                    BulidObjectCmb(ref cmb, dr);
                    AddValidSadeChova(dr, cmb.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    if (drSadotKayamim["Tafkid_Visa"].ToString() != "")
                        cmb.SelectedValue = drSadotKayamim["Tafkid_Visa"].ToString();

                    bErrorExists = SetOneError(cmb, oCell, "Tafkid_Visa", ref oSidur, "0", "");//iIndex.ToString(), "");
                    ErrorImage(imgErr, bErrorExists);
                    break;
                case enKodSade.Mivtza_Visa:
                    imgErr.ID = "imgMivtza_Visa";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(cmb_Mivtza_Visa,2,'');");
                   
                    cmb.ID = "cmb_Mivtza_Visa";
                    BulidObjectCmb(ref cmb, dr);
                    AddValidSadeChova(dr, cmb.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Left);
                    if (drSadotKayamim["Mivtza_Visa"].ToString() != "")
                        cmb.SelectedValue = drSadotKayamim["Mivtza_Visa"].ToString();

                    bErrorExists = SetOneError(cmb, oCell, "Mivtza_Visa", ref oSidur, "0", "");//iIndex.ToString(), "");
                    ErrorImage(imgErr, bErrorExists);
                    break;
                case enKodSade.Lina:
                    imgErr.ID = "imgLina";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(cmb_Lina,2,'');");
                   
                    cmb.ID = "cmb_Lina";
                    BulidObjectCmb(ref cmb, dr);
                    AddValidSadeChova(dr, cmb.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    if (drSadotKayamim["Lina"].ToString() != "")
                        cmb.SelectedValue = drSadotKayamim["Lina"].ToString();

                    bErrorExists =  SetOneError(cmb, oCell, "Lina", ref oSidur, "0", "");//iIndex.ToString(), "");
                    ErrorImage(imgErr, bErrorExists);
                    break;
                case enKodSade.Km_visa:
                    imgErr.ID = "imgKm_visa";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(txt_Km_visa,2,'');");
                   
                    txt.ID = "txt_Km_visa";
                    BulidObjectTextBox(ref txt, dr);
                    AddValidSadeChova(dr, txt.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    checkMaximumKMLesidur(dr, ref txt, "Km_visa");
                    defult = getDefaultFieldFromPeiluyot("Km_visa"); //, txt, oCell,null);
                    if (defult != "")
                        txt.Text = defult;
                  //  txt.Attributes.Add("onFocus", "document.getElementById('" + txt.ID + "').select()");
                    bErrorExists = SetOneError(txt, oCell, "Km_visa", ref oSidur, "0", "");//iIndex.ToString(), "");
                  //  ErrorImage(imgKmVisa, bErrorExists);
                    break;

                case enKodSade.Mispar_Visa:
                    imgErr.ID = "imgMispar_Visa";
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(txt_Mispar_Visa,2,'');");
                  
                    txt.ID = "txt_Mispar_Visa";
                    BulidObjectTextBox(ref txt, dr);
                    AddValidSadeChova(dr, txt.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Left);
                    txt.Attributes.Add("MaxLength","10");
                    defult = getDefaultFieldFromPeiluyot("Mispar_Visa");//,txt,oCell,null);
                    if (defult != "")
                        txt.Text = defult;
                   // txt.Attributes.Add("onFocus", "document.getElementById('" + txt.ID + "').select()");
                    SetOneError(txt, oCell, "Mispar_Visa", ref oSidur, "0", "");//iIndex.ToString(), "");
                    break;
            }
            sugObject = dr["SUS_SADE_IN_MASACH"].ToString();
            if (sugObject == "cmb")
                oCell.Controls.Add(cmb);
            else if (sugObject == "number")
                oCell.Controls.Add(txt);
            //if (bErrorExists)
            //    oCell.Controls.AddAt(1,imgErr);
            oCell.Controls.Add(vldCustomValidator);
            oCell.Controls.Add(vldExtenderCallOut);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private string getDefaultFieldFromPeiluyot(string nameField)//, Control oObj, HtmlTableCell oCell, HtmlImage imgErr)
    {
        string erech = "";
        DataTable dtPeiluyot = new DataTable();
        int i = 0;
       // string shatYezia, mispar_knisa;
       // bool bErrorExists = false;
        try
        {
            
            dtPeiluyot = (DataTable)Session["dtPeiluyot"];
            foreach (DataRow drPeilut in dtPeiluyot.Rows)
            {
                if (drPeilut["MAKAT_NESIA"].ToString().Substring(0, 1) == "5")
                {
                    switch ((enKodSade)Enum.Parse(typeof(enKodSade), nameField))
                    {
                        case enKodSade.Km_visa:
                        case enKodSade.Mispar_Visa:
                            erech = drPeilut[nameField].ToString();
                            HiddenSdotPeilutBeramatSidur.Attributes.Add(nameField, i.ToString());
                            break;
                    }
                }
              /*  else if (drPeilut[nameField].ToString() != "")
                {
                    HiddenSdotPeilutBeramatSidur.Attributes.Add(nameField, i.ToString());
                    erech = drPeilut[nameField].ToString();
                    //shatYezia = drPeilut["SHAT_YETZIA"].ToString();
                    // mispar_knisa = drPeilut["MISPAR_KNISA"].ToString();
                    ////bErrorExists = SetOneError(oObj, oCell, int.Parse(txtId.Value), DateTime.Parse(clnDate.Value),
                    ////                             int.Parse(MisSidur.Value), DateTime.Parse(ShatHatchala.Value).ToShortTimeString(),
                    ////                             DateTime.Parse(shatYezia), int.Parse(mispar_knisa),
                    ////                             DateTime.Parse(shatYezia).ToShortTimeString() + "|" + mispar_knisa, nameField);
                    ////if (imgErr != null)
                    ////    ErrorImage(imgErr, bErrorExists);
                    break;
                }*/
                 
                i++;
            }
            return erech;
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BulidObjectCmb(ref DropDownList cmb, DataRow dr)
    {
        clUtils oUtils = new clUtils();
        DataTable Dt = new DataTable();
               try
        {
            if (dr["STORE_PROCEDUR"].ToString() != "")
            {
                Dt = oUtils.GetDataToCmbBySPname(dr["STORE_PROCEDUR"].ToString());
                cmb.DataSource = Dt;
                cmb.DataTextField = Dt.Columns["description"].ToString();
                cmb.DataValueField = Dt.Columns["code"].ToString();
                cmb.DataBind();
            }
            
            cmb.Width =140;
            
            ListItem item = new ListItem();
            item.Text = "";
            item.Value = "-1";
            cmb.Items.Add(item);
            cmb.SelectedValue = "-1";

            if (dr["BRAT_MECHDAT"].ToString() != "")
                cmb.SelectedValue = dr["BRAT_MECHDAT"].ToString();
          

        }
        catch (Exception ex)
        {
            throw ex;
           //clGeneral.BuildError(Page, ex.Message);
        }
    }
    private void AddValidSadeChova(DataRow dr, string ObjId, ref CustomValidator vldCustomValidator, 
                                    ref AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut,
                                    AjaxControlToolkit.ValidatorCalloutPosition side)
    {
                vldCustomValidator.ID = "vld_" + ObjId;
                vldCustomValidator.ErrorMessage =   " חובה להזין " + dr["SHEM_SADE_IN_MASACH"].ToString();
                vldCustomValidator.ControlToValidate = ObjId;
                vldCustomValidator.Display = ValidatorDisplay.None;

                vldExtenderCallOut.TargetControlID = vldCustomValidator.ID;
                vldExtenderCallOut.ID = "vldEx_" + ObjId;
                vldExtenderCallOut.PopupPosition = side; // AjaxControlToolkit.ValidatorCalloutPosition.Right;        
    }
    private void checkMaximumKMLesidur(DataRow dr, ref TextBox txt, string nameField)
    {
        int i = 0;
        DataTable dtPeiluyot = new DataTable();
        DateTime startDate = new DateTime();
        DateTime endDate = new DateTime();
        double  DakotPeilut;
        TimeSpan tmpDate = new TimeSpan();
        try
        {
             dtPeiluyot = (DataTable)Session["dtPeiluyot"];
             if (dtPeiluyot.Rows.Count > 0)
             {
                 for (i = 0; i < dtPeiluyot.Rows.Count; i++)
                 {
                     if (dtPeiluyot.Rows[i]["MAKAT_NESIA"].ToString().Substring(0,1) == "5")
                     {
                         if (i < dtPeiluyot.Rows.Count - 1)
                         {
                             startDate = DateTime.Parse(dtPeiluyot.Rows[i]["SHAT_YETZIA"].ToString());
                             endDate = DateTime.Parse(dtPeiluyot.Rows[i + 1]["SHAT_YETZIA"].ToString());
                         }
                         break;
                     }
                 }

                 if ((i+1) == dtPeiluyot.Rows.Count)
                 {
                     startDate = DateTime.Parse(dtPeiluyot.Rows[i]["SHAT_YETZIA"].ToString());
                     endDate = DateTime.Parse(ShatGmar.Value);
                 }

                 //חישוב ק''מ
                 tmpDate = (endDate - startDate);
                 DakotPeilut = (tmpDate.Hours * 60 + tmpDate.Minutes + tmpDate.Seconds / 60) * 1.7;
                   txt.Attributes.Add("MAX", Math.Round(DakotPeilut).ToString());
             }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BulidObjectTextBox(ref TextBox txt, DataRow dr)
    {
        RequiredFieldValidator oVldRequired = new RequiredFieldValidator();
        txt.Width = 136;
        txt.Style[HtmlTextWriterStyle.TextAlign] = "right";
        try
        {
            txt.Text = "";
            if (dr["BRAT_MECHDAT"].ToString() != "null" && dr["BRAT_MECHDAT"].ToString() != "")
                txt.Text = dr["BRAT_MECHDAT"].ToString();

            if (dr["TVACH_TACHTON"].ToString() != "")
                txt.Attributes.Add("MIN", dr["TVACH_TACHTON"].ToString());
            if (dr["TVACH_ELION"].ToString() != "")
                txt.Attributes.Add("MAX", dr["TVACH_ELION"].ToString());
            txt.Attributes.Add("onFocus", "document.getElementById('" + txt.ID + "').select()");  
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void Fill_Mispar_Musach_O_Machsan(ref DropDownList cmb)
     {
        clUtils oUtils = new clUtils();
        DataTable Dt = new DataTable();
        try
        {
            Dt = oUtils.Get_Mispar_Musach_O_Machsan(clnDate.Value);
            cmb.DataSource = Dt;
            cmb.DataTextField = Dt.Columns["description"].ToString();
            cmb.DataValueField = Dt.Columns["code"].ToString();
            cmb.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
     }
    private void fill_Kod_shinuy_premia(ref DropDownList cmb)
    {
        DataTable Dt = new DataTable();
        DataRow dr;
        try
        {
            Dt.Columns.Add("code", System.Type.GetType("System.String"));
            Dt.Columns.Add("description", System.Type.GetType("System.String"));
            dr = Dt.NewRow();
            dr["code"] = "1";
            dr["description"] = "רגיל";
            Dt.Rows.InsertAt(dr, 0);
          
            dr = Dt.NewRow();
            dr["code"] = "2";
            dr["description"] = "הגדלה";
            Dt.Rows.InsertAt(dr, 1);
          
            dr = Dt.NewRow();
            dr["code"] = "3";
            dr["description"] = "מחצית";
            Dt.Rows.InsertAt(dr, 2);

            cmb.DataTextField = Dt.Columns["description"].ToString();
            cmb.DataValueField = Dt.Columns["code"].ToString();
            cmb.DataSource = Dt;
            cmb.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void fill_tafkid_viza(ref DropDownList cmb)
    {
        DataTable Dt = new DataTable();
        DataRow dr;
        try
        {
            Dt.Columns.Add("code", System.Type.GetType("System.String"));
            Dt.Columns.Add("description", System.Type.GetType("System.String"));
            dr = Dt.NewRow();
            dr["code"] = "1";
            dr["description"] = "נהג";
            Dt.Rows.InsertAt(dr, 0);

            dr = Dt.NewRow();
            dr["code"] = "2";
            dr["description"] = "נהג משנה";
            Dt.Rows.InsertAt(dr, 1);

            dr = Dt.NewRow();
            dr["code"] = "3";
            dr["description"] = "מדריך";
            Dt.Rows.InsertAt(dr, 2);

            dr = Dt.NewRow();
            dr["code"] = "4";
            dr["description"] = "נהג מדריך";
            Dt.Rows.InsertAt(dr, 3);

            cmb.DataTextField = Dt.Columns["description"].ToString();
            cmb.DataValueField = Dt.Columns["code"].ToString();
            cmb.DataSource = Dt;
            cmb.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BuildPeiluyot()
    {
        HtmlTable oTblP = new HtmlTable();
        HtmlTableRow oTrP = new HtmlTableRow();
        DataTable dtSadotNosafimLePeilut = new DataTable();
        DataTable dtPeiluyot = new DataTable();
        int makat;
        int i = 0;
        clUtils oUtils = new clUtils();
        bool flagNewRow,flagHosefRow,flagElatTrip;
        bool bKavSherut;
        int iOnatyut;
        try
        {
            dtSadotNosafimLePeilut = (DataTable)Session["dtSadotNosafimLePeilut"];
     
            dtPeiluyot = (DataTable)Session["dtPeiluyot"];
            //dtSadotKayamim = oUtils.GetSadotNosafimKayamim(int.Parse(MisIshi.Value), int.Parse(MisSidur.Value), DateTime.Parse(TaarichCA.Value));

            foreach (DataRow drPeilut in dtPeiluyot.Rows)
            {
                if (int.Parse(drPeilut["MISPAR_KNISA"].ToString()) == 0)
                {
                    flagNewRow = true;
                    makat = int.Parse(drPeilut["MAKAT_NESIA"].ToString());

                    bKavSherut = CheckKavSherut(makat, out flagElatTrip, out iOnatyut);
                       
                    foreach (DataRow dr in dtSadotNosafimLePeilut.Rows)
                    {
                        flagHosefRow = false;
                        switch ((enKodSade)Enum.Parse(typeof(enKodSade), dr["KOD_SADE_IN_MASACH"].ToString()))
                        {
                            case enKodSade.Kod_shinuy_premia:
                                if (flagElatTrip)
                                    flagHosefRow = true;
                                break;
                            case enKodSade.Mispar_Siduri_Oto:
                                if (bKavSherut && iOnatyut==71)
                                    flagHosefRow = true;
                                break;


                        }
                        if (flagHosefRow)
                        {
                            oTrP = new HtmlTableRow();
                            BuildRowPeilut(ref oTrP, dr, drPeilut, i, flagNewRow);
                            oTblP.Rows.Add(oTrP);
                            flagNewRow = false;
                        }
                    }  
                }
                i++;   
            }

            if (oTblP.Rows.Count == 0)
                lblEinSadotLepeilut.Visible = true;
            fieldsetPeilut.Controls.Add(oTblP);
            Session["TablePeilut"] = oTblP;
        
        }
        catch (Exception ex)
        {
            throw ex; 
        }
    }
    protected void BuildRowPeilut(ref HtmlTableRow oTrP, DataRow dr, DataRow drPeilutKayemet, int num, bool flagNewRow)
    {
        HtmlTableCell oCell = new HtmlTableCell();
        HtmlTableCell oCellImg = new HtmlTableCell();
        HtmlImage imgErr = new HtmlImage();
        string[] shaa;
        try
        {
            imgErr.Src = "../../Images/ErrorSign.jpg";
            if (flagNewRow)
            {
                oCell.InnerHtml = " מק''ט: "; // +drPeilutKayemet["MAKAT_NESIA"].ToString();   
                oCell.Style.Add("font-weight", "bold");
                oTrP.Cells.Add(oCell);

                oCell = new HtmlTableCell();
                oCell.InnerHtml = drPeilutKayemet["MAKAT_NESIA"].ToString();
                oTrP.Cells.Add(oCell);

                oCell = new HtmlTableCell();
                oCell.InnerHtml = " שעת יציאה: "; // +drPeilutKayemet["MAKAT_NESIA"].ToString();   
                oCell.Style.Add("font-weight", "bold");
                oTrP.Cells.Add(oCell);

                shaa = drPeilutKayemet["SHAT_YETZIA"].ToString().Split(' ');
                oCell = new HtmlTableCell();
                oCell.InnerHtml = shaa[1].Split(':')[0] + ":" + shaa[1].Split(':')[1];// +" " + shaa[0];
                oTrP.Cells.Add(oCell);

                oCell = new HtmlTableCell();
                oCell.Width = "25";
                oTrP.Cells.Add(oCell);
            }
            else
            {
                oCell = new HtmlTableCell();
                oCell.ColSpan = 5;
                oTrP.Cells.Add(oCell);
            }
            oCell = new HtmlTableCell();
            oCell.InnerHtml = dr["SHEM_SADE_IN_MASACH"].ToString() + ":";
            oCell.Style.Add("font-weight", "bold");
            oTrP.Cells.Add(oCell);
            //האובייקט
            oCell = new HtmlTableCell();
            BuildObjectPeilut(ref oCell, dr, drPeilutKayemet, num ,ref imgErr);
            oCellImg.Width = "5px";
            if (imgErr.Style["Display"] == "block")
            {
                oCellImg.Controls.Add(imgErr);
            }
            oTrP.Cells.Add(oCellImg);
            oTrP.Cells.Add(oCell);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BuildObjectPeilut(ref HtmlTableCell oCell, DataRow dr, DataRow drPeilutKayemet, int num, ref HtmlImage imgErr)
    {
        string sugObject;
        string kodObject;
        DropDownList cmb = new DropDownList();
        TextBox txt = new TextBox();
        CustomValidator vldCustomValidator = new CustomValidator();
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut = new AjaxControlToolkit.ValidatorCalloutExtender();
        string key;
        bool bErrorExists = false;
        string shatYezia, mispar_knisa;
        try
        {
            kodObject = dr["KOD_SADE_IN_MASACH"].ToString();
            switch ((enKodSade)Enum.Parse(typeof(enKodSade), kodObject))
            {
                case enKodSade.Kod_shinuy_premia:
                    imgErr.ID = "imgKod_shinuy_premia_" + num; 

                    cmb.ID = "cmb_Kod_shinuy_premia_" + num;
                    fill_Kod_shinuy_premia(ref cmb);
                    BulidObjectCmb(ref cmb, dr);
                    AddValidSadeChova(dr, cmb.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    //validation
                    if (drPeilutKayemet["Kod_shinuy_premia"].ToString() != "")
                        cmb.SelectedValue = drPeilutKayemet["Kod_shinuy_premia"].ToString();
                    shatYezia = drPeilutKayemet["SHAT_YETZIA"].ToString();
                    mispar_knisa = drPeilutKayemet["MISPAR_KNISA"].ToString();
                    bErrorExists = SetOneError(cmb, oCell, int.Parse(txtId.Value), DateTime.Parse(clnDate.Value),
                                              int.Parse(MisSidur.Value), DateTime.Parse(ShatHatchala.Value),
                                              DateTime.Parse(shatYezia), int.Parse(mispar_knisa),
                                              DateTime.Parse(shatYezia).ToShortTimeString() + "|" + mispar_knisa, "Kod_shinuy_premia");
                    ErrorImage(imgErr, bErrorExists);
                     
                    key = DateTime.Parse(shatYezia).ToShortTimeString() + "|" + mispar_knisa;
                    imgErr.Attributes.Add("ondblclick", "GetErrorMessageSadotNosafim(" + cmb.ID + ",3,'"+ key + "');");
                    break;
                case enKodSade.Mispar_Siduri_Oto:
                    txt.ID = "txt_Mispar_Siduri_Oto_" + num;
                    BulidObjectTextBox(ref txt, dr);
                    AddValidSadeChova(dr, txt.ID, ref vldCustomValidator, ref vldExtenderCallOut, AjaxControlToolkit.ValidatorCalloutPosition.Right);
                    //validation
                    if (drPeilutKayemet["Mispar_Siduri_Oto"].ToString() != "")
                        txt.Text = drPeilutKayemet["Mispar_Siduri_Oto"].ToString();

                    shatYezia = drPeilutKayemet["SHAT_YETZIA"].ToString();
                    mispar_knisa = drPeilutKayemet["MISPAR_KNISA"].ToString();
                    bErrorExists = SetOneError(txt, oCell, int.Parse(txtId.Value), DateTime.Parse(clnDate.Value),
                                             int.Parse(MisSidur.Value), DateTime.Parse(ShatHatchala.Value),
                                             DateTime.Parse(shatYezia), int.Parse(mispar_knisa),
                                             DateTime.Parse(shatYezia).ToShortTimeString() + "|" + mispar_knisa, "Mispar_Siduri_Oto");
                  
                    break;

            }
            sugObject = dr["SUS_SADE_IN_MASACH"].ToString();
            if (sugObject == "cmb")
                oCell.Controls.Add(cmb);
            else if (sugObject == "number")
                oCell.Controls.Add(txt);

            oCell.Controls.Add(vldCustomValidator);
            oCell.Controls.Add(vldExtenderCallOut);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnShmor_OnClick(object sender, EventArgs e)
    {
        COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd = new COLL_SIDURIM_OVDIM();
        COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd = null; // new COLL_YAMEY_AVODA_OVDIM();
        COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd = new COLL_OBJ_PEILUT_OVDIM();
        clOvdim objOvdim = new clOvdim();
        try
        {
           
           // SaveSidurimLeoved
            if (checkTkinutSidurFields())
                if (checkTkinutPeilutFields())
                {
                    setObjectSidur(ref oCollSidurimOvdimUpd, ref oCollYameyAvodaUpd);
                    setObjectPeilut(ref oCollPeilutOvdimUpd);
                    objOvdim.SaveSidurimLeoved(oCollSidurimOvdimUpd, oCollYameyAvodaUpd, oCollPeilutOvdimUpd);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HosafatSidur", "window.returnValue=1;window.close();", true);
                     
                }
           
        }
         catch (Exception ex)
        {
            throw ex;
        }

    }
    private bool checkTkinutSidurFields()
    {
        DataTable dtSadotNosafim = new DataTable();
        CustomValidator vld = new CustomValidator();
        DropDownList cmb = new DropDownList();
        TextBox txt = new TextBox();
        string id;
        HtmlTable oTbl = new HtmlTable();
      // bool isValid = true;
        try
        {
            dtSadotNosafim = (DataTable)Session["dtSadotNosafimLesidur"];
            oTbl = (HtmlTable)Session["Table"];
            foreach (DataRow dr in dtSadotNosafim.Rows)
            {
                id = dr["MIKUM_SADE_IN_DB"].ToString().Split('.')[1].Trim();
                if (dr["SUS_SADE_IN_MASACH"].ToString() == "cmb")
                {
                    id = "cmb_" + id;
                    cmb = (DropDownList)oTbl.FindControl(id);
                    vld = (CustomValidator)oTbl.FindControl("vld_" + id);
                    if (!checkValidCmb(cmb,ref vld, dr))
                        return false;
                }
                else
                {
                    id = "txt_" + id;
                    txt = (TextBox)oTbl.FindControl(id);
                    vld = (CustomValidator)oTbl.FindControl("vld_" + id);
                   // isValid = checkValidTxt(txt, ref vld, dr);
                    if (!checkValidTxt(txt, ref vld, dr))
                        return false;
                    //if (txt.ID == "txt_Mispar_Visa")
                    //{
                    //    if (long.Parse(txt.Text) < 100000) //int.Parse(txt.Attributes["MaxLength"]))
                    //    {
                    //        vld.ErrorMessage = "מספר תעודת ויזה צריך להיות גדול";
                    //        vld.IsValid = false;
                    //        isValid = false;
                    //        break;
                    //    }
                    //}   
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool checkTkinutPeilutFields()
    {

        DataTable dtSadotNosafim = new DataTable();
        DataTable dtPeiluyot = new DataTable();
        CustomValidator vld = new CustomValidator();
        DropDownList cmb = new DropDownList();
        TextBox txt = new TextBox();
        string id;
        HtmlTable oTbl = new HtmlTable();
        int i = 0;
        try
        {
            dtSadotNosafim = (DataTable)Session["dtSadotNosafimLePeilut"];
            dtPeiluyot = (DataTable)Session["dtPeiluyot"];
            oTbl = (HtmlTable)Session["TablePeilut"];
            foreach (DataRow drPeilut in dtPeiluyot.Rows)
            {
                foreach (DataRow dr in dtSadotNosafim.Rows)
                {
                    id = dr["MIKUM_SADE_IN_DB"].ToString().Split('.')[1].Trim();
                    if (dr["SUS_SADE_IN_MASACH"].ToString() == "cmb")
                    {
                        id = "cmb_" + id + "_" + i;
                        cmb = (DropDownList)oTbl.FindControl(id);
                        if (cmb != null)
                        {
                            vld = (CustomValidator)oTbl.FindControl("vld_" + id);
                            if (!checkValidCmb(cmb,ref vld, dr))
                                return false;
                        }
                    }
                    else
                    {
                        id = "txt_" + id + "_" + i;
                        txt = (TextBox)oTbl.FindControl(id);
                        if (txt != null)
                        {
                            vld = (CustomValidator)oTbl.FindControl("vld_" + id);
                            if (!checkValidTxt(txt,ref vld,  dr))
                                return false;
                        }
                    }
                }
                i++;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool checkValidCmb(DropDownList cmb, ref CustomValidator vld, DataRow dr)
    {
       try{
           if (dr["SADE_CHOVA"].ToString() == "1" && cmb.SelectedValue == "-1")
            {
                vld.ErrorMessage = " חובה להזין " + dr["SHEM_SADE_IN_MASACH"].ToString();
                vld.IsValid = false;
                return false;
            }
           //בדיקות נוספות יכנסו לכאן בהמשך
            return true;
        } 
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool checkValidTxt(TextBox txt, ref CustomValidator vld, DataRow dr)
    {
        try
        {
            if (dr["SADE_CHOVA"].ToString() == "1" && txt.Text == "")
            {
                vld.ErrorMessage = " חובה להזין " + dr["SHEM_SADE_IN_MASACH"].ToString();
                vld.IsValid = false;
                return false;
            }
            else
            {
                if (clGeneral.IsNumeric(txt.Text))
                {
                    if (txt.Text.IndexOf('.') > -1)
                    {
                        vld.ErrorMessage = "יש להכניס מספר שלם";
                        vld.IsValid = false;
                        return false;
                    }
                    if (txt.Attributes["MIN"] != null)
                    {
                        if (long.Parse(txt.Text) < long.Parse(txt.Attributes["MIN"]))
                        {
                            vld.ErrorMessage = " יש להכניס ערך השווה או גדול ל" + txt.Attributes["MIN"];
                            vld.IsValid = false;
                            return false;
                        }
                    }

                    if (txt.Attributes["MAX"] != null)
                    {
                        if (long.Parse(txt.Text) > long.Parse(txt.Attributes["MAX"]))
                        {
                            vld.ErrorMessage = "יש להכניס ערך עד " + txt.Attributes["MAX"];
                            vld.IsValid = false;
                            return false;

                        }
                    }
                }
                else if (txt.Text.Length>0)
                {
                    vld.ErrorMessage = "יש להכניס ערך מספרי";
                    vld.IsValid = false;
                    return false;
                }
            }
            //בדיקות נוספות יכנסו לכאן בהמשך
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool setObjectSidur(ref COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd, ref COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd)
    {
        DataTable dtSadotNosafim = new DataTable();
        OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
        DropDownList cmb = new DropDownList();
        TextBox txt = new TextBox();
        HtmlTable oTbl = new HtmlTable();
        string id;
        int lina=0;
        bool isParamYemeyAvoda = false;
         DataTable dtSadotKayamim;
        try
        {
            dtSadotNosafim = (DataTable)Session["dtSadotNosafimLesidur"];
            oTbl = (HtmlTable)Session["Table"];
            dtSadotKayamim = (DataTable)Session["dtSadotKayamim"];

            oObjSidurimOvdimUpd.MISPAR_ISHI = int.Parse(txtId.Value);
            oObjSidurimOvdimUpd.TAARICH = DateTime.Parse(clnDate.Value);
            oObjSidurimOvdimUpd.MISPAR_SIDUR = int.Parse(MisSidur.Value);
            oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = int.Parse(MisSidur.Value);
            oObjSidurimOvdimUpd.SHAT_HATCHALA = DateTime.Parse(ShatHatchala.Value);
            oObjSidurimOvdimUpd.SHAT_GMAR = DateTime.Parse(ShatGmar.Value);
            oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = DateTime.Parse(ShatHatchala.Value);
            oObjSidurimOvdimUpd.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
            if (dtSadotKayamim != null)
            {
                if (dtSadotKayamim.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["MISPAR_MUSACH_O_MACHSAN"].ToString()))
                        oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN = int.Parse(dtSadotKayamim.Rows[0]["MISPAR_MUSACH_O_MACHSAN"].ToString());
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["YOM_VISA"].ToString()))
                        oObjSidurimOvdimUpd.YOM_VISA = int.Parse(dtSadotKayamim.Rows[0]["YOM_VISA"].ToString());
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["ACHUZ_KNAS_LEPREMYAT_VISA"].ToString()))
                        oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA = int.Parse(dtSadotKayamim.Rows[0]["ACHUZ_KNAS_LEPREMYAT_VISA"].ToString());
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["ACHUZ_VIZA_BESIKUN"].ToString()))
                        oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN = int.Parse(dtSadotKayamim.Rows[0]["ACHUZ_VIZA_BESIKUN"].ToString());
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["SUG_HAZMANAT_VISA"].ToString()))
                        oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA = int.Parse(dtSadotKayamim.Rows[0]["SUG_HAZMANAT_VISA"].ToString());
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["MISPAR_SHIUREY_NEHIGA"].ToString()))
                        oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA = int.Parse(dtSadotKayamim.Rows[0]["MISPAR_SHIUREY_NEHIGA"].ToString());
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["TAFKID_VISA"].ToString()))
                        oObjSidurimOvdimUpd.TAFKID_VISA = int.Parse(dtSadotKayamim.Rows[0]["TAFKID_VISA"].ToString());
                    if (!string.IsNullOrEmpty(dtSadotKayamim.Rows[0]["MIVTZA_VISA"].ToString()))
                        oObjSidurimOvdimUpd.MIVTZA_VISA = int.Parse(dtSadotKayamim.Rows[0]["MIVTZA_VISA"].ToString());
                }
            }
            foreach (DataRow dr in dtSadotNosafim.Rows)
            {
                id = dr["MIKUM_SADE_IN_DB"].ToString().Split('.')[1].Trim();
                if (dr["SUS_SADE_IN_MASACH"].ToString() == "cmb")
                {
                    id = "cmb_" + id;
                    cmb = (DropDownList)oTbl.FindControl(id);
                }
                else
                {
                    id = "txt_" + id;
                    txt = (TextBox)oTbl.FindControl(id);
                }

                switch ((enKodSade)Enum.Parse(typeof(enKodSade), dr["KOD_SADE_IN_MASACH"].ToString()))
                {
                    case enKodSade.Mispar_Musach_O_Machsan:
                        if (cmb.SelectedItem.Value != "-1")
                        oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN = int.Parse(cmb.SelectedItem.Value);
                        else
                        {
                            oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN = 0;
                            oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSANIsNull = true;
                        }
                    //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSANIsNull = false;
                        break;
                    case enKodSade.Yom_Visa:
                        if (cmb.SelectedItem.Value != "-1")
                            oObjSidurimOvdimUpd.YOM_VISA = int.Parse(cmb.SelectedItem.Value);
                        else
                        {
                            oObjSidurimOvdimUpd.YOM_VISA = 0;
                            oObjSidurimOvdimUpd.YOM_VISAIsNull = true;
                        }
                        //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.YOM_VISAIsNull = false;
                        break;
                    case enKodSade.Achuz_Knas_LePremyat_Visa:
                        if (txt.Text.Length>0)
                            oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA = int.Parse(txt.Text);
                        else
                        {
                            oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA = 0;
                            oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISAIsNull = true;
                        }
                        //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISAIsNull = false;
                        break;
                    case enKodSade.Achuz_Viza_Besikun:
                         if (txt.Text.Length>0)
                        oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN = int.Parse(txt.Text);
                          else
                        {
                            oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN = 0;
                            oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUNIsNull = true;
                        }
                        //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUNIsNull = false;
                        break;
                    case enKodSade.Sug_Hazmanat_Visa:
                        if (cmb.SelectedItem.Value != "-1")
                                oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA = int.Parse(cmb.SelectedItem.Value);
                          else
                        {
                            oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA = 0;
                            oObjSidurimOvdimUpd.SUG_HAZMANAT_VISAIsNull = true;
                        }
                        //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.SUG_HAZMANAT_VISAIsNull = false;
                        break;
                    case enKodSade.Mispar_shiurey_nehiga:
                        if (txt.Text.Length > 0)
                             oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA = int.Parse(txt.Text);
                          else
                        {
                            oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA = 0;
                            oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGAIsNull = true;
                        }
                        //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGAIsNull = false;
                        break;
                    case enKodSade.Tafkid_Visa:
                        if (cmb.SelectedItem.Value !="-1")
                             oObjSidurimOvdimUpd.TAFKID_VISA = int.Parse(cmb.SelectedItem.Value);
                          else
                        {
                            oObjSidurimOvdimUpd.TAFKID_VISA = 0;
                            oObjSidurimOvdimUpd.TAFKID_VISAIsNull = true;
                        }
                        //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.TAFKID_VISAIsNull = false;
                        break;
                    case enKodSade.Mivtza_Visa:
                        if (cmb.SelectedItem.Value!="-1")
                                oObjSidurimOvdimUpd.MIVTZA_VISA = int.Parse(cmb.SelectedItem.Value);
                          else
                        {
                            oObjSidurimOvdimUpd.MIVTZA_VISA = 0;
                            oObjSidurimOvdimUpd.MIVTZA_VISAIsNull = true;
                        }
                        //if ((dr["SADE_CHOVA"].ToString() != "1"))
                        //    oObjSidurimOvdimUpd.MIVTZA_VISAIsNull = false;
                        break;
                    case enKodSade.Lina:
                        isParamYemeyAvoda= true;
                        lina=int.Parse(cmb.SelectedItem.Value); ;
                        break;
                    //case enKodSade.Km_visa:
                    //    HiddenSdotPeilutBeramatSidur.Attributes["Km_visa"] = HiddenSdotPeilutBeramatSidur.Attributes["Km_visa"] + ";" + txt.Text;
                    //    break;
                    //case enKodSade.Mispar_Visa:
                    //    HiddenSdotPeilutBeramatSidur.Attributes["Km_visa"] = HiddenSdotPeilutBeramatSidur.Attributes["Km_visa"] + ";" + cmb.SelectedItem.Value;
                    //    break;  
                }
            }
            oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
          //  clGeneral.UpdateSidurimOvdim(oCollSidurimOvdimUpd);
          //  clDefinitions.UpdateSidurimOvdim(oCollSidurimOvdimUpd);
            if (isParamYemeyAvoda)
                SavePirteyYemeyAvoda(ref oCollYameyAvodaUpd,lina);
            return true;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void setObjectPeilut(ref COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd)
    {
        DataTable dtSadotNosafim = new DataTable();
        
        OBJ_PEILUT_OVDIM oObjPeiluyotOvdimUpd;
        DropDownList cmb = new DropDownList();
        TextBox txt = new TextBox();
        HtmlTable oTbl = new HtmlTable();
        DataTable dtPeiluyot = new DataTable();
        string id;
        bool flag = false;
        int i = 0;
        try
        {
            dtSadotNosafim = (DataTable)Session["dtSadotNosafimLePeilut"];
            dtPeiluyot = (DataTable)Session["dtPeiluyot"];
            oTbl = (HtmlTable)Session["TablePeilut"];

            foreach (DataRow drPeilut in dtPeiluyot.Rows)
            {
                oObjPeiluyotOvdimUpd = new OBJ_PEILUT_OVDIM();
                oObjPeiluyotOvdimUpd.MISPAR_ISHI = int.Parse(txtId.Value);
                oObjPeiluyotOvdimUpd.MISPAR_SIDUR = int.Parse(MisSidur.Value);
                oObjPeiluyotOvdimUpd.NEW_MISPAR_SIDUR = int.Parse(MisSidur.Value);
                oObjPeiluyotOvdimUpd.TAARICH = DateTime.Parse(clnDate.Value);
                oObjPeiluyotOvdimUpd.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                oObjPeiluyotOvdimUpd.SHAT_YETZIA =DateTime.Parse( drPeilut["SHAT_YETZIA"].ToString());
                oObjPeiluyotOvdimUpd.NEW_SHAT_YETZIA = DateTime.Parse(drPeilut["SHAT_YETZIA"].ToString());
                oObjPeiluyotOvdimUpd.SHAT_HATCHALA_SIDUR = DateTime.Parse(ShatHatchala.Value);
                oObjPeiluyotOvdimUpd.NEW_SHAT_HATCHALA_SIDUR = DateTime.Parse(ShatHatchala.Value);
               // oObjPeiluyotOvdimUpd.MISPAR_KNISA = 0;
                oObjPeiluyotOvdimUpd.UPDATE_OBJECT = 1;
                if (!string.IsNullOrEmpty(drPeilut["KM_VISA"].ToString()))
                    oObjPeiluyotOvdimUpd.KM_VISA = int.Parse(drPeilut["KM_VISA"].ToString());
                if (!string.IsNullOrEmpty(drPeilut["MISPAR_VISA"].ToString()))
                    oObjPeiluyotOvdimUpd.MISPAR_VISA = int.Parse(drPeilut["MISPAR_VISA"].ToString());
                if (!string.IsNullOrEmpty(drPeilut["KOD_SHINUY_PREMIA"].ToString()))
                    oObjPeiluyotOvdimUpd.KOD_SHINUY_PREMIA = int.Parse(drPeilut["KOD_SHINUY_PREMIA"].ToString());
                if (!string.IsNullOrEmpty(drPeilut["MISPAR_SIDURI_OTO"].ToString()))
                    oObjPeiluyotOvdimUpd.MISPAR_SIDURI_OTO = int.Parse(drPeilut["MISPAR_SIDURI_OTO"].ToString());
                if (!string.IsNullOrEmpty(drPeilut["MISPAR_KNISA"].ToString()))
                    oObjPeiluyotOvdimUpd.MISPAR_KNISA = int.Parse(drPeilut["MISPAR_KNISA"].ToString());
                HachnasatSdotPeilutBeramatSidur(ref oObjPeiluyotOvdimUpd,i);
                foreach (DataRow dr in dtSadotNosafim.Rows)
                {
                   
                    flag = false;
                    id = dr["MIKUM_SADE_IN_DB"].ToString().Split('.')[1].Trim();
                    if (dr["SUS_SADE_IN_MASACH"].ToString() == "cmb")
                    {
                        id = "cmb_" + id+ "_"+i;
                        cmb = (DropDownList)oTbl.FindControl(id);
                        if (cmb != null)
                            flag = true;
                    }
                    else
                    {
                        id = "txt_" + id + "_" + i;
                        txt = (TextBox)oTbl.FindControl(id);
                        if (txt != null)
                            flag = true;
                    }

                    if (flag)
                    {
                        switch ((enKodSade)Enum.Parse(typeof(enKodSade), dr["KOD_SADE_IN_MASACH"].ToString()))
                        {
                           
                            case enKodSade.Kod_shinuy_premia:
                                if (cmb.SelectedItem.Value != "-1")
                                    oObjPeiluyotOvdimUpd.KOD_SHINUY_PREMIA = int.Parse(cmb.SelectedItem.Value);
                                else
                                {
                                 oObjPeiluyotOvdimUpd.KOD_SHINUY_PREMIA =0;
                                 oObjPeiluyotOvdimUpd.KOD_SHINUY_PREMIAIsNull = true;
                                }
                            //if  ( (dr["SADE_CHOVA"].ToString() != "1"))
                                //    oObjPeiluyotOvdimUpd.KOD_SHINUY_PREMIAIsNull = false;
                                break;
                            case enKodSade.Mispar_Siduri_Oto:
                                if (txt.Text.Length > 0)
                                    oObjPeiluyotOvdimUpd.MISPAR_SIDURI_OTO = int.Parse(txt.Text);
                                else
                                {
                                    oObjPeiluyotOvdimUpd.MISPAR_SIDURI_OTO = 0;
                                    oObjPeiluyotOvdimUpd.MISPAR_SIDURI_OTOIsNull = true;
                                }
                                    //if ((dr["SADE_CHOVA"].ToString() != "1"))
                                    //    oObjPeiluyotOvdimUpd.MISPAR_SIDURI_OTOIsNull = false;
                                break;
                        }
                    }
                }
               
                oCollPeilutOvdimUpd.Add(oObjPeiluyotOvdimUpd);
                i++;
            }
           
       //  clGeneral.UpdatePeilutOvdim(oCollPeilutOvdimUpd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void HachnasatSdotPeilutBeramatSidur(ref OBJ_PEILUT_OVDIM oObjPeiluyotOvdimUpd, int index )
    {
       // string[] pratim;
        int indexPeilut;
        TextBox txt = new TextBox();
        HtmlTable oTbl = new HtmlTable();
         try{
             oTbl = (HtmlTable)Session["Table"]; 
               //ק"מ
             if (HiddenSdotPeilutBeramatSidur.Attributes["Km_visa"] != null)
             {
                 txt = (TextBox)oTbl.FindControl("txt_Km_visa");
                 indexPeilut = int.Parse(HiddenSdotPeilutBeramatSidur.Attributes["Km_visa"].ToString());
                 if ((indexPeilut == index) && (txt.Text.Length > 0))
                     oObjPeiluyotOvdimUpd.KM_VISA = int.Parse(txt.Text);
                 else { 
                     oObjPeiluyotOvdimUpd.KM_VISA = 0;
                    oObjPeiluyotOvdimUpd.KM_VISAIsNull = true;
                 }
             }
             //מס ויזה
             if (HiddenSdotPeilutBeramatSidur.Attributes["Mispar_Visa"] != null)
             {
                 txt = (TextBox)oTbl.FindControl("txt_Mispar_Visa");
                 indexPeilut = int.Parse(HiddenSdotPeilutBeramatSidur.Attributes["Mispar_Visa"].ToString());
                 if ((indexPeilut == index) && (txt.Text.Length > 0))
                     oObjPeiluyotOvdimUpd.MISPAR_VISA = long.Parse(txt.Text);
                 else { oObjPeiluyotOvdimUpd.MISPAR_VISA = 0;
                 oObjPeiluyotOvdimUpd.MISPAR_VISAIsNull = true;

                 }
             }
         }
        catch (Exception ex)
        {
            throw ex;
        }
     }
    private void SavePirteyYemeyAvoda(ref COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd,int lina)
    {
       
        OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
        try
        {
            oObjYameyAvodaUpd.MISPAR_ISHI = int.Parse(txtId.Value);
            oObjYameyAvodaUpd.TAARICH = DateTime.Parse(clnDate.Value);
            oObjYameyAvodaUpd.LINA = lina;
            oObjYameyAvodaUpd.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
            //ייתכן ויגיעו פרמטרים נוספים בהמשך
            oCollYameyAvodaUpd.Add(oObjYameyAvodaUpd);
          //  clGeneral.UpdateYameyAvodaOvdim(oCollYameyAvodaUpd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /**************************/

    protected bool SetOneError(Control oObj, HtmlTableCell hCell, string sFieldName, ref clSidur oSidur, string sPeilutKey, string sImgId)
    {
        bool bErrorExists = false; ;
        DataSet dsFieldsErrors;
        DataView dvFieldsErrors;
        String _ContolType = oObj.GetType().ToString();
        bool IsRasehmet = LoginUser.IsRashemetProfile(LoginUser.GetLoginUser()); // == true ? 1:0;
        dsFieldsErrors = clDefinitions.GetErrorsForFields(IsRasehmet, ErrorsList, int.Parse(txtId.Value), DateTime.Parse(clnDate.Value),
            int.Parse(MisSidur.Value), DateTime.Parse(ShatHatchala.Value), sFieldName);
        dvFieldsErrors = new DataView(dsFieldsErrors.Tables[0]);
        dvFieldsErrors.RowFilter = "SHOW_ERROR=1";

        if (dvFieldsErrors.Count > 0)
        //if (dsFieldsErrors.Tables[0].Rows.Count > 0)
        {
            SetFieldStyle(oObj, hCell, 2, dvFieldsErrors.Count, _ContolType, "red", "white", sPeilutKey, sFieldName, sImgId);
            bErrorExists = true;
        }
        else
        {
            SetFieldStyle(oObj, hCell, 2, dvFieldsErrors.Count, _ContolType, "white", "black", sPeilutKey, sFieldName, sImgId);
        }
        return bErrorExists;
    }
    protected bool SetOneError(Control oObj, HtmlTableCell hCell, int iMisparIshi, DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, DateTime dPeilutShatYetiza, int iMisparKnisa, string sPeilutKey, string sFieldName)
    {
        bool bErrorExists = false; 
        DataSet dsFieldsErrors;
        DataView dvFieldsErrors;
        String _ContolType = oObj.GetType().ToString();
        bool IsRasehmet =LoginUser.IsRashemetProfile(LoginUser.GetLoginUser());

        dsFieldsErrors = clDefinitions.GetErrorsForFields(IsRasehmet, ErrorsList, iMisparIshi, dCardDate, iMisparSidur, dShatHatchala, dPeilutShatYetiza, iMisparKnisa, sFieldName);
        dvFieldsErrors = new DataView(dsFieldsErrors.Tables[0]);
        dvFieldsErrors.RowFilter = "SHOW_ERROR=1";

        if (dvFieldsErrors.Count > 0)
       // if (dsFieldsErrors.Tables[0].Rows.Count > 0)
        {
            SetFieldStyle(oObj, hCell, 3, dvFieldsErrors.Count, _ContolType, "red", "white", sPeilutKey, sFieldName, "");
            bErrorExists = true;
        }
        else
        {
            SetFieldStyle(oObj, hCell, 3, dvFieldsErrors.Count, _ContolType, "white", "black", sPeilutKey, sFieldName, "");
        }
        return bErrorExists;
    }

    protected void SetFieldStyle(Control oObj, HtmlTableCell hCell, int iLevel, int iErrorCount, string sControlType,
                               string sBackColor, string sColor, string sPeilutKey, string sFieldName, string sImgId)
    {
        ((WebControl)oObj).Style.Add("background-color", sBackColor);
        ((WebControl)oObj).Style.Add("color", sColor);
        if (iErrorCount > 0)
        {
            ((WebControl)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
            ((WebControl)oObj).Attributes.Add("ondblClick", "GetErrorMessageSadotNosafim(this," + iLevel + ",'" + sPeilutKey + "')");
            ((WebControl)oObj).Attributes.Add("FName", sFieldName);
            ((WebControl)oObj).Attributes.Add("ImgId", sImgId);
        }

        //switch (sControlType)
        //{
        //    case "System.Web.UI.WebControls.TextBox":
        //        ((TextBox)oObj).Style.Add("background-color", sBackColor);
        //        ((TextBox)oObj).Style.Add("color", sColor);
        //        if (iErrorCount > 0)
        //        {
        //            ((TextBox)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
        //            ((TextBox)oObj).Attributes.Add("ondblClick", "GetErrorMessageSadotNosafim(this," + iLevel + ",'" + sPeilutKey + "')");
        //            ((TextBox)oObj).Attributes.Add("FName", sFieldName);
        //            ((TextBox)oObj).Attributes.Add("ImgId", sImgId);
        //        }
        //        break;
        //    case "System.Web.UI.WebControls.DropDownList":
        //        ((DropDownList)oObj).Style.Add("background-color", sBackColor);
        //        ((DropDownList)oObj).Style.Add("color", sColor);
        //        if (iErrorCount > 0)
        //        {
        //            ((DropDownList)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
        //            ((DropDownList)oObj).Attributes.Add("ImgId", sImgId);
        //            ((DropDownList)oObj).Attributes.Add("ondblClick", "GetErrorMessageSadotNosafim(this," + iLevel + ",'" + sPeilutKey + "')");
        //            ((DropDownList)oObj).Attributes.Add("FName", sFieldName);
        //        }
        //        break;          
        //}
    }

    protected void ErrorImage(HtmlImage imgErr, bool bErrorExists)
    {
        if (bErrorExists)
            imgErr.Style.Add("Display", "block");
        else
            imgErr.Style.Add("Display", "none");
    }
}
