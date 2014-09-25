using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HotelManagement
{

    class MultiCellBuffer
    {           
        private static String[] multicellBuffer = new String[3];
        private static Semaphore pool = new Semaphore(0, 3);

        public MultiCellBuffer() {
            pool.Release(3);
        }

        public MultiCellBuffer(String order) {

        }


        //Thread placeIt = new Thread(new ParameterizedThreadStart(OrderObject));

        public void setOnecell(String order){
        pool.WaitOne();
        Console.WriteLine("Wrt 1" );
        pool.WaitOne();
        Console.WriteLine("Wrt2");
        pool.WaitOne();
        Console.WriteLine("Wrt3");
        pool.WaitOne();
        Console.WriteLine("Wrt what");
        pool.Release();
        Console.WriteLine("Wrt new");
           
        }

        public void getOneCell(){
         pool.Release();
        }
 
    }
}
