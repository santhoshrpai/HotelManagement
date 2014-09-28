using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    class EncoderandDecoder
    {
        public String encode(OrderObject obj)
        {
            String encodedString = String.Format("{0}##{1}##{2}##{3}##{4}##{5}", obj.getsenderID(), obj.getreceiverID(), obj.getcardNumber(), obj.getamount(),obj.getTime(),obj.getRooms());
            return encodedString;
            
        }
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
