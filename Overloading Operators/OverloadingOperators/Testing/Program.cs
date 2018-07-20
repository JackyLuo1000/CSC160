using OverloadingOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction a = new Fraction(0, 1, 2);
            Fraction b = new Fraction(2, 0, 6);
            Console.WriteLine(a - b);
        }
    }
}
