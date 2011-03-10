﻿//------------------------------------------------------------------------------
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

    public class OBJ_LOGTASKMSG : INullable, IOracleCustomType, IXmlSerializable
    {

        private bool m_IsNull;

        private decimal m_IDORDER;

        private bool m_IDORDERIsNull;

        private decimal m_STATUSID;

        private bool m_STATUSIDIsNull;

        private decimal m_SEQUENCE;

        private bool m_SEQUENCEIsNull;

        private System.DateTime m_STARTTIME;

        private bool m_STARTTIMEIsNull;

        private System.DateTime m_ENDTIME;

        private bool m_ENDTIMEIsNull;

        private string m_REMARK;

        private decimal m_IDGROUP;

        private bool m_IDGROUPIsNull;

        public OBJ_LOGTASKMSG()
        {
            // TODO : Add code to initialise the object
        }

        public OBJ_LOGTASKMSG(int Group , int Order , int Status , int Sequence , DateTime StartTime , DateTime EndTime, string Remark)  //: this ()
        {
            m_IDGROUP = Group;
            m_IDORDER = Order;
            m_STATUSID = Status;
            m_SEQUENCE = Sequence;
            m_STARTTIME = StartTime;
            m_ENDTIME = EndTime;
            m_REMARK = Remark;
            this.m_IDGROUPIsNull = false;
            this.m_IDORDERIsNull = false;
            this.m_STATUSIDIsNull = false;
            this.m_SEQUENCEIsNull = false;
            this.m_STARTTIMEIsNull = (StartTime == DateTime.MinValue) ? true: false;
            this.m_ENDTIMEIsNull = (EndTime == DateTime.MinValue)? true: false;
        }

        public virtual bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public static OBJ_LOGTASKMSG Null
        {
            get
            {
                OBJ_LOGTASKMSG obj = new OBJ_LOGTASKMSG();
                obj.m_IsNull = true;
                return obj;
            }
        }

        [OracleObjectMappingAttribute("IDORDER")]
        public decimal IDORDER
        {
            get
            {
                return this.m_IDORDER;
            }
            set
            {
                this.m_IDORDER = value;
            }
        }

        public bool IDORDERIsNull
        {
            get
            {
                return this.m_IDORDERIsNull;
            }
            set
            {
                this.m_IDORDERIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("STATUSID")]
        public decimal STATUSID
        {
            get
            {
                return this.m_STATUSID;
            }
            set
            {
                this.m_STATUSID = value;
            }
        }

        public bool STATUSIDIsNull
        {
            get
            {
                return this.m_STATUSIDIsNull;
            }
            set
            {
                this.m_STATUSIDIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("SEQUENCE")]
        public decimal SEQUENCE
        {
            get
            {
                return this.m_SEQUENCE;
            }
            set
            {
                this.m_SEQUENCE = value;
            }
        }

        public bool SEQUENCEIsNull
        {
            get
            {
                return this.m_SEQUENCEIsNull;
            }
            set
            {
                this.m_SEQUENCEIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("STARTTIME")]
        public System.DateTime STARTTIME
        {
            get
            {
                return this.m_STARTTIME;
            }
            set
            {
                this.m_STARTTIME = value;
            }
        }

        public bool STARTTIMEIsNull
        {
            get
            {
                return this.m_STARTTIMEIsNull;
            }
            set
            {
                this.m_STARTTIMEIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("ENDTIME")]
        public System.DateTime ENDTIME
        {
            get
            {
                return this.m_ENDTIME;
            }
            set
            {
                this.m_ENDTIME = value;
            }
        }

        public bool ENDTIMEIsNull
        {
            get
            {
                return this.m_ENDTIMEIsNull;
            }
            set
            {
                this.m_ENDTIMEIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("REMARK")]
        public string REMARK
        {
            get
            {
                return this.m_REMARK;
            }
            set
            {
                this.m_REMARK = value;
            }
        }

        [OracleObjectMappingAttribute("IDGROUP")]
        public decimal IDGROUP
        {
            get
            {
                return this.m_IDGROUP;
            }
            set
            {
                this.m_IDGROUP = value;
            }
        }

        public bool IDGROUPIsNull
        {
            get
            {
                return this.m_IDGROUPIsNull;
            }
            set
            {
                this.m_IDGROUPIsNull = value;
            }
        }

        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            if ((IDORDERIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "IDORDER", this.IDORDER);
            }
            if ((STATUSIDIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "STATUSID", this.STATUSID);
            }
            if ((SEQUENCEIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SEQUENCE", this.SEQUENCE);
            }
            if ((STARTTIMEIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "STARTTIME", this.STARTTIME);
            }
            if ((ENDTIMEIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "ENDTIME", this.ENDTIME);
            }
            Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "REMARK", this.REMARK);
            if ((IDGROUPIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "IDGROUP", this.IDGROUP);
            }
        }

        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            this.IDORDERIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "IDORDER");
            if ((IDORDERIsNull == false))
            {
                this.IDORDER = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "IDORDER")));
            }
            this.STATUSIDIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "STATUSID");
            if ((STATUSIDIsNull == false))
            {
                this.STATUSID = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "STATUSID")));
            }
            this.SEQUENCEIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SEQUENCE");
            if ((SEQUENCEIsNull == false))
            {
                this.SEQUENCE = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SEQUENCE")));
            }
            this.STARTTIMEIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "STARTTIME");
            if ((STARTTIMEIsNull == false))
            {
                this.STARTTIME = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "STARTTIME")));
            }
            this.ENDTIMEIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "ENDTIME");
            if ((ENDTIMEIsNull == false))
            {
                this.ENDTIME = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "ENDTIME")));
            }
            this.REMARK = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "REMARK")));
            this.IDGROUPIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "IDGROUP");
            if ((IDGROUPIsNull == false))
            {
                this.IDGROUP = ((decimal)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "IDGROUP")));
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

        public static OBJ_LOGTASKMSG Parse(string str)
        {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new OBJ_LOGTASKMSG();
        }
    }

    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.OBJ_LOGTASKMSG")]
    public class OBJ_LOGTASKMSGFactory : IOracleCustomTypeFactory
    {

        public virtual IOracleCustomType CreateObject()
        {
            OBJ_LOGTASKMSG obj = new OBJ_LOGTASKMSG();
            return obj;
        }
    }
}
