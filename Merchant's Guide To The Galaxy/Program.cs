using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merchant_s_Guide_To_The_Galaxy.Rules;

namespace Merchant_s_Guide_To_The_Galaxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var butler = new GalaxyButler();
            try
            {
                butler.ProcessInput("../../input.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }



}
