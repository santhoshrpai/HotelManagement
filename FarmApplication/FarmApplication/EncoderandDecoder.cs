using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    /// <summary>
    /// Encoding and Decoding class
    /// </summary>
    class EncoderandDecoder
    {
        /// <summary>
        /// Encode by inserting ## between two values
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public String encode(OrderObject obj)
        {
            String encodedString = String.Format("{0}##{1}##{2}##{3}##{4}##{5}", obj.getsenderID(), obj.getreceiverID(), obj.getcardNumber(), obj.getamount(),obj.getTime(),obj.getRooms());
            return encodedString;
            
        }

        /// <summary>
        /// Decode by splitting the value by ##
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public OrderObject decode(String str)
        {
            OrderObject decodedobj= new OrderObject();
            string[] parts = str.Split(new string[] { "##" }, StringSplitOptions.None);
            decodedobj.setsenderID(parts[0]);
            decodedobj.setreceiverID(parts[1]);
            decodedobj.setcardNumber(Int32.Parse( parts[2]));
            decodedobj.setamount(Int32.Parse( parts[3]));
            decodedobj.setTime(Convert.ToDateTime(parts[4]));
            decodedobj.setRooms(Int32.Parse(parts[5]));
            return decodedobj;
        }
    }
}
