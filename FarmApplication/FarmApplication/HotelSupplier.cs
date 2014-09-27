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
                Thread.Sleep(2000);
                Int32 p = rng.Next(25, 100);
                HotelSupplier.changePrice(p,hotelName);
            }

        }


        public void processOrder(OrderObject obj)
        {

        }

        //*********************************************
        //          Read Order
        //*********************************************
        public void checkOrderFromMultibuffer()
        {
            MultiCellBuffer sembuffer = new MultiCellBuffer();
            while (true)
            {
               Thread.Sleep(500);
               ArrayList buffer = sembuffer.sendbuffer();
               if(buffer.Count>0)
               {
                   for (int i = 0; i < buffer.Count;i++)
                   {
                       String orderrefernce= (String)buffer[i];
                       if(orderrefernce.Contains(this.hotelName))
                       {
                           sembuffer.getOneCell(orderrefernce);
                           EncoderandDecoder decoder= new EncoderandDecoder();
                           OrderObject obj = new OrderObject();
                           obj=decoder.decode(orderrefernce);
                           processOrder(obj);
                           
                       }
                   }
               }
                
            }
        } 

 



    }
}
