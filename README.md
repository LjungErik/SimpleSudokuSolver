# SimpleSudokuSolver
A basic sudoku solver based on a backtracking algorithm

Resources used during the creation of this solution:
https://en.wikipedia.org/wiki/Sudoku_solving_algorithms
http://pi.math.cornell.edu/~mec/Summer2009/meerkamp/Site/Solving_any_Sudoku_I.html

# Issues with using the currenct solution
It is a brute force solution, which means that it will try all combinations until it finds a solution.
The issue with this is that there are certian sudoku puzzles that it will spend a significant amount of time trying to solve.
A example of this can be found in the test: `SolveSudokuPuzzle_HardSudokuProblem_ForBacktracking_SolveSolution`. 

In this test the solution for the top row is **987654321**, but the puzzle provides no values for the top row. Which in turn means that the algorithm will spend a significant amount of time trying to work its way up to **987654321**.

More info about the weakness with backtracking can be found [here](https://en.wikipedia.org/wiki/Sudoku_solving_algorithms)
