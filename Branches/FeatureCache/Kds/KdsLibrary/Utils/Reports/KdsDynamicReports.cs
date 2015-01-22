using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Web;
using KdsLibrary.UI.SystemManager;
using DalOraInfra.DAL;

namespace KdsLibrary.Utils.Reports
{

    [XmlRoot("DynamicReport")]
    public class KdsDynamicReport
    {
        private List<KdsReport> _KdsReports;
        private KdsSysManResources _resources;

        [XmlElement("Report", typeof(KdsReport))]
        public List<KdsReport> ReportList
        {
            get { return _KdsReports; }
            set { _KdsReports = value; }
        }

        public KdsReport FindReport(string RdlName)
        {
            KdsReport _Report = null;
            _KdsReports.ForEach(delegate(KdsReport Report)
            {
                if (Report.RdlName == RdlName)
                    _Report = Report;
            });
            return _Report;
        }

        public static KdsDynamicReport GetKdsReport()
        {
            KdsDynamicReport DynamicReport = HttpContext.Current.GetApplicationStoredValue("DynamicsReports") as
                KdsDynamicReport;
            if (DynamicReport == null)
            {
                lock (HttpContext.Current.Application)
                {
                    var xmlDynamicReport = new XmlDocument();
                    xmlDynamicReport.Load(HttpContext.Current.Server.MapPath("~/Xml/DynamicsReports.xml"));
                    DynamicReport = (KdsDynamicReport)KdsLibrary.Utils.KdsExtensions.DeserializeObject(
                        typeof(KdsDynamicReport), xmlDynamicReport.OuterXml);
                    HttpContext.Current.AddApplicationStoredValue("DynamicsReports", DynamicReport);
                }
            }
            return DynamicReport;
        }

