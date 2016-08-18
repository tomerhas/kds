using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace KdsBatch.Premia
{
    /// <summary>
    /// Contains settings for premia calculatino routines
    /// </summary>
    public class PremiaSettings
    {
        private const string PERIOD_FORMAT = "MMyyyy";
        public string PremiaRootPath
        {
            get
            {
                string root = ConfigurationManager.AppSettings["PremiaFolder"];
                if (!root.EndsWith("\\")) root += "\\";
                return root;
            }
        }

        public string PremiaInputFileName
        {
            get { return ConfigurationManager.AppSettings["PremiaInputFile"]; }
        }

        public string PremiaMacroFileName
        {
            get { return ConfigurationManager.AppSettings["PremiaMacroFile"]; }
        }

        public string GetInputFullFilePath(DateTime period)
        {
            return String.Concat(GetInputFolderPath(period), "\\",
                PremiaInputFileName);
        }

        public string GetMacroFullPath(DateTime period)
        {
            return String.Concat(PremiaRootPath, period.ToString(PERIOD_FORMAT), "\\",
                PremiaMacroFileName);
        }

        public bool IsInputFolderExists(DateTime period)
        {
            return Directory.Exists(String.Concat(PremiaRootPath, period.ToString(PERIOD_FORMAT)));
        }

        public bool IsMacroFileExists(DateTime period)
        {
            return File.Exists(GetMacroFullPath(period));
        }

        public string GetInputFolderPath(DateTime period)
        {
            return String.Concat(PremiaRootPath, period.ToString(PERIOD_FORMAT)); 
        }
    }
}
