﻿using NUnit.Framework;
using SimpleSudokuSolver.Algorithms;
using SimpleSudokuSolver.Exceptions;

namespace SimpleSudokuSolver.Tests.Algorithms
{
    public class BacktrackingAlgorithmTests
    {
        private BacktrackingAlgorithm _algorithm;

        #region SudokuPuzzles and Solutions

        //Puzzels and solutions taken from https://krazydad.com//sudoku/sfiles/KD_Sudoku_ST_8_v1.pdf
        private int[][,] sudokuPuzzels = new[]
        {
            new int[,]
            {  { 0,0,0,0,0,0,0,0,0 },
               { 8,0,0,0,2,0,0,0,5 },
               { 0,0,0,0,0,6,2,4,0 },
               { 0,3,8,0,0,7,1,0,0 },
               { 2,0,4,0,0,0,3,0,9 },
               { 0,0,7,4,0,0,5,2,0 },
               { 0,7,2,5,0,0,0,0,0 },
               { 6,0,0,0,8,0,0,0,1 },
               { 0,0,0,0,0,0,0,0,0 }  },
            new int[,]
            {  { 0,0,0,0,6,0,0,5,0 },
               { 8,0,1,0,0,7,2,0,0 },
               { 2,7,0,8,0,0,0,0,0 },
               { 0,0,0,5,0,0,0,0,2 },
               { 0,6,0,0,0,0,0,8,0 },
               { 9,0,0,0,0,1,0,0,0 },
               { 0,0,0,0,0,3,0,1,5 },
               { 0,0,5,7,0,0,6,0,3 },
               { 0,8,0,0,9,0,0,0,0 }  },
            new int[,]
            {  { 3,0,5,0,9,0,0,0,0 },
               { 0,6,0,0,0,4,0,5,0 },
               { 0,0,0,0,0,8,3,0,0 },
               { 6,0,0,0,0,2,0,0,0 },
               { 0,0,2,9,0,5,1,0,0 },
               { 0,0,0,4,0,0,0,0,6 },
               { 0,0,4,2,0,0,0,0,0 },
               { 0,2,0,3,0,0,0,7,0 },
               { 0,0,0,0,1,0,4,0,5 }  },
            new int[,]
            {  { 0,0,0,1,4,0,0,8,7 },
               { 0,0,4,0,0,0,3,0,6 },
               { 0,0,0,0,0,5,0,0,0 },
               { 4,0,0,3,0,0,0,0,1 },
               { 0,3,0,0,0,0,0,7,0 },
               { 8,0,0,0,0,6,0,0,9 },
               { 0,0,0,5,0,0,0,0,0 },
               { 2,0,6,0,0,0,5,0,0 },
               { 5,7,0,0,1,9,0,0,0 }  },
            new int[,]
            {  { 0,7,0,0,0,0,0,0,0 },
               { 0,0,0,0,2,0,4,9,0 },
               { 0,0,6,0,0,4,7,3,0 },
               { 0,0,0,0,0,0,9,0,8 },
               { 0,0,0,6,5,3,0,0,0 },
               { 6,0,1,0,0,0,0,0,0 },
               { 1,5,9,3,0,0,2,0,0 },
               { 0,6,7,0,9,0,0,0,0 },
               { 0,0,0,0,0,0,0,4,0 }  },
            new int[,]
            {  { 0,5,0,0,0,2,0,0,3 },
               { 0,0,6,0,0,9,2,5,0 },
               { 4,0,7,0,0,0,0,0,0 },
               { 3,1,0,0,4,0,0,0,0 },
               { 0,0,0,0,0,0,0,0,0 },
               { 0,0,0,0,3,0,0,2,1 },
               { 0,0,0,0,0,0,1,0,4 },
               { 0,6,8,9,0,0,7,0,0 },
               { 7,0,0,5,0,0,0,8,0 }  },
            new int[,]
            {  { 0,0,8,0,5,4,0,0,0 },
               { 0,0,0,0,0,0,0,4,2 },
               { 0,3,0,0,8,0,9,0,0 },
               { 3,0,0,0,0,9,0,0,8 },
               { 0,0,0,0,7,0,0,0,0 },
               { 2,0,0,4,0,0,0,0,6 },
               { 0,0,7,0,1,0,0,3,0 },
               { 1,2,0,0,0,0,0,0,0 },
               { 0,0,0,6,9,0,5,0,0 }  },
            new int[,]
            {  { 7,1,0,0,0,5,0,6,0 },
               { 0,0,4,0,0,0,7,0,0 },
               { 0,0,0,7,9,0,0,0,0 },
               { 0,6,0,0,0,0,2,0,7 },
               { 0,0,0,9,0,8,0,0,0 },
               { 5,0,3,0,0,0,0,1,0 },
               { 0,0,0,0,5,3,0,0,0 },
               { 0,0,8,0,0,0,3,0,0 },
               { 0,7,0,2,0,0,0,4,9 }  }
        };

