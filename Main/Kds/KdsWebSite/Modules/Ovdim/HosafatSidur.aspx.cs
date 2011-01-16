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
using KdsLibrary.UI;
using KdsLibrary;
using KdsBatch;
using KdsLibrary.BL;
using KdsLibrary.Security;
using System.Text;
using KdsLibrary.UDT;



//using System.Collections.Generic;

//using System.Collections.Specialized;
//using KdsLibrary.DAL;

//using KdsLibrary.Utils;

//using KdsWorkFlow.Approvals;

public partial class Modules_Ovdim_HosafatSidur : KdsPage
{
    private enum enPeilut
    {
        hidKisuyTor = 0,
        KISUY_TOR,
        hidShatYezia,
        SHAT_YETZIA,
        TEUR,
        KAV,
        SUG,
        hidMisRechev,
        MISPAR_RECHEV,
        hidMisRishuy,
        MISPAR_RISHUY,
        hidMakat,
        MAKAT,
        DAKOT_HAGDARA,
        hidDakotBafoal,
        DAKOT_BAFOAL,
        hidHosefPeilut,
        HOSEF_PEILUT,
        PEILUT_CHOVA,
        IS_VALID_MAKAT,
        tmpValidMakat,
        IS_VALID_MIS_RECHEV,
        tmpValidMisRechev,
        PRATIM,
        SHAT_YEZIA_DATE,
        txtShatYeziaDate
    }

