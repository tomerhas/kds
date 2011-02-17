using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsTaskManager
{
    public class Message
    {
        public int GroupId { get; set; }
        public int IdOrder { get; set; }
        public TypeCommand TypeCommand { get; set; }
        public TypeStatus StatusReport { get; set; }
        public int Sequence { get; set; }
        public string Remark { get; set; }
    }

    public enum TypeCommand
    {
        Program = 1,
        StoredProcedure = 2
    }
    public enum TypeStatus
    {
        /// <remarks>set on start of action</remarks>
        Running = 1,
        /// <remarks>set on failure</remarks>
        Stopped = 3,
        /// <remarks>set on success</remarks>
        Success = 4
    }

}
