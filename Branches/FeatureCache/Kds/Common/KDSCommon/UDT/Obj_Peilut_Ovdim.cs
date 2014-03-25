﻿using System;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Xml.Serialization;
using System.Xml.Schema;
using ObjectCompare.Attributes;

namespace KDSCommon.UDT
{
        [Serializable]
    public class OBJ_PEILUT_OVDIM : INullable, IOracleCustomType, IXmlSerializable, IComparable
    {

        private bool m_IsNull;

        private long m_OTO_NO;

        private bool m_OTO_NOIsNull;

        private int m_MISPAR_SIDUR_NETZER;

        private bool m_MISPAR_SIDUR_NETZERIsNull;

        private System.DateTime m_TAARICH;

        private bool m_TAARICHIsNull;

        private int m_KOD_SHINUY_PREMIA;

        private bool m_KOD_SHINUY_PREMIAIsNull;

        private long m_OTO_NO_NETZER;

        private bool m_OTO_NO_NETZERIsNull;

        private int m_MISPAR_SIDUR;

        private bool m_MISPAR_SIDURIsNull;

        private int m_MISPAR_KNISA;

        private bool m_MISPAR_KNISAIsNull;

        private int m_SUG_KNISA;

        private bool m_SUG_KNISAIsNull;

        private long m_MAKAT_NETZER;

        private bool m_MAKAT_NETZERIsNull;

        private long m_MISPAR_VISA;

        private bool m_MISPAR_VISAIsNull;

        private long m_MISPAR_MATALA;

        private bool m_MISPAR_MATALAIsNull;

        private int m_MEADKEN_ACHARON;

        private bool m_MEADKEN_ACHARONIsNull;

        private int m_ISHUR_KFULA;

        private bool m_ISHUR_KFULAIsNull;

        private long m_MAKAT_NESIA;

        private bool m_MAKAT_NESIAIsNull;

        private long m_MISPAR_SIDURI_OTO;

        private bool m_MISPAR_SIDURI_OTOIsNull;

        private int m_BITUL_O_HOSAFA;

        private bool m_BITUL_O_HOSAFAIsNull;

        private System.DateTime m_SHAT_BHIRAT_NESIA_NETZER;

        private bool m_SHAT_BHIRAT_NESIA_NETZERIsNull;

        private System.DateTime m_TAARICH_IDKUN_ACHARON;

        private bool m_TAARICH_IDKUN_ACHARONIsNull;

        private string m_HEARA;

        private string m_SHILUT_NETZER;

        private System.DateTime m_SHAT_YETZIA;

        private bool m_SHAT_YETZIAIsNull;

        private System.DateTime m_NEW_SHAT_YETZIA;

        private bool m_NEW_SHAT_YETZIAIsNull;

        private System.DateTime m_SHAT_YETZIA_NETZER;

        private bool m_SHAT_YETZIA_NETZERIsNull;

        private System.DateTime m_SHAT_HATCHALA_SIDUR;

        private bool m_SHAT_HATCHALA_SIDURIsNull;

        private System.DateTime m_NEW_SHAT_HATCHALA_SIDUR;

        private bool m_NEW_SHAT_HATCHALA_SIDURIsNull;

        private int m_IMUT_NETZER;

        private bool m_IMUT_NETZERIsNull;

        private int m_MISPAR_ISHI;

        private bool m_MISPAR_ISHIIsNull;

        private string m_MIKUM_BHIRAT_NESIA_NETZER;

        private bool m_MIKUM_BHIRAT_NESIA_NETZERIsNull;

        private int m_KISUY_TOR;

        private bool m_KISUY_TORIsNull;

        private int m_NEW_MISPAR_SIDUR;

        private bool m_NEW_MISPAR_SIDURIsNull;

        private int m_UPDATE_OBJECT;

        private bool m_UPDATE_OBJECTIsNull;

        private int m_DAKOT_BAFOAL;

        private bool m_DAKOT_BAFOALIsNull;

        private int m_SNIF_TNUA;

        private bool m_SNIF_TNUAIsNull;

        private int m_KM_VISA;

        private bool m_KM_VISAIsNull;

        private string m_TEUR_NESIA;

