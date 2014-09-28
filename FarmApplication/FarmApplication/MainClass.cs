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
        public static MultiCellBuffer multiCellBuffer = new MultiCellBuffer();
        
        static void Main(string[] args)
        {
          
            HotelSupplier supplier1 = new HotelSupplier("Holiday Inn",40,50);
            HotelSupplier supplier2 = new HotelSupplier("Sheraton",30,20);
            HotelSupplier supplier3 = new HotelSupplier("Grandway",25,50);

            Thread hotel1 = new Thread(new ThreadStart(supplier1.pricingModel));
            Thread hotel2 = new Thread(new ThreadStart(supplier2.pricingModel));
            Thread hotel3 = new Thread(new ThreadStart(supplier3.pricingModel));

            hotel1.Start();
            hotel2.Start();
            hotel3.Start();

            Thread hotel1check = new Thread(new ThreadStart(supplier1.checkOrderFromMultibuffer));
            Thread hotel2check = new Thread(new ThreadStart(supplier2.checkOrderFromMultibuffer));
            Thread hotel3check = new Thread(new ThreadStart(supplier3.checkOrderFromMultibuffer));


            hotel1check.Start();
            hotel2check.Start();
            hotel3check.Start();

            TravelAgency agency1 = new TravelAgency("Expedia");
            TravelAgency agency2 = new TravelAgency("ClearTrip");
            TravelAgency agency3 = new TravelAgency("MakeMyTrip");
            TravelAgency agency4 = new TravelAgency("Yatra");
            TravelAgency agency5 = new TravelAgency("Via.com");



            HotelSupplier.priceCutEvent += new priceCutEvent(agency1.HotelOnSale);
            HotelSupplier.priceCutEvent += new priceCutEvent(agency2.HotelOnSale);
            HotelSupplier.priceCutEvent += new priceCutEvent(agency3.HotelOnSale);
            HotelSupplier.priceCutEvent += new priceCutEvent(agency4.HotelOnSale);
            HotelSupplier.priceCutEvent += new priceCutEvent(agency5.HotelOnSale);

            hotel1.Join();
            hotel2.Join();
            hotel3.Join();

            Console.WriteLine("Main thread: All threads have been terminated.");
            
        }
    }
}
