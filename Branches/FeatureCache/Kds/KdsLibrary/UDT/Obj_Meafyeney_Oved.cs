//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KdsLibrary.UDT
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;
    using System.Xml.Serialization;
    using System.Xml.Schema;
    
    
    public class OBJ_MEAFYENEY_OVED : INullable, IOracleCustomType, IXmlSerializable {
        
        private bool m_IsNull;
        
        private decimal m_LINA;
        
        private bool m_LINAIsNull;
        
        private decimal m_BITUL_ZMAN_NESIOT;
        
        private bool m_BITUL_ZMAN_NESIOTIsNull;
        
        private string m_MAAMAD;
        
        private decimal m_HALBASHA;
        
        private bool m_HALBASHAIsNull;
        
        private string m_ISUK;
        
        private decimal m_MISPAR_ISHI;
        
        private bool m_MISPAR_ISHIIsNull;
        
        private decimal m_HASHLAMA_LEYOM;
        
        private bool m_HASHLAMA_LEYOMIsNull;
        
        private decimal m_KOD_HEVRA;
        
        private bool m_KOD_HEVRAIsNull;
        
        public OBJ_MEAFYENEY_OVED() {
            // TODO : Add code to initialise the object
            this.m_LINAIsNull = true;
            this.m_BITUL_ZMAN_NESIOTIsNull = true;
            this.m_HALBASHAIsNull = true;
            this.m_MISPAR_ISHIIsNull = true;
            this.m_HASHLAMA_LEYOMIsNull = true;
            this.m_KOD_HEVRAIsNull = true;
        }
        
        public OBJ_MEAFYENEY_OVED(string str) {
            // TODO : Add code to initialise the object based on the given string 
        }
        
        public virtual bool IsNull {
            get {
                return this.m_IsNull;
            }
        }
        
        public static OBJ_MEAFYENEY_OVED Null {
            get {
                OBJ_MEAFYENEY_OVED obj = new OBJ_MEAFYENEY_OVED();
                obj.m_IsNull = true;
                return obj;
            }
        }
        
        [OracleObjectMappingAttribute("LINA")]
        public decimal LINA {
            get {
                return this.m_LINA;
            }
            set {
                this.m_LINA = value;
            }
        }
        
        public bool LINAIsNull {
            get {
                return this.m_LINAIsNull;
            }
            set {
                this.m_LINAIsNull = value;
            }
        }
        
        [OracleObjectMappingAttribute("BITUL_ZMAN_NESIOT")]
        public decimal BITUL_ZMAN_NESIOT {
            get {
                return this.m_BITUL_ZMAN_NESIOT;
            }
            set {
                this.m_BITUL_ZMAN_NESIOT = value;
            }
        }
        
        public bool BITUL_ZMAN_NESIOTIsNull {
            get {
                return this.m_BITUL_ZMAN_NESIOTIsNull;
            }
            set {
                this.m_BITUL_ZMAN_NESIOTIsNull = value;
            }
        }
        
        [OracleObjectMappingAttribute("MAAMAD")]
        public string MAAMAD {
            get {
                return this.m_MAAMAD;
            }
            set {
                this.m_MAAMAD = value;
            }
        }
        
        [OracleObjectMappingAttribute("HALBASHA")]
        public decimal HALBASHA {
            get {
                return this.m_HALBASHA;
            }
            set {
                this.m_HALBASHA = value;
            }
        }
        
        public bool HALBASHAIsNull {
            get {
                return this.m_HALBASHAIsNull;
            }
            set {
                this.m_HALBASHAIsNull = value;
            }
        }
        
        [OracleObjectMappingAttribute("ISUK")]
        public string ISUK {
            get {
                return this.m_ISUK;
            }
            set {
                this.m_ISUK = value;
            }
        }
        
        [OracleObjectMappingAttribute("MISPAR_ISHI")]
        public decimal MISPAR_ISHI {
            get {
                return this.m_MISPAR_ISHI;
            }
            set {
                this.m_MISPAR_ISHI = value;
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
        
        [OracleObjectMappingAttribute("HASHLAMA_LEYOM")]
        public decimal HASHLAMA_LEYOM {
            get {
                return this.m_HASHLAMA_LEYOM;
            }
            set {
                this.m_HASHLAMA_LEYOM = value;
            }
        }
        
        public bool HASHLAMA_LEYOMIsNull {
            get {
                return this.m_HASHLAMA_LEYOMIsNull;
            }
            set {
                this.m_HASHLAMA_LEYOMIsNull = value;
            }
        }
        
        [OracleObjectMappingAttribute("KOD_HEVRA")]
        public decimal KOD_HEVRA {
            get {
                return this.m_KOD_HEVRA;
            }
            set {
                this.m_KOD_HEVRA = value;
            }
        }
        
        public bool KOD_HEVRAIsNull {
            get {
                return this.m_KOD_HEVRAIsNull;
            }
            set {
                this.m_KOD_HEVRAIsNull = value;
            }
        }
        
        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            if ((LINAIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "LINA", this.LINA);
            }
            if ((BITUL_ZMAN_NESIOTIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "BITUL_ZMAN_NESIOT", this.BITUL_ZMAN_NESIOT);
            }
            Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MAAMAD", this.MAAMAD);
            if ((HALBASHAIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "HALBASHA", this.HALBASHA);
            }
            Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "ISUK", this.ISUK);
            if ((MISPAR_ISHIIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_ISHI", this.MISPAR_ISHI);
            }
            if ((HASHLAMA_LEYOMIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "HASHLAMA_LEYOM", this.HASHLAMA_LEYOM);
            }
            if ((KOD_HEVRAIsNull == false)) {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "KOD_HEVRA", this.KOD_HEVRA);
            }
        }
        
        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            this.LINAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "LINA");
            if ((LINAIsNull == false)) {
                this.LINA = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "LINA")));
            }
            this.BITUL_ZMAN_NESIOTIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "BITUL_ZMAN_NESIOT");
            if ((BITUL_ZMAN_NESIOTIsNull == false)) {
                this.BITUL_ZMAN_NESIOT = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "BITUL_ZMAN_NESIOT")));
            }
            this.MAAMAD = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MAAMAD")));
            this.HALBASHAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "HALBASHA");
            if ((HALBASHAIsNull == false)) {
                this.HALBASHA = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "HALBASHA")));
            }
            this.ISUK = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "ISUK")));
            this.MISPAR_ISHIIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_ISHI");
            if ((MISPAR_ISHIIsNull == false)) {
                this.MISPAR_ISHI = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_ISHI")));
            }
            this.HASHLAMA_LEYOMIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "HASHLAMA_LEYOM");
            if ((HASHLAMA_LEYOMIsNull == false)) {
                this.HASHLAMA_LEYOM = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "HASHLAMA_LEYOM")));
            }
            this.KOD_HEVRAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "KOD_HEVRA");
            if ((KOD_HEVRAIsNull == false)) {
                this.KOD_HEVRA = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "KOD_HEVRA")));
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
        
        public static OBJ_MEAFYENEY_OVED Parse(string str) {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new OBJ_MEAFYENEY_OVED();
        }
    }
    
    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.OBJ_MEAFYENEY_OVED")]
    public class OBJ_MEAFYENEY_OVEDFactory : IOracleCustomTypeFactory {
        
        public virtual IOracleCustomType CreateObject() {
            OBJ_MEAFYENEY_OVED obj = new OBJ_MEAFYENEY_OVED();
            return obj;
        }
    }
}
