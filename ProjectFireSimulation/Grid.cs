using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProjectFireSimulation
{
    class Grid
    {
        private int flagBoundry = 0;            //To check if the burning trees are touching the boundry of grid.
        private int flagFire = 0;               //To keep track of fire.    
        private Queue myQueue = new Queue();    //To keep track of which trees were burned during each time step.
        private Queue countQ = new Queue();     //To keep track of how many trees were burned during each time step.
        // private Pair p=new Pair();
        private int[,] data = new int[21, 21];//declaration of 2D array  
                                              // static char[,] forest;
                                              // public bool checkIFTouchingBoundry();
        public double getProbabilty()
        {
            /*
             If a tree is present and at least one of the neighbours 
             is burning it may or may not catch fire with a probability of 
             50% (use a random number generator to determine this).
             */
            Random random = new Random();
            return random.Next(0, 100);
        }
        public Grid(int x = 0, int y = 0)
        {
            
            //Function to make grid.              

            Pair pr = new Pair();
            pr.r = 10;
            pr.c = 10;
            myQueue.Enqueue(pr);
            int i, j;

            for (i = 0; i < 21; i++)
            {
                for (j = 0; j < 21; j++)
                {
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)

                    {
                        data[i, j] = 2;
                        //  Console.Write('2');
                    }
                    else
                    {
                        data[i, j] = 0;
                        //Console.Write('0');
                        //Console.WriteLine(data[i, j]);
                    }
                }
                // Console.WriteLine();
            }
            data[x / 2, y / 2] = 1;
        }

        public void displayForrest()
        {
            //Funtion to display the grid.

            int i, j;
            for (i = 0; i < 21; i++)
            {
                for (j = 0; j < 21; j++)
                {
                    if (data[i, j] == 0)
                        Console.Write("& ");
                    else if (data[i, j] == 1)
                        Console.Write("X ");
                    else
                        Console.Write("  ");
                    // Console.Write(data[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void evolve()
        {

            int count = 1;
            for (int i = 0; i <= 20; i++)
            {
                for (int j = 0; j <= 20; j++)
                {
                    Pair p = new Pair();
                    if (data[i, j] == 0)
                    {
                        if ((j - 1 >= 0 && data[i, j - 1] == 1) || (j + 1 < 21 && data[i, j + 1] == 1) || (i + 1 < 21 && data[i + 1, j] == 1) || (i - 1 >= 0 && data[i - 1, j] == 1))
                        {
                            double prob = getProbabilty();
                            if (prob >= 50)
                            {
                                data[i, j] = 1;

                                p.r = i;
                                p.c = j;
                                myQueue.Enqueue(p);
                                // Console.WriteLine(p.r + "-" + p.c);
                                count++;
                            }
                        }
                    }
                }
            }
            countQ.Enqueue(count);


        }
        public bool checkIFTouchingBoundry()
        {
            int countOFFire1 = 0;
            int countOfFire2 = 0;

            for (int i = 0; i <= 20; i++)
            {
                if (data[i, 19] == 1)
                {


                    countOFFire1++;
                }
            }
            for (int i = 0; i <= 20; i++)
            {
                if (data[19, i] == 1)
                {


                    countOfFire2++;

                }
            }

            if (countOFFire1 >= 7 && countOfFire2 >= 7)
            {
                Console.WriteLine("*********************************");
                return true;
            }
            else
                return false;

        }
        public void turnFire()
        {

            int i = (int)countQ.Dequeue();
            if (myQueue.Count > 0)
            {
                while (myQueue.Count > 0 && i > 0)
                {

                    Pair temp = (Pair)myQueue.Dequeue();
                    int row = temp.r;
                    int col = temp.c;
                    data[temp.r, temp.c] = 2;
                    if (col - 1 >= 0 && data[row, col - 1] == 0)
                    {
                        data[row, col - 1] = 1;
                        Pair prr = new Pair();
                        prr.r = row;
                        prr.c = col - 1;
                        myQueue.Enqueue(prr);
                    }
                    if (col + 1 < 21 && data[row, col + 1] == 0)
                    {
                        data[row, col + 1] = 1;
                        Pair prr = new Pair();
                        prr.r = row;
                        prr.c = col + 1;
                        myQueue.Enqueue(prr);
                    }
                    if (row + 1 < 21 && data[row + 1, col] == 0)
                    {
                        data[row + 1, col] = 1;
                        Pair prr = new Pair();
                        prr.r = row + 1;
                        prr.c = col;
                        myQueue.Enqueue(prr);
                    }
                    if (row - 1 >= 0 && data[row - 1, col] == 0)
                    {
                        data[row - 1, col] = 1;
                        Pair prr = new Pair();
                        prr.r = row - 1;
                        prr.c = col;
                        myQueue.Enqueue(prr);
                    }
                    i--;
                    // Console.WriteLine(temp.r + " , " + temp.c);
                }
            }
            else if (flagFire == 0)
            {
                for (int m = 0; m < 21; m++)
                {
                    for (int n = 0; n < 21; n++)
                    {
                        if (data[m, n] == 0)
                        {
                            data[m, n] = 1;
                        }
                    }
                }
                flagFire = 1;

            }
            else
            {
                for (int m = 0; m < 21; m++)
                {
                    for (int n = 0; n < 21; n++)
                    {
                        if (data[m, n] == 1)
                        {
                            data[m, n] = 2;
                        }
                    }
                }
            }
        }
        public void startSimulation()
        {
            evolve();
            if (flagBoundry == 1 || checkIFTouchingBoundry() == true)
            {
                flagBoundry = 1;

                turnFire();
            }

            displayForrest();
            Console.WriteLine();
            
        }


    }
}
