using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.Nezer
{
    public class NezerContainer
    {
        public NezerDM Nezer { get; set; }
        public NezerMergeTypes NezerMergeType { get; set; }
        public bool Approved { get; set; }

        public NezerContainer()
        {
            NezerMergeType = NezerMergeTypes.None;
            Approved = false;
        }
    }

    public enum NezerMergeTypes
    {
        None,
        Add,
        Remove,
        Update
    }
}
