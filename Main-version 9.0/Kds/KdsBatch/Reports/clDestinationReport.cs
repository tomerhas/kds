using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Reports
{
    public class clDestinationReport
    {
        private TypeSending _TypeSending;
        private int _Kod;
        private string _Folder;
        private string _eMail;
        private int _MisparIshi;

        public TypeSending TypeSending
        {
            get { return _TypeSending; }
            //  set { _sValue = value; }
        }
        public int Kod
        {
            get { return _Kod; }
            //  set { _sValue = value; }
        }
        public int MisparIshi
        {
            get { return _MisparIshi; }
            //  set { _sValue = value; }
        }        
        public string Folder
        {
            get { return _Folder; }
            //  set { _sValue = value; }
        }
        public string eMail
        {
            get { return _eMail; }
            //  set { _sValue = value; }
        }
        public clDestinationReport(TypeSending type, int kod, string TypeValue, int MisparIshi) : this(type,kod,TypeValue)
        {
            _MisparIshi = MisparIshi;
        }

        public clDestinationReport(TypeSending type, int kod, string TypeValue)
        {
            _TypeSending = type;
            _Kod = kod;

            switch (type)
            {
                case TypeSending.EMail:
                    _eMail = TypeValue;    
                    break;
                case TypeSending.Folder:
                    _Folder = TypeValue;
                    break;
            }
        } 
    }
    
    public enum TypeSending
    {
        EMail,
        Folder
    }
}
