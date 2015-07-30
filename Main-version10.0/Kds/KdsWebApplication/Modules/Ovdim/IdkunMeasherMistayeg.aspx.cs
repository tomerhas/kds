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
            DataView dv;
            try
            {
                lblMis.Text = txtId.Text;
                lblTaarich.Text = clnTaarich.Text;
                fsPratim.Style["display"] = "block";
              //  divPratim.Style["display"] = "block";
            }
            catch (Exception ex)
            {
                clGeneral.BuildError(Page, ex.Message);
            }
        }
   
    }
}