using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.UI;
using KdsLibrary;
using System.Data;
using KdsLibrary.BL;
using KdsLibrary.UDT;
using KdsBatch;
using System.Web.UI.HtmlControls;

public partial class Modules_Ovdim_HosafatPeiluyot : KdsPage
{
     public const int KISUY_TOR = 0;
    public const int hidShatYezia = 1;
    public const int SHAT_YETZIA =2;
    public const int TEUR = 3;
    public const int KAV = 4;
    public const int SUG = 5;
    public const int hidMisRechev = 6;
    public const int MISPAR_RECHEV = 7;
    public const int MAKAT = 8;
    public const int DAKOT_HAGDARA = 9;
    public const int hidDakotBafoal = 10;
    public const int DAKOT_BAFOAL = 11;
    public const int hidHosefPeilut = 12;
    public const int HOSEF_PEILUT = 13;
    public const int IS_VALID_MIS_RECHEV = 14;
    public const int tmpValidMisRechev = 15;
    public const int SHAT_YEZIA_DATE = 16;
    public const int txtShatYeziaDate = 17;
    public const int MISPAR_RISHUY=18;
    public const int MISPAR_KNISA = 19;
    public const int ROW = 20;
    public const int SUG_KNISA = 21;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtElements = new DataTable();
        clUtils oUtils = new clUtils();
        DataTable dtParametrim = new DataTable();
        clOvdim oOvdim = new clOvdim();
        DataTable dtPeiluyotLesidur;
         clKavim oKavim = new clKavim();
         int iMakatType;
        long lMakatNesia;
        int i;
        long lMisRechev=0;
        long lTmpMisRechev, lMisparRishuy;
        try
        {
            ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
            if (!Page.IsPostBack)
            {
                Session["DtPeiluyotNew"] = null;
                Session["DtPeiluyot"] = null;
                SetFixedHeaderGrid(pnlgrdPeiluyot.ClientID, Header);
                SetDefault();
                if (Request.QueryString["SidurID"] != null)
                {
                    txtHiddenMisparSidur.Value = Request.QueryString["SidurID"].ToString();
                }
                if (Request.QueryString["CardDate"] != null)
                {
                    txtHiddenTaarichCA.Value = Request.QueryString["CardDate"].ToString(); //"26/05/2009";//
                }
                ////אם הפרמטר 'לא לשמור פעילות' קיים אז לא שומרים את הפעילות בדף,אם הוא לא קיים,שומרים
                if (Request.QueryString["NoSavePeilut"] != null)
                    SavePeilut.Value = "NO";
                else SavePeilut.Value = "YES";

                if (Request.QueryString["SidurHour"] != null)
                {
                    txtHiddenHourHatchaltSidur.Value = Request.QueryString["SidurHour"].ToString(); //  "12:00"; //
                }
                if (Request.QueryString["SidurDate"] != null)
                {
                    txtSidurDate.Value = Request.QueryString["SidurDate"].ToString(); //"08/03/2010";//"26/05/2009"; //
                }
                if (Request.QueryString["EmpID"] != null)
                {
                    txtHiddenMisparIshi.Value = Request.QueryString["EmpID"].ToString();
                }

                setAttributsElemnt();
                dtElements = oUtils.getAllElements("");
                if (dtElements.Rows.Count > 0)
                {
                    ElementsRelevants.Value = ",";
                    for (i = 0; i < dtElements.Rows.Count; i++)
                        ElementsRelevants.Value = ElementsRelevants.Value + dtElements.Rows[i]["KOD_ELEMENT"].ToString() + ",";
                }
                BuildDtPeiluyot();
                dtParametrim = oUtils.getErechParamByKod("29,98", txtHiddenTaarichCA.Value);
                if (dtParametrim.Rows.Count > 0)
                {
                    for (i = 0; i < dtParametrim.Rows.Count; i++)
                        Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());
                }

                dtPeiluyotLesidur = oOvdim.GetPeiluyotLeOved(int.Parse(txtHiddenMisparIshi.Value), int.Parse(txtHiddenMisparSidur.Value), DateTime.Parse(txtHiddenTaarichCA.Value), DateTime.Parse(txtSidurDate.Value + " " + txtHiddenHourHatchaltSidur.Value));
                Session.Add("dtPeiluyotLesidur", dtPeiluyotLesidur);
                for (i = 0; i < dtPeiluyotLesidur.Rows.Count; i++)
                {
                    lMakatNesia = long.Parse(dtPeiluyotLesidur.Rows[i]["makat_nesia"].ToString());
                    iMakatType = oKavim.GetMakatType(lMakatNesia);

                    if (iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || iMakatType == clKavim.enMakatType.mEmpty.GetHashCode()
                        || (iMakatType == clKavim.enMakatType.mElement.GetHashCode() && ((!string.IsNullOrEmpty(dtPeiluyotLesidur.Rows[i]["bus_number_must"].ToString()))
                        || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700" || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "761"
                        || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "784")
                        && (lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString())
                                                     && (lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
                                                   && (lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString())))
                    {
                        if (!string.IsNullOrEmpty(dtPeiluyotLesidur.Rows[i]["oto_no"].ToString()))
                            lTmpMisRechev = long.Parse(dtPeiluyotLesidur.Rows[i]["oto_no"].ToString());
                        else lTmpMisRechev = -1;

                        if (lMisRechev > 0 && lMisRechev != lTmpMisRechev)
                        {
                            lMisRechev = -1;
                            break;
                        }
                        else lMisRechev = lTmpMisRechev;
                        
                    }
                }

                 ViewState["MisRechev"]= lMisRechev.ToString();
                 lMisparRishuy = 0;
                 oKavim.GetBusLicenseNumber(long.Parse(ViewState["MisRechev"].ToString()), ref lMisparRishuy);
                 ViewState["MisparRishuy"] = lMisparRishuy;
                 txtMisRechev.Value = ViewState["MisRechev"].ToString();
                 txtMisRishuy.Value = lMisparRishuy.ToString();
                 SetFocus();
            }


            if ((long.Parse(txtMisRechev.Value.ToString()) > 0) && ViewState["MisRechev"].ToString() != "-1")
            {
                ViewState["MisRechev"] = txtMisRechev.Value;
                ViewState["MisparRishuy"] = txtMisRishuy.Value;
            }
            else
            {
                txtMisRechev.Value = "0";
                ViewState["MisRechev"] = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BuildDtPeiluyot()
    {
        DataTable dtSource = new DataTable();
        dtSource.Columns.Add("ROW",Type.GetType("System.Int32"));
        dtSource.Columns.Add("KISUY_TOR");
        dtSource.Columns.Add("KISUY_TOR_NEW");
        dtSource.Columns.Add("SHAT_YETZIA");
        dtSource.Columns.Add("MISPAR_KNISA");
        dtSource.Columns.Add("TEUR");
        dtSource.Columns.Add("KAV");
        dtSource.Columns.Add("SUG");
        dtSource.Columns.Add("SUG_PEILUT");
        dtSource.Columns.Add("MISPAR_RECHEV");
        dtSource.Columns.Add("MISPAR_RISHUY");
        dtSource.Columns.Add("MAKAT");
        dtSource.Columns.Add("DAKOT_HAGDARA");
        dtSource.Columns.Add("HAGDARA_LEGMAR");
        dtSource.Columns.Add("DAKOT_BAFOAL");
        dtSource.Columns.Add("HOSEF_PEILUT");
        dtSource.Columns.Add("PEILUT_CHOVA");
        dtSource.Columns.Add("IS_VALID_MAKAT");
        dtSource.Columns.Add("IS_VALID_MIS_RECHEV");
        dtSource.Columns.Add("SHAT_YEZIA_DATE");
        dtSource.Columns.Add("MUST_MIS_RECHEV");
        dtSource.Columns.Add("MUST_DAKOT");
        dtSource.Columns.Add("SUG_KNISA");
        Session.Add("DtPeiluyot", dtSource);
    }

    protected void SetDefault()
    {
        rdKod.Attributes.Add("onclick", "SetTextBox();");
        rdTeur.Attributes.Add("onclick", "SetTextBox();");
    }

    protected void SetFocus()
    {
        txtMisparElement.Attributes.Add("onFocus ", "onSadeFocus(txtMisparElement)");
        txtTeurElement.Attributes.Add("onFocus ", "onSadeFocus(txtTeurElement)");
        txtSugElement.Attributes.Add("onFocus ", "onSadeFocus(txtSugElement)");
        txtErechElement.Attributes.Add("onFocus ", "onSadeFocus(txtErechElement)");
        txtMakat.Attributes.Add("onFocus ", "onSadeFocus(txtMakat)");
    }
    protected void btnAddElement_OnClick(object sender, EventArgs e)
    {
        try
        {

            grdPeiluyot.Style.Add("display", "inline");
            if (Session["DtPeiluyot"] == null) BuildDtPeiluyot();
            DataTable dtPeiluyot = (DataTable)Session["DtPeiluyot"];
            RefreshDataTable(ref  dtPeiluyot);
           
            DataRow dr = dtPeiluyot.NewRow();
            dr["TEUR"] = txtTeurElement.Text;
            dr["SUG"] = "";
            dr["SUG_PEILUT"] = 1;
            dr["MUST_MIS_RECHEV"] = MustRechev.Value;
            if (MustRechev.Value == "true" && (long.Parse(ViewState["MisRechev"].ToString()) > 0))
            {
                dr["MISPAR_RECHEV"] = ViewState["MisRechev"];
                dr["MISPAR_RISHUY"] = ViewState["MisparRishuy"];
            }
            dr["MISPAR_KNISA"] = 0;
            dr["HOSEF_PEILUT"] = 1;
            dr["DAKOT_HAGDARA"] = 0;
            dr["MUST_DAKOT"] = "false";
            dr["MAKAT"] = "7" + txtMisparElement.Text.PadLeft(2, (char)48) + txtErechElement.Text.PadLeft(3, (char)48) + "00";
            dr["ROW"] = dtPeiluyot.Rows.Count;
            dr["SHAT_YEZIA_DATE"] = txtHiddenTaarichCA.Value;
            dtPeiluyot.Rows.Add(dr);

            BindGrid(dtPeiluyot);
            btnHosafa.Style["display"]= "inline";

            ((TextBox)grdPeiluyot.Rows[0].Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Focus();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnAddNesia_OnClick(object sender, EventArgs e)
    {
        clKavim.enMakatType MakatType;
        DataSet dsKavim = new DataSet();
        clKavim oKavim = new clKavim();
        long lMakatNesia = int.Parse(txtMakat.Text);
        DateTime dCardDate = DateTime.Parse(txtHiddenTaarichCA.Value);
        int iMakatValid;
        bool flag = true;
        if (Session["DtPeiluyot"] == null) BuildDtPeiluyot();
        DataTable dtPeiluyot = (DataTable)Session["DtPeiluyot"];

        DataTable dtKavim;
        try
        {
            RefreshDataTable(ref  dtPeiluyot);
            
             MakatType = (clKavim.enMakatType)oKavim.GetMakatType(lMakatNesia); 
           
                switch (MakatType)
                {
                    case clKavim.enMakatType.mKavShirut:
                        dsKavim = oKavim.GetKavimDetailsFromTnuaDS(lMakatNesia, dCardDate, out iMakatValid,1);
                        if (dsKavim.Tables[0].Rows.Count > 0)
                            setNetuneyKavShirut(ref dtPeiluyot, dsKavim);
                        else flag = false;
                        break;
                    case clKavim.enMakatType.mEmpty:
                        dtKavim = oKavim.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                       if (dtKavim.Rows.Count > 0) setNetuneyEmpty(ref dtPeiluyot, dtKavim);
                       else flag = false;
                        break;
                    case clKavim.enMakatType.mNamak:
                        dtKavim = oKavim.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                        if (dtKavim.Rows.Count > 0) setNetuneyNamak(ref dtPeiluyot, dtKavim);
                        else flag = false;
                        break;
                    default: flag = false;
                        break;
                      
                }
           
            if (flag)
            {
                grdPeiluyot.Style.Add("display", "inline");

                BindGrid(dtPeiluyot);
                btnHosafa.Style["display"] = "inline";
                ((TextBox)grdPeiluyot.Rows[0].Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Focus();
            }
            else
            {
                vldMakat.ErrorMessage = "המק''ט שהוקלד שגוי";
                vldMakat.IsValid = false;
                grdPeiluyot.Style.Add("display", "inline");
                BindGrid(dtPeiluyot);
             
                if (dtPeiluyot.Rows.Count > 0)
                { btnHosafa.Style["display"] = "inline"; }
                else btnHosafa.Style["display"] = "none";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void BindGrid(DataTable dtPeiluyot)
    {
        DataView dvPeiluyot = new DataView(dtPeiluyot);
     //   dvPeiluyot.Sort = "SHAT_YETZIA ASC, MISPAR_KNISA ASC";
        
        grdPeiluyot.DataSource = dvPeiluyot;
        grdPeiluyot.DataBind();
    }

    protected void setNetuneyKavShirut(ref DataTable dtPeiluyot, DataSet dsKavim)
    {
        DataRow drNesia = dtPeiluyot.NewRow();
        DataTable dtPirteyKav = dsKavim.Tables[0];
        DataTable dtKnisotKav = dsKavim.Tables[1];
        DataTable dtVisutimKav = dsKavim.Tables[2];
        try
        {
            if(dtPirteyKav.Rows[0]["KISUITOR"].ToString()!="0")
                drNesia["KISUY_TOR"] = dtPirteyKav.Rows[0]["KISUITOR"].ToString();
            drNesia["teur"] = dtPirteyKav.Rows[0]["DESCRIPTION"].ToString();
            drNesia["kav"] = dtPirteyKav.Rows[0]["SHILUT"].ToString();
            drNesia["sug"] = dtPirteyKav.Rows[0]["SUGSHIRUTNAME"].ToString();
            drNesia["makat"] = dtPirteyKav.Rows[0]["MAKAT8"].ToString();
            drNesia["DAKOT_HAGDARA"] = dtPirteyKav.Rows[0]["MAZANTICHNUN"].ToString();
            drNesia["HAGDARA_LEGMAR"] = dtPirteyKav.Rows[0]["MazanTashlum"].ToString();
            if (long.Parse(ViewState["MisRechev"].ToString()) > 0)
            { drNesia["MISPAR_RECHEV"] = ViewState["MisRechev"];
            drNesia["MISPAR_RISHUY"] = ViewState["MisparRishuy"];
            }
            drNesia["HOSEF_PEILUT"] = 1;
            drNesia["MISPAR_KNISA"] = 0;
        //    drNesia["SUG_KNISA"] = dtPirteyKav.Rows[0]["SUGKNISA"].ToString();
            drNesia["MUST_MIS_RECHEV"] = true;
            drNesia["MUST_DAKOT"] = true;
            drNesia["ROW"] = dtPeiluyot.Rows.Count;
            drNesia["SHAT_YEZIA_DATE"] = txtHiddenTaarichCA.Value;
            dtPeiluyot.Rows.Add(drNesia);

            for (int i = 0; i < dtKnisotKav.Rows.Count; i++)
            {
                drNesia = dtPeiluyot.NewRow();
                drNesia["MISPAR_KNISA"] = dtKnisotKav.Rows[i]["Siduri"].ToString();
                drNesia["SUG_KNISA"] = dtKnisotKav.Rows[i]["SUGKNISA"].ToString();
               if (dtKnisotKav.Rows[i]["MokedTchilaName"].ToString() != dtKnisotKav.Rows[i]["YeshuvName"].ToString())
                   drNesia["teur"] = "כנ-" + dtKnisotKav.Rows[i]["YeshuvName"] .ToString() + ", " + dtKnisotKav.Rows[i]["MokedTchilaName"].ToString();
               else
                   drNesia["teur"] = "כנ-" + dtKnisotKav.Rows[i]["MokedTchilaName"].ToString();
                
                drNesia["kav"] = dtPirteyKav.Rows[0]["SHILUT"].ToString();
                drNesia["makat"] = dtPirteyKav.Rows[0]["MAKAT8"].ToString();
                 if (dtKnisotKav.Rows[i]["SugKnisa"].ToString() == "2")
                {
                    drNesia["sug"] = "כניסה חובה";
                    drNesia["MUST_DAKOT"] = "false";
                }
                else { drNesia["sug"] = "כניסה לפי צורך";
                drNesia["MUST_DAKOT"] = true;
                drNesia["teur"] = drNesia["teur"] + " (לפי-צורך)";
                }
                if (long.Parse(ViewState["MisRechev"].ToString()) > 0)
                {
                    drNesia["MISPAR_RECHEV"] = ViewState["MisRechev"];
                    drNesia["MISPAR_RISHUY"] = ViewState["MisparRishuy"];
                }
                drNesia["HOSEF_PEILUT"] = 1;
                drNesia["SUG_PEILUT"] = 2;
                drNesia["ROW"] = dtPeiluyot.Rows.Count;
                drNesia["MUST_MIS_RECHEV"] = "false";
                drNesia["SHAT_YEZIA_DATE"] = txtHiddenTaarichCA.Value;
                dtPeiluyot.Rows.Add(drNesia);
            }

            for (int i = 0; i < dtVisutimKav.Rows.Count; i++)
            {
                drNesia = dtPeiluyot.NewRow();
                drNesia["MISPAR_KNISA"] = 0;// dtKnisotKav.Rows[i]["Siduri"].ToString();
                /// drNesia["SUG_KNISA"] = dtVisutimKav.Rows[i]["SUGKNISA"].ToString();
                drNesia["teur"] = dtVisutimKav.Rows[i]["NIHULNAME"].ToString();
                drNesia["makat"] =  dtVisutimKav.Rows[i]["MAKATVISUT"].ToString();
                if (drNesia["makat"].ToString().Substring(0, 3) == "761" || drNesia["makat"].ToString().Substring(0, 3) == "784")
                {
                    drNesia["sug"] = "";
                    drNesia["kav"] = "";
                    drNesia["MISPAR_RECHEV"] = "";
                    drNesia["MISPAR_RISHUY"] = "";
                }
                else
                {
                    drNesia["sug"] = "ויסות";
                    drNesia["kav"] = dtPirteyKav.Rows[0]["SHILUT"].ToString();
                }
               
                drNesia["MUST_DAKOT"] = "false";
                if (long.Parse(ViewState["MisRechev"].ToString()) > 0)
                {
                    drNesia["MISPAR_RECHEV"] = ViewState["MisRechev"];
                    drNesia["MISPAR_RISHUY"] = ViewState["MisparRishuy"];
                }
                drNesia["HOSEF_PEILUT"] = 1;
                drNesia["SUG_PEILUT"] = 2;
                drNesia["ROW"] = dtPeiluyot.Rows.Count;
                drNesia["MUST_MIS_RECHEV"] = "false";
                drNesia["SHAT_YEZIA_DATE"] = txtHiddenTaarichCA.Value;
                dtPeiluyot.Rows.Add(drNesia);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void setNetuneyNamak(ref DataTable dtPeiluyot, DataTable dtPirteyKav)
    {
        DataRow drNesia = dtPeiluyot.NewRow();
         try
        {
            drNesia["sug"] = "נמ''ק";
            if (dtPirteyKav.Rows[0]["KISUITOR"].ToString() != "0")
                drNesia["KISUY_TOR"] = dtPirteyKav.Rows[0]["KISUITOR"].ToString();
            drNesia["teur"] = dtPirteyKav.Rows[0]["DESCRIPTION"].ToString();
            drNesia["kav"] = dtPirteyKav.Rows[0]["SHILUT"].ToString();
            //drNesia["sug"] = dtPirteyKav.Rows[0]["SUGNAMAKNAME"].ToString();
            drNesia["makat"] = dtPirteyKav.Rows[0]["MAKAT8"].ToString();
            drNesia["DAKOT_HAGDARA"] = dtPirteyKav.Rows[0]["MAZANTICHNUN"].ToString();
            drNesia["HAGDARA_LEGMAR"] = dtPirteyKav.Rows[0]["MazanTashlum"].ToString();
            drNesia["MISPAR_KNISA"] = 0;
            drNesia["HOSEF_PEILUT"] = 1;
           
            drNesia["SUG_PEILUT"] = 2;
            drNesia["MUST_DAKOT"] = true;
            if (long.Parse(ViewState["MisRechev"].ToString()) > 0)
            { drNesia["MISPAR_RECHEV"] = ViewState["MisRechev"];
            drNesia["MISPAR_RISHUY"] = ViewState["MisparRishuy"];
            }
            drNesia["MUST_MIS_RECHEV"] = true;
            drNesia["ROW"] = dtPeiluyot.Rows.Count;
            drNesia["SHAT_YEZIA_DATE"] = txtHiddenTaarichCA.Value;
            dtPeiluyot.Rows.Add(drNesia);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void setNetuneyEmpty(ref DataTable dtPeiluyot, DataTable dtPirteyKav)
    {
        DataRow drNesia = dtPeiluyot.NewRow();
        try
        {
            drNesia["sug"] = "ריקה";
            drNesia["teur"] = dtPirteyKav.Rows[0]["DESCRIPTION"].ToString();
            drNesia["makat"] = dtPirteyKav.Rows[0]["MAKAT8"].ToString();
            drNesia["DAKOT_HAGDARA"] = dtPirteyKav.Rows[0]["MAZANTICHNUN"].ToString();
            drNesia["HAGDARA_LEGMAR"] = dtPirteyKav.Rows[0]["MazanTashlum"].ToString();
             drNesia["MISPAR_KNISA"] = 0;
           
             drNesia["HOSEF_PEILUT"] = 1;
            drNesia["SUG_PEILUT"] = 2;

            if (long.Parse(ViewState["MisRechev"].ToString()) > 0)
            {
                drNesia["MISPAR_RECHEV"] = ViewState["MisRechev"];
                drNesia["MISPAR_RISHUY"] = ViewState["MisparRishuy"];
            }
            drNesia["MUST_MIS_RECHEV"] = true;
            drNesia["ROW"] = dtPeiluyot.Rows.Count;
            drNesia["SHAT_YEZIA_DATE"] = txtHiddenTaarichCA.Value;
            dtPeiluyot.Rows.Add(drNesia);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnHosafa_OnClick(object sender, EventArgs e)
    {
        OBJ_PEILUT_OVDIM oObjPeiluyotOvdimIns;
        COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
        DataRow[] drPeiluyot;
        bool bValid;
        try
        {
            DataTable dtPeiluyot = (DataTable)Session["DtPeiluyot"];
            bValid=RefreshDataTable(ref dtPeiluyot);

            if (bValid)
            {
                if (SavePeilut.Value == "YES")
                {
                    drPeiluyot = dtPeiluyot.Select("HOSEF_PEILUT=1");
                    for (int i = 0; i < drPeiluyot.Length; i++)
                    {

                        oObjPeiluyotOvdimIns = new OBJ_PEILUT_OVDIM();
                        oObjPeiluyotOvdimIns.MISPAR_ISHI = int.Parse(txtHiddenMisparIshi.Value);
                        oObjPeiluyotOvdimIns.MISPAR_SIDUR = int.Parse(txtHiddenMisparSidur.Value);
                        oObjPeiluyotOvdimIns.TAARICH = DateTime.Parse(txtHiddenTaarichCA.Value);
                        oObjPeiluyotOvdimIns.SHAT_YETZIA = DateTime.Parse(drPeiluyot[i]["SHAT_YEZIA_DATE"].ToString());
                        oObjPeiluyotOvdimIns.SHAT_HATCHALA_SIDUR = DateTime.Parse(txtSidurDate.Value + " " + txtHiddenHourHatchaltSidur.Value);
                        oObjPeiluyotOvdimIns.MISPAR_KNISA = int.Parse(drPeiluyot[i]["MISPAR_KNISA"].ToString());
                        oObjPeiluyotOvdimIns.MAKAT_NESIA = int.Parse(drPeiluyot[i]["MAKAT"].ToString());
                        if (!string.IsNullOrEmpty(drPeiluyot[i]["MISPAR_RECHEV"].ToString()))
                            oObjPeiluyotOvdimIns.OTO_NO = int.Parse(drPeiluyot[i]["MISPAR_RECHEV"].ToString());
                        if (!string.IsNullOrEmpty(drPeiluyot[i]["KISUY_TOR_NEW"].ToString()))
                            oObjPeiluyotOvdimIns.KISUY_TOR = int.Parse(drPeiluyot[i]["KISUY_TOR_NEW"].ToString());
                        oObjPeiluyotOvdimIns.BITUL_O_HOSAFA = 2;
                        oObjPeiluyotOvdimIns.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                        if (!string.IsNullOrEmpty(drPeiluyot[i]["DAKOT_BAFOAL"].ToString()))
                            oObjPeiluyotOvdimIns.DAKOT_BAFOAL = int.Parse(drPeiluyot[i]["DAKOT_BAFOAL"].ToString());
                        if (int.Parse(drPeiluyot[i]["MISPAR_KNISA"].ToString()) > 0)
                            oObjPeiluyotOvdimIns.TEUR_NESIA = drPeiluyot[i]["TEUR"].ToString();

                        oCollPeilutOvdimIns.Add(oObjPeiluyotOvdimIns);
                    }
                        clOvdim oOvdim = new clOvdim();
                        oOvdim.DeleteHachanotMechonaInsertPeiluyot(int.Parse(txtHiddenMisparIshi.Value), int.Parse(txtHiddenMisparSidur.Value), DateTime.Parse(txtHiddenTaarichCA.Value),
                                            DateTime.Parse(txtSidurDate.Value + " " + txtHiddenHourHatchaltSidur.Value), oCollPeilutOvdimIns);
                        HttpRuntime.Cache.Remove(txtHiddenMisparIshi.Value + txtHiddenTaarichCA.Value);
                        ScriptManager.RegisterStartupScript(btnHosafa, btnHosafa.GetType(), "close", "window.returnValue=1;window.close();", true);

                   
                }
                else
                {
                    Session["DtPeiluyotNew"] = dtPeiluyot;
                    ScriptManager.RegisterStartupScript(btnHosafa, btnHosafa.GetType(), "close", "window.returnValue=1;window.close();", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(btnHosafa, btnHosafa.GetType(), "err", "alert('תקלה בשמירת הנתונים');window.close();", true);

        }
    }

  
    protected bool RefreshDataTable(ref DataTable dtSource)
    {
        string erech;
        bool cheak;
        bool bValid = true;
        string sShatYetzia,sShatYeziaDate;
        DataRow[] drData;
        sShatYeziaDate="";
        string shaaKayemet="";
        for (int i = 0; i < grdPeiluyot.Rows.Count; i++)
        {
            drData=dtSource.Select("ROW=" + grdPeiluyot.Rows[i].Cells[ROW].Text);
            if (drData.Length > 0)
            {
                sShatYetzia = ((TextBox)grdPeiluyot.Rows[i].Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Text;

                if (sShatYetzia != "&nbsp;")
                    drData[0]["SHAT_YETZIA"] = sShatYetzia;

                erech = ((HtmlInputHidden)grdPeiluyot.Rows[i].Cells[KISUY_TOR].FindControl("KisuyTorHidden")).Value;
                if (erech != "&nbsp;")
                    drData[0]["KISUY_TOR_NEW"] = erech;


                erech = ((TextBox)grdPeiluyot.Rows[i].Cells[MISPAR_RECHEV].FindControl("txtMisRechev")).Text;
                if (erech != "&nbsp;")
                    drData[0]["MISPAR_RECHEV"] = erech;

                erech = ((TextBox)grdPeiluyot.Rows[i].Cells[MISPAR_RISHUY].FindControl("lblMisparRishuy")).Text;
                if (erech != "&nbsp;")
                    drData[0]["MISPAR_RISHUY"] = erech;

                erech = ((TextBox)grdPeiluyot.Rows[i].Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Text;
                if (erech != "&nbsp;")
                    drData[0]["DAKOT_BAFOAL"] = erech;

                drData[0]["IS_VALID_MIS_RECHEV"] = ((TextBox)grdPeiluyot.Rows[i].Cells[tmpValidMisRechev].FindControl("txtIsMisRechevValid")).Text;

                drData[0]["SHAT_YEZIA_DATE"] = ((TextBox)grdPeiluyot.Rows[i].Cells[txtShatYeziaDate].FindControl("txtShatYeziaDate")).Text;
                sShatYeziaDate=drData[0]["SHAT_YEZIA_DATE"] .ToString();

                cheak = ((CheckBox)grdPeiluyot.Rows[i].Cells[HOSEF_PEILUT].FindControl("cbHosef")).Checked;
                if (cheak)
                {
                    drData[0]["HOSEF_PEILUT"] = "1";

                    if (sShatYetzia.Length > 0 && bValid)
                      if (IsPeilutAlreadyExist(sShatYeziaDate)){
                        bValid = false;
                        shaaKayemet = sShatYeziaDate;    
                      }
                }
                else
                    drData[0]["HOSEF_PEILUT"] = "0";
            }
        }

        if (!bValid)
        {
           BindGrid(dtSource);
           ScriptManager.RegisterStartupScript(btnHosafa, btnHosafa.GetType(), "err", " alert('קיימת פעילות בשעת היציאה " + shaaKayemet + ", יש לתקן את השעה');", true);
        }              
        return bValid;
    }

    protected void setAttributsElemnt()
    {
        txtMisparElement.Attributes.Add("KodValid", "true");
        txtTeurElement.Attributes.Add("TeurValid", "true");
        txtErechElement.Attributes.Add("ErechValid", "true");
        txtErechElement.Attributes.Add("Must", "false");
    }

    protected void grdPeiluyot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       TextBox Rechev;
       string makat;
       bool flag = true;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            SetUnvisibleColumns(e.Row);
        }
        else
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
            {
                if (((DataRowView)e.Row.DataItem).Row["SUG_PEILUT"].ToString() == "1")
                {
                    ((TextBox)e.Row.Cells[KISUY_TOR].FindControl("txtKisuiTor")).Style.Add("display", "none");
                    flag = false;
                 }
               
                makat = ((DataRowView)e.Row.DataItem).Row["MAKAT"].ToString();
                if (((makat.Substring(0, 3) == "700" || makat.Substring(0, 3) == "761" || makat.Substring(0, 3) == "784") && flag) || 
                    (int.Parse(((DataRowView)e.Row.DataItem).Row["MISPAR_KNISA"].ToString()) > 0 && ((DataRowView)e.Row.DataItem).Row["MUST_DAKOT"].ToString() == "false"))
                    ((CheckBox)e.Row.Cells[HOSEF_PEILUT].FindControl("cbHosef")).Attributes.Add("disabled", "true");
                if (((DataRowView)e.Row.DataItem).Row["MUST_DAKOT"].ToString() == "false" )
                    ((TextBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Style.Add("display", "none");
                
                
                ((CheckBox)e.Row.Cells[HOSEF_PEILUT].FindControl("cbHosef")).Attributes.Add("onclick", "SamenKnisot(" + (e.Row.RowIndex+1) + ")");
              //  ((CheckBox)e.Row.Cells[HOSEF_PEILUT].FindControl("cbHosef")).Checked = true;

                if (string.IsNullOrEmpty(((DataRowView)e.Row.DataItem).Row["KISUY_TOR"].ToString()))
                {
                    ((TextBox)e.Row.Cells[KISUY_TOR].FindControl("txtKisuiTor")).Enabled = false;
                    ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Attributes.Add("kisuyTor", "0");
                }
                else ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Attributes.Add("kisuyTor", ((DataRowView)e.Row.DataItem).Row["KISUY_TOR"].ToString());
               
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[KISUY_TOR].FindControl("exvldKisuiTor")).BehaviorID = e.Row.ClientID + "_vldExvldKisuiTor";
             ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtKisuiTor")).Attributes.Add("onchange", "onchange_txtKisuyTor(" + e.Row.ClientID + ")");
                if (!string.IsNullOrEmpty(((DataRowView)e.Row.DataItem).Row["MISPAR_KNISA"].ToString()))
                    if (int.Parse(((DataRowView)e.Row.DataItem).Row["MISPAR_KNISA"].ToString()) > 0)
                        ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Enabled = false;
                if (e.Row.Cells[hidShatYezia].Text != "&nbsp;")
                    ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Text = e.Row.Cells[hidShatYezia].Text;
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[SHAT_YETZIA].FindControl("exvldShatYezia")).BehaviorID = e.Row.ClientID + "_vldExvldShatYezia";
                ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).Attributes.Add("onchange", "onchange_txtShatYezia(" + e.Row.ClientID + ",true,'',"  + e.Row.RowIndex +",true)");
                if (e.Row.Cells[SHAT_YEZIA_DATE].Text.IndexOf("nbsp;") == -1)
                {
                    if (e.Row.Cells[SHAT_YEZIA_DATE].Text.Length > 10)
                    {
                        ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).ToolTip = e.Row.Cells[SHAT_YEZIA_DATE].Text.Split(' ')[1] + " " +
                                                                                                                 e.Row.Cells[SHAT_YEZIA_DATE].Text.Split(' ')[0];

                        if (!string.IsNullOrEmpty(((DataRowView)e.Row.DataItem).Row["KISUY_TOR"].ToString()))
                        {
                            ((TextBox)e.Row.Cells[KISUY_TOR].FindControl("txtKisuiTor")).Text = DateTime.Parse(((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).ToolTip).AddMinutes(-int.Parse(((DataRowView)e.Row.DataItem).Row["KISUY_TOR"].ToString())).ToString("HH:mm");

                        }
                    }
                }
               
                e.Row.Cells[DAKOT_HAGDARA].ToolTip =((DataRowView)e.Row.DataItem).Row["HAGDARA_LEGMAR"].ToString();
                Rechev = ((TextBox)e.Row.Cells[MISPAR_RECHEV].FindControl("txtMisRechev"));
                if (e.Row.Cells[hidMisRechev].Text != "&nbsp;")
                    Rechev.Text = e.Row.Cells[hidMisRechev].Text;
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[MISPAR_RECHEV].FindControl("exvMisRechev")).BehaviorID = e.Row.ClientID + "_vldExvldMisRechev";
                Rechev.Attributes.Add("onchange", "onchange_txtMisparRechev(" + e.Row.ClientID + ","  + e.Row.RowIndex +")");
                Rechev.Attributes.Add("onFocus ", "onSadeFocus(" + e.Row.ClientID + "_txtMisRechev)");
               

                ((TextBox)e.Row.Cells[MISPAR_RECHEV].FindControl("lblMisparRishuy")).Text = e.Row.Cells[MISPAR_RISHUY].Text;
                if (e.Row.Cells[MISPAR_RISHUY].Text.IndexOf("nbsp;") == -1 && Rechev.Text.Length>0)
                    Rechev.ToolTip = e.Row.Cells[MISPAR_RISHUY].Text;
                Rechev.Attributes.Add("Is_Required", ((DataRowView)e.Row.DataItem).Row["MUST_MIS_RECHEV"].ToString());
                if (((DataRowView)e.Row.DataItem).Row["MUST_MIS_RECHEV"].ToString() == "false")
                {
                    if (makat.Substring(0, 3) == "761" || makat.Substring(0, 3) == "784")
                        Rechev.Style.Add("display", "none");
                    else Rechev.Attributes.Add("disabled", "true");
                 }

                ((TextBox)e.Row.Cells[MISPAR_RECHEV].FindControl("lblMisparRishuy")).Style.Add("display", "none");
               
                if (e.Row.Cells[hidDakotBafoal].Text != "&nbsp;")
                    ((TextBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Text = e.Row.Cells[hidDakotBafoal].Text;
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[DAKOT_BAFOAL].FindControl("exvDakot")).BehaviorID = e.Row.ClientID + "_vldExvldDakot";
                ((TextBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Attributes.Add("onchange", "onchange_txtDakot(" + e.Row.ClientID + ")");
                ((TextBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Attributes.Add("onFocus ", "onSadeFocus(" + e.Row.ClientID + "_txtDakotBafoal)");

                if (e.Row.Cells[hidHosefPeilut].Text == "1")
                    ((CheckBox)e.Row.Cells[HOSEF_PEILUT].FindControl("cbHosef")).Checked = true;
               
                 ((TextBox)e.Row.Cells[tmpValidMisRechev].FindControl("txtIsMisRechevValid")).Text = e.Row.Cells[IS_VALID_MIS_RECHEV].Text;
                ((TextBox)e.Row.Cells[txtShatYeziaDate].FindControl("txtShatYeziaDate")).Text =e.Row.Cells[SHAT_YEZIA_DATE].Text;
                
                SetUnvisibleColumns(e.Row);

                
            }


    }

    private void SetUnvisibleColumns(GridViewRow CurrentRow)
    {

        CurrentRow.Cells[hidShatYezia].Style.Add("display", "none");
        CurrentRow.Cells[hidMisRechev].Style.Add("display", "none");
        CurrentRow.Cells[hidDakotBafoal].Style.Add("display", "none");
        CurrentRow.Cells[hidHosefPeilut].Style.Add("display", "none");
         CurrentRow.Cells[IS_VALID_MIS_RECHEV].Style.Add("display", "none");
        CurrentRow.Cells[tmpValidMisRechev].Style.Add("display", "none");
        CurrentRow.Cells[MISPAR_KNISA].Style.Add("display", "none");
        CurrentRow.Cells[SHAT_YEZIA_DATE].Style.Add("display", "none");
        CurrentRow.Cells[txtShatYeziaDate].Style.Add("display", "none");
        CurrentRow.Cells[MISPAR_RISHUY].Style.Add("display", "none");
        CurrentRow.Cells[ROW].Style.Add("display", "none");
        CurrentRow.Cells[SUG_KNISA].Style.Add("display", "none");
    }

    private bool IsPeilutAlreadyExist(string sShatYetzia)
    {
        DataTable dtPeiluyot;
        DataRow[] drPeilut;
        bool bExists = false;
        try
        {
            if (Session["dtPeiluyotLesidur"]==null)
            {
                clOvdim oOvdim = new clOvdim();
                dtPeiluyot = oOvdim.GetPeiluyotLeOved(int.Parse(txtHiddenMisparIshi.Value), int.Parse(txtHiddenMisparSidur.Value), DateTime.Parse(txtHiddenTaarichCA.Value), DateTime.Parse(txtSidurDate.Value + " " + txtHiddenHourHatchaltSidur.Value));
                Session.Add("dtPeiluyotLesidur", dtPeiluyot);
            }
            dtPeiluyot = (DataTable)Session["dtPeiluyotLesidur"];
            drPeilut = dtPeiluyot.Select("SUBSTRING(convert(makat_nesia,'System.String'),1,3)<>701 and SUBSTRING(convert(makat_nesia,'System.String'),1,3)<>711 and SUBSTRING(convert(makat_nesia,'System.String'),1,3)<>712 and SHAT_YETZIA=Convert('" + sShatYetzia + "', 'System.DateTime')"); 
            if (drPeilut.Length>0)
                bExists=true;
            //drPeilut = dtPeiluyot.Select("SUBSTRING(convert(makat_nesia,'System.String'),1,3)<>701 and SUBSTRING(convert(makat_nesia,'System.String'),1,3)<>711 and SUBSTRING(convert(makat_nesia,'System.String'),1,3)<>712 and SHAT_YETZIA=Convert('" + DateTime.Parse(txtSidurDate.Value).AddDays(1).ToShortDateString() + " " + sShatYetzia + "', 'System.DateTime')");
            //if (drPeilut.Length > 0)
            //    bExists = true;
         
            return bExists;  
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
