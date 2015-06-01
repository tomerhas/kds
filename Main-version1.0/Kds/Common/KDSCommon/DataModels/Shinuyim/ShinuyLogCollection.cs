using KDSCommon.Enums;
using KDSCommon.Interfaces.Shinuyim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.Shinuyim
{
    public class ShinuyLogCollection : IShinuyLogCollection
    {
        private List<SingleShinuyLog> _logShinuyimList;

        public void AddShinuy(IShinuy shinuy,SidurDM sidur)
        {
            SingleShinuyLog log = new SingleShinuyLog();
            log.ShinuyType = shinuy.ShinuyType;

            SetSidurProps(sidur, log);

            _logShinuyimList.Add(log);
        }

        public void AddShinuy(IShinuy shinuy, SidurDM sidur,PeilutDM peilut)
        {
            SingleShinuyLog log = new SingleShinuyLog();
            log.ShinuyType = shinuy.ShinuyType;

            SetSidurProps(sidur, log);

            if (peilut != null)
            {
                log.ShatYetzia = peilut.dFullShatYetzia;
            }

            _logShinuyimList.Add(log);
        }

        private  void SetSidurProps(SidurDM sidur, SingleShinuyLog log)
        {
            if (sidur != null)
            {
                log.MisparIshi = sidur.iMisparIshi;
                log.Taarich = sidur.dSidurDate;
                log.ShatHatchala = sidur.dFullShatHatchala;
                log.ShatGmar = sidur.dOldFullShatGmar;
            }
        }


    }
}
