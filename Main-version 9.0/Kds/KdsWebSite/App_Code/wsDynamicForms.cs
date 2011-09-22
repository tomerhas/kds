using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using KdsLibrary.BL;
using System.Data;
using System.Collections.Generic;
using KdsLibrary;

/// <summary>
/// Summary description for wsSidurim
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class wsDynamicForms : System.Web.Services.WebService
{
    private clDynamicForms _DynamicFormsBl;
    public wsDynamicForms()
    {
        _DynamicFormsBl = clDynamicForms.GetInstance();
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetKodOfDescription(string Description, int DynamicFormType)
    {
        string sKod = string.Empty;
        try
        {
            if (Description != string.Empty)
                sKod = _DynamicFormsBl.GetKodOfDescription(Description, (clGeneral.enDynamicFormType)DynamicFormType);
            return sKod;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public string GetDescriptionOfKod(int kod, int DynamicFormType)
    {
        string description = string.Empty;
        try
        {
            if (kod != 0)
                description = _DynamicFormsBl.GetDescriptionOfKod(kod, (clGeneral.enDynamicFormType)DynamicFormType);
            return description;
        }
        catch
        {
            return description ;
        }
    }

    #region AutoComplete functions
    [WebMethod]
    public string[] GetMatchingKod(string prefixText, int count, string contextKey)
    {
        int iSidurimForm = 1;
        bool result;
        result = Int32.TryParse(contextKey, out iSidurimForm);

        try
        {
            return _DynamicFormsBl.GetMatchingKod(prefixText, (clGeneral.enDynamicFormType)iSidurimForm);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }
    [WebMethod]
    public string[] GetMatchingDescription(string prefixText, int count, string contextKey)
    {
        int iSidurimForm = 1;
        bool result;
        result = Int32.TryParse(contextKey, out iSidurimForm);

        try
        {
            return _DynamicFormsBl.GetMatchingDescription(prefixText, (clGeneral.enDynamicFormType)iSidurimForm);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }
    #endregion
}

