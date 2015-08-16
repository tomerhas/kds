using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSBLLogic.Managers
{
    public class ClockManager : IClockManager
    {
        private IUnityContainer _container;
        public ClockManager(IUnityContainer container)
        {
            _container = container;
        }

        public void SaveShaonimMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment)
        {
             _container.Resolve<IClockDAL>().SaveHarmonyMovment(collHarmonyMovment);
        }
    }
}
