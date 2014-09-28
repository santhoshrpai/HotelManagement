using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HotelManagement
{

     /// <summary>
     /// TravelAgency which receives the price cut event and books the rooms
     /// </summary>
   public class TravelAgency
    {
       Random random = new Random();
       private String Agencyname;
       int noOfRooms = 0;

       public TravelAgency(String name)
       { Agencyname = name; }

       /// <summary>
       /// Checks the price rise and fall and places the order accordingly
       /// </summary>
       /// <param name="p"></param>
       /// <param name="hotelname"></param>
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

       /// <summary>
       /// Callback method from the hotelsupplier to confirm the order
       /// </summary>
       /// <param name="obj"></param>
       public static void callback(OrderObject obj)
       {
           Console.WriteLine("\n\n\t\tValidation Success!!! Payment Received\n\t\t**********Booking Reciept ***************\n\t\tHotel:{0}\n\t\tAgency:{1}\n\t\tCard No:{2}\n\t\tAmount:{3}\n\t\tOrder Time:{4}\n\t\t*********************************\n", obj.getreceiverID(), obj.getsenderID(), obj.getcardNumber(), obj.getamount(), obj.getOrderTime());
       }


            /// <summary>
            /// Places the Order
            /// </summary>
            /// <param name="noOfRooms"></param>
            /// <param name="price"></param>
            /// <param name="hotelname"></param>
            /// <param name="agencyname"></param>
            public void placeOrder(int noOfRooms, int price ,String hotelname, String agencyname)
            {
                Random rnd = new Random();
                Int32 creditcard =rnd.Next(5000, 7500); // CC numbers are generated randomly to evaluate INVALID usecase also
                OrderObject order = new OrderObject();
                order.setsenderID(agencyname);
                order.setreceiverID(hotelname);
                order.setcardNumber(creditcard);
                order.setamount(noOfRooms * price);
                order.setRooms(noOfRooms);

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
                   Monitor.Exit(MainClass.multiCellBuffer);
                   
                }
             }
       }

    }
