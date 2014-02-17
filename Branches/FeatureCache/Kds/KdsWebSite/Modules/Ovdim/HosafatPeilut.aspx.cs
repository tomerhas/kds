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
using KdsLibrary.BL;
using KdsLibrary.Security;
using KdsLibrary.UDT;
using KdsBatch;
using KDSCommon.DataModels.UDT;
using KDSCommon.Helpers;
using KDSCommon.Enums;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.DAL;

public partial class Modules_Ovdim_HosafatPeilut : KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtElements = new DataTable();
        clUtils oUtils = new clUtils();
        DataTable dtParametrim = new DataTable();
        try
        {
            ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
            btnSave.Style.Add("Display", "none");
            if (!Page.IsPostBack)
            {
                SetDefault();
                if (Request.QueryString["SidurID"] != null)
                {
                    txtHiddenMisparSidur.Value = Request.QueryString["SidurID"].ToString();
                }
                if (Request.QueryString["CardDate"] != null)
                {
                    txtHiddenTaarichCA.Value = Request.QueryString["CardDate"].ToString(); //"26/05/2009";//
                }
                //אם הפרמטר 'לא לשמור פעילות' קיים אז לא שומרים את הפעילות בדף,אם הוא לא קיים,שומרים
                if (Request.QueryString["NoSavePeilut"] != null)
                    SavePeilut.Value = "NO";
                else SavePeilut.Value = "YES";

                if (Request.QueryString["SidurHour"] != null)
                {
                    txtHiddenHourHatchaltSidur.Value = Request.QueryString["SidurHour"].ToString(); //  "12:00"; //
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
                    for (int i = 0; i < dtElements.Rows.Count; i++)
                        ElementsRelevants.Value = ElementsRelevants.Value + dtElements.Rows[i]["KOD_ELEMENT"].ToString() + ",";
                }

                dtParametrim = oUtils.getErechParamByKod("29", txtHiddenTaarichCA.Value);
                if (dtParametrim.Rows.Count > 0)
                {
                    for (int i = 0; i < dtParametrim.Rows.Count; i++)
                        Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }
    protected void SetDefault()
    {
        rdKod.Attributes.Add("onclick", "SetTextBox();");
        rdTeur.Attributes.Add("onclick", "SetTextBox();");
        txtTeurElement.Enabled = false;
    }
    //protected void setAttributsNesiaa()
    //{
    //   txtMakat.Attributes.Add("MakatType", "");
    //}
    protected void setAttributsElemnt()
    {
        txtMisparElement.Attributes.Add("KodValid", "true");
        txtTeurElement.Attributes.Add("TeurValid", "true");
        txtHosefErechElement.Attributes.Add("ErechValid", "true");
        txtMisRechev.Attributes.Add("MisRechevValid", "true");
        //txtShatYezia.Attributes.Add("ShaaValid", "true");   
    }

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        enMakatType MakatType;
        DataTable dtKavim = new DataTable();
        long lMakatNesia = int.Parse(txtMakat.Text);
        DateTime dCardDate = DateTime.Parse(txtHiddenTaarichCA.Value);
        int iMakatValid;
        bool flag = true;
        try
        {
            NikuySadot();

            MakatType = (enMakatType)StaticBL.GetMakatType(lMakatNesia); //(enMakatType)int.Parse(txtMakat.Attributes["MakatType"].ToString());
            var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
            switch (MakatType)
            {
                case enMakatType.mKavShirut:
                    var kavimdDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                    dtKavim = kavimdDal.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);
                    if (dtKavim.Rows.Count > 0)
                        setNetuneyKavShirut(dtKavim);
                    else flag = false;

                    break;
                case enMakatType.mEmpty:
                    dtKavim = kavimDal.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                    if (dtKavim.Rows.Count > 0)
                        setNetuneyEmpty(dtKavim);
                    else flag = false;

                    break;
                case enMakatType.mNamak:
                    dtKavim = kavimDal.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                    if (dtKavim.Rows.Count > 0)
                        setNetuneyNamak(dtKavim);
                    else flag = false;

                    break;
            }
            if (flag)
            {
                tblPeilutNesiaa.Style.Add("display", "inline");
                divButtons.Visible = true;
                divButtons.Style.Add("display", "inline");
            }
            else
            {
                vldMakat.ErrorMessage = "המק''ט שהוקלד שגוי";
                vldMakat.IsValid = false;
                tblPeilutNesiaa.Style.Add("display", "none");
                divButtons.Style.Add("display", "none");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void NikuySadot()
    {
        txtKisuiTor.Text = "";
        txtShatYeziaKatalog.Text = "";
        lblTeur.Text = "";
        lblKav.Text = "";
        lblSug.Text = "";
        txtMisRechevKatalog.Text = "";
        lblMakatKatalog.Text = "";
        lblDakotHagdara.Text = "";
        txtDakotBafoal.Text = "";
        IsValidRechevKatalog.Value = "";
    }
    protected void setNetuneyKavShirut(DataTable dtKavim)
    {
        string kisuy_tor;
        try
        {
            //kisuy Tor  
            kisuy_tor = dtKavim.Rows[0]["KISUITOR"].ToString();
            if (kisuy_tor != "0" && kisuy_tor != "")
            {
                txtKisuiTor.Attributes.Add("kisuy_tor", kisuy_tor);
                txtKisuiTor.Enabled = true;
            }
            else
            {
                txtKisuiTor.Attributes.Add("kisuy_tor", "0");
                txtKisuiTor.Enabled = false;
            }
            lblTeur.Text = dtKavim.Rows[0]["DESCRIPTION"].ToString();
            lblKav.Text = dtKavim.Rows[0]["SHILUT"].ToString();
            lblSug.Text = dtKavim.Rows[0]["SUGSHIRUTNAME"].ToString();
            lblMakatKatalog.Text = dtKavim.Rows[0]["MAKAT8"].ToString();
            lblDakotHagdara.Text = dtKavim.Rows[0]["MAZANTICHNUN"].ToString();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void setNetuneyNamak(DataTable dtKavim)
    {
        string kisuy_tor;
        try
        {
            kisuy_tor = dtKavim.Rows[0]["KISUITOR"].ToString();
            if (kisuy_tor != "0" && kisuy_tor != "")
            {
                txtKisuiTor.Attributes.Add("kisuy_tor", kisuy_tor);
                txtKisuiTor.Enabled = true;
            }
            else
            {
                txtKisuiTor.Attributes.Add("kisuy_tor", "0");
                txtKisuiTor.Enabled = false;
            }
            lblTeur.Text = dtKavim.Rows[0]["DESCRIPTION"].ToString();
            lblKav.Text = dtKavim.Rows[0]["SHILUT"].ToString();
            lblSug.Text = dtKavim.Rows[0]["SUGNAMAKNAME"].ToString();
            lblMakatKatalog.Text = dtKavim.Rows[0]["MAKAT8"].ToString();
            lblDakotHagdara.Text = dtKavim.Rows[0]["MAZANTICHNUN"].ToString();
            txtKisuiTor.Enabled = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void setNetuneyEmpty(DataTable dtKavim)
    {
        try
        {
            lblTeur.Text = dtKavim.Rows[0]["DESCRIPTION"].ToString();
            ///  lblKav.Text = dtKavim.Rows[0]["SHILUT"].ToString();
            /// lblSug.Text = dtKavim.Rows[0]["SUGNAMAKNAME"].ToString();
            lblMakatKatalog.Text = dtKavim.Rows[0]["MAKAT8"].ToString();
            lblDakotHagdara.Text = dtKavim.Rows[0]["MAZANTICHNUN"].ToString();
            txtKisuiTor.Enabled = false;
            txtKisuiTor.Attributes.Add("kisuy_tor", "0");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string[] netunim;
        OBJ_PEILUT_OVDIM oObjPeiluyotOvdimIns = new OBJ_PEILUT_OVDIM();
        COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
        string date;

        try
        {
            if (!IsPeilutAlreadyExist())
            {
                netunim = ReturnValue.Value.Split(';');

                oObjPeiluyotOvdimIns.MISPAR_ISHI = int.Parse(txtHiddenMisparIshi.Value);
                oObjPeiluyotOvdimIns.MISPAR_SIDUR = int.Parse(txtHiddenMisparSidur.Value);
                oObjPeiluyotOvdimIns.TAARICH = DateTime.Parse(txtHiddenTaarichCA.Value);
                date = netunim[(int)clGeneral.enNetuneyPeilut.ShatYeziaDate] + " " + netunim[(int)clGeneral.enNetuneyPeilut.ShatYezia];
                oObjPeiluyotOvdimIns.SHAT_YETZIA = DateTime.Parse(date);
                date = txtHiddenTaarichCA.Value + " " + txtHiddenHourHatchaltSidur.Value;
                oObjPeiluyotOvdimIns.SHAT_HATCHALA_SIDUR = DateTime.Parse(date);
                oObjPeiluyotOvdimIns.MISPAR_KNISA = int.Parse(netunim[(int)clGeneral.enNetuneyPeilut.MisparKnisa]);
                oObjPeiluyotOvdimIns.MAKAT_NESIA = int.Parse(netunim[(int)clGeneral.enNetuneyPeilut.Makat]);
                if (netunim[(int)clGeneral.enNetuneyPeilut.MisparRechev] != "")
                    oObjPeiluyotOvdimIns.OTO_NO = int.Parse(netunim[(int)clGeneral.enNetuneyPeilut.MisparRechev]);
                if (netunim[(int)clGeneral.enNetuneyPeilut.KisuyTorDakot] != "")
                    oObjPeiluyotOvdimIns.KISUY_TOR = int.Parse(netunim[(int)clGeneral.enNetuneyPeilut.KisuyTorDakot]);
                oObjPeiluyotOvdimIns.BITUL_O_HOSAFA = int.Parse(netunim[(int)clGeneral.enNetuneyPeilut.Bitul_O_Hosafa]);
                //  oObjPeiluyotOvdimIns.snif_tnua ?? לא קיים
                oObjPeiluyotOvdimIns.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                if (netunim[(int)clGeneral.enNetuneyPeilut.DakotBafoal] != "" && netunim[(int)clGeneral.enNetuneyPeilut.DakotBafoal] != "0")
                    oObjPeiluyotOvdimIns.DAKOT_BAFOAL = int.Parse(netunim[(int)clGeneral.enNetuneyPeilut.DakotBafoal]);

                oCollPeilutOvdimIns.Add(oObjPeiluyotOvdimIns);

                clDefinitions.InsertPeilutOvdim(oCollPeilutOvdimIns);
                HttpRuntime.Cache.Remove(txtHiddenMisparIshi.Value + txtHiddenTaarichCA.Value);
                ScriptManager.RegisterStartupScript(btnSave, this.GetType(), "close", "window.close();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType(), "err", " alert('קיימת פעילות בשעת היציאה שדיווחת, יש לתקן את השעה');", true);
                divButtons.Style.Add("Display", "block");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool IsPeilutAlreadyExist()
    {
        clOvdim oWorkCard = new clOvdim();
        DateTime HourHatchaltSidur = new DateTime();
        string ShatYetzia;
        try
        {
            HourHatchaltSidur = DateTime.Parse(txtHiddenTaarichCA.Value + " " + txtHiddenHourHatchaltSidur.Value);
            if (ShaatYeziaDate.Value != "")
                ShatYetzia = ShaatYeziaDate.Value;
            else ShatYetzia = txtHiddenTaarichCA.Value;

            if (Sug_Peilut.Value == "1")
                ShatYetzia += " " + txtShatYeziaKatalog.Text;
            else
                ShatYetzia += " " + txtShatYezia.Text;


            return oWorkCard.CheckPeilutExist(int.Parse(txtHiddenMisparSidur.Value),
                                              int.Parse(txtHiddenMisparIshi.Value),
                                              HourHatchaltSidur, DateTime.Parse(ShatYetzia));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