        public OBJ_PEILUT_OVDIM()
        {
            // TODO : Add code to initialise the object
            this.m_OTO_NOIsNull = true;
            this.m_MISPAR_SIDUR_NETZERIsNull = true;
            this.m_TAARICHIsNull = true;
            this.m_KOD_SHINUY_PREMIAIsNull = true;
            this.m_OTO_NO_NETZERIsNull = true;
            this.m_MISPAR_SIDURIsNull = true;
            this.m_MISPAR_KNISAIsNull = true;
            this.m_SUG_KNISAIsNull = true;
            this.m_MAKAT_NETZERIsNull = true;
            this.m_MISPAR_VISAIsNull = true;
            this.m_MISPAR_MATALAIsNull = true;
            this.m_MEADKEN_ACHARONIsNull = true;
            this.m_ISHUR_KFULAIsNull = true;
            this.m_MAKAT_NESIAIsNull = true;
            this.m_MISPAR_SIDURI_OTOIsNull = true;
            this.m_BITUL_O_HOSAFAIsNull = true;
            this.m_SHAT_BHIRAT_NESIA_NETZERIsNull = true;
            this.m_TAARICH_IDKUN_ACHARONIsNull = true;
            this.m_SHAT_YETZIAIsNull = true;
            this.m_NEW_SHAT_YETZIAIsNull = true;
            this.m_SHAT_YETZIA_NETZERIsNull = true;
            this.m_SHAT_HATCHALA_SIDURIsNull = true;
            this.m_NEW_SHAT_HATCHALA_SIDURIsNull = true;
            this.m_IMUT_NETZERIsNull = true;
            this.m_MISPAR_ISHIIsNull = true;
            this.m_MIKUM_BHIRAT_NESIA_NETZERIsNull = true;
            this.m_KISUY_TORIsNull = true;
            this.m_UPDATE_OBJECTIsNull = true;
            this.m_DAKOT_BAFOALIsNull = true;
            this.m_NEW_MISPAR_SIDURIsNull = true;
            this.m_SNIF_TNUAIsNull = true;
            this.m_KM_VISAIsNull = true;

            //m_IsNull = false;
        }

        public OBJ_PEILUT_OVDIM(string str)
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

        public static OBJ_PEILUT_OVDIM Null
        {
            get
            {
                OBJ_PEILUT_OVDIM obj = new OBJ_PEILUT_OVDIM();
                obj.m_IsNull = true;
                return obj;
            }
        }
        public int CompareTo(object obj)
        {

            if (obj is OBJ_PEILUT_OVDIM)
            {
                //return this.CompareTo((obj as OBJ_PEILUT_OVDIM).NEW_SHAT_YETZIA);  // compare by shat yetiza
                return NEW_SHAT_YETZIA.CompareTo((obj as OBJ_PEILUT_OVDIM).NEW_SHAT_YETZIA);
            }
            else
            {
                throw new ArgumentException("Object is not a OBJ_PEILUT_OVDIM");
            }

        }
        [OracleObjectMappingAttribute("OTO_NO")]
        public long OTO_NO
        {
            get
            {
                return this.m_OTO_NO;
            }
            set
            {
                this.m_OTO_NO = value;
                this.m_OTO_NOIsNull = false;
            }
        }

