using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
namespace HotelManagement
{
    /// <summary>
    /// MultiCellBuffer to act as buffer space to place order
    /// </summary>
     class MultiCellBuffer
    {
         public static ArrayList buffer = new ArrayList();
         public static Semaphore pool;

         public MultiCellBuffer ()
         {
             pool = new Semaphore(0, 3);
             pool.Release(3);
         }

         /// <summary>
         /// Set an order in the buffer
         /// </summary>
         /// <param name="order"></param>
        public void setOnecell(String order){
         if (buffer.Count < 3)
            {
                pool.WaitOne();
                buffer.Add(order);
                string[] parts = order.Split(new string[] { ":" }, StringSplitOptions.None);
            }
        }

         /// <summary>
         /// Get an order from the buffer
         /// </summary>
         /// <param name="hotelname"></param>
         /// <returns></returns>
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
