using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Shinuyim;
using KdsShinuyim.Enums;

namespace KDSCommon.Enums
{
    public interface IShinuy
    {
        ShinuyTypes ShinuyType { get; }
        void ExecShinuy(ShinuyInputData inputData);

    }
}