        public bool OTO_NOIsNull
        {
            get
            {
                return this.m_OTO_NOIsNull;
            }
            set
            {
                this.m_OTO_NOIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MISPAR_SIDUR_NETZER")]
        public int MISPAR_SIDUR_NETZER
        {
            get
            {
                return this.m_MISPAR_SIDUR_NETZER;
            }
            set
            {
                this.m_MISPAR_SIDUR_NETZER = value;
                this.m_MISPAR_SIDUR_NETZERIsNull = false;
            }
        }

        public bool MISPAR_SIDUR_NETZERIsNull
        {
            get
            {
                return this.m_MISPAR_SIDUR_NETZERIsNull;
            }
            set
            {
                this.m_MISPAR_SIDUR_NETZERIsNull = value;
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

        [OracleObjectMappingAttribute("KOD_SHINUY_PREMIA")]
        public int KOD_SHINUY_PREMIA
        {
            get
            {
                return this.m_KOD_SHINUY_PREMIA;
            }
            set
            {
                this.m_KOD_SHINUY_PREMIA = value;
                this.m_KOD_SHINUY_PREMIAIsNull = false;
            }
        }

        public bool KOD_SHINUY_PREMIAIsNull
        {
            get
            {
                return this.m_KOD_SHINUY_PREMIAIsNull;
            }
            set
            {
                this.m_KOD_SHINUY_PREMIAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("OTO_NO_NETZER")]
        public long OTO_NO_NETZER
        {
            get
            {
                return this.m_OTO_NO_NETZER;
            }
            set
            {
                this.m_OTO_NO_NETZER = value;
                this.m_OTO_NO_NETZERIsNull = false;
            }
        }

        public bool OTO_NO_NETZERIsNull
        {
            get
            {
                return this.m_OTO_NO_NETZERIsNull;
            }
            set
            {
                this.m_OTO_NO_NETZERIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MISPAR_SIDUR")]
        public int MISPAR_SIDUR
        {
            get
            {
                return this.m_MISPAR_SIDUR;
            }
            set
            {
                this.m_MISPAR_SIDUR = value;
                this.m_MISPAR_SIDURIsNull = false;
            }
        }

        public bool MISPAR_SIDURIsNull
        {
            get
            {
                return this.m_MISPAR_SIDURIsNull;
            }
            set
            {
                this.m_MISPAR_SIDURIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MISPAR_KNISA")]
        public int MISPAR_KNISA
        {
            get
            {
                return this.m_MISPAR_KNISA;
            }
            set
            {
                this.m_MISPAR_KNISA = value;
                this.m_MISPAR_KNISAIsNull = false;
            }
        }

        public bool MISPAR_KNISAIsNull
        {
            get
            {
                return this.m_MISPAR_KNISAIsNull;
            }
            set
            {
                this.m_MISPAR_KNISAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("SUG_KNISA")]
        public int SUG_KNISA
        {
            get
            {
                return this.m_SUG_KNISA;
            }
            set
            {
                this.m_SUG_KNISA = value;
                this.m_SUG_KNISAIsNull = false;
            }
        }

        public bool SUG_KNISAIsNull
        {
            get
            {
                return this.m_SUG_KNISAIsNull;
            }
            set
            {
                this.m_SUG_KNISAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MAKAT_NETZER")]
        public long MAKAT_NETZER
        {
            get
            {
                return this.m_MAKAT_NETZER;
            }
            set
            {
                this.m_MAKAT_NETZER = value;
                this.m_MAKAT_NETZERIsNull = false;
            }
        }

        public bool MAKAT_NETZERIsNull
        {
            get
            {
                return this.m_MAKAT_NETZERIsNull;
            }
            set
            {
                this.m_MAKAT_NETZERIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MISPAR_VISA")]
        public long MISPAR_VISA
        {
            get
            {
                return this.m_MISPAR_VISA;
            }
            set
            {
                this.m_MISPAR_VISA = value;
                this.m_MISPAR_VISAIsNull = false;
            }
        }

        public bool MISPAR_VISAIsNull
        {
            get
            {
                return this.m_MISPAR_VISAIsNull;
            }
            set
            {
                this.m_MISPAR_VISAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MISPAR_MATALA")]
        public long MISPAR_MATALA
        {
            get
            {
                return this.m_MISPAR_MATALA;
            }
            set
            {
                this.m_MISPAR_MATALA = value;
                this.m_MISPAR_MATALAIsNull = false;
            }
        }

        public bool MISPAR_MATALAIsNull
        {
            get
            {
                return this.m_MISPAR_MATALAIsNull;
            }
            set
            {
                this.m_MISPAR_MATALAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MEADKEN_ACHARON")]
        public int MEADKEN_ACHARON
        {
            get
            {
                return this.m_MEADKEN_ACHARON;
            }
            set
            {
                this.m_MEADKEN_ACHARON = value;
                this.m_MEADKEN_ACHARONIsNull = false;
            }
        }

        public bool MEADKEN_ACHARONIsNull
        {
            get
            {
                return this.m_MEADKEN_ACHARONIsNull;
            }
            set
            {
                this.m_MEADKEN_ACHARONIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("ISHUR_KFULA")]
        public int ISHUR_KFULA
        {
            get
            {
                return this.m_ISHUR_KFULA;
            }
            set
            {
                this.m_ISHUR_KFULA = value;
                this.m_ISHUR_KFULAIsNull = false;
            }
        }

        public bool ISHUR_KFULAIsNull
        {
            get
            {
                return this.m_ISHUR_KFULAIsNull;
            }
            set
            {
                this.m_ISHUR_KFULAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MAKAT_NESIA")]
        public long MAKAT_NESIA
        {
            get
            {
                return this.m_MAKAT_NESIA;
            }
            set
            {
                this.m_MAKAT_NESIA = value;
                this.m_MAKAT_NESIAIsNull = false;
            }
        }

        public bool MAKAT_NESIAIsNull
        {
            get
            {
                return this.m_MAKAT_NESIAIsNull;
            }
            set
            {
                this.m_MAKAT_NESIAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("MISPAR_SIDURI_OTO")]
        public long MISPAR_SIDURI_OTO
        {
            get
            {
                return this.m_MISPAR_SIDURI_OTO;
            }
            set
            {
                this.m_MISPAR_SIDURI_OTO = value;
                this.m_MISPAR_SIDURI_OTOIsNull = false;
            }
        }

        public bool MISPAR_SIDURI_OTOIsNull
        {
            get
            {
                return this.m_MISPAR_SIDURI_OTOIsNull;
            }
            set
            {
                this.m_MISPAR_SIDURI_OTOIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("BITUL_O_HOSAFA")]
        public int BITUL_O_HOSAFA
        {
            get
            {
                return this.m_BITUL_O_HOSAFA;
            }
            set
            {
                this.m_BITUL_O_HOSAFA = value;
                this.m_BITUL_O_HOSAFAIsNull = false;
            }
        }

        public bool BITUL_O_HOSAFAIsNull
        {
            get
            {
                return this.m_BITUL_O_HOSAFAIsNull;
            }
            set
            {
                this.m_BITUL_O_HOSAFAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("SHAT_BHIRAT_NESIA_NETZER")]
        public System.DateTime SHAT_BHIRAT_NESIA_NETZER
        {
            get
            {
                return this.m_SHAT_BHIRAT_NESIA_NETZER;
            }
            set
            {
                this.m_SHAT_BHIRAT_NESIA_NETZER = value;
                this.m_SHAT_BHIRAT_NESIA_NETZERIsNull = false;
            }
        }

        public bool SHAT_BHIRAT_NESIA_NETZERIsNull
        {
            get
            {
                return this.m_SHAT_BHIRAT_NESIA_NETZERIsNull;
            }
            set
            {
                this.m_SHAT_BHIRAT_NESIA_NETZERIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("TAARICH_IDKUN_ACHARON")]
        public System.DateTime TAARICH_IDKUN_ACHARON
        {
            get
            {
                return this.m_TAARICH_IDKUN_ACHARON;
            }
            set
            {
                this.m_TAARICH_IDKUN_ACHARON = value;
                this.m_TAARICH_IDKUN_ACHARONIsNull = false;
            }
        }

        public bool TAARICH_IDKUN_ACHARONIsNull
        {
            get
            {
                return this.m_TAARICH_IDKUN_ACHARONIsNull;
            }
            set
            {
                this.m_TAARICH_IDKUN_ACHARONIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("HEARA")]
        public string HEARA
        {
            get
            {
                return this.m_HEARA;
            }
            set
            {
                this.m_HEARA = value;
            }
        }

        [OracleObjectMappingAttribute("SHILUT_NETZER")]
        public string SHILUT_NETZER
        {
            get
            {
                return this.m_SHILUT_NETZER;
            }
            set
            {
                this.m_SHILUT_NETZER = value;
            }
        }

        [OracleObjectMappingAttribute("SHAT_YETZIA")]
        public System.DateTime SHAT_YETZIA
        {
            get
            {
                return this.m_SHAT_YETZIA;
            }
            set
            {
                this.m_SHAT_YETZIA = value;
                this.m_SHAT_YETZIAIsNull = false;
            }
        }

        public bool SHAT_YETZIAIsNull
        {
            get
            {
                return this.m_SHAT_YETZIAIsNull;
            }
            set
            {
                this.m_SHAT_YETZIAIsNull = value;
            }
        }


        [OracleObjectMappingAttribute("NEW_SHAT_YETZIA")]
        public System.DateTime NEW_SHAT_YETZIA
        {
            get
            {
                return this.m_NEW_SHAT_YETZIA;
            }
            set
            {
                this.m_NEW_SHAT_YETZIA = value;
                this.m_NEW_SHAT_YETZIAIsNull = false;
            }
        }

        public bool NEW_SHAT_YETZIAIsNull
        {
            get
            {
                return this.m_NEW_SHAT_YETZIAIsNull;
            }
            set
            {
                this.m_NEW_SHAT_YETZIAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("SHAT_YETZIA_NETZER")]
        public System.DateTime SHAT_YETZIA_NETZER
        {
            get
            {
                return this.m_SHAT_YETZIA_NETZER;
            }
            set
            {
                this.m_SHAT_YETZIA_NETZER = value;
                this.m_SHAT_YETZIA_NETZERIsNull = false;
            }
        }

        public bool SHAT_YETZIA_NETZERIsNull
        {
            get
            {
                return this.m_SHAT_YETZIA_NETZERIsNull;
            }
            set
            {
                this.m_SHAT_YETZIA_NETZERIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("SHAT_HATCHALA_SIDUR")]
        public System.DateTime SHAT_HATCHALA_SIDUR
        {
            get
            {
                return this.m_SHAT_HATCHALA_SIDUR;
            }
            set
            {
                this.m_SHAT_HATCHALA_SIDUR = value;
                this.m_SHAT_HATCHALA_SIDURIsNull = false;
            }
        }

        public bool SHAT_HATCHALA_SIDURIsNull
        {
            get
            {
                return this.m_SHAT_HATCHALA_SIDURIsNull;
            }
            set
            {
                this.m_SHAT_HATCHALA_SIDURIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("NEW_SHAT_HATCHALA_SIDUR")]
        public System.DateTime NEW_SHAT_HATCHALA_SIDUR
        {
            get
            {
                return this.m_NEW_SHAT_HATCHALA_SIDUR;
            }
            set
            {
                this.m_NEW_SHAT_HATCHALA_SIDUR = value;
                this.m_NEW_SHAT_HATCHALA_SIDURIsNull = false;
            }
        }

        public bool NEW_SHAT_HATCHALA_SIDURIsNull
        {
            get
            {
                return this.m_NEW_SHAT_HATCHALA_SIDURIsNull;
            }
            set
            {
                this.m_NEW_SHAT_HATCHALA_SIDURIsNull = value;
            }
        }
        [OracleObjectMappingAttribute("IMUT_NETZER")]
        public int IMUT_NETZER
        {
            get
            {
                return this.m_IMUT_NETZER;
            }
            set
            {
                this.m_IMUT_NETZER = value;
                this.m_IMUT_NETZERIsNull = false;
            }
        }

        public bool IMUT_NETZERIsNull
        {
            get
            {
                return this.m_IMUT_NETZERIsNull;
            }
            set
            {
                this.m_IMUT_NETZERIsNull = value;
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

        [OracleObjectMappingAttribute("MIKUM_BHIRAT_NESIA_NETZER")]
        public string MIKUM_BHIRAT_NESIA_NETZER
        {
            get
            {
                return this.m_MIKUM_BHIRAT_NESIA_NETZER;
            }
            set
            {
                this.m_MIKUM_BHIRAT_NESIA_NETZER = value;
                this.m_MIKUM_BHIRAT_NESIA_NETZERIsNull = false;
            }
        }

        public bool MIKUM_BHIRAT_NESIA_NETZERIsNull
        {
            get
            {
                return this.m_MIKUM_BHIRAT_NESIA_NETZERIsNull;
            }
            set
            {
                this.m_MIKUM_BHIRAT_NESIA_NETZERIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("KISUY_TOR")]
        public int KISUY_TOR
        {
            get
            {
                return this.m_KISUY_TOR;
            }
            set
            {
                this.m_KISUY_TOR = value;
                this.m_KISUY_TORIsNull = false;
            }
        }

        public bool KISUY_TORIsNull
        {
            get
            {
                return this.m_KISUY_TORIsNull;
            }
            set
            {
                this.m_KISUY_TORIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("KM_VISA")]
        public int KM_VISA
        {
            get
            {
                return this.m_KM_VISA;
            }
            set
            {
                this.m_KM_VISA = value;
                this.m_KM_VISAIsNull = false;
            }
        }

        public bool KM_VISAIsNull
        {
            get
            {
                return this.m_KM_VISAIsNull;
            }
            set
            {
                this.m_KM_VISAIsNull = value;
            }
        }

        [OracleObjectMappingAttribute("TEUR_NESIA")]
        public string TEUR_NESIA
        {
            get
            {
                return this.m_TEUR_NESIA;
            }
            set
            {
                this.m_TEUR_NESIA = value;
            }
        }

        [OracleObjectMappingAttribute("SNIF_TNUA")]
        public int SNIF_TNUA
        {
            get
            {
                return this.m_SNIF_TNUA;
            }
            set
            {
                this.m_SNIF_TNUA = value;
                this.m_SNIF_TNUAIsNull = false;
            }
        }

        public bool SNIF_TNUAIsNull
        {
            get
            {
                return this.m_SNIF_TNUAIsNull;
            }
            set
            {
                this.m_SNIF_TNUAIsNull = value;
            }
        }

        [DontCompare]
        [OracleObjectMappingAttribute("UPDATE_OBJECT")]
        public int UPDATE_OBJECT
        {
            get
            {
                return this.m_UPDATE_OBJECT;
            }
            set
            {
                this.m_UPDATE_OBJECT = value;
                this.m_UPDATE_OBJECTIsNull = false;
            }
        }
        [DontCompare]
        public bool UPDATE_OBJECTIsNull
        {
            get
            {
                return this.m_UPDATE_OBJECTIsNull;
            }
            set
            {
                this.m_UPDATE_OBJECTIsNull = value;
            }
        }
        [OracleObjectMappingAttribute("DAKOT_BAFOAL")]
        public int DAKOT_BAFOAL
        {
            get
            {
                return this.m_DAKOT_BAFOAL;
            }
            set
            {
                this.m_DAKOT_BAFOAL = value;
                this.m_DAKOT_BAFOALIsNull = false;
            }
        }

        public bool DAKOT_BAFOALIsNull
        {
            get
            {
                return this.m_DAKOT_BAFOALIsNull;
            }
            set
            {
                this.m_DAKOT_BAFOALIsNull = value;
            }
        }
        [OracleObjectMappingAttribute("NEW_MISPAR_SIDUR")]
        public int NEW_MISPAR_SIDUR
        {
            get
            {
                return this.m_NEW_MISPAR_SIDUR;
            }
            set
            {
                this.m_NEW_MISPAR_SIDUR = value;
                this.m_NEW_MISPAR_SIDURIsNull = false;
            }
        }

        public bool NEW_MISPAR_SIDURIsNull
        {
            get
            {
                return this.m_NEW_MISPAR_SIDURIsNull;
            }
            set
            {
                this.m_NEW_MISPAR_SIDURIsNull = value;
            }
        }
        public virtual void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            if ((OTO_NOIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "OTO_NO", this.OTO_NO);
            }
            if ((MISPAR_SIDUR_NETZERIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_SIDUR_NETZER", this.MISPAR_SIDUR_NETZER);
            }
            if ((TAARICHIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "TAARICH", this.TAARICH);
            }
            if ((KOD_SHINUY_PREMIAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "KOD_SHINUY_PREMIA", this.KOD_SHINUY_PREMIA);
            }
            if ((OTO_NO_NETZERIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "OTO_NO_NETZER", this.OTO_NO_NETZER);
            }
            if ((MISPAR_SIDURIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_SIDUR", this.MISPAR_SIDUR);
            }
            if ((MISPAR_KNISAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_KNISA", this.MISPAR_KNISA);
            }
            if ((SUG_KNISAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SUG_KNISA", this.SUG_KNISA);
            }
            if ((MAKAT_NETZERIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MAKAT_NETZER", this.MAKAT_NETZER);
            }
            if ((MISPAR_VISAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_VISA", this.MISPAR_VISA);
            }
            if ((MISPAR_MATALAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_MATALA", this.MISPAR_MATALA);
            }
            if ((MEADKEN_ACHARONIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MEADKEN_ACHARON", this.MEADKEN_ACHARON);
            }
            if ((ISHUR_KFULAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "ISHUR_KFULA", this.ISHUR_KFULA);
            }
            if ((MAKAT_NESIAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MAKAT_NESIA", this.MAKAT_NESIA);
            }
            if ((MISPAR_SIDURI_OTOIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_SIDURI_OTO", this.MISPAR_SIDURI_OTO);
            }
            if ((BITUL_O_HOSAFAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "BITUL_O_HOSAFA", this.BITUL_O_HOSAFA);
            }
            if ((SHAT_BHIRAT_NESIA_NETZERIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SHAT_BHIRAT_NESIA_NETZER", this.SHAT_BHIRAT_NESIA_NETZER);
            }
            if ((TAARICH_IDKUN_ACHARONIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "TAARICH_IDKUN_ACHARON", this.TAARICH_IDKUN_ACHARON);
            }
            Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "HEARA", this.HEARA);
            Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SHILUT_NETZER", this.SHILUT_NETZER);
            if ((SHAT_YETZIAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SHAT_YETZIA", this.SHAT_YETZIA);
            }
            if ((NEW_SHAT_YETZIAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "NEW_SHAT_YETZIA", this.NEW_SHAT_YETZIA);
            }
            if ((SHAT_YETZIA_NETZERIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SHAT_YETZIA_NETZER", this.SHAT_YETZIA_NETZER);
            }
            if ((SHAT_HATCHALA_SIDURIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SHAT_HATCHALA_SIDUR", this.SHAT_HATCHALA_SIDUR);
            }
            if ((NEW_SHAT_HATCHALA_SIDURIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "NEW_SHAT_HATCHALA_SIDUR", this.NEW_SHAT_HATCHALA_SIDUR);
            }
            if ((IMUT_NETZERIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "IMUT_NETZER", this.IMUT_NETZER);
            }
            if ((MISPAR_ISHIIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MISPAR_ISHI", this.MISPAR_ISHI);
            }

            Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "MIKUM_BHIRAT_NESIA_NETZER", this.MIKUM_BHIRAT_NESIA_NETZER);

            if ((KISUY_TORIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "KISUY_TOR", this.KISUY_TOR);
            }
            if ((SNIF_TNUAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "SNIF_TNUA", this.SNIF_TNUA);
            }
            if ((KM_VISAIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "KM_VISA", this.KM_VISA);
            }

            Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "TEUR_NESIA", this.TEUR_NESIA);

            if ((UPDATE_OBJECTIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "UPDATE_OBJECT", this.UPDATE_OBJECT);
            }
            if ((DAKOT_BAFOALIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "DAKOT_BAFOAL", this.DAKOT_BAFOAL);
            }
            if ((NEW_MISPAR_SIDURIsNull == false))
            {
                Oracle.DataAccess.Types.OracleUdt.SetValue(con, pUdt, "NEW_MISPAR_SIDUR", this.NEW_MISPAR_SIDUR);
            }
        }

        public virtual void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, System.IntPtr pUdt)
        {
            this.OTO_NOIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "OTO_NO");
            if ((OTO_NOIsNull == false))
            {
                this.OTO_NO = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "OTO_NO")));
            }
            this.MISPAR_SIDUR_NETZERIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_SIDUR_NETZER");
            if ((MISPAR_SIDUR_NETZERIsNull == false))
            {
                this.MISPAR_SIDUR_NETZER = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_SIDUR_NETZER")));
            }
            this.TAARICHIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "TAARICH");
            if ((TAARICHIsNull == false))
            {
                this.TAARICH = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "TAARICH")));
            }
            this.KOD_SHINUY_PREMIAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "KOD_SHINUY_PREMIA");
            if ((KOD_SHINUY_PREMIAIsNull == false))
            {
                this.KOD_SHINUY_PREMIA = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "KOD_SHINUY_PREMIA")));
            }
            this.OTO_NO_NETZERIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "OTO_NO_NETZER");
            if ((OTO_NO_NETZERIsNull == false))
            {
                this.OTO_NO_NETZER = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "OTO_NO_NETZER")));
            }
            this.MISPAR_SIDURIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_SIDUR");
            if ((MISPAR_SIDURIsNull == false))
            {
                this.MISPAR_SIDUR = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_SIDUR")));
            }
            this.MISPAR_KNISAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_KNISA");
            if ((MISPAR_KNISAIsNull == false))
            {
                this.MISPAR_KNISA = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_KNISA")));
            }
            this.SUG_KNISAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SUG_KNISA");
            if ((SUG_KNISAIsNull == false))
            {
                this.SUG_KNISA = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SUG_KNISA")));
            }
            this.MAKAT_NETZERIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MAKAT_NETZER");
            if ((MAKAT_NETZERIsNull == false))
            {
                this.MAKAT_NETZER = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MAKAT_NETZER")));
            }
            this.MISPAR_VISAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_VISA");
            if ((MISPAR_VISAIsNull == false))
            {
                this.MISPAR_VISA = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_VISA")));
            }
            this.MISPAR_MATALAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_MATALA");
            if ((MISPAR_MATALAIsNull == false))
            {
                this.MISPAR_MATALA = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_MATALA")));
            }
            this.MEADKEN_ACHARONIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MEADKEN_ACHARON");
            if ((MEADKEN_ACHARONIsNull == false))
            {
                this.MEADKEN_ACHARON = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MEADKEN_ACHARON")));
            }
            this.ISHUR_KFULAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "ISHUR_KFULA");
            if ((ISHUR_KFULAIsNull == false))
            {
                this.ISHUR_KFULA = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "ISHUR_KFULA")));
            }
            this.MAKAT_NESIAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MAKAT_NESIA");
            if ((MAKAT_NESIAIsNull == false))
            {
                this.MAKAT_NESIA = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MAKAT_NESIA")));
            }
            this.MISPAR_SIDURI_OTOIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_SIDURI_OTO");
            if ((MISPAR_SIDURI_OTOIsNull == false))
            {
                this.MISPAR_SIDURI_OTO = ((long)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_SIDURI_OTO")));
            }
            this.BITUL_O_HOSAFAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "BITUL_O_HOSAFA");
            if ((BITUL_O_HOSAFAIsNull == false))
            {
                this.BITUL_O_HOSAFA = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "BITUL_O_HOSAFA")));
            }
            this.SHAT_BHIRAT_NESIA_NETZERIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SHAT_BHIRAT_NESIA_NETZER");
            if ((SHAT_BHIRAT_NESIA_NETZERIsNull == false))
            {
                this.SHAT_BHIRAT_NESIA_NETZER = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SHAT_BHIRAT_NESIA_NETZER")));
            }
            this.TAARICH_IDKUN_ACHARONIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "TAARICH_IDKUN_ACHARON");
            if ((TAARICH_IDKUN_ACHARONIsNull == false))
            {
                this.TAARICH_IDKUN_ACHARON = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "TAARICH_IDKUN_ACHARON")));
            }
            this.HEARA = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "HEARA")));
            this.SHILUT_NETZER = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SHILUT_NETZER")));
            this.SHAT_YETZIAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SHAT_YETZIA");
            if ((SHAT_YETZIAIsNull == false))
            {
                this.SHAT_YETZIA = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SHAT_YETZIA")));
            }

            this.NEW_SHAT_YETZIAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "NEW_SHAT_YETZIA");
            if ((NEW_SHAT_YETZIAIsNull == false))
            {
                this.NEW_SHAT_YETZIA = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "NEW_SHAT_YETZIA")));
            }

            this.SHAT_YETZIA_NETZERIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SHAT_YETZIA_NETZER");
            if ((SHAT_YETZIA_NETZERIsNull == false))
            {
                this.SHAT_YETZIA_NETZER = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SHAT_YETZIA_NETZER")));
            }
            this.SHAT_HATCHALA_SIDURIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SHAT_HATCHALA_SIDUR");
            if ((SHAT_HATCHALA_SIDURIsNull == false))
            {
                this.SHAT_HATCHALA_SIDUR = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SHAT_HATCHALA_SIDUR")));
            }
            this.NEW_SHAT_HATCHALA_SIDURIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "NEW_SHAT_HATCHALA_SIDUR");
            if ((NEW_SHAT_HATCHALA_SIDURIsNull == false))
            {
                this.NEW_SHAT_HATCHALA_SIDUR = ((System.DateTime)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "NEW_SHAT_HATCHALA_SIDUR")));
            }
            this.IMUT_NETZERIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "IMUT_NETZER");
            if ((IMUT_NETZERIsNull == false))
            {
                this.IMUT_NETZER = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "IMUT_NETZER")));
            }
            this.MISPAR_ISHIIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "MISPAR_ISHI");
            if ((MISPAR_ISHIIsNull == false))
            {
                this.MISPAR_ISHI = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MISPAR_ISHI")));
            }

            this.MIKUM_BHIRAT_NESIA_NETZER = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "MIKUM_BHIRAT_NESIA_NETZER")));

