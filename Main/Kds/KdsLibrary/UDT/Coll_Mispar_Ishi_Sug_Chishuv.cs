
namespace KdsLibrary.UDT
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;
    using System.Xml.Serialization;
    using System.Xml.Schema;
    using KdsLibrary.Utils;


    public class COLL_MISPAR_ISHI_SUG_CHISHUV : INullable, IOracleCustomType, IXmlSerializable
    {

        private bool m_IsNull;

        private OBJ_MISPAR_ISHI_SUG_CHISHUV[] m_OBJ_MISPAR_ISHI_SUG_CHISHUV;

        public COLL_MISPAR_ISHI_SUG_CHISHUV()
        {
            // TODO : Add code to initialise the object
            this.m_IsNull = true;
        }

        public COLL_MISPAR_ISHI_SUG_CHISHUV(string str)
        {
            // TODO : Add code to initialise the object based on the given string 
        }

        public void Add(OBJ_MISPAR_ISHI_SUG_CHISHUV Object)
        {
            if (Object.IsNull) return;

            int Size;
            if (m_OBJ_MISPAR_ISHI_SUG_CHISHUV == null)
            {
                m_OBJ_MISPAR_ISHI_SUG_CHISHUV = new OBJ_MISPAR_ISHI_SUG_CHISHUV[1];
                m_OBJ_MISPAR_ISHI_SUG_CHISHUV[0] = Object;
            }
            else
            {
                Size = m_OBJ_MISPAR_ISHI_SUG_CHISHUV.Length;
                m_OBJ_MISPAR_ISHI_SUG_CHISHUV = (OBJ_MISPAR_ISHI_SUG_CHISHUV[])clGeneral.ResizeArray(m_OBJ_MISPAR_ISHI_SUG_CHISHUV, Size + 1);
                m_OBJ_MISPAR_ISHI_SUG_CHISHUV[Size] = Object;
            }
            this.m_IsNull = false;
        }

        public virtual bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public static COLL_MISPAR_ISHI_SUG_CHISHUV Null
        {
            get
            {
                COLL_MISPAR_ISHI_SUG_CHISHUV obj = new COLL_MISPAR_ISHI_SUG_CHISHUV();
                obj.m_IsNull = true;
                return obj;
            }
        }

        [OracleArrayMappingAttribute()]
        public virtual OBJ_MISPAR_ISHI_SUG_CHISHUV[] Value
        {
            get
            {
                return this.m_OBJ_MISPAR_ISHI_SUG_CHISHUV;
            }
            set
            {
                this.m_OBJ_MISPAR_ISHI_SUG_CHISHUV = value;
            }
        }

        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            if (this.IsNull == false)
            {
                OracleUdt.SetValue(con, pUdt, 0, this.m_OBJ_MISPAR_ISHI_SUG_CHISHUV);
            }
        }

        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            this.m_OBJ_MISPAR_ISHI_SUG_CHISHUV = ((OBJ_MISPAR_ISHI_SUG_CHISHUV[])(OracleUdt.GetValue(con, pUdt, 0)));
        }

        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            // TODO : Read Serialized Xml Data
        }

        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            // TODO : Serialize object to xml data
        }

        public virtual XmlSchema GetSchema()
        {
            // TODO : Implement GetSchema
            return null;
        }

        public override string ToString()
        {
            // TODO : Return a string that represents the current object
            return "";
        }

        public static COLL_MISPAR_ISHI_SUG_CHISHUV Parse(string str)
        {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new COLL_MISPAR_ISHI_SUG_CHISHUV();
        }
    }

    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.COLL_MISPAR_ISHI_SUG_CHISHUV")]
    public class COLL_MISPAR_ISHI_SUG_CHISHUVFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory
    {

        public virtual IOracleCustomType CreateObject()
        {
            COLL_MISPAR_ISHI_SUG_CHISHUV obj = new COLL_MISPAR_ISHI_SUG_CHISHUV();
            return obj;
        }

        public virtual System.Array CreateArray(int length)
        {
            OBJ_MISPAR_ISHI_SUG_CHISHUV[] collElem = new OBJ_MISPAR_ISHI_SUG_CHISHUV[length];
            return collElem;
        }

        public virtual System.Array CreateStatusArray(int length)
        {
            return null;
        }
    }
}

