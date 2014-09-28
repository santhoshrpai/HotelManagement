using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    /// <summary>
    /// Bank Service to validate the Credit Card number
    /// </summary>
    class BankService
    {
        /// <summary>
        /// Validating CC number
        /// </summary>
        /// <param name="encrytedcard"></param>
        /// <returns></returns>
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
