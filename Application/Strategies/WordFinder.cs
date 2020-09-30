using Application.Interface;
using System;
using System.Collections.Generic;

namespace Application.Strategies
{
    public class WordFinder : IWordFinder
    {

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            List<string> wordsToFinder = new List<string>()
            {
                "Beyond","Job","NetCore"
            };
            var wordString = string.Join("", wordstream);
            List<string> wordsFinder = new List<string>();
            char[,] matrix = new char[64, 64];
            int r = 0;
            int c = 0;
            foreach (String line in wordstream)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] values = line.Split(' ');

                    foreach (var item in values)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            matrix[r, c] = Convert.ToChar(item);
                            r++;
                        }
                    }
                    r = 0;
                    c++;
                }
            }
            foreach (var word in wordsToFinder)
            {
                if(patternSearch(matrix, word.ToUpper()))
                {
                    wordsFinder.Add(word);
                }
            }
            return wordsFinder;
        }

        // Rows and columns in given grid 
        static int R = 64, C = 64;

        // For searching in all 8 direction 
        static int[] x = { -1, -1, -1, 0, 0, 1, 1, 1 };
        static int[] y = { -1, 0, 1, -1, 1, -1, 0, 1 };

        // This function searches in all 8-direction 
        // from point (row, col) in grid[, ] 
        static bool search2D(char[,] grid, int row,
                            int col, String word)
        {
            // If first character of word doesn't match 
            // with given starting point in grid. 
            if (grid[row, col] != word[0])
            {
                return false;
            }

            int len = word.Length;

            // Search word in all 8 directions 
            // starting from (row, col) 
            for (int dir = 0; dir < 8; dir++)
            {
                // Initialize starting point 
                // for current direction 
                int k, rd = row + x[dir], cd = col + y[dir];

                // First character is already checked, 
                // match remaining characters 
                for (k = 1; k < len; k++)
                {
                    // If out of bound break 
                    if (rd >= R || rd < 0 || cd >= C || cd < 0)
                    {
                        break;
                    }

                    // If not matched, break 
                    if (grid[rd, cd] != word[k])
                    {
                        break;
                    }

                    // Moving in particular direction 
                    rd += x[dir];
                    cd += y[dir];
                }

                // If all character matched, then value of k 
                // must be equal to length of word 
                if (k == len)
                {
                    return true;
                }
            }
            return false;
        }

        // Searches given word in a given 
        // matrix in all 8 directions 
        static bool patternSearch(char[,] grid,
                                String word)
        {
            // Consider every point as starting 
            // point and search given word 
            for (int row = 0; row < 64; row++)
            {
                for (int col = 0; col < 64; col++)
                {
                    if (search2D(grid, row, col, word))
                    {
                        Console.WriteLine("pattern found at " + row + ", " + col);
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
