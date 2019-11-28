using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(SyndicationServiceLibrary.Feed));
            host.Open();
            Console.WriteLine("Host Open");
            string s = Console.ReadLine();
                 
        }
    }
}
