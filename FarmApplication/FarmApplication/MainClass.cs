using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;


public delegate void priceCutEvent(Int32 pr, String hotelname);
public delegate void orderinBuffer();
namespace HotelManagement
{
    class MainClass
    {
        static ArrayList  validcreditcards= new ArrayList();
        
        

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


            validcreditcards.Add("1234");
            validcreditcards.Add("4225");
            validcreditcards.Add("7334");
            validcreditcards.Add("0556");
            MultiCellBuffer sembuffer = new MultiCellBuffer("allocate");
           // sembuffer.setOnecell("value");
            HotelSupplier supplier1 = new HotelSupplier("Holiday Inn",40);
            HotelSupplier supplier2 = new HotelSupplier("Hilton",30);
            HotelSupplier supplier3 = new HotelSupplier("Grandway",25);
            HotelSupplier supplier4 = new HotelSupplier("Parktown", 25);

            Thread hotel1 = new Thread(new ThreadStart(supplier1.pricingModel));
            Thread hotel2 = new Thread(new ThreadStart(supplier2.pricingModel));
            Thread hotel3 = new Thread(new ThreadStart(supplier3.pricingModel));
            Thread hotel4 = new Thread(new ThreadStart(supplier4.pricingModel));

            hotel1.Start();
            hotel2.Start();
            hotel3.Start();
            hotel4.Start();

            Thread hotel1check = new Thread(new ThreadStart(supplier1.checkOrderFromMultibuffer));
            Thread hotel2check = new Thread(new ThreadStart(supplier2.checkOrderFromMultibuffer));
            Thread hotel3check = new Thread(new ThreadStart(supplier3.checkOrderFromMultibuffer));
            Thread hotel4check = new Thread(new ThreadStart(supplier4.checkOrderFromMultibuffer));


            hotel1check.Start();
            hotel2check.Start();
            hotel3check.Start();
            hotel4check.Start();

            TravelAgency agency1 = new TravelAgency("Expedia");
            TravelAgency agency2 = new TravelAgency("Priceline");
            TravelAgency agency3 = new TravelAgency("hotwire");
            TravelAgency agency4= new TravelAgency("Yaatra");



            HotelSupplier.priceCutEvent += new priceCutEvent(agency1.HotelOnSale);
            HotelSupplier.priceCutEvent += new priceCutEvent(agency2.HotelOnSale);
            HotelSupplier.priceCutEvent += new priceCutEvent(agency3.HotelOnSale);
            HotelSupplier.priceCutEvent += new priceCutEvent(agency4.HotelOnSale);
            
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
