using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


public delegate void priceCutEvent(Int32 pr, String hotelname);
public delegate void orderinBuffer();
namespace HotelManagement
{
    class MainClass
    {
        
        static void Main(string[] args)
        {


            // Encoder and Decoder Testing
      /*      OrderObject or = new OrderObject();
            or.setsenderID("Thread1");
            or.setreceiverID("Pravin");
            or.setcardNumber(2353);
            or.setamount(23);
            OrderObject newz = new OrderObject();
            EncoderandDecoder ende = new EncoderandDecoder();
            String value = ende.Encoder(or);
            Console.WriteLine(value);
            newz=ende.Decoder(value);
            Console.WriteLine(newz.getsenderID());
            Console.WriteLine(newz.getreceiverID());
            Console.WriteLine(newz.getamount());
            Console.WriteLine(newz.getcardNumber());*/


           // MultiCellBuffer sembuffer = new MultiCellBuffer();
           // sembuffer.setOnecell("value");
            HotelSupplier supplier1 = new HotelSupplier("Holiday Inn",40);
           // HotelSupplier supplier2 = new HotelSupplier("Chen Son",30);
            //HotelSupplier supplier3 = new HotelSupplier("Machans",25);

            Thread hotel1 = new Thread(new ThreadStart(supplier1.pricingModel));
            //Thread hotel2 = new Thread(new ThreadStart(supplier2.pricingModel));
            //Thread hotel3 = new Thread(new ThreadStart(supplier3.pricingModel));
            hotel1.Start();
           // hotel2.Start();
            //hotel3.Start();

            Thread hotel1check = new Thread(new ThreadStart(supplier1.checkOrderFromMultibuffer));
            hotel1check.Start();

            TravelAgency agency = new TravelAgency();
            HotelSupplier.priceCutEvent += new priceCutEvent(agency.HotelOnSale);
            
            //Thread[] retailers = new Thread[3];
            //for (int i = 0; i < 1; i++)
            //{
            //    retailers[i] = new thread(new threadstart(agency.retailerfunction));
            //    retailers[i].name = (i + 1).tostring();
            //    retailers[i].start();
            //}
        }
    }
}
