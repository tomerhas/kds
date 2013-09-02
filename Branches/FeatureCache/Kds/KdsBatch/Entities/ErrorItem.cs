using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsBatch.Errors;

namespace KdsBatch.Entities
{
    public class ErrorItem
    {
        public TypeCheck Id { get; set; }
        public OriginError Origin { get; set; }
        public ErrorItem(TypeCheck type, OriginError origin)
        {
            Id = type;
            Origin = origin;
        }
    }
}
