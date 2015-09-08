using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.UDT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCompare;

namespace ObjectComparerTester
{

    [TestClass]
    public class ObjectCompareTest
    {
        [TestMethod]
        public void ObjectCompare_TwoObjects_Succeed()
        {
            OBJ_YAMEY_AVODA_OVDIM obj = new OBJ_YAMEY_AVODA_OVDIM();
            obj.BITUL_ZMAN_NESIOT = 1.1m;
            obj.MEADKEN_ACHARON = 3;
            ModificationRecorder<OBJ_YAMEY_AVODA_OVDIM> wrapper = new ModificationRecorder<OBJ_YAMEY_AVODA_OVDIM>(obj,true);

            wrapper.ContainedItem.BITUL_ZMAN_NESIOT = 2;

            Assert.IsTrue(wrapper.HasChanged(), "Did not find change");
        }
    }
}
