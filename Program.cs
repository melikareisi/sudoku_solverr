using System;

namespace SudokuSolver
{
    public class Sudoku
    {
        private int[,] grid;

        public int[,] Grid
        {
            get { return grid; }
            set { grid = value; }
        }

        public Sudoku(int[,] initialGrid)
        {
            grid = initialGrid;
        }

        public bool SolveSudoku()
        {
            return RecursiveSolve(0, 0);
        }

        private bool RecursiveSolve(int row, int col)
        {
            // اگر به آخر رسیدیم، یعنی سودوکو حل شده است
            if (row == 9)
            {
                return true;
            }
            // اگر به آخر ستون رسیدیم، به ستون اول سطر بعدی برو
            if (col == 9)
            {
                return RecursiveSolve(row + 1, 0);
            }
            // اگر در این خانه عددی بود، به خانه بعدی برو
            if (grid[row, col] != 0)
            {
                return RecursiveSolve(row, col + 1);
            }

            // سعی در قرار دادن اعداد در سلول جاری
            for (int num = 1; num <= 9; num++)
            {
                if (IsValidLocation(row, col, num))
                {
                    grid[row, col] = num;
                    if (RecursiveSolve(row, col + 1))
                    {
                        return true;
                    }
                    grid[row, col] = 0;
                }
            }
            return false;
        }

        private bool IsValidLocation(int row, int col, int num)
        {
            // بررسی تکراری بودن در سطر و ستون
            for (int i = 0; i < 9; i++)
            {
                if (grid[row, i] == num || grid[i, col] == num)
                {
                    return false;
                }
            }

            // بررسی تکراری بودن در مربع 3x3
            int boxRow = (row / 3) * 3;
            int boxCol = (col / 3) * 3;
            for (int i = boxRow; i < boxRow + 3; i++)
            {
                for (int j = boxCol; j < boxCol + 3; j++)
                {
                    if (grid[i, j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void DisplaySudokuGrid()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Solution:");
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write(grid[row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }

    class SudokuSolverProgram
    {
        static void Main(string[] args)
        {
            int[,] inputGrid = GetSudokuGridInput();
            Sudoku sudoku = new Sudoku(inputGrid);
            if (sudoku.SolveSudoku())
            {
                sudoku.DisplaySudokuGrid();
                Console.Beep(1000, 500);
                Console.Beep(1500, 2000);
            }
            else
            {
                Console.WriteLine("The Sudoku puzzle cannot be solved!");
            }
            Console.ReadLine();
        }

        static int[,] GetSudokuGridInput()
        {
            int[,] inputGrid = new int[9, 9];
            Console.WriteLine("Enter the Sudoku puzzle:");
            for (int row = 0; row < 9; row++)
            {
                string[] rowValues = Console.ReadLine().Split(' ');
                for (int col = 0; col < 9; col++)
                {
                    inputGrid[row, col] = int.Parse(rowValues[col]);
                }
            }
            return inputGrid;
        }
    }
}