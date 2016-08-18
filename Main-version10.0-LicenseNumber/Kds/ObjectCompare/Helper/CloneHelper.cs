using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ObjectCompare.Helper
{
    public class CloneHelper
    {
        public static T Clone<T>(T itemToClone) where T:class
        {
            MemoryStream ms = new MemoryStream();
            //NetDataContractSerializer bf = new NetDataContractSerializer();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, itemToClone);
            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj as T;
        }

    }
}
