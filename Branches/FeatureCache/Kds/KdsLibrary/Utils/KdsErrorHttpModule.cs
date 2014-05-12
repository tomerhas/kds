using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Diagnostics;
using System.ComponentModel;

namespace KdsLibrary.Utils
{
    public class KdsErrorHttpModule : IHttpModule
    {
         #region IHttpModule Members
        public void Init(HttpApplication application)
        {
            application.Error += new EventHandler(application_Error);
        }
        public void Dispose() { }
        #endregion

        public void application_Error(object sender, EventArgs e)
        {
            //Exception exc = HttpContext.Current.Server.GetLastError();
            //if (exc != null)
            //{
            //    if (!exc.IsFileNotFoundException())
            //    {
            //        clGeneral.LogError(exc);
            //        if (HttpContext.Current.Session != null)
            //            HttpContext.Current.Session["LastError"] = exc;
            //    }
            //    string qString = GetQueryStringForErrorPage(exc);
            //    //string redirectUrl = String.Format("{0}/ErrorPage.aspx?{1}",
            //    //    HttpContext.Current.Request.ApplicationPath,
            //    //    qString);
            //    string redirectUrl = String.Format("ErrorPage.aspx?{0}",
            //       qString);
            //    //HttpContext.Current.Server.ClearError();
            //    //HttpContext.Current.Response.Redirect(redirectUrl);
            //    HttpContext.Current.Server.Transfer(redirectUrl);
            //}
        }
      
        private string GetQueryStringForErrorPage(Exception exc)
        {
            string qsData = "LastError=" + exc.Message;
            string encoded = String.Empty;
            if (exc.IsFileNotFoundException())
                qsData += "&ErrorCode=404";
            return qsData;
        }

        
    }

}
