using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsBatch.Entities;

namespace KdsBatch.Errors
{
    public static class GlobalData
    {
        public static List<ErrorItem> ActiveErrors { get; set; }
        public static List<CardError> CardErrors { get; set; }
    }
}
