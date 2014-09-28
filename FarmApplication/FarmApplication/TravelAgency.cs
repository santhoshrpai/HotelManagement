using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HotelManagement
{

  //************************************************************************************************************
  //          Class TravelAgency
  //
  // Definition:
  //************************************************************************************************************
   public class TravelAgency
    {
       Random random = new Random();
       private String Agencyname;
       int noOfRooms = 0;

       public TravelAgency(String name)
       { Agencyname = name; }

            //*********************************************
            //          Method RetailerFunction
            //*********************************************
       public void RetailerFunction()
            {
                /*HotelSupplier room = new HotelSupplier();
                for (Int32 i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Int32 p = room.getPrice();
                    Console.WriteLine("Hotel Supplier {0} has everyday price : ${1} each", Thread.CurrentThread.Name, p);

                }*/
            }
            //*********************************************
            //          Method HotelFunc
            //*********************************************
       public void HotelOnSale(Int32 p, String hotelname)
       {
           // Check the price and generate the number of rooms based on it
           if (p < 70)
           {
               noOfRooms = random.Next(1, 5);
           }
           else
           {
               noOfRooms = random.Next(5, 10);
              
           }
           placeOrder(noOfRooms, p, hotelname, Agencyname);
           Thread.Sleep(random.Next(500, 1000));
       }

       public static void callback(OrderObject obj)
       {
           Console.WriteLine("\n\n\t\tValidation Success!!! Payment Received\n\t\t**********Booking Reciept ***************\n\t\tHotel:{0}\n\t\tAgency:{1}\n\t\tCard No:{2}\n\t\tAmount:{3}\n\t\t\n\t\tNo of Rooms:{4}\n\t\tTAX:(0.08%){5}\n\t\tTotal:{6}\n\t\tOrder Time:{7}\n\t\t*********************************\n", obj.getreceiverID(), obj.getsenderID(), obj.getcardNumber(), obj.getamount(), obj.getRooms(), obj.getamount() * 0.08, obj.getamount() * 1.08, obj.getOrderTime());

       }


            //*********************************************
            //          placeOrder
            //*********************************************
            public void placeOrder(int noOfRooms, int price ,String hotelname, String agencyname)
            {
                Random rnd = new Random();
                Int32 creditcard =rnd.Next(1000, 7999);
                OrderObject order = new OrderObject();
                order.setsenderID(agencyname);
                order.setreceiverID(hotelname);
                order.setcardNumber(creditcard);
                order.setamount(noOfRooms * price);
                order.setRooms(noOfRooms);

                //Console.WriteLine("-------------------------------------------------------Order has been Created-------------/n{0}/n{1}--------", order.getsenderID(), order.getreceiverID());

                bool flag = false;
                try
                {
                    Monitor.TryEnter(MainClass.multiCellBuffer, rnd.Next(1000, 1500), ref flag);
                    if (flag)
                    {
                        DateTime now = DateTime.UtcNow;
                        order.setTime(now);
                        // Encoding
                        EncoderandDecoder encoder = new EncoderandDecoder();
                        String encodedOrder = encoder.encode(order);
                        MainClass.multiCellBuffer.setOnecell(encodedOrder);
                    }
                    else
                        Thread.Sleep(rnd.Next(1000, 1500));
                }
                finally
                {
                   // if (flag)
                        
                        Monitor.Exit(MainClass.multiCellBuffer);
                     //   Console.WriteLine("-------------------------------------------------------LOCK released         --------------- Order has been Created-------------/n{0}/n{1}--------", order.getsenderID(), order.getreceiverID());

                }
             }
       }

    }
