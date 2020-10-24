using System;

namespace ProjectFireSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Making a grid of 21x21 grid of cells, 
            where all cells contain a tree except
            for a 1-cell thick perimeter boundary 
            layer of empty cells and then displaying it.*/

            Grid obj = new Grid(21, 21); 
            obj.displayForrest();
            //Console.WriteLine();
            Console.WriteLine("Press enter to watch next burning step or press any other key to exit the program");
            ConsoleKeyInfo keyEntered;
            while (true)
            {
                keyEntered = Console.ReadKey();
                if (keyEntered.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    obj.startSimulation();
                    Console.WriteLine("Press enter to watch next burning step or press any other key to exit the program");
                }
                else
                {
                    return;
                }
            }

        }
    }
}
