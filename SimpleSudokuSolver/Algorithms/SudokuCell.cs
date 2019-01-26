using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSudokuSolver.Algorithms
{
    public class SudokuCell
    {
        public int Row { get; }
        public int Column { get; }

        public SudokuCell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
