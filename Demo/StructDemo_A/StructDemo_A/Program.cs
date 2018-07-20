using ExtMethods;
using StructDemo_A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructDemo_A
{
    public class Program
    {
        bool? isAttending;

        public static void Main(string[] args)
        {
            //int max = int.MaxValue;
            //int num = (uint)max + 1;

            //Console.WriteLine($"Num = {num}");

            Dragon smaug = new Dragon() { Name = "Smaug, The Terrible", Age = 171};
            Dragon otherDrgn = smaug;

            otherDrgn.Name = "Phteven";
            otherDrgn.Age = 10;
            Dragon blueEyes = new Dragon() { Name = "Blue Eyes White Dragon", Age = 1000 };
            Dragon[] dragons = { smaug, otherDrgn, blueEyes };
            dragons.Print();
            //Console.WriteLine(smaug);
            //otherDrgn = null;
            //Console.WriteLine(otherDrgn);
        }
    }
}
