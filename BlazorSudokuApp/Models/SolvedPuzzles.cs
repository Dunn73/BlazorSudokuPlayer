namespace Models.SolvedPuzzles;

public record Solved {
    public int? Id {get; set;}
    public (int hours, int minutes, int seconds)? HoursMinutesSeconds {get; set;}
    public int? HintsUsed {get; set;}

    public List<Solved> GenerateEmptySolvedData(){
        var emptySolvedData = new List<Solved>{
        };
        return emptySolvedData;
    }
    public void AddOrUpdateSolvedData(Solved newRecord, List<Solved> solvedData)
    {
        // Check if the new record has a duplicate ID
        var existingRecord = solvedData.FirstOrDefault(record => record.Id == newRecord.Id);

        if (existingRecord != null)
        {
            // Remove the existing record
            solvedData.Remove(existingRecord);
        }

        // Add the new record
        solvedData.Add(newRecord);
    }
}