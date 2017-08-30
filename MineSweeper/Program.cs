using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {

            //Int array of X to represent each field (rows * columns)
            //Y bombs to be randomly placed in fields between 0-X
            //Bombs represented by -1
            //Each field to be allocated a number between 0-8 to represent the number of bombs in its direct vicinity (left, right, above, below, diagonally)
            //Fields at a corner can have 0-3
            //Fields at the edge (but not a corner) can have 0-5
            //Fields are counted row# * column#
            //Bombs in vicinity are counted for each combination of row# and column# +/- 1

            // number of rows, columns and total fields
            int columns = 5;
            int rows = 5;
            int fields = rows * columns;

            //number of bombs
            //TODO: must be somewhat less than number of total fields?
            int bombs = 5;

            // array with total number of fields
            var area = new int[fields];

            //randomly place bombs
            int[] bombpositions = RandomlyPlaceBombsInFields(ref area, bombs);
            AddBombCountToFields(ref area, bombpositions, rows, columns, bombs);

            //write out array to console
            //nested for loops for drawing rows and columns
            //loop rows, but for each row you loop the columns
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    int p = (r * rows) + c;
                    int f = area[p];
                    // add some padding so we can see what's going on!
                    // a number gets three spaces total, so the - for bombs will take up a space!
                    if (f == -1)
                        Console.Write(" ");
                    else
                        Console.Write("  ");
                    Console.Write(f.ToString() + " |");
                }
                Console.WriteLine();
            }

            //pause
            Console.ReadKey();
        }

        static void AddBombCountToFields(ref int[] area, int[] bombPositions, int rows, int columns, int bombs)
        {
            // loop bomb positions to be able to add hints
            foreach (int pos in bombPositions)
            {
                //the position is the place in the array where the bomb has been placed.
                //the integer resulting from dividing by number of columns gives you its row number
                //the column number is position less row number times total rows

                int r = pos / columns;
                int c = pos - (r * rows);

                //mini-loop the surrounding fields
                int prevRow = (r - 1 < 0) ? r : (r - 1);
                int prevCol = (c - 1 < 0) ? c : (c - 1);
                int nextRow = (r + 1 >= rows) ? r : (r + 1);
                int nextCol = (c + 1 >= columns) ? c : (c + 1);

                for (int i = prevRow; i <= nextRow; i++)
                {
                    for (int j = prevCol; j <= nextCol; j++)
                    {
                        int m = (i * rows) + j;
                        if (area[m] >= 0)
                            area[m]++;
                    }
                }

            }
        }

        /// <summary>
        /// Places specified number of bombs into the array at randomly generated places
        /// Bombs represented by -1
        /// </summary>
        /// <param name="area"></param>
        /// <param name="bombs"></param>
        static int[] RandomlyPlaceBombsInFields(ref int[] area, int bombs)
        {
            int[] bombPositions = new int[bombs];
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < bombs; i++)
            {
                int b = r.Next(0, area.Length-1);

                // if there is already a bomb here, just try again
                if (area[b] == -1)
                    i--;
                else
                {
                    area[b] = -1;
                    bombPositions[i] = b;
                }
            }
            return bombPositions;            
        }
    }
}
