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
         private static ArrayList buffer = new ArrayList();
         public static Semaphore pool;

         public MultiCellBuffer() {
         pool= new Semaphore(0, 3);
         pool.Release(3);
       
        }

        //Thread placeIt = new Thread(new Parameterize  dThreadStart(OrderObject));

        public void setOnecell(String order){
        pool.WaitOne();
            if(buffer.Count<3)
            {
                buffer.Add(order);
                Console.WriteLine("***********Order is set in buffer");
            }       
        }



        public void getOneCell(String order){
         buffer.Remove(order);
         
         Console.WriteLine("***********Order removed from buffer {0}",pool.Release());
     
        }

        public ArrayList sendbuffer()
        {
            return buffer;
        }
 
    }
}
