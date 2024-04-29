using System.Text;
namespace Controllers.SudokuSolver;
public class Solver
{
    static bool IsValidBoard(char[][] board)
{
    foreach (char[] row in board)
    {
        if (row.Length != 9)
            return false;
        
        // Check for duplicate digits in the row
        HashSet<char> rowSet = new HashSet<char>();
        foreach (char digit in row)
        {
            if (!char.IsDigit(digit) && digit != '.')
                return false;
            
            if (digit != '.' && !rowSet.Add(digit))
                return false; // Duplicate digit found in the row
        }
    }

    // Check for duplicate digits in each column
    for (int col = 0; col < 9; col++)
    {
        HashSet<char> colSet = new HashSet<char>();
        for (int row = 0; row < 9; row++)
        {
            char digit = board[row][col];
            if (digit != '.' && !colSet.Add(digit))
                return false; // Duplicate digit found in the column
        }
    }

    // Check for duplicate digits in each 3x3 subgrid
    for (int startRow = 0; startRow < 9; startRow += 3)
    {
        for (int startCol = 0; startCol < 9; startCol += 3)
        {
            HashSet<char> subgridSet = new HashSet<char>();
            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    char digit = board[row][col];
                    if (digit != '.' && !subgridSet.Add(digit))
                        return false; // Duplicate digit found in the subgrid
                }
            }
        }
    }

    return true;
}

    static bool IsValidMove(char[][] board, int row, int col, int num)
    {
        // Check if the number is already in the row
        if (new string(board[row]).Contains(num.ToString()))
            return false;

        // Check if the number is already in the column
        for (int i = 0; i < 9; i++)
        {
            if (board[i][col] == num.ToString()[0])
                return false;
        }

        // Check if the number is already in the 3x3 box
        int startRow = 3 * (row / 3);
        int startCol = 3 * (col / 3);
        for (int i = startRow; i < startRow + 3; i++)
        {
            for (int j = startCol; j < startCol + 3; j++)
            {
                if (board[i][j] == num.ToString()[0])
                    return false;
            }
        }

        return true;
    }

    static Tuple<int, int> FindEmptyLocation(char[][] board)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i][j] == '.')
                    return Tuple.Create(i, j);
            }
        }
        return null;
    }

    static bool SolvePartialSudoku(char[][] board)
    {
        Tuple<int, int> emptyLocation = FindEmptyLocation(board);
        if (emptyLocation == null)
            return true; // The board is filled completely

        int row = emptyLocation.Item1;
        int col = emptyLocation.Item2;
        for (int num = 1; num <= 9; num++)
        {
            if (IsValidMove(board, row, col, num))
            {
                board[row][col] = num.ToString()[0];
                if (SolvePartialSudoku(board))
                    return true;
                board[row][col] = '.'; // Backtrack
            }
        }
        return false;
    }

    public string SolveSudoku(string sudokuStr)
    {
        char[][] board = new char[9][];
        for (int i = 0; i < 9; i++)
        {
            board[i] = sudokuStr.Substring(i * 9, 9).ToCharArray();
        }

        if (!IsValidBoard(board))
            return "invalid board";

        if (SolvePartialSudoku(board))
        {
            // Add final digits to the partial solve
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '.')
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsValidMove(board, i, j, num))
                            {
                                board[i][j] = num.ToString()[0];
                                break;
                            }
                        }
                    }
                }
            }
            StringBuilder solution = new StringBuilder();
            foreach (char[] row in board)
            {
                solution.Append(new string(row));
            }
            return solution.ToString().Trim();
        }
        else
        {
            return "no solution";
        }
    }
   
    public (bool isValid, string invalidCells) BoardValidator(string sudoku)
    {
        if (sudoku.Length != 81)
            throw new ArgumentException("Invalid input length. Expected 81 characters.");

        HashSet<string> invalidCells = new HashSet<string>();

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                char currentChar = sudoku[row * 9 + col];

                if (currentChar != '.')
                {
                    // Check rows and columns
                    for (int i = 0; i < 9; i++)
                    {
                        if ((i != row && sudoku[i * 9 + col] == currentChar) ||
                            (i != col && sudoku[row * 9 + i] == currentChar))
                        {
                            invalidCells.Add($"({row+1},{col+1})");
                            break;
                        }
                    }

                    // Check 3x3 grid
                    int startRow = row - row % 3;
                    int startCol = col - col % 3;
                    for (int i = startRow; i < startRow + 3; i++)
                    {
                        for (int j = startCol; j < startCol + 3; j++)
                        {
                            if (!(i == row && j == col) && sudoku[i * 9 + j] == currentChar)
                            {
                                invalidCells.Add($"({row+1},{col+1})");
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (invalidCells.Count == 0)
            return (true, "");
        else
            return (false, string.Join(" ", invalidCells));
    }
}



