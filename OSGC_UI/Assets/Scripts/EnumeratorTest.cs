using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class EnumeratorTest : ProcedureBase
{
    delegate void method();
    List<method> methods;
    List<method>.Enumerator currentMethod;
    bool finished = false;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        methods = new List<method>();
        methods.Add(Method1);
        methods.Add(Method2);
        methods.Add(Method3);
        currentMethod = methods.GetEnumerator();
        MoveNext();
    }

    public override void RunUpdate()
    {
        if (!finished)
        {
            currentMethod.Current();
        }
        else
        {
            EndProcedure(true);
        }
    }

    void MoveNext()
    {
        if (!currentMethod.MoveNext())
        {
            finished = true;
        }
    }

    void Method1()
    {
        Debug.Log("Hello");
        MoveNext();
    }

    void Method2()
    {
        Debug.Log("World!");
        MoveNext();
    }

    void Method3()
    {
        Debug.Log("Goodbye!");
        MoveNext();
    }
}
