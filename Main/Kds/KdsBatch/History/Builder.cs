using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using KdsLibrary;
//using KdsLibrary.DAL;
using System.IO;
using System.Configuration;
//using KdsLibrary.UDT;
namespace KdsBatch.History
{
    public class Builder : IDisposable      
    {
        private string _fileName;
        private char _delimeter;
        public List<string[]> Items;

        public Builder() { }

        public Builder(string filename, char del)
        {
            _fileName = filename;
            _delimeter = del;
            Items = new List<string[]>();
        }

        public void Build()
        {

            try
            {
                using (StreamReader reader = new StreamReader(_fileName, Encoding.Default))
                {
                    string textRow;
                    string[] textRowArr;
                    while (reader.Peek() > 0)
                    {
                        textRow = reader.ReadLine();
                        textRowArr = textRow.Split(_delimeter);
                        Items.Add(textRowArr);
                    }
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Builder Error: " + ex.Message + "\n" + ex.StackTrace);
            }
        }


        public void Dispose()
        {
            Items = null;
            GC.Collect();
        }
    }
}
