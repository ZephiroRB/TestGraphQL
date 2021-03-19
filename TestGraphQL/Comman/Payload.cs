using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Comman
{
    public class Payload
    {
        protected Payload(IReadOnlyList<TransactionMessage>? message = null)
        {
            Message = message;
        }
        public IReadOnlyList<TransactionMessage>? Message { get; }
    }
}
