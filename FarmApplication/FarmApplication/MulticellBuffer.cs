using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
namespace HotelManagement
{

     class MultiCellBuffer
    {
         public static ArrayList buffer = new ArrayList();
         public static Semaphore pool;
         private bool writable = true;

         public MultiCellBuffer ()
         {
             pool = new Semaphore(0, 3);
             pool.Release(3);
         }

        //Thread placeIt = new Thread(new Parameterize  dThreadStart(OrderObject));

        public void setOnecell(String order){
          
            if (buffer.Count < 3)
            {
                pool.WaitOne();
                buffer.Add(order);
                string[] parts = order.Split(new string[] { ":" }, StringSplitOptions.None);
                // Console.WriteLine("\n**********Order Has created***************\nHotel:{0}\nAgency:{1}\nCard No:{2}\nAmount:{3}\n*********************************\n", parts[1], parts[0],parts[2],parts[3]);
            }
        
        }



        public String getOneCell(String hotelname){
            if (buffer.Count > 0)
            {
                for (int i = 0; i < buffer.Count; i++)
                {
                    try
                    {
                        {
                            String singleorder = (String)buffer[i];
                            if (hotelname != null)
                            {
                                if (singleorder.Contains(hotelname))
                                {
                                    buffer.Remove(singleorder);
                                    pool.Release();
                                    string[] parts = singleorder.Split(new string[] { ":" }, StringSplitOptions.None);
                                  //  Console.WriteLine("\nOrder Recognized sent for process\nHotel:{0} \nAgency:{1}\n\n", parts[1], parts[0]);
                                    return singleorder;
                                }
                            }
                        }
                    }
                    catch
                    {
                       
                    }
                }
            }
            return null;
        }
    }
}
