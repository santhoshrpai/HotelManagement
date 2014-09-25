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
       int noOfRooms = 0;
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
            //          Method farmerFunc
            //*********************************************
            public void HotelOnSale(Int32 p)
            {
                Console.WriteLine("Hotel Supplier on sale:has everyday low price : ${0} per room", p);

                // Check the price and generate the number of rooms based on it
                if (p < 70)
                {
                    noOfRooms = random.Next(1, 5);
                }
                else
                {
                    noOfRooms = random.Next(5, 10);
                }
                   
                placeOrder(noOfRooms, p);
                
            }


            public static void placeOrder(int noOfRooms, int price)
            {
                OrderObject order = new OrderObject();
                order.setsenderID("Thread1");
                order.setreceiverID("Pravin");
                order.setcardNumber(2353);
                order.setamount(noOfRooms * price);

                // Encoding
                EncoderandDecoder encoder = new EncoderandDecoder();
                String encodedOrder = encoder.encode(order);
                

            }

            
        }

    }
