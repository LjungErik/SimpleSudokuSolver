using SimpleSudokuSolver.Algorithms;
using SimpleSudokuSolver.Exceptions;
using System;

namespace SimpleSudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            /* replace sudokuPuzzle with the puzzle you want to solve */
            /* emåty cells are represented by a 0 */
            int[,] sudokuPuzzle =
            {  { 0,0,0,0,0,0,0,0,0 },
               { 8,0,0,0,2,0,0,0,5 },
               { 0,0,0,0,0,6,2,4,0 },
               { 0,3,8,0,0,7,1,0,0 },
               { 2,0,4,0,0,0,3,0,9 },
               { 0,0,7,4,0,0,5,2,0 },
               { 0,7,2,5,0,0,0,0,0 },
               { 6,0,0,0,8,0,0,0,1 },
               { 0,0,0,0,0,0,0,0,0 }  };

            var algo = new BacktrackingAlgorithm();

            try
            {
                var solution = algo.SolveSudukoPuzzle(sudokuPuzzle);
                PrintSolution(sudokuPuzzle, solution);
            } catch (InvalidSudokuException)
            {
                Console.WriteLine("Sudoku puzzle is of invalid size");
            } catch(SolutionNotFoundException)
            {
                Console.WriteLine("Solution cannot be found for the sudoku puzzle");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private static void PrintSolution(int[,] puzzle, int[,] solution)
        {
            Console.WriteLine("Puzzle: ");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($" {puzzle[i, j]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Found solution:");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($" {solution[i, j]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("SOLVED!");
        }
    }
}