        private int[][,] sudukoSolutions = new[]
        {
            new int[,]
            {  { 4,2,6,9,3,5,8,1,7 },
               { 8,1,3,7,2,4,6,9,5 },
               { 7,9,5,8,1,6,2,4,3 },
               { 9,3,8,2,5,7,1,6,4 },
               { 2,5,4,1,6,8,3,7,9 },
               { 1,6,7,4,9,3,5,2,8 },
               { 3,7,2,5,4,1,9,8,6 },
               { 6,4,9,3,8,2,7,5,1 },
               { 5,8,1,6,7,9,4,3,2 }  },
            new int[,]
            {  { 3,4,9,1,6,2,7,5,8 },
               { 8,5,1,3,4,7,2,6,9 },
               { 2,7,6,8,5,9,1,3,4 },
               { 7,1,8,5,3,6,9,4,2 },
               { 5,6,2,9,7,4,3,8,1 },
               { 9,3,4,2,8,1,5,7,6 },
               { 6,9,7,4,2,3,8,1,5 },
               { 4,2,5,7,1,8,6,9,3 },
               { 1,8,3,6,9,5,4,2,7 }  },
            new int[,]
            {  { 3,4,5,6,9,1,7,8,2 },
               { 2,6,8,7,3,4,9,5,1 },
               { 7,1,9,5,2,8,3,6,4 },
               { 6,9,3,1,7,2,5,4,8 },
               { 4,8,2,9,6,5,1,3,7 },
               { 1,5,7,4,8,3,2,9,6 },
               { 8,7,4,2,5,9,6,1,3 },
               { 5,2,1,3,4,6,8,7,9 },
               { 9,3,6,8,1,7,4,2,5 }  },
            new int[,]
            {  { 3,6,5,1,4,2,9,8,7 },
               { 1,2,4,9,8,7,3,5,6 },
               { 7,8,9,6,3,5,1,2,4 },
               { 4,9,7,3,5,8,2,6,1 },
               { 6,3,2,4,9,1,8,7,5 },
               { 8,5,1,7,2,6,4,3,9 },
               { 9,4,8,5,6,3,7,1,2 },
               { 2,1,6,8,7,4,5,9,3 },
               { 5,7,3,2,1,9,6,4,8 }  },
            new int[,]
            {  { 5,7,4,9,3,1,6,8,2 },
               { 8,1,3,7,2,6,4,9,5 },
               { 2,9,6,5,8,4,7,3,1 },
               { 7,3,5,4,1,2,9,6,8 },
               { 9,4,8,6,5,3,1,2,7 },
               { 6,2,1,8,7,9,3,5,4 },
               { 1,5,9,3,4,8,2,7,6 },
               { 4,6,7,2,9,5,8,1,3 },
               { 3,8,2,1,6,7,5,4,9 }  },
            new int[,]
            {  { 8,5,9,6,7,2,4,1,3 },
               { 1,3,6,4,8,9,2,5,7 },
               { 4,2,7,3,5,1,8,9,6 },
               { 3,1,5,2,4,8,6,7,9 },
               { 6,7,2,1,9,5,3,4,8 },
               { 9,8,4,7,3,6,5,2,1 },
               { 5,9,3,8,2,7,1,6,4 },
               { 2,6,8,9,1,4,7,3,5 },
               { 7,4,1,5,6,3,9,8,2 }  },
            new int[,]
            {  { 6,9,8,2,5,4,3,1,7 },
               { 7,1,5,9,6,3,8,4,2 },
               { 4,3,2,7,8,1,9,6,5 },
               { 3,6,1,5,2,9,4,7,8 },
               { 5,8,4,1,7,6,2,9,3 },
               { 2,7,9,4,3,8,1,5,6 },
               { 9,5,7,8,1,2,6,3,4 },
               { 1,2,6,3,4,5,7,8,9 },
               { 8,4,3,6,9,7,5,2,1 },  },
            new int[,]
            {  { 7,1,9,3,4,5,8,6,2 },
               { 2,5,4,8,6,1,7,9,3 },
               { 8,3,6,7,9,2,4,5,1 },
               { 9,6,1,5,3,4,2,8,7 },
               { 4,2,7,9,1,8,5,3,6 },
               { 5,8,3,6,2,7,9,1,4 },
               { 1,9,2,4,5,3,6,7,8 },
               { 6,4,8,1,7,9,3,2,5 },
               { 3,7,5,2,8,6,1,4,9 }, }
        };

        #endregion

        [SetUp]
        public void Setup()
        {
            _algorithm = new BacktrackingAlgorithm();
        }

