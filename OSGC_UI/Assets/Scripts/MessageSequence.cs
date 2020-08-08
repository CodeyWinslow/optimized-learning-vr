using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSequence : MonoBehaviour
{
    public delegate void MessageSequenceFinishedMethod();
    public event MessageSequenceFinishedMethod OnceSequenceFinished;

    public List<Message> messages;
    List<Message>.Enumerator currentMessage;

    void Awake()
    {
        if (messages != null)
        {
            currentMessage = messages.GetEnumerator();
            currentMessage.MoveNext();
        }

        foreach(Message m in messages)
        {
            m.onClick += NextMessage;
        }
    }

    public void Begin()
    {
        if (currentMessage.Current != null)
            currentMessage.Current.Show();
    }

    public void NextMessage(Message mess)
    {
        mess.Hide();
        if (!currentMessage.MoveNext())
        {
            if (OnceSequenceFinished != null)
                OnceSequenceFinished();
            return;
        }
        currentMessage.Current.Show();
    }

    public void Finish()
    {
        if (currentMessage.Current != null)
            currentMessage.Current.Hide();
    }    
}
