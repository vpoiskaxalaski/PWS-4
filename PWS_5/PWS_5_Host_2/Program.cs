using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PWS_5_Host_2
{
    class Program
    {
        static void Main(string[] args)
        {
            C1.Service1Client service1Client = new C1.Service1Client("tcp");

            Console.WriteLine("A1 { f = 1.3f, k = 2, s = 111 }, A2 { f = 5.3f, k = 2, s = 333 }");
            var sumResult = service1Client.Sum(new PWS_5_WCF.A { f = 1.3f, k = 2, s = "111" }, new PWS_5_WCF.A { f = 5.3f, k = 2, s = "333" });
            Console.WriteLine($"Sum\nf = {sumResult.f}\nk = {sumResult.k}\ns = {sumResult.s}\n");

            Console.WriteLine("Concat: s=" + sumResult.s + ", d=" + sumResult.f);
            Console.WriteLine($"\nresult = " + service1Client.Concat(sumResult.s, sumResult.f));

            Console.WriteLine("\nAdd: x=" + sumResult.k + ", y=8");
            Console.WriteLine($"\nresult = " + service1Client.Add(sumResult.k, 8));

            service1Client.Close();
            
            Console.ReadKey();
        }
    }
}
