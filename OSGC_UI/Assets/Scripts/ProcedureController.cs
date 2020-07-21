using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIControlCenter))]
public class ProcedureController : MonoBehaviour
{
    UIControlCenter controls;
    List<ProcedureBase> procedures;
    public UIControlCenter Controls { get { return controls; } }
    
    int procIndex = 0;
    ProcedureBase currentProc;

    void Awake()
    {
        controls = GetComponent<UIControlCenter>();
    }
    // Start is called before the first frame update
    void Start()
    {
        procedures = new List<ProcedureBase>();
        procedures.Add(new SimpleProcedure());
        StartNextProcedure();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentProc != null)
        {
            if (currentProc.Finished)
            {
                if (currentProc.Success)
                    Debug.Log("Finished successfully!");
                else
                    Debug.Log("FAILED");

                StartNextProcedure();
            }

            if (currentProc != null)
                currentProc.RunUpdate();
        }
    }

    void StartNextProcedure()
    {
        if (procIndex < procedures.Count)
        {
            currentProc = procedures[procIndex++];
            currentProc.BeginProcedure(this);
        }
        else
        {
            Debug.Log("All procedures finished!");
            currentProc = null;
        }
    }
}
