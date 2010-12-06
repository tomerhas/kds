using KdsLibrary.BL;
using KdsLibrary;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Modules_Ovdim_BakashatTashlum : System.Web.UI.Page
{
    private clOvdim _oOvdim ;
    private int _Mispar_Ishi, _QuotaDemand, _ConfirmedValueUnit,_UnitValueMovedToVaad,_ConfirmedValueVaad ,_Balance;
    private string _Unit, _Month;
    private clGeneral.enMonthlyQuotaForm _FormType;


    protected void Page_Load(object sender, EventArgs e)
    {
        _oOvdim = clOvdim.GetInstance();
        try
        {
            if (!IsPostBack)
            {
                if (GetDataFromQuery())
                    FillDataToForm();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void FillDataToForm()
    {
        TxtFullName.Text = _oOvdim.GetOvedFullName(_Mispar_Ishi);
        TxtMisparIshi.Text = _Mispar_Ishi.ToString();
        TxtBalance.Text = _Balance.ToString();
        TxtMevukach.Text = _QuotaDemand.ToString();
        TxtApprovedInUnit.Text = _ConfirmedValueUnit.ToString();
        TxtApprovedInUnit.Attributes.Add("OriginalValue", _ConfirmedValueUnit.ToString());
        TxtAavaratToVaadP.Text = _UnitValueMovedToVaad.ToString();
        TxtUavarLevaadatPnim.Text = _UnitValueMovedToVaad.ToString();
        TxtFormType.Text = ((int)Enum.Parse(typeof(clGeneral.enMonthlyQuotaForm),_FormType.ToString())).ToString();
        switch ((clGeneral.enMonthlyQuotaForm)_FormType)
        {
            case clGeneral.enMonthlyQuotaForm.Agafit:
                DivDetailsForVaadatPnim.Style.Add("Display", "none");
                DivDetailsForAgaf.Style.Add("Display", "block");
                CmpValidApprovedInVaadatP_1.Visible = false;
                CmpValidApprovedInVaadatP_2.Visible = false;
                CmpValidApprovedInVaadatP_3.Visible = false;
                break;
            case clGeneral.enMonthlyQuotaForm.VaadatPnim:
                CmpValidAavaratToVaadP_1.Visible = false;
                CmpValidAavaratToVaadP_2.Visible = false;
                CmpValidAavaratToVaadP_3.Visible = false;
                CmpValidAavaratToVaadP_4.Visible = false;
                CmpValidApprovedInUnit_1.Visible = false;
                CmpValidApprovedInUnit_2.Visible = false;
                CmpValidApprovedInUnit_3.Visible = false;
                CmpValidApprovedInUnit_4.Visible = false;
                DivDetailsForVaadatPnim.Style.Add("Display", "block");
                DivDetailsForAgaf.Style.Add("Display", "none");
                TxtApprovedInVaadatP.Text = _ConfirmedValueVaad.ToString();
                TxtUnit.Text = _Unit;
                TxtMonth.Text = _Month;
                break;
        }
    }

    private bool GetDataFromQuery()
    {
        try
        {
            _Mispar_Ishi = clGeneral.GetIntegerValue(Request.QueryString["MisparIshi"].ToString());
            _FormType = (clGeneral.enMonthlyQuotaForm)clGeneral.GetIntegerValue(Request.QueryString["Level"].ToString());
            _QuotaDemand = clGeneral.GetIntegerValue(Request.QueryString["QuotaDemand"].ToString());
            _ConfirmedValueUnit = clGeneral.GetIntegerValue(Request.QueryString["ConfirmedValueUnit"].ToString());
            _UnitValueMovedToVaad = clGeneral.GetIntegerValue(Request.QueryString["UnitValueMovedToVaad"].ToString());
            _ConfirmedValueVaad = clGeneral.GetIntegerValue(Request.QueryString["ConfirmedValueVaad"].ToString());
            _Balance = clGeneral.GetIntegerValue(Request.QueryString["Balance"].ToString());
            _Unit = (Request.QueryString["Unit"] == null) ? "" :  Server.UrlDecode(Request.QueryString["Unit"].ToString());
            _Month = (Request.QueryString["Month"] == null) ? "" : Request.QueryString["Month"].ToString();
            return true;
        }
        catch
        {
            return false;   
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "close", "ConfirmAndClose()", true);
        }
    }

}
