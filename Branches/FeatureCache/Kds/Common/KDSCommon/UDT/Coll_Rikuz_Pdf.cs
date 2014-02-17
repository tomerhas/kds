
namespace KDSCommon.UDT
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;
    using System.Xml.Serialization;
    using System.Xml.Schema;
    using KDSCommon.Helpers;


    public class COLL_RIKUZ_PDF : INullable, IOracleCustomType, IXmlSerializable
    {

        private bool m_IsNull;

        private OBJ_RIKUZ_PDF[] m_OBJ_RIKUZ_PDF;

        public COLL_RIKUZ_PDF()
        {
            // TODO : Add code to initialise the object
            this.m_IsNull = true;
        }

        public COLL_RIKUZ_PDF(string str)
        {
            // TODO : Add code to initialise the object based on the given string 
        }

        public void Add(OBJ_RIKUZ_PDF Object)
        {
            if (Object.IsNull) return;

            int Size;
            if (m_OBJ_RIKUZ_PDF == null)
            {
                m_OBJ_RIKUZ_PDF = new OBJ_RIKUZ_PDF[1];
                m_OBJ_RIKUZ_PDF[0] = Object;
            }
            else
            {
                Size = m_OBJ_RIKUZ_PDF.Length;
                m_OBJ_RIKUZ_PDF = (OBJ_RIKUZ_PDF[])ArrayHelper.ResizeArray(m_OBJ_RIKUZ_PDF, Size + 1);
                m_OBJ_RIKUZ_PDF[Size] = Object;
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

        public static COLL_RIKUZ_PDF Null
        {
            get
            {
                COLL_RIKUZ_PDF obj = new COLL_RIKUZ_PDF();
                obj.m_IsNull = true;
                return obj;
            }
        }

        [OracleArrayMappingAttribute()]
        public virtual OBJ_RIKUZ_PDF[] Value
        {
            get
            {
                return this.m_OBJ_RIKUZ_PDF;
            }
            set
            {
                this.m_OBJ_RIKUZ_PDF = value;
            }
        }

        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            if (this.IsNull == false)
            {
                OracleUdt.SetValue(con, pUdt, 0, this.m_OBJ_RIKUZ_PDF);
            }
        }

        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            this.m_OBJ_RIKUZ_PDF = ((OBJ_RIKUZ_PDF[])(OracleUdt.GetValue(con, pUdt, 0)));
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

        public static COLL_RIKUZ_PDF Parse(string str)
        {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new COLL_RIKUZ_PDF();
        }
    }

    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.COLL_RIKUZ_PDF")]
    public class COLL_RIKUZ_PDFFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory
    {

        public virtual IOracleCustomType CreateObject()
        {
            COLL_RIKUZ_PDF obj = new COLL_RIKUZ_PDF();
            return obj;
        }

        public virtual System.Array CreateArray(int length)
        {
            OBJ_RIKUZ_PDF[] collElem = new OBJ_RIKUZ_PDF[length];
            return collElem;
        }

        public virtual System.Array CreateStatusArray(int length)
        {
            return null;
        }
    }
}

