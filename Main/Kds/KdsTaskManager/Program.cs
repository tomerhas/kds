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
            if (Utilities.Debug)
                EventLog.WriteEntry(Utilities.EventLogSource, "Manager waked up ", EventLogEntryType.Information);
            Manager oManager = new Manager();
            if (oManager.HasSomethingToDo)
                oManager.Run();
            else
                if (Utilities.Debug)
                    EventLog.WriteEntry(Utilities.EventLogSource, "There is no activities to execute ...", EventLogEntryType.Information);
        }
    }
}
