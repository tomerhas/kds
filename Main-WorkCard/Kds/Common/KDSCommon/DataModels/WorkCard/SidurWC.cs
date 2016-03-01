using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    /// <summary>
    /// Contains the data of a users single sidur
    /// This is a view model requested by the new workcard display (angular) 
    /// and contains only the data related to the display
    /// </summary>
    public class SidurWC
    {
        public SinglePropertyData<string> CollapseSrc { get; set; }
        public SinglePropertyData<int> MisparSidur { get; set; }
        public SinglePropertyData<DateTime> FullShatHatchala { get; set; }
        public SinglePropertyData<DateTime> FullShatGmar { get; set; }
        public SinglePropertyData<int> SibatHachtamaIn { get; set; }
        public SinglePropertyData<int> SibatHachtamaOut { get; set; }
        public SinglePropertyData<DateTime> FullShatHatchalaLetashlum { get; set; }
        public SinglePropertyData<DateTime> FullShatGmarLetashlum { get; set; }
        public SinglePropertyData<int> Hariga { get; set; }
        public SinglePropertyData<int> Pizul { get; set; }
        public SinglePropertyData<string> Hashlama { get; set; }
        public SinglePropertyData<string> Hamara { get; set; }
        public SinglePropertyData<string> OutMichsa { get; set; }
        public SinglePropertyData<int> LoLetashlum { get; set; }
        public SinglePropertyData<bool> SidurActive { get; set; }

        public List<PeilutWC> PeilutList { get; set; }

        public SidurWC()
        {
            CollapseSrc = new SinglePropertyData<string>();
            MisparSidur = new SinglePropertyData<int>();
            FullShatHatchala = new SinglePropertyData<DateTime>();
            FullShatGmar = new SinglePropertyData<DateTime>();
            SibatHachtamaIn = new SinglePropertyData<int>();
            SibatHachtamaOut = new SinglePropertyData<int>();
            FullShatHatchalaLetashlum = new SinglePropertyData<DateTime>();
            FullShatGmarLetashlum = new SinglePropertyData<DateTime>();
            Hariga = new SinglePropertyData<int>();
            Pizul = new SinglePropertyData<int>();
            Hashlama = new SinglePropertyData<string>();
            Hamara = new SinglePropertyData<string>();
            OutMichsa = new SinglePropertyData<string>();
            LoLetashlum = new SinglePropertyData<int>();
            SidurActive = new SinglePropertyData<bool>();

            PeilutList = new List<PeilutWC>();
        }

    }
}
