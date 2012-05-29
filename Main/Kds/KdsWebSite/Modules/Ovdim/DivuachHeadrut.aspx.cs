using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.UI;
using KdsLibrary;
using KdsLibrary.BL;
using System.Data;
using KdsBatch;
using KdsLibrary.Security;
public partial class Modules_Ovdim_DivuachHeadrut :KdsPage
{
    public  clParameters _objParameters;
    public clMeafyenyOved _MeafyeneyOved;
    public string sDateCard; 
    private string[] arrParams;
    private int iMisparIshiKiosk;
    protected override void CreateUser()
    {
        if ((Session["arrParams"] != null))
        {
            arrParams = (string[])Session["arrParams"];
            SetUserKiosk(arrParams);
        }
        else { base.CreateUser(); }
    }


    private void SetUserKiosk(string[] arrParamsKiosk)
    {
        iMisparIshiKiosk = int.Parse(arrParamsKiosk[0].ToString());


        LoginUser = LoginUser.GetLimitedUser(iMisparIshiKiosk.ToString());
        LoginUser.InjectEmployeeNumber(iMisparIshiKiosk.ToString());
        //MasterPage mp = (MasterPage)Page.Master;
        //mp.DisabledMenuAndToolBar = true;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try 
        {
          ////  pnlEndDateHeadrut.ScriptManagerObj = ScriptManagerKds;
          //  clnEndDateHeadrut.OnChangeScript = "EnableButton();";
            clnEndDateHeadrut.OnChangeCalScript = "EnableButton();";

            ViewState["MisparIshi"] = int.Parse(Request.QueryString["MisparIshi"].ToString());
            ViewState["DateCard"] = DateTime.Parse(Request.QueryString["DateCard"].ToString());

             _MeafyeneyOved = new clMeafyenyOved(int.Parse(Request.QueryString["MisparIshi"].ToString()), DateTime.Parse(ViewState["DateCard"].ToString()));
             _objParameters = new clParameters(DateTime.Parse(ViewState["DateCard"].ToString()), clGeneral.GetSugYom(clGeneral.GetYamimMeyuchadim(), DateTime.Parse(ViewState["DateCard"].ToString()), clGeneral.GetSugeyYamimMeyuchadim()));//, _MeafyeneyOved.iMeafyen56));
           
            if (!Page.IsPostBack)
            {
                ViewState["TimeStart"] = ViewState["DateCard"];
                if (Request.QueryString["Status"].Trim().Length > 0)
                {
                    ViewState["Status"] = Request.QueryString["Status"].Trim();
                }
                
                if (Request.QueryString["MisparSidur"].Trim().Length>0)
                {
                    ViewState["MisparSidur"] = int.Parse(Request.QueryString["MisparSidur"].ToString());
               
                    if (Request.QueryString["TimeStart"].Trim().Length > 0)
                    {
                        ViewState["TimeStart"] = DateTime.Parse(Request.QueryString["TimeStart"].ToString());
                        txtStartTime.Text = DateTime.Parse(Request.QueryString["TimeStart"].ToString()).ToShortTimeString();
                    }
                    if (Request.QueryString["TimeEnd"].Trim().Length > 0)
                    {
                        ViewState["TimeEnd"] = DateTime.Parse(Request.QueryString["TimeEnd"].ToString());
                        txtEndTime.Text = DateTime.Parse(Request.QueryString["TimeEnd"].ToString()).ToShortTimeString();
                    }
                }
                    else if (ddlHeadrutType.SelectedIndex==-1)
                {
                    
                    btnUpdate.Attributes.Add("disabled", "true");
                    btnUpdate.ControlStyle.CssClass = "btnWorkCardLongDis";
                    txtStartTime.Attributes.Add("disabled", "true");
                    txtEndTime.Attributes.Add("disabled", "true");
                    txtStartTime.Attributes.Add("onfocus", "this.className='WorkCardSidurTextBoxFocus';");
                    txtStartTime.Attributes.Add("onblur", "this.className='WorkCardPeilutTextBox';");
                    txtEndTime.Attributes.Add("onfocus", "this.className='WorkCardSidurTextBoxFocus';");
                    txtEndTime.Attributes.Add("onblur", "this.className='WorkCardPeilutTextBox';");
                }
               
                InitData();
                sDateCard = DateTime.Parse(ViewState["DateCard"].ToString()).ToShortDateString();
              
              //  clnEndDateHeadrut.Attributes.Add("onblur", "EnableButton();");
                clnEndDateHeadrut.OnBlurCalFunction = "EnableButton();";
                drHeaara.Style.Add("display", "none");
                drTaarichAd.Style.Add("display", "none");
                trAddition.Style.Add("height", "267px");
                drVldTaarichAd.Style.Add("display", "none");
            }
             
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitData()
    {
        clUtils oUtils=new clUtils();
        clWorkCard oWorkCard = new clWorkCard();
        DataTable dtHeadrutType, dtSidurim, dtHeadrutTypeNew;
        DataRow[] dr;
        DataRow NewDr;
        string tnaim;
        try
        {
            dtSidurim = oWorkCard.GetSidurimLeoved(int.Parse(Request.QueryString["MisparIshi"].ToString()), DateTime.Parse(ViewState["DateCard"].ToString()));
            ViewState["dtSidurim"] = dtSidurim;
            dtHeadrutType = oUtils.GetSidurimWithMeafyenim(DateTime.Parse(Request.QueryString["DateCard"].ToString()));
            NewDr = dtHeadrutType.NewRow();
            NewDr["teur_sidur_meychad"] = "בחר";
            NewDr["kod_sidur_meyuchad"] = -1;
            dtHeadrutType.Rows.InsertAt(NewDr, 0);
            
            dtHeadrutType.TableName = "SugHeadrut";
            ddlHeadrutType.DataTextField = "teur_sidur_meychad";
            ddlHeadrutType.DataValueField = "kod_sidur_meyuchad";
            tnaim="(sidur_misug_headrut is not null or kod_sidur_meyuchad=-1 or  nitan_ledaveach_ad_taarich is not null) ";
            if (ViewState["Status"] != null)
                if (int.Parse(ViewState["Status"].ToString())==-1)
                    tnaim += " and rashai_ledaveach=1";
            dr = dtHeadrutType.Select(tnaim, "teur_sidur_meychad asc");
            dtHeadrutTypeNew = new DataTable();
            dtHeadrutTypeNew = dtHeadrutType.Clone();
            foreach (DataRow dRow in dr)
                dtHeadrutTypeNew.ImportRow(dRow);

            ddlHeadrutType.DataSource = dtHeadrutTypeNew;
            ddlHeadrutType.DataBind();
            if (Request.QueryString["MisparSidur"].Trim().Length > 0)
            {
                ddlHeadrutType.Enabled = false;
                ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "SetDdl", "selectedSidur=document.all('ddlHeadrutType').options[document.all('ddlHeadrutType').selectedIndex];", true);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
     }

    protected void ddlHeadrutType_DataBound(object sender, EventArgs e)
    {
        string sShatHatchala;
        DateTime dShatGmar;
        if (ddlHeadrutType.Items.Count > 0)
        {
            DataTable dtHeadruyot = (DataTable)ddlHeadrutType.DataSource;
            ddlHeadrutType.Items.Cast<ListItem>()
                              .ToList()
                              .ForEach
                              (
                                item =>
                                {
                                    if (Request.QueryString["MisparSidur"].Trim().Length> 0)
                                    {
                                        if (item.Value == Request.QueryString["MisparSidur"])
                                        {
                                            item.Selected = true;
                                        }
                                     }
                                    //else if (item.Value == "99822" && (_MeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() || ((DataTable)ViewState["dtSidurim"]).Rows.Count>0))
                                    //    {
                                    //        item.Enabled = false;
                                    //    } 
                                  
                                    //if (dtHeadruyot.Select("kod_sidur_meyuchad=" + item.Value).Any(row => row["sidur_yachid_beyom"].ToString() != ""))
                                    //{
                                    //    item.Attributes.Add("sidur_yachid_beyom", "True");
                                    //}
                                 
                                    if (dtHeadruyot.Select("kod_sidur_meyuchad=" + item.Value).Any(row => row["shat_hatchala_muteret"].ToString() != ""))
                                    {
                                        sShatHatchala = string.Concat(DateTime.Parse(ViewState["DateCard"].ToString()).ToShortDateString(), " ", DateTime.Parse(dtHeadruyot.Select("kod_sidur_meyuchad=" + item.Value).FirstOrDefault(row => row["shat_hatchala_muteret"].ToString() != "")["shat_hatchala_muteret"].ToString()).ToShortTimeString());
                                        item.Attributes.Add("max_shat_hatchala", sShatHatchala);
                                    }
                                    else
                                    {
                                        item.Attributes.Add("max_shat_hatchala", _objParameters.dSidurStartLimitHourParam1.ToString());
                                     }

                                    if (dtHeadruyot.Select("kod_sidur_meyuchad=" + item.Value).Any(row => row["shat_gmar_muteret"].ToString() != ""))
                                    {
                                        dShatGmar = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(dtHeadruyot.Select("kod_sidur_meyuchad=" + item.Value).FirstOrDefault(row => row["shat_gmar_muteret"].ToString() != "")["shat_gmar_muteret"].ToString()).ToShortTimeString(),DateTime.Parse(ViewState["DateCard"].ToString()));

                                        if (dShatGmar >= clGeneral.GetDateTimeFromStringHour("00:01", DateTime.Parse(ViewState["DateCard"].ToString())) && dShatGmar <= clGeneral.GetDateTimeFromStringHour("07:59", DateTime.Parse(ViewState["DateCard"].ToString())))
                                        {
                                            dShatGmar = dShatGmar.AddDays(1);
                                        }

                                        item.Attributes.Add("max_gmar_muteret", dShatGmar.ToString());
                                     }
                                    else
                                    {
                                        item.Attributes.Add("max_gmar_muteret", _objParameters.dSidurLimitShatGmar.ToString());
                                     }

                                    if (dtHeadruyot.Select("kod_sidur_meyuchad=" + item.Value).Any(row => row["nitan_ledaveach_ad_taarich"].ToString() != ""))
                                    {
                                        item.Attributes.Add("nitan_ledaveach_ad_taarich", "True");
                                    }
                                    if (dtHeadruyot.Select("kod_sidur_meyuchad=" + item.Value).Any(row => row["headrut_hova_ledaveach_shaot"].ToString() != ""))
                                    {
                                        item.Attributes.Add("headrut_hova_ledaveach_shaot", "True");
                                    }
                                   
                                }
                              );
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        clWorkCard oWorkCard = new clWorkCard();
        int iSugHeadrut;
        string sMessage, sTaarichim = "" ;
        DateTime dShatHatchala, dShatSiyum;
        DataTable dtYamim = new DataTable();
        try
        {
            iSugHeadrut = ddlHeadrutType.SelectedIndex; 
            if (Page.IsValid)
            {
                InitData();
                ddlHeadrutType.SelectedIndex = iSugHeadrut;
                if (clnEndDateHeadrut.Text.Length > 0)
                {
                    dtYamim = oWorkCard.GetYemeyAvodaOvdim(int.Parse(ViewState["MisparIshi"].ToString()), DateTime.Parse(ViewState["DateCard"].ToString()).AddDays(1), DateTime.Parse(clnEndDateHeadrut.Text));
                    if (dtYamim.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtYamim.Rows)
                        {
                            sTaarichim += DateTime.Parse(dr["taarich"].ToString()).ToShortDateString() + ',';
                        }
                        sTaarichim = sTaarichim.Substring(0, sTaarichim.Length - 1);
                        sMessage = "לא ניתן לדווח היעדרות ממושכת, קיים כבר דיווח בתאריכים " + sTaarichim;
                        ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "err", "HideShaotRow(document.all('ddlHeadrutType').options[document.all('ddlHeadrutType').selectedIndex]);alert('" + sMessage + "');", true);

                    }
                    else
                    {
                        if (CheckChafifa())
                        {
                             sMessage = "סידור ההיעדרות חופף בשעות עם סידור קיים";
                            ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "err", "HideShaotRow(document.all('ddlHeadrutType').options[document.all('ddlHeadrutType').selectedIndex]);alert('" + sMessage + "');", true);
                        }
                        else
                        {
                            dShatHatchala = clGeneral.GetDateTimeFromStringHour(txtStartTime.Text, DateTime.Parse(ViewState["DateCard"].ToString()).Date);
                            dShatSiyum = clGeneral.GetDateTimeFromStringHour(txtEndTime.Text, DateTime.Parse(ViewState["DateCard"].ToString()).Date);


                            oWorkCard.InsYemeyAvodaWithSidurim(int.Parse(ViewState["MisparIshi"].ToString()), DateTime.Parse(ViewState["DateCard"].ToString()), DateTime.Parse(clnEndDateHeadrut.Text), dShatHatchala, dShatSiyum, DateTime.Parse(ddlHeadrutType.SelectedItem.Attributes["max_shat_hatchala"]), DateTime.Parse(ddlHeadrutType.SelectedItem.Attributes["max_gmar_muteret"]), int.Parse(ddlHeadrutType.SelectedValue), int.Parse(LoginUser.UserInfo.EmployeeNumber));
                            ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "Close", "window.returnValue=1;window.close();", true);
                        }
                    }
                }
                else
                {
                    if (CheckChafifa())
                    {
                        sMessage = "סידור ההיעדרות חופף בשעות עם סידור קיים";
                        ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "err", "HideShaotRow(document.all('ddlHeadrutType').options[document.all('ddlHeadrutType').selectedIndex]);alert('" + sMessage + "');", true);
                    }
                    else
                    {
                        dShatHatchala = clGeneral.GetDateTimeFromStringHour(txtStartTime.Text, DateTime.Parse(ViewState["DateCard"].ToString()).Date);
                        dShatSiyum = clGeneral.GetDateTimeFromStringHour(txtEndTime.Text, DateTime.Parse(ViewState["DateCard"].ToString()).Date);

                        oWorkCard.InsUpdSidurimOvdim(int.Parse(ViewState["MisparIshi"].ToString()), DateTime.Parse(ViewState["DateCard"].ToString()), int.Parse(ddlHeadrutType.SelectedValue), dShatHatchala, dShatSiyum, int.Parse(LoginUser.UserInfo.EmployeeNumber));
                        ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "Close", "window.returnValue=1;window.close();", true);
                    }
                }

            }
            else
            {
                InitData();
                ddlHeadrutType.SelectedIndex = iSugHeadrut;
                ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "SetDdl", "HideShaotRow(document.all('ddlHeadrutType').options[document.all('ddlHeadrutType').selectedIndex]);", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(btnUpdate, btnUpdate.GetType(), "error", "HideShaotRow(document.all('ddlHeadrutType').options[document.all('ddlHeadrutType').selectedIndex]);alert('תקלה בשמירת הנתונים');", true);
         }
    }

    private bool CheckChafifa()
    {
        bool bChofef = false;
        DataRow[] drRowSidurim;
        DateTime dStartSidur, dEndSidur;
        try
        {
            drRowSidurim = ((DataTable)ViewState["dtSidurim"]).Select("MISPAR_SIDUR <> 99200 AND SHAT_HATCHALA is not null and (MISPAR_SIDUR <> " + ddlHeadrutType.SelectedValue + " or SHAT_HATCHALA<>Convert('" + ViewState["TimeStart"] + "', 'System.DateTime')) AND (Lo_letashlum=0 or (Lo_letashlum=1 and kod_siba_lo_letashlum=1)) and Bitul_O_Hosafa not in(1,3)", "SHAT_HATCHALA ASC");
            dStartSidur = clGeneral.GetDateTimeFromStringHour(txtStartTime.Text,DateTime.Parse(ViewState["DateCard"].ToString()));
            dEndSidur = clGeneral.GetDateTimeFromStringHour(txtEndTime.Text,DateTime.Parse(ViewState["DateCard"].ToString()));
            if (dEndSidur >= clGeneral.GetDateTimeFromStringHour("00:01", DateTime.Parse(ViewState["DateCard"].ToString())) && dEndSidur <= clGeneral.GetDateTimeFromStringHour("07:59", DateTime.Parse(ViewState["DateCard"].ToString())))
            {
                dEndSidur = dEndSidur.AddDays(1);
            }
            for (int I = 0; I < drRowSidurim.Length; I++)
            {
                if (string.IsNullOrEmpty(drRowSidurim[I]["SHAT_GMAR"].ToString()))
                { drRowSidurim[I]["SHAT_GMAR"] = drRowSidurim[I]["SHAT_HATCHALA"]; }

                if ((dStartSidur<DateTime.Parse(drRowSidurim[I]["SHAT_GMAR"].ToString()) && dEndSidur>DateTime.Parse(drRowSidurim[I]["SHAT_HATCHALA"].ToString()))
                   ||(dEndSidur>DateTime.Parse(drRowSidurim[I]["SHAT_HATCHALA"].ToString()) && dEndSidur<DateTime.Parse(drRowSidurim[I]["SHAT_GMAR"].ToString()))
                || (dStartSidur<DateTime.Parse(drRowSidurim[I]["SHAT_HATCHALA"].ToString()) && dEndSidur>DateTime.Parse(drRowSidurim[I]["SHAT_GMAR"].ToString())))
                {
                    bChofef=true;
                }
            }

            return bChofef;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
 
}
