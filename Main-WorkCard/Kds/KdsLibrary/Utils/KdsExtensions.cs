using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Xml.Xsl;
using System.Xml;

namespace KdsLibrary.Utils
{
    public static class KdsExtensions
    {
        public static bool IsFileNotFoundException(this Exception exc)
        {
            bool result = false;
            HttpException httpExc = exc as HttpException;
            if (httpExc != null)
                result = httpExc.GetHttpCode() == 404;
            return result;
        }
        
        public static object DeserializeObject(System.Type type, string xmlSerialized)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            TextReader reader = new StringReader(xmlSerialized);
            return serializer.Deserialize(reader);
        }

        public static string SerializeObject(object objToSerialzie)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);
            XmlSerializer serializer = new XmlSerializer(objToSerialzie.GetType());
            serializer.Serialize(writer, objToSerialzie);
            return sb.ToString();
        }

        public static string TransformXml(string Xml, string StyleSheet)
        {
            return TransformXml(Xml, StyleSheet, null);
        }
        public static string TransformXml(string Xml, string StyleSheet,
                    XsltArgumentList xsltArgs)
        {
            XsltSettings xsltSet = new XsltSettings(true, true);
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            xslTransform.Load(StyleSheet, xsltSet,
                new XmlUrlResolver());
            StringReader reader = new StringReader(Xml);
            XmlReader xReader = new XmlTextReader(reader);
            StringBuilder sbOut = new StringBuilder();
            StringWriter writer = new StringWriter(sbOut);
            XmlWriter xWriter = new XmlTextWriter(writer);
            xslTransform.Transform(xReader, xsltArgs, xWriter);
            writer.Close();
            return sbOut.ToString();
        }
        public static object GetApplicationStoredValue(this HttpContext context,
            string key)
        {
            if (context.IsDebuggingEnabled)
                return context.Session[key];
            else return context.Application[key];
        }

        public static void AddApplicationStoredValue(this HttpContext context, 
            string key, object value)
        {
            if (context.IsDebuggingEnabled)
                context.Session.Add(key, value);
            else context.Application.Add(key, value);
        }

        public static void RemoveApplicationStoredValue(this HttpContext context,
            string key)
        {
            if (context.IsDebuggingEnabled)
                context.Session.Remove(key);
            else context.Application.Remove(key);
        }

        public static DataTable SelectDistinct(this DataTable sourceTable,
            params string[] fields)
        {
            DataTable dt = new DataTable();
            StringBuilder sbSort = new StringBuilder();
            foreach (string field in fields)
            {
                dt.Columns.Add(field, sourceTable.Columns[field].DataType);
                sbSort.AppendFormat("{0},", field);
            }
            if (sbSort.Length > 0)
                sbSort.Remove(sbSort.Length - 1, 1);
            object[] lastValue = new object[fields.Length];
            foreach (DataRow dr in sourceTable.Select("", sbSort.ToString()))
            {
                if (lastValue[0] == null)
                {
                    CopyRow(dt, dr, lastValue, fields);
                }
                else
                {
                    bool rowsEqual = true;
                    for (int i = 0; i < fields.Length; ++i)
                    {
                        if (!ColumnEqual(lastValue[i], dr[fields[i]]))
                        {
                            rowsEqual = false;
                            break;
                        }
                    }
                    if (!rowsEqual)
                    {
                        CopyRow(dt, dr, lastValue, fields);
                    }
                }
            }
            
            return dt;

        }

        private static void CopyRow(DataTable targetTable, DataRow sourceRow, 
            object[] lastValue, string[] fields)
        {
            var newRow = targetTable.NewRow();
            for (int i = 0; i < fields.Length; ++i)
            {
                lastValue[i] = sourceRow[fields[i]];
                newRow[fields[i]] = sourceRow[fields[i]];
            }
            targetTable.Rows.Add(newRow);
        }
        
        private static bool ColumnEqual(object A, object B)
        {
            // Compares two values to see if they are equal. Also compares DBNULL.Value.
            // Note: If your DataTable contains object fields, then you must extend this
            // function to handle them in a meaningful way if you intend to group on them.

            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is DBNull.Value
                return false;
            return (A.Equals(B));  // value type standard comparison

        }
    }
}
