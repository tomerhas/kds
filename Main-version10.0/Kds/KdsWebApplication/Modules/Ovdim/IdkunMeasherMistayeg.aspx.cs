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
using KdsLibrary.Utils.Reports;
using System.Collections.Generic;
using KdsLibrary.UI.SystemManager;
using KDSCommon.Interfaces;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;

namespace KdsWebApplication.Modules.Ovdim
{
    public partial class IdkunMeasherMistayeg : KdsPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtParametrim = new DataTable();
            clUtils oUtils = new clUtils();

            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                PageHeader = "רשימת כרטיסי עבודה לעובד";
                LoadMessages((DataList)Master.FindControl("lstMessages"));

                rdoId.Attributes.Add("onclick", "SetTextBox();");
                rdoName.Attributes.Add("onclick", "SetTextBox();");

                KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                if (iSecurity == KdsSecurityLevel.ViewAll) //|| iSecurity == KdsSecurityLevel.ViewOnlyEmployeeData)
                {
                    AutoCompleteExtenderByName.ContextKey = "";
                    AutoCompleteExtenderID.ContextKey = "";
                }
                else if ((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
                {
                    AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                    AutoCompleteExtenderByName.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                }
                else
                {
                    AutoCompleteExtenderByName.ContextKey = ""; // LoginUser.UserInfo.EmployeeNumber;
                    AutoCompleteExtenderID.ContextKey = "";// LoginUser.UserInfo.EmployeeNumber;
                    txtId.Enabled = false;
                    rdoId.Enabled = false;
                    rdoName.Enabled = false;
                }

                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                for (int i = 0; i < dtParametrim.Rows.Count; i++)
                    Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());

            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtCtb = new DataTable();
            clOvdim oOvdim = new clOvdim();
            DataView dv = new DataView();
            string status;
            int misIshi;
            try
            {
                var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();

               // lnkTaarich.InnerText = txtId.Text;
                lblMis.Text = txtId.Text;
                lnkTaarich.InnerText = clnTaarich.Text;

                dtCtb = cache.GetCacheItem<DataTable>(CachedItems.StatusWC).Copy();
                ddlstatus.DataTextField = "teur_measher_o_mistayeg";
                ddlstatus.DataValueField = "kod_measher_o_mistayeg";
                ddlstatus.DataSource = dtCtb;
                ddlstatus.DataBind();

                misIshi = int.Parse(lblMis.Text);
                dt = oOvdim.GetStatusKartis(misIshi, DateTime.Parse(clnTaarich.Text), int.Parse(LoginUser.GetLoginUser().UserInfo.EmployeeNumber));
                if (dt.Rows.Count > 0)
                {
                  
                    status = dt.Rows[0]["MEASHER_O_MISTAYEG"].ToString();
                    if (status == "1")
                    {
                        ddlstatus.SelectedValue = "1";
                        EnableUpdate(false);
                    }
                    else
                    {
                        dtCtb.Rows.RemoveAt(1);
                        ddlstatus.DataSource = dtCtb;
                        ddlstatus.DataBind();

                        if (string.IsNullOrEmpty(status))
                            ddlstatus.SelectedValue = "2";
                        else ddlstatus.SelectedValue = status;
                        txtSiba.Text = dt.Rows[0]["RESON"].ToString();

                        var shinuyManager = ServiceLocator.Current.GetInstance<IShinuyimManager>();
                        dt = shinuyManager.GetIdkuneyRashemet(misIshi, DateTime.Parse(clnTaarich.Text));
                        if ((string.IsNullOrEmpty(status) || status == "0") && (dt.Rows.Count == 0 || dt.Select("pakad_id in(46,47) and GOREM_MEADKEN=" + misIshi).Length == 0))
                            EnableUpdate(true);
                        else EnableUpdate(false);
                    }
                }
                
                fsPratim.Style["display"] = "block";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnableUpdate(bool enable)
        {
            btnUpdate.Enabled = enable;
            ddlstatus.Enabled = enable;
            txtSiba.Enabled = enable;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            clOvdim oOvdim = new clOvdim();
            try
            {
                oOvdim.UpdateStatusByRashemet(int.Parse(lblMis.Text), DateTime.Parse(clnTaarich.Text), int.Parse(LoginUser.GetLoginUser().UserInfo.EmployeeNumber),int.Parse(ddlstatus.SelectedValue),txtSiba.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
    }
}