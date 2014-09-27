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
    // Definition:
    //************************************************************************************************************
    
    public class HotelSupplier
    {
        static Random rng = new Random();
        public static event priceCutEvent priceCutEvent;
        //private static Int32 Rooms = 10;
        private static Int32 hotelPrice;
        private String hotelName;

       public HotelSupplier(String name , Int32 initialPrice)
        {
            this.hotelName = name;
            hotelPrice = initialPrice;
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
                Console.WriteLine("{0} on sale price : ${1} per room",hotelName,price);
                if (priceCutEvent != null)
                    priceCutEvent(price,hotelName);
            }
            hotelPrice = price;
        }

        //*********************************************
        //          Method HotelPricecreation
        //*********************************************
        public void pricingModel()
        {
            for (Int32 i = 0; i < 20; i++)
            {
                //Timing
                Thread.Sleep(5000);
                Int32 p = rng.Next(25, 100);
                HotelSupplier.changePrice(p,hotelName);
            }

        }

        public void processOrder(OrderObject obj)
        {
            FarmApplication.EncryptDecryptService.ServiceClient client = new FarmApplication.EncryptDecryptService.ServiceClient();
            String encryptedstring = client.Encrypt(obj.getcardNumber().ToString());
            BankService bankser = new BankService();
            String result=bankser.validateCard(encryptedstring);
            if (result == "Valid")
            {
                Console.WriteLine("Validation Success!!! Payment Received");
                Console.WriteLine("\n\t\t**********Booking Reciept ***************\n\t\tHotel:{0}\n\t\tAgency:{1}\n\t\tCard No:{2}\n\t\tAmount:{3}\n\t\tTAX:(0.08%){4}\n\t\tTotal:{5}\n\t\t*********************************\n", obj.getreceiverID(), obj.getsenderID(), obj.getcardNumber(), obj.getamount(), obj.getamount() * 0.08,obj.getamount()*1.08);

            }
            else
                Console.WriteLine("Invalid Card - Not Registered");
                Console.WriteLine("\n\t\t**********Booking Reciept ***************");
        }

        //*********************************************
        //          Read Order
        //*********************************************
        public void checkOrderFromMultibuffer()
        {
            MultiCellBuffer sembuffer = new MultiCellBuffer();
            while (true)
            {
                //Timing
              
               Thread.Sleep(6000);
               //Console.WriteLine("check buffer for {0}", hotelName);
               String placedorder = sembuffer.getOneCell(this.hotelName);
               if (placedorder != null)
               {
                   OrderObject obj = new OrderObject();
                   EncoderandDecoder decoder = new EncoderandDecoder();
                   obj = decoder.decode(placedorder);
                   processOrder(obj);
               }
            }
        } 

 



    }
}
