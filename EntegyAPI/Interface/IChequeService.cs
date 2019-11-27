using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntegyAPI.Interface
{
    public interface IChequeService
    {
        string ConvertDecimalToWord(string amt);
    }
}
