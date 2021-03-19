using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Comman
{
    public class TransactionMessage
    {
        public TransactionMessage(string message, string code)
        {
            Message = message;
            Code = code;
        }
        public string Message { get; }

        public string Code { get; }
    }
}
