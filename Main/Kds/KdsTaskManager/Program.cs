using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace KdsTaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Utilities.EventLogSource = ConfigurationSettings.AppSettings["EventLogSource"].ToString();
            Utilities.Debug = (KdsLibrary.clGeneral.GetIntegerValue(ConfigurationSettings.AppSettings["Debug"].ToString()) == 1) ? true : false;
            Utilities.RecipientsList = (ConfigurationSettings.AppSettings["RecipientsMailList"].ToString()).Split(';');

            Utilities.WriteLog("Manager waked up ", Utilities.SeverityLevel.Information);
            Manager oManager = new Manager();
            if (oManager.HasSomethingToDo)
                oManager.Run();
            else

                Utilities.WriteLog("There is no activities to execute ...", Utilities.SeverityLevel.Information);
        }
    }
}
