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
    
    
    public class COLL_OBJ_PEILUT_OVDIM : INullable, IOracleCustomType, IXmlSerializable {
        
        private bool m_IsNull;
        private int _Count;
        private OBJ_PEILUT_OVDIM[] m_OBJ_PEILUT_OVDIM;
        
        public COLL_OBJ_PEILUT_OVDIM() {
            // TODO : Add code to initialise the object
        }
        
        public COLL_OBJ_PEILUT_OVDIM(string str) {
            // TODO : Add code to initialise the object based on the given string 
        }
        
        public virtual bool IsNull {
            get {
                return this.m_IsNull;
            }
        }
        public int Count
        {
            get
            {
                return this._Count;
            }
        }

        public void Add(OBJ_PEILUT_OVDIM Object)
        {
            if (Object.IsNull) return;

            int Size;
            if (m_OBJ_PEILUT_OVDIM == null)
            {
                m_OBJ_PEILUT_OVDIM = new OBJ_PEILUT_OVDIM[1];
                m_OBJ_PEILUT_OVDIM[0] = Object;
            }
            else
            {
                Size = m_OBJ_PEILUT_OVDIM.Length;
                m_OBJ_PEILUT_OVDIM = (OBJ_PEILUT_OVDIM[])clGeneral.ResizeArray(m_OBJ_PEILUT_OVDIM, Size + 1);
                m_OBJ_PEILUT_OVDIM[Size] = Object;
            }
            _Count = m_OBJ_PEILUT_OVDIM.Length;
        }

        public void RemoveAt(int index)
        {
            OBJ_PEILUT_OVDIM[] newObj = new OBJ_PEILUT_OVDIM[m_OBJ_PEILUT_OVDIM.Length - 1];
            for (int i = 0; i < m_OBJ_PEILUT_OVDIM.Length; i++)
            {
                if (i < index) newObj[i] = m_OBJ_PEILUT_OVDIM[i];
                if (i > index) newObj[i - 1] = m_OBJ_PEILUT_OVDIM[i];
            }
            m_OBJ_PEILUT_OVDIM = newObj;
            _Count = m_OBJ_PEILUT_OVDIM.Length;
        }

        public static COLL_OBJ_PEILUT_OVDIM Null {
            get {
                COLL_OBJ_PEILUT_OVDIM obj = new COLL_OBJ_PEILUT_OVDIM();
                obj.m_IsNull = true;
                return obj;
            }
        }
        
        [OracleArrayMappingAttribute()]
        public virtual OBJ_PEILUT_OVDIM[] Value {
            get {
                return this.m_OBJ_PEILUT_OVDIM;
            }
            set {
                this.m_OBJ_PEILUT_OVDIM = value;
            }
        }
        
        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            OracleUdt.SetValue(con, pUdt, 0, this.m_OBJ_PEILUT_OVDIM);
        }
        
        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt) {
            this.m_OBJ_PEILUT_OVDIM = ((OBJ_PEILUT_OVDIM[])(OracleUdt.GetValue(con, pUdt, 0)));
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
        
        public static COLL_OBJ_PEILUT_OVDIM Parse(string str) {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new COLL_OBJ_PEILUT_OVDIM();
        }
    }
    
    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.COLL_OBJ_PEILUT_OVDIM")]
    public class COLL_OBJ_PEILUT_OVDIMFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory {
        
        public virtual IOracleCustomType CreateObject() {
            COLL_OBJ_PEILUT_OVDIM obj = new COLL_OBJ_PEILUT_OVDIM();
            return obj;
        }
        
        public virtual System.Array CreateArray(int length) {
            OBJ_PEILUT_OVDIM[] collElem = new OBJ_PEILUT_OVDIM[length];
            return collElem;
        }
        
        public virtual System.Array CreateStatusArray(int length) {
            return null;
        }
    }
}
