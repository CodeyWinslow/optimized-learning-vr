
public class ProcedureReport
{
    ProcedureType _type;
    int _totalMoves;
    int _correctMoves;
    bool _successful;

    public ProcedureReport(ProcedureType type, int totalMoves, int correctMoves, bool successful)
    {
        _type = type;
        _totalMoves = totalMoves;
        _correctMoves = correctMoves;
        _successful = successful;
    }

    public ProcedureType Type { get { return _type; } }
    public int TotalMoves { get { return _totalMoves; } }
    public int CorrectMoves { get { return _correctMoves; } }
    public bool Successful { get { return _successful; } }
}
