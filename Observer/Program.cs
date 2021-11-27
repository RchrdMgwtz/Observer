using System;

namespace Observer
{
    internal static class Program
    {
        private static void Main()
        {
            var observer = new Observer();
            observer.Start();
            
            Console.ReadLine();
        }
    }
}