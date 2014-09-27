using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    public class OrderObject
    {
       private String senderID;
       private String receiverID;
       private Int32 cardNumber;  
       private Int32 amount;

        public String getsenderID()
        {
            return this.senderID;
        }
        public void setsenderID(String newsenderID)
        {
            this.senderID = newsenderID;
        }


        public String getreceiverID()
        {
            return this.receiverID;
        }
        public void setreceiverID(String newreceiverID)
        {
            this.receiverID = newreceiverID;
        }



        public Int32 getcardNumber()
        {
            return this.cardNumber;
        }
        public void setcardNumber(Int32 newcardNumber)
        {
            this.cardNumber = newcardNumber;
        }



        public Int32 getamount()
        {
            return this.amount;
        }
        public void setamount(Int32 newamount)
        {
            this.amount = newamount;
        }

    }
}
