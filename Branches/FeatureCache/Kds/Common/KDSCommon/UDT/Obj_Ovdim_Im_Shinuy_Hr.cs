//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KDSCommon.UDT
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;
    using System.Xml.Serialization;
    using System.Xml.Schema;
    
    
    public class OBJ_OVDIM_IM_SHINUY_HR : INullable, IOracleCustomType, IXmlSerializable {
        
        private bool m_IsNull;
        
        private System.DateTime m_TAARICH_IDKUN_HR;
        
        private bool m_TAARICH_IDKUN_HRIsNull;
        
        private System.DateTime m_TAARICH;
        
        private bool m_TAARICHIsNull;
        
        private decimal m_MISPAR_ISHI;
        
        private bool m_MISPAR_ISHIIsNull;
        
        private string m_TAVLA;

        private bool m_TAVLAIsNull;

        public OBJ_OVDIM_IM_SHINUY_HR() {
            // TODO : Add code to initialise the object
            this.m_TAARICH_IDKUN_HRIsNull = true;
            this.m_TAARICHIsNull = true;
            this.m_MISPAR_ISHIIsNull = true;
            this.m_TAVLAIsNull = true;
        }
        
        public OBJ_OVDIM_IM_SHINUY_HR(string str) {
            // TODO : Add code to initialise the object based on the given string 
        }
        
        public virtual bool IsNull {
            get {
                return this.m_IsNull;
            }
        }
        
        public static OBJ_OVDIM_IM_SHINUY_HR Null {
            get {
                OBJ_OVDIM_IM_SHINUY_HR obj = new OBJ_OVDIM_IM_SHINUY_HR();
                obj.m_IsNull = true;
                return obj;
            }
        }
        
        [OracleObjectMappingAttribute("TAARICH_IDKUN_HR")]
        public System.DateTime TAARICH_IDKUN_HR {
            get {
                return this.m_TAARICH_IDKUN_HR;
            }
            set {
                this.m_TAARICH_IDKUN_HR = value;
                this.m_TAARICH_IDKUN_HRIsNull = false;
            }
        }
        
        public bool TAARICH_IDKUN_HRIsNull {
            get {
                return this.m_TAARICH_IDKUN_HRIsNull;
            }
            set {
                this.m_TAARICH_IDKUN_HRIsNull = value;
            }
        }
        
        [OracleObjectMappingAttribute("TAARICH")]
        public System.DateTime TAARICH {
            get {
                return this.m_TAARICH;
            }
            set {
                this.m_TAARICH = value;
                this.m_TAARICHIsNull = false;
            }
        }
        
        public bool TAARICHIsNull {
            get {
                return this.m_TAARICHIsNull;
            }
            set {
                this.m_TAARICHIsNull = value;
            }
        }
        
        [OracleObjectMappingAttribute("MISPAR_ISHI")]
        public decimal MISPAR_ISHI {
            get {
                return this.m_MISPAR_ISHI;
            }
            set {
                this.m_MISPAR_ISHI = value;
                this.m_MISPAR_ISHIIsNull = false;
            }
        }
        
        public bool MISPAR_ISHIIsNull {
            get {
                return this.m_MISPAR_ISHIIsNull;
            }
            set {
                this.m_MISPAR_ISHIIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("TAVLA")]
        public string TAVLA
        {
            get
            {
                return this.m_TAVLA;
            }
            set
            {
                this.m_TAVLA = value;
                this.m_TAVLAIsNull = false;
            }
        }

        public bool TAVLAIsNull
        {
            get
            {
                return this.m_TAVLAIsNull;
            }
            set
            {
                this.m_TAVLAIsNull = value;
            }
        }
        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            if ((TAARICH_IDKUN_HRIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "TAARICH_IDKUN_HR", this.TAARICH_IDKUN_HR);
            }
            if ((TAARICHIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "TAARICH", this.TAARICH);
            }
            if ((MISPAR_ISHIIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_ISHI", this.MISPAR_ISHI);
            }
            if ((TAVLAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "TAVLA", this.TAVLA);
            }
        }
        
        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            this.TAARICH_IDKUN_HRIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "TAARICH_IDKUN_HR");
            if ((TAARICH_IDKUN_HRIsNull == false)) {
                this.TAARICH_IDKUN_HR = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "TAARICH_IDKUN_HR")));
            }
            this.TAARICHIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "TAARICH");
            if ((TAARICHIsNull == false)) {
                this.TAARICH = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "TAARICH")));
            }
            this.MISPAR_ISHIIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_ISHI");
            if ((MISPAR_ISHIIsNull == false)) {
                this.MISPAR_ISHI = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_ISHI")));
            }
            this.TAVLAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "TAVLA");
            if ((TAVLAIsNull == false))
            {
                this.TAVLA = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "TAVLA")));
            }
        }
        
        public virtual void ReadXml(System.Xml.XmlReader reader) {
            // TODO : Read Serialized Xml Data
        }
        
        public virtual void WriteXml(System.Xml.XmlWriter writer) {
            // TODO : Serialize object to xml data
        }
        
        public virtual XmlSchema GetSchema() {
            // TODO : Implement GetSchema
            return null;
        }
        
        public override string ToString() {
            // TODO : Return a string that represents the current object
            return "";
        }
        
        public static OBJ_OVDIM_IM_SHINUY_HR Parse(string str) {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new OBJ_OVDIM_IM_SHINUY_HR();
        }
    }
    
    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.OBJ_OVDIM_IM_SHINUY_HR")]
    public class OBJ_OVDIM_IM_SHINUY_HRFactory : IOracleCustomTypeFactory {
        
        public virtual IOracleCustomType CreateObject() {
            OBJ_OVDIM_IM_SHINUY_HR obj = new OBJ_OVDIM_IM_SHINUY_HR();
            return obj;
        }
    }
}
