using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSudokuSolver.Exceptions
{
    public class InvalidSudokuException : Exception
    {
        public InvalidSudokuException() :
            base("Invalid Sudoku Puzzle") {
        }
    }
}
