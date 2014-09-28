using KDSCommon.DataModels;
using KDSCommon.Enums;
using System;

namespace KDSCommon.Interfaces.Shinuyim
{
    public interface IShinuyLogCollection
    {
       // void AddShinuy(IShinuy shinuy);
        void AddShinuy(IShinuy shinuy,SidurDM sidur);
        void AddShinuy(IShinuy shinuy, SidurDM sidur, PeilutDM peilut);
    }
}