    public const int SHAT_YETZIA = 3;
    public const int TEUR = 4;
    public const int MISPAR_RECHEV = 8;
    public const int MISPAR_RISHUY = 10;
    public const int MAKAT = 12;
    public const int HOSEF_PEILUT = 17;
    public const int PEILUT_CHOVA = 18;
    public const int PRATIM = 23;
    public const int TXT_SHAT_YETZIA = 25;
    public const int MISPAR_KNISA = 26;
    

    
    protected void Page_Load(object sender, EventArgs e)
    {
        ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
        btnIdkunGridHidden.Style.Add("Display", "none");
        clUtils oUtils = new clUtils();
        DataTable dtParametrim = new DataTable();
        DataTable dtElements = new DataTable();
        DataTable dtSidurimMeyuchadim = new DataTable();
       
        if (!Page.IsPostBack)
        {
          //  btnMapa.Style["disabled"] = "disabled";
            SetFixedHeaderGrid(pnlgrdPeiluyot.ClientID, Header);
            sugSidur.Value = "1";
            AautoTeurSidur.ContextKey = "27,53;99200,99214";
            AautoKodSidur.ContextKey = "27,53;99200,99214";
            if (Request.QueryString["CardDate"] != null)
            {
                TaarichCA.Value = Request.QueryString["CardDate"].ToString(); //"26/05/2009"; //
            }
            if (Request.QueryString["EmpID"] != null)
            {
                MisparIshi.Value =  Request.QueryString["EmpID"].ToString();
            }
          //  txtShatGmar.Attributes.Add("Date","");

            //שליפת פרמטרים חיצוניים ושמירתם
            dtParametrim = oUtils.getErechParamByKod("1,3,4,29,30,80,93,98,244", TaarichCA.Value);
            if (dtParametrim.Rows.Count > 0)
            {
                for (int i = 0; i < dtParametrim.Rows.Count; i++)
                    Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());
            }
            dtElements = oUtils.GetElementsVeMeafyenim(TaarichCA.Value);
            Session["ELEMENTS"] = dtElements;
          //  cbMisSidur.Enabled = false;
            dtSidurimMeyuchadim = oUtils.getSidurimMeyuchadim(DateTime.Parse(TaarichCA.Value), DateTime.Parse(TaarichCA.Value));
            if (dtSidurimMeyuchadim.Rows.Count > 0)
            {
                SidureyEadrut.Value = ",99200,99214,";
                for (int i = 0; i < dtSidurimMeyuchadim.Rows.Count; i++)
                    if (int.Parse(dtSidurimMeyuchadim.Rows[i]["Kod_Meafyen"].ToString()) == 53 || int.Parse(dtSidurimMeyuchadim.Rows[i]["Kod_Meafyen"].ToString()) == 27)
                        SidureyEadrut.Value = SidureyEadrut.Value + dtSidurimMeyuchadim.Rows[i]["MISPAR_SIDUR"].ToString() + ",";
            }
            SetFocus();
            HiddenTakin.Value = "true";
        }
    }

    private void SetFocus()
    {
        txtMisSiduri.Attributes.Add("onFocus ", "onSadeFocus(txtMisSiduri)");
        txtMisSidurMapa.Attributes.Add("onFocus ", "onSadeFocus(txtMisSidurMapa)");
        txtTeurSidur.Attributes.Add("onFocus ", "onSadeFocus(txtTeurSidur)");
    }
    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        int type;
        DataTable dtSource = new DataTable();
        try
        {

            type = int.Parse(sugSidur.Value);
           
                txtShatHatchala.Text = "";
                txtShatHatchala.ToolTip = "";
                txtShatGmar.Text = "";
                txtShatGmar.ToolTip = "";
                YeziratDTSource(ref dtSource);
                btnHosafatPeilut.Disabled = false;
                if (type == 1)//סידור מפה
                {
                    txtTeurSidur.Text = "";
                    lblMisSidur.Text = txtMisSidurMapa.Text;
                    InitializeSidurMapa(ref dtSource);
                    //   txtMisSidurMapa.Visible = true;
                    txtMisSiduri.Text = txtMisSidurMapa.Text;
                   
                }
                else //סידור מיוחד
                {
                    //   txtTeurSidur.Text = txtTeurSidur.Text;
                    lblMisSidur.Text = txtMisSiduri.Text;
                    InitializeSidurMeyucahd(ref dtSource);
                    trMsgNextDay.Style["display"] = "none";
                }
                BindGridPeiluyot();
                pirteySidur.Style["display"] = "inline";
                txtShatHatchala.Focus();
                btnMapa.Style["disabled"]="";
                btnMeyuchad.Style["disabled"] = "";
                //if (txtShatHatchala.Attributes["NEXT"].ToString() =="true")
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + lblYomHaba.Text + "');", true);
            
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void InitializeSidurMapa(ref DataTable dtSource)
    {
        clKavim oKavim = new clKavim();
        int result;
        DataSet dsSidur = new DataSet();

        clUtils oUtils = new clUtils();
        DataTable dtMeafyeneySidur = new DataTable();
        //   DataTable dtMustElemnts = new DataTable();
        DataRow[] drSelect;
        string sSQL = "";
        string shaa;
        DateTime shatYezia = new DateTime();
        DateTime shatGmar = new DateTime();
        DateTime shatHatchala = new DateTime();
        try
        {
            dsSidur = oKavim.GetSidurAndPeiluyotFromTnua(int.Parse(lblMisSidur.Text), DateTime.Parse(TaarichCA.Value),1,out result);
            //     dtMustElemnts = (DataTable)Session["ELEMENTS"];
            if (result == 0)
            {
                //שעת התחלה ושעת גמר
                if (dsSidur.Tables[1].Rows.Count > 0)
                {

                    shatHatchala = clGeneral.GetDateTimeFromStringHour(dsSidur.Tables[1].Rows[0]["SHAA"].ToString(),DateTime.Parse(TaarichCA.Value)); 
                    shaa = shatHatchala.ToShortTimeString();
                    txtShatHatchala.Text = shaa;
                    TaarichHatchala.Value = shaa + " ";
                    if (shatHatchala.Date > DateTime.Parse(TaarichCA.Value))
                    {
                    //    txtShatHatchala.Attributes.Add("NEXT", "true");
                    //    lblYomHaba.Text = lblYomHaba.Text + DateTime.Parse(TaarichCA.Value).AddDays(1).ToShortDateString();
                    //    trMsgNextDay.Style["display"] = "inline";
                        TaarichHatchala.Value = TaarichHatchala.Value + DateTime.Parse(TaarichCA.Value).AddDays(1).ToShortDateString();
                    }
                    else
                    {
                    //    txtShatHatchala.Attributes.Add("NEXT", "false");                     
                    //    trMsgNextDay.Style["display"] = "none";
                        TaarichHatchala.Value = TaarichHatchala.Value + DateTime.Parse(TaarichCA.Value).ToShortDateString();
                    }
                    txtShatHatchala.ToolTip = TaarichHatchala.Value;
                    if (!string.IsNullOrEmpty(dsSidur.Tables[1].Rows[dsSidur.Tables[1].Rows.Count - 1]["MAZANTASHLUM"].ToString()))
                    {
                        shatGmar = clGeneral.GetDateTimeFromStringHour(dsSidur.Tables[1].Rows[dsSidur.Tables[1].Rows.Count - 1]["SHAA"].ToString(), DateTime.Parse(TaarichCA.Value));
                        shatGmar = shatGmar.AddMinutes(int.Parse(dsSidur.Tables[1].Rows[dsSidur.Tables[1].Rows.Count - 1]["MAZANTASHLUM"].ToString()));
                        txtShatGmar.Text = shatGmar.ToShortTimeString();

                        TaarichGmar.Value = shatGmar.ToString().Split(' ')[1] + " " + shatGmar.ToString().Split(' ')[0];
                        txtShatGmar.ToolTip = TaarichGmar.Value;
                    }
                    else TaarichGmar.Value = "";
                    
                }

                foreach (DataRow dr in dsSidur.Tables[1].Rows)
                {
                    DataRow drSource = dtSource.NewRow();
                    int type=0;
                    shatYezia = clGeneral.GetDateTimeFromStringHour(dr["SHAA"].ToString(),DateTime.Parse(TaarichCA.Value));
                    drSource["SHAT_YETZIA"] = shatYezia.ToShortTimeString();
                    drSource["SHAT_YEZIA_DATE"] = shatYezia;
                    drSource["KAV"] = dr["SHILUT"];
                    drSource["HOSEF_PEILUT"] = "0";
                    drSource["MUST_DAKOT"] = true;
                    drSource["DAKOT_HAGDARA"] = dr["MAZANTICHNUN"];
                    drSource["HAGDARA_LEGMAR"] = dr["MazanTashlum"];
                    if (dr["KISUITOR"].ToString().Length > 0 && dr["KISUITOR"].ToString() != "0")
                    {
                        drSource["KISUY_TOR"] = getKisuyTorString(int.Parse(dr["KISUITOR"].ToString()), shatYezia);
                        //shatYezia = shatYezia.AddMinutes(int.Parse(dr["KISUITOR"].ToString()) * (-1));
                        //shaa = shatYezia.ToString().Split(' ')[1];
                        //drSource["KISUY_TOR"] = shaa.Split(':')[0] + ":" + shaa.Split(':')[1];
                    }
                    if(dr["SiduriKnisa"]!=System.DBNull.Value)
                    {
                        drSource["MISPAR_KNISA"]=dr["SiduriKnisa"];
                        drSource["SUG_KNISA"] = dr["SugKnisa"];
                        drSource["SUG"] = "כניסה";

                        if (dr["SiduriKnisa"] != System.DBNull.Value && int.Parse(dr["SiduriKnisa"].ToString()) > 0)
                        {
                            drSource["DAKOT_HAGDARA"] = "0";
                            if (dr["TeurNesiaa"].ToString() != dr["YeshuvName"].ToString() && dr["YeshuvName"].ToString().Length > 0)

                                drSource["TEUR"] = "כנ-" + dr["YeshuvName"].ToString() + ", " + dr["TeurNesiaa"].ToString();
                            else
                                drSource["TEUR"] = "כנ-" + dr["TeurNesiaa"].ToString();
                        }
                        if (dr["SugKnisa"].ToString() == "2")
                        {
                        //    drNesia["sug"] = "כניסה חובה";
                            drSource["MUST_DAKOT"] = "false";
                        }
                        else
                        {
                          //  drNesia["sug"] = "כניסה לפי צורך";
                            drSource["MUST_DAKOT"] = true;
                            //drNesia["teur"] = drNesia["teur"] + " (לפי-צורך)";
                        }
                    }
                    else
                    {
                        drSource["MISPAR_KNISA"] = 0;
                        drSource["SUG"] = dr["SUGSHIRUTNAME"];
                        drSource["TEUR"] = dr["TEURNESIAA"];
                        if (dr["MAKAT"].ToString().Substring(0, 3) == "700" || dr["MAKAT"].ToString().Substring(0, 3) == "761"
                             || dr["MAKAT"].ToString().Substring(0, 3) == "784")
                        {
                            drSource["MUST_DAKOT"] = "false";
                            drSource["DAKOT_HAGDARA"] = "0";
                        }
                    }
                    //    num = dr["MAKAT8"].ToString().Length;
                  
                    drSource["MAKAT"] = dr["MAKAT8"];
                    
                    
                    drSource["HOSEF_PEILUT"] = "0";

                    drSource["PRATIM"] = getPratimLeMakat(int.Parse(dr["MAKAT8"].ToString()), ref drSource, ref type);

                    drSource["PRATIM"] = drSource["PRATIM"] + ";KISUY_TOR=" + dr["KISUITOR"].ToString(); 
                    if (type==5)
                        drSource["PRATIM"] += ";ENABLE_MAKAT=True";
                    else drSource["PRATIM"] += ";ENABLE_MAKAT=False";
                    dtSource.Rows.Add(drSource);
                }
                Session["DataSource"] = dtSource;
               
                //הוצאת סוג גמר
                //if ( txtShatHatchala.Attributes["NEXT"].ToString() =="true")
                //    dtMeafyeneySidur = oUtils.InitDtMeafyeneySugSidur(DateTime.Parse(TaarichCA.Value), DateTime.Parse(TaarichCA.Value).AddDays(1));
                //else
                dtMeafyeneySidur = oUtils.InitDtMeafyeneySugSidur(DateTime.Parse(TaarichCA.Value), DateTime.Parse(TaarichCA.Value));
                sSQL = "SUG_SIDUR ='" + dsSidur.Tables[0].Rows[0]["SUGSIDUR"].ToString() + "' AND KOD_MEAFYEN='3'";
                drSelect = dtMeafyeneySidur.Select(sSQL);
                txtShatGmar.Attributes.Add("SugGmar", drSelect[0]["ERECH"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected string getKisuyTorString(int kisuyTor, DateTime shatYezia)
    {
        string shaa;
        try
        {
            shatYezia = shatYezia.AddMinutes(kisuyTor * (-1));
            shaa = shatYezia.ToString().Split(' ')[1];
            return (shaa.Split(':')[0] + ":" + shaa.Split(':')[1]);
                
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected string getPratimLeMakat(long makat, ref DataRow drSource,ref int type)
    {
        DataTable dtMustElemnts = new DataTable();
        clKavim oKavim = new clKavim();
        DataRow[] drSelect;
        string sSQL = "";
      //  int sug;
        string pratim = "";
        try
        {
            type = oKavim.GetMakatType(makat);
            if (type == 5)
            {
                dtMustElemnts = (DataTable)Session["ELEMENTS"];
                sSQL = "KOD_ELEMENT='" + makat.ToString().Substring(1, 2) + "' AND KOD_MEAFYEN='11'";
                drSelect = dtMustElemnts.Select(sSQL);
                if (drSelect.Length > 0)
                    pratim = "RECHEV_CHOVA=1";
                else pratim = "RECHEV_CHOVA=0";

                drSource["DAKOT_HAGDARA"] = "0";
                drSource["HAGDARA_LEGMAR"] = "";
                drSource["MUST_DAKOT"] = "false";
            }
            else pratim = "RECHEV_CHOVA=1";
            pratim = pratim + ";SUG_MAKAT=" + type;

            return pratim;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void InitializeSidurMeyucahd(ref DataTable dtSource)
    {
        int KodSidur;
        int type = 0;
        DataTable dtMustElemnts = new DataTable();
        clUtils oUtils = new clUtils();
        clOvdim oOvdim = new clOvdim();
        DataTable dtArachim = new DataTable();
        DataRow[] drSelect;
        string sSQL = "";
        string erech;
        bool enable_makat = false;

        try
        {
           // txtShatHatchala.Attributes.Add("NEXT", "false");
            KodSidur = int.Parse(lblMisSidur.Text);
            dtMustElemnts = oUtils.GetMeafyenSidurByKodSidur(KodSidur, TaarichCA.Value);
            sSQL = "KOD_MEAFYEN =46";
            drSelect = dtMustElemnts.Select(sSQL);
            if (drSelect.Length > 0)
            {
                if (drSelect[0]["KOD_MEAFYEN"].ToString() == "46" && drSelect[0]["ERECH"].ToString() == "1")
                    btnHosafatPeilut.Disabled = true;
            }
            else
            {
                btnHosafatPeilut.Disabled = false;
                sSQL = "KOD_MEAFYEN in (93,94,95,45)";
                drSelect = dtMustElemnts.Select(sSQL);
                for (int i = 0; i < drSelect.Length; i++)
                {
                    enable_makat = false;
                    DataRow dr = dtSource.NewRow();
                    dr["HOSEF_PEILUT"] = "1";
                    if (drSelect[i]["KOD_MEAFYEN"].ToString() == "45")
                        dr["MAKAT"] = "50000000";
                    else{
                        erech = int.Parse(drSelect[i]["ERECH"].ToString()) < 10 ? "0" + drSelect[i]["ERECH"].ToString() : drSelect[i]["ERECH"].ToString();
                        dr["MAKAT"] = "7" + erech + "00000";
                        enable_makat = true;
                    }
                    dr["MUST_DAKOT"] = "false";
                    dr["DAKOT_HAGDARA"] = "0";
                    dr["PEILUT_CHOVA"] = "1";
                    dr["MISPAR_KNISA"] = "0";
                    dr["PRATIM"] = getPratimLeMakat(int.Parse(dr["MAKAT"].ToString()), ref dr, ref type);
                    dr["PRATIM"] += ";KISUY_TOR=;ENABLE_MAKAT=" + enable_makat; 
                    dtSource.Rows.Add(dr);
                }
            }
           
            Session["DataSource"] = dtSource;

            //מגבלות
            sSQL = "KOD_MEAFYEN in (7,8)";
            drSelect = dtMustElemnts.Select(sSQL);
            MustMeafyenim.Attributes.Remove("Meafyen7");
            MustMeafyenim.Attributes.Remove("Meafyen8");
            if (drSelect.Length > 0)
            {
                for (int i = 0; i < drSelect.Length; i++)
                    MustMeafyenim.Attributes.Add("Meafyen" + drSelect[i]["KOD_MEAFYEN"].ToString(), drSelect[i]["ERECH"].ToString());
                MustMeafyenim.Value = "1";
            }
            else MustMeafyenim.Value = "0";

            //  הוצאת סוג גמר נהגות או מנהל או מפעיל 
            sSQL = "KOD_MEAFYEN =54";
            drSelect = dtMustElemnts.Select(sSQL);
            if (drSelect.Length > 0)
            {
                dtArachim = oOvdim.GetArachimLeOved(int.Parse(MisparIshi.Value),DateTime.Parse(TaarichCA.Value));
                sSQL = "KOD_NATUN=6 and ERECH in('0122', '0123', '0124', '0127')";
                drSelect = dtArachim.Select(sSQL);
                if (drSelect.Length > 0)
                    txtShatGmar.Attributes.Add("SugGmar", drSelect[0]["ERECH"].ToString());
                else txtShatGmar.Attributes.Add("SugGmar", "");
            }
            else
            {
                sSQL = "KOD_MEAFYEN =3";
                drSelect = dtMustElemnts.Select(sSQL);
                if (drSelect.Length > 0)
                    txtShatGmar.Attributes.Add("SugGmar", drSelect[0]["ERECH"].ToString());
                else
                    txtShatGmar.Attributes.Add("SugGmar", "");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void YeziratDTSource(ref DataTable dtSource)
    {
        dtSource.Columns.Add("KISUY_TOR");
        dtSource.Columns.Add("SHAT_YETZIA");
        dtSource.Columns.Add("TEUR");
        dtSource.Columns.Add("KAV");
        dtSource.Columns.Add("SUG");
        dtSource.Columns.Add("MISPAR_KNISA");
        dtSource.Columns.Add("SUG_KNISA");
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
        dtSource.Columns.Add("PRATIM");
        dtSource.Columns.Add("SHAT_YEZIA_DATE");
        dtSource.Columns.Add("MUST_DAKOT");
    }
    protected void BindGridPeiluyot()
    {
        DataView dv;
        try
        {
            dv = new DataView((DataTable)Session["DataSource"]);
           // dv.Sort = "SHAT_YETZIA ASC, MISPAR_KNISA ASC";
            grdPeiluyot.DataSource = dv;
            if (dv.Table.Rows.Count == 0)
            {
                tsEmpty.Visible = true;
                dv.Table.Rows.Add(dv.Table.NewRow());
                grdPeiluyot.DataBind();
                grdPeiluyot.Rows[0].Style.Add("Display", "none");  //Visible = false;
                ((HtmlAnchor)grdPeiluyot.HeaderRow.FindControl("lbSamenHakol")).Disabled = true;
                ((HtmlAnchor)grdPeiluyot.HeaderRow.FindControl("lbNake")).Disabled = true;

               
            }
            else
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                    if (dv.Table.Rows[i]["MAKAT"].ToString() == "")
                    {
                        dv.Table.Rows.RemoveAt(i);
                        break;
                    }
                grdPeiluyot.DataBind();
                tsEmpty.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdPeiluyot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string makat;
        string[] pratim;
        TextBox Rechev;
        DataTable dtElements;
        DataRow[] drSelect;
        string sSQL = "";
        if (e.Row.RowType == DataControlRowType.Header)
        {
            SetUnvisibleColumns(e.Row);
        }
        else
            if ( e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
            {
                //(tsEmpty.Visible == false)
                if (e.Row.Cells[(int)enPeilut.hidKisuyTor].Text != "&nbsp;")
                    ((TextBox)e.Row.Cells[(int)enPeilut.KISUY_TOR].FindControl("txtKisuiTor")).Text = e.Row.Cells[(int)enPeilut.hidKisuyTor].Text;
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[(int)enPeilut.KISUY_TOR].FindControl("exvldKisuiTor")).BehaviorID = e.Row.ClientID + "_vldExvldKisuiTor";
                ((TextBox)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("txtKisuiTor")).Attributes.Add("onchange", "onchange_txtKisuyTor(" + e.Row.ClientID + ")");

                if (e.Row.Cells[(int)enPeilut.hidShatYezia].Text != "&nbsp;")
                    ((TextBox)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("txtShatYezia")).Text = e.Row.Cells[(int)enPeilut.hidShatYezia].Text;
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("exvldShatYezia")).BehaviorID = e.Row.ClientID + "_vldExvldShatYezia";
                ((TextBox)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("txtShatYezia")).Attributes.Add("onchange", "onchange_txtShatYezia(" + e.Row.ClientID + ",true,'')");
                if (e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text.IndexOf("nbsp;") == -1)
                {
                    if (e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text.Split(' ').Length > 1)
                        ((TextBox)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("txtShatYezia")).ToolTip = " תאריך שעת היציאה הוא " + e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text.Split(' ')[1] + " " +
                                                                                                                 e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text.Split(' ')[0];
                    else ((TextBox)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("txtShatYezia")).ToolTip = " תאריך שעת היציאה הוא " + e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text;

                }
              //  ((HtmlInputHidden)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("DateHidden")).Value = 
                if (!string.IsNullOrEmpty(((DataRowView)e.Row.DataItem).Row["MISPAR_KNISA"].ToString()))
                {
                    if (int.Parse(((DataRowView)e.Row.DataItem).Row["MISPAR_KNISA"].ToString()) > 0)
                    {
                        ((TextBox)e.Row.Cells[(int)enPeilut.SHAT_YETZIA].FindControl("txtShatYezia")).Enabled = false;
                        if (int.Parse(((DataRowView)e.Row.DataItem).Row["SUG_KNISA"].ToString()) == 3)
                            ((TextBox)e.Row.Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Style.Add("display", "block");
                        else
                            ((TextBox)e.Row.Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Style.Add("display", "none");
                    }
                }
                e.Row.Cells[(int)enPeilut.DAKOT_HAGDARA].ToolTip = ((DataRowView)e.Row.DataItem).Row["HAGDARA_LEGMAR"].ToString();
                Rechev = ((TextBox)e.Row.Cells[(int)enPeilut.MISPAR_RECHEV].FindControl("txtMisRechev"));
                if (e.Row.Cells[(int)enPeilut.hidMisRechev].Text != "&nbsp;")
                    Rechev.Text = e.Row.Cells[(int)enPeilut.hidMisRechev].Text;
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[(int)enPeilut.MISPAR_RECHEV].FindControl("exvMisRechev")).BehaviorID = e.Row.ClientID + "_vldExvldMisRechev";
                Rechev.Attributes.Add("onchange", "onchange_txtMisparRechev(" + e.Row.ClientID + "," + (e.Row.RowIndex+1) +")");
                Rechev.Attributes.Add("onFocus ", "onSadeFocus(" + e.Row.ClientID + "_txtMisRechev)");
                
             //   ((TextBox)e.Row.Cells[(int)enPeilut.MISPAR_RISHUY].FindControl("lblMisparRishuy")).Text = e.Row.Cells[(int)enPeilut.hidMisRishuy].Text;
                if (e.Row.Cells[(int)enPeilut.hidMisRishuy].Text != "&nbsp;")
                    Rechev.ToolTip = e.Row.Cells[(int)enPeilut.hidMisRishuy].Text;
             //?   if (e.Row.Cells[(int)enPeilut.hidMisRishuy].Text != "&nbsp;")
             //?       ((HtmlInputText)e.Row.Cells[(int)enPeilut.MISPAR_RISHUY].FindControl("lblMisparRishuy")).Value = e.Row.Cells[(int)enPeilut.hidMisRishuy].Text;

                makat = e.Row.Cells[(int)enPeilut.hidMakat].Text;
                if (makat != "&nbsp;")
                {
                    ((TextBox)e.Row.Cells[(int)enPeilut.MAKAT].FindControl("txtMakat")).Text = makat;
                  //  if (makat.Substring(1,3)!= "93" && makat.Substring(1,3)!= "94" && makat.Substring(1,3)!="95")
                     //   ((TextBox)e.Row.Cells[(int)enPeilut.MAKAT].FindControl("txtMakat")).Enabled = false;
                    //if (e.Row.Cells[(int)enPeilut.hidMakat].Text == "50000000")//מקט ויזה לא ניתן לשינוי
                        
                }

               // ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[(int)enPeilut.MISPAR_RECHEV].FindControl("exvMakat")).BehaviorID = e.Row.ClientID + "_vldExvldMakat";
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[(int)enPeilut.MAKAT].FindControl("exvMakat")).BehaviorID = e.Row.ClientID + "_vldExvldMakat";
                ((TextBox)e.Row.Cells[(int)enPeilut.MAKAT].FindControl("txtMakat")).Attributes.Add("onchange", "onchange_txtMakat(" + e.Row.ClientID + ")");
                ((TextBox)e.Row.Cells[(int)enPeilut.MAKAT].FindControl("txtMakat")).Attributes.Add("onFocus ", "onSadeFocus(" + e.Row.ClientID + "_txtMakat)");

                if (e.Row.Cells[(int)enPeilut.hidDakotBafoal].Text != "&nbsp;")
                    ((TextBox)e.Row.Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Text = e.Row.Cells[(int)enPeilut.hidDakotBafoal].Text;
                ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("exvDakot")).BehaviorID = e.Row.ClientID + "_vldExvldDakot";
                ((TextBox)e.Row.Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Attributes.Add("onchange", "onchange_txtDakot(" + e.Row.ClientID + ")");
                ((TextBox)e.Row.Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Attributes.Add("onFocus ", "onSadeFocus(" + e.Row.ClientID + "_txtDakotBafoal)");
                if (((DataRowView)e.Row.DataItem).Row["MUST_DAKOT"].ToString() == "false") {
                    ((TextBox)e.Row.Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Style.Add("display", "none");
                }
                if (((DataRowView)e.Row.DataItem).Row["MISPAR_KNISA"].ToString() !="")
                    if (int.Parse(((DataRowView)e.Row.DataItem).Row["MISPAR_KNISA"].ToString()) > 0 && ((DataRowView)e.Row.DataItem).Row["MUST_DAKOT"].ToString() == "false")
                        ((CheckBox)e.Row.Cells[(int)enPeilut.HOSEF_PEILUT].FindControl("cbHosef")).Attributes.Add("disabled", "true");
                if (makat.Substring(0, 3) == "700" || makat.Substring(0, 3) == "761" || makat.Substring(0, 3) == "784")
                    ((CheckBox)e.Row.Cells[(int)enPeilut.HOSEF_PEILUT].FindControl("cbHosef")).Attributes.Add("disabled", "true");
                if (e.Row.Cells[(int)enPeilut.hidHosefPeilut].Text == "1")
                    ((CheckBox)e.Row.Cells[(int)enPeilut.HOSEF_PEILUT].FindControl("cbHosef")).Checked = true;
                if (e.Row.Cells[(int)enPeilut.PEILUT_CHOVA].Text == "1")
                {
                    ((CheckBox)e.Row.Cells[(int)enPeilut.HOSEF_PEILUT].FindControl("cbHosef")).Checked = true;
                    ((CheckBox)e.Row.Cells[(int)enPeilut.HOSEF_PEILUT].FindControl("cbHosef")).Enabled = false;
                }
                ((CheckBox)e.Row.Cells[HOSEF_PEILUT].FindControl("cbHosef")).Attributes.Add("onclick", "SamenKnisot(" + (e.Row.RowIndex + 1) + ")");

                ((TextBox)e.Row.Cells[(int)enPeilut.tmpValidMakat].FindControl("txtIsMakatValid")).Text = e.Row.Cells[(int)enPeilut.IS_VALID_MAKAT].Text;
                ((TextBox)e.Row.Cells[(int)enPeilut.tmpValidMisRechev].FindControl("txtIsMisRechevValid")).Text = e.Row.Cells[(int)enPeilut.IS_VALID_MIS_RECHEV].Text;
                ((TextBox)e.Row.Cells[(int)enPeilut.txtShatYeziaDate].FindControl("txtShatYeziaDate")).Text = e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text;
                //if (e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text != "&nbsp;")
                //    ((TextBox)e.Row.Cells[(int)enPeilut.t].FindControl("txtShatYeziaDate")).Text = e.Row.Cells[(int)enPeilut.hidShatYezia].Text;

                SetUnvisibleColumns(e.Row);

                if (e.Row.Cells[(int)enPeilut.PRATIM].Text != "&nbsp;")
                {
                    pratim = e.Row.Cells[(int)enPeilut.PRATIM].Text.Split(';');
                    Rechev.Attributes.Add("Is_Required", pratim[0].Split('=')[1]);
                    if ((Rechev.Text == "&nbsp;" || Rechev.Text == "") && pratim[0].Split('=')[1] == "1")
                        Rechev.Text = "0";
                    if (pratim[0].Split('=')[1] == "0" || makat.Substring(0, 3) == "701" || makat.Substring(0, 3) == "711" || makat.Substring(0, 3) == "712")
                        ((TextBox)e.Row.Cells[(int)enPeilut.MISPAR_RECHEV].FindControl("txtMisRechev")).Enabled = false;
                    if (pratim[1].Split('=')[1] != "1" && pratim[1].Split('=')[1] != "3")// לא נמק או שרות
                        ((TextBox)e.Row.Cells[(int)enPeilut.KISUY_TOR].FindControl("txtKisuiTor")).Enabled = false;
                    if (pratim.Length > 2)
                        if (pratim[2].Split('=')[1] == "0" || pratim[2].Split('=')[1] == "")
                            ((TextBox)e.Row.Cells[(int)enPeilut.KISUY_TOR].FindControl("txtKisuiTor")).Enabled = false;
                        else
                        {
                            ((TextBox)e.Row.Cells[(int)enPeilut.KISUY_TOR].FindControl("txtKisuiTor")).Attributes.Add("Kisuy_Tor", pratim[2].Split('=')[1]);
                            if (e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text != "&nbsp;" && ((TextBox)e.Row.Cells[(int)enPeilut.KISUY_TOR].FindControl("txtKisuiTor")).Text == "")
                                ((TextBox)e.Row.Cells[(int)enPeilut.KISUY_TOR].FindControl("txtKisuiTor")).Text = getKisuyTorString(int.Parse(pratim[2].Split('=')[1]), DateTime.Parse(e.Row.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Text));
                        }
                    if ((pratim.Length > 3 && pratim[3].Split('=')[1] == "False") )
                        ((TextBox)e.Row.Cells[(int)enPeilut.MAKAT].FindControl("txtMakat")).Enabled = false;

                    //הוספת נתון לפרטים - בדיקה אם הוא אלמנט מסוג דקות במאפיין 4
                    //אם כן והוא אלמנט אז לא חייב שתהיה שעת גמר
               
                    if (sugSidur.Value == "2")
                    {
                        dtElements = (DataTable)Session["ELEMENTS"];
                        sSQL = "KOD_ELEMENT='" + makat.ToString().Substring(1, 2) + "' AND KOD_MEAFYEN='4' AND  ERECH=1";
                        // sSQL = "KOD_MEAFYEN =4 and ERECH=1";
                        drSelect = dtElements.Select(sSQL);
                        if (drSelect.Length > 0)
                            e.Row.Cells[(int)enPeilut.PRATIM].Text = e.Row.Cells[(int)enPeilut.PRATIM].Text + ";MUST_GMAR=FALSE";
                        else
                            e.Row.Cells[(int)enPeilut.PRATIM].Text = e.Row.Cells[(int)enPeilut.PRATIM].Text + ";MUST_GMAR=TRUE";
                    }
                }
                
                Rechev.Attributes.Add("OldV", Rechev.Text);
            }


    }

    protected void BtIdkunGrid_Click(object sender, EventArgs e)
    {
        DataTable dtSource = new DataTable();
        String sXMLResult = "";
        string sScript = "";
      
        try
        {

            dtSource = (DataTable)Session["DataSource"];
            RefreshDataTable(ref dtSource);

            if (HosafatElement.Value == "sof")
            {
                //    sXMLResult = BuildSidurAndPeiluyotXml();
                if (IdkunNetunim())
                {
                    sScript = "window.returnValue=1;";
                    sScript += "window.close();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HosafatSidur", sScript, true);
                }
                else if (!tsEmpty.Visible) 
                    BindGridPeiluyot();
            }
            else if (HosafatElement.Value != "")
            {
                //HosefElemntToGrid(ref dtSource);
                HosafatElement.Value = "";
                //if (dtSource.Rows[0]["MAKAT"].ToString() == "")
                //    dtSource.Rows.Remove(dtSource.Rows[0]);
                if (Session["DtPeiluyotNew"] != null)
                    HosefNewPeiluyot(ref dtSource,(DataTable)Session["DtPeiluyotNew"]);
                Session["DataSource"] = dtSource;
                BindGridPeiluyot();
            }
            else
            {//נוסף ע''י שרי בעקבות באג 
                Session["DataSource"] = dtSource;
                BindGridPeiluyot();
            }

            txtShatGmar.ToolTip = TaarichGmar.Value;
            txtShatHatchala.ToolTip = TaarichHatchala.Value;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void HosefNewPeiluyot(ref DataTable dtSource, DataTable dtNewPeiluyot)
    {
        DataRow[] drPeiluyot;
        DataRow newRow;
        string makat="";
        int type = 0;
        try
        {
            drPeiluyot = dtNewPeiluyot.Select("HOSEF_PEILUT=1");
            for (int i = 0; i < drPeiluyot.Length; i++)
            {
                newRow = dtSource.NewRow();
                //newRow["KISUY_TOR"] = drPeiluyot[i]["KISUY_TOR"];
                newRow["SHAT_YETZIA"] = drPeiluyot[i]["SHAT_YETZIA"];
                newRow["TEUR"] = drPeiluyot[i]["TEUR"];
                newRow["KAV"] = drPeiluyot[i]["KAV"];
                newRow["SUG"] = drPeiluyot[i]["SUG"];
                newRow["MISPAR_RECHEV"] = drPeiluyot[i]["MISPAR_RECHEV"];
                newRow["MISPAR_RISHUY"] = drPeiluyot[i]["MISPAR_RISHUY"];
                newRow["MAKAT"] = drPeiluyot[i]["MAKAT"];
                if (int.Parse(drPeiluyot[i]["MISPAR_KNISA"].ToString())>0)
                    newRow["DAKOT_HAGDARA"] = "0";
                else
                    newRow["DAKOT_HAGDARA"] = drPeiluyot[i]["DAKOT_HAGDARA"];
                newRow["HAGDARA_LEGMAR"] = drPeiluyot[i]["HAGDARA_LEGMAR"];
                newRow["DAKOT_BAFOAL"]= drPeiluyot[i]["DAKOT_BAFOAL"];
                newRow["HOSEF_PEILUT"] = drPeiluyot[i]["HOSEF_PEILUT"];
                newRow["PEILUT_CHOVA"] = drPeiluyot[i]["PEILUT_CHOVA"];
                newRow["IS_VALID_MAKAT"] = drPeiluyot[i]["IS_VALID_MAKAT"];
                newRow["IS_VALID_MIS_RECHEV"] = drPeiluyot[i]["IS_VALID_MIS_RECHEV"];
                newRow["PRATIM"] = getPratimLeMakat(int.Parse(newRow["MAKAT"].ToString()), ref newRow,ref type);
                newRow["PRATIM"] += ";KISUY_TOR=" + drPeiluyot[i]["KISUY_TOR"];// +";ENABLE_MAKAT=False";
                makat = drPeiluyot[i]["MAKAT"].ToString();
                if (type==5)
                    newRow["PRATIM"] += ";ENABLE_MAKAT=True";
                else newRow["PRATIM"] += ";ENABLE_MAKAT=False";
                newRow["SHAT_YEZIA_DATE"] =  drPeiluyot[i]["SHAT_YEZIA_DATE"];
                //if (txtShatHatchala.Attributes["NEXT"].ToString() =="true" &&
                //    DateTime.Parse(drPeiluyot[i]["SHAT_YEZIA_DATE"].ToString().Split(' ')[0]) != DateTime.Parse(TaarichCA.Value).AddDays(1))
                //      newRow["SHAT_YEZIA_DATE"] =  DateTime.Parse(drPeiluyot[i]["SHAT_YEZIA_DATE"].ToString()).AddDays(1).ToString();
                newRow["MUST_DAKOT"] = drPeiluyot[i]["MUST_DAKOT"];
                newRow["MISPAR_KNISA"] = drPeiluyot[i]["MISPAR_KNISA"];
                newRow["SUG_KNISA"] = drPeiluyot[i]["SUG_KNISA"];
                dtSource.Rows.Add(newRow);

            }
            Session["DtPeiluyotNew"] = null;
            Session["DtPeiluyot"] = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void HosefElemntToGrid(ref DataTable dtSource)
    //{
    //    string[] PirteyElement;
    //    DateTime shatPeilutShenosfa;
    //    int count = 0;
    //    bool flag = false;
    //    try
    //    {
    //        PirteyElement = HosafatElement.Value.Split(';');
    //        DataRow dr = dtSource.NewRow();

    //        dr["KISUY_TOR"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.KisuyTorShaa];
    //        dr["SHAT_YETZIA"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.ShatYezia];
    //        dr["TEUR"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.Teur];
    //        dr["KAV"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.Kav];
    //        dr["SUG"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.Sug];
    //        dr["MISPAR_RECHEV"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.MisparRechev];

    //        dr["MISPAR_RISHUY"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.MisparRishuy];
    //        dr["MAKAT"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.Makat];
    //        dr["DAKOT_HAGDARA"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.DakotHagdara];
    //        dr["DAKOT_BAFOAL"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.DakotBafoal];
    //        dr["PRATIM"] = getPratimLeMakat(int.Parse(PirteyElement[(int)clGeneral.enNetuneyPeilut.Makat]));
    //        dr["PRATIM"] += ";KISUY_TOR=" + PirteyElement[(int)clGeneral.enNetuneyPeilut.KisuyTorDakot];
    //        dr["HOSEF_PEILUT"] = "1";
    //        dr["SHAT_YEZIA_DATE"] = PirteyElement[(int)clGeneral.enNetuneyPeilut.ShatYeziaDate] + " " + dr["SHAT_YETZIA"]; //DestTime.Value + " " + dr["SHAT_YETZIA"]; 

    //        shatPeilutShenosfa = DateTime.Parse(dr["SHAT_YEZIA_DATE"].ToString());
    //        if(dtSource.Rows[0]["MAKAT"].ToString()== "")
    //            count =dtSource.Rows.Count-1;
    //        else
    //            count =dtSource.Rows.Count;
    //        for (int i = 0; i < count; i++)
    //        {

    //            if (dtSource.Rows[i]["SHAT_YEZIA_DATE"].ToString().IndexOf("nbsp;") == -1 && dtSource.Rows[i]["SHAT_YEZIA_DATE"].ToString() != "")
    //            {
    //                if (shatPeilutShenosfa < DateTime.Parse(dtSource.Rows[i]["SHAT_YEZIA_DATE"].ToString()))
    //                {
    //                    dtSource.Rows.InsertAt(dr, i);
    //                    flag = true;
    //                    break;
    //                }
    //            }
    //        }

    //        if (!flag)// dtSource.Rows.Count==1 && dtSource.Rows[0]["MAKAT"].ToString()== "")
    //            dtSource.Rows.Add(dr);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void RefreshDataTable(ref DataTable dtSource)
    {
        string erech;
        bool cheak;
        string MakatYashan;
        for (int i = 0; i < grdPeiluyot.Rows.Count; i++)
        {
            erech = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.KISUY_TOR].FindControl("txtKisuiTor")).Text;
            if (erech != "&nbsp;")
                dtSource.Rows[i]["KISUY_TOR"] = erech;

            erech = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.SHAT_YETZIA].FindControl("txtShatYezia")).Text;
            if (erech != "&nbsp;")//{
                dtSource.Rows[i]["SHAT_YETZIA"] = erech;
            //    dtSource.Rows[i]["SHAT_YEZIA_DATE"] = ((HtmlInputHidden)grdPeiluyot.Rows[i].Cells[(int)enPeilut.SHAT_YETZIA].FindControl("DateHidden")).Value;
            //}

            erech = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.MISPAR_RECHEV].FindControl("txtMisRechev")).Text;
            if (erech != "&nbsp;")
                dtSource.Rows[i]["MISPAR_RECHEV"] = erech;

           //? erech = ((HtmlInputText)grdPeiluyot.Rows[i].Cells[(int)enPeilut.MISPAR_RISHUY].FindControl("lblMisparRishuy")).Value;
            //erech = (grdPeiluyot.Rows[i].Cells[(int)enPeilut.hidMisRishuy]).Text;
            //if (erech != "&nbsp;")
            //    dtSource.Rows[i]["MISPAR_RISHUY"] = erech;
            dtSource.Rows[i]["MISPAR_RISHUY"] = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.MISPAR_RISHUY].FindControl("lblMisparRishuy")).Text;

            erech = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Text;
            if (erech != "&nbsp;")
                dtSource.Rows[i]["DAKOT_BAFOAL"] = erech;

            cheak = ((CheckBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.HOSEF_PEILUT].FindControl("cbHosef")).Checked;
            if (cheak)
                dtSource.Rows[i]["HOSEF_PEILUT"] = "1";
            else
                dtSource.Rows[i]["HOSEF_PEILUT"] = "0";

            dtSource.Rows[i]["IS_VALID_MAKAT"] = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.tmpValidMakat].FindControl("txtIsMakatValid")).Text;
            dtSource.Rows[i]["IS_VALID_MIS_RECHEV"] = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.tmpValidMisRechev].FindControl("txtIsMisRechevValid")).Text;

            MakatYashan = grdPeiluyot.Rows[i].Cells[(int)enPeilut.hidMakat].Text;
            erech = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.MAKAT].FindControl("txtMakat")).Text;
            if (dtSource.Rows[i]["IS_VALID_MAKAT"].ToString().Split(';').Length > 2)
            {
                erech = MakatYashan;
                dtSource.Rows[i]["IS_VALID_MAKAT"] = "";
                ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.tmpValidMakat].FindControl("txtIsMakatValid")).Text = "";
            }
            if (erech != "&nbsp;")
                dtSource.Rows[i]["MAKAT"] = erech;

            dtSource.Rows[i]["SHAT_YEZIA_DATE"] = ((TextBox)grdPeiluyot.Rows[i].Cells[(int)enPeilut.txtShatYeziaDate].FindControl("txtShatYeziaDate")).Text;
        }
    }

    private void SetUnvisibleColumns(GridViewRow CurrentRow)
    {

        CurrentRow.Cells[(int)enPeilut.hidKisuyTor].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.hidShatYezia].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.hidMisRechev].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.hidMakat].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.hidDakotBafoal].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.hidHosefPeilut].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.hidMisRishuy].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.MISPAR_RISHUY].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.PEILUT_CHOVA].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.IS_VALID_MAKAT].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.IS_VALID_MIS_RECHEV].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.tmpValidMakat].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.tmpValidMisRechev].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.PRATIM].Style.Add("display", "none");
        CurrentRow.Cells[MISPAR_KNISA].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.SHAT_YEZIA_DATE].Style.Add("display", "none");
        CurrentRow.Cells[(int)enPeilut.txtShatYeziaDate].Style.Add("display", "none");
    }

    private string BuildSidurAndPeiluyotXml()
    {
        DataTable dt = new DataTable();
        StringBuilder sXML = new StringBuilder();
        DataRow dr;
        string[] pratim;
        try
        {
            dt = (DataTable)Session["DataSource"];
            sXML.Append("<SIDUR>");
            sXML.Append("<MISPAR_SIDUR>" + lblMisSidur.Text + "</MISPAR_SIDUR>");
            sXML.Append("<SHAT_HATCHALA>" + txtShatHatchala.Text + "</SHAT_HATCHALA>");
            sXML.Append("<SHAT_GMAR>" + txtShatGmar.Text + "</SHAT_GMAR>");
            sXML.Append("<PEILUYOT>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];

                if (dr["HOSEF_PEILUT"] == "1")
                {
                    pratim = dr["PRATIM"].ToString().Split(';');

                    sXML.Append("<PEILUT>");
                    sXML.Append(string.Concat("<INDEX_PEILUT>", i.ToString(), "</INDEX_PEILUT>"));
                    if (pratim[1].Split('=')[1] == "5")
                        sXML.Append("<SUG_PEILUT>0</SUG_PEILUT>");
                    else
                        sXML.Append("<SUG_PEILUT>1</SUG_PEILUT>");
                    sXML.Append(string.Concat("<KISUY_TOR_SHAA>", dr["KISUY_TOR"].ToString(), "</KISUY_TOR_SHAA>"));
                    sXML.Append(string.Concat("<SHAT_YEZIA>", dr["SHAT_YETZIA"].ToString(), "</SHAT_YEZIA>"));
                    sXML.Append(string.Concat("<TEUR>", dr["TEUR"].ToString(), "</TEUR>"));
                    sXML.Append(string.Concat("<SUG>", dr["SUG"].ToString(), "</SUG>"));
                    sXML.Append(string.Concat("<MISPAR_RECHEV>", dr["MISPAR_RECHEV"].ToString(), "</MISPAR_RECHEV>"));
                    sXML.Append(string.Concat("<MISPAR_RISHUY>", dr["MISPAR_RISHUY"].ToString(), "</MISPAR_RISHUY>"));
                    sXML.Append(string.Concat("<MAKAT>", dr["MAKAT"].ToString(), "</MAKAT>"));
                    sXML.Append(string.Concat("<DAKOT_HAGDARA>", dr["DAKOT_HAGDARA"].ToString(), "</DAKOT_HAGDARA>"));
                    sXML.Append(string.Concat("<DAKOT_BAFOAL>", dr["DAKOT_BAFOAL"].ToString(), "</DAKOT_BAFOAL>"));
                    sXML.Append("<MISPAR_KNISA>0</MISPAR_KNISA>");
                    sXML.Append("<BITUL_O_HOSAFA>2</BITUL_O_HOSAFA>");
                    if (pratim.Length > 2)
                        if (pratim[2].Split('=')[1] != "")
                            sXML.Append(string.Concat("<KISUY_TOR_DAKOT>", pratim[2].Split('=')[1], "</KISUY_TOR_DAKOT>"));
                        else
                            sXML.Append("<KISUY_TOR_DAKOT>0</KISUY_TOR_DAKOT>");
                    else sXML.Append("<KISUY_TOR_DAKOT>0</KISUY_TOR_DAKOT>");
                    sXML.Append("</PEILUT>");
                }
            }
            sXML.Append("</PEILUYOT>");
            sXML.Append("</SIDUR>");
            return sXML.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool IdkunNetunim()
    {

        COLL_SIDURIM_OVDIM oCollSidurimOvdimIns = new COLL_SIDURIM_OVDIM();
        COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
        clOvdim oWorkCard = new clOvdim();
        DateTime HourSidur = new DateTime();
        try
        {
           // if (txtShatHatchala.Attributes["NEXT"].ToString() == "true" || DateTime.Parse(TaarichHatchala.Value).Date > DateTime.Parse(TaarichCA.Value))
            if ( DateTime.Parse(TaarichHatchala.Value).Date > DateTime.Parse(TaarichCA.Value))
                HourSidur = DateTime.Parse(TaarichCA.Value + " " + txtShatHatchala.Text + ":00").AddDays(1);
            else
                HourSidur = DateTime.Parse(TaarichCA.Value + " " + txtShatHatchala.Text + ":00");
            if (!oWorkCard.CheckSidurExist(int.Parse(lblMisSidur.Text), int.Parse(MisparIshi.Value), HourSidur))
            {
                SetPirteySidur(ref oCollSidurimOvdimIns);
                clDefinitions.InsertSidurimOvdim(oCollSidurimOvdimIns);

                if (tsEmpty.Visible == false)
                {
                    SetPirteyPeiluyot(ref oCollPeilutOvdimIns);
                    clDefinitions.InsertPeilutOvdim(oCollPeilutOvdimIns);
                }
                HttpRuntime.Cache.Remove(MisparIshi.Value + HourSidur.ToString().Split(' ')[0].ToString());
                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('קיים סידור בשעת ההתחלה שדיווחת, יש לתקן את השעה');", true);
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    //private bool IsSidurAlreadyExist()
    //{
    //    clOvdim oWorkCard = new clOvdim();
    //    DateTime HourSidur = new DateTime();
    //    string ShatYetzia;
    //    try
    //    {
    //        HourSidur = DateTime.Parse(TaarichCA.Value + " " + txtShatHatchala.Text + ":00");
    //        return oWorkCard.CheckSidurExist(int.Parse(MisparIshi.Value), HourSidur);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void SetPirteySidur(ref COLL_SIDURIM_OVDIM oCollSidurimOvdimIns)
    {
        OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
        try
        {
            oObjSidurimOvdimIns.MISPAR_ISHI = int.Parse(MisparIshi.Value);
            oObjSidurimOvdimIns.MISPAR_SIDUR = int.Parse(lblMisSidur.Text);

            //if (txtShatHatchala.Attributes["NEXT"].ToString() == "true")
            //{
            //    oObjSidurimOvdimIns.TAARICH = DateTime.Parse(TaarichCA.Value).AddDays(1);
            //    oObjSidurimOvdimIns.SHAT_HATCHALA = DateTime.Parse(TaarichCA.Value + " " + txtShatHatchala.Text + ":00").AddDays(1);
            //    oObjSidurimOvdimIns.SHAYAH_LEYOM_KODEM = 1;
            //}
            //else 
            if (DateTime.Parse(TaarichHatchala.Value).Date > DateTime.Parse(TaarichCA.Value)){
                oObjSidurimOvdimIns.TAARICH = DateTime.Parse(TaarichCA.Value);
                oObjSidurimOvdimIns.SHAT_HATCHALA = DateTime.Parse(TaarichCA.Value + " " + txtShatHatchala.Text + ":00").AddDays(1);
                oObjSidurimOvdimIns.SHAYAH_LEYOM_KODEM = 1;
            }
            else{
                oObjSidurimOvdimIns.TAARICH = DateTime.Parse(TaarichCA.Value);
                oObjSidurimOvdimIns.SHAT_HATCHALA = DateTime.Parse(TaarichCA.Value + " " + txtShatHatchala.Text + ":00");
            }

            if (txtShatGmar.Text != "")
                oObjSidurimOvdimIns.SHAT_GMAR = DateTime.Parse(TaarichGmar.Value.Split(' ')[1] + " " + txtShatGmar.Text + ":00");        
            oObjSidurimOvdimIns.CHARIGA = 0;
            oObjSidurimOvdimIns.HAMARAT_SHABAT = 0;
            oObjSidurimOvdimIns.BITUL_O_HOSAFA = 2;
            oObjSidurimOvdimIns.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
            
            oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetPirteyPeiluyot(ref COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns)
    {

        OBJ_PEILUT_OVDIM oObjPeiluyotOvdimIns;
        DataTable dt = new DataTable();
        DataRow drPeilut;
        string taarich;
        string erech;
        try
        {
             dt = (DataTable)Session["DataSource"];
             //if (txtShatHatchala.Attributes["NEXT"].ToString() == "true")
             //    taarich = DateTime.Parse(TaarichCA.Value).AddDays(1).ToShortDateString().Split(' ')[0];
             //else 
             taarich = TaarichCA.Value;
             for (int i = 0; i < dt.Rows.Count; i++)
            {
                oObjPeiluyotOvdimIns = new OBJ_PEILUT_OVDIM();
                drPeilut = dt.Rows[i];

                if (drPeilut["HOSEF_PEILUT"] == "1")
                {
                    oObjPeiluyotOvdimIns.MISPAR_ISHI = int.Parse(MisparIshi.Value);
                    oObjPeiluyotOvdimIns.MISPAR_SIDUR = int.Parse(lblMisSidur.Text);
                    oObjPeiluyotOvdimIns.TAARICH = DateTime.Parse(taarich);
                    oObjPeiluyotOvdimIns.SHAT_YETZIA = DateTime.Parse(drPeilut["SHAT_YEZIA_DATE"].ToString());
                    if (DateTime.Parse(TaarichHatchala.Value).Date > DateTime.Parse(TaarichCA.Value))
                        oObjPeiluyotOvdimIns.SHAT_HATCHALA_SIDUR = DateTime.Parse(taarich + " " + txtShatHatchala.Text + ":00").AddDays(1);
                    else oObjPeiluyotOvdimIns.SHAT_HATCHALA_SIDUR = DateTime.Parse(taarich + " " + txtShatHatchala.Text + ":00");
                    oObjPeiluyotOvdimIns.MAKAT_NESIA = int.Parse(drPeilut["MAKAT"].ToString());
                    oObjPeiluyotOvdimIns.MISPAR_KNISA = int.Parse(drPeilut["MISPAR_KNISA"].ToString());
                    if (oObjPeiluyotOvdimIns.MISPAR_KNISA >0)
                        oObjPeiluyotOvdimIns.TEUR_NESIA = drPeilut["TEUR"].ToString();
                    erech = drPeilut["MISPAR_RECHEV"].ToString();
                    if (erech != "")
                        oObjPeiluyotOvdimIns.OTO_NO = int.Parse(erech);

                    if (drPeilut["PRATIM"].ToString().Split(';').Length > 2)
                    {
                        erech = drPeilut["PRATIM"].ToString().Split(';')[2].Split('=')[1].ToString();
                        if (erech != "" && erech != "0")
                        {
                            ChashevKisuyTor(oObjPeiluyotOvdimIns.SHAT_YETZIA, drPeilut["KISUY_TOR"].ToString(), ref erech);
                            oObjPeiluyotOvdimIns.KISUY_TOR = int.Parse(erech);
                        }
                    }
                   
                    oObjPeiluyotOvdimIns.BITUL_O_HOSAFA = 2;
                    //  oObjPeiluyotOvdimIns.snif_tnua ?? לא קיים
                    oObjPeiluyotOvdimIns.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                    erech = drPeilut["DAKOT_BAFOAL"].ToString();
                    if (erech != "")
                        oObjPeiluyotOvdimIns.DAKOT_BAFOAL = int.Parse(erech);

                    oCollPeilutOvdimIns.Add(oObjPeiluyotOvdimIns);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ChashevKisuyTor(DateTime shatYezia, string kisuy_tor, ref string dakot)
    {
        DateTime shatKisuyTorMax;
        DateTime shatKisuyTorNochahit;
        try{
            shatKisuyTorMax = shatYezia.AddMinutes(int.Parse(dakot) *(-1));
            shatKisuyTorNochahit = DateTime.Parse(shatYezia.ToShortDateString() + " " + kisuy_tor);
            if (shatKisuyTorNochahit > shatYezia)
                shatKisuyTorNochahit = shatKisuyTorNochahit.AddDays(-1);
            dakot = (shatYezia - shatKisuyTorNochahit).Minutes.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
