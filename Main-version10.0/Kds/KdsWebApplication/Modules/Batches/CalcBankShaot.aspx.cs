using KdsBatch.TaskManager;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KdsWebApplication.Modules.Batches
{
    public partial class CalcBankShaot : KdsPage
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtParametrim = new DataTable();
                clUtils oUtils = new clUtils();
                if (!Page.IsPostBack)
                {
                 
                    PageHeader = "הרצת בנק שעות";
                    dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
               
                    clGeneral.LoadDateCombo(ddlBank, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
                }
            }
            catch (Exception ex)
            {
                clGeneral.BuildError(Page, ex.Message);
            }
        }

        protected void btnRunBank_Click(object sender, EventArgs e)
        {
            DateTime month;
            int Mitkan;
            Utils clUtils = new Utils();
            long lRequestNum;
            string sMessage;
            DataTable dtRequest;
            clRequest objRequest = new clRequest();
            try
            {

                Mitkan = int.Parse(txtMitkan.Text);
                month = DateTime.Parse("01/" + ddlBank.SelectedValue);

                lRequestNum = clUtils.ChishuvBankShaotMeshekByParams(Mitkan, month);

                dtRequest = objRequest.GetLogBakashot(0, 0, lRequestNum, "-1", "E");
                if (dtRequest.Rows.Count > 0)
                {
                    sMessage = "קיימות שגיאות בטבלת לוג";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Err", "alert('" + sMessage + "');", true);
                }
                else
                {
                    sMessage = "ההרצה בוצעה בהצלחה";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Err", "alert('" + sMessage + "');", true);
                }

            }
            catch (Exception ex)
            {
                clGeneral.BuildError(Page, ex.Message);
            }
        }
      
    }
}