            this.KISUY_TORIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "KISUY_TOR");
            if ((KISUY_TORIsNull == false))
            {
                this.KISUY_TOR = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "KISUY_TOR")));
            }
            this.KM_VISAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "KM_VISA");
            if ((KM_VISAIsNull == false))
            {
                this.KM_VISA = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "KM_VISA")));
            }

            this.TEUR_NESIA = ((string)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "TEUR_NESIA")));

            this.SNIF_TNUAIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "SNIF_TNUA");
            if ((SNIF_TNUAIsNull == false))
            {
                this.SNIF_TNUA = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "SNIF_TNUA")));
            }
            this.UPDATE_OBJECTIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "UPDATE_OBJECT");
            if ((UPDATE_OBJECTIsNull == false))
            {
                this.UPDATE_OBJECT = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "UPDATE_OBJECT")));
            }
            this.DAKOT_BAFOALIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "DAKOT_BAFOAL");
            if ((DAKOT_BAFOALIsNull == false))
            {
                this.DAKOT_BAFOAL = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "DAKOT_BAFOAL")));
            }

            this.NEW_MISPAR_SIDURIsNull = Oracle.DataAccess.Types.OracleUdt.IsDBNull(con, pUdt, "NEW_MISPAR_SIDUR");
            if ((NEW_MISPAR_SIDURIsNull == false))
            {
                this.NEW_MISPAR_SIDUR = ((int)(Oracle.DataAccess.Types.OracleUdt.GetValue(con, pUdt, "NEW_MISPAR_SIDUR")));
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

        public static OBJ_PEILUT_OVDIM Parse(string str)
        {
            // TODO : Add code needed to parse the string and get the object represented by the string
            return new OBJ_PEILUT_OVDIM();
        }
    }

    // Factory to create an object for the above class
    [OracleCustomTypeMappingAttribute("KDSADMIN.OBJ_PEILUT_OVDIM")]
    public class OBJ_PEILUT_OVDIMFactory : IOracleCustomTypeFactory
    {

        public virtual IOracleCustomType CreateObject()
        {
            OBJ_PEILUT_OVDIM obj = new OBJ_PEILUT_OVDIM();
            return obj;
        }
    }
}