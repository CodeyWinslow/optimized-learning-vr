using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class Reporter : MonoBehaviour
{

    public static Dictionary<ProcedureType,string> Strings = new Dictionary<ProcedureType, string>
    {
        { ProcedureType.SimpleProcedure, "Simple Procedure" },
        { ProcedureType.IntermediateProcedure, "Intermediate Procedure" },
        { ProcedureType.AdvancedProcedure, "Advanced Procedure" }
    };
    //list of all procedure reports
    List<ProcedureReport> reports;

    //file to write to
    [SerializeField]
    string logFilePath;

    public int ReportCount { get { return reports.Count; } }

    private void Awake()
    {
        reports = new List<ProcedureReport>();
    }

    public void Add(ProcedureReport rep)
    {
        reports.Add(rep);
    }

    public ProcedureReport GetReport(int index)
    {
        return reports[index];
    }

    public bool WriteReport()
    {
        if (logFilePath == "") return false;

        StreamWriter wr;

        try
        {
            wr = new StreamWriter(logFilePath, true);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }

        wr.WriteLine("===(" + DateTime.Now + ")===");

        foreach (ProcedureReport r in reports)
        {
            wr.WriteLine(Strings[r.Type]
                + ": \nCorrect Moves: "
                + r.CorrectMoves
                + "\nTotal Moves: "
                + r.TotalMoves
                + "\nSuccessful: "
                + (r.Successful ? "yes" : "no"));
        }

        return true;
    }
}
