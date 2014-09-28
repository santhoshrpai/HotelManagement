using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;



namespace HotelManagement
{
    /// <summary>
    /// Hotel Supplier which invokes price cut event and receives and processes the order from MulticellBuffer
    /// </summary>
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

        public Int32 getPrice()
        {
            return hotelPrice;
        }
       
        public String getName()
        {
            return hotelName;
        }

       /// <summary>
       /// Changes the price and invokes Price cut event
       /// </summary>
       /// <param name="price"></param>
       /// <param name="hotelName"></param>
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

        /// <summary>
        /// Generates random price for a HotelSupplier
        /// </summary>
        public void pricingModel()
        {
            for (Int32 i = 0; i < 5; i++)
            {
                Thread.Sleep(rng.Next(500,1000)); // Sleep before generating next price cut event
                Int32 p = rng.Next(25, 100);
                HotelSupplier.changePrice(p,hotelName);
            }

        }

        /// <summary>
        /// Process the Order
        /// </summary>
        /// <param name="obj"></param>
        public void processOrder(OrderObject obj)
        {
            if (totalNumberOfRooms < obj.getRooms())
            {
                Console.WriteLine("\n\n\t\tTotal rooms available at {0} are {1}. Requested number of rooms are {2} !! Sorry {3}, we dont allow our guests to stay in air!!\n", obj.getreceiverID(), totalNumberOfRooms
                ,obj.getRooms(),obj.getsenderID());
                return;
            }
            FarmApplication.EncryptDecryptService.ServiceClient client = new FarmApplication.EncryptDecryptService.ServiceClient();
            String encryptedstring = client.Encrypt(obj.getcardNumber().ToString());
            BankService bankser = new BankService();
            String result=bankser.validateCard(encryptedstring);
            if (result == "Valid")
            {
                totalNumberOfRooms-=obj.getRooms();
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

        /// <summary>
        /// Check the MultiCellBuffer for Orders
        /// </summary>
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