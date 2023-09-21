using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public class TransactionResponse
    {
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

    }
}
