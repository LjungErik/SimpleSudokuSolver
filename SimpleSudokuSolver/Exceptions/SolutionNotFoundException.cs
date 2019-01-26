using System;

namespace SimpleSudokuSolver.Exceptions
{
    public class SolutionNotFoundException : Exception
    {
        public SolutionNotFoundException():
            base("No solution could be found")
        { }
    }
}
