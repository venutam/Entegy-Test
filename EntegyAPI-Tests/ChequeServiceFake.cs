using System;
using System.Collections.Generic;
using System.Text;
using EntegyAPI.Interface;

namespace EntegyAPI_Tests
{
    class ChequeServiceFake : IChequeService
    {
        public ChequeServiceFake()
        {

        }

        public string ConvertDecimalToWord(string amt)
        {
            return "";
        }
    }
}
