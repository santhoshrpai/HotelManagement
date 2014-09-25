using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


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
        private static Int32 Rooms = 10;
        private static Int32 hotelPrice = 30;
        //*********************************************
        //          Method getPrice
        //*********************************************
        public Int32 getPrice()
        {
            return hotelPrice;
        }

        //*********************************************
        //          Method changePrice
        //*********************************************
        public static void changePrice(Int32 price)
        {
            if (price < hotelPrice)
            {
                if (priceCutEvent != null)
                    priceCutEvent(price);
            }
            hotelPrice = price;
        }

        //*********************************************
        //          Method HotelPricecreation
        //*********************************************
        public void pricingModel()
        {
            for (Int32 i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Int32 p = rng.Next(25, 100);
                HotelSupplier.changePrice(p);
            }

        }


        //*********************************************
        //          Method OrderProcessing
        //*********************************************
        public void OrderProcessing()
        {


        }   
    }
}
