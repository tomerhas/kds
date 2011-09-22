using KdsLibrary.BL;
using KdsLibrary;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using KdsWorkFlow.Approvals;

public partial class Modules_Ovdim_BakashatTashlum : System.Web.UI.Page
{
    private clOvdim oOvdim ;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtShaot;
        try
        {
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(new DateTime(1900, 1, 1, 0, 0, 0));
            if (!IsPostBack)
            {
                SetApproveFieldsVisible();
                if (Request.QueryString["Tzuga"] == "1")
                {
                    trMeshekShabat.Visible = false; 
                    trNihulShabat.Visible = false;
                    trShaot200.Visible = false;
                }
                else if(Request.QueryString["Tzuga"] == "2")
                {
                    trMeshekChol.Visible = false;
                    trNihulChol.Visible = false; 
                }

                    oOvdim = new clOvdim();
                    if (Request.QueryString["Editable"] != null)
                        SetFormReadOnlyMode();
                    else if (ApprovalRequestAlreadyExists)
                    {
                        
                        ApprovalRequest approvalRequest = ApprovalRequest.GetExistingApprovalRequest(
                            GetApprovalKeyFromQueryString());
                        SetFormForApproval(approvalRequest);
                    }
                    if ((Request.QueryString["MisparIshi"] != null) & (Request.QueryString["BakashaId"] != ""))
                    {
                        //lblMichsa.Text = oOvdim.GetMeafyenLeOved(int.Parse(Request.QueryString["MisparIshi"].ToString()), DateTime.Parse(Request.QueryString["Taarich"].ToString()), clGeneral.enMeafyeneyOved.MichsaLeShaotNosafot);
                    

                        dtShaot = oOvdim.GetShaotMealMichsa(int.Parse(Request.QueryString["MisparIshi"].ToString()), DateTime.Parse(Request.QueryString["Taarich"].ToString()), long.Parse(Request.QueryString["BakashaId"].ToString()));

                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode().ToString()).Length > 0)
                        {
                            lblKizuzMeshekChol.Text =  (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString())/60).ToString("0.00");
                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode().ToString()).Length > 0)
                        { lblMichsaMeshekChol.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) ).ToString("0.00"); }
                        }
                        else
                        { trMeshekChol.Visible = false; }

                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode().ToString()).Length > 0)
                        {
                            lblKizuzNihulChol.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) / 60).ToString("0.00");
                            if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode().ToString()).Length > 0)
                            { lblMichsaNihulChol.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString())).ToString("0.00"); }
                        }
                        else
                        { trNihulChol.Visible = false; }

                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidShabat.GetHashCode().ToString()).Length > 0)
                        {
                            lblKizuzMeshekShabat.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidShabat.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) / 60).ToString("0.00");
                            if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode().ToString()).Length > 0)
                            { lblMichsaMeshekShabat.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString())).ToString("0.00"); }
                        }
                        else
                        { trMeshekShabat.Visible = false; }

                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode().ToString()).Length > 0)
                        {
                            lblKizuzNihulShabat.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) / 60).ToString("0.00"); ;
                            if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode().ToString()).Length > 0)
                            { lblMichsaNihulShabat.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) ).ToString("0.00"); }
                        }
                        else
                        { trNihulShabat.Visible = false; }

                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.Kizuz125.GetHashCode().ToString()).Length > 0)
                        {
                            lblShaot125.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.Kizuz125.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) / 60).ToString("0.00");
                        ViewState["Shaot125"] = lblShaot125.Text;
                        }

                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.Kizuz150.GetHashCode().ToString()).Length > 0)
                        {
                            lblShaot150.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.Kizuz150.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) / 60).ToString("0.00");
                        ViewState["Shaot150"] = lblShaot150.Text;
                        }

                        if (dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.Kizuz200.GetHashCode().ToString()).Length > 0)
                        {
                            lblShaot200.Text = (double.Parse(dtShaot.Select("KOD_RECHIV=" + clGeneral.enRechivim.Kizuz200.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) / 60).ToString("0.00");
                        ViewState["Shaot200"] = lblShaot200.Text;
                        }

                        if (Request.QueryString["Tzuga"] == "1")
                        {
                            object computed = dtShaot.Compute("sum(ERECH_RECHIV)", "KOD_RECHIV IN(" + clGeneral.enRechivim.Kizuz125.GetHashCode().ToString() + "," + clGeneral.enRechivim.Kizuz150.GetHashCode().ToString() + ")");
                            if (computed != DBNull.Value)
                                lblSumShaot.Text = (double.Parse(computed.ToString()) / 60).ToString("0.00");
                        }
                        else if (Request.QueryString["Tzuga"] == "2")
                        {
                            object computed = dtShaot.Compute("sum(ERECH_RECHIV)", "KOD_RECHIV IN(" + clGeneral.enRechivim.Kizuz200.GetHashCode().ToString() + ")");
                            if (computed != DBNull.Value)
                                lblSumShaot.Text = (double.Parse(computed.ToString()) / 60).ToString("0.00");
                        }
                        else
                        {
                            object computed = dtShaot.Compute("sum(ERECH_RECHIV)", "KOD_RECHIV IN(" + clGeneral.enRechivim.Kizuz125.GetHashCode().ToString() + "," + clGeneral.enRechivim.Kizuz150.GetHashCode().ToString() + "," + clGeneral.enRechivim.Kizuz200.GetHashCode().ToString() + ")");
                            if (computed != DBNull.Value)
                                lblSumShaot.Text = (double.Parse(computed.ToString()) / 60).ToString("0.00");
                        }

                        txtShaot.Text = lblSumShaot.Text;
                        if (string.IsNullOrEmpty(lblSumShaot.Text)) { lblSumShaot.Text = "0"; }
                        
                        txtSiba.Attributes.Add("maxLength", txtSiba.MaxLength.ToString());
                        txtSiba.Attributes.Add("onkeypress", "doKeypress(this);");
                        txtSiba.Attributes.Add("onbeforepaste", "doBeforePaste(this);");
                        txtSiba.Attributes.Add("onpaste", "doPaste(this);");
                    }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetApproveFieldsVisible()
    {
        bool enable = ApprovalRequestAlreadyExists;
        RangeValidator1.Enabled = enable;
        RequiredFieldValidator1.Enabled = enable;
        CompareValidator1.Enabled = enable;
        txtShaotMeushar.Visible = enable;
        lblShaotMeushar.Visible = enable;
        lblPrevLevelShaotMeushar.Visible = enable;
        txtPrevLevelShaotMeushar.Visible = enable;
        if(enable)
        {
            var approvalKey = GetApprovalKeyFromQueryString();
            trShaot125.Visible = approvalKey.Approval.Code == 34;
            trShaot150.Visible = approvalKey.Approval.Code == 34;
        }
    }

    private void SetFormForApproval(ApprovalRequest approvalRequest)
    {
        DivTextEditable.Visible = true;
        DivEmployeeDetails.Visible = true;
        txtPrevLevelShaotMeushar.Enabled=false;
        TitleWindow.Text = lblHeaderMessage.Text = "שעות נוספות שקוזזו";
        TxtFullName.Text = oOvdim.GetOvedFullName(clGeneral.GetIntegerValue(Request.QueryString["MisparIshi"].ToString()));
        TxtMisparIshi.Text = Request.QueryString["MisparIshi"].ToString();
        TxtMonth.Text = DateTime.Parse(Request.QueryString["Taarich"].ToString()).ToString("MM/yyyy");
        txtShaot.Text = approvalRequest.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY].ToString();
        txtShaot.Enabled = false;
        txtSiba.Text = approvalRequest.GetAdditionalDataValue("siba").ToString();
        txtSiba.Enabled = false;
        RangeValidator1.MaximumValue = txtShaot.Text;
        if (CheckIfAllowToAccept())
        {
            if(approvalRequest.State==ApprovalRequestState.Approved)
                txtShaotMeushar.Text =
                    approvalRequest.GetAdditionalDataValue("erech_meushar").ToString();    
            else
                txtShaotMeushar.Text = txtShaot.Text;
        }
        else txtShaotMeushar.Text =
            approvalRequest.GetAdditionalDataValue("erech_meushar").ToString();
        btnSend.Text = "אישור";
        vldSiba.Enabled = false;
        if (approvalRequest.Approval.Level > 1)
        {
            var prevLevelRequest = approvalRequest.GetPreviousLevelApprovalRequest();
            if (prevLevelRequest != null)
            {
                object erechMeusharValue = prevLevelRequest.GetAdditionalDataValue("erech_meushar");
                if (erechMeusharValue != null)
                    txtPrevLevelShaotMeushar.Text = erechMeusharValue.ToString();
                else txtPrevLevelShaotMeushar.Text = String.Empty;
            }
        }
        else
        {
            txtPrevLevelShaotMeushar.Visible = false;
            lblPrevLevelShaotMeushar.Visible = false;
        }
    }
    private void SetFormReadOnlyMode()
    {
            DivTextEditable.Visible = false;
            DivEmployeeDetails.Visible = true;
            TitleWindow.Text = lblHeaderMessage.Text = "שעות נוספות שקוזזו";
            TxtFullName.Text = oOvdim.GetOvedFullName(clGeneral.GetIntegerValue(Request.QueryString["MisparIshi"].ToString()));
            TxtMisparIshi.Text = Request.QueryString["MisparIshi"].ToString();
            TxtMonth.Text = DateTime.Parse(Request.QueryString["Taarich"].ToString()).ToString("MM/yyyy");
            btnSend.Text = "סגור";
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string sSugYechida,sMessage;
        int iKodIshur;
        float fShaot = 0;
        float fShaot200 = 0;
        ApprovalRequestState StateRequest;
        if (ApprovalRequestAlreadyExists)
        {
            PreformApproveForRequest();
            return;
        }
        if (Request.QueryString["Editable"] != null)
            ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "close", "window.returnValue=true;window.close();", true);
        else
        {
            if (Page.IsValid)
            {
                StateRequest = ApprovalRequestState.Empty;
                sMessage = "";
                if (txtShaot.Text.Length > 0)
                {
                    if (float.Parse(txtShaot.Text) > float.Parse(lblSumShaot.Text))
                    {
                        ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "err", "alert('לא ניתן לבקש מעבר ל- " + lblSumShaot.Text + " שעות' );", true);
                    }
                    else
                    {

                        oOvdim = new clOvdim();
                        sSugYechida = oOvdim.GetSugYechidaLeoved(int.Parse(Request.QueryString["MisparIshi"].ToString()), DateTime.Parse(Request.QueryString["Taarich"].ToString()));

                        if (ViewState["Shaot200"] != null)
                        {
                            fShaot200 = float.Parse(ViewState["Shaot200"].ToString());
                            if (sSugYechida.ToLower() == "m_me" || sSugYechida.ToLower() == "m_ms")
                            { iKodIshur = clGeneral.enKodIshur.TashlumShaotNosafotShabatOvdeyMusac.GetHashCode(); }
                            else { iKodIshur = clGeneral.enKodIshur.TashlumShaotNosafotShabat.GetHashCode(); }
                            fShaot = Math.Min(float.Parse(txtShaot.Text), float.Parse(ViewState["Shaot200"].ToString()));
                            StateRequest = OpenRequest(iKodIshur, fShaot);
                            if (StateRequest == ApprovalRequestState.AlreadyExists)
                            {
                                sMessage = sMessage + "\\n בקשה לתשלום שעות שבת - כבר הוגשה בקשה שנמצאת בטיפול, לא ניתן לשנות בשלב זה";
                            }
                            else if (StateRequest != ApprovalRequestState.Pending)
                            {
                                sMessage = sMessage + "\\n                                                                 תקלה בתהליך העברת בקשה לתשלום שעות שבת";
                            }
                        }

                        if ((ViewState["Shaot125"] != null || ViewState["Shaot150"] != null) && float.Parse(lblSumShaot.Text) > fShaot200)
                        {
                            if (sSugYechida.ToLower() == "m_me" || sSugYechida.ToLower() == "m_ms")
                            { iKodIshur = clGeneral.enKodIshur.TashlumShaotNosafotOvdeyMusac.GetHashCode(); }
                            else { iKodIshur = clGeneral.enKodIshur.TashlumShaotNosafot.GetHashCode(); }
                            fShaot = float.Parse(txtShaot.Text) - fShaot200;
                            StateRequest = OpenRequest(iKodIshur, fShaot);
                            if (StateRequest == ApprovalRequestState.AlreadyExists)
                            {
                                sMessage = sMessage + "\\n בקשה לתשלום שעות חול - כבר הוגשה בקשה שנמצאת בטיפול, לא ניתן לשנות בשלב זה";
                            }
                            else if (StateRequest != ApprovalRequestState.Pending)
                            {
                                sMessage = sMessage + "\\n                                                               תקלה בתהליך העברת  בקשה לתשלום שעות חול";
                            }
                        }



                        if (sMessage.Length > 0)
                        {
                            ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "err", "alert('" + sMessage + "');", true);

                        }
                        ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "close", "window.returnValue=true;window.close();", true);

                    }
                }
            }
        }
    }

    private ApprovalRequestState OpenRequest(int iKodIshur, float fShaot)
    {
        try
        {
            WorkCard Wc = new WorkCard();
            Wc.WorkDate = DateTime.Parse(Request.QueryString["Taarich"].ToString()).AddMonths(-1).AddDays(+1);
            Wc.SidurStart = Wc.WorkDate;
            Wc.ActivityStart = Wc.WorkDate;

            ApprovalRequest AppRequest;
            AppRequest = ApprovalRequest.CreateApprovalRequest(Request.QueryString["MisparIshi"].ToString(), iKodIshur, Wc, RequestValues.WithFirstRequestValues(fShaot), true, "Siba", txtSiba.Text);
            AppRequest.ProcessRequest();

            return AppRequest.State;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private ApprovalKeyWithTag GetApprovalKeyFromQueryString()
    {
        ApprovalKeyWithTag key = new ApprovalKeyWithTag();
        key.Employee = new Employee(Request.QueryString["MisparIshi"]);
        key.Approval = new Approval();
        key.Approval.Code = int.Parse(Request.QueryString["KodIshur"]);
        key.Approval.Level = int.Parse(Request.QueryString["Level"]);
        key.WorkCard = new WorkCard();
        key.WorkCard.WorkDate = DateTime.Parse(Request.QueryString["Taarich"]);
        key.WorkCard.SidurNumber = int.Parse(Request.QueryString["SidurNum"]);
        key.WorkCard.SidurStart = DateTime.Parse(Request.QueryString["SidurStart"]);
        key.WorkCard.ActivityStart = DateTime.Parse(Request.QueryString["ActivityStart"]);
        key.WorkCard.ActivityNumber = int.Parse(Request.QueryString["ActivityNum"]);
        key.RequestValues.SetValue(RequestValues.FIRST_REQUEST_VALUE_KEY,
            Request.QueryString["ErechMevukash"]);
        key.RequestValues.SetValue(RequestValues.SECOND_REQUEST_VALUE_KEY,
            Request.QueryString["ErechMevukash2"]);
        return key;
    }

    private bool CheckIfAllowToAccept()
    {
        bool enableAccept = false;
        if (bool.TryParse(Request.QueryString["AllowAccept"], out enableAccept))
            btnSend.Enabled = enableAccept;
        else btnSend.Enabled = false;
        if (!btnSend.Enabled) btnSend.CssClass = "ImgButtonOvedPrevDisable";
        txtShaotMeushar.Enabled = btnSend.Enabled;
        return enableAccept;
    }

    private bool ApprovalRequestAlreadyExists
    {
        get 
        {
            bool exists = false;
            if (!String.IsNullOrEmpty(Request.QueryString["Approve"]))
            {
                if (!bool.TryParse(Request.QueryString["Approve"], out exists)) 
                    exists = false;
            }
            return exists;
        }
    }

    private void PreformApproveForRequest()
    {
        if (Page.IsValid)
        {
            ApprovalManager approvalManager =
                new ApprovalManager(KdsLibrary.Security.LoginUser.GetLoginUser());

            ApprovalRequest outRequest = null, nextRequest = null;
            bool success = false;
            ApprovalKey key = GetApprovalKeyFromQueryString();
            if (double.Parse(txtShaotMeushar.Text) > 0)
                success = approvalManager.AcceptApprovalRequest(key,
                    out outRequest, out nextRequest, "erech_meushar", txtShaotMeushar.Text);
                
            else success = approvalManager.DeclineApprovalRequest(key, out outRequest,
                "erech_meushar", txtShaotMeushar.Text);

            if (success)
                ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "close",
                       "window.returnValue=true;window.close();", true);
            else GenerateErrorScript(outRequest);
        }
    }

    private void GenerateErrorScript(ApprovalRequest outRequest)
    {
        ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "error",
            String.Format("alert('{0}')", outRequest.ErrorMessage), true);
    }
}