        [XmlElement("Resources", typeof(KdsSysManResources))]
        public KdsSysManResources Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }
        public override string ToString()
        {
            return KdsExtensions.SerializeObject(this);
        }

    }

    public class KdsReport
    {
        private string _RdlName, _Roles, _PageHeader;
        private List<KdsFilter> _FilterList;
        private int _FilterByLine;
        private List<KdsUserControl> _UserControls;
        private Orientation _Orientation;
        private ProductionType _ProductionType;
        private string _RSVersion;
        private int _ZoomPercent = 90 ;
        private string _SERVICE_URL_CONFIG_KEY;
        private string _URL_CONFIG_KEY;
        private string _EXTENSION;

        public KdsReport() { }

        [XmlAttribute("RdlName")]
        public string RdlName
        {
            get { return _RdlName; }
            set { _RdlName = value; }
        }
        
        [XmlAttribute("Orientation")]
        public Orientation Orientation
        {
            get { return _Orientation; }
            set { _Orientation= value; }
        }

        [XmlAttribute("ZoomPercent")]
        public int ZoomPercent
        {
            get { return _ZoomPercent; }
            set { _ZoomPercent = value; }
        }

        [XmlAttribute("ProductionType")]
        public ProductionType ProductionType 
        {
            get { return _ProductionType; }
            set { _ProductionType = value; }
        }

        [XmlAttribute("Roles")]
        public string Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }

        [XmlAttribute("FilterByLine")]
        public int FilterByLine
        {
            get { return _FilterByLine; }
            set { _FilterByLine = value; }
        }

        [XmlAttribute("PageHeader")]
        public string PageHeader
        {
            get { return _PageHeader; }
            set { _PageHeader = value; }
        }

        [XmlElement("Filter")]
        public List<KdsFilter> FilterList
        {
            get { return _FilterList; }
            set { _FilterList = value; }
        }

        [XmlElement("UserControl")]
        public List<KdsUserControl> UserControl
        {
            get { return _UserControls; }
            set { _UserControls = value; }
        }

        [XmlAttribute("RSVersion")]
        public string RSVersion
        {
            get { return _RSVersion; }
            set { _RSVersion = value; }
        }
        [XmlAttribute("URL_CONFIG_KEY")]
        public string URL_CONFIG_KEY
        {
            get { return _URL_CONFIG_KEY; }
            set { _URL_CONFIG_KEY = value; }
        }
        [XmlAttribute("SERVICE_URL_CONFIG_KEY")]
        public string SERVICE_URL_CONFIG_KEY
        {
            get { return _SERVICE_URL_CONFIG_KEY; }
            set { _SERVICE_URL_CONFIG_KEY = value; }
        }
        [XmlAttribute("EXTENSION")]
        public string EXTENSION
        {
            get { return _EXTENSION; }
            set { _EXTENSION = value; }
        }
        [XmlIgnore]
        public ReportName NameReport
        {
            get
            {
                return (ReportName)Enum.Parse(typeof(ReportName), _RdlName);
            }
        }


        public KdsUserControl FindUserControl(UserControlType Type)
        {
            KdsUserControl UcResult = new KdsUserControl();
            UcResult = (KdsUserControl)_UserControls.Find(delegate(KdsUserControl Uc)
            {
                return (Uc.Type == Type);
            });
            return UcResult;
        }
    }

    public class KdsFilter : IComparable<KdsFilter>
    {
        private string _ParameterName, _Caption, _DataType, _DefaultValue, _ErrorMessageValidator, _ClassName="";
        private bool _IsParent = false, _Required = false, _RunAtServer=false;
        private KdsBoxeType _BoxeType;
        private KdsListValueExtended _DropDownList;
        private List<FilterValidator> _FilterValidator; 

        public KdsFilter() { }

        public int CompareTo(KdsFilter other) 
        {
            return _ClassName.CompareTo(other.ClassName); 
        }

        [XmlAttribute("ClassName")]
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        [XmlAttribute("ParameterName")]
        public string ParameterName
        {
            get { return _ParameterName; }
            set { _ParameterName = value; }
        }

        [XmlAttribute("Caption")]
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }

        [XmlAttribute("DataType")]
        public string DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }

        [XmlAttribute("BoxeType")]
        public KdsBoxeType BoxeType
        {
            get { return _BoxeType; }
            set { _BoxeType = value; }
        }

        [XmlAttribute("IsParent")]
        public bool IsParent
        {
            get { return _IsParent; }
            set { _IsParent = value; }
        }

        [XmlAttribute("DefaultValue")]
        public string DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }
        [XmlAttribute("Required")]
        public bool Required
        {
            get { return _Required; }
            set { _Required = value; }
        }

        [XmlAttribute("RunAtServer")]
        public bool RunAtServer
        {
            get { return _RunAtServer; }
            set { _RunAtServer = value; }
        }

        [XmlElement("FilterValidator")]
        public List<FilterValidator> FilterValidator
        {
            get { return _FilterValidator; }
            set { _FilterValidator = value; }
        }

        [XmlElement("DropDownList")]
        public KdsListValueExtended DropDownList
        {
            get { return _DropDownList; }
            set { _DropDownList = value; }
        }
    }
    public class FilterValidator
    {
        private string _ErrorMessageValidator, _ValidationFunction, _ValidatorName;

        [XmlAttribute("ValidatorName")]
        public string ValidatorName
        {
            get { return _ValidatorName; }
            set { _ValidatorName = value; }
        }

        [XmlAttribute("ErrorMessageValidator")]
        public string ErrorMessageValidator
        {
            get { return _ErrorMessageValidator; }
            set { _ErrorMessageValidator = value; }
        }
        [XmlAttribute("ValidationFunction")]
        public string ValidationFunction
        {
            get { return _ValidationFunction; }
            set { _ValidationFunction = value; }
        }
    }

    public class KdsListValueExtended : KdsListValue
    {
        private List<ConditionFilter> _ConditionFilter;
        private KdsQueryType _QueryType;
        private List<ParameterOfFunction> _ParameterOfFunctions;
        private string _LibraryName;

        public KdsListValueExtended() { }

        [XmlAttribute("QueryType")]
        public KdsQueryType QueryType
        {
            get { return _QueryType; }
            set { _QueryType = value; }
        }

        [XmlAttribute("LibraryName")]
        public string LibraryName
        {
            get { return _LibraryName; }
            set { _LibraryName = value; }
        }

        [XmlElement("Condition")]
        public List<ConditionFilter> ConditionFilters
        {
            get { return _ConditionFilter; }
            set { _ConditionFilter = value; }
        }
        [XmlElement("ParameterOfFunction")]
        public List<ParameterOfFunction> ParameterOfFunctions
        {
            get { return _ParameterOfFunctions; }
            set { _ParameterOfFunctions = value; }
        }
    }
    public class KdsListOfElements : KdsListValueExtended
    {
        private string _Description;
        private string _ServiceMethod;
        private ElementType _Type;

        [XmlAttribute("Description")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [XmlAttribute("Type")]
        public ElementType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        [XmlAttribute("ServiceMethod")]
        public string ServiceMethod
        {
            get { return _ServiceMethod; }
            set { _ServiceMethod = value; }
        }

    }

    public class ConditionFilter
    {
        private string _Name, _ControlReference;
        private ParameterType _ParameterType;

        public ConditionFilter() { }



        [XmlAttribute("Name")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [XmlAttribute("ControlReference")]
        public string ControlReference
        {
            get { return _ControlReference; }
            set { _ControlReference = value; }
        }

        [XmlAttribute("ParameterType")]
        public ParameterType ParameterType
        {
            get { return _ParameterType; }
            set { _ParameterType = value; }
        }


    }

    public class ParameterOfFunction : ConditionFilter
    {
        private ParameterFunctionType _Type;
        private ParameterReferenceType _ReferenceType;
        private List<ParameterOfFunction> _ParameterOfFunctions;
        private string _value;
        private string _DataType;
        private string _LibraryName;

        [XmlAttribute("Type")]
        public ParameterFunctionType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        [XmlAttribute("Value")]
        public string Value
        {
            get
            {
                return _value;
            }
            set { _value = value; }
        }
        [XmlAttribute("DataType")]
        public string DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }

        [XmlAttribute("ReferenceType")]
        public ParameterReferenceType ReferenceType
        {
            get { return _ReferenceType; }
            set { _ReferenceType = value; }
        }

        [XmlAttribute("LibraryName")]
        public string LibraryName
        {
            get { return _LibraryName; }
            set { _LibraryName = value; }
        }

        [XmlElement("ParameterOfFunction")]
        public List<ParameterOfFunction> ParameterOfFunctions
        {
            get { return _ParameterOfFunctions; }
            set { _ParameterOfFunctions = value; }
        }

        [XmlIgnore]
        public object GetObjectValue
        {
            get { return _value; }
        }
    }

    public class KdsUserControl
    {
        private UserControlType _Type;
        private string _Parameter;
        private bool _Enabled;
        private KdsListOfElements _ListOfElements;

        [XmlAttribute("Type")]
        public UserControlType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        [XmlAttribute("Enabled")]
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        [XmlAttribute("Parameter")]
        public string Parameter
        {
            get { return _Parameter; }
            set { _Parameter = value; }
        }

        [XmlElement("ListOfElements")]
        public KdsListOfElements ListOfElements
        {
            get { return _ListOfElements; }
            set { _ListOfElements = value; }
        }

    }

    public enum KdsBoxeType
    {
        Button ,
        TextBox,
        Calendar,
        CheckBoxList,
        RadioButtonList,
        DropDown,
        ListBox,
        ListElementSelector,
        ListBoxExtended,
        DropDownCheckBoxes
    }

    public enum KdsQueryType
    {
        Select,
        StoredProcedure,
        Function,
        AjaxAutoComplete
    }

    public enum UserControlType
    {
        OvedName,
        ListOfElement
    }

    public enum ElementType
    {
        MisparIshi,
        Normal
    }

    public enum ParameterFunctionType
    {
        ByValue,
        ByReference
    }

    public enum ParameterReferenceType
    {
        Function,
        Property
    }

    public enum ReportName
    {
        IshurimLekartisAvoda=0,
        PrintWorkCard=1,
        WorkCard=2,
        WorkCardVisa=3,
        RikuzAvodaChodshi2=4,
        RikuzAvodaChodshiOnLine2=5,
        IshurimLerashemet=900,
        RptIturim = 300,
        Presence=200,
        DriverWithoutTacograph=500,
        DriverWithoutSignature=600,
        DisregardDrivers=400,
        DisregardDriversVisot=401,
        PremiotPresence=800,
        HashvaatRizotChishuv=1000,
        AbsentWorkers=700,
        HashvaatChodsheyRizotChishuv=1001,
        IdkuneyRashemet=901,
        Average=100,
        DescriptionAllComponents=2000,
        AverageLeOved=100,
        HityazvutBePundakimHitchashbenut=310,
        HityazvutBePundakimTlunot=311,
        HityazvutBePundakimKalkalit=312,
      //  PrintWorkCard=1,
        DriverWithPlaints=601,
        ChafifotSidureyNihulTnua=955,
        ReportNesiotKfulot=950,
        IdkuneyRashemetMasach4=902,
        Query4=956,
        DayDataEggt=316,
     //   RikuzAvodaChodshi2=4,
        SpecificAverage=110,
        ProfitOfLinesDetailed=812,
        ProfitOfLinesGroup=813,
        RdlPrecenceOvdeyMeshekShishiShabat=302,
        RdlCountOvdeyMeshekShabat=304,
        RdlReportMushalimDetails=201,
        KamutIdkuneyRashemet=903,
        Tigburim=202,
        AverageSnifEzor=113,
        AverageSnifInEzor=112,
        AverageOvdimBeSnif=111,
        Keytanot=957,
        RptRitzatChishuv=1002,
        RptSidureyVaadOvdim=301,
        RdlOvdimPeilim = 203

    }
    public enum Orientation
    {
        Portrait, 
        Landscape ,
    }
    public enum ProductionType
    {
        Normal,
        Heavy
    }
}
