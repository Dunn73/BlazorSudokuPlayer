namespace Models.Sudoku;
public class SudokuInfo {
        public int? Id {get; set;}
        public string? Puzzle { get; set; }
        public string? CurrentPuzzle { get; set;}
        public string? Solution { get; set; }
        public int? GivenNumbers { get; set; }
        public List<(int, int, char)>? userInputs {get; set;} = new List<(int, int, char)>();
        public List<(int, int, List<(char,bool)>)>? Notes {get; set;} = new List<(int, int, List<(char,bool)>)>();
        public SudokuInfo GenerateEmptySudoku(){
        var emptySudokuInfo = new SudokuInfo{
                Id = null,
                Puzzle = "",
                CurrentPuzzle = ".................................................................................",
                Solution = "",
                GivenNumbers = 0,
                userInputs = [],
                Notes = []
                };
        return emptySudokuInfo;
    }
    }