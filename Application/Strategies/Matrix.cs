using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Auxiliary;
using Application.Interface;

namespace Application.Strategies
{
    public class Matrix : IMatrix
    {
        private char[,] grid;
        public char[,] Grid
        {
            get
            {
                return grid;
            }
        }
        public char[,] CreateMatrix()
        {
            grid = new char[64, 64];
            string[] wordsInStream  = new string[3];
            wordsInStream[0] = "JOB";
            wordsInStream[1] = "BEYOND";
            wordsInStream[2] = "NETCORE";

            PopulateGridWords(wordsInStream, Grid);
            PopulateEmptyElements(grid);

            return grid;
        }
        private void PopulateGridWords(string[] words, char[,] grid)
        {
            bool wordPlaced = false;
            int numberWordsToPlace = Helper.CountElements(words);

            // iterate Words to place
            for (int wordCurrent = 0; wordCurrent < numberWordsToPlace; wordCurrent++)
            {
                wordPlaced = false;
                while (!wordPlaced)
                {
                    // Get random starting point for word
                    GridPosition coord = new GridPosition(Helper.Random(0, grid.GetLength(0) - 1), Helper.Random(0, grid.GetLength(1) - 1));
                    if (PlaceWordInGrid(coord, words[wordCurrent], grid))
                    {
                        wordPlaced = true;
                    }
                }
            }
        }
        private bool PlaceWordInGrid(GridPosition pos, string word, char[,] grid)
        {
            int x =  pos.Row;
            int y = pos.Col;

            // elements represent placements options, 0 == left->right, 1 = right->left, etc. (in order presented below)
            int[] placementOptions = new int[2] { 2, 2};
            int placementOption = 2;
            bool haveOptions = false;

            for (int counter = 0; counter < word.Length; counter++)
            {
                // If point empty or point contains same letter word's current character
                if (grid[x, y] == '\0' | grid[x, y] == word[0])
                {
                    if (SpaceRight(word, pos, grid))
                    {
                        placementOptions[0] = 1;
                        haveOptions = true;
                    }

                    if (SpaceDown(word, pos, grid))
                    {
                        placementOptions[1] = 2;
                        haveOptions = true;
                    }



                    if (haveOptions)
                    {
                        while (placementOption == 2)
                        {
                            placementOption = placementOptions[Helper.Random(0, placementOptions.Length - 1)];
                        }

                        switch (placementOption)
                        {
                            case 1:
                                PlaceWordRight(word, pos, grid);
                                break;                          
                            case 2:
                                PlaceWordDown(word, pos, grid);
                                break;                                                  
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private void PopulateEmptyElements(char[,] grid)
        {
            for (int counterRow = 0; counterRow < grid.GetLength(0); counterRow++)
            {
                for (int counterCol = 0; counterCol < grid.GetLength(1); counterCol++)
                {
                    if (grid[counterRow, counterCol] == '\0')
                    {
                        grid[counterRow, counterCol] = Helper.Random('a', 'z');
                    }
                }
            }
        }

        private void PlaceWordRight(string word, GridPosition pos, char[,] grid)
        {
            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[pos.Row, pos.Col + counter] = word[counter];
            }
        } // place word left -> right
       
        private void PlaceWordDown(string word, GridPosition pos, char[,] grid)
        {
            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[pos.Row + counter, pos.Col] = word[counter];
            }
        } // place word up -> down
       
       
        private bool SpaceRight(string word, GridPosition pos, char[,] grid)
        {
            if ((grid.GetLength(0)) - pos.Col >= word.Length)
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[pos.Row, pos.Col + counter] != '\0' && grid[pos.Row, pos.Col + counter] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
      
        private bool SpaceDown(string word, GridPosition pos, char[,] grid)
        {
            if ((grid.GetLength(0)) - pos.Row >= word.Length)
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[pos.Row + counter, pos.Col] != '\0' && grid[pos.Row + counter, pos.Col] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space up -> down
       
    }
}
