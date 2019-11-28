using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWS_5_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Simplex.Service1Client service1Client = new Simplex.Service1Client();

            Console.WriteLine("A1 { f = 1.1f, k = 1, s = 1 }, A2 { f = 5.3f, k = 2, s = 231 }");
            var sumResult = service1Client.Sum(new Simplex.A { f = 1.1f, k = 1, s = "1" }, new Simplex.A { f = 5.3f, k = 2, s = "231" });
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
