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

           // HotelManagement.EcryptDecryptService.ServiceClient client= 
           //WebBrowserPravin.EncryptDecryptService.ServiceClient client = new WebBrowserPravin.EncryptDecryptService.ServiceClient();
           // lbl_encrypt_output.Text = client.Encrypt(Txt_encrypt_input.Text);
           // WebBrowserPravin.EncryptDecryptService.ServiceClient client = new WebBrowserPravin.EncryptDecryptService.ServiceClient();
           // lbl_decrypt_output.Text = client.Decrypt(lbl_decrypt_input.Text);
    }
}
