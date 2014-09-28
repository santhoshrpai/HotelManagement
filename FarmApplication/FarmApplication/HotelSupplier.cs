using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;



namespace HotelManagement
{
   
    //************************************************************************************************************
    //          Class HotelSupplier
    //
    // Definition: Hotel Supplier which invokes price cut event and receives the order from MulticellBuffer
    //************************************************************************************************************
    
    public class HotelSupplier
    {
        static Random rng = new Random();
        public static event priceCutEvent priceCutEvent;
        private static Int32 hotelPrice;
        private String hotelName;
        private Int32 totalNumberOfRooms;

       public HotelSupplier(String name , Int32 initialPrice, Int32 totalRooms)
        {
            this.hotelName = name;
            hotelPrice = initialPrice;
            totalNumberOfRooms = totalRooms;
        }
        //*********************************************
        //          Method getPrice
        //*********************************************
        public Int32 getPrice()
        {
            return hotelPrice;
        }
        //*********************************************
        //          Method getName
        //*********************************************
        public String getName()
        {
            return hotelName;
        }



        //*********************************************
        //          Method changePrice
        //*********************************************
        public static void changePrice(Int32 price, String hotelName)
        {
            if (price < hotelPrice)
            {
                Console.WriteLine("\n\n\n\t\t***Price Down!!! Hurry up in BOOKING!!*** \n\t\t{0} on sale price : ${1} per room\n\n", hotelName, price);
            } else {
                Console.WriteLine("\n\n\n\t\t***Price Up!!! Market is at Peak,:-( !*** \n\t\t{0} on sale price : ${1} per room\n\n", hotelName, price);
            }
            if (priceCutEvent != null)
            {
                priceCutEvent(price, hotelName);
            }
            hotelPrice = price;
        }

        //*********************************************
        //          Method HotelPricecreation
        //*********************************************
        public void pricingModel()
        {
            for (Int32 i = 0; i < 5; i++)
            {
                //Timing
                Thread.Sleep(rng.Next(500,1000));
                Int32 p = rng.Next(25, 100);
                HotelSupplier.changePrice(p,hotelName);
            }

        }

        public void processOrder(OrderObject obj)
        {
            if (totalNumberOfRooms < obj.getRooms())
            {
                Console.WriteLine("\n\n\t\tRooms are full at {0} !! Sorry {1}!!\n", obj.getreceiverID(), obj.getsenderID());
                return;
            }
            FarmApplication.EncryptDecryptService.ServiceClient client = new FarmApplication.EncryptDecryptService.ServiceClient();
            String encryptedstring = client.Encrypt(obj.getcardNumber().ToString());
            BankService bankser = new BankService();
            String result=bankser.validateCard(encryptedstring);
            if (result == "Valid")
            {
                totalNumberOfRooms--;
                DateTime now = DateTime.UtcNow;
                TimeSpan difference = now.Subtract(obj.getTime());
                obj.setOrderTime(difference.TotalSeconds);
                TravelAgency.callback(obj);

            }
            else
            {
                Console.WriteLine("\n\n\n\t\tInvalid Card - Order received from {0} for {1} failed!!", obj.getsenderID(), obj.getreceiverID());
            }
        }

        //*********************************************
        //          Read Order
        //*********************************************
        public void checkOrderFromMultibuffer()
        {
            while (true)
            {
               String placedorder = MainClass.multiCellBuffer.getOneCell(this.hotelName);
               if (placedorder != null)
               {
                   EncoderandDecoder decoder = new EncoderandDecoder();
                   OrderObject obj = decoder.decode(placedorder);
                   processOrder(obj);
               }
            }
        } 

 



    }
}
