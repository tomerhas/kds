using System;
using KDSCommon.DataModels;
using System.Data;

namespace KDSCommon.Interfaces
{
    public interface IParametersManager
    {
        clParametersDM CreateClsParametrs(DateTime dCardDate, int iSugYom);
        clParametersDM CreateClsParametrs(DateTime dCardDate, int iSugYom, string type);
        clParametersDM CreateClsParametrs(DateTime dCardDate, int iSugYom, string type, DataTable dtParams);
    }
}
