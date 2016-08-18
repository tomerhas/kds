//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
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
    using KdsLibrary.Utils;
    
    
    public class COLL_CHISHUV_CHODESH : INullable, IOracleCustomType, IXmlSerializable {
        
        private bool m_IsNull;
        
        private OBJ_CHISHUV_CHODESH[] m_OBJ_CHISHUV_CHODESH;
        
        public COLL_CHISHUV_CHODESH() {
            // TODO : Add code to initialise the object
            this.m_IsNull = true;
        }
        
        public COLL_CHISHUV_CHODESH(string str) {
            // TODO : Add code to initialise the object based on the given string 
        }

        public void Add(OBJ_CHISHUV_CHODESH Object)
        {
            if (Object.IsNull) return;

            int Size ;
            if (m_OBJ_CHISHUV_CHODESH == null)
            {
                m_OBJ_CHISHUV_CHODESH = new OBJ_CHISHUV_CHODESH[1];
                m_OBJ_CHISHUV_CHODESH[0] = Object;
            }
            else
            {
                Size = m_OBJ_CHISHUV_CHODESH.Length;
                m_OBJ_CHISHUV_CHODESH = (OBJ_CHISHUV_CHODESH[])clGeneral.ResizeArray(m_OBJ_CHISHUV_CHODESH, Size + 1);
                m_OBJ_CHISHUV_CHODESH[Size] = Object;
             }
            this.m_IsNull = false;
        }

      public virtual bool IsNull {
            get {
                return this.m_IsNull;
            }
        }
        
        public static COLL_CHISHUV_CHODESH Null {
            get {
                COLL_CHISHUV_CHODESH obj = new COLL_CHISHUV_CHODESH();
                obj.m_IsNull = true;
                return obj;
            }
        }
        
        [OracleArrayMappingAttribute()]
        public virtual OBJ_CHISHUV_CHODESH[] Value {
            get {
                return this.m_OBJ_CHISHUV_CHODESH;
            }
            set {
                this.m_OBJ_CHISHUV_CHODESH = value;
            }
        }

        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            if (this.IsNull == false)
            {
                OracleUdt.SetValue(con, pUdt, 0, this.m_OBJ_CHISHUV_CHODESH);
            }
        }
        
        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            this.m_OBJ_CHISHUV_CHODESH = ((OBJ_CHISHUV_CHODESH[])(OracleUdt.GetValue(con, pUdt, 0)));
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
        
        public static COLL_CHISHUV_CHODESH Parse(string str) {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new COLL_CHISHUV_CHODESH();
        }
    }
    
    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.COLL_CHISHUV_CHODESH")]
    public class COLL_CHISHUV_CHODESHFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory {
        
        public virtual IOracleCustomType CreateObject() {
            COLL_CHISHUV_CHODESH obj = new COLL_CHISHUV_CHODESH();
            return obj;
        }
        
        public virtual System.Array CreateArray(int length) {
            OBJ_CHISHUV_CHODESH[] collElem = new OBJ_CHISHUV_CHODESH[length];
            return collElem;
        }
        
        public virtual System.Array CreateStatusArray(int length) {
            return null;
        }
    }
}
