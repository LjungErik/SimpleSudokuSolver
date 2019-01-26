using SimpleSudokuSolver.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSudokuSolver.Algorithms
{
    public class BacktrackingAlgorithm
    {
        private static int EMPTY_CELL = 0;

        public int[,] SolveSudukoPuzzle(int[,] puzzle)
        {
            if (puzzle.GetLength(0) != 9 || puzzle.GetLength(1) != 9)
            {
                //Only allow 9x9 sudukos
                throw new InvalidSudokuException();
            }

            var puzzleToSolve = (int[,])puzzle.Clone();

            var prevChangedCells = new Stack<SudokuCell>();

            var currentCell = GetNextUnsolvedCell(puzzleToSolve, 0, 0);
            while(currentCell != null)
            {
                int row = currentCell.Row;
                int col = currentCell.Column;

                if(puzzleToSolve[row,col] == 9)
                {
                    if(prevChangedCells.Count == 0)
                    {
                        //If there are no previous cells
                        //then it is not possible to find a solution
                        throw new SolutionNotFoundException();
                    }

                    puzzleToSolve[row, col] = 0;
                    currentCell = prevChangedCells.Pop();
                }
                else
                {
                    puzzleToSolve[row, col] += 1;

                    if(IsValid(puzzleToSolve, row, col))
                    {
                        prevChangedCells.Push(currentCell);
                        currentCell = GetNextUnsolvedCell(puzzleToSolve, row, col);
                    }
                }
            }

            return puzzleToSolve;
        }

        #region Helper functions

        private SudokuCell GetNextUnsolvedCell(int[,] puzzle, int row, int col)
        {
            int nextRow = row;
            int nextCol = col;

            while(nextRow < 9)
            {
                if (puzzle[nextRow, nextCol] == EMPTY_CELL)
                {
                    return new SudokuCell(nextRow, nextCol);
                }

                nextRow = nextRow + ((nextCol + 1) / 9);
                nextCol = (nextCol + 1) % 9;
            }

            return null;
        }

        private bool IsValid(int[,] puzzle, int row, int col)
        {
            return 
                IsRowValid(puzzle, row, col) &&
                IsColumnValid(puzzle, row, col) &&
                IsBoxValid(puzzle, row, col);
        }

        private bool IsColumnValid(int[,] puzzle, int row, int col)
        {
            int value = puzzle[row, col];

            for (int i = 0; i < 9; i++)
            {
                if (i != row && puzzle[i, col] == value)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsRowValid(int[,] puzzle, int row, int col)
        {
            int value = puzzle[row, col];

            for (int i = 0; i < 9; i++)
            {
                if (i != col && puzzle[row, i] == value)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsBoxValid(int[,] puzzle, int row, int col)
        {
            int value = puzzle[row, col];

            int boxTopRow = (row / 3) * 3;
            int boxLeftCol = (col / 3) * 3;

            for (int i = boxTopRow; i < boxTopRow + 3; i++)
            {
                for (int j = boxLeftCol; j < boxLeftCol + 3; j++)
                {
                    if (i != row && j != col && puzzle[i, j] == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion 
    }
}