        [Test]
        public void SolveSudokuPuzzle_InvalidSudoku10x9_ThrowException()
        {
            int[,] invalid_10x9 = {
               { 0,0,6,0,0,8,5,0,0 },
               { 0,0,0,0,7,0,6,1,3 },
               { 0,0,0,0,0,0,0,0,9 },
               { 0,0,0,0,9,0,0,0,1 },
               { 0,0,1,0,0,0,8,0,0 },
               { 4,0,0,5,3,0,0,0,0 },
               { 1,0,7,0,5,3,0,0,0 },
               { 0,5,0,0,6,4,0,0,0 },
               { 3,0,0,1,0,0,0,6,0 },
               { 0,0,0,0,0,0,0,0,0 }
            };

            Assert.Throws<InvalidSudokuException>(() => _algorithm.SolveSudukoPuzzle(invalid_10x9));
        }

        [Test]
        public void SolveSudokuPuzzle_InvalidSudoku9x10_ThrowException()
        {
            int[,] invalid_9x10 = {
               { 0,0,6,0,0,8,5,0,0,0 },
               { 0,0,0,0,7,0,6,1,3,0 },
               { 0,0,0,0,0,0,0,0,9,0 },
               { 0,0,0,0,9,0,0,0,1,0 },
               { 0,0,1,0,0,0,8,0,0,0 },
               { 4,0,0,5,3,0,0,0,0,0 },
               { 1,0,7,0,5,3,0,0,0,0 },
               { 0,5,0,0,6,4,0,0,0,0 },
               { 3,0,0,1,0,0,0,6,0,0 },
            };

            Assert.Throws<InvalidSudokuException>(() => _algorithm.SolveSudukoPuzzle(invalid_9x10));
        }

        [Test]
        public void SolveSudokuPuzzle_InvalidSudoku6x6_ThrowException()
        {
            int[,] invalid_6x6 = {
               { 0,0,0,0,0,0 },
               { 0,0,0,0,0,0 },
               { 0,0,0,0,0,0 },
               { 0,0,0,0,0,0 },
               { 0,0,0,0,0,0 },
               { 0,0,0,0,0,0 },
            };

            Assert.Throws<InvalidSudokuException>(() => _algorithm.SolveSudukoPuzzle(invalid_6x6));
        }

        [Test]
        public void SolveSudokuPuzzle_UnSolvableSuduko_ThrowException()
        {
            int[,] unsolvable =
            {  { 2,0,0,0,0,0,0,0,0 },
               { 8,0,0,0,2,0,0,0,5 },
               { 0,0,0,0,0,6,2,4,0 },
               { 0,3,8,0,0,7,1,0,0 },
               { 2,0,4,0,0,0,3,0,9 },
               { 0,0,7,4,0,0,5,2,0 },
               { 0,7,2,5,0,0,0,0,0 },
               { 6,0,0,0,8,0,0,0,1 },
               { 0,0,0,0,0,0,0,0,0 }  };

            Assert.Throws<SolutionNotFoundException>(() => _algorithm.SolveSudukoPuzzle(unsolvable));
        }

        [Test]
        public void SolveSudokuPuzzle_CollectionOfSudokus_ProvideValidSolution()
        {
            for (int i = 0; i < sudokuPuzzels.Length; i++)
            {
                var solution = _algorithm.SolveSudukoPuzzle(sudokuPuzzels[i]);
                Assert.AreEqual(sudukoSolutions[i], solution, $"Invalid solution to sudoku puzzle {i}");
            }
        }

        /// <summary>
        /// This tests contains a sudoku problem that 
        /// exploits weaknesses with the backtracking solution,
        /// more info found here: https://en.wikipedia.org/wiki/Sudoku_solving_algorithms
        /// </summary>
        [Explicit]
        [Test]
        public void SolveSudokuPuzzle_HardSudokuProblem_ForBacktracking_SolveSolution()
        {
            int[,] hardToSolve = {
               { 0,0,0,0,0,0,0,0,0 },
               { 0,0,0,0,0,3,0,8,5 },
               { 0,0,1,0,2,0,0,0,0 },
               { 0,0,0,5,0,7,0,0,0 },
               { 0,0,4,0,0,0,1,0,0 },
               { 0,9,0,0,0,0,0,0,0 },
               { 5,0,0,0,0,0,0,7,3 },
               { 0,0,2,0,1,0,0,0,0 },
               { 0,0,0,0,4,0,0,0,9 },
            };

            int[,] expectedSolution =
            {
               { 9,8,7,6,5,4,3,2,1 },
               { 2,4,6,1,7,3,9,8,5 },
               { 3,5,1,9,2,8,7,4,6 },
               { 1,2,8,5,3,7,6,9,4 },
               { 6,3,4,8,9,2,1,5,7 },
               { 7,9,5,4,6,1,8,3,2 },
               { 5,1,9,2,8,6,4,7,3 },
               { 4,7,2,3,1,9,5,6,8 },
               { 8,6,3,7,4,5,2,1,9 } };

            var solution = _algorithm.SolveSudukoPuzzle(hardToSolve);

            Assert.AreEqual(expectedSolution, solution);
        }
    }
}
