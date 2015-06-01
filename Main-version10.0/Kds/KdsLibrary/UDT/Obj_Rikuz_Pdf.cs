namespace KdsLibrary.UDT
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;
    using System.Xml.Serialization;
    using System.Xml.Schema;


    public class OBJ_RIKUZ_PDF : INullable, IOracleCustomType, IXmlSerializable
    {

        private bool m_IsNull;

          private int m_MISPAR_ISHI;

        private bool m_MISPAR_ISHIIsNull;

        private long m_BAKASHA_ID;

        private bool m_BAKASHA_IDIsNull;

        private System.DateTime m_TAARICH;

        private bool m_TAARICHIsNull;

        private int m_SUG_CHISHUV;

        private bool m_SUG_CHISHUVIsNull;

        private byte[] m_RIKUZ_PDF;

        private bool m_RIKUZ_PDFIsNull;

        public OBJ_RIKUZ_PDF()
        {
            // TODO : Add code to initialise the object
            this.m_BAKASHA_IDIsNull = true;
            this.m_TAARICHIsNull = true;
            this.m_MISPAR_ISHIIsNull = true;
            this.m_SUG_CHISHUVIsNull = true;
            this.m_RIKUZ_PDFIsNull = true;
        }

        public OBJ_RIKUZ_PDF(string str)
        {
            // TODO : Add code to initialise the object based on the given string 
        }

        public virtual bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public static OBJ_RIKUZ_PDF Null
        {
            get
            {
                OBJ_RIKUZ_PDF obj = new OBJ_RIKUZ_PDF();
                obj.m_IsNull = true;
                return obj;
            }
        }

        [OracleObjectMappingAttribute("MISPAR_ISHI")]
        public int MISPAR_ISHI
        {
            get
            {
                return this.m_MISPAR_ISHI;
            }
            set
            {
                this.m_MISPAR_ISHI = value;
                this.m_MISPAR_ISHIIsNull = false;
            }
        }

        public bool MISPAR_ISHIIsNull
        {
            get
            {
                return this.m_MISPAR_ISHIIsNull;
            }
            set
            {
                this.m_MISPAR_ISHIIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("BAKASHA_ID")]
        public long BAKASHA_ID
        {
            get
            {
                return this.m_BAKASHA_ID;
            }
            set
            {
                this.m_BAKASHA_ID = value;
                this.m_BAKASHA_IDIsNull = false;
            }
        }

        public bool BAKASHA_IDIsNull
        {
            get
            {
                return this.m_BAKASHA_IDIsNull;
            }
            set
            {
                this.m_BAKASHA_IDIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("TAARICH")]
        public System.DateTime TAARICH
        {
            get
            {
                return this.m_TAARICH;
            }
            set
            {
                this.m_TAARICH = value;
                this.m_TAARICHIsNull = false;
            }
        }

        public bool TAARICHIsNull
        {
            get
            {
                return this.m_TAARICHIsNull;
            }
            set
            {
                this.m_TAARICHIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("SUG_CHISHUV")]
        public int SUG_CHISHUV
        {
            get
            {
                return this.m_SUG_CHISHUV;
            }
            set
            {
                this.m_SUG_CHISHUV = value;
                this.m_SUG_CHISHUVIsNull = false;
            }
        }

        public bool SUG_CHISHUVIsNull
        {
            get
            {
                return this.m_SUG_CHISHUVIsNull;
            }
            set
            {
                this.m_SUG_CHISHUVIsNull = value;
            }
        }


        [OracleObjectMappingAttribute("RIKUZ_PDF")]
        public byte[] RIKUZ_PDF
        {
            get
            {
                return this.m_RIKUZ_PDF;
            }
            set
            {
                this.m_RIKUZ_PDF = value;
                this.m_RIKUZ_PDFIsNull = false;
            }
        }

        public bool RIKUZ_PDFIsNull
        {
            get
            {
                return this.m_RIKUZ_PDFIsNull;
            }
            set
            {
                this.m_RIKUZ_PDFIsNull = value;
            }
        }

        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            if ((MISPAR_ISHIIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_ISHI", this.MISPAR_ISHI);
            }
            if ((BAKASHA_IDIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "BAKASHA_ID", this.BAKASHA_ID);
            }
            if ((TAARICHIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "TAARICH", this.TAARICH);
            }
            if ((SUG_CHISHUVIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SUG_CHISHUV", this.SUG_CHISHUV);
            }
            if ((RIKUZ_PDFIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "RIKUZ_PDF", this.RIKUZ_PDF);
            }
        }

        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            this.MISPAR_ISHIIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_ISHI");
            if ((MISPAR_ISHIIsNull == false))
            {
                this.MISPAR_ISHI = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_ISHI")));
            }
            this.BAKASHA_IDIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "BAKASHA_ID");
            if ((BAKASHA_IDIsNull == false))
            {
                this.BAKASHA_ID = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "BAKASHA_ID")));
            }
            this.TAARICHIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "TAARICH");
            if ((TAARICHIsNull == false))
            {
                this.TAARICH = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "TAARICH")));
            }
            this.SUG_CHISHUVIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SUG_CHISHUV");
            if ((SUG_CHISHUVIsNull == false))
            {
                this.SUG_CHISHUV = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SUG_CHISHUV")));
            }
            this.RIKUZ_PDFIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "RIKUZ_PDF");
            if ((MISPAR_ISHIIsNull == false))
            {
                this.RIKUZ_PDF = ((byte[])(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "RIKUZ_PDF")));
            }
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

        public static OBJ_RIKUZ_PDF Parse(string str)
        {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new OBJ_RIKUZ_PDF();
        }
    }

    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.OBJ_RIKUZ_PDF")]
    public class OBJ_RIKUZ_PDFFactory : IOracleCustomTypeFactory
    {

        public virtual IOracleCustomType CreateObject()
        {
            OBJ_RIKUZ_PDF obj = new OBJ_RIKUZ_PDF();
            return obj;
        }
    }
}
