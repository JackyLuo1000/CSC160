using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDemo
{
    public delegate void DemoDelegate();

    public static class Publisher
    {
        public static event DemoDelegate Demo;

        public static void RunDelegate()
        {
            if(Demo != null)
            {
                Demo();
            }
            else
            {
                Console.WriteLine("Nothing to invoke");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Invoke Delegate
            Publisher.RunDelegate();

            //Subscribe
            Publisher.Demo += Method1;
            Publisher.Demo += Method1;
            Publisher.Demo += Method1;
            
            //Invoke Delegate
            Publisher.RunDelegate();

            //Break the delegate
            //Publisher.Demo = null;

            //Invoke Delegate
            Publisher.RunDelegate();

        }

        public static void Method1()
        {
            Console.WriteLine("Method1 was invoked");
        }
    }
}
