using System;
using KDSCommon.DataModels;
using System.Data;
using System.Collections.Specialized;
using KDSCommon.Enums;
namespace KDSCommon.Interfaces.Managers
{

     public interface INezerMergeManager
    {
         void Merge(DataTable dtNezer, OrderedDictionary odSidurim);
    }
}
