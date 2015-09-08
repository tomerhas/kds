using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCompare.Results
{
    /// <summary>
    /// Result container for all the fields compared
    /// </summary>
    public class CompareResultContainer
    {
        public List<CompareResult> CompareResultsList { get; set; }
        public CompareResultContainer()
        {
            CompareResultsList = new List<CompareResult>();
        }

        public void Add(CompareResult result)
        {
            CompareResultsList.Add(result);
        }
    }
}
