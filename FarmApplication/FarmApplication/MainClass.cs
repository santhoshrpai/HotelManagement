using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


public delegate void priceCutEvent(Int32 pr);
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


            MultiCellBuffer sembuffer = new MultiCellBuffer();
            sembuffer.setOnecell("value");
            HotelSupplier supplier = new HotelSupplier();
            Thread hotel = new Thread(new ThreadStart(supplier.pricingModel));
            hotel.Start();
            TravelAgency agency = new TravelAgency();
            HotelSupplier.priceCutEvent += new priceCutEvent(agency.HotelOnSale);
            Thread[] retailers = new Thread[3];
            for (int i = 0; i < 1; i++)
            {
                retailers[i] = new Thread(new ThreadStart(agency.RetailerFunction));
                retailers[i].Name = (i + 1).ToString();
                retailers[i].Start();
            }
        }
    }
}
