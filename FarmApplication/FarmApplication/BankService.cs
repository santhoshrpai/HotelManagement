using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    class BankService
    {
        public string validateCard(String encrytedcard)
        {
            FarmApplication.EncryptDecryptService.ServiceClient client = new FarmApplication.EncryptDecryptService.ServiceClient();
            String decryptedcard = client.Decrypt(encrytedcard);
            if(Int32.Parse(decryptedcard)<6000)
            {
                return "Valid";
            }
                return "Invalid";
        }
   }
}
